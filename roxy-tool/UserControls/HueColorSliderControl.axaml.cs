using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using Avalonia.Media;
using roxy_tool.Classes;
using Avalonia.Data;

namespace roxy_tool.UserControls
{
    public class HueColorSliderControl : UserControl
    {
        // Controls
        Slider colorSlider;
        NumericUpDown colorValueNumeric;
        Button colorButton;

        // Events
        public EventHandler<ByteEventArgs> OnClosed;

        int activeIndex = 0;

        public HueColorSliderControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.colorSlider = this.FindControl<Slider>("colorSlider");
            colorSlider.PropertyChanged += ColorSlider_PropertyChanged;
            this.colorValueNumeric = this.FindControl<NumericUpDown>("colorValueNumeric");
            var okButton = this.FindControl<Button>("okButton");
            okButton.Click += OkButton_Click;
            var cancelButton = this.FindControl<Button>("cancelButton");
            cancelButton.Click += CancelButton_Click;
            this.colorButton = this.FindControl<Button>("colorButton");
        }

        private void ColorSlider_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if(colorButton.Background?.ToString() != ApproxColorBrush.ToString())
                colorButton.Background = ApproxColorBrush;
        }

        private void OkButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new ByteEventArgs(activeIndex, Convert.ToByte(colorValueNumeric.Value)));
        }

        private void CancelButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //SetMapping(mapping);
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new ByteEventArgs(-1, 0));   // Index -1 to cancel
        }

        public void SetHue(int index, byte c)
        {
            activeIndex = index;

            colorSlider.Value = c;
        }

        public Color ApproxColor { get { return HueToColor.Convert((byte)colorSlider.Value); } }

        public SolidColorBrush ApproxColorBrush {  get { return new SolidColorBrush(ApproxColor); } }
    }
}
