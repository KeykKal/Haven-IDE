namespace IDE_test
{
    partial class Settings
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chk_Log = new System.Windows.Forms.CheckBox();
            this.chk_Hook = new System.Windows.Forms.CheckBox();
            this.chk_disassemble = new System.Windows.Forms.CheckBox();
            this.chk_overwrite = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.configPage = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.relodedSelectButton = new System.Windows.Forms.Button();
            this.relodedFilePath = new System.Windows.Forms.TextBox();
            this.relodedPathLabel = new System.Windows.Forms.Label();
            this.gameSelectButton = new System.Windows.Forms.Button();
            this.gameFilePath = new System.Windows.Forms.TextBox();
            this.GameName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.designSettings = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.multiCommentStyColorButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.stringStyColorButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.MSGStyColorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.singelCommentStyColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nummberStyColorButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.baseStylColorButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.configPage.SuspendLayout();
            this.designSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // chk_Log
            // 
            this.chk_Log.AutoSize = true;
            this.chk_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chk_Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Log.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_Log.Location = new System.Drawing.Point(317, 181);
            this.chk_Log.Name = "chk_Log";
            this.chk_Log.Size = new System.Drawing.Size(75, 19);
            this.chk_Log.TabIndex = 4;
            this.chk_Log.Text = "Show Log";
            this.chk_Log.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Log.UseVisualStyleBackColor = false;
            // 
            // chk_Hook
            // 
            this.chk_Hook.AutoSize = true;
            this.chk_Hook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Hook.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_Hook.Location = new System.Drawing.Point(136, 181);
            this.chk_Hook.Name = "chk_Hook";
            this.chk_Hook.Size = new System.Drawing.Size(107, 19);
            this.chk_Hook.TabIndex = 5;
            this.chk_Hook.Text = "Enable Hooking";
            this.chk_Hook.UseVisualStyleBackColor = true;
            // 
            // chk_disassemble
            // 
            this.chk_disassemble.AutoSize = true;
            this.chk_disassemble.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_disassemble.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_disassemble.Location = new System.Drawing.Point(317, 219);
            this.chk_disassemble.Name = "chk_disassemble";
            this.chk_disassemble.Size = new System.Drawing.Size(88, 19);
            this.chk_disassemble.TabIndex = 6;
            this.chk_disassemble.Text = "Disassemble";
            this.chk_disassemble.UseVisualStyleBackColor = true;
            // 
            // chk_overwrite
            // 
            this.chk_overwrite.AutoSize = true;
            this.chk_overwrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_overwrite.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_overwrite.Location = new System.Drawing.Point(136, 219);
            this.chk_overwrite.Name = "chk_overwrite";
            this.chk_overwrite.Size = new System.Drawing.Size(74, 19);
            this.chk_overwrite.TabIndex = 7;
            this.chk_overwrite.Text = "Overwrite";
            this.chk_overwrite.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.configPage);
            this.tabControl1.Controls.Add(this.designSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(601, 294);
            this.tabControl1.TabIndex = 8;
            // 
            // configPage
            // 
            this.configPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.configPage.Controls.Add(this.label9);
            this.configPage.Controls.Add(this.relodedSelectButton);
            this.configPage.Controls.Add(this.relodedFilePath);
            this.configPage.Controls.Add(this.relodedPathLabel);
            this.configPage.Controls.Add(this.gameSelectButton);
            this.configPage.Controls.Add(this.gameFilePath);
            this.configPage.Controls.Add(this.GameName);
            this.configPage.Controls.Add(this.label8);
            this.configPage.Controls.Add(this.chk_overwrite);
            this.configPage.Controls.Add(this.chk_disassemble);
            this.configPage.Controls.Add(this.chk_Hook);
            this.configPage.Controls.Add(this.chk_Log);
            this.configPage.Location = new System.Drawing.Point(4, 24);
            this.configPage.Name = "configPage";
            this.configPage.Padding = new System.Windows.Forms.Padding(3);
            this.configPage.Size = new System.Drawing.Size(593, 266);
            this.configPage.TabIndex = 0;
            this.configPage.Text = "Config";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(3, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(583, 32);
            this.label9.TabIndex = 15;
            this.label9.Text = "Please Ignore this empty space I am bad at designing";
            // 
            // relodedSelectButton
            // 
            this.relodedSelectButton.Location = new System.Drawing.Point(507, 112);
            this.relodedSelectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.relodedSelectButton.Name = "relodedSelectButton";
            this.relodedSelectButton.Size = new System.Drawing.Size(36, 23);
            this.relodedSelectButton.TabIndex = 14;
            this.relodedSelectButton.Text = "...";
            this.relodedSelectButton.UseVisualStyleBackColor = true;
            this.relodedSelectButton.Visible = false;
            this.relodedSelectButton.Click += new System.EventHandler(this.relodedSelectButton_Click);
            // 
            // relodedFilePath
            // 
            this.relodedFilePath.Location = new System.Drawing.Point(92, 112);
            this.relodedFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.relodedFilePath.Name = "relodedFilePath";
            this.relodedFilePath.ReadOnly = true;
            this.relodedFilePath.Size = new System.Drawing.Size(419, 23);
            this.relodedFilePath.TabIndex = 13;
            this.relodedFilePath.Visible = false;
            // 
            // relodedPathLabel
            // 
            this.relodedPathLabel.AutoSize = true;
            this.relodedPathLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.relodedPathLabel.Location = new System.Drawing.Point(5, 116);
            this.relodedPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.relodedPathLabel.Name = "relodedPathLabel";
            this.relodedPathLabel.Size = new System.Drawing.Size(74, 15);
            this.relodedPathLabel.TabIndex = 12;
            this.relodedPathLabel.Text = "Reloded exe:";
            this.relodedPathLabel.Visible = false;
            // 
            // gameSelectButton
            // 
            this.gameSelectButton.Location = new System.Drawing.Point(507, 64);
            this.gameSelectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gameSelectButton.Name = "gameSelectButton";
            this.gameSelectButton.Size = new System.Drawing.Size(36, 23);
            this.gameSelectButton.TabIndex = 11;
            this.gameSelectButton.Text = "...";
            this.gameSelectButton.UseVisualStyleBackColor = true;
            this.gameSelectButton.Visible = false;
            this.gameSelectButton.Click += new System.EventHandler(this.gameSelectButton_Click);
            // 
            // gameFilePath
            // 
            this.gameFilePath.Location = new System.Drawing.Point(92, 64);
            this.gameFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gameFilePath.Name = "gameFilePath";
            this.gameFilePath.ReadOnly = true;
            this.gameFilePath.Size = new System.Drawing.Size(419, 23);
            this.gameFilePath.TabIndex = 10;
            this.gameFilePath.Visible = false;
            // 
            // GameName
            // 
            this.GameName.AutoSize = true;
            this.GameName.ForeColor = System.Drawing.SystemColors.Control;
            this.GameName.Location = new System.Drawing.Point(27, 68);
            this.GameName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GameName.Name = "GameName";
            this.GameName.Size = new System.Drawing.Size(52, 15);
            this.GameName.TabIndex = 9;
            this.GameName.Text = "P4G exe:";
            this.GameName.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(597, 47);
            this.label8.TabIndex = 8;
            this.label8.Text = "W.I.P Config\'s";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // designSettings
            // 
            this.designSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.designSettings.Controls.Add(this.label7);
            this.designSettings.Controls.Add(this.label6);
            this.designSettings.Controls.Add(this.multiCommentStyColorButton);
            this.designSettings.Controls.Add(this.label5);
            this.designSettings.Controls.Add(this.stringStyColorButton);
            this.designSettings.Controls.Add(this.label4);
            this.designSettings.Controls.Add(this.MSGStyColorButton);
            this.designSettings.Controls.Add(this.label3);
            this.designSettings.Controls.Add(this.singelCommentStyColorButton);
            this.designSettings.Controls.Add(this.label2);
            this.designSettings.Controls.Add(this.nummberStyColorButton);
            this.designSettings.Controls.Add(this.label1);
            this.designSettings.Controls.Add(this.baseStylColorButton);
            this.designSettings.Location = new System.Drawing.Point(4, 24);
            this.designSettings.Name = "designSettings";
            this.designSettings.Padding = new System.Windows.Forms.Padding(3);
            this.designSettings.Size = new System.Drawing.Size(593, 266);
            this.designSettings.TabIndex = 1;
            this.designSettings.Text = "Design";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(593, 52);
            this.label7.TabIndex = 14;
            this.label7.Text = "W.I.P Custom Syntax";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(201, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Multilined comment color";
            // 
            // multiCommentStyColorButton
            // 
            this.multiCommentStyColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.multiCommentStyColorButton.FlatAppearance.BorderSize = 0;
            this.multiCommentStyColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiCommentStyColorButton.Location = new System.Drawing.Point(353, 108);
            this.multiCommentStyColorButton.Name = "multiCommentStyColorButton";
            this.multiCommentStyColorButton.Size = new System.Drawing.Size(25, 25);
            this.multiCommentStyColorButton.TabIndex = 12;
            this.multiCommentStyColorButton.UseVisualStyleBackColor = false;
            this.multiCommentStyColorButton.Click += new System.EventHandler(this.multiCommentStyColorButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(189, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "String color";
            // 
            // stringStyColorButton
            // 
            this.stringStyColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.stringStyColorButton.FlatAppearance.BorderSize = 0;
            this.stringStyColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stringStyColorButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stringStyColorButton.Location = new System.Drawing.Point(263, 60);
            this.stringStyColorButton.Name = "stringStyColorButton";
            this.stringStyColorButton.Size = new System.Drawing.Size(25, 25);
            this.stringStyColorButton.TabIndex = 10;
            this.stringStyColorButton.UseVisualStyleBackColor = false;
            this.stringStyColorButton.Click += new System.EventHandler(this.stringStyColorButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(393, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Function/MSG color";
            // 
            // MSGStyColorButton
            // 
            this.MSGStyColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.MSGStyColorButton.FlatAppearance.BorderSize = 0;
            this.MSGStyColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MSGStyColorButton.Location = new System.Drawing.Point(513, 108);
            this.MSGStyColorButton.Name = "MSGStyColorButton";
            this.MSGStyColorButton.Size = new System.Drawing.Size(25, 25);
            this.MSGStyColorButton.TabIndex = 8;
            this.MSGStyColorButton.UseVisualStyleBackColor = false;
            this.MSGStyColorButton.Click += new System.EventHandler(this.MSGStyColorButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(331, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "singel line comment color";
            // 
            // singelCommentStyColorButton
            // 
            this.singelCommentStyColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.singelCommentStyColorButton.FlatAppearance.BorderSize = 0;
            this.singelCommentStyColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.singelCommentStyColorButton.Location = new System.Drawing.Point(482, 60);
            this.singelCommentStyColorButton.Name = "singelCommentStyColorButton";
            this.singelCommentStyColorButton.Size = new System.Drawing.Size(25, 25);
            this.singelCommentStyColorButton.TabIndex = 6;
            this.singelCommentStyColorButton.UseVisualStyleBackColor = false;
            this.singelCommentStyColorButton.Click += new System.EventHandler(this.singelCommentStyColorButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(45, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number color";
            // 
            // nummberStyColorButton
            // 
            this.nummberStyColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.nummberStyColorButton.FlatAppearance.BorderSize = 0;
            this.nummberStyColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nummberStyColorButton.Location = new System.Drawing.Point(132, 108);
            this.nummberStyColorButton.Name = "nummberStyColorButton";
            this.nummberStyColorButton.Size = new System.Drawing.Size(25, 25);
            this.nummberStyColorButton.TabIndex = 4;
            this.nummberStyColorButton.UseVisualStyleBackColor = false;
            this.nummberStyColorButton.Click += new System.EventHandler(this.nummberStyColorButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(45, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Base color";
            // 
            // baseStylColorButton
            // 
            this.baseStylColorButton.BackColor = System.Drawing.SystemColors.Control;
            this.baseStylColorButton.FlatAppearance.BorderSize = 0;
            this.baseStylColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.baseStylColorButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.baseStylColorButton.Location = new System.Drawing.Point(112, 60);
            this.baseStylColorButton.Name = "baseStylColorButton";
            this.baseStylColorButton.Size = new System.Drawing.Size(25, 25);
            this.baseStylColorButton.TabIndex = 2;
            this.baseStylColorButton.UseVisualStyleBackColor = true;
            this.baseStylColorButton.Click += new System.EventHandler(this.baseStylColorButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(601, 294);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.configPage.ResumeLayout(false);
            this.configPage.PerformLayout();
            this.designSettings.ResumeLayout(false);
            this.designSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.CheckBox chk_Hook;
        public System.Windows.Forms.CheckBox chk_disassemble;
        public System.Windows.Forms.CheckBox chk_overwrite;
        public System.Windows.Forms.CheckBox chk_Log;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage configPage;
        private System.Windows.Forms.TabPage designSettings;
        private System.Windows.Forms.Button baseStylColorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button singelCommentStyColorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button nummberStyColorButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button multiCommentStyColorButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button stringStyColorButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button MSGStyColorButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button gameSelectButton;
        private System.Windows.Forms.TextBox gameFilePath;
        private System.Windows.Forms.Label GameName;
        private System.Windows.Forms.Button relodedSelectButton;
        private System.Windows.Forms.TextBox relodedFilePath;
        private System.Windows.Forms.Label relodedPathLabel;
        private System.Windows.Forms.Label label9;
    }
}