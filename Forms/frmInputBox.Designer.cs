namespace MeteoInfo.Forms
{
    partial class frmInputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInputBox));
            this.Lab_Info = new System.Windows.Forms.Label();
            this.TB_Value = new System.Windows.Forms.TextBox();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lab_Info
            // 
            this.Lab_Info.AccessibleDescription = null;
            this.Lab_Info.AccessibleName = null;
            resources.ApplyResources(this.Lab_Info, "Lab_Info");
            this.Lab_Info.Font = null;
            this.Lab_Info.Name = "Lab_Info";
            // 
            // TB_Value
            // 
            this.TB_Value.AccessibleDescription = null;
            this.TB_Value.AccessibleName = null;
            resources.ApplyResources(this.TB_Value, "TB_Value");
            this.TB_Value.BackgroundImage = null;
            this.TB_Value.Font = null;
            this.TB_Value.Name = "TB_Value";
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
            // frmInputBox
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.TB_Value);
            this.Controls.Add(this.Lab_Info);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Info;
        internal System.Windows.Forms.TextBox TB_Value;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
    }
}