using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gtk;
using Roxy.Lib;

namespace roxy_tool
{
    public class KeyMappingWindow : Window
    {
        private List<ComboBox> buttonCombo = new List<ComboBox>();

        byte[] mapping = new byte[16];

        public KeyMappingWindow(string title) : base(title)
        {
            this.Resizable = false;
            VBox widgets = new VBox(false, 2);
            for (int i = 0; i < 12; i++)
            {
                HBox temp = new HBox(false, 2);
                Label tempLabel = new Label($"Button {i + 1}");
                ComboBox tempCB = new ComboBox(ConfigDefines.KeyList.Select(x => x.Name).ToArray());
                tempCB.Active = 0;
                temp.PackStart(tempLabel, false, true, 2);
                temp.PackStart(tempCB, false, true, 2);
                buttonCombo.Add(tempCB);
                widgets.PackStart(temp, false, true, 2);
            }
            string[] axes = new string[] { "Axis X+", "Axis X-", "Axis Y+", "Axis Y-" };
            for (int i = 0; i < 4; i++)
            {
                HBox temp = new HBox(false, 2);
                Label tempLabel = new Label(axes[i]);
                ComboBox tempCB = new ComboBox(ConfigDefines.KeyList.Select(x => x.Name).ToArray());
                tempCB.Active = 0;
                temp.PackStart(tempLabel, false, true, 2);
                temp.PackStart(tempCB, false, true, 2);
                buttonCombo.Add(tempCB);
                widgets.PackStart(temp, false, true, 2);
            }
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
            this.DeleteEvent += KeyMappingWindow_DeleteEvent;
        }

        private void KeyMappingWindow_DeleteEvent(object o, DeleteEventArgs args)
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
                buttonCombo[i].Active = ConfigDefines.KeyList.IndexOf(ConfigDefines.KeyList.First(x => x.Code == map[i]));
            }
            Array.Copy(map, mapping, 16);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[16];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i] = ConfigDefines.KeyList[buttonCombo[i].Active].Code;
            }

            return bytes;
        }
    }
}
