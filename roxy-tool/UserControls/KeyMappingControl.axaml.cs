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
    public class KeyMappingControl : UserControl
    {
        List<ComboBox> buttonCombo = new List<ComboBox>();
        byte[] mapping = new byte[16];

        public EventHandler OnClosed;

        public KeyMappingControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            for (int i = 0; i < 12; i++)
            {
                var combo = this.FindControl<ComboBox>($"button{i + 1}Combo");
                combo.Items = ConfigDefines.KeyList;
                combo.SelectedIndex = 0;
                buttonCombo.Add(combo);
            }
            var comboXPos = this.FindControl<ComboBox>("axisXPosCombo");
            comboXPos.Items = ConfigDefines.KeyList;
            comboXPos.SelectedIndex = 0;
            var comboXNeg = this.FindControl<ComboBox>("axisXNegCombo");
            comboXNeg.Items = ConfigDefines.KeyList;
            comboXNeg.SelectedIndex = 0;
            var comboYPos = this.FindControl<ComboBox>("axisYPosCombo");
            comboYPos.Items = ConfigDefines.KeyList;
            comboYPos.SelectedIndex = 0;
            var comboYNeg = this.FindControl<ComboBox>("axisYNegCombo");
            comboYNeg.Items = ConfigDefines.KeyList;
            comboYNeg.SelectedIndex = 0;
            buttonCombo.Add(comboXPos);
            buttonCombo.Add(comboXNeg);
            buttonCombo.Add(comboYPos);
            buttonCombo.Add(comboYNeg);

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
                buttonCombo[i].SelectedItem = ConfigDefines.KeyList.First(x => x.Code == map[i]);
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
