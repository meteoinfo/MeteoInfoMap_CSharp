namespace MeteoInfo.Forms
{
    partial class frmOpenMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenMap));
            this.label1 = new System.Windows.Forms.Label();
            this.LB_MapFiles = new System.Windows.Forms.ListBox();
            this.B_OpenMapFile = new System.Windows.Forms.Button();
            this.CB_RemoveMaps = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_OtherFolder = new System.Windows.Forms.RadioButton();
            this.RB_DefaultFolder = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // LB_MapFiles
            // 
            this.LB_MapFiles.FormattingEnabled = true;
            resources.ApplyResources(this.LB_MapFiles, "LB_MapFiles");
            this.LB_MapFiles.Name = "LB_MapFiles";
            // 
            // B_OpenMapFile
            // 
            resources.ApplyResources(this.B_OpenMapFile, "B_OpenMapFile");
            this.B_OpenMapFile.Name = "B_OpenMapFile";
            this.B_OpenMapFile.UseVisualStyleBackColor = true;
            this.B_OpenMapFile.Click += new System.EventHandler(this.B_OpenMapFile_Click);
            // 
            // CB_RemoveMaps
            // 
            resources.ApplyResources(this.CB_RemoveMaps, "CB_RemoveMaps");
            this.CB_RemoveMaps.Name = "CB_RemoveMaps";
            this.CB_RemoveMaps.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_OtherFolder);
            this.groupBox1.Controls.Add(this.RB_DefaultFolder);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // RB_OtherFolder
            // 
            resources.ApplyResources(this.RB_OtherFolder, "RB_OtherFolder");
            this.RB_OtherFolder.Name = "RB_OtherFolder";
            this.RB_OtherFolder.TabStop = true;
            this.RB_OtherFolder.UseVisualStyleBackColor = true;
            // 
            // RB_DefaultFolder
            // 
            resources.ApplyResources(this.RB_DefaultFolder, "RB_DefaultFolder");
            this.RB_DefaultFolder.Name = "RB_DefaultFolder";
            this.RB_DefaultFolder.TabStop = true;
            this.RB_DefaultFolder.UseVisualStyleBackColor = true;
            this.RB_DefaultFolder.CheckedChanged += new System.EventHandler(this.RB_DefaultFolder_CheckedChanged);
            // 
            // frmOpenMap
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CB_RemoveMaps);
            this.Controls.Add(this.B_OpenMapFile);
            this.Controls.Add(this.LB_MapFiles);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOpenMap";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmOpenMap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox LB_MapFiles;
        private System.Windows.Forms.Button B_OpenMapFile;
        private System.Windows.Forms.CheckBox CB_RemoveMaps;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_DefaultFolder;
        private System.Windows.Forms.RadioButton RB_OtherFolder;
    }
}