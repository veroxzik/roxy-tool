using System;
using System.Collections.Generic;
using System.Text;

namespace roxy_tool
{
    interface IConfigPanel
    {
        void PopulateControls(byte[] configBytes);

        byte[] GetConfigBytes();

        void PopulateRgbControls(byte[] configBytes);

        byte[] GetRgbConfigBytes();

        int GetComboBoxIndex(sbyte sens);

        sbyte GetByteFromComboBox(int index);
    }
}
