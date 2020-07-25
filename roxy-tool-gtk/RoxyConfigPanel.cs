using Gdk;
using Gtk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Roxy.Lib;

namespace roxy_tool
{
    public class RoxyConfigPanel : IConfigPanel
    {
        public Grid OptionsGrid { get; private set; }
        private Dictionary<string, Widget> widgetDict = new Dictionary<string, Widget>();

        public RoxyConfigPanel()
        {
            OptionsGrid = new Grid();
            Entry boardLabelEntry = new Entry();
            boardLabelEntry.MaxLength = 11;
            boardLabelEntry.Hexpand = true;
            widgetDict[OptionStrings.BoardLabel] = boardLabelEntry;
            CheckButton hideSerialCheck = new CheckButton("Hide Serial Number");
            widgetDict[OptionStrings.HideSerial] = hideSerialCheck;
            CheckButton invertQe1Check = new CheckButton("Invert QE1");
            widgetDict[OptionStrings.InvertQe1] = invertQe1Check;
            CheckButton invertQe2Check = new CheckButton("Invert QE2");
            widgetDict[OptionStrings.InvertQe2] = invertQe2Check;
            CheckButton led1Check = new CheckButton("LED1 always on");
            widgetDict[OptionStrings.Led1On] = led1Check;
            CheckButton analogInputCheck = new CheckButton("Analog Input (Not QE)");
            widgetDict[OptionStrings.AnalogInput] = analogInputCheck;
            CheckButton analogButtonsCheck = new CheckButton("Enable Analog Buttons");
            widgetDict[OptionStrings.AnalogButtons] = analogButtonsCheck;
            Grid flagsGrid = new Grid();
            flagsGrid.RowHomogeneous = true;
            flagsGrid.Attach(hideSerialCheck, 0, 0, 1, 1);
            flagsGrid.Attach(invertQe1Check, 0, 1, 1, 1);
            flagsGrid.Attach(invertQe2Check, 0, 2, 1, 1);
            flagsGrid.Attach(led1Check, 0, 3, 1, 1);
            flagsGrid.Attach(analogInputCheck, 0, 4, 1, 1);
            flagsGrid.Attach(analogButtonsCheck, 0, 5, 1, 1);
            ComboBox qe1Combo = new ComboBox(new string[] { "1:1", "1:2", "1:3", "1:4", "1:6", "1:8", "1:11", "1:16",
                "2:1", "3:1", "4:1", "6:1", "8:1", "11:1", "16:1", "600 PPR"});
            qe1Combo.Active = 0;
            widgetDict[OptionStrings.Qe1Sensitivity] = qe1Combo;
            ComboBox qe2Combo = new ComboBox(new string[] { "1:1", "1:2", "1:3", "1:4", "1:6", "1:8", "1:11", "1:16",
                "2:1", "3:1", "4:1", "6:1", "8:1", "11:1", "16:1", "600 PPR"});
            qe2Combo.Active = 0;
            widgetDict[OptionStrings.Qe2Sensitivity] = qe2Combo;
            ComboBox ps2Combo = new ComboBox(new string[] { "Disabled", "Pop'n Music", "IIDX (QE1)", "IIDX (QE2)" });
            ps2Combo.Active = 0;
            widgetDict[OptionStrings.Ps2Mode] = ps2Combo;
            ComboBox rgbInterfaceCombo = new ComboBox(new string[] { "Disabled", "WS2812B (via Bypass Port)", "TLC59711 (On-board)" });
            rgbInterfaceCombo.Active = 0;
            widgetDict[OptionStrings.RgbInterface] = rgbInterfaceCombo;
            SpinButton rgbBrightnessSpinButton = new SpinButton(0, 255, 1);
            rgbBrightnessSpinButton.Value = 50;
            widgetDict[OptionStrings.RgbBrightness] = rgbBrightnessSpinButton;
            Label rgbPercent = new Label("(255 Max)");
            rgbPercent.WidthRequest = 5;
            HBox rgbBrightnessBox = new HBox(false, 2);
            rgbBrightnessBox.PackStart(rgbBrightnessSpinButton, false, true, 2);
            rgbBrightnessBox.PackStart(rgbPercent, false, false, 2);
            SpinButton debounceSpinButton = new SpinButton(0, 255, 1);
            debounceSpinButton.Value = 50;
            widgetDict[OptionStrings.DebounceTime] = debounceSpinButton;
            Label msLabel = new Label("ms");
            msLabel.WidthRequest = 5;
            HBox debounceBox = new HBox(false, 2);
            debounceBox.PackStart(debounceSpinButton, false, true, 2);
            debounceBox.PackStart(msLabel, false, false, 2);
            ComboBox rgbModeCombo = new ComboBox(new string[] { "Reactive Fallback", "RGB1/2 pulse with QE1/2", "Turbocharger (TLC59711)" });
            rgbModeCombo.Active = 0;
            widgetDict[OptionStrings.RgbMode] = rgbModeCombo;
            RGBA col = new RGBA { Red = 1.0, Alpha = 1.0};
            ColorButton rgb1Button = new ColorButton(col);
            widgetDict[OptionStrings.Rgb1Color] = rgb1Button;
            ColorButton rgb2Button = new ColorButton(col);
            widgetDict[OptionStrings.Rgb2Color] = rgb2Button;
            HBox rgbButtonBox = new HBox(false, 2);
            rgbButtonBox.PackStart(new Label("RGB 1:"), false, true, 2);
            rgbButtonBox.PackStart(rgb1Button, true, true, 2);
            rgbButtonBox.PackStart(new Label("RGB 2:"), false, true, 2);
            rgbButtonBox.PackStart(rgb2Button, true, true, 2);
            ComboBox emulationCombo = new ComboBox(new string[] { "Disabled", "IIDX premium", "SDVX NEMSYS entry" });
            emulationCombo.Active = 0;
            widgetDict[OptionStrings.AscEmulation] = emulationCombo;
            SpinButton axisDebounceSpinButton = new SpinButton(0, 255, 1);
            axisDebounceSpinButton.Value = 20;
            widgetDict[OptionStrings.AxisDebounceTime] = axisDebounceSpinButton;
            Label msLabel2 = new Label("ms");
            msLabel2.WidthRequest = 5;
            HBox axisDebounceBox = new HBox(false, 2);
            axisDebounceBox.PackStart(axisDebounceSpinButton, false, true, 2);
            axisDebounceBox.PackStart(msLabel2, false, false, 2);

            OptionsGrid.Attach(new Label("Board Label:"), 0, 0, 1, 1);
            OptionsGrid.Attach(boardLabelEntry, 1, 0, 1, 1);
            OptionsGrid.Attach(new Label("Flags:"), 0, 1, 1, 1);
            OptionsGrid.Attach(flagsGrid, 1, 1, 1, 1);
            OptionsGrid.Attach(new Label("QE1 Sensitivity:"), 0, 2, 1, 1);
            OptionsGrid.Attach(qe1Combo, 1, 2, 1, 1);
            OptionsGrid.Attach(new Label("QE2 Sensitivity:"), 0, 3, 1, 1);
            OptionsGrid.Attach(qe2Combo, 1, 3, 1, 1);
            OptionsGrid.Attach(new Label("PS2 Mode:"), 0, 4, 1, 1);
            OptionsGrid.Attach(ps2Combo, 1, 4, 1, 1);
            OptionsGrid.Attach(new Label("RGB Interface:"), 0, 5, 1, 1);
            OptionsGrid.Attach(rgbInterfaceCombo, 1, 5, 1, 1);
            OptionsGrid.Attach(new Label("RGB Brightness:"), 0, 6, 1, 1);
            OptionsGrid.Attach(rgbBrightnessBox, 1, 6, 1, 1);
            OptionsGrid.Attach(new Label("Debounce Time:"), 0, 7, 1, 1);
            OptionsGrid.Attach(debounceBox, 1, 7, 1, 1);
            OptionsGrid.Attach(new Label("RGB Mode:"), 0, 8, 1, 1);
            OptionsGrid.Attach(rgbModeCombo, 1, 8, 1, 1);
            OptionsGrid.Attach(new Label("RGB Color:"), 0, 9, 1, 1);
            OptionsGrid.Attach(rgbButtonBox, 1, 9, 1, 1);
            OptionsGrid.Attach(new Label("ASC Emulation:"), 0, 10, 1, 1);
            OptionsGrid.Attach(emulationCombo, 1, 10, 1, 1);
            OptionsGrid.Attach(new Label("Axis Debounce Time:"), 0, 11, 1, 1);
            OptionsGrid.Attach(axisDebounceBox, 1, 11, 1, 1);
        }

