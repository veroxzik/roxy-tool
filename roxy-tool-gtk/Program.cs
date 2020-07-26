using Gtk;
using System;
using System.Collections;
using System.Collections.Generic;
using HidSharp;
using System.Linq;
using ELFSharp.ELF;
using roxy_tool;
using System.Runtime.CompilerServices;
using Roxy.Lib;

namespace gtksharp_test
{
    static class Program
    {
        static TextBuffer consoleText;
        static EntryBuffer elfString = new EntryBuffer("No File Loaded", 255);
        static List<Widget> flashEnabledWidgets = new List<Widget>();

        static HidDevice device;
        static HidDevice bootloader;

        static DeviceList devList;

        static Board currentBoard = Board.None;
        static IConfigPanel currentConfig;

        static RoxyConfigPanel roxyConfigPanel = new RoxyConfigPanel();
        static ArcinConfigPanel arcinConfigPanel = new ArcinConfigPanel();
        static ArcinRoxyConfigPanel arcinRoxyConfigPanel = new ArcinRoxyConfigPanel();
        static HeaderBar configHeaderBar;

        static bool waitingForBootloader = false;
        static byte[] elfData = new byte[0];
        static bool isElfLoaded = false;

        static DateTime lastDeviceGet = DateTime.Now;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Init();
            Window window = new Window("Roxy Configuration and Flash Tool");
            window.SetIconFromFile("logo-icon.png");
            window.Resize(500, 600);
            window.DeleteEvent += delegate { Application.Quit(); };           

            // Menu bar
            MenuBar menuBar = new MenuBar();
            MenuItem boardSelectItem = new MenuItem("Board Select");
            menuBar.Add(boardSelectItem);
            Menu boardMenu = new Menu();
            boardSelectItem.Submenu = boardMenu;
            RadioMenuItem roxyItem = new RadioMenuItem("Roxy");
            roxyItem.Active = true;
            roxyItem.Toggled += BoardItem_Toggled;
            RadioMenuItem arcinItem = new RadioMenuItem("arcin");
            arcinItem.JoinGroup(roxyItem);
            boardMenu.Add(roxyItem);
            boardMenu.Add(arcinItem);
            MenuItem aboutItem = new MenuItem("About");
            menuBar.Add(aboutItem);
            Menu aboutMenu = new Menu();
            aboutItem.Submenu = aboutMenu;
            MenuItem licensesItem = new MenuItem("Licenses");
            licensesItem.ButtonPressEvent += LicensesItem_ButtonPressEvent;
            aboutMenu.Add(licensesItem);

            // Notebook

            // Config page
            Button readConfigButton = new Button("Read Config");
            readConfigButton.ButtonReleaseEvent += ReadConfigButton_ButtonReleaseEvent;
            Button writeConfigButton = new Button("Write Config");
            writeConfigButton.ButtonReleaseEvent += WriteConfigButton_ButtonReleaseEvent;
            HBox configButtonBox = new HBox(true, 2);
            configButtonBox.PackStart(readConfigButton, false, true, 2);
            configButtonBox.PackStart(writeConfigButton, false, true, 2);
            HeaderBar configHeader = new HeaderBar();
            configHeader.Title = "Config Options";
            configHeader.Subtitle = "Roxy";
            configHeaderBar = configHeader;

            Grid configMainGrid = new Grid();
            configMainGrid.ColumnHomogeneous = true;
            configMainGrid.Attach(configButtonBox, 0, 0, 1, 1);
            configMainGrid.Attach(configHeader, 0, 1, 1, 1);
            configMainGrid.Attach(roxyConfigPanel.OptionsGrid, 0, 2, 1, 1);
            configMainGrid.Attach(arcinConfigPanel.OptionsGrid, 0, 2, 1, 1);
            configMainGrid.Attach(arcinRoxyConfigPanel.OptionsGrid, 0, 2, 1, 1);

            // Flash page
            Label filenameLabel = new Label("Select an ELF File:");
            filenameLabel.Halign = Align.Start;
            filenameLabel.Xpad = 10;
            FileChooserButton elfFileButton = new FileChooserButton("Browse for ELF File", FileChooserAction.Open);
            elfFileButton.FileSet += ElfFileButton_FileSet;
            Button flashButton = new Button("Flash to Chip");
            flashButton.Clicked += FlashButton_Clicked;
            flashButton.Sensitive = false;
            flashEnabledWidgets.Add(flashButton);
            Button deviceCheck = new Button("Check for Devices");
            deviceCheck.Clicked += delegate { LookForDevices(); };
            VBox flashBox = new VBox(false, 2);
            flashBox.PackStart(filenameLabel, false, true, 2);
            flashBox.PackStart(elfFileButton, false, true, 2);
            flashBox.PackStart(flashButton, false, true, 2);
            flashBox.PackStart(deviceCheck, false, true, 2);

