namespace Roxy.Tool.WinForms
{
    partial class ArcinControlPanel
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
            this.ws2812bCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ps2Combo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.qe2Combo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.qe1Combo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.invertQE2Check = new System.Windows.Forms.CheckBox();
            this.led1Check = new System.Windows.Forms.CheckBox();
            this.led2Check = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.serialNumCheck = new System.Windows.Forms.CheckBox();
            this.invertQE1Check = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ws2812bCombo
            // 
            this.ws2812bCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ws2812bCombo.FormattingEnabled = true;
            this.ws2812bCombo.Items.AddRange(new object[] {
            "Disabled",
            "B9"});
            this.ws2812bCombo.Location = new System.Drawing.Point(96, 235);
            this.ws2812bCombo.Name = "ws2812bCombo";
            this.ws2812bCombo.Size = new System.Drawing.Size(129, 21);
            this.ws2812bCombo.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "WS2812B Mode";
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
            this.ps2Combo.Location = new System.Drawing.Point(96, 208);
            this.ps2Combo.Name = "ps2Combo";
            this.ps2Combo.Size = new System.Drawing.Size(129, 21);
            this.ps2Combo.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 32;
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
            "16:1"});
            this.qe2Combo.Location = new System.Drawing.Point(96, 181);
            this.qe2Combo.Name = "qe2Combo";
            this.qe2Combo.Size = new System.Drawing.Size(129, 21);
            this.qe2Combo.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 30;
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
            "16:1"});
            this.qe1Combo.Location = new System.Drawing.Point(96, 154);
            this.qe1Combo.Name = "qe1Combo";
            this.qe1Combo.Size = new System.Drawing.Size(129, 21);
            this.qe1Combo.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "QE1 Sensitivity";
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
            // led2Check
            // 
            this.led2Check.AutoSize = true;
            this.led2Check.Location = new System.Drawing.Point(3, 95);
            this.led2Check.Name = "led2Check";
            this.led2Check.Size = new System.Drawing.Size(103, 17);
            this.led2Check.TabIndex = 4;
            this.led2Check.Text = "LED2 always on";
            this.led2Check.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.serialNumCheck);
            this.flowLayoutPanel1.Controls.Add(this.invertQE1Check);
            this.flowLayoutPanel1.Controls.Add(this.invertQE2Check);
            this.flowLayoutPanel1.Controls.Add(this.led1Check);
            this.flowLayoutPanel1.Controls.Add(this.led2Check);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(96, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(128, 119);
            this.flowLayoutPanel1.TabIndex = 27;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Flags:";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(96, 3);
            this.labelText.MaxLength = 11;
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(128, 20);
            this.labelText.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Board Label:";
            // 
            // ArcinControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ws2812bCombo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ps2Combo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.qe2Combo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.qe1Combo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.label1);
            this.Name = "ArcinControlPanel";
            this.Size = new System.Drawing.Size(228, 259);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ws2812bCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ps2Combo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox qe2Combo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox qe1Combo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox invertQE2Check;
        private System.Windows.Forms.CheckBox led1Check;
        private System.Windows.Forms.CheckBox led2Check;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox serialNumCheck;
        private System.Windows.Forms.CheckBox invertQE1Check;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox labelText;
        private System.Windows.Forms.Label label1;
    }
}
