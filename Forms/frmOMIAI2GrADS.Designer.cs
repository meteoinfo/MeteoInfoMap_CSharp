namespace MeteoInfo.Forms
{
    partial class frmOMIAI2GrADS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOMIAI2GrADS));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_Increment = new System.Windows.Forms.ComboBox();
            this.TB_Increment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DTP_End = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP_Start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.B_Convert = new System.Windows.Forms.Button();
            this.TB_DataFolder = new System.Windows.Forms.TextBox();
            this.B_DataFolder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.CB_Increment);
            this.groupBox1.Controls.Add(this.TB_Increment);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.DTP_End);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DTP_Start);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // CB_Increment
            // 
            this.CB_Increment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Increment.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Increment, "CB_Increment");
            this.CB_Increment.Name = "CB_Increment";
            // 
            // TB_Increment
            // 
            resources.ApplyResources(this.TB_Increment, "TB_Increment");
            this.TB_Increment.Name = "TB_Increment";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // DTP_End
            // 
            resources.ApplyResources(this.DTP_End, "DTP_End");
            this.DTP_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_End.Name = "DTP_End";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // DTP_Start
            // 
            resources.ApplyResources(this.DTP_Start, "DTP_Start");
            this.DTP_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_Start.Name = "DTP_Start";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // B_Convert
            // 
            resources.ApplyResources(this.B_Convert, "B_Convert");
            this.B_Convert.Name = "B_Convert";
            this.B_Convert.UseVisualStyleBackColor = true;
            this.B_Convert.Click += new System.EventHandler(this.B_Convert_Click);
            // 
            // TB_DataFolder
            // 
            resources.ApplyResources(this.TB_DataFolder, "TB_DataFolder");
            this.TB_DataFolder.Name = "TB_DataFolder";
            // 
            // B_DataFolder
            // 
            resources.ApplyResources(this.B_DataFolder, "B_DataFolder");
            this.B_DataFolder.Name = "B_DataFolder";
            this.B_DataFolder.UseVisualStyleBackColor = true;
            this.B_DataFolder.Click += new System.EventHandler(this.B_DataFolder_Click);
            // 
            // frmOMIAI2GrADS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.B_Convert);
            this.Controls.Add(this.TB_DataFolder);
            this.Controls.Add(this.B_DataFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOMIAI2GrADS";
            this.Load += new System.EventHandler(this.frmOMIAI2GrADS_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CB_Increment;
        private System.Windows.Forms.TextBox TB_Increment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DTP_End;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DTP_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button B_Convert;
        private System.Windows.Forms.TextBox TB_DataFolder;
        private System.Windows.Forms.Button B_DataFolder;
    }
}