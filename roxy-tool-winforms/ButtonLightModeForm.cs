using Roxy.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roxy.Tool.WinForms
{
    public partial class ButtonLightModeForm : Form
    {
        List<ComboBox> buttonCombo = new List<ComboBox>();

        byte[] mapping = new byte[8];

        public ButtonLightModeForm()
        {
            InitializeComponent();
            buttonCombo.Add(button1Combo);
            buttonCombo.Add(button2Combo);
            buttonCombo.Add(button3Combo);
            buttonCombo.Add(button4Combo);
            buttonCombo.Add(button5Combo);
            buttonCombo.Add(button6Combo);
            buttonCombo.Add(button7Combo);
            buttonCombo.Add(button8Combo);
            buttonCombo.Add(button9Combo);
            buttonCombo.Add(button10Combo);
            buttonCombo.Add(button11Combo);
            buttonCombo.Add(button12Combo);

            foreach (var combo in buttonCombo)
            {
                combo.Items.AddRange(ConfigDefines.ButtonLightModes.ToArray());
                combo.SelectedIndex = 0;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            mapping = GetMapping();
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SetMapping(mapping);
            DialogResult = DialogResult.Cancel;
        }

        public void SetMapping(byte[] map)
        {
            for (int i = 0; i < buttonCombo.Count; i++)
            {
                buttonCombo[i].SelectedIndex = (map[i / 2] >> ((i % 2) * 4)) & 0xF;
            }
            fadeDurationNum.Value = map[6] | map[7] << 8;
            Array.Copy(map, mapping, 8);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[8];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((buttonCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
            }
            bytes[6] = Convert.ToByte(Convert.ToInt16(fadeDurationNum.Value) & 0xFF);
            bytes[7] = Convert.ToByte((Convert.ToInt16(fadeDurationNum.Value) >> 8) & 0xFF);

            return bytes;
        }
    }
}
