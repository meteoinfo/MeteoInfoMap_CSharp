namespace MeteoInfo.Forms
{
    partial class frmUVSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUVSet));
            this.label1 = new System.Windows.Forms.Label();
            this.CB_U = new System.Windows.Forms.ComboBox();
            this.CB_V = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_NoUV = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.NUD_XSkip = new System.Windows.Forms.NumericUpDown();
            this.NUD_YSkip = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GB_WindFieldSet = new System.Windows.Forms.GroupBox();
            this.RB_U_V = new System.Windows.Forms.RadioButton();
            this.RB_Dir_Speed = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_XSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_YSkip)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.GB_WindFieldSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // CB_U
            // 
            this.CB_U.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_U.FormattingEnabled = true;
            resources.ApplyResources(this.CB_U, "CB_U");
            this.CB_U.Name = "CB_U";
            // 
            // CB_V
            // 
            this.CB_V.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_V.FormattingEnabled = true;
            resources.ApplyResources(this.CB_V, "CB_V");
            this.CB_V.Name = "CB_V";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // B_OK
            // 
            resources.ApplyResources(this.B_OK, "B_OK");
            this.B_OK.Name = "B_OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_NoUV
            // 
            resources.ApplyResources(this.B_NoUV, "B_NoUV");
            this.B_NoUV.Name = "B_NoUV";
            this.B_NoUV.UseVisualStyleBackColor = true;
            this.B_NoUV.Click += new System.EventHandler(this.B_NoUV_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // NUD_XSkip
            // 
            resources.ApplyResources(this.NUD_XSkip, "NUD_XSkip");
            this.NUD_XSkip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_XSkip.Name = "NUD_XSkip";
            this.NUD_XSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NUD_YSkip
            // 
            resources.ApplyResources(this.NUD_YSkip, "NUD_YSkip");
            this.NUD_YSkip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_YSkip.Name = "NUD_YSkip";
            this.NUD_YSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NUD_XSkip);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // GB_WindFieldSet
            // 
            this.GB_WindFieldSet.Controls.Add(this.RB_Dir_Speed);
            this.GB_WindFieldSet.Controls.Add(this.RB_U_V);
            resources.ApplyResources(this.GB_WindFieldSet, "GB_WindFieldSet");
            this.GB_WindFieldSet.Name = "GB_WindFieldSet";
            this.GB_WindFieldSet.TabStop = false;
            // 
            // RB_U_V
            // 
            resources.ApplyResources(this.RB_U_V, "RB_U_V");
            this.RB_U_V.Name = "RB_U_V";
            this.RB_U_V.TabStop = true;
            this.RB_U_V.UseVisualStyleBackColor = true;
            // 
            // RB_Dir_Speed
            // 
            resources.ApplyResources(this.RB_Dir_Speed, "RB_Dir_Speed");
            this.RB_Dir_Speed.Name = "RB_Dir_Speed";
            this.RB_Dir_Speed.TabStop = true;
            this.RB_Dir_Speed.UseVisualStyleBackColor = true;
            // 
            // frmUVSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GB_WindFieldSet);
            this.Controls.Add(this.NUD_YSkip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.B_NoUV);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.CB_V);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_U);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUVSet";            
            ((System.ComponentModel.ISupportInitialize)(this.NUD_XSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_YSkip)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GB_WindFieldSet.ResumeLayout(false);
            this.GB_WindFieldSet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox CB_U;
        internal System.Windows.Forms.ComboBox CB_V;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_NoUV;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown NUD_XSkip;
        internal System.Windows.Forms.NumericUpDown NUD_YSkip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.GroupBox GB_WindFieldSet;
        internal System.Windows.Forms.RadioButton RB_Dir_Speed;
        internal System.Windows.Forms.RadioButton RB_U_V;
    }
}