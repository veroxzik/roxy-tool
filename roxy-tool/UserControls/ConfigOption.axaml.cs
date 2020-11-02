using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using roxy_tool.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using roxy_tool.Classes;
using Roxy.Lib;

namespace roxy_tool.UserControls
{
    public class ConfigOption : UserControl
    {
        TextBlock labelText;
        Grid inputGrid;
        TextBox inputText;
        NumericUpDown inputNumeric;
        TextBlock unitsText;
        ComboBox inputCombo;
        Button input1Button;
        Button input2Button;
        StackPanel checkBoxPanel;
        List<uint> boardSettings = new List<uint>();
        byte color1, color2;

        public EventHandler Click;
        public EventHandler Click2;

        public ConfigOption()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            labelText = this.FindControl<TextBlock>("labelText");
            inputGrid = this.FindControl<Grid>("inputGrid");
            inputText = this.FindControl<TextBox>("inputText");
            inputNumeric = this.FindControl<NumericUpDown>("inputNumeric");
            unitsText = this.FindControl<TextBlock>("unitsText");
            inputCombo = this.FindControl<ComboBox>("inputCombo");
            input1Button = this.FindControl<Button>("input1Button");
            input1Button.Click += Input1Button_Click;
            input2Button = this.FindControl<Button>("input2Button");
            input2Button.Click += Input2Button_Click;
            checkBoxPanel = this.FindControl<StackPanel>("checkBoxPanel");
        }

        private void Input1Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Click?.Invoke(this, new EventArgs());
        }

        private void Input2Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Click2?.Invoke(this, new EventArgs());
        }       

        public void SetBoard(BoardType board)
        {
            if (boardSettings.Count == 1)
            {
                if ((boardSettings[0] & (uint)board) > 0)
                    this.IsVisible = true;
                else
                    this.IsVisible = false;
            }
            else if (boardSettings.Count > 1)
            {
                for (int i = 0; i < boardSettings.Count; i++)
                {
                    if ((boardSettings[i] & (uint)board) > 0)
                        checkBoxPanel.Children[i].IsVisible = true;
                    else
                        checkBoxPanel.Children[i].IsVisible = false;
                }
            }
        }

        public string LabelText
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        public string InputText
        {
            set 
            {
                inputText.IsVisible = true;
                inputNumeric.IsVisible = false;
                unitsText.IsVisible = false;
                inputCombo.IsVisible = false;
                input1Button.IsVisible = false;
                input2Button.IsVisible = false;
                checkBoxPanel.IsVisible = false;
            }
        }

        public void SetText(string text)
        {
            inputText.Text = text;
        }

        public string GetText()
        {
            return inputText.Text;
        }

        public string NumericOptions
        {
            set
            {
                string[] values = value.Split('|');
                inputNumeric.Minimum = Convert.ToInt32(values[0]);
                inputNumeric.Maximum = Convert.ToInt32(values[1]);
                unitsText.Text = values[2];
                inputText.IsVisible = false;
                inputNumeric.IsVisible = true;
                unitsText.IsVisible = true;
                inputCombo.IsVisible = false;
                input1Button.IsVisible = false;
                input2Button.IsVisible = false;
                checkBoxPanel.IsVisible = false;
            }
        }

        public void SetNumber(int num)
        {
            inputNumeric.Value = num;
        }

        public int GetNumber()
        {
            return Convert.ToInt32(inputNumeric.Value);
        }

        public string ComboOptions
        {
            set 
            { 
                string[] values = value.Split('|');
                inputCombo.Items = values;
                inputText.IsVisible = false;
                inputNumeric.IsVisible = false;
                unitsText.IsVisible = false;
                inputCombo.IsVisible = true;
                inputCombo.SelectedIndex = 0;
                input1Button.IsVisible = false;
                input2Button.IsVisible = false;
                checkBoxPanel.IsVisible = false;
            }
        }

        public void SetCombo(int index)
        {
            inputCombo.SelectedIndex = index;
        }

        public int GetCombo()
        {
            return inputCombo.SelectedIndex;
        }

        public string ButtonText
        {
            set
            {
                string[] text = value.Split('|');
                if(text.Length == 1)
                {
                    input1Button.IsVisible = true;
                    input1Button.Content = text[0];
                    input2Button.IsVisible = false;
                } 
                else if(text.Length == 2)
                {
                    input1Button.IsVisible = true;
                    input1Button.Content = text[0];
                    input2Button.IsVisible = true;
                    input2Button.Content = text[1];
                    inputGrid.ColumnDefinitions = new ColumnDefinitions("*,*");
                }
                inputText.IsVisible = false;
                inputNumeric.IsVisible = false;
                unitsText.IsVisible = false;
                inputCombo.IsVisible = false;
                checkBoxPanel.IsVisible = false;
            }
        }

        public void SetButtonColor(int index, Color color)
        {
            if (index < 0 || index > 1)
                return;

            if (index == 0)
                input1Button.Background = new SolidColorBrush(color);
            else
                input2Button.Background = new SolidColorBrush(color);
        }

        public void SetButtonColor(int index, byte hue)
        {
            if (index < 0 || index > 1)
                return;

            if (index == 0)
                color1 = hue;
            else
                color2 = hue;

            SetButtonColor(index, HueToColor.Convert(hue));
        }

        public byte GetHue(int index)
        {
            if (index < 0 || index > 1)
                return 0;

            if (index == 0)
                return color1;
            else
                return color2;
        }

        public string CheckOptions
        {
            set
            {
                string[] text = value.Split('|');
                foreach (var option in text)
                {
                    checkBoxPanel.Children.Add(new CheckBox() { Content = option });
                }
                inputText.IsVisible = false;
                inputNumeric.IsVisible = false;
                unitsText.IsVisible = true;
                inputCombo.IsVisible = false;
                input1Button.IsVisible = false;
                input2Button.IsVisible = false;
                checkBoxPanel.IsVisible = true;
            }
        }

        public void SetCheck(uint input)
        {
            for (int i = 0; i < checkBoxPanel.Children.Count; i++)
            {
                (checkBoxPanel.Children[i] as CheckBox).IsChecked = ((input >> i) & 0x1) > 0;
            }
        }

        public uint GetCheck()
        {
            uint ret = 0;
            for (int i = 0; i < checkBoxPanel.Children.Count; i++)
            {
                ret |= Convert.ToUInt32(((checkBoxPanel.Children[i] as CheckBox).IsChecked == true)) << i;
            }
            return ret;
        }

        public string ConfigSettings
        {
            set
            {
                string[] text = value.Split('|');
                foreach (var option in text)
                {
                    uint temp = 0;
                    if(option.Contains('r'))
                    {
                        temp |= (uint)BoardType.Roxy;
                    }
                    if (option.Contains('a'))
                    {
                        temp |= (uint)BoardType.arcin;
                    }
                    if (option.Contains('x'))
                    {
                        temp |= (uint)BoardType.arcinRoxy;
                    }
                    boardSettings.Add(temp);
                }
            }
        }
    }
}
