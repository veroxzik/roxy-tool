using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Roxy.Lib;
using System;
using System.Collections.Generic;

namespace roxy_tool.UserControls
{
    public partial class ArgbControl : UserControl
    {
        List<ComboBox> argbModeCombo = new List<ComboBox>();
        List<NumericUpDown> argbNum = new List<NumericUpDown>();
        List<CheckBox> argbInvertCheck = new List<CheckBox>();

        byte[] mapping = new byte[7];

        public EventHandler OnClosed;

        public ArgbControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            for (int i = 0; i < 4; i++)
            {
                var combo = this.FindControl<ComboBox>($"argb{i + 1}ModeCombo");
                combo.Items = ConfigDefines.ArgbModes;
                combo.SelectedIndex = 0;
                argbModeCombo.Add(combo);

                var num = this.FindControl<NumericUpDown>($"argb{i + 1}Num");
                argbNum.Add(num);

                var check = this.FindControl<CheckBox>($"argb{i + 1}InvertCheck");
                argbInvertCheck.Add(check);
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
            for (int i = 0; i < argbModeCombo.Count; i++)
            {
                argbModeCombo[i].SelectedIndex = (map[i / 2] >> ((i % 2) * 4)) & 0xF;
                argbNum[i].Value = map[i + 2];
                argbInvertCheck[i].IsChecked = Convert.ToBoolean(map[6] & (1 << i));
            }

            Array.Copy(map, mapping, 7);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[7];

            for (int i = 0; i < argbModeCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((argbModeCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
                bytes[i + 2] = Convert.ToByte(argbNum[i].Value);
                bytes[6] |= Convert.ToByte(Convert.ToInt32(argbInvertCheck[i].IsChecked) << i);
            }

            return bytes;
        }
    }
}