        public byte[] GetConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x18;  // Roxy is 24 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes((widgetDict[OptionStrings.BoardLabel] as Entry).Text);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            uint flags =
                Convert.ToUInt32((widgetDict[OptionStrings.AnalogButtons] as CheckButton).Active) << 6 |
                Convert.ToUInt32((widgetDict[OptionStrings.AnalogInput] as CheckButton).Active) << 5 |
                0 << 4 |    // LED2 bit unused
                Convert.ToUInt32((widgetDict[OptionStrings.Led1On] as CheckButton).Active) << 3 |
                Convert.ToUInt32((widgetDict[OptionStrings.InvertQe2] as CheckButton).Active) << 2 |
                Convert.ToUInt32((widgetDict[OptionStrings.InvertQe1] as CheckButton).Active) << 1 |
                Convert.ToUInt32((widgetDict[OptionStrings.HideSerial] as CheckButton).Active);
            Array.Copy(BitConverter.GetBytes(flags), 0, configBytes, 16, 4);
            configBytes[20] = (byte)GetByteFromComboBox((widgetDict[OptionStrings.Qe1Sensitivity] as ComboBox).Active);
            configBytes[21] = (byte)GetByteFromComboBox((widgetDict[OptionStrings.Qe2Sensitivity] as ComboBox).Active);
            configBytes[22] = (byte)(widgetDict[OptionStrings.Ps2Mode] as ComboBox).Active;
            configBytes[23] = (byte)(widgetDict[OptionStrings.RgbInterface] as ComboBox).Active;
            configBytes[24] = (byte)(widgetDict[OptionStrings.RgbBrightness] as SpinButton).Value;
            configBytes[25] = (byte)(widgetDict[OptionStrings.DebounceTime] as SpinButton).Value;
            configBytes[26] = (byte)(widgetDict[OptionStrings.AscEmulation] as ComboBox).Active;
            configBytes[27] = (byte)(widgetDict[OptionStrings.AxisDebounceTime] as SpinButton).Value;

