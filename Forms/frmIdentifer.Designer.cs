namespace MeteoInfo.Forms
{
    partial class frmIdentifer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIdentifer));
            this.ListView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.AccessibleDescription = null;
            this.ListView1.AccessibleName = null;
            resources.ApplyResources(this.ListView1, "ListView1");
            this.ListView1.BackgroundImage = null;
            this.ListView1.Font = null;
            this.ListView1.GridLines = true;
            this.ListView1.Name = "ListView1";
            this.ListView1.UseCompatibleStateImageBehavior = false;
            // 
            // frmIdentifer
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.ListView1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = null;
            this.Name = "frmIdentifer";
            this.Load += new System.EventHandler(this.frmIdentifer_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmIdentifer_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView ListView1;
    }
}