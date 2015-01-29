namespace MeteoInfo.Forms
{
    partial class frmOutputMapData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutputMapData));
            this.Lab_ShpLayer = new System.Windows.Forms.Label();
            this.CB_MapLayers = new System.Windows.Forms.ComboBox();
            this.CB_OutputFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.B_Convert = new System.Windows.Forms.Button();
            this.B_Close = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lab_ShpLayer
            // 
            resources.ApplyResources(this.Lab_ShpLayer, "Lab_ShpLayer");
            this.Lab_ShpLayer.Name = "Lab_ShpLayer";
            // 
            // CB_MapLayers
            // 
            this.CB_MapLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_MapLayers.FormattingEnabled = true;
            resources.ApplyResources(this.CB_MapLayers, "CB_MapLayers");
            this.CB_MapLayers.Name = "CB_MapLayers";
            this.CB_MapLayers.SelectedIndexChanged += new System.EventHandler(this.CB_MapLayers_SelectedIndexChanged);
            // 
            // CB_OutputFormat
            // 
            this.CB_OutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_OutputFormat.FormattingEnabled = true;
            resources.ApplyResources(this.CB_OutputFormat, "CB_OutputFormat");
            this.CB_OutputFormat.Name = "CB_OutputFormat";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // B_Convert
            // 
            resources.ApplyResources(this.B_Convert, "B_Convert");
            this.B_Convert.Name = "B_Convert";
            this.B_Convert.UseVisualStyleBackColor = true;
            this.B_Convert.Click += new System.EventHandler(this.B_Convert_Click);
            // 
            // B_Close
            // 
            resources.ApplyResources(this.B_Close, "B_Close");
            this.B_Close.Name = "B_Close";
            this.B_Close.UseVisualStyleBackColor = true;
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            // 
            // frmOutputMapData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.B_Convert);
            this.Controls.Add(this.CB_OutputFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_MapLayers);
            this.Controls.Add(this.Lab_ShpLayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOutputMapData";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmConvertMapLayer_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_ShpLayer;
        private System.Windows.Forms.ComboBox CB_MapLayers;
        private System.Windows.Forms.ComboBox CB_OutputFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_Convert;
        private System.Windows.Forms.Button B_Close;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}