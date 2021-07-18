using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using roxy_tool.Classes;
using System;
using System.Collections.Generic;
using Roxy.Lib;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia.Media;

namespace roxy_tool.UserControls
{
    public class ButtonLedControl : UserControl
    {
        List<ComboBox> buttonModeCombo = new List<ComboBox>();
        List<ComboBox> buttonTypeCombo = new List<ComboBox>();
        List<Button> rgbButton = new List<Button>();
        NumericUpDown fadeTimeNumeric;
        HueColorSliderControl rgbControl;
        ScrollViewer mainViewer;
        ScrollViewer rgbViewer;
        byte[] typeMapping = new byte[14];
        byte[] rgbMapping = new byte[12];
        byte[] hue = new byte[12];

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
                var combo = this.FindControl<ComboBox>($"button{i + 1}ModeCombo");
                combo.Items = ConfigDefines.ButtonLightModes;
                combo.SelectedIndex = 0;
                buttonModeCombo.Add(combo);

                combo = this.FindControl<ComboBox>($"button{i + 1}TypeCombo");
                combo.Items = ConfigDefines.ButtonLightTypes;
                combo.SelectedIndex = 0;
                combo.SelectionChanged += TypeCombo_SelectionChanged;
                buttonTypeCombo.Add(combo);

                var button = this.FindControl<Button>($"button{i + 1}RgbButton");
                button.Click += RgbButton_Click;
                rgbButton.Add(button);
            }
            this.fadeTimeNumeric = this.FindControl<NumericUpDown>("fadeTimeNumeric");
            this.rgbControl = this.FindControl<HueColorSliderControl>("rgbControl");
            this.rgbControl.OnClosed += rgbControl_OnClosed;
            this.mainViewer = this.FindControl<ScrollViewer>("mainScrollViewer");
            this.rgbViewer = this.FindControl<ScrollViewer>("rgbScrollViewer");

            var okButton = this.FindControl<Button>("okButton");
            okButton.Click += OkButton_Click;
            var cancelButton = this.FindControl<Button>("cancelButton");
            cancelButton.Click += CancelButton_Click;
        }

        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num = Convert.ToInt32(Regex.Replace((sender as ComboBox).Name, "[^0-9]", "")) - 1;
            if (e.AddedItems.Contains("Standard"))
                rgbButton[num].IsEnabled = false;
            else
                rgbButton[num].IsEnabled = true;
        }

        private void OkButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            typeMapping = GetTypeMapping();
            rgbMapping = GetRgbMapping();
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new EventArgs());
        }

        private void CancelButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SetMapping(typeMapping, rgbMapping);
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            { this.Parent.IsVisible = false; }));
            OnClosed?.Invoke(this, new EventArgs());
        }

        private void RgbButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var control = sender as Control;
            int num = Convert.ToInt32(Regex.Replace(control.Name, "[^0-9]", "")) - 1;

            rgbControl.SetHue(num, hue[num]);
            mainViewer.IsVisible = false;
            rgbViewer.IsVisible = true;
        }

        private void rgbControl_OnClosed(object sender, ByteEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                mainViewer.IsVisible = true;
                if (e.Index != -1)
                {
                    hue[e.Index] = e.Value;
                    rgbButton[e.Index].Content = e.Value.ToString();
                    rgbButton[e.Index].Background = new SolidColorBrush(HueToColor.Convert(e.Value));
                }
            }));
        }

        private void SetMapping(byte[] typeMap, byte[] rgbMap)
        {
            SetMapping(typeMap.Take(8).ToArray(), typeMap.Skip(8).Take(6).ToArray(), rgbMap);
        }

        public void SetMapping(byte[] modeMap, byte[] typeMap, byte[] rgbMap)
        {
            for (int i = 0; i < buttonModeCombo.Count; i++)
            {
                buttonModeCombo[i].SelectedIndex = (modeMap[i / 2] >> ((i % 2) * 4)) & 0xF;
                buttonTypeCombo[i].SelectedIndex = (typeMap[i / 2] >> ((i % 2) * 4)) & 0xF;
                if (buttonTypeCombo[i].SelectedIndex == 0)
                    rgbButton[i].IsEnabled = false;
                else
                    rgbButton[i].IsEnabled = true;
                rgbButton[i].Content = rgbMap[i].ToString();
                rgbButton[i].Background = new SolidColorBrush(HueToColor.Convert(rgbMap[i]));
            }
            fadeTimeNumeric.Value = modeMap[6] | modeMap[7] << 8;

            Array.Copy(modeMap, typeMapping, 8);
            Array.Copy(typeMap, 0, typeMapping, 8, 6);
            Array.Copy(rgbMap, 0, rgbMapping, 0, 12);
        }

        public byte[] GetTypeMapping()
        {
            byte[] bytes = new byte[14];

            for (int i = 0; i < buttonModeCombo.Count; i++)
            {
                bytes[i / 2] |= Convert.ToByte((buttonModeCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
                bytes[(i / 2) + 8] |= Convert.ToByte((buttonTypeCombo[i].SelectedIndex & 0xF) << ((i % 2) * 4));
            }
            bytes[6] = Convert.ToByte(Convert.ToInt16(fadeTimeNumeric.Value) & 0xFF);
            bytes[7] = Convert.ToByte((Convert.ToInt16(fadeTimeNumeric.Value) >> 8) & 0xFF);

            return bytes;
        }

        public byte[] GetRgbMapping()
        {
            byte[] bytes = new byte[12];

            for (int i = 0; i < buttonModeCombo.Count; i++)
            {
                bytes[i] = hue[i];
            }

            return bytes;
        }
    }
}
