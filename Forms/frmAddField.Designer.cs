namespace MeteoInfo.Forms
{
    partial class frmAddField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddField));
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_OK = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CB_Type
            // 
            this.CB_Type.AccessibleDescription = null;
            this.CB_Type.AccessibleName = null;
            resources.ApplyResources(this.CB_Type, "CB_Type");
            this.CB_Type.BackgroundImage = null;
            this.CB_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Type.Font = null;
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Name = "CB_Type";
            // 
            // B_Cancel
            // 
            this.B_Cancel.AccessibleDescription = null;
            this.B_Cancel.AccessibleName = null;
            resources.ApplyResources(this.B_Cancel, "B_Cancel");
            this.B_Cancel.BackgroundImage = null;
            this.B_Cancel.Font = null;
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_OK
            // 
            this.B_OK.AccessibleDescription = null;
            this.B_OK.AccessibleName = null;
            resources.ApplyResources(this.B_OK, "B_OK");
            this.B_OK.BackgroundImage = null;
            this.B_OK.Font = null;
            this.B_OK.Name = "B_OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // Label2
            // 
            this.Label2.AccessibleDescription = null;
            this.Label2.AccessibleName = null;
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.Font = null;
            this.Label2.Name = "Label2";
            // 
            // TB_Name
            // 
            this.TB_Name.AccessibleDescription = null;
            this.TB_Name.AccessibleName = null;
            resources.ApplyResources(this.TB_Name, "TB_Name");
            this.TB_Name.BackgroundImage = null;
            this.TB_Name.Font = null;
            this.TB_Name.Name = "TB_Name";
            // 
            // Label1
            // 
            this.Label1.AccessibleDescription = null;
            this.Label1.AccessibleName = null;
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.Font = null;
            this.Label1.Name = "Label1";
            // 
            // frmAddField
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.CB_Type);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TB_Name);
            this.Controls.Add(this.Label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddField";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmAddField_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox CB_Type;
        internal System.Windows.Forms.Button B_Cancel;
        internal System.Windows.Forms.Button B_OK;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TB_Name;
        internal System.Windows.Forms.Label Label1;
    }
}