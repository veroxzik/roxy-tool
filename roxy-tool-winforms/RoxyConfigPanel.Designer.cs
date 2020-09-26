namespace Roxy.Tool.WinForms
{
    partial class RoxyConfigPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.rgb2ColorButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.rgb1ColorButton = new System.Windows.Forms.Button();
            this.rgbModeCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.debounceNum = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialNumCheck = new System.Windows.Forms.CheckBox();
            this.invertQE1Check = new System.Windows.Forms.CheckBox();
            this.invertQE2Check = new System.Windows.Forms.CheckBox();
            this.led1Check = new System.Windows.Forms.CheckBox();
            this.analogInputCheck = new System.Windows.Forms.CheckBox();
            this.analogButtonsCheck = new System.Windows.Forms.CheckBox();
            this.ascEmuCombo = new System.Windows.Forms.ComboBox();
            this.rgbPercentLabel = new System.Windows.Forms.Label();
            this.rgbBrightnessNum = new System.Windows.Forms.NumericUpDown();
            this.rgbBrightnessLabel = new System.Windows.Forms.Label();
            this.rgbCombo = new System.Windows.Forms.ComboBox();
            this.rgbModeLabel = new System.Windows.Forms.Label();
            this.ps2Combo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.qe2Combo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.qe1Combo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.invertLightsCheck = new System.Windows.Forms.CheckBox();
            this.splitQE1Check = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.axisDebounceNum = new System.Windows.Forms.NumericUpDown();
            this.controllerOutputCombo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.keyMapButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonLedModeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.debounceNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbBrightnessNum)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axisDebounceNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 816);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 25);
            this.label10.TabIndex = 67;
            this.label10.Text = "ASC Emulation";
            // 
            // rgb2ColorButton
            // 
            this.rgb2ColorButton.Location = new System.Drawing.Point(328, 757);
            this.rgb2ColorButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rgb2ColorButton.Name = "rgb2ColorButton";
            this.rgb2ColorButton.Size = new System.Drawing.Size(127, 42);
            this.rgb2ColorButton.TabIndex = 66;
            this.rgb2ColorButton.Text = "RGB 2";
            this.rgb2ColorButton.UseVisualStyleBackColor = true;
            this.rgb2ColorButton.Click += new System.EventHandler(this.rgb2ColorButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 766);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 25);
            this.label9.TabIndex = 65;
            this.label9.Text = "RGB Color";
            // 
            // rgb1ColorButton
            // 
            this.rgb1ColorButton.Location = new System.Drawing.Point(172, 757);
            this.rgb1ColorButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rgb1ColorButton.Name = "rgb1ColorButton";
            this.rgb1ColorButton.Size = new System.Drawing.Size(127, 42);
            this.rgb1ColorButton.TabIndex = 64;
            this.rgb1ColorButton.Text = "RGB 1";
            this.rgb1ColorButton.UseVisualStyleBackColor = true;
            this.rgb1ColorButton.Click += new System.EventHandler(this.rgb1ColorButton_Click);
            // 
            // rgbModeCombo
            // 
            this.rgbModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rgbModeCombo.FormattingEnabled = true;
            this.rgbModeCombo.Items.AddRange(new object[] {
            "HID Only",
            "RGB1/2 pulse with QE1/2",
            "Turbocharger (TLC59711)"});
            this.rgbModeCombo.Location = new System.Drawing.Point(174, 707);
            this.rgbModeCombo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rgbModeCombo.Name = "rgbModeCombo";
            this.rgbModeCombo.Size = new System.Drawing.Size(277, 32);
            this.rgbModeCombo.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 713);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 25);
            this.label8.TabIndex = 62;
            this.label8.Text = "RGB Mode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(411, 663);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 25);
            this.label6.TabIndex = 61;
            this.label6.Text = "ms";
            // 
            // debounceNum
            // 
            this.debounceNum.Location = new System.Drawing.Point(174, 659);
            this.debounceNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.debounceNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.debounceNum.Name = "debounceNum";
            this.debounceNum.Size = new System.Drawing.Size(226, 29);
            this.debounceNum.TabIndex = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 663);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 25);
            this.label7.TabIndex = 59;
            this.label7.Text = "Debounce Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 25);
            this.label2.TabIndex = 46;
            this.label2.Text = "Flags:";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(174, 6);
            this.labelText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelText.MaxLength = 11;
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(275, 29);
            this.labelText.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 44;
            this.label1.Text = "Board Label:";
            // 
            // serialNumCheck
            // 
            this.serialNumCheck.AutoSize = true;
            this.serialNumCheck.Location = new System.Drawing.Point(6, 6);
            this.serialNumCheck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.serialNumCheck.Name = "serialNumCheck";
            this.serialNumCheck.Size = new System.Drawing.Size(207, 29);
            this.serialNumCheck.TabIndex = 0;
            this.serialNumCheck.Text = "Hide Serial Number";
            this.serialNumCheck.UseVisualStyleBackColor = true;
            // 
            // invertQE1Check
            // 
            this.invertQE1Check.AutoSize = true;
            this.invertQE1Check.Location = new System.Drawing.Point(6, 47);
            this.invertQE1Check.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.invertQE1Check.Name = "invertQE1Check";
            this.invertQE1Check.Size = new System.Drawing.Size(131, 29);
            this.invertQE1Check.TabIndex = 1;
            this.invertQE1Check.Text = "Invert QE1";
            this.invertQE1Check.UseVisualStyleBackColor = true;
            // 
            // invertQE2Check
            // 
            this.invertQE2Check.AutoSize = true;
            this.invertQE2Check.Location = new System.Drawing.Point(6, 88);
            this.invertQE2Check.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.invertQE2Check.Name = "invertQE2Check";
            this.invertQE2Check.Size = new System.Drawing.Size(131, 29);
            this.invertQE2Check.TabIndex = 2;
            this.invertQE2Check.Text = "Invert QE2";
            this.invertQE2Check.UseVisualStyleBackColor = true;
            // 
            // led1Check
            // 
            this.led1Check.AutoSize = true;
            this.led1Check.Location = new System.Drawing.Point(6, 129);
            this.led1Check.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.led1Check.Name = "led1Check";
            this.led1Check.Size = new System.Drawing.Size(179, 29);
            this.led1Check.TabIndex = 3;
            this.led1Check.Text = "LED1 always on";
            this.led1Check.UseVisualStyleBackColor = true;
            // 
            // analogInputCheck
            // 
            this.analogInputCheck.AutoSize = true;
            this.analogInputCheck.Location = new System.Drawing.Point(6, 170);
            this.analogInputCheck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.analogInputCheck.Name = "analogInputCheck";
            this.analogInputCheck.Size = new System.Drawing.Size(231, 29);
            this.analogInputCheck.TabIndex = 5;
            this.analogInputCheck.Text = "Analog Input (Not QE)";
            this.analogInputCheck.UseVisualStyleBackColor = true;
            // 
            // analogButtonsCheck
            // 
            this.analogButtonsCheck.AutoSize = true;
            this.analogButtonsCheck.Location = new System.Drawing.Point(6, 211);
            this.analogButtonsCheck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.analogButtonsCheck.Name = "analogButtonsCheck";
            this.analogButtonsCheck.Size = new System.Drawing.Size(237, 29);
            this.analogButtonsCheck.TabIndex = 4;
            this.analogButtonsCheck.Text = "Enable Analog Buttons";
            this.analogButtonsCheck.UseVisualStyleBackColor = true;
            // 
            // ascEmuCombo
            // 
            this.ascEmuCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ascEmuCombo.FormattingEnabled = true;
            this.ascEmuCombo.Items.AddRange(new object[] {
            "Disabled",
            "IIDX premium",
            "SDVX NEMSYS entry"});
            this.ascEmuCombo.Location = new System.Drawing.Point(174, 810);
            this.ascEmuCombo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ascEmuCombo.Name = "ascEmuCombo";
            this.ascEmuCombo.Size = new System.Drawing.Size(277, 32);
            this.ascEmuCombo.TabIndex = 68;
            // 
            // rgbPercentLabel
            // 
            this.rgbPercentLabel.AutoSize = true;
            this.rgbPercentLabel.Location = new System.Drawing.Point(350, 615);
            this.rgbPercentLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.rgbPercentLabel.Name = "rgbPercentLabel";
            this.rgbPercentLabel.Size = new System.Drawing.Size(102, 25);
            this.rgbPercentLabel.TabIndex = 58;
            this.rgbPercentLabel.Text = "(255 Max)";
            // 
            // rgbBrightnessNum
            // 
            this.rgbBrightnessNum.Location = new System.Drawing.Point(174, 611);
            this.rgbBrightnessNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rgbBrightnessNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rgbBrightnessNum.Name = "rgbBrightnessNum";
            this.rgbBrightnessNum.Size = new System.Drawing.Size(165, 29);
            this.rgbBrightnessNum.TabIndex = 57;
            this.rgbBrightnessNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // rgbBrightnessLabel
            // 
            this.rgbBrightnessLabel.AutoSize = true;
            this.rgbBrightnessLabel.Location = new System.Drawing.Point(7, 615);
            this.rgbBrightnessLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.rgbBrightnessLabel.Name = "rgbBrightnessLabel";
            this.rgbBrightnessLabel.Size = new System.Drawing.Size(150, 25);
            this.rgbBrightnessLabel.TabIndex = 56;
            this.rgbBrightnessLabel.Text = "RGB Brightness";
            // 
            // rgbCombo
            // 
            this.rgbCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rgbCombo.FormattingEnabled = true;
            this.rgbCombo.Items.AddRange(new object[] {
            "Disabled",
            "WS2812B",
            "TLC59711"});
            this.rgbCombo.Location = new System.Drawing.Point(174, 561);
            this.rgbCombo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rgbCombo.Name = "rgbCombo";
            this.rgbCombo.Size = new System.Drawing.Size(277, 32);
            this.rgbCombo.TabIndex = 55;
            // 
            // rgbModeLabel
            // 
            this.rgbModeLabel.AutoSize = true;
            this.rgbModeLabel.Location = new System.Drawing.Point(6, 567);
            this.rgbModeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.rgbModeLabel.Name = "rgbModeLabel";
            this.rgbModeLabel.Size = new System.Drawing.Size(133, 25);
            this.rgbModeLabel.TabIndex = 54;
            this.rgbModeLabel.Text = "RGB Interface";
            // 
            // ps2Combo
            // 
            this.ps2Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ps2Combo.FormattingEnabled = true;
            this.ps2Combo.Items.AddRange(new object[] {
            "Disabled",
            "Pop\'n Music",
            "IIDX (QE1)",
            "IIDX (QE2)"});
            this.ps2Combo.Location = new System.Drawing.Point(174, 511);
            this.ps2Combo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ps2Combo.Name = "ps2Combo";
            this.ps2Combo.Size = new System.Drawing.Size(277, 32);
            this.ps2Combo.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 517);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 25);
            this.label5.TabIndex = 52;
            this.label5.Text = "PS2 Mode";
            // 
            // qe2Combo
            // 
            this.qe2Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qe2Combo.FormattingEnabled = true;
            this.qe2Combo.Location = new System.Drawing.Point(174, 462);
            this.qe2Combo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.qe2Combo.Name = "qe2Combo";
            this.qe2Combo.Size = new System.Drawing.Size(277, 32);
            this.qe2Combo.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 467);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 50;
            this.label4.Text = "QE2 Sensitivity";
            // 
            // qe1Combo
            // 
            this.qe1Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qe1Combo.FormattingEnabled = true;
            this.qe1Combo.Location = new System.Drawing.Point(174, 412);
            this.qe1Combo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.qe1Combo.Name = "qe1Combo";
            this.qe1Combo.Size = new System.Drawing.Size(277, 32);
            this.qe1Combo.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 417);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 48;
            this.label3.Text = "QE1 Sensitivity";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.serialNumCheck);
            this.flowLayoutPanel1.Controls.Add(this.invertQE1Check);
            this.flowLayoutPanel1.Controls.Add(this.invertQE2Check);
            this.flowLayoutPanel1.Controls.Add(this.led1Check);
            this.flowLayoutPanel1.Controls.Add(this.analogInputCheck);
            this.flowLayoutPanel1.Controls.Add(this.analogButtonsCheck);
            this.flowLayoutPanel1.Controls.Add(this.invertLightsCheck);
            this.flowLayoutPanel1.Controls.Add(this.splitQE1Check);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(174, 54);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(279, 347);
            this.flowLayoutPanel1.TabIndex = 47;
            // 
            // invertLightsCheck
            // 
            this.invertLightsCheck.AutoSize = true;
            this.invertLightsCheck.Location = new System.Drawing.Point(6, 252);
            this.invertLightsCheck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.invertLightsCheck.Name = "invertLightsCheck";
            this.invertLightsCheck.Size = new System.Drawing.Size(204, 29);
            this.invertLightsCheck.TabIndex = 6;
            this.invertLightsCheck.Text = "Invert Button Lights";
            this.invertLightsCheck.UseVisualStyleBackColor = true;
            // 
            // splitQE1Check
            // 
            this.splitQE1Check.AutoSize = true;
            this.splitQE1Check.Location = new System.Drawing.Point(6, 293);
            this.splitQE1Check.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.splitQE1Check.Name = "splitQE1Check";
            this.splitQE1Check.Size = new System.Drawing.Size(187, 29);
            this.splitQE1Check.TabIndex = 7;
            this.splitQE1Check.Text = "Enable Split QE1";
            this.splitQE1Check.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(411, 864);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 25);
            this.label11.TabIndex = 74;
            this.label11.Text = "ms";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 864);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 25);
            this.label12.TabIndex = 72;
            this.label12.Text = "Axis Debounce";
            // 
            // axisDebounceNum
            // 
            this.axisDebounceNum.Location = new System.Drawing.Point(174, 860);
            this.axisDebounceNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.axisDebounceNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.axisDebounceNum.Name = "axisDebounceNum";
            this.axisDebounceNum.Size = new System.Drawing.Size(226, 29);
            this.axisDebounceNum.TabIndex = 73;
            // 
            // controllerOutputCombo
            // 
            this.controllerOutputCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controllerOutputCombo.FormattingEnabled = true;
            this.controllerOutputCombo.Items.AddRange(new object[] {
            "Joystick",
            "Keyboard",
            "Joystick + Keyboard"});
            this.controllerOutputCombo.Location = new System.Drawing.Point(174, 908);
            this.controllerOutputCombo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.controllerOutputCombo.Name = "controllerOutputCombo";
            this.controllerOutputCombo.Size = new System.Drawing.Size(277, 32);
            this.controllerOutputCombo.TabIndex = 76;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 914);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(160, 25);
            this.label13.TabIndex = 75;
            this.label13.Text = "Controller Output";
            // 
            // keyMapButton
            // 
            this.keyMapButton.Location = new System.Drawing.Point(172, 958);
            this.keyMapButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.keyMapButton.Name = "keyMapButton";
            this.keyMapButton.Size = new System.Drawing.Size(282, 42);
            this.keyMapButton.TabIndex = 77;
            this.keyMapButton.Text = "Keyboard Mapping";
            this.keyMapButton.UseVisualStyleBackColor = true;
            this.keyMapButton.Click += new System.EventHandler(this.keyMapButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 967);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 25);
            this.label14.TabIndex = 78;
            this.label14.Text = "Key Mapping";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 1021);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(166, 25);
            this.label15.TabIndex = 80;
            this.label15.Text = "Button LED Mode";
            // 
            // buttonLedModeButton
            // 
            this.buttonLedModeButton.Location = new System.Drawing.Point(172, 1012);
            this.buttonLedModeButton.Margin = new System.Windows.Forms.Padding(6);
            this.buttonLedModeButton.Name = "buttonLedModeButton";
            this.buttonLedModeButton.Size = new System.Drawing.Size(282, 42);
            this.buttonLedModeButton.TabIndex = 79;
            this.buttonLedModeButton.Text = "Button LED Mode";
            this.buttonLedModeButton.UseVisualStyleBackColor = true;
            this.buttonLedModeButton.Click += new System.EventHandler(this.buttonLedModeButton_Click);
            // 
            // RoxyConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label15);
            this.Controls.Add(this.buttonLedModeButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.keyMapButton);
            this.Controls.Add(this.controllerOutputCombo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.axisDebounceNum);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rgb2ColorButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rgb1ColorButton);
            this.Controls.Add(this.rgbModeCombo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.debounceNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ascEmuCombo);
            this.Controls.Add(this.rgbPercentLabel);
            this.Controls.Add(this.rgbBrightnessNum);
            this.Controls.Add(this.rgbBrightnessLabel);
            this.Controls.Add(this.rgbCombo);
            this.Controls.Add(this.rgbModeLabel);
            this.Controls.Add(this.ps2Combo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.qe2Combo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.qe1Combo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "RoxyConfigPanel";
            this.Size = new System.Drawing.Size(458, 1072);
            ((System.ComponentModel.ISupportInitialize)(this.debounceNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbBrightnessNum)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axisDebounceNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button rgb2ColorButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button rgb1ColorButton;
        private System.Windows.Forms.ComboBox rgbModeCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown debounceNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox labelText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox serialNumCheck;
        private System.Windows.Forms.CheckBox invertQE1Check;
        private System.Windows.Forms.CheckBox invertQE2Check;
        private System.Windows.Forms.CheckBox led1Check;
        private System.Windows.Forms.CheckBox analogInputCheck;
        private System.Windows.Forms.CheckBox analogButtonsCheck;
        private System.Windows.Forms.ComboBox ascEmuCombo;
        private System.Windows.Forms.Label rgbPercentLabel;
        private System.Windows.Forms.NumericUpDown rgbBrightnessNum;
        private System.Windows.Forms.Label rgbBrightnessLabel;
        private System.Windows.Forms.ComboBox rgbCombo;
        private System.Windows.Forms.Label rgbModeLabel;
        private System.Windows.Forms.ComboBox ps2Combo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox qe2Combo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox qe1Combo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown axisDebounceNum;
        private System.Windows.Forms.ComboBox controllerOutputCombo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button keyMapButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox invertLightsCheck;
        private System.Windows.Forms.CheckBox splitQE1Check;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonLedModeButton;
    }
}
