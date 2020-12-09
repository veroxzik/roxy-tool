using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;

namespace roxy_tool.UserControls
{
    public class TurntableControl : UserControl
    {
        ConfigOption ttMode;
        ConfigOption ttType;
        ConfigOption ttDirection;
        ConfigOption ttAxis;
        ConfigOption ttNumLeds;
        ConfigOption ttHue;
        ConfigOption ttSat;
        ConfigOption ttVal;
        byte[] mapping = new byte[7];

        public EventHandler OnClosed;

        public TurntableControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            ttMode = this.FindControl<ConfigOption>("ttMode");
            ttType = this.FindControl<ConfigOption>("ttType");
            ttDirection = this.FindControl<ConfigOption>("ttDirection");
            ttAxis = this.FindControl<ConfigOption>("ttAxis");
            ttNumLeds = this.FindControl<ConfigOption>("ttNumLeds");
            ttHue = this.FindControl<ConfigOption>("ttHue");
            ttSat = this.FindControl<ConfigOption>("ttSat");
            ttVal = this.FindControl<ConfigOption>("ttVal");

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
            ttMode.SetCombo(map[0]);
            ttNumLeds.SetNumber(map[1]);
            ttAxis.SetCombo(map[2]);
            ttHue.SetNumber(map[3]);
            ttSat.SetNumber(map[4]);
            ttVal.SetNumber(map[5]);
            ttType.SetCombo(map[6] & 0xF);
            ttDirection.SetCombo((map[6] >> 4) & 0xF);
            Array.Copy(map, mapping, 7);
        }

        public byte[] GetMapping()
        {
            byte[] bytes = new byte[7];
            bytes[0] = (byte)ttMode.GetCombo();
            bytes[1] = (byte)ttNumLeds.GetNumber();
            bytes[2] = (byte)ttAxis.GetCombo();
            bytes[3] = (byte)ttHue.GetNumber();
            bytes[4] = (byte)ttSat.GetNumber();
            bytes[5] = (byte)ttVal.GetNumber();
            bytes[6] = (byte)((ttType.GetCombo() & 0xF) | ((ttDirection.GetCombo() & 0xF) << 4));
            return bytes;
        }
    }
}
