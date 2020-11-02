using HidSharp;
using System;

namespace roxy_tool.Classes
{
    public class FlashEventArgs : EventArgs
    {
        public HidDevice Device { get; private set; }
        public FlashEventArgs(HidDevice dev)
        {
            Device = dev;
        }
    }
}
