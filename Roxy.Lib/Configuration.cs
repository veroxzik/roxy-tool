using System;
using System.Collections.Generic;
using System.Text;

namespace Roxy.Lib
{
    public class Configuration
    {
        public string Label { get; private set; }
        public uint Flags { get; private set; }
        public sbyte QE1Sens { get; private set; }
        public sbyte QE2Sens { get; private set; }
        public byte PS2Mode { get; private set; }
        public byte RgbInterface { get; private set; }
        public byte Brightness { get; private set; }
        public byte ButtonDebounce { get; private set; }
        public byte AscEmulation { get; private set; }
        public byte AxisDebounce { get; private set; }

        public Configuration(byte[] bytes)
        {
            Label = Encoding.ASCII.GetString(bytes, 4, 12);
            Flags = BitConverter.ToUInt32(bytes, 16);
            QE1Sens = (sbyte)bytes[20];
            QE2Sens = (sbyte)bytes[21];
            PS2Mode = bytes[22];
            RgbInterface = bytes[23];
            Brightness = bytes[24];
            ButtonDebounce = bytes[25];
            AscEmulation = bytes[26];
            AxisDebounce = bytes[27];
        }
    }
}
