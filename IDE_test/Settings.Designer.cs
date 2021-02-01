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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComplierOutput = new System.Windows.Forms.Label();
            this.compilerFolderPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chk_Log = new System.Windows.Forms.CheckBox();
            this.chk_Hook = new System.Windows.Forms.CheckBox();
            this.chk_disassemble = new System.Windows.Forms.CheckBox();
            this.chk_overwrite = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(646, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // ComplierOutput
            // 
            this.ComplierOutput.AutoSize = true;
            this.ComplierOutput.ForeColor = System.Drawing.SystemColors.Control;
            this.ComplierOutput.Location = new System.Drawing.Point(42, 61);
            this.ComplierOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ComplierOutput.Name = "ComplierOutput";
            this.ComplierOutput.Size = new System.Drawing.Size(59, 15);
            this.ComplierOutput.TabIndex = 1;
            this.ComplierOutput.Text = "Compiler:";
            // 
            // compilerFolderPath
            // 
            this.compilerFolderPath.Location = new System.Drawing.Point(120, 58);
            this.compilerFolderPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.compilerFolderPath.Name = "compilerFolderPath";
            this.compilerFolderPath.Size = new System.Drawing.Size(396, 23);
            this.compilerFolderPath.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_Log
            // 
            this.chk_Log.AutoSize = true;
            this.chk_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.chk_Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Log.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_Log.Location = new System.Drawing.Point(301, 102);
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
            this.chk_Hook.Location = new System.Drawing.Point(120, 102);
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
            this.chk_disassemble.Location = new System.Drawing.Point(301, 138);
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
            this.chk_overwrite.Location = new System.Drawing.Point(120, 138);
            this.chk_overwrite.Name = "chk_overwrite";
            this.chk_overwrite.Size = new System.Drawing.Size(74, 19);
            this.chk_overwrite.TabIndex = 7;
            this.chk_overwrite.Text = "Overwrite";
            this.chk_overwrite.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(646, 194);
            this.Controls.Add(this.chk_overwrite);
            this.Controls.Add(this.chk_disassemble);
            this.Controls.Add(this.chk_Hook);
            this.Controls.Add(this.chk_Log);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.compilerFolderPath);
            this.Controls.Add(this.ComplierOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.Label ComplierOutput;
        private System.Windows.Forms.TextBox compilerFolderPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.CheckBox chk_Hook;
        public System.Windows.Forms.CheckBox chk_disassemble;
        public System.Windows.Forms.CheckBox chk_overwrite;
        public System.Windows.Forms.CheckBox chk_Log;
    }
}