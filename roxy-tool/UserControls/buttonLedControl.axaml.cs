using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using roxy_tool.Classes;
using System;
using System.Collections.Generic;
using Roxy.Lib;

namespace roxy_tool.UserControls
{
    public class ButtonLedControl : UserControl
    {
        List<ComboBox> buttonCombo = new List<ComboBox>();
        NumericUpDown fadeTimeNumeric;
        byte[] mapping = new byte[16];

        public EventHandler OnClosed;

        public ButtonLedControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            for (int i = 0; i < 12; i++)
            {
                var combo = this.FindControl<ComboBox>($"button{i + 1}Combo");
                combo.Items = ConfigDefines.ButtonLightModes;
                combo.SelectedIndex = 0;
                buttonCombo.Add(combo);
            }
            this.fadeTimeNumeric = this.FindControl<NumericUpDown>("fadeTimeNumeric");

            var okButton = this.FindControl<Button>("okButton");
            okButton.Click += OkButton_Click;
            var cancelButton = this.FindControl<Button>("cancelButton");
            cancelButton.Click += CancelButton_Click;
        }

        private void OkButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            mapping = GetMapping();
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new EventArgs());
        }

        private void CancelButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SetMapping(mapping);
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new EventArgs());
        }

        public void SetMapping(byte[] map)
        {
            for (int i = 0; i < buttonCombo.Count; i++)
            {
                buttonCombo[i].SelectedIndex = (map[i / 2] >> ((i % 2) * 4)) & 0xF;
            }
            fadeTimeNumeric.Value = map[6] | map[7] << 8;
            Array.Copy(map, mapping, 8);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[8];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((buttonCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
            }
            bytes[6] = Convert.ToByte(Convert.ToInt16(fadeTimeNumeric.Value) & 0xFF);
            bytes[7] = Convert.ToByte((Convert.ToInt16(fadeTimeNumeric.Value) >> 8) & 0xFF);

            return bytes;
        }
    }
}
