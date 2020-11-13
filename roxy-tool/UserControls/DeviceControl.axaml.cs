using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;

namespace roxy_tool.UserControls
{
    public class DeviceControl : UserControl
    {
        ConfigOption flagsCheck;
        ConfigOption svre9Left;
        ConfigOption svre9Right;
        byte[] mapping = new byte[5];

        public EventHandler OnClosed;

        public DeviceControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            flagsCheck = this.FindControl<ConfigOption>("flagsCheck");
            svre9Left = this.FindControl<ConfigOption>("svre9Left");
            svre9Right = this.FindControl<ConfigOption>("svre9Right");

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

            flagsCheck.SetCheck(BitConverter.ToUInt32(map, 0));
            svre9Left.SetCombo(map[4] & 0xF);
            svre9Right.SetCombo((map[4] >> 4) & 0xF);
            Array.Copy(map, mapping, 5);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[5];
            var flags = flagsCheck.GetCheck();
            Array.Copy(BitConverter.GetBytes(flags), bytes, 4);
            bytes[4] = (byte)((svre9Left.GetCombo() & 0xF) | ((svre9Right.GetCombo() << 4) & 0xF0));

            return bytes;
        }
    }
}
