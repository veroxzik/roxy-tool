using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roxy_tool
{
    public class ArcinConfigPanel : IConfigPanel
    {
        public Grid OptionsGrid { get; private set; }
        private Dictionary<string, Widget> widgetDict = new Dictionary<string, Widget>();

        private Dictionary<sbyte, int> comboBoxDict = new Dictionary<sbyte, int>()
        {
            {0, 0 },
            {-2, 1 },
            {-3, 2 },
            {-4, 3 },
            {-6, 4 },
            {-8, 5 },
            {-11, 6 },
            {-16, 7 },
            {2, 8 },
            {3, 9 },
            {4, 10 },
            {6, 11 },
            {8, 12 },
            {11, 13 },
            {16, 14 },
        };

        public ArcinConfigPanel()
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
            CheckButton led2Check = new CheckButton("LED2 always on");
            widgetDict[OptionStrings.Led2On] = led2Check;
            //CheckButton analogInputCheck = new CheckButton("Analog Input (Not QE)");
            //widgetDict[OptionStrings.AnalogInput] = analogInputCheck;
            //CheckButton analogButtonsCheck = new CheckButton("Enable Analog Buttons");
            //widgetDict[OptionStrings.AnalogButtons] = analogButtonsCheck;
            Grid flagsGrid = new Grid();
            flagsGrid.RowHomogeneous = true;
            flagsGrid.Attach(hideSerialCheck, 0, 0, 1, 1);
            flagsGrid.Attach(invertQe1Check, 0, 1, 1, 1);
            flagsGrid.Attach(invertQe2Check, 0, 2, 1, 1);
            flagsGrid.Attach(led1Check, 0, 3, 1, 1);
            flagsGrid.Attach(led2Check, 0, 4, 1, 1);
            //flagsGrid.Attach(analogInputCheck, 0, 5, 1, 1);
            //flagsGrid.Attach(analogButtonsCheck, 0, 6, 1, 1);
            ComboBox qe1Combo = new ComboBox(new string[] { "1:1", "1:2", "1:3", "1:4", "1:6", "1:8", "1:11", "1:16",
                "2:1", "3:1", "4:1", "6:1", "8:1", "11:1", "16:1"});
            qe1Combo.Active = 0;
            widgetDict[OptionStrings.Qe1Sensitivity] = qe1Combo;
            ComboBox qe2Combo = new ComboBox(new string[] { "1:1", "1:2", "1:3", "1:4", "1:6", "1:8", "1:11", "1:16",
                "2:1", "3:1", "4:1", "6:1", "8:1", "11:1", "16:1"});
            qe2Combo.Active = 0;
            widgetDict[OptionStrings.Qe2Sensitivity] = qe2Combo;
            ComboBox ps2Combo = new ComboBox(new string[] { "Disabled", "Pop'n Music", "IIDX (QE1)", "IIDX (QE2)" });
            ps2Combo.Active = 0;
            widgetDict[OptionStrings.Ps2Mode] = ps2Combo;
            ComboBox rgbInterfaceCombo = new ComboBox(new string[] { "Disabled", "WS2812B (QE9)" });
            rgbInterfaceCombo.Active = 0;
            widgetDict[OptionStrings.RgbInterface] = rgbInterfaceCombo;

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
        }

        public byte[] GetConfigBytes()
        {
            byte[] configBytes = new byte[64];
            configBytes[0] = 0xc0;  // Report ID
            configBytes[1] = 0x00;  // Segment must be 0
            configBytes[2] = 0x14;  // arcin is 20 bytes
            configBytes[3] = 0x00;  // Padding byte
            byte[] label = Encoding.ASCII.GetBytes((widgetDict[OptionStrings.BoardLabel] as Entry).Text);
            Array.Resize(ref label, 12);
            Array.Copy(label, 0, configBytes, 4, 12);
            uint flags =
                //Convert.ToUInt32((widgetDict[OptionStrings.AnalogButtons] as CheckButton).Active) << 6 |
                //Convert.ToUInt32((widgetDict[OptionStrings.AnalogInput] as CheckButton).Active) << 5 |
                Convert.ToUInt32((widgetDict[OptionStrings.Led2On] as CheckButton).Active) << 4 |
                Convert.ToUInt32((widgetDict[OptionStrings.Led1On] as CheckButton).Active) << 3 |
                Convert.ToUInt32((widgetDict[OptionStrings.InvertQe2] as CheckButton).Active) << 2 |
                Convert.ToUInt32((widgetDict[OptionStrings.InvertQe1] as CheckButton).Active) << 1 |
                Convert.ToUInt32((widgetDict[OptionStrings.HideSerial] as CheckButton).Active);
            Array.Copy(BitConverter.GetBytes(flags), 0, configBytes, 16, 4);
            configBytes[20] = (byte)GetByteFromComboBox((widgetDict[OptionStrings.Qe1Sensitivity] as ComboBox).Active);
            configBytes[21] = (byte)GetByteFromComboBox((widgetDict[OptionStrings.Qe2Sensitivity] as ComboBox).Active);
            configBytes[22] = (byte)(widgetDict[OptionStrings.Ps2Mode] as ComboBox).Active;
            configBytes[23] = (byte)(widgetDict[OptionStrings.RgbInterface] as ComboBox).Active;

            return configBytes;
        }

        public void PopulateControls(byte[] configBytes)
        {
            string label = Encoding.ASCII.GetString(configBytes, 4, 12);
            uint flags = BitConverter.ToUInt32(configBytes, 16);
            sbyte qe1Sens = (sbyte)configBytes[20];
            sbyte qe2Sens = (sbyte)configBytes[21];
            byte ps2Mode = configBytes[22];
            byte rgbInterface = configBytes[23];

            (widgetDict[OptionStrings.BoardLabel] as Entry).Text = label;
            (widgetDict[OptionStrings.HideSerial] as CheckButton).Active = (flags >> 0 & 0x1) == 0x1;
            (widgetDict[OptionStrings.InvertQe1] as CheckButton).Active = (flags >> 1 & 0x1) == 0x1;
            (widgetDict[OptionStrings.InvertQe2] as CheckButton).Active = (flags >> 2 & 0x1) == 0x1;
            (widgetDict[OptionStrings.Led1On] as CheckButton).Active = (flags >> 3 & 0x1) == 0x1;
            (widgetDict[OptionStrings.Led2On] as CheckButton).Active = (flags >> 4 & 0x1) == 0x1;
            //(widgetDict[OptionStrings.AnalogInput] as CheckButton).Active = (flags >> 5 & 0x1) == 0x1;
            //(widgetDict[OptionStrings.AnalogButtons] as CheckButton).Active = (flags >> 6 & 0x1) == 0x1;
            (widgetDict[OptionStrings.Qe1Sensitivity] as ComboBox).Active = GetComboBoxIndex(qe1Sens);
            (widgetDict[OptionStrings.Qe2Sensitivity] as ComboBox).Active = GetComboBoxIndex(qe2Sens);
            (widgetDict[OptionStrings.Ps2Mode] as ComboBox).Active = ps2Mode;
            (widgetDict[OptionStrings.RgbInterface] as ComboBox).Active = rgbInterface;
        }

        public void PopulateRgbControls(byte[] configBytes)
        {
        }

        public byte[] GetRgbConfigBytes()
        {
            byte[] configBytes = new byte[64];
            return configBytes;
        }

        public sbyte GetByteFromComboBox(int index)
        {
            return comboBoxDict.Where(x => x.Value == index).FirstOrDefault().Key;
        }

        public int GetComboBoxIndex(sbyte sens)
        {
            return comboBoxDict.Where(x => x.Key == sens).FirstOrDefault().Value;
        }

    }
}
