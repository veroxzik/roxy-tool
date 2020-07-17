namespace Roxy.Tool.WinForms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.writeConfigButton = new System.Windows.Forms.Button();
            this.readConfigButton = new System.Windows.Forms.Button();
            this.configGroupBox = new System.Windows.Forms.GroupBox();
            this.configPanel = new System.Windows.Forms.Panel();
            this.roxyConfigPanel = new Roxy.Tool.WinForms.RoxyConfigPanel();
            this.arcinConfigPanel = new Roxy.Tool.WinForms.ArcinControlPanel();
            this.arcinRoxyControlPanel = new Roxy.Tool.WinForms.ArcinRoxyControlPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.loadElfButton = new System.Windows.Forms.Button();
            this.filenameText = new System.Windows.Forms.TextBox();
            this.flashButton = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.boardSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roxySelectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcinRoxySelectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcinSelectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configGroupBox.SuspendLayout();
            this.configPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writeConfigButton
            // 
            this.writeConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeConfigButton.Enabled = false;
            this.writeConfigButton.Location = new System.Drawing.Point(152, 6);
            this.writeConfigButton.Name = "writeConfigButton";
            this.writeConfigButton.Size = new System.Drawing.Size(125, 23);
            this.writeConfigButton.TabIndex = 1;
            this.writeConfigButton.Text = "Write Config";
            this.writeConfigButton.UseVisualStyleBackColor = true;
            this.writeConfigButton.Click += new System.EventHandler(this.writeConfigButton_Click);
            // 
            // readConfigButton
            // 
            this.readConfigButton.Enabled = false;
            this.readConfigButton.Location = new System.Drawing.Point(8, 6);
            this.readConfigButton.Name = "readConfigButton";
            this.readConfigButton.Size = new System.Drawing.Size(125, 23);
            this.readConfigButton.TabIndex = 0;
            this.readConfigButton.Text = "Read Config";
            this.readConfigButton.UseVisualStyleBackColor = true;
            this.readConfigButton.Click += new System.EventHandler(this.readConfigButton_Click);
            // 
            // configGroupBox
            // 
            this.configGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configGroupBox.AutoSize = true;
            this.configGroupBox.Controls.Add(this.configPanel);
            this.configGroupBox.Enabled = false;
            this.configGroupBox.Location = new System.Drawing.Point(8, 35);
            this.configGroupBox.Name = "configGroupBox";
            this.configGroupBox.Size = new System.Drawing.Size(269, 511);
            this.configGroupBox.TabIndex = 2;
            this.configGroupBox.TabStop = false;
            this.configGroupBox.Text = "Config Options";
            // 
            // configPanel
            // 
            this.configPanel.AutoScroll = true;
            this.configPanel.Controls.Add(this.roxyConfigPanel);
            this.configPanel.Controls.Add(this.arcinConfigPanel);
            this.configPanel.Controls.Add(this.arcinRoxyControlPanel);
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configPanel.Location = new System.Drawing.Point(3, 16);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(263, 492);
            this.configPanel.TabIndex = 1;
            // 
            // roxyConfigPanel
            // 
            this.roxyConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roxyConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.roxyConfigPanel.Name = "roxyConfigPanel";
            this.roxyConfigPanel.Size = new System.Drawing.Size(263, 492);
            this.roxyConfigPanel.TabIndex = 0;
            // 
            // arcinConfigPanel
            // 
            this.arcinConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arcinConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.arcinConfigPanel.Name = "arcinConfigPanel";
            this.arcinConfigPanel.Size = new System.Drawing.Size(263, 492);
            this.arcinConfigPanel.TabIndex = 1;
            // 
            // arcinRoxyControlPanel
            // 
            this.arcinRoxyControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arcinRoxyControlPanel.Location = new System.Drawing.Point(0, 0);
            this.arcinRoxyControlPanel.Name = "arcinRoxyControlPanel";
            this.arcinRoxyControlPanel.Size = new System.Drawing.Size(263, 492);
            this.arcinRoxyControlPanel.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.configGroupBox);
            this.tabPage2.Controls.Add(this.writeConfigButton);
            this.tabPage2.Controls.Add(this.readConfigButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 578);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.loadElfButton);
            this.tabPage3.Controls.Add(this.filenameText);
            this.tabPage3.Controls.Add(this.flashButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(283, 552);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Flash";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Selected file:";
            // 
            // loadElfButton
            // 
            this.loadElfButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadElfButton.Location = new System.Drawing.Point(8, 3);
            this.loadElfButton.Name = "loadElfButton";
            this.loadElfButton.Size = new System.Drawing.Size(269, 23);
            this.loadElfButton.TabIndex = 4;
            this.loadElfButton.Text = "Load ELF";
            this.loadElfButton.UseVisualStyleBackColor = true;
            this.loadElfButton.Click += new System.EventHandler(this.loadElfButton_Click);
            // 
            // filenameText
            // 
            this.filenameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameText.Location = new System.Drawing.Point(8, 45);
            this.filenameText.Name = "filenameText";
            this.filenameText.ReadOnly = true;
            this.filenameText.Size = new System.Drawing.Size(269, 20);
            this.filenameText.TabIndex = 7;
            // 
            // flashButton
            // 
            this.flashButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flashButton.Enabled = false;
            this.flashButton.Location = new System.Drawing.Point(8, 71);
            this.flashButton.Name = "flashButton";
            this.flashButton.Size = new System.Drawing.Size(269, 39);
            this.flashButton.TabIndex = 5;
            this.flashButton.Text = "Flash To Chip";
            this.flashButton.UseVisualStyleBackColor = true;
            this.flashButton.Click += new System.EventHandler(this.flashButton_Click);
            // 
            // statusText
            // 
            this.statusText.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusText.Location = new System.Drawing.Point(293, 24);
            this.statusText.Multiline = true;
            this.statusText.Name = "statusText";
            this.statusText.ReadOnly = true;
            this.statusText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusText.Size = new System.Drawing.Size(207, 578);
            this.statusText.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boardSelectToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(500, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // boardSelectToolStripMenuItem
            // 
            this.boardSelectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roxySelectMenuItem,
            this.arcinRoxySelectMenuItem,
            this.arcinSelectMenuItem});
            this.boardSelectToolStripMenuItem.Name = "boardSelectToolStripMenuItem";
            this.boardSelectToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.boardSelectToolStripMenuItem.Text = "Board Select";
            // 
            // roxySelectMenuItem
            // 
            this.roxySelectMenuItem.Checked = true;
            this.roxySelectMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.roxySelectMenuItem.Name = "roxySelectMenuItem";
            this.roxySelectMenuItem.Size = new System.Drawing.Size(189, 22);
            this.roxySelectMenuItem.Text = "Roxy";
            this.roxySelectMenuItem.Click += new System.EventHandler(this.roxySelectMenuItem_Click);
            // 
            // arcinRoxySelectMenuItem
            // 
            this.arcinRoxySelectMenuItem.Name = "arcinRoxySelectMenuItem";
            this.arcinRoxySelectMenuItem.Size = new System.Drawing.Size(189, 22);
            this.arcinRoxySelectMenuItem.Text = "arcin (Roxy Firmware)";
            this.arcinRoxySelectMenuItem.Click += new System.EventHandler(this.arcinRoxySelectMenuItem_Click);
            // 
            // arcinSelectMenuItem
            // 
            this.arcinSelectMenuItem.Name = "arcinSelectMenuItem";
            this.arcinSelectMenuItem.Size = new System.Drawing.Size(189, 22);
            this.arcinSelectMenuItem.Text = "arcin";
            this.arcinSelectMenuItem.Click += new System.EventHandler(this.arcinSelectMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licensesToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // licensesToolStripMenuItem
            // 
            this.licensesToolStripMenuItem.Name = "licensesToolStripMenuItem";
            this.licensesToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.licensesToolStripMenuItem.Text = "Licenses";
            this.licensesToolStripMenuItem.Click += new System.EventHandler(this.licensesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 602);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.configGroupBox.ResumeLayout(false);
            this.configPanel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button writeConfigButton;
        private System.Windows.Forms.Button readConfigButton;
        private System.Windows.Forms.GroupBox configGroupBox;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem boardSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roxySelectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcinSelectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licensesToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button loadElfButton;
        private System.Windows.Forms.TextBox statusText;
        private System.Windows.Forms.TextBox filenameText;
        private System.Windows.Forms.Button flashButton;
        private RoxyConfigPanel roxyConfigPanel;
        private ArcinControlPanel arcinConfigPanel;
        private ArcinRoxyControlPanel arcinRoxyControlPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem arcinRoxySelectMenuItem;
    }
}