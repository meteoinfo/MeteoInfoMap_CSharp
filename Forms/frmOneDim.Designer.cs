namespace MeteoInfo.Forms
{
    partial class frmOneDim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOneDim));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_DataInfo = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Draw = new System.Windows.Forms.ToolStripButton();
            this.TSB_ClearDrawing = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_PreTime = new System.Windows.Forms.ToolStripButton();
            this.TSB_NextTime = new System.Windows.Forms.ToolStripButton();
            this.TSB_Animitor = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TB_NewVariable = new System.Windows.Forms.TextBox();
            this.CHB_NewVariable = new System.Windows.Forms.CheckBox();
            this.Lab_PlotDims = new System.Windows.Forms.Label();
            this.CB_PlotDims = new System.Windows.Forms.ComboBox();
            this.GB_Dimentions = new System.Windows.Forms.GroupBox();
            this.CB_Lon2 = new System.Windows.Forms.ComboBox();
            this.CB_Lat2 = new System.Windows.Forms.ComboBox();
            this.CB_Level2 = new System.Windows.Forms.ComboBox();
            this.CB_Time2 = new System.Windows.Forms.ComboBox();
            this.CB_Lon1 = new System.Windows.Forms.ComboBox();
            this.CB_Lat1 = new System.Windows.Forms.ComboBox();
            this.CB_Level1 = new System.Windows.Forms.ComboBox();
            this.CB_Time1 = new System.Windows.Forms.ComboBox();
            this.CHB_Lon = new System.Windows.Forms.CheckBox();
            this.CHB_Lat = new System.Windows.Forms.CheckBox();
            this.CHB_Level = new System.Windows.Forms.CheckBox();
            this.CHB_Time = new System.Windows.Forms.CheckBox();
            this.Lab_DrawType = new System.Windows.Forms.Label();
            this.CB_Vars = new System.Windows.Forms.ComboBox();
            this.CB_DrawType = new System.Windows.Forms.ComboBox();
            this.Lab_Var = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.TSB_ViewData = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GB_Dimentions.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_DataInfo,
            this.ToolStripSeparator1,
            this.TSB_Draw,
            this.TSB_ViewData,
            this.TSB_ClearDrawing,
            this.ToolStripSeparator3,
            this.TSB_PreTime,
            this.TSB_NextTime,
            this.TSB_Animitor});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // TSB_DataInfo
            // 
            this.TSB_DataInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_DataInfo, "TSB_DataInfo");
            this.TSB_DataInfo.Name = "TSB_DataInfo";
            this.TSB_DataInfo.Click += new System.EventHandler(this.TSB_DataInfo_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            resources.ApplyResources(this.ToolStripSeparator1, "ToolStripSeparator1");
            // 
            // TSB_Draw
            // 
            this.TSB_Draw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_Draw, "TSB_Draw");
            this.TSB_Draw.Name = "TSB_Draw";
            this.TSB_Draw.Click += new System.EventHandler(this.TSB_Draw_Click);
            // 
            // TSB_ClearDrawing
            // 
            this.TSB_ClearDrawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_ClearDrawing, "TSB_ClearDrawing");
            this.TSB_ClearDrawing.Name = "TSB_ClearDrawing";
            this.TSB_ClearDrawing.Click += new System.EventHandler(this.TSB_ClearDrawing_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            resources.ApplyResources(this.ToolStripSeparator3, "ToolStripSeparator3");
            // 
            // TSB_PreTime
            // 
            this.TSB_PreTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_PreTime, "TSB_PreTime");
            this.TSB_PreTime.Name = "TSB_PreTime";
            this.TSB_PreTime.Click += new System.EventHandler(this.TSB_PreTime_Click);
            // 
            // TSB_NextTime
            // 
            this.TSB_NextTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_NextTime, "TSB_NextTime");
            this.TSB_NextTime.Name = "TSB_NextTime";
            this.TSB_NextTime.Click += new System.EventHandler(this.TSB_NextTime_Click);
            // 
            // TSB_Animitor
            // 
            this.TSB_Animitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_Animitor, "TSB_Animitor");
            this.TSB_Animitor.Name = "TSB_Animitor";
            this.TSB_Animitor.Click += new System.EventHandler(this.TSB_Animitor_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TB_NewVariable);
            this.panel1.Controls.Add(this.CHB_NewVariable);
            this.panel1.Controls.Add(this.Lab_PlotDims);
            this.panel1.Controls.Add(this.CB_PlotDims);
            this.panel1.Controls.Add(this.GB_Dimentions);
            this.panel1.Controls.Add(this.Lab_DrawType);
            this.panel1.Controls.Add(this.CB_Vars);
            this.panel1.Controls.Add(this.CB_DrawType);
            this.panel1.Controls.Add(this.Lab_Var);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // TB_NewVariable
            // 
            resources.ApplyResources(this.TB_NewVariable, "TB_NewVariable");
            this.TB_NewVariable.Name = "TB_NewVariable";
            // 
            // CHB_NewVariable
            // 
            resources.ApplyResources(this.CHB_NewVariable, "CHB_NewVariable");
            this.CHB_NewVariable.Name = "CHB_NewVariable";
            this.CHB_NewVariable.UseVisualStyleBackColor = true;
            this.CHB_NewVariable.CheckedChanged += new System.EventHandler(this.CHB_NewVariable_CheckedChanged);
            // 
            // Lab_PlotDims
            // 
            resources.ApplyResources(this.Lab_PlotDims, "Lab_PlotDims");
            this.Lab_PlotDims.Name = "Lab_PlotDims";
            // 
            // CB_PlotDims
            // 
            this.CB_PlotDims.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PlotDims.FormattingEnabled = true;
            resources.ApplyResources(this.CB_PlotDims, "CB_PlotDims");
            this.CB_PlotDims.Name = "CB_PlotDims";
            this.CB_PlotDims.SelectedIndexChanged += new System.EventHandler(this.CB_PlotDims_SelectedIndexChanged);
            this.CB_PlotDims.SelectedValueChanged += new System.EventHandler(this.CB_PlotDims_SelectedValueChanged);
            // 
            // GB_Dimentions
            // 
            this.GB_Dimentions.Controls.Add(this.CB_Lon2);
            this.GB_Dimentions.Controls.Add(this.CB_Lat2);
            this.GB_Dimentions.Controls.Add(this.CB_Level2);
            this.GB_Dimentions.Controls.Add(this.CB_Time2);
            this.GB_Dimentions.Controls.Add(this.CB_Lon1);
            this.GB_Dimentions.Controls.Add(this.CB_Lat1);
            this.GB_Dimentions.Controls.Add(this.CB_Level1);
            this.GB_Dimentions.Controls.Add(this.CB_Time1);
            this.GB_Dimentions.Controls.Add(this.CHB_Lon);
            this.GB_Dimentions.Controls.Add(this.CHB_Lat);
            this.GB_Dimentions.Controls.Add(this.CHB_Level);
            this.GB_Dimentions.Controls.Add(this.CHB_Time);
            resources.ApplyResources(this.GB_Dimentions, "GB_Dimentions");
            this.GB_Dimentions.Name = "GB_Dimentions";
            this.GB_Dimentions.TabStop = false;
            // 
            // CB_Lon2
            // 
            this.CB_Lon2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Lon2.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Lon2, "CB_Lon2");
            this.CB_Lon2.Name = "CB_Lon2";
            // 
            // CB_Lat2
            // 
            this.CB_Lat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Lat2.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Lat2, "CB_Lat2");
            this.CB_Lat2.Name = "CB_Lat2";
            // 
            // CB_Level2
            // 
            this.CB_Level2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Level2.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Level2, "CB_Level2");
            this.CB_Level2.Name = "CB_Level2";
            // 
            // CB_Time2
            // 
            this.CB_Time2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Time2.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Time2, "CB_Time2");
            this.CB_Time2.Name = "CB_Time2";
            // 
            // CB_Lon1
            // 
            this.CB_Lon1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Lon1.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Lon1, "CB_Lon1");
            this.CB_Lon1.Name = "CB_Lon1";
            // 
            // CB_Lat1
            // 
            this.CB_Lat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Lat1.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Lat1, "CB_Lat1");
            this.CB_Lat1.Name = "CB_Lat1";
            // 
            // CB_Level1
            // 
            this.CB_Level1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Level1.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Level1, "CB_Level1");
            this.CB_Level1.Name = "CB_Level1";
            // 
            // CB_Time1
            // 
            this.CB_Time1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Time1.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Time1, "CB_Time1");
            this.CB_Time1.Name = "CB_Time1";
            // 
            // CHB_Lon
            // 
            resources.ApplyResources(this.CHB_Lon, "CHB_Lon");
            this.CHB_Lon.Name = "CHB_Lon";
            this.CHB_Lon.UseVisualStyleBackColor = true;
            this.CHB_Lon.CheckedChanged += new System.EventHandler(this.CHB_Lon_CheckedChanged);
            // 
            // CHB_Lat
            // 
            resources.ApplyResources(this.CHB_Lat, "CHB_Lat");
            this.CHB_Lat.Name = "CHB_Lat";
            this.CHB_Lat.UseVisualStyleBackColor = true;
            this.CHB_Lat.CheckedChanged += new System.EventHandler(this.CHB_Lat_CheckedChanged);
            // 
            // CHB_Level
            // 
            resources.ApplyResources(this.CHB_Level, "CHB_Level");
            this.CHB_Level.Name = "CHB_Level";
            this.CHB_Level.UseVisualStyleBackColor = true;
            this.CHB_Level.CheckedChanged += new System.EventHandler(this.CHB_Level_CheckedChanged);
            // 
            // CHB_Time
            // 
            resources.ApplyResources(this.CHB_Time, "CHB_Time");
            this.CHB_Time.Name = "CHB_Time";
            this.CHB_Time.UseVisualStyleBackColor = true;
            this.CHB_Time.CheckedChanged += new System.EventHandler(this.CHB_Time_CheckedChanged);
            // 
            // Lab_DrawType
            // 
            resources.ApplyResources(this.Lab_DrawType, "Lab_DrawType");
            this.Lab_DrawType.Name = "Lab_DrawType";
            // 
            // CB_Vars
            // 
            this.CB_Vars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Vars.FormattingEnabled = true;
            resources.ApplyResources(this.CB_Vars, "CB_Vars");
            this.CB_Vars.Name = "CB_Vars";
            this.CB_Vars.SelectedIndexChanged += new System.EventHandler(this.CB_Vars_SelectedIndexChanged);
            // 
            // CB_DrawType
            // 
            this.CB_DrawType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_DrawType.FormattingEnabled = true;
            resources.ApplyResources(this.CB_DrawType, "CB_DrawType");
            this.CB_DrawType.Name = "CB_DrawType";
            this.CB_DrawType.SelectedIndexChanged += new System.EventHandler(this.CB_DrawType_SelectedIndexChanged);
            // 
            // Lab_Var
            // 
            resources.ApplyResources(this.Lab_Var, "Lab_Var");
            this.Lab_Var.Name = "Lab_Var";
            // 
            // zedGraphControl1
            // 
            resources.ApplyResources(this.zedGraphControl1, "zedGraphControl1");
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            // 
            // TSB_ViewData
            // 
            this.TSB_ViewData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_ViewData, "TSB_ViewData");
            this.TSB_ViewData.Name = "TSB_ViewData";
            this.TSB_ViewData.Click += new System.EventHandler(this.TSB_ViewData_Click);
            // 
            // frmOneDim
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmOneDim";
            this.Load += new System.EventHandler(this.frmOneDim_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GB_Dimentions.ResumeLayout(false);
            this.GB_Dimentions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Lab_PlotDims;
        private System.Windows.Forms.ComboBox CB_PlotDims;
        internal System.Windows.Forms.GroupBox GB_Dimentions;
        private System.Windows.Forms.ComboBox CB_Lon2;
        private System.Windows.Forms.ComboBox CB_Lat2;
        private System.Windows.Forms.ComboBox CB_Level2;
        private System.Windows.Forms.ComboBox CB_Time2;
        private System.Windows.Forms.ComboBox CB_Lon1;
        private System.Windows.Forms.ComboBox CB_Lat1;
        private System.Windows.Forms.ComboBox CB_Level1;
        private System.Windows.Forms.ComboBox CB_Time1;
        private System.Windows.Forms.CheckBox CHB_Lon;
        private System.Windows.Forms.CheckBox CHB_Lat;
        private System.Windows.Forms.CheckBox CHB_Level;
        private System.Windows.Forms.CheckBox CHB_Time;
        private System.Windows.Forms.Label Lab_DrawType;
        private System.Windows.Forms.ComboBox CB_Vars;
        private System.Windows.Forms.ComboBox CB_DrawType;
        private System.Windows.Forms.Label Lab_Var;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        internal System.Windows.Forms.ToolStripButton TSB_DataInfo;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton TSB_Draw;
        internal System.Windows.Forms.ToolStripButton TSB_ClearDrawing;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton TSB_PreTime;
        internal System.Windows.Forms.ToolStripButton TSB_NextTime;
        internal System.Windows.Forms.ToolStripButton TSB_Animitor;
        internal System.Windows.Forms.TextBox TB_NewVariable;
        internal System.Windows.Forms.CheckBox CHB_NewVariable;
        private System.Windows.Forms.ToolStripButton TSB_ViewData;
    }
}