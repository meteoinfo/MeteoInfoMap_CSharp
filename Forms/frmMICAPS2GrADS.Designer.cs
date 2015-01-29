namespace MeteoInfo.Forms
{
    partial class frmMICAPS2GrADS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMICAPS2GrADS));
            this.TB_DataFolder = new System.Windows.Forms.TextBox();
            this.B_DataFolder = new System.Windows.Forms.Button();
            this.CLB_Variables = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.B_SelAll = new System.Windows.Forms.Button();
            this.B_SelNone = new System.Windows.Forms.Button();
            this.B_Convert = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_Increment = new System.Windows.Forms.ComboBox();
            this.TB_Increment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DTP_End = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP_Start = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_DataFolder
            // 
            this.TB_DataFolder.AccessibleDescription = null;
            this.TB_DataFolder.AccessibleName = null;
            resources.ApplyResources(this.TB_DataFolder, "TB_DataFolder");
            this.TB_DataFolder.BackgroundImage = null;
            this.TB_DataFolder.Font = null;
            this.TB_DataFolder.Name = "TB_DataFolder";
            // 
            // B_DataFolder
            // 
            this.B_DataFolder.AccessibleDescription = null;
            this.B_DataFolder.AccessibleName = null;
            resources.ApplyResources(this.B_DataFolder, "B_DataFolder");
            this.B_DataFolder.BackgroundImage = null;
            this.B_DataFolder.Font = null;
            this.B_DataFolder.Name = "B_DataFolder";
            this.B_DataFolder.UseVisualStyleBackColor = true;
            this.B_DataFolder.Click += new System.EventHandler(this.B_DataFolder_Click);
            // 
            // CLB_Variables
            // 
            this.CLB_Variables.AccessibleDescription = null;
            this.CLB_Variables.AccessibleName = null;
            resources.ApplyResources(this.CLB_Variables, "CLB_Variables");
            this.CLB_Variables.BackgroundImage = null;
            this.CLB_Variables.CheckOnClick = true;
            this.CLB_Variables.Font = null;
            this.CLB_Variables.FormattingEnabled = true;
            this.CLB_Variables.Name = "CLB_Variables";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // B_SelAll
            // 
            this.B_SelAll.AccessibleDescription = null;
            this.B_SelAll.AccessibleName = null;
            resources.ApplyResources(this.B_SelAll, "B_SelAll");
            this.B_SelAll.BackgroundImage = null;
            this.B_SelAll.Font = null;
            this.B_SelAll.Name = "B_SelAll";
            this.B_SelAll.UseVisualStyleBackColor = true;
            this.B_SelAll.Click += new System.EventHandler(this.B_SelAll_Click);
            // 
            // B_SelNone
            // 
            this.B_SelNone.AccessibleDescription = null;
            this.B_SelNone.AccessibleName = null;
            resources.ApplyResources(this.B_SelNone, "B_SelNone");
            this.B_SelNone.BackgroundImage = null;
            this.B_SelNone.Font = null;
            this.B_SelNone.Name = "B_SelNone";
            this.B_SelNone.UseVisualStyleBackColor = true;
            this.B_SelNone.Click += new System.EventHandler(this.B_SelNone_Click);
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
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.CB_Increment);
            this.groupBox1.Controls.Add(this.TB_Increment);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.DTP_End);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DTP_Start);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            // 
            // CB_Increment
            // 
            this.CB_Increment.AccessibleDescription = null;
            this.CB_Increment.AccessibleName = null;
            resources.ApplyResources(this.CB_Increment, "CB_Increment");
            this.CB_Increment.BackgroundImage = null;
            this.CB_Increment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Increment.Font = null;
            this.CB_Increment.FormattingEnabled = true;
            this.CB_Increment.Name = "CB_Increment";
            // 
            // TB_Increment
            // 
            this.TB_Increment.AccessibleDescription = null;
            this.TB_Increment.AccessibleName = null;
            resources.ApplyResources(this.TB_Increment, "TB_Increment");
            this.TB_Increment.BackgroundImage = null;
            this.TB_Increment.Font = null;
            this.TB_Increment.Name = "TB_Increment";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // DTP_End
            // 
            this.DTP_End.AccessibleDescription = null;
            this.DTP_End.AccessibleName = null;
            resources.ApplyResources(this.DTP_End, "DTP_End");
            this.DTP_End.BackgroundImage = null;
            this.DTP_End.CalendarFont = null;
            this.DTP_End.Font = null;
            this.DTP_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_End.Name = "DTP_End";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // DTP_Start
            // 
            this.DTP_Start.AccessibleDescription = null;
            this.DTP_Start.AccessibleName = null;
            resources.ApplyResources(this.DTP_Start, "DTP_Start");
            this.DTP_Start.BackgroundImage = null;
            this.DTP_Start.CalendarFont = null;
            this.DTP_Start.Font = null;
            this.DTP_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_Start.Name = "DTP_Start";
            // 
            // frmMICAPS2GrADS
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.B_Convert);
            this.Controls.Add(this.B_SelNone);
            this.Controls.Add(this.B_SelAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CLB_Variables);
            this.Controls.Add(this.TB_DataFolder);
            this.Controls.Add(this.B_DataFolder);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmMICAPS2GrADS";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmMICAPS2GrADS_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_DataFolder;
        private System.Windows.Forms.Button B_DataFolder;
        private System.Windows.Forms.CheckedListBox CLB_Variables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button B_SelAll;
        private System.Windows.Forms.Button B_SelNone;
        private System.Windows.Forms.Button B_Convert;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DTP_End;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTP_Start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CB_Increment;
        private System.Windows.Forms.TextBox TB_Increment;

    }
}