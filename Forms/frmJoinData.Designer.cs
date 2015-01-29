namespace MeteoInfo.Forms
{
    partial class frmJoinData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoinData));
            this.B_SelNone = new System.Windows.Forms.Button();
            this.B_SelAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.LB_AllFiles = new System.Windows.Forms.ListBox();
            this.LB_SelectedFiles = new System.Windows.Forms.ListBox();
            this.B_DataFolder = new System.Windows.Forms.Button();
            this.TB_DataFolder = new System.Windows.Forms.TextBox();
            this.B_UnSel = new System.Windows.Forms.Button();
            this.B_Sel = new System.Windows.Forms.Button();
            this.B_Close = new System.Windows.Forms.Button();
            this.B_Join = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.B_AddTimeDim = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_TimeDimName = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_SelNone
            // 
            resources.ApplyResources(this.B_SelNone, "B_SelNone");
            this.B_SelNone.Name = "B_SelNone";
            this.B_SelNone.UseVisualStyleBackColor = true;
            this.B_SelNone.Click += new System.EventHandler(this.B_SelNone_Click);
            // 
            // B_SelAll
            // 
            resources.ApplyResources(this.B_SelAll, "B_SelAll");
            this.B_SelAll.Name = "B_SelAll";
            this.B_SelAll.UseVisualStyleBackColor = true;
            this.B_SelAll.Click += new System.EventHandler(this.B_SelAll_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // LB_AllFiles
            // 
            this.LB_AllFiles.FormattingEnabled = true;
            resources.ApplyResources(this.LB_AllFiles, "LB_AllFiles");
            this.LB_AllFiles.Name = "LB_AllFiles";
            this.LB_AllFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // LB_SelectedFiles
            // 
            this.LB_SelectedFiles.FormattingEnabled = true;
            resources.ApplyResources(this.LB_SelectedFiles, "LB_SelectedFiles");
            this.LB_SelectedFiles.Name = "LB_SelectedFiles";
            this.LB_SelectedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // B_DataFolder
            // 
            resources.ApplyResources(this.B_DataFolder, "B_DataFolder");
            this.B_DataFolder.Name = "B_DataFolder";
            this.B_DataFolder.UseVisualStyleBackColor = true;
            this.B_DataFolder.Click += new System.EventHandler(this.B_DataFolder_Click);
            // 
            // TB_DataFolder
            // 
            resources.ApplyResources(this.TB_DataFolder, "TB_DataFolder");
            this.TB_DataFolder.Name = "TB_DataFolder";
            // 
            // B_UnSel
            // 
            resources.ApplyResources(this.B_UnSel, "B_UnSel");
            this.B_UnSel.Name = "B_UnSel";
            this.B_UnSel.UseVisualStyleBackColor = true;
            this.B_UnSel.Click += new System.EventHandler(this.B_UnSel_Click);
            // 
            // B_Sel
            // 
            resources.ApplyResources(this.B_Sel, "B_Sel");
            this.B_Sel.Name = "B_Sel";
            this.B_Sel.UseVisualStyleBackColor = true;
            this.B_Sel.Click += new System.EventHandler(this.B_Sel_Click);
            // 
            // B_Close
            // 
            resources.ApplyResources(this.B_Close, "B_Close");
            this.B_Close.Name = "B_Close";
            this.B_Close.UseVisualStyleBackColor = true;
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // B_Join
            // 
            resources.ApplyResources(this.B_Join, "B_Join");
            this.B_Join.Name = "B_Join";
            this.B_Join.UseVisualStyleBackColor = true;
            this.B_Join.Click += new System.EventHandler(this.B_Join_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            // 
            // B_AddTimeDim
            // 
            resources.ApplyResources(this.B_AddTimeDim, "B_AddTimeDim");
            this.B_AddTimeDim.Name = "B_AddTimeDim";
            this.B_AddTimeDim.UseVisualStyleBackColor = true;
            this.B_AddTimeDim.Click += new System.EventHandler(this.B_AddTimeDim_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TB_TimeDimName
            // 
            resources.ApplyResources(this.TB_TimeDimName, "TB_TimeDimName");
            this.TB_TimeDimName.Name = "TB_TimeDimName";
            // 
            // frmJoinData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TB_TimeDimName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_AddTimeDim);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.B_Join);
            this.Controls.Add(this.B_Sel);
            this.Controls.Add(this.B_UnSel);
            this.Controls.Add(this.TB_DataFolder);
            this.Controls.Add(this.B_DataFolder);
            this.Controls.Add(this.LB_SelectedFiles);
            this.Controls.Add(this.LB_AllFiles);
            this.Controls.Add(this.B_SelNone);
            this.Controls.Add(this.B_SelAll);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmJoinData";
            this.Load += new System.EventHandler(this.frmJoinData_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_SelNone;
        private System.Windows.Forms.Button B_SelAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox LB_AllFiles;
        private System.Windows.Forms.ListBox LB_SelectedFiles;
        private System.Windows.Forms.Button B_DataFolder;
        private System.Windows.Forms.TextBox TB_DataFolder;
        private System.Windows.Forms.Button B_UnSel;
        private System.Windows.Forms.Button B_Sel;
        private System.Windows.Forms.Button B_Close;
        private System.Windows.Forms.Button B_Join;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button B_AddTimeDim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_TimeDimName;
    }
}