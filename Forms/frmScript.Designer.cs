namespace MeteoInfo.Forms
{
    partial class frmScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScript));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMI_File = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_RunScript = new System.Windows.Forms.ToolStripButton();
            this.RTB_ScriptText = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(526, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TSMI_File
            // 
            this.TSMI_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.TSMI_OpenFile,
            this.TSMI_SaveFile,
            this.TSMI_SaveAs});
            this.TSMI_File.Name = "TSMI_File";
            this.TSMI_File.Size = new System.Drawing.Size(39, 21);
            this.TSMI_File.Text = "File";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // TSMI_OpenFile
            // 
            this.TSMI_OpenFile.Name = "TSMI_OpenFile";
            this.TSMI_OpenFile.Size = new System.Drawing.Size(131, 22);
            this.TSMI_OpenFile.Text = "Open File";
            this.TSMI_OpenFile.Click += new System.EventHandler(this.TSMI_OpenFile_Click);
            // 
            // TSMI_SaveFile
            // 
            this.TSMI_SaveFile.Name = "TSMI_SaveFile";
            this.TSMI_SaveFile.Size = new System.Drawing.Size(131, 22);
            this.TSMI_SaveFile.Text = "Save File";
            this.TSMI_SaveFile.Click += new System.EventHandler(this.TSMI_SaveFile_Click);
            // 
            // TSMI_SaveAs
            // 
            this.TSMI_SaveAs.Name = "TSMI_SaveAs";
            this.TSMI_SaveAs.Size = new System.Drawing.Size(131, 22);
            this.TSMI_SaveAs.Text = "Save As";
            this.TSMI_SaveAs.Click += new System.EventHandler(this.TSMI_SaveAs_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_RunScript});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(526, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TSB_RunScript
            // 
            this.TSB_RunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_RunScript.Image = ((System.Drawing.Image)(resources.GetObject("TSB_RunScript.Image")));
            this.TSB_RunScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_RunScript.Name = "TSB_RunScript";
            this.TSB_RunScript.Size = new System.Drawing.Size(23, 22);
            this.TSB_RunScript.Text = "Run Script";
            this.TSB_RunScript.Click += new System.EventHandler(this.TSB_RunScript_Click);
            // 
            // RTB_ScriptText
            // 
            this.RTB_ScriptText.AutoWordSelection = true;
            this.RTB_ScriptText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB_ScriptText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB_ScriptText.Location = new System.Drawing.Point(0, 50);
            this.RTB_ScriptText.Margin = new System.Windows.Forms.Padding(2);
            this.RTB_ScriptText.Name = "RTB_ScriptText";
            this.RTB_ScriptText.Size = new System.Drawing.Size(526, 346);
            this.RTB_ScriptText.TabIndex = 2;
            this.RTB_ScriptText.Text = "";
            this.RTB_ScriptText.WordWrap = false;
            // 
            // frmScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(526, 396);
            this.Controls.Add(this.RTB_ScriptText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmScript";
            this.ShowIcon = false;
            this.Text = "MeteoInfo Script";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMI_OpenFile;
        private System.Windows.Forms.ToolStripMenuItem TSMI_SaveFile;
        private System.Windows.Forms.ToolStripMenuItem TSMI_SaveAs;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSB_RunScript;
        internal System.Windows.Forms.RichTextBox RTB_ScriptText;
    }
}