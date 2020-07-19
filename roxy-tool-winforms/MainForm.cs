using ELFSharp.ELF;
using HidSharp;
using Roxy.Lib;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Roxy.Tool.WinForms
{
    public partial class MainForm : Form
    {
        OpenFileDialog loadElf = new OpenFileDialog();
        byte[] elfData = new byte[0];

        HidDevice device;
        HidDevice bootloader;

        DeviceList devList;

        Board currentBoard = Board.None;
        IConfigPanel currentConfig;

        bool waitingForBootloader;
        EventHandler flashElfEvent;

        bool isElfLoaded = false;

        public MainForm()
        {
            InitializeComponent();

            devList = DeviceList.Local;
            devList.Changed += List_Changed;
            LookForDevices();

            flashElfEvent += flashElf;

#if ARCIN_BUILD
            this.Text = "arcin Flash and Configuration Tool";
#endif
        }

        private void List_Changed(object sender, DeviceListChangedEventArgs e)
        {
            LookForDevices();
        }

        private void LookForDevices()
        {
            // Look for bootloader and for arcin
            bootloader = null;
            device = null;
            currentBoard = Board.None;
            SetFlashButtonStatus(false);
            var hidDevices = devList.GetHidDevices().ToArray();
            foreach (var device in devList.GetHidDevices())
            {
                if (device.VendorID == 7504 && device.ProductID == 24704)
                {
                    this.device = device;
                    if(device.GetProductName().Contains("Roxy"))
                    {
                        StatusWrite("arcin (Roxy firmware) found!");
                        SetBoard(Board.arcinRoxy, true);
                    }
                    else
                    {
                        StatusWrite("arcin found!");
                        SetBoard(Board.arcin, true);
                    }
                    SetFlashButtonStatus(isElfLoaded);
                    readConfigButton_Click(this, new EventArgs());
                    return;
                }
                if (device.VendorID == 7504 && device.ProductID == 24708)
                {
                    bootloader = device;
                    StatusWrite("arcin bootloader found!");
                    SetFlashButtonStatus(isElfLoaded);
                    EnableConfigBox(false);

                    if (waitingForBootloader)
                        flashElfEvent?.Invoke(this, new EventArgs());

                    return;
                }
                if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0002)
                {
                    this.device = device;
                    StatusWrite("Roxy found!");
                    SetFlashButtonStatus(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    readConfigButton_Click(this, new EventArgs());
                    return;
                }
                if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0001)
                {
                    bootloader = device;
                    StatusWrite("Roxy bootloader found!");
                    SetFlashButtonStatus(isElfLoaded);
                    EnableConfigBox(false);

                    if (waitingForBootloader)
                        flashElfEvent?.Invoke(this, new EventArgs());

                    return;
                }
                if (device.VendorID == 0x1CCF && device.ProductID == 0x8048)
                {
                    this.device = device;
                    StatusWrite("IIDX premium controller (probably a Roxy) found!");
                    SetFlashButtonStatus(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    readConfigButton_Click(this, new EventArgs());
                    return;
                }
                if (device.VendorID == 0x1CCF && device.ProductID == 0x101C)
                {
                    this.device = device;
                    StatusWrite("SDVX NEMSYS entry controller (probably a Roxy) found!");
                    SetFlashButtonStatus(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    readConfigButton_Click(this, new EventArgs());
                    return;
                }
            }
            EnableConfigBox(false);
        }

        void SetFlashButtonStatus(bool state)
        {
            flashButton.InvokeIfRequired(() =>
            {
                flashButton.Enabled = state;
            });
        }

        void EnableConfigBox(bool state)
        {
            readConfigButton.InvokeIfRequired(() =>
            {
                readConfigButton.Enabled = state;
            });
            writeConfigButton.InvokeIfRequired(() =>
            {
                writeConfigButton.Enabled = state;
            });
            configGroupBox.InvokeIfRequired(() =>
            {
                configGroupBox.Enabled = state;
                configGroupBox.Text = "Config Options (" + currentBoard.ToString() + ")";
                switch (currentBoard)
                {
                    case Board.arcin:
                        currentConfig = arcinConfigPanel;
                        arcinConfigPanel.Visible = true;
                        roxyConfigPanel.Visible = false;
                        break;
                    default:
                        currentConfig = roxyConfigPanel;
                        roxyConfigPanel.Visible = true;
                        arcinConfigPanel.Visible = false;
                        break;
                }
            });
        }

        void StatusWrite(string text)
        {
            statusText.InvokeIfRequired(() =>
            {
                statusText.AppendText(text + Environment.NewLine);
            });
        }

        private void loadElfButton_Click(object sender, EventArgs e)
        {
            if (loadElf.ShowDialog() == DialogResult.OK)
            {
                elfData = new byte[0];
                filenameText.Text = Path.GetFileName(loadElf.FileName);

                using (var elf = ELFReader.Load(loadElf.FileName))
                {
                    // Sections that are relevant:
                    // Section 1, 2, 3, 4
                    // TODO: Remove hardcode

                    for (int i = 1; i < 5; i++)
                    {
                        int offset = elfData.Length;
                        var data = elf.Sections[i].GetContents();
                        Array.Resize(ref elfData, elfData.Length + data.Length);
                        data.CopyTo(elfData, offset);
                    }

                    float num = (float)elfData.Length / 64;
                    double frac = num - Math.Floor(num);
                    int padding = (int)((1.0f - frac) * 64);
                    Array.Resize(ref elfData, elfData.Length + padding);

                    StatusWrite("ELF file loaded!");
                    isElfLoaded = true;
                    SetFlashButtonStatus(isElfLoaded && (device != null || bootloader != null));
                }
            }
        }

        private void flashButton_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                // Reset to bootloader
                StatusWrite("Resetting to bootloader...");
                if (device != null)
                {
                    HidStream hidStream;
                    if (device.TryOpen(out hidStream))
                    {
                        using (hidStream)
                        {
                            var buf = new byte[] { 176, 0x10 };
                            hidStream.SetFeature(buf, 0, 2);
                        }
                    }
                }
                waitingForBootloader = true;
            }
            else if (bootloader != null)
                flashElf(this, new EventArgs());
        }

        void flashElf(object sender, EventArgs e)
        {
            if (bootloader != null)
            {
                StatusWrite("Preparing flash...");
                HidStream hidStream;
                if (bootloader.TryOpen(out hidStream))
                {
                    using (hidStream)
                    {
                        StatusWrite("Writing flash...");
                        var buf = new byte[] { 0, 0x20 };
                        hidStream.SetFeature(buf);

                        byte[] output = new byte[65];
                        for (int i = 0; i < elfData.Length; i += 64)
                        {
                            Array.Copy(elfData, i, output, 1, 64);
                            hidStream.Write(output);
                        }

                        StatusWrite("Closing flash...");
                        buf = new byte[] { 0, 0x21 };
                        hidStream.SetFeature(buf);

                        StatusWrite("Resetting to application...");
                        buf = new byte[] { 0, 0x11 };
                        hidStream.SetFeature(buf);
                    }

                    waitingForBootloader = false;
                }
                else
                {
                    StatusWrite("Failed to open bootloader.");
                }

            }
            else
            {
                StatusWrite("Bootloader not found!");
            }
        }

        private void readConfigButton_Click(object sender, EventArgs e)
        {
            if (device != null && currentConfig != null)
            {
                StatusWrite("Reading config...");
                HidStream hidStream;
                if (device.TryOpen(out hidStream))
                {
                    bool config0 = false;
                    bool config1 = currentBoard != Board.Roxy;
                    int attempts = 0;
                    while ((!config0 || !config1) && attempts < 5)
                    {
                        byte[] configBytes = new byte[64];
                        configBytes[0] = 0xc0;
                        hidStream.GetFeature(configBytes);
                        if (configBytes[0] != 0xc0)
                        {
                            StatusWrite("Mismatch in config report ID.");
                            return;
                        }
                        else
                        {
                            if (configBytes[1] == 0)
                            {
                                currentConfig.PopulateControls(configBytes);
                                config0 = true;
                            }
                            else if (configBytes[1] == 1)
                            {
                                currentConfig.PopulateRgbControls(configBytes);
                                config1 = true;
                            }
                            attempts++;
                        }
                    }
                    if (attempts >= 5)
                    {
                        StatusWrite("Failure to get all config reports. Is the correct firmware flashed?");
                    }
                }
            }
        }

        private void writeConfigButton_Click(object sender, EventArgs e)
        {
            if (device != null && currentConfig != null)
            {
                StatusWrite("Writing config...");
                var configBytes = currentConfig.GetConfigBytes();
                var rgbBytes = currentConfig.GetRgbConfigBytes();

                HidStream hidStream;
                if (device.TryOpen(out hidStream))
                {
                    try
                    {
                        hidStream.SetFeature(configBytes, 0, 64);
                        StatusWrite("Config written.");
                        if (rgbBytes != null)
                        {
                            hidStream.SetFeature(rgbBytes, 0, 64);
                            StatusWrite("RGB config written.");
                        }
                        hidStream.SetFeature(new byte[] { 0xb0, 0x20 });
                        StatusWrite("Restarting board!");
                        MessageBox.Show("Write success!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error writing config:\n" + ex.Message, "Error");
                    }
                }
                else
                {
                    StatusWrite("Could not open arcin!");
                }
            }
        }

        private void forceRoxyButton_Click(object sender, EventArgs e)
        {
            currentBoard = Board.Roxy;
            EnableConfigBox(true);
        }

        private void licensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseForm lf = new LicenseForm();
            lf.ShowDialog();
        }

        private void roxySelectMenuItem_Click(object sender, EventArgs e)
        {
            SetBoard(Board.Roxy, device != null);
        }

        private void arcinSelectMenuItem_Click(object sender, EventArgs e)
        {
            SetBoard(Board.arcin, device != null);
        }

        private void arcinRoxySelectMenuItem_Click(object sender, EventArgs e)
        {
            SetBoard(Board.arcinRoxy, device != null);
        }

        private void SetBoard(Board board, bool configEnabled)
        {
            currentBoard = board;
            switch (currentBoard)
            {
                case Board.arcin:
                    this.InvokeIfRequired(() =>
                    {
                        arcinSelectMenuItem.Checked = true;
                        arcinRoxySelectMenuItem.Checked = false;
                        roxySelectMenuItem.Checked = false;
                    });
                    EnableConfigBox(configEnabled);
                    break;
                case Board.Roxy:
                    this.InvokeIfRequired(() =>
                    {
                        roxySelectMenuItem.Checked = true;
                        arcinSelectMenuItem.Checked = false;
                        arcinRoxySelectMenuItem.Checked = false;
                    });
                    EnableConfigBox(configEnabled);
                    break;
                case Board.arcinRoxy:
                    this.InvokeIfRequired(() =>
                    {
                        arcinRoxySelectMenuItem.Checked = true;
                        arcinSelectMenuItem.Checked = false;
                        roxySelectMenuItem.Checked = false;
                    });
                    EnableConfigBox(configEnabled);
                    break;
                default:
                    break;
            }
        }
    }
}
