using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using roxy_tool.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Roxy.Lib;

namespace roxy_tool.UserControls
{
    public class JoystickMappingControl : UserControl
    {
        List<ComboBox> buttonCombo = new List<ComboBox>();
        byte[] mapping = new byte[6];

        public EventHandler OnClosed;

        public JoystickMappingControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            for (int i = 0; i < 12; i++)
            {
                var combo = this.FindControl<ComboBox>($"button{i + 1}Combo");
                combo.Items = ConfigDefines.ButtonList;
                combo.SelectedIndex = 0;
                buttonCombo.Add(combo);
            }

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
            Array.Copy(map, mapping, 6);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[6];

            for (int i = 0; i < buttonCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((buttonCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
            }

            return bytes;
        }
    }
}
