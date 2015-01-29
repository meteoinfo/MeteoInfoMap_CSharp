namespace MeteoInfo.Forms
{
    partial class frmGridViewSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGridViewSet));
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_OK = new System.Windows.Forms.Button();
            this.CHB_InterpolateGrid = new System.Windows.Forms.CheckBox();
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
            // CHB_InterpolateGrid
            // 
            this.CHB_InterpolateGrid.AccessibleDescription = null;
            this.CHB_InterpolateGrid.AccessibleName = null;
            resources.ApplyResources(this.CHB_InterpolateGrid, "CHB_InterpolateGrid");
            this.CHB_InterpolateGrid.BackgroundImage = null;
            this.CHB_InterpolateGrid.Font = null;
            this.CHB_InterpolateGrid.Name = "CHB_InterpolateGrid";
            this.CHB_InterpolateGrid.UseVisualStyleBackColor = true;
            // 
            // frmGridViewSet
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.CHB_InterpolateGrid);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmGridViewSet";
            this.Load += new System.EventHandler(this.frmGridViewSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.CheckBox CHB_InterpolateGrid;
    }
}