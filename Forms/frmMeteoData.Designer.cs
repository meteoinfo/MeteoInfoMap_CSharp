namespace MeteoInfo
{
    partial class frmMeteoData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeteoData));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.B_RemoveAllData = new System.Windows.Forms.Button();
            this.LB_DataFiles = new System.Windows.Forms.ListBox();
            this.Lab_Variable = new System.Windows.Forms.Label();
            this.CHB_ColorVar = new System.Windows.Forms.CheckBox();
            this.Lab_DrawType = new System.Windows.Forms.Label();
            this.CB_DrawType = new System.Windows.Forms.ComboBox();
            this.CB_Variable = new System.Windows.Forms.ComboBox();
            this.Lab_Level = new System.Windows.Forms.Label();
            this.Lab_Time = new System.Windows.Forms.Label();
            this.CB_Level = new System.Windows.Forms.ComboBox();
            this.CB_Time = new System.Windows.Forms.ComboBox();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TSB_Open = new System.Windows.Forms.ToolStripDropDownButton();
            this.TSMI_GrADSData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_GRIBData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_NetCDFData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HDFData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_MICAPSData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HYSPLITData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HYSPLITConc = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HYSPLITParticle = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HYSPLITTraj = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_ARLData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_ASCIIData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_LonLatStations = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SYNOPData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_METARData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_ISHData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMI_ASCIIGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SuferGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_AWXData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_GeoTiffData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_HRITData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSB_DataInfo = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Draw = new System.Windows.Forms.ToolStripButton();
            this.TSB_ViewData = new System.Windows.Forms.ToolStripButton();
            this.TSB_ClearDrawing = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_PreTime = new System.Windows.Forms.ToolStripButton();
            this.TSB_NextTime = new System.Windows.Forms.ToolStripButton();
            this.TSB_Animitor = new System.Windows.Forms.ToolStripButton();
            this.TSB_CreateAnimatorFile = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_DrawSetting = new System.Windows.Forms.ToolStripButton();
            this.TSB_Setting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_SectionPlot = new System.Windows.Forms.ToolStripButton();
            this.TSB_1DPlot = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.B_RemoveAllData);
            this.splitContainer1.Panel1.Controls.Add(this.LB_DataFiles);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.Lab_Variable);
            this.splitContainer1.Panel2.Controls.Add(this.CHB_ColorVar);
            this.splitContainer1.Panel2.Controls.Add(this.Lab_DrawType);
            this.splitContainer1.Panel2.Controls.Add(this.CB_DrawType);
            this.splitContainer1.Panel2.Controls.Add(this.CB_Variable);
            this.splitContainer1.Panel2.Controls.Add(this.Lab_Level);
            this.splitContainer1.Panel2.Controls.Add(this.Lab_Time);
            this.splitContainer1.Panel2.Controls.Add(this.CB_Level);
            this.splitContainer1.Panel2.Controls.Add(this.CB_Time);
            // 
            // B_RemoveAllData
            // 
            resources.ApplyResources(this.B_RemoveAllData, "B_RemoveAllData");
            this.B_RemoveAllData.Name = "B_RemoveAllData";
            this.B_RemoveAllData.UseVisualStyleBackColor = true;
            this.B_RemoveAllData.Click += new System.EventHandler(this.B_RemoveAllData_Click);
            // 
            // LB_DataFiles
            // 
            resources.ApplyResources(this.LB_DataFiles, "LB_DataFiles");
            this.LB_DataFiles.FormattingEnabled = true;
            this.LB_DataFiles.Name = "LB_DataFiles";
            this.LB_DataFiles.SelectedIndexChanged += new System.EventHandler(this.LB_DataFiles_SelectedIndexChanged);
            this.LB_DataFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LB_DataFiles_KeyDown);
            this.LB_DataFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LB_DataFiles_MouseDown);
            // 
            // Lab_Variable
            // 
            resources.ApplyResources(this.Lab_Variable, "Lab_Variable");
            this.Lab_Variable.Name = "Lab_Variable";
            // 
            // CHB_ColorVar
            // 
            resources.ApplyResources(this.CHB_ColorVar, "CHB_ColorVar");
            this.CHB_ColorVar.Name = "CHB_ColorVar";
            this.CHB_ColorVar.UseVisualStyleBackColor = true;
            // 
            // Lab_DrawType
            // 
            resources.ApplyResources(this.Lab_DrawType, "Lab_DrawType");
            this.Lab_DrawType.Name = "Lab_DrawType";
            // 
            // CB_DrawType
            // 
            resources.ApplyResources(this.CB_DrawType, "CB_DrawType");
            this.CB_DrawType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_DrawType.FormattingEnabled = true;
            this.CB_DrawType.Name = "CB_DrawType";
            this.CB_DrawType.SelectedIndexChanged += new System.EventHandler(this.CB_DrawType_SelectedIndexChanged_1);
            // 
            // CB_Variable
            // 
            resources.ApplyResources(this.CB_Variable, "CB_Variable");
            this.CB_Variable.FormattingEnabled = true;
            this.CB_Variable.Name = "CB_Variable";
            this.CB_Variable.SelectedIndexChanged += new System.EventHandler(this.CB_Variable_SelectedIndexChanged);
            // 
            // Lab_Level
            // 
            resources.ApplyResources(this.Lab_Level, "Lab_Level");
            this.Lab_Level.Name = "Lab_Level";
            // 
            // Lab_Time
            // 
            resources.ApplyResources(this.Lab_Time, "Lab_Time");
            this.Lab_Time.Name = "Lab_Time";
            // 
            // CB_Level
            // 
            resources.ApplyResources(this.CB_Level, "CB_Level");
            this.CB_Level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Level.FormattingEnabled = true;
            this.CB_Level.Name = "CB_Level";
            this.CB_Level.SelectedIndexChanged += new System.EventHandler(this.CB_Level_SelectedIndexChanged);
            // 
            // CB_Time
            // 
            resources.ApplyResources(this.CB_Time, "CB_Time");
            this.CB_Time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Time.FormattingEnabled = true;
            this.CB_Time.Name = "CB_Time";
            this.CB_Time.SelectedIndexChanged += new System.EventHandler(this.CB_Time_SelectedIndexChanged);
            // 
            // BottomToolStripPanel
            // 
            resources.ApplyResources(this.BottomToolStripPanel, "BottomToolStripPanel");
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // TopToolStripPanel
            // 
            resources.ApplyResources(this.TopToolStripPanel, "TopToolStripPanel");
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // RightToolStripPanel
            // 
            resources.ApplyResources(this.RightToolStripPanel, "RightToolStripPanel");
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // LeftToolStripPanel
            // 
            resources.ApplyResources(this.LeftToolStripPanel, "LeftToolStripPanel");
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // ContentPanel
            // 
            resources.ApplyResources(this.ContentPanel, "ContentPanel");
            // 
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_Open,
            this.TSB_DataInfo,
            this.ToolStripSeparator1,
            this.TSB_Draw,
            this.TSB_ViewData,
            this.TSB_ClearDrawing,
            this.ToolStripSeparator3,
            this.TSB_PreTime,
            this.TSB_NextTime,
            this.TSB_Animitor,
            this.TSB_CreateAnimatorFile,
            this.ToolStripSeparator2,
            this.TSB_DrawSetting,
            this.TSB_Setting,
            this.toolStripSeparator4,
            this.TSB_SectionPlot,
            this.TSB_1DPlot});
            this.toolStrip2.Name = "toolStrip2";
            // 
            // TSB_Open
            // 
            resources.ApplyResources(this.TSB_Open, "TSB_Open");
            this.TSB_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_GrADSData,
            this.TSMI_GRIBData,
            this.TSMI_NetCDFData,
            this.TSMI_HDFData,
            this.TSMI_MICAPSData,
            this.TSMI_HYSPLITData,
            this.TSMI_ARLData,
            this.TSMI_ASCIIData,
            this.TSMI_AWXData,
            this.TSMI_GeoTiffData,
            this.TSMI_HRITData});
            this.TSB_Open.Name = "TSB_Open";
            // 
            // TSMI_GrADSData
            // 
            resources.ApplyResources(this.TSMI_GrADSData, "TSMI_GrADSData");
            this.TSMI_GrADSData.Name = "TSMI_GrADSData";
            this.TSMI_GrADSData.Click += new System.EventHandler(this.TSMI_GrADSData_Click);
            // 
            // TSMI_GRIBData
            // 
            resources.ApplyResources(this.TSMI_GRIBData, "TSMI_GRIBData");
            this.TSMI_GRIBData.Name = "TSMI_GRIBData";
            this.TSMI_GRIBData.Click += new System.EventHandler(this.TSMI_GRIBData_Click);
            // 
            // TSMI_NetCDFData
            // 
            resources.ApplyResources(this.TSMI_NetCDFData, "TSMI_NetCDFData");
            this.TSMI_NetCDFData.Name = "TSMI_NetCDFData";
            this.TSMI_NetCDFData.Click += new System.EventHandler(this.TSMI_NetCDFData_Click);
            // 
            // TSMI_HDFData
            // 
            resources.ApplyResources(this.TSMI_HDFData, "TSMI_HDFData");
            this.TSMI_HDFData.Name = "TSMI_HDFData";
            this.TSMI_HDFData.Click += new System.EventHandler(this.TSMI_HDFData_Click);
            // 
            // TSMI_MICAPSData
            // 
            resources.ApplyResources(this.TSMI_MICAPSData, "TSMI_MICAPSData");
            this.TSMI_MICAPSData.Name = "TSMI_MICAPSData";
            this.TSMI_MICAPSData.Click += new System.EventHandler(this.TSMI_MICAPSData_Click);
            // 
            // TSMI_HYSPLITData
            // 
            resources.ApplyResources(this.TSMI_HYSPLITData, "TSMI_HYSPLITData");
            this.TSMI_HYSPLITData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_HYSPLITConc,
            this.TSMI_HYSPLITParticle,
            this.TSMI_HYSPLITTraj});
            this.TSMI_HYSPLITData.Name = "TSMI_HYSPLITData";
            // 
            // TSMI_HYSPLITConc
            // 
            resources.ApplyResources(this.TSMI_HYSPLITConc, "TSMI_HYSPLITConc");
            this.TSMI_HYSPLITConc.Name = "TSMI_HYSPLITConc";
            this.TSMI_HYSPLITConc.Click += new System.EventHandler(this.TSMI_HYSPLITConc_Click);
            // 
            // TSMI_HYSPLITParticle
            // 
            resources.ApplyResources(this.TSMI_HYSPLITParticle, "TSMI_HYSPLITParticle");
            this.TSMI_HYSPLITParticle.Name = "TSMI_HYSPLITParticle";
            this.TSMI_HYSPLITParticle.Click += new System.EventHandler(this.TSMI_HYSPLITParticle_Click);
            // 
            // TSMI_HYSPLITTraj
            // 
            resources.ApplyResources(this.TSMI_HYSPLITTraj, "TSMI_HYSPLITTraj");
            this.TSMI_HYSPLITTraj.Name = "TSMI_HYSPLITTraj";
            this.TSMI_HYSPLITTraj.Click += new System.EventHandler(this.TSMI_HYSPLITTraj_Click);
            // 
            // TSMI_ARLData
            // 
            resources.ApplyResources(this.TSMI_ARLData, "TSMI_ARLData");
            this.TSMI_ARLData.Name = "TSMI_ARLData";
            this.TSMI_ARLData.Click += new System.EventHandler(this.TSMI_ARLData_Click);
            // 
            // TSMI_ASCIIData
            // 
            resources.ApplyResources(this.TSMI_ASCIIData, "TSMI_ASCIIData");
            this.TSMI_ASCIIData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_LonLatStations,
            this.TSMI_SYNOPData,
            this.TSMI_METARData,
            this.TSMI_ISHData,
            this.toolStripSeparator5,
            this.TSMI_ASCIIGrid,
            this.TSMI_SuferGrid});
            this.TSMI_ASCIIData.Name = "TSMI_ASCIIData";
            // 
            // TSMI_LonLatStations
            // 
            resources.ApplyResources(this.TSMI_LonLatStations, "TSMI_LonLatStations");
            this.TSMI_LonLatStations.Name = "TSMI_LonLatStations";
            this.TSMI_LonLatStations.Click += new System.EventHandler(this.TSMI_LonLatStations_Click);
            // 
            // TSMI_SYNOPData
            // 
            resources.ApplyResources(this.TSMI_SYNOPData, "TSMI_SYNOPData");
            this.TSMI_SYNOPData.Name = "TSMI_SYNOPData";
            this.TSMI_SYNOPData.Click += new System.EventHandler(this.TSMI_SYNOPData_Click);
            // 
            // TSMI_METARData
            // 
            resources.ApplyResources(this.TSMI_METARData, "TSMI_METARData");
            this.TSMI_METARData.Name = "TSMI_METARData";
            this.TSMI_METARData.Click += new System.EventHandler(this.TSMI_METARData_Click_1);
            // 
            // TSMI_ISHData
            // 
            resources.ApplyResources(this.TSMI_ISHData, "TSMI_ISHData");
            this.TSMI_ISHData.Name = "TSMI_ISHData";
            this.TSMI_ISHData.Click += new System.EventHandler(this.TSMI_ISHData_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // TSMI_ASCIIGrid
            // 
            resources.ApplyResources(this.TSMI_ASCIIGrid, "TSMI_ASCIIGrid");
            this.TSMI_ASCIIGrid.Name = "TSMI_ASCIIGrid";
            this.TSMI_ASCIIGrid.Click += new System.EventHandler(this.TSMI_ASCIIGrid_Click);
            // 
            // TSMI_SuferGrid
            // 
            resources.ApplyResources(this.TSMI_SuferGrid, "TSMI_SuferGrid");
            this.TSMI_SuferGrid.Name = "TSMI_SuferGrid";
            this.TSMI_SuferGrid.Click += new System.EventHandler(this.TSMI_SuferGrid_Click);
            // 
            // TSMI_AWXData
            // 
            resources.ApplyResources(this.TSMI_AWXData, "TSMI_AWXData");
            this.TSMI_AWXData.Name = "TSMI_AWXData";
            this.TSMI_AWXData.Click += new System.EventHandler(this.TSMI_AWXData_Click);
            // 
            // TSMI_GeoTiffData
            // 
            resources.ApplyResources(this.TSMI_GeoTiffData, "TSMI_GeoTiffData");
            this.TSMI_GeoTiffData.Name = "TSMI_GeoTiffData";
            this.TSMI_GeoTiffData.Click += new System.EventHandler(this.TSMI_GeoTiffData_Click);
            // 
            // TSMI_HRITData
            // 
            resources.ApplyResources(this.TSMI_HRITData, "TSMI_HRITData");
            this.TSMI_HRITData.Name = "TSMI_HRITData";
            this.TSMI_HRITData.Click += new System.EventHandler(this.TSMI_HRITData_Click);
            // 
            // TSB_DataInfo
            // 
            resources.ApplyResources(this.TSB_DataInfo, "TSB_DataInfo");
            this.TSB_DataInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_DataInfo.Name = "TSB_DataInfo";
            this.TSB_DataInfo.Click += new System.EventHandler(this.TSB_DataInfo_Click);
            // 
            // ToolStripSeparator1
            // 
            resources.ApplyResources(this.ToolStripSeparator1, "ToolStripSeparator1");
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            // 
            // TSB_Draw
            // 
            resources.ApplyResources(this.TSB_Draw, "TSB_Draw");
            this.TSB_Draw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Draw.Name = "TSB_Draw";
            this.TSB_Draw.Click += new System.EventHandler(this.TSB_Draw_Click);
            // 
            // TSB_ViewData
            // 
            resources.ApplyResources(this.TSB_ViewData, "TSB_ViewData");
            this.TSB_ViewData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_ViewData.Name = "TSB_ViewData";
            this.TSB_ViewData.Click += new System.EventHandler(this.TSB_ViewData_Click);
            // 
            // TSB_ClearDrawing
            // 
            resources.ApplyResources(this.TSB_ClearDrawing, "TSB_ClearDrawing");
            this.TSB_ClearDrawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_ClearDrawing.Name = "TSB_ClearDrawing";
            this.TSB_ClearDrawing.Click += new System.EventHandler(this.TSB_ClearDrawing_Click);
            // 
            // ToolStripSeparator3
            // 
            resources.ApplyResources(this.ToolStripSeparator3, "ToolStripSeparator3");
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            // 
            // TSB_PreTime
            // 
            resources.ApplyResources(this.TSB_PreTime, "TSB_PreTime");
            this.TSB_PreTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_PreTime.Name = "TSB_PreTime";
            this.TSB_PreTime.Click += new System.EventHandler(this.TSB_PreTime_Click);
            // 
            // TSB_NextTime
            // 
            resources.ApplyResources(this.TSB_NextTime, "TSB_NextTime");
            this.TSB_NextTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_NextTime.Name = "TSB_NextTime";
            this.TSB_NextTime.Click += new System.EventHandler(this.TSB_NextTime_Click);
            // 
            // TSB_Animitor
            // 
            resources.ApplyResources(this.TSB_Animitor, "TSB_Animitor");
            this.TSB_Animitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Animitor.Name = "TSB_Animitor";
            this.TSB_Animitor.Click += new System.EventHandler(this.TSB_Animitor_Click);
            // 
            // TSB_CreateAnimatorFile
            // 
            resources.ApplyResources(this.TSB_CreateAnimatorFile, "TSB_CreateAnimatorFile");
            this.TSB_CreateAnimatorFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_CreateAnimatorFile.Name = "TSB_CreateAnimatorFile";
            this.TSB_CreateAnimatorFile.Click += new System.EventHandler(this.TSB_CreateAnimatorFile_Click);
            // 
            // ToolStripSeparator2
            // 
            resources.ApplyResources(this.ToolStripSeparator2, "ToolStripSeparator2");
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            // 
            // TSB_DrawSetting
            // 
            resources.ApplyResources(this.TSB_DrawSetting, "TSB_DrawSetting");
            this.TSB_DrawSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_DrawSetting.Name = "TSB_DrawSetting";
            this.TSB_DrawSetting.Click += new System.EventHandler(this.TSB_DrawSetting_Click);
            // 
            // TSB_Setting
            // 
            resources.ApplyResources(this.TSB_Setting, "TSB_Setting");
            this.TSB_Setting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_Setting.Name = "TSB_Setting";
            this.TSB_Setting.Click += new System.EventHandler(this.TSB_Setting_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // TSB_SectionPlot
            // 
            resources.ApplyResources(this.TSB_SectionPlot, "TSB_SectionPlot");
            this.TSB_SectionPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_SectionPlot.Name = "TSB_SectionPlot";
            this.TSB_SectionPlot.Click += new System.EventHandler(this.TSB_ProfilePlot_Click);
            // 
            // TSB_1DPlot
            // 
            resources.ApplyResources(this.TSB_1DPlot, "TSB_1DPlot");
            this.TSB_1DPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSB_1DPlot.Name = "TSB_1DPlot";
            this.TSB_1DPlot.Click += new System.EventHandler(this.TSB_1DPlot_Click);
            // 
            // frmMeteoData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeteoData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMeteoData_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMeteoData_FormClosed);
            this.Load += new System.EventHandler(this.frmMeteoData_Load);
            this.MouseEnter += new System.EventHandler(this.frmMeteoData_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.frmMeteoData_MouseLeave);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripDropDownButton TSB_Open;
        private System.Windows.Forms.ToolStripMenuItem TSMI_GrADSData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_MICAPSData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HYSPLITData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ARLData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_NetCDFData;
        internal System.Windows.Forms.ToolStripButton TSB_DataInfo;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton TSB_Draw;
        internal System.Windows.Forms.ToolStripButton TSB_ClearDrawing;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton TSB_PreTime;
        internal System.Windows.Forms.ToolStripButton TSB_NextTime;
        internal System.Windows.Forms.ToolStripButton TSB_Animitor;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton TSB_DrawSetting;
        private System.Windows.Forms.ToolStripButton TSB_Setting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton TSB_SectionPlot;
        private System.Windows.Forms.ToolStripButton TSB_1DPlot;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HYSPLITConc;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HYSPLITParticle;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HYSPLITTraj;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ASCIIData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_LonLatStations;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ISHData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_METARData;
        private System.Windows.Forms.ToolStripButton TSB_CreateAnimatorFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ASCIIGrid;
        private System.Windows.Forms.ToolStripMenuItem TSMI_SuferGrid;
        private System.Windows.Forms.ToolStripButton TSB_ViewData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_GRIBData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HDFData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_AWXData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_SYNOPData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_HRITData;
        private System.Windows.Forms.ListBox LB_DataFiles;
        private System.Windows.Forms.Button B_RemoveAllData;
        private System.Windows.Forms.ToolStripMenuItem TSMI_GeoTiffData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label Lab_Variable;
        private System.Windows.Forms.CheckBox CHB_ColorVar;
        private System.Windows.Forms.Label Lab_DrawType;
        internal System.Windows.Forms.ComboBox CB_DrawType;
        private System.Windows.Forms.ComboBox CB_Variable;
        private System.Windows.Forms.Label Lab_Level;
        private System.Windows.Forms.Label Lab_Time;
        internal System.Windows.Forms.ComboBox CB_Level;
        internal System.Windows.Forms.ComboBox CB_Time;

    }
}