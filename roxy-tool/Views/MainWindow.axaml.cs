using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using DynamicData;
using ELFSharp.ELF;
using HidSharp;
using roxy_tool.Classes;
using roxy_tool.Enums;
using roxy_tool.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roxy.Lib;
using System.Security.Cryptography;

namespace roxy_tool.Views
{
    public class MainWindow : Window, INotifyPropertyChanged
    {
        // Controls
        MenuItem licensesMenuItem;
        MenuItem scanMenuItem;

        DataGrid boardGrid;

        TabControl tabControl;

        Button loadElfButton;
        TextBlock elfFilenameText;
        Button flashElfButton;

        TextBlock consoleText;
        Border consoleContainer;

        Grid configTab;

        Grid configButtons;
        Button readConfigButton;
        Button writeConfigButton;

        ConfigPanel configPanel;

        KeyMappingControl keyMappingControl;
        ScrollViewer keyMappingScrollViewer;

        ButtonLedControl buttonLedControl;
        ScrollViewer buttonLedScrollViewer;

        HueColorSliderControl rgbControl;
        ScrollViewer rgbScrollViewer;

        DeviceControl deviceControl;
        ScrollViewer deviceScrollViewer;

        JoystickMappingControl joystickMappingControl;
        ScrollViewer joystickMappingScrollViewer;

        TurntableControl ttControl;
        ScrollViewer ttScrollViewer;

        // Flashing
        string elfFilePath;
        byte[] elfData = new byte[0];
        bool isElfLoaded = false;
        bool waitingForBootloader;
        string serialForBootloader;
        EventHandler<FlashEventArgs> flashElfEvent;

        // Devices
        BoardType currentBoard = BoardType.Roxy;

        DeviceList devList;

        RoxyDevice SelectedDevice { get { return ConnectedDevices.Count > 0 ? (RoxyDevice)boardGrid.SelectedItem : null; } }

