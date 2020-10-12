using HidSharp;
using Roxy.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roxy.Tool.WinForms
{
    public class DeviceSelectionComboxBoxItem
    {
        private string name;
        public string Name
        { get; set; }
        
        private HidDevice device;
        public HidDevice Device
        { get; set; }

        private Board board;
        public Board Board
        { get; set; }

        private bool isBootloader = false;
        public bool IsBootLoader
        { get; set;  }

        public override string ToString()
        {
            return $"{this.Name} - {this.Device.DevicePath}";
        }
    }
}
