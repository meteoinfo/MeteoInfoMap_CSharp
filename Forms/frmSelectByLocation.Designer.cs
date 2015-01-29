namespace MeteoInfo.Forms
{
    partial class frmSelectByLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectByLocation));
            this.label1 = new System.Windows.Forms.Label();
            this.CB_FromLayer = new System.Windows.Forms.ComboBox();
            this.CB_SelType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_RelatedLayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.B_Select = new System.Windows.Forms.Button();
            this.B_Clear = new System.Windows.Forms.Button();
            this.ChB_SelFeaturesOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
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
            // CB_SelType
            // 
            this.CB_SelType.AccessibleDescription = null;
            this.CB_SelType.AccessibleName = null;
            resources.ApplyResources(this.CB_SelType, "CB_SelType");
            this.CB_SelType.BackgroundImage = null;
            this.CB_SelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_SelType.Font = null;
            this.CB_SelType.FormattingEnabled = true;
            this.CB_SelType.Name = "CB_SelType";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // CB_RelatedLayer
            // 
            this.CB_RelatedLayer.AccessibleDescription = null;
            this.CB_RelatedLayer.AccessibleName = null;
            resources.ApplyResources(this.CB_RelatedLayer, "CB_RelatedLayer");
            this.CB_RelatedLayer.BackgroundImage = null;
            this.CB_RelatedLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_RelatedLayer.Font = null;
            this.CB_RelatedLayer.FormattingEnabled = true;
            this.CB_RelatedLayer.Name = "CB_RelatedLayer";
            this.CB_RelatedLayer.SelectedIndexChanged += new System.EventHandler(this.CB_RelatedLayer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // B_Select
            // 
            this.B_Select.AccessibleDescription = null;
            this.B_Select.AccessibleName = null;
            resources.ApplyResources(this.B_Select, "B_Select");
            this.B_Select.BackgroundImage = null;
            this.B_Select.Font = null;
            this.B_Select.Name = "B_Select";
            this.B_Select.UseVisualStyleBackColor = true;
            this.B_Select.Click += new System.EventHandler(this.B_Select_Click);
            // 
            // B_Clear
            // 
            this.B_Clear.AccessibleDescription = null;
            this.B_Clear.AccessibleName = null;
            resources.ApplyResources(this.B_Clear, "B_Clear");
            this.B_Clear.BackgroundImage = null;
            this.B_Clear.Font = null;
            this.B_Clear.Name = "B_Clear";
            this.B_Clear.UseVisualStyleBackColor = true;
            this.B_Clear.Click += new System.EventHandler(this.B_Clear_Click);
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
            // frmSelectByLocation
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.ChB_SelFeaturesOnly);
            this.Controls.Add(this.B_Select);
            this.Controls.Add(this.B_Clear);
            this.Controls.Add(this.CB_RelatedLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CB_SelType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_FromLayer);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectByLocation";
            this.Load += new System.EventHandler(this.frmSelectByLocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_FromLayer;
        private System.Windows.Forms.ComboBox CB_SelType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_RelatedLayer;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button B_Select;
        internal System.Windows.Forms.Button B_Clear;
        private System.Windows.Forms.CheckBox ChB_SelFeaturesOnly;
    }
}