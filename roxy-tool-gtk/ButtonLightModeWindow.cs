using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gtk;
using Roxy.Lib;

namespace roxy_tool
{
    public class ButtonLightModeWindow : Window
    {
        private List<ComboBox> buttonCombo = new List<ComboBox>();
        private SpinButton fadeTimeSpin;

        byte[] mapping = new byte[8];

        public ButtonLightModeWindow(string title) : base(title)
        {
            this.Resizable = false;
            VBox widgets = new VBox(false, 2);
            for (int i = 0; i < 12; i++)
            {
                HBox temp = new HBox(false, 2);
                Label tempLabel = new Label($"Button {i + 1}");
                ComboBox tempCB = new ComboBox(ConfigDefines.ButtonLightModes.ToArray());
                tempCB.Active = 0;
                temp.PackStart(tempLabel, false, true, 2);
                temp.PackStart(tempCB, false, true, 2);
                buttonCombo.Add(tempCB);
                widgets.PackStart(temp, false, true, 2);
            }
            HBox temp2 = new HBox(false, 2);
            fadeTimeSpin = new SpinButton(0, 5000, 1);
            temp2.PackStart(new Label("Fade Time"), false, true, 2);
            temp2.PackStart(fadeTimeSpin, false, true, 2);
            temp2.PackStart(new Label("ms"), false, true, 2);
            widgets.PackStart(temp2, false, true, 2);
            HBox buttonBox = new HBox(false, 2);
            Button okButton = new Button();
            okButton.Label = "OK";
            okButton.Clicked += OkButton_Clicked;
            Button cancelButton = new Button();
            cancelButton.Label = "Cancel";
            cancelButton.Clicked += CancelButton_Clicked;
            buttonBox.PackStart(okButton, true, true, 2);
            buttonBox.PackStart(cancelButton, true, true, 2);
            widgets.PackStart(buttonBox, false, true, 2);

            this.Add(widgets);
            this.DeleteEvent += ButtonLightModeWindow_DeleteEvent;
        }

        private void ButtonLightModeWindow_DeleteEvent(object o, DeleteEventArgs args)
        {
            this.Hide();
            args.RetVal = true;
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            mapping = GetMapping();
            this.Hide();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            SetMapping(mapping);
            this.Hide();
        }

        public void SetMapping(byte[] map)
        {
            for (int i = 0; i < buttonCombo.Count; i++)
            {
                buttonCombo[i].Active = (map[i / 2] >> ((i % 2) * 4)) & 0xF;
            }
            fadeTimeSpin.Value = map[6] | map[7] << 8;
            Array.Copy(map, mapping, 8);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[8];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((buttonCombo[i].Active & 0xF) << ((i % 2) * 4));
            }
            bytes[6] = Convert.ToByte(Convert.ToInt16(fadeTimeSpin.Value) & 0xFF);
            bytes[7] = Convert.ToByte((Convert.ToInt16(fadeTimeSpin.Value) >> 8) & 0xFF);

            return bytes;
        }
    }
}
