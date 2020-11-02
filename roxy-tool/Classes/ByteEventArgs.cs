using System;

namespace roxy_tool.Classes
{
    public class ByteEventArgs : EventArgs
    {
        public int Index { get; set; }
        public byte Value { get; set; }

        public ByteEventArgs() { }

        public ByteEventArgs(int index, byte val)
        {
            Index = index;
            Value = val;
        }
    }
}