            return configBytes;
        }

        public void PopulateControls(byte[] configBytes)
        {
            var config = new Configuration(configBytes);

            (widgetDict[OptionStrings.BoardLabel] as Entry).Text = config.Label;
            (widgetDict[OptionStrings.HideSerial] as CheckButton).Active = (config.Flags >> 0 & 0x1) == 0x1;
            (widgetDict[OptionStrings.InvertQe1] as CheckButton).Active = (config.Flags >> 1 & 0x1) == 0x1;
            (widgetDict[OptionStrings.InvertQe2] as CheckButton).Active = (config.Flags >> 2 & 0x1) == 0x1;
            (widgetDict[OptionStrings.Led1On] as CheckButton).Active = (config.Flags >> 3 & 0x1) == 0x1;
            // Ignore LED2 (bit 4)
            (widgetDict[OptionStrings.AnalogInput] as CheckButton).Active = (config.Flags >> 5 & 0x1) == 0x1;
            (widgetDict[OptionStrings.AnalogButtons] as CheckButton).Active = (config.Flags >> 6 & 0x1) == 0x1;
            (widgetDict[OptionStrings.Qe1Sensitivity] as ComboBox).Active = GetComboBoxIndex(config.QE1Sens);
            (widgetDict[OptionStrings.Qe2Sensitivity] as ComboBox).Active = GetComboBoxIndex(config.QE2Sens);
            (widgetDict[OptionStrings.Ps2Mode] as ComboBox).Active = config.PS2Mode;
            (widgetDict[OptionStrings.RgbInterface] as ComboBox).Active = config.RgbInterface;
            (widgetDict[OptionStrings.RgbBrightness] as SpinButton).Value = config.Brightness;
            (widgetDict[OptionStrings.DebounceTime] as SpinButton).Value = config.ButtonDebounce;
            (widgetDict[OptionStrings.AscEmulation] as ComboBox).Active = config.AscEmulation;
            (widgetDict[OptionStrings.AxisDebounceTime] as SpinButton).Value = config.AxisDebounce;
        }

        public void PopulateRgbControls(byte[] configBytes)
        {
            byte mode = configBytes[4];
            //uint flags = BitConverter.ToUInt32(configBytes, 5);
            byte led1Hue = configBytes[5];
            byte led2Hue = configBytes[6];

            (widgetDict[OptionStrings.RgbMode] as ComboBox).Active = mode;
            var rgb = ColorConversion.HSV2RGB(new HSV() { Hue = led1Hue / 255.0 * 360.0, Saturation = 1.0, Value = 1.0 });
            (widgetDict[OptionStrings.Rgb1Color] as ColorButton).Rgba = rgb;
            rgb = ColorConversion.HSV2RGB(new HSV() { Hue = led2Hue / 255.0 * 360.0, Saturation = 1.0, Value = 1.0 });
            (widgetDict[OptionStrings.Rgb2Color] as ColorButton).Rgba = rgb;
        }

        public byte[] GetRgbConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x01;  // RGB config is Segment 1
            configBytes[2] = 0x03;  // Length
            configBytes[3] = 0x00;  // Padding byte
            configBytes[4] = (byte)(widgetDict[OptionStrings.RgbMode] as ComboBox).Active;
            var hsv = ColorConversion.RGB2HSV((widgetDict[OptionStrings.Rgb1Color] as ColorButton).Rgba);
            if (hsv.Saturation == 0)    // Hardcode white edge case
                hsv.Hue = 255;
            configBytes[5] = (byte)(hsv.Hue / 360.0 * 255.0);
            hsv = ColorConversion.RGB2HSV((widgetDict[OptionStrings.Rgb2Color] as ColorButton).Rgba);
            if (hsv.Saturation == 0)    // Hardcode white edge case
                hsv.Hue = 255;
            configBytes[6] = (byte)(hsv.Hue / 360.0 * 255.0);

            return configBytes;
        }

        public sbyte GetByteFromComboBox(int index)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Value == index).FirstOrDefault().Key;
        }

        public int GetComboBoxIndex(sbyte sens)
        {
            return ConfigDefines.ComboBoxDict.Where(x => x.Key == sens).FirstOrDefault().Value;
        }
    }
}
