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
        private KeyMappingForm keyMapper = new KeyMappingForm();
        private ButtonLightModeForm ledModeForm = new ButtonLightModeForm();

        public ArcinRoxyControlPanel()
        {
            InitializeComponent();

            qe1Combo.Items.AddRange(ConfigDefines.QeDropdownStrings.ToArray());
            qe2Combo.Items.AddRange(ConfigDefines.QeDropdownStrings.ToArray());

            qe1Combo.SelectedIndex = 0;
            qe2Combo.SelectedIndex = 0;
            ps2Combo.SelectedIndex = 0;
            ws2812bCombo.SelectedIndex = 0;
            controllerOutputCombo.SelectedIndex = 0;
        }

        public byte[] GetConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x19;  // arcin (Roxy) is 25 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes(labelText.Text);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            uint flags =
                Convert.ToUInt32(invertLightsCheck.Checked) << 7 |
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
            configBytes[28] = (byte)controllerOutputCombo.SelectedIndex;

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
                invertLightsCheck.Checked = (config.Flags >> 7 & 0x1) == 0x1;
                try { qe1Combo.SelectedIndex = GetComboBoxIndex(config.QE1Sens); }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing QE1 Sensitivty. Defaulting to 1:1.");
                    qe1Combo.SelectedIndex = 0;
                }
                try { qe2Combo.SelectedIndex = GetComboBoxIndex(config.QE2Sens); }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing QE2 Sensitivty. Defaulting to 1:1.");
                    qe2Combo.SelectedIndex = 0;
                }
                try { ps2Combo.SelectedIndex = config.PS2Mode; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing PS2 Mode. Defaulting to Disabled.");
                    ps2Combo.SelectedIndex = 0;
                }
                try { ws2812bCombo.SelectedIndex = config.RgbInterface < 2 ? config.RgbInterface : 0; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing WS2812B Mode. Defaulting to Disabled.");
                    ws2812bCombo.SelectedIndex = 0;
                }
                debounceNum.Value = config.ButtonDebounce;
                try { ascEmuCombo.SelectedIndex = config.AscEmulation; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing ASC Emulation. Defaulting to Disabled.");
                    ascEmuCombo.SelectedIndex = 0;
                }
                axisDebounceNum.Value = config.AxisDebounce;
                try { controllerOutputCombo.SelectedIndex = config.ControllerOutput; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing Controller Output mode. Defaulting to Joystick.");
                    controllerOutputCombo.SelectedIndex = 0;
                }
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

        public void PopulateKeyMappingControls(byte[] configBytes)
        {
            keyMapper.SetMapping(configBytes.Skip(4).ToArray());
            ledModeForm.SetMapping(configBytes.Skip(26).ToArray());
        }

        public byte[] GetKeyMappingBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x02;  // Key mapping config is Segment 2
            configBytes[2] = 0x1E;  // Length
            configBytes[3] = 0x00;  // Padding byte
            Array.Copy(keyMapper.GetMapping(), 0, configBytes, 4, 16);
            // 6 bytes of joystick remap (one nibble per button)
            Array.Copy(ledModeForm.GetMapping(), 0, configBytes, 26, 8);

            return configBytes;
        }

        public sbyte GetByteFromComboBox(int index)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Value == index).FirstOrDefault().Key;
        }

        public int GetComboBoxIndex(sbyte sens)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Key == sens).FirstOrDefault().Value;
        }

        private void keyMapButton_Click(object sender, EventArgs e)
        {
            keyMapper.ShowDialog();
        }

        private void buttonLedModeButton_Click(object sender, EventArgs e)
        {
            ledModeForm.ShowDialog();
        }
    }
}