            Notebook mainNotebook = new Notebook();
            mainNotebook.AppendPage(configMainGrid, new Label("Config"));
            mainNotebook.AppendPage(flashBox, new Label("Flash"));

            // Console log
            TextView consoleTextBox = new TextView();
            consoleTextBox.Editable = false;
            consoleTextBox.CursorVisible = false;
            consoleTextBox.WidthRequest = 200;
            consoleTextBox.WrapMode = WrapMode.Word;
            consoleTextBox.LeftMargin = 5;
            consoleTextBox.RightMargin = 5;
            consoleTextBox.TopMargin = 5;
            consoleTextBox.BottomMargin = 5;
            consoleText = consoleTextBox.Buffer;

            // Body content
            HBox bodyBox = new HBox(false, 2);
            bodyBox.PackStart(mainNotebook, true, true, 2);
            bodyBox.PackStart(consoleTextBox, false, true, 2);

            // Overall page
            VBox vBox = new VBox(false, 2);
            vBox.PackStart(menuBar, false, false, 0);
            vBox.PackStart(bodyBox, true, true, 0);

            // Window
            window.Add(vBox);
            window.ShowAll();

            // Setup HID items
            devList = DeviceList.Local;
            devList.Changed += DevList_Changed;
            LookForDevices();

            // Hide arcin panel
            arcinConfigPanel.OptionsGrid.Visible = false;

            Application.Run();
        }

        private static void ReadConfigButton_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            if (device != null && currentConfig != null)
            {
                StatusWrite("Reading config...");
                HidStream hidStream;
                if (device.TryOpen(out hidStream))
                {
                    try
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
                    catch (Exception ex)
                    {
                        StatusWrite("Exception thrown trying to run GetFeature. Exception: " + ex.Message);
                    }
                }
            }
        }

        private static void WriteConfigButton_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
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
                        StatusWrite("Write Success!");
                    }
                    catch (Exception ex)
                    {
                        StatusWrite("ERROR: Could not write config. Exception: " + ex.Message);
                    }
                }
                else
                {
                    StatusWrite("Could not open device!");
                }
            }
        }

        private static void LicensesItem_ButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            // License window
            Window licenseWindow = new Window("Licenses");
            licenseWindow.SetIconFromFile("logo-icon.png");
            licenseWindow.Resize(300, 300);
            licenseWindow.Resizable = false;
            Label licenseLabel = new Label();
            licenseLabel.Text = @"Roxy Configuration and Flash Tool
Copyright (c) 2020, Verox Zik

arcin firmware, original configuration tool
Copyright (c) 2013, Vegard Storheil Eriksen
All rights reserved.

GtkSharp
Used under the LGPL v2.0 License.
<https://github.com/GtkSharp/GtkSharp>

ELFSharp
Copyright (c) 2011 Konrad Kruczyński and other contributors
<https://github.com/konrad-kruczynski/elfsharp>

HIDSharp
Copyright 2010-2019 James F. Bellinger
<http://www.zer7.com/software/hidsharp>

