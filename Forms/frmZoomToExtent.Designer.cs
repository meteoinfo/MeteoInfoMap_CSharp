namespace MeteoInfo.Forms
{
    partial class frmZoomToExtent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZoomToExtent));
            this.B_Close = new System.Windows.Forms.Button();
            this.B_Zoom = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_MaxLat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_MinLat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_MaxLon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_MinLon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            // B_Zoom
            // 
            this.B_Zoom.AccessibleDescription = null;
            this.B_Zoom.AccessibleName = null;
            resources.ApplyResources(this.B_Zoom, "B_Zoom");
            this.B_Zoom.BackgroundImage = null;
            this.B_Zoom.Font = null;
            this.B_Zoom.Name = "B_Zoom";
            this.B_Zoom.UseVisualStyleBackColor = true;
            this.B_Zoom.Click += new System.EventHandler(this.B_Zoom_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.TB_MaxLat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TB_MinLat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TB_MaxLon);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TB_MinLon);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // TB_MaxLat
            // 
            this.TB_MaxLat.AccessibleDescription = null;
            this.TB_MaxLat.AccessibleName = null;
            resources.ApplyResources(this.TB_MaxLat, "TB_MaxLat");
            this.TB_MaxLat.BackgroundImage = null;
            this.TB_MaxLat.Font = null;
            this.TB_MaxLat.Name = "TB_MaxLat";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // TB_MinLat
            // 
            this.TB_MinLat.AccessibleDescription = null;
            this.TB_MinLat.AccessibleName = null;
            resources.ApplyResources(this.TB_MinLat, "TB_MinLat");
            this.TB_MinLat.BackgroundImage = null;
            this.TB_MinLat.Font = null;
            this.TB_MinLat.Name = "TB_MinLat";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // TB_MaxLon
            // 
            this.TB_MaxLon.AccessibleDescription = null;
            this.TB_MaxLon.AccessibleName = null;
            resources.ApplyResources(this.TB_MaxLon, "TB_MaxLon");
            this.TB_MaxLon.BackgroundImage = null;
            this.TB_MaxLon.Font = null;
            this.TB_MaxLon.Name = "TB_MaxLon";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // TB_MinLon
            // 
            this.TB_MinLon.AccessibleDescription = null;
            this.TB_MinLon.AccessibleName = null;
            resources.ApplyResources(this.TB_MinLon, "TB_MinLon");
            this.TB_MinLon.BackgroundImage = null;
            this.TB_MinLon.Font = null;
            this.TB_MinLon.Name = "TB_MinLon";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // frmZoomToExtent
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.B_Zoom);
            this.Controls.Add(this.groupBox1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmZoomToExtent";
            this.Load += new System.EventHandler(this.frmZoomToExtent_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_Close;
        private System.Windows.Forms.Button B_Zoom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_MaxLat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_MinLat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_MaxLon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_MinLon;
        private System.Windows.Forms.Label label2;
    }
}