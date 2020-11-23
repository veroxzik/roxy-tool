using HidSharp;
using Roxy.Lib;
using System;
using System.Collections.Generic;

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
                        if (configBytes.Count == 0)
                        {
                            //StatusWrite("Failed to get any config reports.");
                        }
                        else
                        {
                            //StatusWrite($"Found {configBytes.Count} config reports.");
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