        public ObservableCollection<RoxyDevice> ConnectedDevices { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.Configure();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Configure()
        {
            this.licensesMenuItem = this.FindControl<MenuItem>("licensesMenuItem");
            licensesMenuItem.Click += (async (s, e) => 
           {
               var window = new LicenseWindow();
               await window.ShowDialog(this);
           });
            this.scanMenuItem = this.FindControl<MenuItem>("scanMenuItem");
            scanMenuItem.Click += (s, e) => { LookForDevices(); };

            ConnectedDevices = new ObservableCollection<RoxyDevice>();
            this.boardGrid = this.FindControl<DataGrid>("boardGrid");
            boardGrid.DataContext = this;
            boardGrid.SelectionChanged += BoardGrid_SelectionChanged;

            this.tabControl = this.FindControl<TabControl>("tabControl");

            this.loadElfButton = this.FindControl<Button>("loadElfButton");
            loadElfButton.Click += LoadElfFile;
            this.elfFilenameText = this.FindControl<TextBlock>("elfFilenameText");
            this.flashElfButton = this.FindControl<Button>("flashElfButton");
            flashElfButton.Click += FlashElfButton;

            this.consoleText = this.FindControl<TextBlock>("consoleText");
            this.consoleContainer = this.FindControl<Border>("consoleContainer");

            this.configTab = this.FindControl<Grid>("configTab");

            this.configButtons = this.FindControl<Grid>("configButtons");
            this.readConfigButton = this.FindControl<Button>("readConfigButton");
            readConfigButton.Click += ReadConfigButton_Click;
            this.writeConfigButton = this.FindControl<Button>("writeConfigButton");
            writeConfigButton.Click += WriteConfigButton_Click;

            this.configPanel = this.FindControl<ConfigPanel>("configPanel");
            configPanel.SubPanelChanged += SubPanelChanged;

            this.rgbControl = this.FindControl<HueColorSliderControl>("rgbControl");
            rgbControl.OnClosed += rgbControl_Closed;
            this.rgbScrollViewer = this.FindControl<ScrollViewer>("rgbScrollViewer");

            this.keyMappingControl = this.FindControl<KeyMappingControl>("keyMappingControl");
            keyMappingControl.OnClosed += configControl_Closed;
            this.keyMappingScrollViewer = this.FindControl<ScrollViewer>("keyMappingScrollViewer");

            this.buttonLedControl = this.FindControl<ButtonLedControl>("buttonLedControl");
            buttonLedControl.OnClosed += configControl_Closed;
            this.buttonLedScrollViewer = this.FindControl<ScrollViewer>("buttonLedScrollViewer");

            this.deviceControl = this.FindControl<DeviceControl>("deviceControl");
            deviceControl.OnClosed += configControl_Closed;
            this.deviceScrollViewer = this.FindControl<ScrollViewer>("deviceScrollViewer");

            this.joystickMappingControl = this.FindControl<JoystickMappingControl>("joystickMappingControl");
            joystickMappingControl.OnClosed += configControl_Closed;
            this.joystickMappingScrollViewer = this.FindControl<ScrollViewer>("joystickMappingScrollViewer");

            this.ttControl = this.FindControl<TurntableControl>("ttControl");
            ttControl.OnClosed += configControl_Closed;
            this.ttScrollViewer = this.Find<ScrollViewer>("ttScrollViewer");

            var svre9left = this.FindControl<Button>("svre9Left");
            svre9left.Click += ((s, e) =>
            {
                SendCommand(1, 0);
            });
            var svre9right = this.FindControl<Button>("svre9Right");
            svre9right.Click += ((s, e) =>
            {
                SendCommand(1, 1);
            });

            flashElfEvent += flashElf;

            RoxyDevice.StatusWrite = delegate (string status)
            {
                StatusWrite(status);
            };

            devList = DeviceList.Local;
            devList.Changed += DevList_Changed;
            LookForDevices();
        }

        private void DevList_Changed(object sender, DeviceListChangedEventArgs e)
        {
            LookForDevices();
        }

        private void LookForDevices()
        {
            var devicesFound = new List<RoxyDevice>();
            SetFlashButtonStatus(false);
            foreach (var device in devList.GetHidDevices())
            {
                if (device.VendorID == 7504 && device.ProductID == 24704)
                {
                    if (device.GetProductName() != null && device.GetProductName().Contains("Roxy"))
                    {
                        var dev = new RoxyDevice(device, BoardType.arcinRoxy, false);
                        if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null && dev.IsConfigLoaded)
                        {
                            devicesFound.Add(dev);
                        }
                    }
                    else
                    {
                        var dev = new RoxyDevice(device, BoardType.arcin, false);
                        if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null && dev.IsConfigLoaded)
                        {
                            devicesFound.Add(dev);
                        }
                    }
                }
                else if (device.VendorID == 7504 && device.ProductID == 24708)
                {
                    var dev = new RoxyDevice(device, BoardType.arcin, true);
                    if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null)
                    {
                        devicesFound.Add(dev);
                    }

                    if (waitingForBootloader && dev.Device.GetSerialNumber() == serialForBootloader)
                        flashElfEvent?.Invoke(this, new FlashEventArgs(dev.Device));
                }
                else if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0002)
                {
                    var dev = new RoxyDevice(device, BoardType.Roxy, false);
                    if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null && dev.IsConfigLoaded)
                    {
                        devicesFound.Add(dev);
                    }
                }
                else if (device.VendorID == 0x16D0 && device.ProductID == 0x0F8B && device.ReleaseNumberBcd == 0x0001)
                {
                    var dev = new RoxyDevice(device, BoardType.Roxy, true);
                    if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null)
                    {
                        devicesFound.Add(dev);
                    }

                    if (waitingForBootloader && dev.Device.GetSerialNumber() == serialForBootloader)
                        flashElfEvent?.Invoke(this, new FlashEventArgs(dev.Device));

                }
                else if (device.VendorID == 0x1CCF && device.ProductID == 0x8048)
                {
                    var dev = new RoxyDevice(device, BoardType.Roxy, false, SpoofType.IIDX);
                    if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null && dev.IsConfigLoaded)
                    {
                        devicesFound.Add(dev);
                    }
                }
                else if (device.VendorID == 0x1CCF && device.ProductID == 0x101C)
                {
                    var dev = new RoxyDevice(device, BoardType.Roxy, false, SpoofType.SDVX);
                    if (devicesFound.FirstOrDefault(x => x.Equals(dev)) == null && dev.IsConfigLoaded)
                    {
                        devicesFound.Add(dev);
                    }
                }
            }

            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                var devToRemove = ConnectedDevices.Except(devicesFound);
                ConnectedDevices.RemoveMany(devToRemove);
                foreach (var dev in devicesFound)
                {
                    if (!ConnectedDevices.Contains(dev))
                    {
                        ConnectedDevices.Add(dev);
                    }
                }
                if (ConnectedDevices.Count == 0)
                {
                    configTab.IsEnabled = false;
                    StatusWrite("No devices found. If one is connected, please unplug it and try again.");
                }
                else
                {
                    StatusWrite($"Found {ConnectedDevices.Count} device(s). Select from the table above.");
                    SetFlashButtonStatus(isElfLoaded);
                    boardGrid.InvalidateVisual();
                }
            }));
        }

        private async void LoadElfFile(object sender, RoutedEventArgs args)
        {
            try
            {
                var files = await this.OpenFileBrowser();
                if (files.Count() != 0)
                {
                    elfData = new byte[0];
                    elfFilePath = files.First();
                    elfFilenameText.Text = elfFilePath;

                    using (var elf = ELFReader.Load(elfFilePath))
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
                        SetFlashButtonStatus(isElfLoaded && (SelectedDevice != null));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private void SetFlashButtonStatus(bool state)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { flashElfButton.IsEnabled = state; }));
        }

        private void FlashElfButton(object sender, RoutedEventArgs args)
        {
            if(SelectedDevice != null)
            {
                serialForBootloader = "";
                if(SelectedDevice.IsBootloader)
                {
                    flashElfEvent(this, new FlashEventArgs(SelectedDevice.Device));
                }
                else
                {
                    StatusWrite("Resetting to bootloader...");
                    HidStream hidStream;
                    if(SelectedDevice.Device.TryOpen(out hidStream))
                    {
                        serialForBootloader = SelectedDevice.Device.GetSerialNumber();
                        using (hidStream)
                        {
                            hidStream.SetFeature(new byte[] { 176, 0x10 });
                        }
                        Thread.Sleep(100);
                        waitingForBootloader = true;
                    }
                    else
                    {
                        StatusWrite("Failed to open device. Please unplug it and try again.");
                    }
                }
            }
        }

        private void flashElf(object sender, FlashEventArgs e)
        {
            if(e.Device != null)
            {
                StatusWrite("Preparing to flash...");
                HidStream hidStream;
                if(e.Device.TryOpen(out hidStream))
                {
                    using (hidStream)
                    {
                        StatusWrite("Writing flash...");
                        hidStream.SetFeature(new byte[] { 0, 0x20 });

                        byte[] output = new byte[65];
                        for (int i = 0; i < elfData.Length; i+= 64)
                        {
                            Array.Copy(elfData, i, output, 1, 64);
                            hidStream.Write(output);
                        }

                        StatusWrite("Closing flash...");
                        hidStream.SetFeature(new byte[] { 0, 0x21 });

                        StatusWrite("Resetting to application...");
                        hidStream.SetFeature(new byte[] { 0, 0x11 });
                    }

                    waitingForBootloader = false;
                    serialForBootloader = "";
                }
                else
                {
                    StatusWrite("Failed to open bootloader. Please unplug it and try again.");
                }
            }
        }

        private void SetBoard(BoardType board)
        {
            switch (board)
            {
                case BoardType.Roxy:
                    currentBoard = BoardType.Roxy;
                    break;
                case BoardType.arcinRoxy:
                    currentBoard = BoardType.arcinRoxy;
                    break;
                case BoardType.arcin:
                    currentBoard = BoardType.arcin;
                    break;
                default:
                    break;
            }
            configPanel.SetBoard(currentBoard);
        }

        private void BoardGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (boardGrid.SelectedItem == null)
            {
                configTab.IsEnabled = false;
                return;
            }

            var board = (RoxyDevice)boardGrid.SelectedItem;
            SetBoard(board.BoardType);
            if (board.IsBootloader)
            {
                configTab.IsEnabled = false;
            }
            else
            { 
                configTab.IsEnabled = true;
                UpdateControls();
            }
            SetFlashButtonStatus(isElfLoaded);
        }

        private void ReadConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var result = SelectedDevice?.ReadConfig();

            if (result == true)
            {
                UpdateControls();
            }
            else if (result == false)
            {
                StatusWrite("Failed to get config. Unplug and reconnect.");
            }
        }

        private void UpdateControls()
        {
            var dev = SelectedDevice;
            if (dev != null && dev.IsConfigLoaded)
            {
                switch (dev.BoardType)
                {
                    case BoardType.Roxy:
                    case BoardType.arcinRoxy:
                        configPanel.SetMapping(dev.StdConfig, dev.RgbConfig);
                        if (dev.KeyConfig != null)
                        {
                            keyMappingControl.SetMapping(dev.KeyConfig.KeyMapping);
                            buttonLedControl.SetMapping(dev.KeyConfig.LedMode);
                            joystickMappingControl.SetMapping(dev.KeyConfig.JoystickMapping);
                            ttControl.SetMapping(dev.RgbConfig.TurntableMapping);
                        }
                        if (dev.DeviceConfig != null)
                            deviceControl.SetMapping(dev.DeviceConfig.Data);
                        break;
                    case BoardType.arcin:
                        configPanel.SetMapping(dev.StdConfig, dev.RgbConfig);
                        break;
                    default:
                        break;
                }
            }
        }

        private void WriteConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var dev = SelectedDevice;
            if (dev != null && dev.IsConfigLoaded)
            {
                List<byte[]> configBytes = new List<byte[]>();

                // Get standard config bytes regardless
                var stdMapping = configPanel.GetStdMapping();
                byte[] stdBytes = new byte[64];
                stdBytes[0] = 0xc0; // Report ID
                stdBytes[1] = 0x00; // Standard config is Segment 0
                stdBytes[2] = (byte)stdMapping.Length;  // Length
                stdBytes[3] = 0x00; // Padding
                Array.Copy(stdMapping, 0, stdBytes, 4, stdMapping.Length);
                configBytes.Add(stdBytes);

                // Get additional bytes
                if (dev.BoardType == BoardType.Roxy || dev.BoardType == BoardType.arcinRoxy)
                {
                    // Get RGB bytes
                    var rgbMapping = configPanel.GetRgbMapping();
                    var ttMapping = ttControl.GetMapping();
                    byte[] rgbBytes = new byte[64];
                    rgbBytes[0] = 0xc0; // Report ID
                    rgbBytes[1] = 0x01; // RGB config is Segment 1
                    rgbBytes[2] = (byte)(rgbMapping.Length + ttMapping.Length); // Length
                    rgbBytes[3] = 0x00; // Padding
                    Array.Copy(rgbMapping, 0, rgbBytes, 4, rgbMapping.Length);
                    Array.Copy(ttMapping, 0, rgbBytes, 4 + rgbMapping.Length, ttMapping.Length);
                    configBytes.Add(rgbBytes);

                    // Get mapping bytes
                    var keyMapping = keyMappingControl.GetMapping();
                    var ledMapping = buttonLedControl.GetMapping();
                    var joyMapping = joystickMappingControl.GetMapping();
                    byte[] mappingBytes = new byte[64];
                    mappingBytes[0] = 0xc0; // Report ID
                    mappingBytes[1] = 0x02; // Key mapping config is Segment 2
                    mappingBytes[2] = (byte)(keyMapping.Length + joyMapping.Length + ledMapping.Length); // Length
                    mappingBytes[3] = 0x00; // Padding 
                    Array.Copy(keyMapping, 0, mappingBytes, 4, keyMapping.Length);
                    Array.Copy(joyMapping, 0, mappingBytes, 20, joyMapping.Length);
                    Array.Copy(ledMapping, 0, mappingBytes, 26, ledMapping.Length);
                    configBytes.Add(mappingBytes);

                    // Get device bytes
                    var devMapping = deviceControl.GetMapping();
                    byte[] devBytes = new byte[64];
                    devBytes[0] = 0xc0; // Report ID
                    devBytes[1] = 0x03; // Device mapping is Segement 3
                    devBytes[2] = (byte)devMapping.Length;  // Length
                    devBytes[3] = 0x00; // Padding
                    Array.Copy(devMapping, 0, devBytes, 4, devMapping.Length);
                    configBytes.Add(devBytes);
                }

                StatusWrite("Writing config...");
                HidStream hidStream;
                if(dev.Device.TryOpen(out hidStream))
                {
                    using (hidStream)
                    {
                        try
                        {
                            foreach (var bytes in configBytes)
                            {
                                hidStream.SetFeature(bytes, 0, 64);
                            }
                            StatusWrite($"Config written: {configBytes.Count} pages");
                            hidStream.SetFeature(new byte[] { 0xb0, 0x20 });
                            StatusWrite("Restarting board!");
                        }
                        catch (Exception ex)
                        {
                            StatusWrite($"Error writing config:\n {ex.Message}");
                        }
                    }
                }
                else
                {
                    StatusWrite("Failed to open the device. Unplug and reconnect.");
                }
            }
        }

        private void SendCommand(uint commandID, uint data)
        {
            var dev = SelectedDevice;
            if (dev != null && dev.IsConfigLoaded)
            {
                var bytes = new byte[7];
                bytes[0] = 0xd0;    // Report ID
                Array.Copy(BitConverter.GetBytes((UInt16)commandID), 0, bytes, 1, 2);
                Array.Copy(BitConverter.GetBytes(data), 0, bytes, 3, 4);

                HidStream hidStream;
                if (dev.Device.TryOpen(out hidStream))
                {
                    using (hidStream)
                    {
                        try
                        {
                            hidStream.SetFeature(bytes);
                        }
                        catch (Exception ex)
                        {
                            StatusWrite($"Error sending command:\n {ex.Message}");
                        }
                    }
                }
                else
                {
                    StatusWrite("Failed to open the device. Unplug and reconnect.");
                }
            }
        }

        private void SubPanelChanged(object sender, ConfigPanelEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                configButtons.IsEnabled = false;
                configPanel.IsVisible = false;
                switch (e.SubPanel)
                {
                    case ControlSubPanel.RGB1:
                        rgbControl.SetHue(0, e.Value);
                        rgbScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.RGB2:
                        rgbControl.SetHue(1, e.Value);
                        rgbScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.KeyMapping:
                        keyMappingScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.ButtonLedMode:
                        buttonLedScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.DeviceControl:
                        deviceScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.JoystickMapping:
                        joystickMappingScrollViewer.IsVisible = true;
                        break;
                    case ControlSubPanel.TurntableControl:
                        ttScrollViewer.IsVisible = true;
                        break;
                    default:
                        break;
                }
            }));
        }

        private void rgbControl_Closed(object sender, ByteEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                configButtons.IsEnabled = true;
                configPanel.IsVisible = true;
                if(e.Index != -1)
                    configPanel.SetColor(e.Index, e.Value);
            }));
        }

        private void configControl_Closed(object sender, EventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                configButtons.IsEnabled = true;
                configPanel.IsVisible = true;
            }));
        }

        private async Task<string[]> OpenFileBrowser()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select an ELF file",
                AllowMultiple = true,
                Filters = new List<FileDialogFilter>() { new FileDialogFilter() { Name = "ELF Files (*.elf)", Extensions = new List<string>() { "*" } } }
            };
            return await dialog.ShowAsync(GetWindow());
        }

        Window GetWindow() => (Window)this.VisualRoot;

        public void StatusWrite(string line)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                consoleText.Text += line + Environment.NewLine;
            }));
        }
    }
}
