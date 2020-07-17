using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Roxy.Lib;

namespace Roxy.Tool.WinForms
{
    public partial class ArcinRoxyControlPanel : UserControl, IConfigPanel
    {
        public ArcinRoxyControlPanel()
        {
            InitializeComponent();

            qe1Combo.SelectedIndex = 0;
            qe2Combo.SelectedIndex = 0;
            ps2Combo.SelectedIndex = 0;
            ws2812bCombo.SelectedIndex = 0;
        }

        public byte[] GetConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x18;  // arcin (Roxy) is 24 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes(labelText.Text);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            uint flags =
                Convert.ToUInt32(analogButtonsCheck.Checked) << 6 |
                Convert.ToUInt32(analogInputCheck.Checked) << 5 |
                Convert.ToUInt32(led2Check.Checked) << 4 |
                Convert.ToUInt32(led1Check.Checked) << 3 |
                Convert.ToUInt32(invertQE2Check.Checked) << 2 |
                Convert.ToUInt32(invertQE1Check.Checked) << 1 |
                Convert.ToUInt32(serialNumCheck.Checked);
            Array.Copy(BitConverter.GetBytes(flags), 0, configBytes, 16, 4);
            configBytes[20] = (byte)GetByteFromComboBox(qe1Combo.SelectedIndex);
            configBytes[21] = (byte)GetByteFromComboBox(qe2Combo.SelectedIndex);
            configBytes[22] = (byte)ps2Combo.SelectedIndex;
            configBytes[23] = (byte)ws2812bCombo.SelectedIndex;
            // configBytes[24] is not used (RGB brightness)
            configBytes[25] = (byte)debounceNum.Value;
            configBytes[26] = (byte)ascEmuCombo.SelectedIndex;
            configBytes[27] = (byte)axisDebounceNum.Value;

            return configBytes;
        }

        public void PopulateControls(byte[] configBytes)
        {
            var config = new Configuration(configBytes);

            labelText.InvokeIfRequired(() =>
            {
                labelText.Text = config.Label;
                serialNumCheck.Checked = (config.Flags & 0x1) == 0x1;
                invertQE1Check.Checked = (config.Flags >> 1 & 0x1) == 0x1;
                invertQE2Check.Checked = (config.Flags >> 2 & 0x1) == 0x1;
                led1Check.Checked = (config.Flags >> 3 & 0x1) == 0x1;
                led2Check.Checked = (config.Flags >> 4 & 0x1) == 0x1;
                analogInputCheck.Checked = (config.Flags >> 5 & 0x1) == 0x1;
                analogButtonsCheck.Checked = (config.Flags >> 6 & 0x1) == 0x1;
                qe1Combo.SelectedIndex = GetComboBoxIndex(config.QE1Sens);
                qe2Combo.SelectedIndex = GetComboBoxIndex(config.QE2Sens);
                ps2Combo.SelectedIndex = config.PS2Mode;
                ws2812bCombo.SelectedIndex = config.RgbInterface < 2 ? config.RgbInterface : 0;
                debounceNum.Value = config.ButtonDebounce;
                ascEmuCombo.SelectedIndex = config.AscEmulation;
                axisDebounceNum.Value = config.AxisDebounce;
            });
        }

        public void PopulateRgbControls(byte[] configBytes)
        {
            throw new NotImplementedException();
        }

        public byte[] GetRgbConfigBytes()
        {
            return null;
        }

        public sbyte GetByteFromComboBox(int index)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Value == index).FirstOrDefault().Key;
        }

        public int GetComboBoxIndex(sbyte sens)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Key == sens).FirstOrDefault().Value;
        }
    }
}
