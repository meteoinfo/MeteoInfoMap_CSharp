namespace MeteoInfo.Forms
{
    partial class frmClipping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClipping));
            this.CB_FromLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChB_SelFeaturesOnly = new System.Windows.Forms.CheckBox();
            this.CB_ClippingLayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.B_Apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CB_FromLayer
            // 
            this.CB_FromLayer.AccessibleDescription = null;
            this.CB_FromLayer.AccessibleName = null;
            resources.ApplyResources(this.CB_FromLayer, "CB_FromLayer");
            this.CB_FromLayer.BackgroundImage = null;
            this.CB_FromLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_FromLayer.Font = null;
            this.CB_FromLayer.FormattingEnabled = true;
            this.CB_FromLayer.Name = "CB_FromLayer";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // ChB_SelFeaturesOnly
            // 
            this.ChB_SelFeaturesOnly.AccessibleDescription = null;
            this.ChB_SelFeaturesOnly.AccessibleName = null;
            resources.ApplyResources(this.ChB_SelFeaturesOnly, "ChB_SelFeaturesOnly");
            this.ChB_SelFeaturesOnly.BackgroundImage = null;
            this.ChB_SelFeaturesOnly.Font = null;
            this.ChB_SelFeaturesOnly.Name = "ChB_SelFeaturesOnly";
            this.ChB_SelFeaturesOnly.UseVisualStyleBackColor = true;
            // 
            // CB_ClippingLayer
            // 
            this.CB_ClippingLayer.AccessibleDescription = null;
            this.CB_ClippingLayer.AccessibleName = null;
            resources.ApplyResources(this.CB_ClippingLayer, "CB_ClippingLayer");
            this.CB_ClippingLayer.BackgroundImage = null;
            this.CB_ClippingLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ClippingLayer.Font = null;
            this.CB_ClippingLayer.FormattingEnabled = true;
            this.CB_ClippingLayer.Name = "CB_ClippingLayer";
            this.CB_ClippingLayer.SelectedIndexChanged += new System.EventHandler(this.CB_ClippingLayer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // B_Apply
            // 
            this.B_Apply.AccessibleDescription = null;
            this.B_Apply.AccessibleName = null;
            resources.ApplyResources(this.B_Apply, "B_Apply");
            this.B_Apply.BackgroundImage = null;
            this.B_Apply.Font = null;
            this.B_Apply.Name = "B_Apply";
            this.B_Apply.UseVisualStyleBackColor = true;
            this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
            // 
            // frmClipping
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Apply);
            this.Controls.Add(this.ChB_SelFeaturesOnly);
            this.Controls.Add(this.CB_ClippingLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CB_FromLayer);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClipping";
            this.Load += new System.EventHandler(this.frmClipping_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_FromLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChB_SelFeaturesOnly;
        private System.Windows.Forms.ComboBox CB_ClippingLayer;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button B_Apply;
    }
}