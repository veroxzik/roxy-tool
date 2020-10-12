using ELFSharp.ELF;
using HidSharp;
using Roxy.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

        List<int> configPages;
        const int maxConfig = 5;

        bool waitingForBootloader;
        EventHandler<FlashEventArgs> flashElfEvent;

        bool isElfLoaded = false;

        class FlashEventArgs : EventArgs
        {
            public bool ForceDevice { get; set; }
        }

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

            Helper.StatusWrite = StatusWrite;
        }

        private void List_Changed(object sender, DeviceListChangedEventArgs e)
        {
            LookForDevices();
        }

        private void LookForDevices()
        {
            deviceListComboBox.Items.Clear();
            configPages = new List<int>();
            // Look for bootloader and for arcin
            bootloader = null;
            device = null;
            currentBoard = Board.None;
            SetFlashButtonStatus(false);
            
            foreach (var device in devList.GetHidDevices())
            {
                var deviceComboBoxItem = new DeviceSelectionComboxBoxItem() { Device = device };

                if (device.VendorID == 7504 && device.ProductID == 24704)
                {
                    if(device.GetProductName() != null && device.GetProductName().Contains("Roxy"))
                    {
                        deviceComboBoxItem.Board = Board.arcinRoxy;
                        deviceComboBoxItem.Name = "Arcin Roxy";
                    }
                    else
                    {
                        deviceComboBoxItem.Board = Board.arcin;
                        deviceComboBoxItem.Name = "Arcin";
                    }
                }
                else if (device.VendorID == 7504 && device.ProductID == 24708)
                {
                    deviceComboBoxItem.Board = Board.arcin;
                    deviceComboBoxItem.Name = "Arcin";
                    deviceComboBoxItem.IsBootLoader = true;
                }
                else if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0002)
                {
                    deviceComboBoxItem.Board = Board.Roxy;
                    deviceComboBoxItem.Name = "Roxy";
                }
                else if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0001)
                {
                    deviceComboBoxItem.Board = Board.Roxy;
                    deviceComboBoxItem.IsBootLoader = true;
                    deviceComboBoxItem.Name = "Roxy";
                }
                else if (device.VendorID == 0x1CCF && device.ProductID == 0x8048)
                {
                    deviceComboBoxItem.Board = Board.Roxy;
                    deviceComboBoxItem.Name = "IIDX premium controller (probably a Roxy)";
                }
                else if (device.VendorID == 0x1CCF && device.ProductID == 0x101C)
                {
                    deviceComboBoxItem.Board = Board.Roxy;
                    deviceComboBoxItem.Name = "SDVX NEMSYS entry controller (probably a Roxy)";
                }
                else
                {
                    continue;
                }
                
                deviceListComboBox.Items.Add(deviceComboBoxItem);
            }

            if (deviceListComboBox.Items.Count > 0)
            {
                deviceListComboBox.SelectedIndex = 0;
                deviceListComboBox_OnSelectionChangeCommited(this, new EventArgs());
            }
        }

        private void deviceListComboBox_OnSelectionChangeCommited(object sender, EventArgs e)
        {
            var selection = (DeviceSelectionComboxBoxItem) deviceListComboBox.SelectedItem;
            
            if (selection.IsBootLoader)
            {
                this.bootloader = selection.Device;
                if (waitingForBootloader)
                {
                    flashElfEvent?.Invoke(this, new FlashEventArgs());
                }
            }
            else
            {
                this.device = selection.Device;
                readConfigButton_Click(this, new EventArgs());
            }
            StatusWrite($"{selection.Name}{(selection.IsBootLoader ? " bootloader" : "")} selected!");

            SetFlashButtonStatus(isElfLoaded && selection.IsBootLoader);
            EnableConfigBox(!selection.IsBootLoader);
            SetBoard(selection.Board, !selection.IsBootLoader);
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
                configGroupBox.Text = $"Config Options ({currentBoard})";
                switch (currentBoard)
                {
                    case Board.arcin:
                        currentConfig = arcinConfigPanel;
                        arcinConfigPanel.Visible = true;
                        arcinRoxyControlPanel.Visible = false;
                        roxyConfigPanel.Visible = false;
                        break;
                    case Board.arcinRoxy:
                        currentConfig = arcinRoxyControlPanel;
                        arcinRoxyControlPanel.Visible = true;
                        arcinConfigPanel.Visible = false;
                        roxyConfigPanel.Visible = false;
                        break;
                    case Board.Roxy:
                        currentConfig = roxyConfigPanel;
                        roxyConfigPanel.Visible = true;
                        arcinConfigPanel.Visible = false;
                        arcinRoxyControlPanel.Visible = false;
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
                Thread.Sleep(100);
                waitingForBootloader = true;
            }
            else if (bootloader != null)
                flashElf(this, new FlashEventArgs());
        }

        void flashElf(object sender, FlashEventArgs e)
        {
            HidDevice flasher = null;
            if (bootloader != null)
                flasher = bootloader;
            else if (e.ForceDevice && device != null)
                flasher = device;

            if (flasher != null)
            {
                StatusWrite("Preparing flash...");
                HidStream hidStream;
                if (flasher.TryOpen(out hidStream))
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
                configPages.Clear();
                StatusWrite("Reading config...");
                HidStream hidStream;
                if (device.TryOpen(out hidStream))
                {
                    try
                    {
                        int attempts = 0;
                        while (attempts < maxConfig)
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
                                if (configBytes[1] == 0 && !configPages.Contains(0))
                                {
                                    currentConfig.PopulateControls(configBytes);
                                    configPages.Add(0);
                                }
                                else if (configBytes[1] == 1 && !configPages.Contains(1))
                                {
                                    currentConfig.PopulateRgbControls(configBytes);
                                    configPages.Add(1);
                                }
                                else if (configBytes[1] == 2 && !configPages.Contains(2))
                                {
                                    currentConfig.PopulateKeyMappingControls(configBytes);
                                    configPages.Add(2);
                                }
                                attempts++;
                            }
                        }
                        if (configPages.Count == 0)
                        {
                            StatusWrite("Failure to get any config reports.");
                        }
                        else
                        {
                            StatusWrite($"Found {configPages.Count} config reports.");
                        }
                    }
                    catch (Exception ex)
                    {
                        StatusWrite($"Failed to get config.{Environment.NewLine}- Has the board booted properly?{Environment.NewLine}- Is the correct board selected?");
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
                var mapBytes = currentConfig.GetKeyMappingBytes();

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
                        if (mapBytes != null)
                        {
                            hidStream.SetFeature(mapBytes, 0, 64);
                            StatusWrite("Key Mapping config written.");
                        }
                        hidStream.SetFeature(new byte[] { 0xb0, 0x20 });
                        StatusWrite("Restarting board!");
                        MessageBox.Show("Write success!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error writing config:\n {ex.Message}", "Error");
                    }
                }
                else
                {
                    StatusWrite("Could not open board!");
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
