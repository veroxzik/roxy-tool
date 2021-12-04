using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Roxy.Lib;
using roxy_tool.Classes;
using roxy_tool.Enums;
using SharpDX.Text;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace roxy_tool.UserControls
{
    public class ConfigPanel : UserControl
    {
        StackPanel optionsPanel;

        Dictionary<string, ConfigOption> options = new Dictionary<string, ConfigOption>();

        public EventHandler<ConfigPanelEventArgs> SubPanelChanged;

        public ConfigPanel()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.optionsPanel = this.FindControl<StackPanel>("optionsPanel");

            Type type = typeof(OptionStrings);
            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var name = (string)field.GetValue(null);
                var control = this.FindControl<ConfigOption>(name);
                if (control != null)
                    options.Add(name, control);
            }

            options[OptionStrings.RgbConfig].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.RGB1, options[OptionStrings.RgbConfig].GetHue(0)); });
            options[OptionStrings.RgbConfig].Click2 += ((s, e) => { InvokeSubPanel(ControlSubPanel.RGB2, options[OptionStrings.RgbConfig].GetHue(1)); });
            options[OptionStrings.KeyboardMapping].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.KeyMapping); });
            options[OptionStrings.ButtonLedMode].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.ButtonLedMode); });
            options[OptionStrings.DeviceControl].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.DeviceControl); });
            options[OptionStrings.JoystickMapping].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.JoystickMapping); });
            options[OptionStrings.TurntableControl].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.TurntableControl); });
            options[OptionStrings.ArgbControl].Click += ((s, e) => { InvokeSubPanel(ControlSubPanel.ArgbControl); });
        }

        private void InvokeSubPanel(ControlSubPanel panel, byte value = 0)
        {
            SubPanelChanged?.Invoke(
                this, new ConfigPanelEventArgs(panel, value));
        }

        public void SetBoard(BoardType board)
        {
            Dispatcher.UIThread.InvokeAsync(new Action(() =>
            {
                foreach (var child in optionsPanel.Children)
                {
                    ((ConfigOption)child).SetBoard(board);
                }
            }));
        }

        public void SetMapping(StandardConfiguration stdConfig, RgbConfiguration rgbConfig)
        {
            options[OptionStrings.BoardLabel].SetText(stdConfig.Label);
            options[OptionStrings.FlagsCheck].SetCheck(stdConfig.Flags);
            options[OptionStrings.Qe1Sensitivity].SetCombo(ConfigDefines.ByteToCombo(stdConfig.QE1Sens));
            options[OptionStrings.Qe1ReductionRatio].SetCombo(stdConfig.QE1ReductionRatio);
            options[OptionStrings.Qe1DeadzoneAngle].SetCombo(stdConfig.QE1AxisDeadzone);
            options[OptionStrings.Qe2Sensitivity].SetCombo(ConfigDefines.ByteToCombo(stdConfig.QE2Sens));
            options[OptionStrings.Qe2ReductionRatio].SetCombo(stdConfig.QE2ReductionRatio);
            options[OptionStrings.Qe2DeadzoneAngle].SetCombo(stdConfig.QE2AxisDeadzone);
            options[OptionStrings.Ps2Mode].SetCombo(stdConfig.PS2Mode);
            options[OptionStrings.RgbInterface].SetCombo(stdConfig.RgbInterface);
            options[OptionStrings.RgbBrightness].SetNumber(stdConfig.RgbBrightness);
            options[OptionStrings.DebounceTime].SetNumber(stdConfig.ButtonDebounce);
            options[OptionStrings.RgbMode].SetCombo(rgbConfig.Mode);
            options[OptionStrings.RgbConfig].SetButtonColor(0, rgbConfig.Led1Hue);
            options[OptionStrings.RgbConfig].SetButtonColor(1, rgbConfig.Led2Hue);
            options[OptionStrings.AxisDebounceTime].SetNumber(stdConfig.AxisDebounce);
            options[OptionStrings.AxisSustainTime].SetNumber(stdConfig.AxisSustain);
            options[OptionStrings.AscEmulation].SetCombo(stdConfig.AscEmulation);
            options[OptionStrings.ControllerOutput].SetCombo(stdConfig.ControllerOutput);
        }

        public void SetColor(int index, byte value)
        {
            options[OptionStrings.RgbConfig].SetButtonColor(index, value);
        }

        public byte[] GetStdMapping()
        {
            byte[] config = new byte[30];
            byte[] label = Encoding.ASCII.GetBytes(options[OptionStrings.BoardLabel].GetText());
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, config, 0, 12);
            uint flags = options[OptionStrings.FlagsCheck].GetCheck();
            Array.Copy(BitConverter.GetBytes(flags), 0, config, 12, 4);
            config[16] = (byte)ConfigDefines.ComboToByte(options[OptionStrings.Qe1Sensitivity].GetCombo());
            config[17] = (byte)ConfigDefines.ComboToByte(options[OptionStrings.Qe2Sensitivity].GetCombo());
            config[18] = (byte)options[OptionStrings.Ps2Mode].GetCombo();
            config[19] = (byte)options[OptionStrings.RgbInterface].GetCombo();
            config[20] = (byte)options[OptionStrings.RgbBrightness].GetNumber();
            config[21] = (byte)options[OptionStrings.DebounceTime].GetNumber();
            config[22] = (byte)options[OptionStrings.AscEmulation].GetCombo();
            config[23] = (byte)options[OptionStrings.AxisDebounceTime].GetNumber();
            config[24] = (byte)options[OptionStrings.ControllerOutput].GetCombo();
            config[25] = (byte)options[OptionStrings.Qe1ReductionRatio].GetCombo();
            config[26] = (byte)options[OptionStrings.Qe2ReductionRatio].GetCombo();
            config[27] = (byte)options[OptionStrings.AxisSustainTime].GetNumber();
            config[28] = (byte)options[OptionStrings.Qe1DeadzoneAngle].GetNumber();
            config[29] = (byte)options[OptionStrings.Qe2DeadzoneAngle].GetNumber();

            return config;
        }

        public byte[] GetRgbMapping()
        {
            byte[] config = new byte[3];
            config[0] = (byte)options[OptionStrings.RgbMode].GetCombo();
            config[1] = (byte)options[OptionStrings.RgbConfig].GetHue(0);
            config[2] = (byte)options[OptionStrings.RgbConfig].GetHue(1);

            return config;
        }
    }
}
