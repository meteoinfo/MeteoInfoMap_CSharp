namespace MeteoInfo.Forms
{
    partial class frmProjection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjection));
            this.label1 = new System.Windows.Forms.Label();
            this.CB_Projection = new System.Windows.Forms.ComboBox();
            this.Lab_CentralMeridian = new System.Windows.Forms.Label();
            this.TB_CentralMeridian = new System.Windows.Forms.TextBox();
            this.TB_RefLat = new System.Windows.Forms.TextBox();
            this.Lab_RefLat = new System.Windows.Forms.Label();
            this.TB_StandPara1 = new System.Windows.Forms.TextBox();
            this.Lab_SP1 = new System.Windows.Forms.Label();
            this.TB_StandPara2 = new System.Windows.Forms.TextBox();
            this.Lab_SP2 = new System.Windows.Forms.Label();
            this.TB_FalseEasting = new System.Windows.Forms.TextBox();
            this.Lab_FalseEasting = new System.Windows.Forms.Label();
            this.TB_FalseNorthing = new System.Windows.Forms.TextBox();
            this.Lab_FalseNorthing = new System.Windows.Forms.Label();
            this.GB_Parameters = new System.Windows.Forms.GroupBox();
            this.TB_ScaleFactor = new System.Windows.Forms.TextBox();
            this.Lab_ScaleFactor = new System.Windows.Forms.Label();
            this.B_Apply = new System.Windows.Forms.Button();
            this.B_Close = new System.Windows.Forms.Button();
            this.GB_Parameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // CB_Projection
            // 
            this.CB_Projection.AccessibleDescription = null;
            this.CB_Projection.AccessibleName = null;
            resources.ApplyResources(this.CB_Projection, "CB_Projection");
            this.CB_Projection.BackgroundImage = null;
            this.CB_Projection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Projection.Font = null;
            this.CB_Projection.FormattingEnabled = true;
            this.CB_Projection.Name = "CB_Projection";
            this.CB_Projection.SelectedIndexChanged += new System.EventHandler(this.CB_Projection_SelectedIndexChanged);
            // 
            // Lab_CentralMeridian
            // 
            this.Lab_CentralMeridian.AccessibleDescription = null;
            this.Lab_CentralMeridian.AccessibleName = null;
            resources.ApplyResources(this.Lab_CentralMeridian, "Lab_CentralMeridian");
            this.Lab_CentralMeridian.Font = null;
            this.Lab_CentralMeridian.Name = "Lab_CentralMeridian";
            // 
            // TB_CentralMeridian
            // 
            this.TB_CentralMeridian.AccessibleDescription = null;
            this.TB_CentralMeridian.AccessibleName = null;
            resources.ApplyResources(this.TB_CentralMeridian, "TB_CentralMeridian");
            this.TB_CentralMeridian.BackgroundImage = null;
            this.TB_CentralMeridian.Font = null;
            this.TB_CentralMeridian.Name = "TB_CentralMeridian";
            // 
            // TB_RefLat
            // 
            this.TB_RefLat.AccessibleDescription = null;
            this.TB_RefLat.AccessibleName = null;
            resources.ApplyResources(this.TB_RefLat, "TB_RefLat");
            this.TB_RefLat.BackgroundImage = null;
            this.TB_RefLat.Font = null;
            this.TB_RefLat.Name = "TB_RefLat";
            // 
            // Lab_RefLat
            // 
            this.Lab_RefLat.AccessibleDescription = null;
            this.Lab_RefLat.AccessibleName = null;
            resources.ApplyResources(this.Lab_RefLat, "Lab_RefLat");
            this.Lab_RefLat.Font = null;
            this.Lab_RefLat.Name = "Lab_RefLat";
            // 
            // TB_StandPara1
            // 
            this.TB_StandPara1.AccessibleDescription = null;
            this.TB_StandPara1.AccessibleName = null;
            resources.ApplyResources(this.TB_StandPara1, "TB_StandPara1");
            this.TB_StandPara1.BackgroundImage = null;
            this.TB_StandPara1.Font = null;
            this.TB_StandPara1.Name = "TB_StandPara1";
            // 
            // Lab_SP1
            // 
            this.Lab_SP1.AccessibleDescription = null;
            this.Lab_SP1.AccessibleName = null;
            resources.ApplyResources(this.Lab_SP1, "Lab_SP1");
            this.Lab_SP1.Font = null;
            this.Lab_SP1.Name = "Lab_SP1";
            // 
            // TB_StandPara2
            // 
            this.TB_StandPara2.AccessibleDescription = null;
            this.TB_StandPara2.AccessibleName = null;
            resources.ApplyResources(this.TB_StandPara2, "TB_StandPara2");
            this.TB_StandPara2.BackgroundImage = null;
            this.TB_StandPara2.Font = null;
            this.TB_StandPara2.Name = "TB_StandPara2";
            // 
            // Lab_SP2
            // 
            this.Lab_SP2.AccessibleDescription = null;
            this.Lab_SP2.AccessibleName = null;
            resources.ApplyResources(this.Lab_SP2, "Lab_SP2");
            this.Lab_SP2.Font = null;
            this.Lab_SP2.Name = "Lab_SP2";
            // 
            // TB_FalseEasting
            // 
            this.TB_FalseEasting.AccessibleDescription = null;
            this.TB_FalseEasting.AccessibleName = null;
            resources.ApplyResources(this.TB_FalseEasting, "TB_FalseEasting");
            this.TB_FalseEasting.BackgroundImage = null;
            this.TB_FalseEasting.Font = null;
            this.TB_FalseEasting.Name = "TB_FalseEasting";
            // 
            // Lab_FalseEasting
            // 
            this.Lab_FalseEasting.AccessibleDescription = null;
            this.Lab_FalseEasting.AccessibleName = null;
            resources.ApplyResources(this.Lab_FalseEasting, "Lab_FalseEasting");
            this.Lab_FalseEasting.Font = null;
            this.Lab_FalseEasting.Name = "Lab_FalseEasting";
            // 
            // TB_FalseNorthing
            // 
            this.TB_FalseNorthing.AccessibleDescription = null;
            this.TB_FalseNorthing.AccessibleName = null;
            resources.ApplyResources(this.TB_FalseNorthing, "TB_FalseNorthing");
            this.TB_FalseNorthing.BackgroundImage = null;
            this.TB_FalseNorthing.Font = null;
            this.TB_FalseNorthing.Name = "TB_FalseNorthing";
            // 
            // Lab_FalseNorthing
            // 
            this.Lab_FalseNorthing.AccessibleDescription = null;
            this.Lab_FalseNorthing.AccessibleName = null;
            resources.ApplyResources(this.Lab_FalseNorthing, "Lab_FalseNorthing");
            this.Lab_FalseNorthing.Font = null;
            this.Lab_FalseNorthing.Name = "Lab_FalseNorthing";
            // 
            // GB_Parameters
            // 
            this.GB_Parameters.AccessibleDescription = null;
            this.GB_Parameters.AccessibleName = null;
            resources.ApplyResources(this.GB_Parameters, "GB_Parameters");
            this.GB_Parameters.BackgroundImage = null;
            this.GB_Parameters.Controls.Add(this.TB_ScaleFactor);
            this.GB_Parameters.Controls.Add(this.Lab_ScaleFactor);
            this.GB_Parameters.Controls.Add(this.TB_FalseNorthing);
            this.GB_Parameters.Controls.Add(this.Lab_FalseNorthing);
            this.GB_Parameters.Controls.Add(this.TB_FalseEasting);
            this.GB_Parameters.Controls.Add(this.Lab_FalseEasting);
            this.GB_Parameters.Controls.Add(this.TB_StandPara2);
            this.GB_Parameters.Controls.Add(this.Lab_SP2);
            this.GB_Parameters.Controls.Add(this.TB_StandPara1);
            this.GB_Parameters.Controls.Add(this.Lab_SP1);
            this.GB_Parameters.Controls.Add(this.TB_RefLat);
            this.GB_Parameters.Controls.Add(this.Lab_RefLat);
            this.GB_Parameters.Controls.Add(this.TB_CentralMeridian);
            this.GB_Parameters.Controls.Add(this.Lab_CentralMeridian);
            this.GB_Parameters.Font = null;
            this.GB_Parameters.Name = "GB_Parameters";
            this.GB_Parameters.TabStop = false;
            // 
            // TB_ScaleFactor
            // 
            this.TB_ScaleFactor.AccessibleDescription = null;
            this.TB_ScaleFactor.AccessibleName = null;
            resources.ApplyResources(this.TB_ScaleFactor, "TB_ScaleFactor");
            this.TB_ScaleFactor.BackgroundImage = null;
            this.TB_ScaleFactor.Font = null;
            this.TB_ScaleFactor.Name = "TB_ScaleFactor";
            // 
            // Lab_ScaleFactor
            // 
            this.Lab_ScaleFactor.AccessibleDescription = null;
            this.Lab_ScaleFactor.AccessibleName = null;
            resources.ApplyResources(this.Lab_ScaleFactor, "Lab_ScaleFactor");
            this.Lab_ScaleFactor.Font = null;
            this.Lab_ScaleFactor.Name = "Lab_ScaleFactor";
            // 
            // B_Apply
            // 
            this.B_Apply.AccessibleDescription = null;
            this.B_Apply.AccessibleName = null;
            resources.ApplyResources(this.B_Apply, "B_Apply");
            this.B_Apply.BackgroundImage = null;
            this.B_Apply.Font = null;
            this.B_Apply.Name = "B_Apply";
            this.B_Apply.UseVisualStyleBackColor = true;
            this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
            // 
            // B_Close
            // 
            this.B_Close.AccessibleDescription = null;
            this.B_Close.AccessibleName = null;
            resources.ApplyResources(this.B_Close, "B_Close");
            this.B_Close.BackgroundImage = null;
            this.B_Close.Font = null;
            this.B_Close.Name = "B_Close";
            this.B_Close.UseVisualStyleBackColor = true;
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // frmProjection
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Apply);
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.GB_Parameters);
            this.Controls.Add(this.CB_Projection);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmProjection";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmProjection_Load);
            this.GB_Parameters.ResumeLayout(false);
            this.GB_Parameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_Projection;
        private System.Windows.Forms.Label Lab_CentralMeridian;
        private System.Windows.Forms.TextBox TB_CentralMeridian;
        private System.Windows.Forms.TextBox TB_RefLat;
        private System.Windows.Forms.Label Lab_RefLat;
        private System.Windows.Forms.TextBox TB_StandPara1;
        private System.Windows.Forms.Label Lab_SP1;
        private System.Windows.Forms.TextBox TB_StandPara2;
        private System.Windows.Forms.Label Lab_SP2;
        private System.Windows.Forms.TextBox TB_FalseEasting;
        private System.Windows.Forms.Label Lab_FalseEasting;
        private System.Windows.Forms.TextBox TB_FalseNorthing;
        private System.Windows.Forms.Label Lab_FalseNorthing;
        private System.Windows.Forms.GroupBox GB_Parameters;
        internal System.Windows.Forms.Button B_Apply;
        internal System.Windows.Forms.Button B_Close;
        private System.Windows.Forms.TextBox TB_ScaleFactor;
        private System.Windows.Forms.Label Lab_ScaleFactor;
    }
}