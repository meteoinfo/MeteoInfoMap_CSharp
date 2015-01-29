namespace MeteoInfo.Forms
{
    partial class frmMeasurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeasurement));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_Feature = new System.Windows.Forms.ToolStripButton();
            this.TSB_Distance = new System.Windows.Forms.ToolStripButton();
            this.TSB_Area = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSL_Units = new System.Windows.Forms.ToolStripLabel();
            this.TSCB_Units = new System.Windows.Forms.ToolStripComboBox();
            this.TB_content = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = null;
            this.toolStrip1.AccessibleName = null;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackgroundImage = null;
            this.toolStrip1.Font = null;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_Feature,
            this.TSB_Distance,
            this.TSB_Area,
            this.toolStripSeparator1,
            this.TSL_Units,
            this.TSCB_Units});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // TSB_Feature
            // 
            this.TSB_Feature.AccessibleDescription = null;
            this.TSB_Feature.AccessibleName = null;
            resources.ApplyResources(this.TSB_Feature, "TSB_Feature");
            this.TSB_Feature.BackgroundImage = null;
            this.TSB_Feature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Feature.Name = "TSB_Feature";
            this.TSB_Feature.Click += new System.EventHandler(this.TSB_Feature_Click);
            // 
            // TSB_Distance
            // 
            this.TSB_Distance.AccessibleDescription = null;
            this.TSB_Distance.AccessibleName = null;
            resources.ApplyResources(this.TSB_Distance, "TSB_Distance");
            this.TSB_Distance.BackgroundImage = null;
            this.TSB_Distance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Distance.Name = "TSB_Distance";
            this.TSB_Distance.Click += new System.EventHandler(this.TSB_Distance_Click);
            // 
            // TSB_Area
            // 
            this.TSB_Area.AccessibleDescription = null;
            this.TSB_Area.AccessibleName = null;
            resources.ApplyResources(this.TSB_Area, "TSB_Area");
            this.TSB_Area.BackgroundImage = null;
            this.TSB_Area.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Area.Name = "TSB_Area";
            this.TSB_Area.Click += new System.EventHandler(this.TSB_Area_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AccessibleDescription = null;
            this.toolStripSeparator1.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // TSL_Units
            // 
            this.TSL_Units.AccessibleDescription = null;
            this.TSL_Units.AccessibleName = null;
            resources.ApplyResources(this.TSL_Units, "TSL_Units");
            this.TSL_Units.BackgroundImage = null;
            this.TSL_Units.Name = "TSL_Units";
            // 
            // TSCB_Units
            // 
            this.TSCB_Units.AccessibleDescription = null;
            this.TSCB_Units.AccessibleName = null;
            resources.ApplyResources(this.TSCB_Units, "TSCB_Units");
            this.TSCB_Units.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TSCB_Units.Name = "TSCB_Units";
            this.TSCB_Units.SelectedIndexChanged += new System.EventHandler(this.TSCB_Units_SelectedIndexChanged);
            // 
            // TB_content
            // 
            this.TB_content.AccessibleDescription = null;
            this.TB_content.AccessibleName = null;
            resources.ApplyResources(this.TB_content, "TB_content");
            this.TB_content.BackgroundImage = null;
            this.TB_content.Font = null;
            this.TB_content.Name = "TB_content";
            // 
            // frmMeasurement
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.TB_content);
            this.Controls.Add(this.toolStrip1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeasurement";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmMeasurement_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMeasurement_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSB_Distance;
        private System.Windows.Forms.ToolStripButton TSB_Area;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel TSL_Units;
        private System.Windows.Forms.ToolStripComboBox TSCB_Units;
        private System.Windows.Forms.TextBox TB_content;
        private System.Windows.Forms.ToolStripButton TSB_Feature;
    }
}