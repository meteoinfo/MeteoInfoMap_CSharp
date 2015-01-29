namespace MeteoInfo.Forms
{
    partial class frmChart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChart));
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.CB_ChartType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.AccessibleDescription = null;
            this.zedGraphControl1.AccessibleName = null;
            resources.ApplyResources(this.zedGraphControl1, "zedGraphControl1");
            this.zedGraphControl1.BackgroundImage = null;
            this.zedGraphControl1.Font = null;
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = null;
            this.toolStrip1.AccessibleName = null;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackgroundImage = null;
            this.toolStrip1.Font = null;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel5,
            this.CB_ChartType});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // ToolStripLabel5
            // 
            this.ToolStripLabel5.AccessibleDescription = null;
            this.ToolStripLabel5.AccessibleName = null;
            resources.ApplyResources(this.ToolStripLabel5, "ToolStripLabel5");
            this.ToolStripLabel5.BackgroundImage = null;
            this.ToolStripLabel5.Name = "ToolStripLabel5";
            // 
            // CB_ChartType
            // 
            this.CB_ChartType.AccessibleDescription = null;
            this.CB_ChartType.AccessibleName = null;
            resources.ApplyResources(this.CB_ChartType, "CB_ChartType");
            this.CB_ChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ChartType.Name = "CB_ChartType";
            this.CB_ChartType.SelectedIndexChanged += new System.EventHandler(this.CB_ChartType_SelectedIndexChanged);
            // 
            // frmChart
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = null;
            this.Icon = null;
            this.Name = "frmChart";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmChart_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel5;
        internal System.Windows.Forms.ToolStripComboBox CB_ChartType;
    }
}