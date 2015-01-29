namespace MeteoInfo.Forms
{
    partial class frmMICAPS4To
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMICAPS4To));
            this.LB_SelectedFiles = new System.Windows.Forms.ListBox();
            this.B_OpenFiles = new System.Windows.Forms.Button();
            this.B_Convert = new System.Windows.Forms.Button();
            this.CB_OutputFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_SelectedFiles
            // 
            this.LB_SelectedFiles.AccessibleDescription = null;
            this.LB_SelectedFiles.AccessibleName = null;
            resources.ApplyResources(this.LB_SelectedFiles, "LB_SelectedFiles");
            this.LB_SelectedFiles.BackgroundImage = null;
            this.LB_SelectedFiles.Font = null;
            this.LB_SelectedFiles.FormattingEnabled = true;
            this.LB_SelectedFiles.Name = "LB_SelectedFiles";
            this.LB_SelectedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // B_OpenFiles
            // 
            this.B_OpenFiles.AccessibleDescription = null;
            this.B_OpenFiles.AccessibleName = null;
            resources.ApplyResources(this.B_OpenFiles, "B_OpenFiles");
            this.B_OpenFiles.BackgroundImage = null;
            this.B_OpenFiles.Font = null;
            this.B_OpenFiles.Name = "B_OpenFiles";
            this.B_OpenFiles.UseVisualStyleBackColor = true;
            this.B_OpenFiles.Click += new System.EventHandler(this.B_OpenFiles_Click);
            // 
            // B_Convert
            // 
            this.B_Convert.AccessibleDescription = null;
            this.B_Convert.AccessibleName = null;
            resources.ApplyResources(this.B_Convert, "B_Convert");
            this.B_Convert.BackgroundImage = null;
            this.B_Convert.Font = null;
            this.B_Convert.Name = "B_Convert";
            this.B_Convert.UseVisualStyleBackColor = true;
            this.B_Convert.Click += new System.EventHandler(this.B_Convert_Click);
            // 
            // CB_OutputFormat
            // 
            this.CB_OutputFormat.AccessibleDescription = null;
            this.CB_OutputFormat.AccessibleName = null;
            resources.ApplyResources(this.CB_OutputFormat, "CB_OutputFormat");
            this.CB_OutputFormat.BackgroundImage = null;
            this.CB_OutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_OutputFormat.Font = null;
            this.CB_OutputFormat.FormattingEnabled = true;
            this.CB_OutputFormat.Name = "CB_OutputFormat";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AccessibleDescription = null;
            this.statusStrip1.AccessibleName = null;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackgroundImage = null;
            this.statusStrip1.Font = null;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AccessibleDescription = null;
            this.toolStripProgressBar1.AccessibleName = null;
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            // 
            // frmMICAPS4To
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CB_OutputFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.B_Convert);
            this.Controls.Add(this.B_OpenFiles);
            this.Controls.Add(this.LB_SelectedFiles);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmMICAPS4To";
            this.Load += new System.EventHandler(this.frmMICAPS4To_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LB_SelectedFiles;
        private System.Windows.Forms.Button B_OpenFiles;
        private System.Windows.Forms.Button B_Convert;
        private System.Windows.Forms.ComboBox CB_OutputFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}