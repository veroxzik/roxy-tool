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
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.axisDebounceNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.debounceNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbBrightnessNum)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axisDebounceNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 393);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 67;
            this.label10.Text = "ASC Emulation";
            // 
            // rgb2ColorButton
            // 
            this.rgb2ColorButton.Location = new System.Drawing.Point(178, 361);
            this.rgb2ColorButton.Name = "rgb2ColorButton";
            this.rgb2ColorButton.Size = new System.Drawing.Size(69, 23);
            this.rgb2ColorButton.TabIndex = 66;
            this.rgb2ColorButton.Text = "RGB 2";
            this.rgb2ColorButton.UseVisualStyleBackColor = true;
            this.rgb2ColorButton.Click += new System.EventHandler(this.rgb2ColorButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 366);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 65;
            this.label9.Text = "RGB Color";
            // 
            // rgb1ColorButton
            // 
            this.rgb1ColorButton.Location = new System.Drawing.Point(93, 361);
            this.rgb1ColorButton.Name = "rgb1ColorButton";
            this.rgb1ColorButton.Size = new System.Drawing.Size(69, 23);
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
            this.rgbModeCombo.Location = new System.Drawing.Point(94, 334);
            this.rgbModeCombo.Name = "rgbModeCombo";
            this.rgbModeCombo.Size = new System.Drawing.Size(153, 21);
            this.rgbModeCombo.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "RGB Mode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "ms";
            // 
            // debounceNum
            // 
            this.debounceNum.Location = new System.Drawing.Point(94, 308);
            this.debounceNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.debounceNum.Name = "debounceNum";
            this.debounceNum.Size = new System.Drawing.Size(123, 20);
            this.debounceNum.TabIndex = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Debounce Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Flags:";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(95, 3);
            this.labelText.MaxLength = 11;
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(152, 20);
            this.labelText.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Board Label:";
            // 
            // serialNumCheck
            // 
            this.serialNumCheck.AutoSize = true;
            this.serialNumCheck.Location = new System.Drawing.Point(3, 3);
            this.serialNumCheck.Name = "serialNumCheck";
            this.serialNumCheck.Size = new System.Drawing.Size(117, 17);
            this.serialNumCheck.TabIndex = 0;
            this.serialNumCheck.Text = "Hide Serial Number";
            this.serialNumCheck.UseVisualStyleBackColor = true;
            // 
            // invertQE1Check
            // 
            this.invertQE1Check.AutoSize = true;
            this.invertQE1Check.Location = new System.Drawing.Point(3, 26);
            this.invertQE1Check.Name = "invertQE1Check";
            this.invertQE1Check.Size = new System.Drawing.Size(77, 17);
            this.invertQE1Check.TabIndex = 1;
            this.invertQE1Check.Text = "Invert QE1";
            this.invertQE1Check.UseVisualStyleBackColor = true;
            // 
            // invertQE2Check
            // 
            this.invertQE2Check.AutoSize = true;
            this.invertQE2Check.Location = new System.Drawing.Point(3, 49);
            this.invertQE2Check.Name = "invertQE2Check";
            this.invertQE2Check.Size = new System.Drawing.Size(77, 17);
            this.invertQE2Check.TabIndex = 2;
            this.invertQE2Check.Text = "Invert QE2";
            this.invertQE2Check.UseVisualStyleBackColor = true;
            // 
            // led1Check
            // 
            this.led1Check.AutoSize = true;
            this.led1Check.Location = new System.Drawing.Point(3, 72);
            this.led1Check.Name = "led1Check";
            this.led1Check.Size = new System.Drawing.Size(103, 17);
            this.led1Check.TabIndex = 3;
            this.led1Check.Text = "LED1 always on";
            this.led1Check.UseVisualStyleBackColor = true;
            // 
            // analogInputCheck
            // 
            this.analogInputCheck.AutoSize = true;
            this.analogInputCheck.Location = new System.Drawing.Point(3, 95);
            this.analogInputCheck.Name = "analogInputCheck";
            this.analogInputCheck.Size = new System.Drawing.Size(130, 17);
            this.analogInputCheck.TabIndex = 5;
            this.analogInputCheck.Text = "Analog Input (Not QE)";
            this.analogInputCheck.UseVisualStyleBackColor = true;
            // 
            // analogButtonsCheck
            // 
            this.analogButtonsCheck.AutoSize = true;
            this.analogButtonsCheck.Location = new System.Drawing.Point(3, 118);
            this.analogButtonsCheck.Name = "analogButtonsCheck";
            this.analogButtonsCheck.Size = new System.Drawing.Size(134, 17);
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
            this.ascEmuCombo.Location = new System.Drawing.Point(94, 390);
            this.ascEmuCombo.Name = "ascEmuCombo";
            this.ascEmuCombo.Size = new System.Drawing.Size(153, 21);
            this.ascEmuCombo.TabIndex = 68;
            // 
            // rgbPercentLabel
            // 
            this.rgbPercentLabel.AutoSize = true;
            this.rgbPercentLabel.Location = new System.Drawing.Point(190, 284);
            this.rgbPercentLabel.Name = "rgbPercentLabel";
            this.rgbPercentLabel.Size = new System.Drawing.Size(54, 13);
            this.rgbPercentLabel.TabIndex = 58;
            this.rgbPercentLabel.Text = "(255 Max)";
            // 
            // rgbBrightnessNum
            // 
            this.rgbBrightnessNum.Location = new System.Drawing.Point(94, 282);
            this.rgbBrightnessNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rgbBrightnessNum.Name = "rgbBrightnessNum";
            this.rgbBrightnessNum.Size = new System.Drawing.Size(90, 20);
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
            this.rgbBrightnessLabel.Location = new System.Drawing.Point(3, 284);
            this.rgbBrightnessLabel.Name = "rgbBrightnessLabel";
            this.rgbBrightnessLabel.Size = new System.Drawing.Size(82, 13);
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
            this.rgbCombo.Location = new System.Drawing.Point(94, 255);
            this.rgbCombo.Name = "rgbCombo";
            this.rgbCombo.Size = new System.Drawing.Size(153, 21);
            this.rgbCombo.TabIndex = 55;
            // 
            // rgbModeLabel
            // 
            this.rgbModeLabel.AutoSize = true;
            this.rgbModeLabel.Location = new System.Drawing.Point(2, 258);
            this.rgbModeLabel.Name = "rgbModeLabel";
            this.rgbModeLabel.Size = new System.Drawing.Size(75, 13);
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
            this.ps2Combo.Location = new System.Drawing.Point(94, 228);
            this.ps2Combo.Name = "ps2Combo";
            this.ps2Combo.Size = new System.Drawing.Size(153, 21);
            this.ps2Combo.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "PS2 Mode";
            // 
            // qe2Combo
            // 
            this.qe2Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qe2Combo.FormattingEnabled = true;
            this.qe2Combo.Items.AddRange(new object[] {
            "1:1",
            "1:2",
            "1:3",
            "1:4",
            "1:6",
            "1:8",
            "1:11",
            "1:16",
            "2:1",
            "3:1",
            "4:1",
            "6:1",
            "8:1",
            "11:1",
            "16:1",
            "600 PPR",
            "400 PPR",
            "360 PPR"});
            this.qe2Combo.Location = new System.Drawing.Point(94, 201);
            this.qe2Combo.Name = "qe2Combo";
            this.qe2Combo.Size = new System.Drawing.Size(153, 21);
            this.qe2Combo.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "QE2 Sensitivity";
            // 
            // qe1Combo
            // 
            this.qe1Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qe1Combo.FormattingEnabled = true;
            this.qe1Combo.Items.AddRange(new object[] {
            "1:1",
            "1:2",
            "1:3",
            "1:4",
            "1:6",
            "1:8",
            "1:11",
            "1:16",
            "2:1",
            "3:1",
            "4:1",
            "6:1",
            "8:1",
            "11:1",
            "16:1",
            "600 PPR",
            "400 PPR",
            "360 PPR"});
            this.qe1Combo.Location = new System.Drawing.Point(94, 174);
            this.qe1Combo.Name = "qe1Combo";
            this.qe1Combo.Size = new System.Drawing.Size(153, 21);
            this.qe1Combo.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(95, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(152, 139);
            this.flowLayoutPanel1.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(223, 419);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "ms";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 419);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Axis Debounce";
            // 
            // axisDebounceNum
            // 
            this.axisDebounceNum.Location = new System.Drawing.Point(94, 417);
            this.axisDebounceNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.axisDebounceNum.Name = "axisDebounceNum";
            this.axisDebounceNum.Size = new System.Drawing.Size(123, 20);
            this.axisDebounceNum.TabIndex = 73;
            // 
            // RoxyConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "RoxyConfigPanel";
            this.Size = new System.Drawing.Size(250, 445);
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
    }
}
