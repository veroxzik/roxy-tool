using HidSharp;
using Roxy.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace roxy_tool.Classes
{
    public class RoxyDevice
    {
        private Dictionary<int, byte[]> configBytes = new Dictionary<int, byte[]>();
        private static int maxConfig = 5;

        public BoardType BoardType { get; private set; }
        public string BoardTypeString
        {
            get
            {
                switch (BoardType)
                {
                    case BoardType.Roxy:
                        return "Roxy";
                    case BoardType.arcinRoxy:
                        return "arcin (Roxy FW)";
                    case BoardType.arcin:
                        return "arcin";
                }
                return string.Empty;
            }
        }
        public HidDevice Device { get; private set; }
        public bool IsBootloader { get; private set; }
        public string SerialNumber { get { return Device?.GetSerialNumber(); } }
        public string FirmwareVersion { get; private set; }
        public BoardVersion BoardVersion { get; private set; }
        public string BoardVersionString
        {
            get
            {
                switch (BoardVersion)
                {
                    case BoardVersion.RoxyV1_1:
                        return "v1.1";
                    case BoardVersion.RoxyV2_0:
                        return "v2.0";
                    default:
                        return "Unknown";
                }
            }
        }
        public string BoardName
        {
            get
            {
                string name = "";
                switch (BoardType)
                {
                    case BoardType.Roxy:
                        name = "Roxy";
                        break;
                    case BoardType.arcinRoxy:
                        name = "arcin (Roxy FW)";
                        break;
                    case BoardType.arcin:
                        name = "arcin";
                        break;
                    default:
                        break;
                }
                if (StdConfig?.Label.Length > 0)
                    name += $" [{StdConfig.Label}]";
                switch (SpoofType)
                {
                    case SpoofType.IIDX:
                        name += " (IIDX Spoof)";
                        break;
                    case SpoofType.SDVX:
                        name += " (SDVX Spoof)";
                        break;
                }
                if (IsBootloader)
                    name += " bootloader";

                return name;
            }
        }
        public SpoofType SpoofType { get; private set; }
        public bool IsConfigLoaded { get { return configBytes.Count > 0; } }
        public StandardConfiguration StdConfig { get; private set; }
        public RgbConfiguration RgbConfig { get; private set; }
        public KeyMappingConfiguration KeyConfig { get; private set; }
        public DeviceConfiguration DeviceConfig { get; private set; }
        public int NumConfigPages { get; private set; }

        public static Action<string> StatusWrite { get; set; }

        public RoxyDevice(HidDevice device, BoardType type, bool isBootloader, SpoofType spoof = SpoofType.None)
        {
            Device = device;
            BoardType = type;
            IsBootloader = isBootloader;
            SpoofType = spoof;

            // Attempt to load config as soon as any device is discovered
            if(!IsBootloader)
            {
                ReadConfig();
                GetFirmwareVersion();
                GetBoardVersion();
            }
        }

        public bool ReadConfig()
        {
            configBytes.Clear();
            HidStream hidStream;
            if(Device.TryOpen(out hidStream))
            {
                using (hidStream)
                {
                    try
                    {
                        int attempts = 0;
                        while(attempts < maxConfig)
                        {
                            byte[] config = new byte[64];
                            config[0] = 0xc0;
                            hidStream.GetFeature(config);
                            if (config[0] != 0xc0)
                            {
                                //StatusWrite("Mismatch in config report ID");
                                attempts++;
                            }
                            else
                            {
                                if(!configBytes.ContainsKey(config[1]))
                                {
                                    configBytes.Add(config[1], config);
                                    switch (config[1])
                                    {
                                        case 0:
                                            StdConfig = new StandardConfiguration(config);
                                            break;
                                        case 1:
                                            RgbConfig = new RgbConfiguration(config);
                                            break;
                                        case 2:
                                            KeyConfig = new KeyMappingConfiguration(config);
                                            break;
                                        case 3:
                                            DeviceConfig = new DeviceConfiguration(config);
                                            break;
                                    }
                                }
                                attempts++;
                            }
                        }
                        NumConfigPages = configBytes.Count;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool GetFirmwareVersion()
        {
            FirmwareVersion = "Unknown";
            HidStream hidStream;
            if (Device.TryOpen(out hidStream))
            {
                using (hidStream)
                {
                    try
                    {
                        byte[] version = new byte[64];
                        version[0] = 0xa0;
                        hidStream.GetFeature(version);
                        if (version[0] == 0xa0)
                        {
                            FirmwareVersion = Encoding.ASCII.GetString(version.Skip(4).ToArray()).Trim('\0');
                        }
                    }
                    catch (Exception ex)
                    {
                        //StatusWrite("Failed to get config. Please disconnect and reconnect the board.");
                        return false;
                    }
                }
            }
            else
            {
                //StatusWrite("Failed to open device. Please disconnect and reconnect.");
                return false;
            }

            return true;
        }

        public bool GetBoardVersion()
        {
            BoardVersion = BoardVersion.Undefined;
            HidStream hidStream;
            if (Device.TryOpen(out hidStream))
            {
                using (hidStream)
                {
                    try
                    {
                        byte[] version = new byte[64];
                        version[0] = 0xa4;
                        hidStream.GetFeature(version);
                        if (version[0] == 0xa4)
                        {
                            string ver = Encoding.ASCII.GetString(version.Skip(4).ToArray()).Trim('\0');
                            switch (ver)
                            {
                                case "Roxy v1.1":
                                    BoardVersion = BoardVersion.RoxyV1_1;
                                    break;
                                case "Roxy v2.0":
                                    BoardVersion = BoardVersion.RoxyV2_0;
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //StatusWrite("Failed to get config. Please disconnect and reconnect the board.");
                        return false;
                    }
                }
            }
            else
            {
                //StatusWrite("Failed to open device. Please disconnect and reconnect.");
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is RoxyDevice)
            {
                var comp = (RoxyDevice)obj;
                return this.Device.VendorID == comp.Device.VendorID && this.Device.ProductID == comp.Device.ProductID && this.SerialNumber == comp.SerialNumber && comp.IsBootloader == this.IsBootloader;
            }
            else
                return false;
        }
    }
}
