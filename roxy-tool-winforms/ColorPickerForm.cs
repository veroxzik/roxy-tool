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
    public partial class ColorPickerForm : Form
    {
        public int Value { get { return colorTrackBar.Value; } }
        public Color ApproxColor { get; private set; }

        private Color[] colorList = new Color[256];

        public ColorPickerForm()
        {
            InitializeComponent();

            colorValueText.Text = "0";
            colorTrackBar.Value = 0;

            float slope1 = (255.0f - 160.0f) / 31.0f;
            float slope2 = 160.0f / 31.0f;
            float slope3 = 255.0f / 95.0f;

            // Generate color list
            for (int i = 0; i < 256; i++)
            {
                int r = 0, g = 0, b = 0;

                if (i < 32)
                {
                    r = (int)(255.0f - slope1 * i);
                    g = (int)(slope1 * i);
                    b = 0;
                }
                else if (i < 64)
                {
                    r = 170;
                    g = (int)(slope3 * i);
                    b = 0;
                }
                else if (i < 96)
                {
                    r = (int)(160.0f - slope2 * (i - 64));
                    g = (int)(slope3 * i);
                    b = 0;
                }
                else if (i < 128)
                {
                    r = 0;
                    g = (int)(255.0f - slope1 * (i - 96));
                    b = (int)(slope1 * (i - 96));
                }
                else if (i < 160)
                {
                    r = 0;
                    g = (int)(160.0f - slope2 * (i - 128));
                    b = (int)(95.0f + slope2 * (i - 128));
                }
                else
                {
                    r = (int)(slope3 * (i - 160));
                    g = 0;
                    b = (int)(255.0f - slope3 * (i - 160));
                }

                colorList[i] = Color.FromArgb(r, g, b);
            }
        }

        public void SetHue(int c)
        {
            colorTrackBar.Value = c;
            colorValueText.Text = colorTrackBar.Value.ToString();
            ApproxColor = colorList[colorTrackBar.Value];
            colorPanel.BackColor = ApproxColor;
        }

        private void colorTrackBar_Scroll(object sender, EventArgs e)
        {
            colorValueText.Text = colorTrackBar.Value.ToString();
            ApproxColor = colorList[colorTrackBar.Value];
            colorPanel.BackColor = ApproxColor;
        }

        private void colorValueText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (colorValueText.Text.Length == 2 && char.IsDigit(e.KeyChar))
            {
                if (Convert.ToInt32(colorValueText.Text.Remove(colorValueText.SelectionStart, colorValueText.SelectionLength) + e.KeyChar) > 255)
                {
                    e.Handled = true;
                    colorValueText.Text = "255";
                    colorTrackBar.Value = 255;
                    ApproxColor = colorList[colorTrackBar.Value];
                    colorPanel.BackColor = ApproxColor;
                }
            }

            if (!e.Handled && char.IsDigit(e.KeyChar))
            {
                if (colorValueText.Text.Length <= 2 || (colorValueText.Text.Length <= 3 && colorValueText.SelectionLength > 0))
                {
                    colorTrackBar.Value = Convert.ToInt32(colorValueText.Text.Remove(colorValueText.SelectionStart, colorValueText.SelectionLength) + e.KeyChar);
                    ApproxColor = colorList[colorTrackBar.Value];
                    colorPanel.BackColor = ApproxColor;
                }
            }
        }

        private void colorValueText_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(colorValueText.Text))
                colorValueText.Text = "0";

            if (Convert.ToInt32(colorValueText.Text) > 255)
                colorValueText.Text = "255";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
