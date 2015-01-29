namespace MeteoInfo.Forms
{
    partial class frmComboBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComboBox));
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_OK = new System.Windows.Forms.Button();
            this.CB_Item = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // CB_Item
            // 
            this.CB_Item.AccessibleDescription = null;
            this.CB_Item.AccessibleName = null;
            resources.ApplyResources(this.CB_Item, "CB_Item");
            this.CB_Item.BackgroundImage = null;
            this.CB_Item.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Item.Font = null;
            this.CB_Item.FormattingEnabled = true;
            this.CB_Item.Name = "CB_Item";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // frmComboBox
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.CB_Item);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmComboBox";
            this.Load += new System.EventHandler(this.frmComboBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.ComboBox CB_Item;
        private System.Windows.Forms.Label label1;
    }
}