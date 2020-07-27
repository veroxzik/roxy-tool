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
    public partial class KeyMappingForm : Form
    {
        List<ComboBox> buttonCombo = new List<ComboBox>();

        byte[] mapping = new byte[16];

        public KeyMappingForm()
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
            buttonCombo.Add(axisXPosCombo);
            buttonCombo.Add(axisXNegCombo);
            buttonCombo.Add(axisYPosCombo);
            buttonCombo.Add(axisYNegCombo);


            foreach (var combo in buttonCombo)
            {
                combo.Items.AddRange(ConfigDefines.KeyList.ToArray());
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
                buttonCombo[i].SelectedItem = ConfigDefines.KeyList.First(x => x.Code == map[i]);
                //buttonCombo[i].SelectedIndex = ConfigDefines.KeyList.IndexOf(ConfigDefines.KeyList.First(x => x.Code == map[i]));
            }
            Array.Copy(map, mapping, 16);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[16];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i] = (buttonCombo[i].SelectedItem as KeyEntry).Code;
            }

            return bytes;
        }
    }
}
