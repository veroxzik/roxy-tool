using roxy_tool.Enums;
using System;

namespace roxy_tool.Classes
{
    public class ConfigPanelEventArgs : EventArgs
    {
        public ControlSubPanel SubPanel { get; private set; }
        public byte Value { get; private set; }
        public ConfigPanelEventArgs(ControlSubPanel subPanel, byte value = 0)
        {
            SubPanel = subPanel;
            Value = value;
        }
    }
}
