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
    public partial class RoxyConfigPanel : UserControl, IConfigPanel
    {
        private ColorPickerForm color1Picker = new ColorPickerForm();
        private ColorPickerForm color2Picker = new ColorPickerForm();
        private KeyMappingForm keyMapper = new KeyMappingForm();
        private ButtonLightModeForm ledModeMapper = new ButtonLightModeForm();

        public RoxyConfigPanel()
        {
            InitializeComponent();

            qe1Combo.Items.AddRange(ConfigDefines.QeDropdownStrings.ToArray());
            qe2Combo.Items.AddRange(ConfigDefines.QeDropdownStrings.ToArray());

            qe1Combo.SelectedIndex = 0;
            qe2Combo.SelectedIndex = 0;
            ps2Combo.SelectedIndex = 0;
            rgbCombo.SelectedIndex = 0;
            rgbBrightnessNum.Value = 100;
            rgbModeCombo.SelectedIndex = 0;
            ascEmuCombo.SelectedIndex = 0;
            controllerOutputCombo.SelectedIndex = 0;
        }

        public byte[] GetConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x19;  // Roxy is 25 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes(labelText.Text);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            uint flags =
                Convert.ToUInt32(splitQE1Check.Checked) << 8 |
                Convert.ToUInt32(invertLightsCheck.Checked) << 7 |
                Convert.ToUInt32(analogButtonsCheck.Checked) << 6 |
                Convert.ToUInt32(analogInputCheck.Checked) << 5 |
                0 << 4 |    // LED2 bit unused
                Convert.ToUInt32(led1Check.Checked) << 3 |
                Convert.ToUInt32(invertQE2Check.Checked) << 2 |
                Convert.ToUInt32(invertQE1Check.Checked) << 1 |
                Convert.ToUInt32(serialNumCheck.Checked);
            Array.Copy(BitConverter.GetBytes(flags), 0, configBytes, 16, 4);
            configBytes[20] = (byte)GetByteFromComboBox(qe1Combo.SelectedIndex);
            configBytes[21] = (byte)GetByteFromComboBox(qe2Combo.SelectedIndex);
            configBytes[22] = (byte)ps2Combo.SelectedIndex;
            configBytes[23] = (byte)rgbCombo.SelectedIndex;
            configBytes[24] = (byte)rgbBrightnessNum.Value;
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
                // Ignore LED2 (bit 4)
                analogInputCheck.Checked = (config.Flags >> 5 & 0x1) == 0x1;
                analogButtonsCheck.Checked = (config.Flags >> 6 & 0x1) == 0x1;
                invertLightsCheck.Checked = (config.Flags >> 7 & 0x1) == 0x1;
                splitQE1Check.Checked = (config.Flags >> 8 & 0x1) == 0x1;
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
                try { rgbCombo.SelectedIndex = config.RgbInterface; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing RGB Interface. Defaulting to Disabled.");
                    rgbCombo.SelectedIndex = 0;
                }
                rgbBrightnessNum.Value = config.Brightness;
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
            byte mode = configBytes[4];
            //uint flags = BitConverter.ToUInt32(configBytes, 5);
            byte led1Hue = configBytes[5];
            byte led2Hue = configBytes[6];

            rgbModeCombo.InvokeIfRequired(() => {
                try { rgbModeCombo.SelectedIndex = mode; }
                catch
                {
                    Helper.StatusWrite?.Invoke("Error parsing RGB Mode. Defaulting to HID Only.");
                    rgbModeCombo.SelectedIndex = 0;
                }
                color1Picker.SetHue(led1Hue);
                rgb1ColorButton.BackColor = color1Picker.ApproxColor;
                color2Picker.SetHue(led2Hue);
                rgb2ColorButton.BackColor = color2Picker.ApproxColor;
            });
        }

        public byte[] GetRgbConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x01;  // RGB config is Segment 1
            configBytes[2] = 0x03;  // Length
            configBytes[3] = 0x00;  // Padding byte
            configBytes[4] = (byte)rgbModeCombo.SelectedIndex;
            configBytes[5] = (byte)color1Picker.Value;
            configBytes[6] = (byte)color2Picker.Value;

            return configBytes;
        }

        public void PopulateKeyMappingControls(byte[] configBytes)
        {
            keyMapper.SetMapping(configBytes.Skip(4).ToArray());
            ledModeMapper.SetMapping(configBytes.Skip(26).ToArray());
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
            Array.Copy(ledModeMapper.GetMapping(), 0, configBytes, 26, 8);

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

        private void rgb1ColorButton_Click(object sender, EventArgs e)
        {
            if (color1Picker.ShowDialog() == DialogResult.OK)
            {
                rgb1ColorButton.BackColor = color1Picker.ApproxColor;
            }
        }

        private void rgb2ColorButton_Click(object sender, EventArgs e)
        {
            if (color2Picker.ShowDialog() == DialogResult.OK)
            {
                rgb2ColorButton.BackColor = color2Picker.ApproxColor;
            }
        }

        private void keyMapButton_Click(object sender, EventArgs e)
        {
            keyMapper.ShowDialog();
        }

        private void buttonLedModeButton_Click(object sender, EventArgs e)
        {
            ledModeMapper.ShowDialog();
        }
    }
}
