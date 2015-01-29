namespace MeteoInfo.Forms
{
    partial class frmAddXYLayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddXYLayer));
            this.B_Cancel = new System.Windows.Forms.Button();
            this.TB_Info = new System.Windows.Forms.TextBox();
            this.TB_InFile = new System.Windows.Forms.TextBox();
            this.B_InFile = new System.Windows.Forms.Button();
            this.GB_SelField = new System.Windows.Forms.GroupBox();
            this.CB_LatFld = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.CB_LonFld = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.B_AddData = new System.Windows.Forms.Button();
            this.GB_SelField.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Cancel
            // 
            resources.ApplyResources(this.B_Cancel, "B_Cancel");
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // TB_Info
            // 
            this.TB_Info.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.TB_Info, "TB_Info");
            this.TB_Info.Name = "TB_Info";
            // 
            // TB_InFile
            // 
            resources.ApplyResources(this.TB_InFile, "TB_InFile");
            this.TB_InFile.Name = "TB_InFile";
            // 
            // B_InFile
            // 
            resources.ApplyResources(this.B_InFile, "B_InFile");
            this.B_InFile.Name = "B_InFile";
            this.B_InFile.UseVisualStyleBackColor = true;
            this.B_InFile.Click += new System.EventHandler(this.B_InFile_Click);
            // 
            // GB_SelField
            // 
            this.GB_SelField.Controls.Add(this.CB_LatFld);
            this.GB_SelField.Controls.Add(this.Label3);
            this.GB_SelField.Controls.Add(this.CB_LonFld);
            this.GB_SelField.Controls.Add(this.Label2);
            resources.ApplyResources(this.GB_SelField, "GB_SelField");
            this.GB_SelField.Name = "GB_SelField";
            this.GB_SelField.TabStop = false;
            // 
            // CB_LatFld
            // 
            this.CB_LatFld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_LatFld.FormattingEnabled = true;
            resources.ApplyResources(this.CB_LatFld, "CB_LatFld");
            this.CB_LatFld.Name = "CB_LatFld";
            // 
            // Label3
            // 
            resources.ApplyResources(this.Label3, "Label3");
            this.Label3.Name = "Label3";
            // 
            // CB_LonFld
            // 
            this.CB_LonFld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_LonFld.FormattingEnabled = true;
            resources.ApplyResources(this.CB_LonFld, "CB_LonFld");
            this.CB_LonFld.Name = "CB_LonFld";
            // 
            // Label2
            // 
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.Name = "Label2";
            // 
            // B_AddData
            // 
            resources.ApplyResources(this.B_AddData, "B_AddData");
            this.B_AddData.Name = "B_AddData";
            this.B_AddData.UseVisualStyleBackColor = true;
            this.B_AddData.Click += new System.EventHandler(this.B_AddData_Click);
            // 
            // frmAddXYLayer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.TB_Info);
            this.Controls.Add(this.TB_InFile);
            this.Controls.Add(this.B_InFile);
            this.Controls.Add(this.GB_SelField);
            this.Controls.Add(this.B_AddData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddXYLayer";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmAddXYLayer_Load);
            this.GB_SelField.ResumeLayout(false);
            this.GB_SelField.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button B_Cancel;
        internal System.Windows.Forms.TextBox TB_Info;
        internal System.Windows.Forms.TextBox TB_InFile;
        internal System.Windows.Forms.Button B_InFile;
        internal System.Windows.Forms.GroupBox GB_SelField;
        internal System.Windows.Forms.ComboBox CB_LatFld;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox CB_LonFld;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button B_AddData;
    }
}