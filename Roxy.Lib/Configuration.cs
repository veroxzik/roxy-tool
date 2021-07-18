using System;
using System.Linq;
using System.Text;

namespace Roxy.Lib
{
    public abstract class Configuration
    {
        public abstract byte[] GetBytes();
    }

    public class StandardConfiguration : Configuration
    {
        public string Label { get; set; }
        public uint Flags { get; set; }
        public sbyte QE1Sens { get; set; }
        public sbyte QE2Sens { get; set; }
        public byte PS2Mode { get; set; }
        public byte RgbInterface { get; set; }
        public byte RgbBrightness { get; set; }
        public byte ButtonDebounce { get; set; }
        public byte AscEmulation { get; set; }
        public byte AxisDebounce { get; set; }
        public byte ControllerOutput { get; set; }
        public byte QE1ReductionRatio { get; set; }
        public byte QE2ReductionRatio { get; set; }
        public byte AxisSustain { get; set; }

        public StandardConfiguration(byte[] bytes)
        {
            Label = Encoding.ASCII.GetString(bytes, 4, 12).Replace("\0", string.Empty);
            Flags = BitConverter.ToUInt32(bytes, 16);
            QE1Sens = (sbyte)bytes[20];
            QE2Sens = (sbyte)bytes[21];
            PS2Mode = bytes[22];
            RgbInterface = bytes[23];
            RgbBrightness = bytes[24];
            ButtonDebounce = bytes[25];
            AscEmulation = bytes[26];
            AxisDebounce = bytes[27];
            ControllerOutput = bytes[28];
            QE1ReductionRatio = bytes[29];
            QE2ReductionRatio = bytes[30];
            AxisSustain = bytes[31];
        }
        public override byte[] GetBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x1C;  // Roxy is 28 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes(Label);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            Array.Copy(BitConverter.GetBytes(Flags), 0, configBytes, 16, 4);
            configBytes[20] = (byte)QE1Sens;
            configBytes[21] = (byte)QE2Sens;
            configBytes[22] = PS2Mode;
            configBytes[23] = RgbInterface;
            configBytes[24] = RgbBrightness;
            configBytes[25] = ButtonDebounce;
            configBytes[26] = AscEmulation;
            configBytes[27] = AxisDebounce;
            configBytes[28] = ControllerOutput;
            configBytes[29] = QE1ReductionRatio;
            configBytes[30] = QE2ReductionRatio;
            configBytes[31] = AxisSustain;

            return configBytes;
        }
    }

    public class RgbConfiguration : Configuration
    {
        public byte Mode { get; set; }
        public byte Led1Hue { get; set; }
        public byte Led2Hue { get; set; }
        public byte[] TurntableMapping { get; set; }
        public byte[] ButtonLedHue { get; set; }

        public RgbConfiguration(byte[] bytes)
        {
            Mode = bytes[4];
            Led1Hue = bytes[5];
            Led2Hue = bytes[6];
            TurntableMapping = bytes.Skip(7).Take(7).ToArray();
            ButtonLedHue = bytes.Skip(14).Take(12).ToArray();
        }
        public override byte[] GetBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x01;  // RGB config is Segment 1
            configBytes[2] = 0x03;  // Length
            configBytes[3] = 0x00;  // Padding byte
            configBytes[4] = Mode;
            configBytes[5] = Led1Hue;
            configBytes[6] = Led2Hue;

            return configBytes;
        }
    }

    public class KeyMappingConfiguration : Configuration
    {
        public byte[] KeyMapping { get; private set; }
        public byte[] JoystickMapping { get; private set; }
        public byte[] LedMode { get; private set; }
        public byte[] LedType { get; private set; }

        public KeyMappingConfiguration(byte[] bytes)
        {
            KeyMapping = bytes.Skip(4).Take(16).ToArray();
            JoystickMapping = bytes.Skip(20).Take(6).ToArray();
            LedMode = bytes.Skip(26).Take(8).ToArray();
            LedType = bytes.Skip(34).Take(6).ToArray();
        }

        public override byte[] GetBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x02;  // Key mapping config is Segment 2
            configBytes[2] = 0x1E;  // Length
            configBytes[3] = 0x00;  // Padding byte
            Array.Copy(KeyMapping, 0, configBytes, 4, 16);
            Array.Copy(JoystickMapping, 0, configBytes, 20, 6);
            Array.Copy(LedMode, 0, configBytes, 26, 8);
            Array.Copy(LedType, 0, configBytes, 34, 6);

            return configBytes;
        }
    }

    public class DeviceConfiguration : Configuration
    {
        public uint EnableFlags { get; private set; }
        public byte Svre9Buttons { get; private set; }
        public byte[] Data { get; private set; }

        public DeviceConfiguration(byte[] bytes)
        {
            EnableFlags = BitConverter.ToUInt32(bytes, 4);
            Svre9Buttons = bytes[9];
            Data = new byte[bytes.Length - 4];
            Array.Copy(bytes, 4, Data, 0, 5);
        }

        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