Special Thanks:
theKeithD's python bootloader script
<https://github.com/theKeithD/arcin>";
            licenseLabel.Xpad = 10;
            licenseLabel.Ypad = 10;
            licenseWindow.Add(licenseLabel);

            licenseWindow.ShowAll();
        }

        private static void FlashButton_Clicked(object sender, EventArgs e)
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
                        waitingForBootloader = true;
                    }
                    else
                    {
                        StatusWrite("Failed to open HID device. Check permissions.");
                    }
                }
                
            }
            else if (bootloader != null)
                FlashElf();
        }

        private static void ElfFileButton_FileSet(object sender, EventArgs e)
        {
            var fileButton = (FileChooserButton)sender;

            elfData = new byte[0];

            using (var elf = ELFReader.Load(fileButton.Filename))
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
                SetFlashWidgets(isElfLoaded && (device != null || bootloader != null));
            }
        }

        private static void FlashElf()
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

        private static void DevList_Changed(object sender, DeviceListChangedEventArgs e)
        {
            LookForDevices();
        }

        private static void LookForDevices()
        {
            // Look for bootloader and for arcin
            bootloader = null;
            device = null;
            currentBoard = Board.None;
            SetFlashWidgets(false);
            var hidDevices = devList.GetHidDevices().ToArray();
            foreach (var dev in devList.GetHidDevices())
            {
                if (dev.VendorID == 7504 && dev.ProductID == 24704)
                {
                    device = dev;
                    if (device.GetProductName().Contains("Roxy"))
                    {
                        StatusWrite("arcin (Roxy firmware) found!");
                        SetBoard(Board.arcinRoxy, true);
                    }
                    else
                    {
                        StatusWrite("arcin found!");
                        SetBoard(Board.arcin, true);
                    }
                    SetFlashWidgets(isElfLoaded);
                    return;
                }
                if (dev.VendorID == 7504 && dev.ProductID == 24708)
                {
                    bootloader = dev;
                    StatusWrite("arcin bootloader found!");
                    SetFlashWidgets(isElfLoaded);
                    //EnableConfigBox(false);

                    if (waitingForBootloader)
                        FlashElf();

                    return;
                }
                if (dev.VendorID == 0x16D0 && dev.ProductID == 0x0F8B && dev.ReleaseNumberBcd == 0x0002)
                {
                    device = dev;
                    StatusWrite("Roxy found!");
                    SetFlashWidgets(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    ReadConfigButton_ButtonReleaseEvent(null, new ButtonReleaseEventArgs());
                    return;
                }
                if (dev.VendorID == 0x16D0 && dev.ProductID == 0x0F8B && dev.ReleaseNumberBcd == 0x0001)
                {
                    bootloader = dev;
                    StatusWrite("Roxy bootloader found!");
                    SetFlashWidgets(isElfLoaded);
                    //EnableConfigBox(false);

                    if (waitingForBootloader)
                        FlashElf();

                    return;
                }
                if (dev.VendorID == 0x1CCF && dev.ProductID == 0x8048)
                {
                    device = dev;
                    StatusWrite("IIDX premium controller (probably a Roxy) found!");
                    SetFlashWidgets(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    ReadConfigButton_ButtonReleaseEvent(null, new ButtonReleaseEventArgs());
                    return;
                }
                if (dev.VendorID == 0x1CCF && dev.ProductID == 0x101C)
                {
                    device = dev;
                    StatusWrite("SDVX NEMSYS entry controller (probably a Roxy) found!");
                    SetFlashWidgets(isElfLoaded);
                    SetBoard(Board.Roxy, true);
                    ReadConfigButton_ButtonReleaseEvent(null, new ButtonReleaseEventArgs());
                    return;
                }
            }
            //EnableConfigBox(false);
        }

        private static void SetFlashWidgets(bool status)
        {
            foreach (var widget in flashEnabledWidgets)
            {
                widget.Sensitive = status;
            }
        }

        private static void BoardItem_Toggled(object sender, EventArgs e)
        {
            var radio = (RadioMenuItem)sender;

            SetBoard(radio.Active ? Board.Roxy : Board.arcin, false);
        }

        private static void SetBoard(Board board, bool deviceFound)
        {
            switch (board)
            {
                case Board.None:
                    break;
                case Board.arcin:
                    currentConfig = arcinConfigPanel;
                    arcinConfigPanel.OptionsGrid.Visible = true;
                    arcinRoxyConfigPanel.OptionsGrid.Visible = false;
                    roxyConfigPanel.OptionsGrid.Visible = false;
                    configHeaderBar.Subtitle = "arcin";
                    break;
                case Board.Roxy:
                    currentConfig = roxyConfigPanel;
                    arcinConfigPanel.OptionsGrid.Visible = false;
                    arcinRoxyConfigPanel.OptionsGrid.Visible = false;
                    roxyConfigPanel.OptionsGrid.Visible = true;
                    configHeaderBar.Subtitle = "Roxy";
                    break;
                case Board.arcinRoxy:
                    currentConfig = arcinRoxyConfigPanel;
                    arcinRoxyConfigPanel.OptionsGrid.Visible = true;
                    arcinConfigPanel.OptionsGrid.Visible = false;
                    roxyConfigPanel.OptionsGrid.Visible = false;
                    configHeaderBar.Subtitle = "arcin (Roxy Firmware)";
                    break;
                default:
                    break;
            }
        }

        private static void StatusWrite(string text)
        {
            consoleText.Text += text + Environment.NewLine;
        }
        
    }
}
