namespace MeteoInfo.Forms
{
    partial class frmPluginManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPluginManager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.B_UpdateList = new System.Windows.Forms.Button();
            this.CLB_Plugins = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_PluginDetail = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.B_UpdateList);
            this.groupBox1.Controls.Add(this.CLB_Plugins);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // B_UpdateList
            // 
            this.B_UpdateList.AccessibleDescription = null;
            this.B_UpdateList.AccessibleName = null;
            resources.ApplyResources(this.B_UpdateList, "B_UpdateList");
            this.B_UpdateList.BackgroundImage = null;
            this.B_UpdateList.Font = null;
            this.B_UpdateList.Name = "B_UpdateList";
            this.B_UpdateList.UseVisualStyleBackColor = true;
            this.B_UpdateList.Click += new System.EventHandler(this.B_UpdateList_Click);
            // 
            // CLB_Plugins
            // 
            this.CLB_Plugins.AccessibleDescription = null;
            this.CLB_Plugins.AccessibleName = null;
            resources.ApplyResources(this.CLB_Plugins, "CLB_Plugins");
            this.CLB_Plugins.BackgroundImage = null;
            this.CLB_Plugins.Font = null;
            this.CLB_Plugins.FormattingEnabled = true;
            this.CLB_Plugins.Name = "CLB_Plugins";
            this.CLB_Plugins.SelectedIndexChanged += new System.EventHandler(this.CLB_Plugins_SelectedIndexChanged);
            this.CLB_Plugins.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CLB_Plugins_ItemCheck);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // TB_PluginDetail
            // 
            this.TB_PluginDetail.AccessibleDescription = null;
            this.TB_PluginDetail.AccessibleName = null;
            resources.ApplyResources(this.TB_PluginDetail, "TB_PluginDetail");
            this.TB_PluginDetail.BackgroundImage = null;
            this.TB_PluginDetail.Font = null;
            this.TB_PluginDetail.Name = "TB_PluginDetail";
            // 
            // frmPluginManager
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.TB_PluginDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Font = null;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPluginManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button B_UpdateList;
        private System.Windows.Forms.CheckedListBox CLB_Plugins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_PluginDetail;
    }
}