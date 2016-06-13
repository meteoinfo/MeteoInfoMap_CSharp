using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using MeteoInfoC.Map;
using MeteoInfoC.Layout;
using MeteoInfoC.Legend;
using MeteoInfoC.Data;
using MeteoInfoC.Data.MeteoData;
using MeteoInfoC.Drawing;
using MeteoInfoC.Global;
using MeteoInfoC.Layer;
using MeteoInfoC.Shape;
using MeteoInfoC.Projections;
//using DotSpatial.Projections;

namespace MeteoInfo.Forms
{   
    public partial class frmSectionPlot : Form
    {
        #region Variables
        LayersLegend G_LayersLegend = new LayersLegend();
        MapLayout _mapLayout = new MapLayout();

        public static frmSectionPlot pCurrenWin = null;
        public LegendScheme _legendScheme = null;
        public static int _lastAddedLayerHandle;
        public static ArrayList _legendPolygons = new ArrayList();
        public static XYScreenSet _XYScreenSet = 
            new XYScreenSet(0, 360, 0, 180, 80, 20, 50, 70);
        public static frmIdentifer G_FrmIdentifer = null;

        MeteoDataInfo _meteoDataInfo = new MeteoDataInfo();        
        bool _useSameLegendScheme = false;
        string m_Title = "";
        ToolStripButton _currentTool;

        private GridData _gridData = null;
        double[] _CValues, _X, _Y;
        Color[] _colors;
        //ArrayList _borders = new ArrayList();        
        //double[,] m_GridData;
        //double[,] m_DiscreteData;
        DrawType2D _2DDrawType = new DrawType2D();        
        //ArrayList m_GrADSSTData = new ArrayList();
        //Boolean m_UseSameGridInterSet = false;
        Boolean m_HasNoData;
        InterpolationSetting m_GridInterp = new InterpolationSetting();        
        double m_MinData, m_MaxData;
        //double _XDelt, _YDelt;              
        int _XNum, _YNum;
        private int _StrmDensity = 4; 
                
        PlotDimension _plotDimension = new PlotDimension();
        List<string> _XGridStrs = new List<string>();
        List<string> _YGridStrs = new List<string>();
        private Boolean _isSamePlotDim = false;

        #endregion

        #region Constructor
        public frmSectionPlot(MeteoDataInfo aDataInfo)
        {
            InitializeComponent();

            _meteoDataInfo = aDataInfo;
            pCurrenWin = this;
            TB_NewVariable.Visible = false;
        }

        #endregion

        #region Methods

        #region Loading
        private void frmProfilePlot_Load(object sender, EventArgs e)
        {
            //Set size
            this.Width = 1020;
            this.Height = 600;

            ////Set map view control
            //_mapLayout.ActiveMapFrame.MapView.IsGeoMap = false;
            //_mapLayout.ActiveMapFrame.MapView.SmoothingMode = SmoothingMode.AntiAlias;
            //_mapLayout.DefaultLayoutMap.MapFrame.GridLineColor = Color.DarkGray;
            //_mapLayout.DefaultLegend.LegendStyle = LegendStyles.Normal; 
            //G_LayersLegend.ActiveMapFrame.MapView = _mapLayout.ActiveMapFrame.MapView;

            //Set 2D panel
            SetMapLayerPanel();
            G_LayersLegend.MapFrames[0].LayoutBounds = new Rectangle(40, 36, 606, 420);
            G_LayersLegend.MapFrames[0].MapView.IsGeoMap = false;
            G_LayersLegend.MapFrames[0].IsFireMapViewUpdate = true;
            _mapLayout.MapFrames = G_LayersLegend.MapFrames;
            _mapLayout.AddElement(new LayoutMap(_mapLayout.MapFrames[0]));
            LayoutLegend legend = _mapLayout.AddLegend(660, 100);
            legend.LegendStyle = LegendStyles.Bar_Vertical;
            legend.Width = 36;
            legend.Height = 295;
            _mapLayout.AddText("MeteoInfo: Meteorological Data Infomation System", 320, 20);
            _mapLayout.PaintGraphics();
            //_mapLayout.MainTitle.Text = "MeteoInfo: Meteorological Data Infomation System";  

            //Set layout zoom combobox
            TSCB_PageZoom.Items.Clear();
            TSCB_PageZoom.Items.AddRange(new string[] { "20%", "50%", "75%", "100%", "150%", "200%", "300%" });
            TSCB_PageZoom.Text = (_mapLayout.Zoom * 100).ToString() + "%";
            
            //Set label position            
            Lab_PlotDims.Left = CB_PlotDims.Left - Lab_PlotDims.Width;
            Lab_Var.Left = CB_Vars.Left - Lab_Var.Width;
            Lab_DrawType.Left = CB_DrawType.Left - Lab_DrawType.Width;

            //Set map title
            m_Title = Resources.GlobalResource.ResourceManager.GetString("SectionPlot_Title");

            //Set current tool
            TSB_None.PerformClick();

            //Set dimensions
            CB_Lat2.Visible = false;
            CB_Level2.Visible = false;
            CB_Lon2.Visible = false;
            CB_Time2.Visible = false;
            CHB_Time.Enabled = false;
            CHB_Level.Enabled = false;
            CHB_Lon.Enabled = false;
            CHB_Lat.Enabled = false;
            CHB_ColorVar.Visible = false;
            

            UpdateDimensions();                 

            //Set draw type
            CB_DrawType.Items.Clear();
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.HYSPLIT_Particle:
                    CB_DrawType.Items.Add(DrawType2D.Station_Point);
                    break;
                default:
                    CB_DrawType.Items.Add(DrawType2D.Contour);
                    CB_DrawType.Items.Add(DrawType2D.Shaded);
                    CB_DrawType.Items.Add(DrawType2D.Grid_Fill);
                    CB_DrawType.Items.Add(DrawType2D.Grid_Point);
                    CB_DrawType.Items.Add(DrawType2D.Vector);
                    CB_DrawType.Items.Add(DrawType2D.Barb);
                    CB_DrawType.Items.Add(DrawType2D.Streamline);
                    break;
            }            
            CB_DrawType.SelectedIndex = 0;

            if (_meteoDataInfo.IsGridData)
            {
                CLB_Stations.Visible = false;
            }
            else
            {
                CLB_Stations.Visible = true;
                CLB_Stations.CheckOnClick = true;
                CHB_Lat.Visible = false;
                CHB_Lon.Visible = false;
                CB_Lat1.Visible = false;
                CB_Lat2.Visible = false;
                CB_Lon1.Visible = false;
                CB_Lon2.Visible = false;
            }
        }        

        private void SetMapLayerPanel()
        {
            ////Lab_Head           
            //System.Windows.Forms.Label Lab_Head = new System.Windows.Forms.Label();
            //Lab_Head.Dock = DockStyle.Top;
            //Lab_Head.Font = new Font("SimSun", 9F);
            ////Lab_Head.Text = "Layers";
            //Lab_Head.Text = Resources.GlobalResource.ResourceManager.GetString("Lab_Head_Text");
            //Lab_Head.Height = 24;
            //Lab_Head.BackColor = Color.LightGray;

            //Panel PanelLayersHead = new Panel();
            //PanelLayersHead.BorderStyle = BorderStyle.None;            
            //PanelLayersHead.Controls.Add(Lab_Head);
            //PanelLayersHead.Dock = DockStyle.Fill;
            //PanelLayersHead.Height = Lab_Head.Height;
            //PanelLayersHead.BackColor = Lab_Head.BackColor;

            //splitContainer1.Panel2.Controls.Add(Lab_Head);
            //splitContainer1.Panel2.Controls.Add(G_LayersLegend);
            tabControl1.TabPages[1].Controls.Add(G_LayersLegend);
            //G_LayersLegend.Top = splitContainer1.Panel2.Top + 24;
            //G_LayersLegend.Width = splitContainer1.Panel2.Width;
            //G_LayersLegend.Height = splitContainer1.Panel2.Height - 24;
            //G_LayersLegend.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //    | System.Windows.Forms.AnchorStyles.Left)
            //    | System.Windows.Forms.AnchorStyles.Right);
            G_LayersLegend.Dock = DockStyle.Fill;
            //G_LayersLegend.BorderStyle = BorderStyle.FixedSingle;
            G_LayersLegend.Select();
            MapFrame mf = new MapFrame();
            mf.Active = true;
            G_LayersLegend.AddMapFrame(mf);
            //G_LayersLegend.ActiveMapFrameChanged += new EventHandler(ActiveMapFrameChanged);
            G_LayersLegend.MapFramesUpdated += new EventHandler(MapFramesUpdated);

            splitContainer1.Panel1MinSize = GB_Dimentions.Width + 20;
            splitContainer1.SplitterDistance = splitContainer1.Panel1MinSize;

            //Add map layout            
            splitContainer1.Panel2.Controls.Add(_mapLayout);
            _mapLayout.Dock = DockStyle.Fill;
            //_mapLayout.ActiveMapFrame.MapView.MouseDown += new MouseEventHandler(this.MapView_MouseDown);
            //_mapLayout.MouseMove += new MouseEventHandler(this.Layout_MouseMove);
            //_mapLayout.ElementSeleted += new EventHandler(this.Layout_ElementSelected);
            //_mapLayout.ZoomChanged += new EventHandler(this.MapLayout_ZoomChanged);
            _mapLayout.ActiveMapFrameChanged += new EventHandler(Layout_ActiveMapFrameChanged);
            _mapLayout.MapFramesUpdated += new EventHandler(Layout_MapFramesUpdated);
        }

        private void UpdateDimensions()
        {
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:
                    UpdateParasGrADSGrid();
                    break;
                case MeteoDataType.GrADS_Station:
                    UpdateParasGrADSStation();
                    break;
                case MeteoDataType.NetCDF:
                    UpdateParasNetCDFGrid();
                    break;
                case MeteoDataType.ARL_Grid:
                    UpdateParasARLGrid();
                    break;
                case MeteoDataType.HYSPLIT_Conc:
                    UpdateParasHYSPLITConc();
                    break;
                case MeteoDataType.HYSPLIT_Particle :
                    UpdateParasHYSPLITParticle();
                    break;
                case MeteoDataType.MICAPS_1:
                    UpdateParasMICAPS1();
                    break;
                case MeteoDataType.MICAPS_4:
                    UpdateParasMICAPS4();
                    break;  
                case MeteoDataType.GRIB1:
                    UpdateParasGRIB1();
                    break;
                case MeteoDataType.GRIB2:
                    UpdateParasGRIB2();
                    break;
            }            
        }

        private void SetDimensions()
        {                       
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lon:
                    CHB_Lat.Checked = false;
                    CHB_Level.Checked = false;
                    CHB_Time.Checked = true;
                    CHB_Lon.Checked = true;
                    break;
                case PlotDimension.Time_Lat:
                    CHB_Level.Checked = false;
                    CHB_Lon.Checked = false;
                    CHB_Time.Checked = true;
                    CHB_Lat.Checked = true;
                    break;
                case PlotDimension.Level_Time:
                    CHB_Lon.Checked = false;
                    CHB_Lat.Checked = false;
                    CHB_Time.Checked = true;
                    CHB_Level.Checked = true;
                    break;
                case PlotDimension.Level_Lon:
                    CHB_Lat.Checked = false;
                    CHB_Time.Checked = false;
                    CHB_Level.Checked = true;
                    CHB_Lon.Checked = true;
                    break;
                case PlotDimension.Level_Lat:
                    CHB_Time.Checked = false;
                    CHB_Lon.Checked = false;
                    CHB_Level.Checked = true;
                    CHB_Lat.Checked = true;
                    break;
            }
        }

        #endregion

        //private void MapView_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (_currentTool.Text == "Identifer" && e.Button == MouseButtons.Left)
        //    {
        //        double ProjX, ProjY;
        //        ProjX = 0;
        //        ProjY = 0;
        //        Single sX, sY;
        //        sX = e.X;
        //        sY = e.Y;
        //        G_LayersLegend.ActiveMapFrame.MapView.ScreenToProj(ref ProjX, ref ProjY, e.X, e.Y);
        //        if (G_LayersLegend.SelectedNode.NodeType == NodeTypes.LayerNode)
        //        {
        //            LayerNode aLN = (LayerNode)G_LayersLegend.SelectedNode;
        //            MapLayer aMLayer = aLN.MapFrame.MapView.GetLayerFromHandle(aLN.LayerHandle);
        //            if (aMLayer.LayerType != LayerTypes.VectorLayer)
        //                return;

        //            VectorLayer aLayer = (VectorLayer)aMLayer;

        //            int Buffer = 5;
        //            Extent aExtent = new Extent();
        //            G_LayersLegend.ActiveMapFrame.MapView.ScreenToProj(ref ProjX, ref ProjY, sX - Buffer, sY + Buffer);
        //            aExtent.minX = ProjX;
        //            aExtent.minY = ProjY;
        //            G_LayersLegend.ActiveMapFrame.MapView.ScreenToProj(ref ProjX, ref ProjY, sX + Buffer, sY - Buffer);
        //            aExtent.maxX = ProjX;
        //            aExtent.maxY = ProjY;

        //            List<int> SelectedShapes = aLayer.SelectShapes(aExtent);
        //            if (SelectedShapes.Count > 0)
        //            {
        //                //MessageBox.Show(SelectedShapes[0].ToString());
        //                if (G_FrmIdentifer == null)
        //                {
        //                    G_FrmIdentifer = new frmIdentifer();
        //                    G_FrmIdentifer.RefForm = this;
        //                    G_FrmIdentifer.Show(this);
        //                }
        //                else
        //                {
        //                    G_FrmIdentifer.Activate();
        //                }

        //                G_FrmIdentifer.ListView1.Items.Clear();
        //                string fieldStr, valueStr;
        //                int shapeIdx = SelectedShapes[0];

        //                fieldStr = "Index";
        //                valueStr = shapeIdx.ToString();
        //                G_FrmIdentifer.ListView1.Items.Add(fieldStr).SubItems.Add(valueStr);

        //                if (aLayer.ShapeNum > 0)
        //                {
        //                    for (int i = 0; i < aLayer.NumFields; i++)
        //                    {
        //                        fieldStr = aLayer.GetFieldName(i);
        //                        valueStr = aLayer.GetCellValue(i, shapeIdx).ToString();
        //                        G_FrmIdentifer.ListView1.Items.Add(fieldStr).SubItems.Add(valueStr);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void CHB_Time_CheckedChanged(object sender, EventArgs e)
        {
            CB_Time2.Visible = CHB_Time.Checked;
            if (CHB_Time.Checked)
            {
                if (CB_Time1.Items.Count > 0)
                {
                    CB_Time1.SelectedIndex = 0;
                    CB_Time2.SelectedIndex = CB_Time2.Items.Count - 1;
                }
                CB_Time1.Enabled = false;
                CB_Time2.Enabled = false;
            }
            else
            {
                CB_Time1.Enabled = true;
                CB_Time2.Enabled = true;
            }
        }

        private void CHB_Level_CheckedChanged(object sender, EventArgs e)
        {
            CB_Level2.Visible = CHB_Level.Checked;
            if (CHB_Level.Checked)
            {
                if (CB_Level1.Items.Count > 0)
                {
                    CB_Level1.SelectedIndex = 0;
                    CB_Level2.SelectedIndex = CB_Level2.Items.Count - 1;
                }
                CB_Level1.Enabled = false;
                CB_Level2.Enabled = false;
            }
            else
            {
                CB_Level1.Enabled = true;
                CB_Level2.Enabled = true;
            }
        }

        private void CHB_Lat_CheckedChanged(object sender, EventArgs e)
        {
            CB_Lat2.Visible = CHB_Lat.Checked;
            if (CHB_Lat.Checked)
            {
                if (CB_Lat1.Items.Count > 0)
                {
                    CB_Lat1.SelectedIndex = 0;
                }
                CB_Lat1.Enabled = false;
                CB_Lat2.Enabled = false;
            }
            else
            {
                CB_Lat1.Enabled = true;
                CB_Lat2.Enabled = true;
            }
        }

        private void CHB_Lon_CheckedChanged(object sender, EventArgs e)
        {
            CB_Lon2.Visible = CHB_Lon.Checked;
            if (CHB_Lon.Checked)
            {
                if (CB_Lon1.Items.Count > 0)
                {
                    CB_Lon1.SelectedIndex = 0;
                }
                CB_Lon1.Enabled = false;
                CB_Lon2.Enabled = false;
            }
            else
            {
                CB_Lon1.Enabled = true;
                CB_Lon2.Enabled = true;
            }
        }                     

        private void UpdateParasGrADSGrid()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
            
            //Set time
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.TDEF.TNum; i++)
            {
                CB_Time1.Items.Add(aDataInfo.TDEF.times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;                       

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.XDEF.XNum; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;

            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.YDEF.YNum; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Enabled = true;
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.VARDEF.VNum; i++)
            {
                CB_Vars.Items.Add(aDataInfo.VARDEF.Vars[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasGrADSStation()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;            

            //Set times
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.TDEF.TNum; i++)
            {
                CB_Time1.Items.Add(aDataInfo.TDEF.times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Enabled = true;
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.UpperVariables.Count; i++)
            {
                CB_Vars.Items.Add(aDataInfo.UpperVariables[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            //Set stations
            CLB_Stations.Items.Clear();
            List<string> stations = aDataInfo.GetUpperLevelStations(0);
            foreach (string st in stations)
                CLB_Stations.Items.Add(st);
            CLB_Stations.SetItemChecked(0, true);

            UpdateEndDimSet();
        }

        private void UpdateParasMICAPS1()
        {
            int i;
            MICAPS1DataInfo aDataInfo = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add("Station Point");
            CB_DrawType.Items.Add("Contour");
            CB_DrawType.Items.Add("Shaded");
            CB_DrawType.Items.Add("Barb");
            CB_DrawType.Items.Add("Weather Symbol");
            CB_DrawType.SelectedIndex = 0;

            //Set times
            CB_Time1.Items.Clear();
            CB_Time1.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Enabled = true;
            CB_Vars.Items.Clear();
            string[] items = new string[] {"Total Cloud","Wind Direction","Wind Speed","Pressure",
                "PressVar 3h","Weather Past 1","Weather Past 2","Precipitation 6h", "Low Cloud Shape",
                "Low Cloud Amount","Low Cloud Height","Dew Point","Visibility","Weather Now",
                "Temprature","Middle Cloud Shape","High Cloud Shape"};
            for (i = 0; i < items.Length; i++)
            {
                CB_Vars.Items.Add(items[i]);
            }
            if (aDataInfo.hasAllCols)
            {
                CB_Vars.Items.Add("TempVar 24h");
                CB_Vars.Items.Add("PressVar 24h");
            }
            CB_Vars.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            CB_Lat1.Items.Clear();

            UpdateEndDimSet();
        }

        private void UpdateParasMICAPS4()
        {
            int i;
            MICAPS4DataInfo aDataInfo = (MICAPS4DataInfo)_meteoDataInfo.DataInfo;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add("Contour");
            CB_DrawType.Items.Add("Shaded");
            CB_DrawType.Items.Add("Grid Fill");
            CB_DrawType.Items.Add("Grid Point");
            CB_DrawType.SelectedIndex = 0;

            //Set times
            CB_Time1.Items.Clear();
            CB_Time1.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Items.Clear();
            //CB_Vars.Enabled = false;

            //Set level
            CB_Level1.Items.Clear();
            CB_Level1.Items.Add(aDataInfo.level.ToString());
            CB_Level1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.XNum; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;
            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.YNum; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasHYSPLITConc()
        {
            int i;
            HYSPLITConcDataInfo aDataInfo = new HYSPLITConcDataInfo();
            aDataInfo = (HYSPLITConcDataInfo)_meteoDataInfo.DataInfo;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add("Contour");
            CB_DrawType.Items.Add("Shaded");
            CB_DrawType.Items.Add("Grid Fill");
            CB_DrawType.Items.Add("Grid Point");
            CB_DrawType.SelectedIndex = 0;

            //Set times
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.time_num; i++)
            {
                CB_Time1.Items.Add(aDataInfo.sample_start[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.pollutant_num; i++)
            {
                CB_Vars.Items.Add(aDataInfo.pollutants[i]);
            }
            CB_Vars.SelectedIndex = 0;

            //Set levels
            CB_Level1.Items.Clear();
            for (i = 0; i < aDataInfo.level_num; i++)
            {
                CB_Level1.Items.Add(aDataInfo.heights[i].ToString());
            }
            CB_Level1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.X.Length; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;
            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.Y.Length; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasHYSPLITParticle()
        {
            int i;
            HYSPLITParticleInfo aDataInfo = new HYSPLITParticleInfo();
            aDataInfo = (HYSPLITParticleInfo)_meteoDataInfo.DataInfo;
            
            //Set times
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Items.Clear();
            CB_Vars.Items.Add("unDef");
            CB_Vars.SelectedIndex = 0;

            //Set levels
            CB_Level1.Items.Clear();
            CB_Level1.Items.Add("unDef");
            CB_Level1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            CB_Lon1.Items.Add("unDef");
            CB_Lon1.SelectedIndex = 0;
            CB_Lat1.Items.Clear();
            CB_Lat1.Items.Add("unDef");
            CB_Lat1.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasARLGrid()
        {
            int i;
            ARLDataInfo aDataInfo = new ARLDataInfo();
            aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;

            ////Set draw type
            //CB_DrawType.Items.Clear();
            //CB_DrawType.Items.Add("Contour");
            //CB_DrawType.Items.Add("Shaded");
            //CB_DrawType.Items.Add("Grid Fill");
            //CB_DrawType.Items.Add("Grid Point");
            //CB_DrawType.Items.Add("Vector");
            //CB_DrawType.Items.Add("Vector Color");
            //CB_DrawType.SelectedIndex = 0;

            //Set times
            //string timeFormat;
            //if ((aDataInfo.times[1] - aDataInfo.times[0]).Duration().Days >= 1)
            //{
            //    timeFormat = "yyyy-MM-dd";
            //}
            //else if ((aDataInfo.times[1] - aDataInfo.times[0]).Duration().Hours >= 1)
            //{
            //    timeFormat = "yyyy-MM-dd HH";
            //}
            //else
            //{
            //    timeFormat = "yyyy-MM-dd HH:mm";
            //}
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.TimeNum; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));                
            }
            CB_Time1.SelectedIndex = 0;

            ////Set levels
            //CB_Level1.Items.Clear();
            //for (i = 0; i < aDataInfo.levelNum; i++)
            //{
            //    CB_Level1.Items.Add((i + 1).ToString());
            //}
            //CB_Level1.SelectedIndex = 0;
                        
            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.X.Length; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;
            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.Y.Length; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Vars.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasNetCDFGrid()
        {
            int i;
            NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
            aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;

            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            ////Set levels
            //CB_Level1.Items.Clear();            
            //if (aDataInfo.levels != null)
            //{
            //    for (i = 0; i < aDataInfo.levels.Length; i++)
            //    {
            //        CB_Level1.Items.Add(aDataInfo.levels[i].ToString());
            //    }
            //}
            //else
            //{
            //    CB_Level1.Items.Add("Surface");
            //}
            //CB_Level1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.X.Length; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;
            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.Y.Length; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                if (aDataInfo.Variables[i].DimNumber > 1)
                    CB_Vars.Items.Add(aDataInfo.Variables[i].Name);
            }

            CB_Vars.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasGRIB1()
        {
            int i;
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;

            //Set time
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.X.Length; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;

            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.Y.Length; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Enabled = true;
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Vars.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateParasGRIB2()
        {
            int i;
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;

            //Set time
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.X.Length; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;

            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.Y.Length; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.Y[i].ToString());
            }
            CB_Lat1.SelectedIndex = 0;

            //Set vars
            CB_Vars.Enabled = true;
            CB_Vars.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Vars.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            UpdateEndDimSet();
        }

        private void UpdateEndDimSet()
        {
            object[] obj = new object[CB_Time1.Items.Count];
            CB_Time1.Items.CopyTo(obj, 0);
            CB_Time2.Items.Clear();
            CB_Time2.Items.AddRange(obj);
            CB_Time2.SelectedIndex = CB_Time2.Items.Count - 1;
            
            obj = new object[CB_Level1.Items.Count];
            CB_Level1.Items.CopyTo(obj, 0);
            CB_Level2.Items.Clear();
            CB_Level2.Items.AddRange(obj);
            CB_Level2.SelectedIndex = CB_Level2.Items.Count - 1;
            
            obj = new object[CB_Lon1.Items.Count];
            CB_Lon1.Items.CopyTo(obj, 0);
            CB_Lon2.Items.Clear();
            CB_Lon2.Items.AddRange(obj);
            CB_Lon2.SelectedIndex = CB_Lon2.Items.Count - 1;

            obj = new object[CB_Lat1.Items.Count];
            CB_Lat1.Items.CopyTo(obj, 0);
            CB_Lat2.Items.Clear();
            CB_Lat2.Items.AddRange(obj);
            CB_Lat2.SelectedIndex = CB_Lat2.Items.Count - 1;            
        }

        private void UpdateEndDimSetS(ComboBox CB1, ComboBox CB2)
        {
            object[] obj = new object[CB1.Items.Count];
            CB1.Items.CopyTo(obj, 0);
            CB2.Items.Clear();
            CB2.Items.AddRange(obj);
            CB2.SelectedIndex = CB2.Items.Count - 1;            
        }

        private void CB_Vars_SelectedIndexChanged(object sender, EventArgs e)
        {
            _useSameLegendScheme = false;
            //m_UseSameGridInterSet = false;
            _meteoDataInfo.VariableIndex = CB_Vars.SelectedIndex;

            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:
                case MeteoDataType.GrADS_Station:
                    SetGrADSVar();                    
                    break;
                case MeteoDataType.ARL_Grid:
                    SetARLVar();                    
                    break;
                case MeteoDataType.MICAPS_1:
                    CB_Level1.Items.Clear();
                    CB_Level1.Items.Add("Surface");
                    CB_Level1.SelectedIndex = 0;                    
                    break;
                case MeteoDataType.GRIB1:
                    SetGRIB1Var();
                    break;
                case MeteoDataType.GRIB2:
                    SetGRIB2Var();
                    break;
                case MeteoDataType.NetCDF:
                    SetNetCDFVar();
                    break;
            }
            UpdateEndDimSetS(CB_Level1, CB_Level2);

            TSB_Draw.Enabled = true;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;

            //Set plot dimensions
            string pdStr = CB_PlotDims.Text;
            CB_PlotDims.Items.Clear();
            if (_meteoDataInfo.IsGridData)
            {
                if (CB_Level1.Items.Count > 1)
                {
                    CB_PlotDims.Items.Add(PlotDimension.Level_Lat.ToString());
                    CB_PlotDims.Items.Add(PlotDimension.Level_Lon.ToString());
                }
                if (CB_Time1.Items.Count > 1)
                {
                    CB_PlotDims.Items.Add(PlotDimension.Time_Lat.ToString());
                    CB_PlotDims.Items.Add(PlotDimension.Time_Lon.ToString());
                }
            }
            if (CB_Level1.Items.Count > 1 && CB_Time1.Items.Count > 1)
            {
                CB_PlotDims.Items.Add(PlotDimension.Level_Time.ToString());
            }
            //foreach (string pd in Enum.GetNames(typeof(clsMeteoData.PlotDimension)))
            //{
            //    CB_PlotDims.Items.Add(pd);
            //}
            //CB_PlotDims.SelectedIndex = 0;
            if (pdStr != "" && CB_PlotDims.Items.Contains(pdStr))
            {
                _isSamePlotDim = true;
                CB_PlotDims.SelectedItem = pdStr;                
            }
            else
            {
                _isSamePlotDim = false;
                if (CB_PlotDims.Items.Count > 0)
                    CB_PlotDims.SelectedIndex = 0;
                //CB_PlotDims.SelectedValue = CB_PlotDims.Items[0];
            }
            //CB_PlotDims.SelectedItem = clsMeteoData.PlotDimension.Time_Lon.ToString();
        }

        private void SetGrADSVar()
        {
            int aIdx = CB_Vars.SelectedIndex;
            int i;
            int levelIdx = CB_Level1.SelectedIndex;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;

            CB_Level1.Items.Clear();
            if (_meteoDataInfo.DataType == MeteoDataType.GrADS_Grid)
            {
                if (aDataInfo.VARDEF.Vars[aIdx].LevelNum == 0)
                {
                    CB_Level1.Items.Add("Surface");
                    CB_Level1.SelectedIndex = 0;
                }
                else
                {
                    for (i = 0; i < aDataInfo.VARDEF.Vars[aIdx].LevelNum; i++)
                    {
                        CB_Level1.Items.Add(Convert.ToString(aDataInfo.ZDEF.ZLevels[i]));
                    }
                    if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                        CB_Level1.SelectedIndex = levelIdx;
                    else
                        CB_Level1.SelectedIndex = 0;
                }
            }
            else
            {
                for (i = 0; i < aDataInfo.UpperVariables[aIdx].LevelNum; i++)
                    CB_Level1.Items.Add((i + 1).ToString());

                if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                    CB_Level1.SelectedIndex = levelIdx;
                else
                    CB_Level1.SelectedIndex = 0;
            }
        }

        private void SetGRIB1Var()
        {
            int aIdx = CB_Vars.SelectedIndex;
            int i;
            int levelIdx = CB_Level1.SelectedIndex;
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;
            Variable aPar = aDataInfo.Variables[aIdx];

            CB_Level1.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aPar.LevelNum; i++)
                {
                    CB_Level1.Items.Add(aPar.Levels[i]);
                }
                if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                {
                    CB_Level1.SelectedIndex = levelIdx;
                }
                else
                {
                    CB_Level1.SelectedIndex = 0;
                }
            }
        }

        private void SetGRIB2Var()
        {
            int aIdx = CB_Vars.SelectedIndex;
            int i;
            int levelIdx = CB_Level1.SelectedIndex;
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;
            Variable aPar = aDataInfo.Variables[aIdx];

            CB_Level1.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aPar.LevelNum; i++)
                {
                    CB_Level1.Items.Add(aPar.Levels[i]);
                }
                if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                {
                    CB_Level1.SelectedIndex = levelIdx;
                }
                else
                {
                    CB_Level1.SelectedIndex = 0;
                }
            }
        }

        private void SetARLVar()
        {
            int aIdx = CB_Vars.SelectedIndex;
            int i;
            int levelIdx = CB_Level1.SelectedIndex;
            ARLDataInfo aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;

            CB_Level1.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 1)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level1.Items.Add(aDataInfo.levels[aDataInfo.Variables[aIdx].LevelIdxs[i]].ToString());
                }
                if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                {
                    CB_Level1.SelectedIndex = levelIdx;
                }
                else
                {
                    CB_Level1.SelectedIndex = 0;
                }
            }
        }

        private void SetNetCDFVar()
        {
            NetCDFDataInfo aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;
            int aIdx = CB_Vars.SelectedIndex;
            int i;
            int levelIdx = CB_Level1.SelectedIndex;

            string varName = CB_Vars.Text;
            aIdx = aDataInfo.VariableNames.IndexOf(varName);
            CB_Level1.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level1.Items.Add(aDataInfo.Variables[aIdx].Levels[i].ToString());
                }
                if ((levelIdx > -1) && (CB_Level1.Items.Count > levelIdx))
                {
                    CB_Level1.SelectedIndex = levelIdx;
                }
                else
                {
                    CB_Level1.SelectedIndex = 0;
                }
            }
        }

        private void TSB_DataInfo_Click(object sender, EventArgs e)
        {
            frmDataInfo aFrmDI = new frmDataInfo();
            aFrmDI.SetTextBox(_meteoDataInfo.InfoText);
            aFrmDI.Show(this); 
        }

        private void TSB_Draw_Click(object sender, EventArgs e)
        {
            if (CB_PlotDims.Text == "")
            {
                MessageBox.Show("It's not a muti dimension variable: " + CB_Vars.Text + "!", "Error");
                return;
            }
            
            this.Cursor = Cursors.WaitCursor;

            _plotDimension = (PlotDimension)Enum.Parse(typeof(PlotDimension),
                CB_PlotDims.Text, true);

            _meteoDataInfo.DimensionSet = _plotDimension;

            //Get X/Y
            //GetXYCoords();
            GetXYGridStrs();
            _mapLayout.ActiveMapFrame.MapView.XGridStrs = new List<string>(_XGridStrs);
            _mapLayout.ActiveMapFrame.MapView.YGridStrs = new List<string>(_YGridStrs);

            //Draw 2D figure
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:                                   
                //case MeteoDataType.MICAPS_4:                    
                case MeteoDataType.HYSPLIT_Conc:                    
                case MeteoDataType.ARL_Grid:                    
                case MeteoDataType.NetCDF:                    
                case MeteoDataType.GRIB1:
                case MeteoDataType.GRIB2:
                    GetGridData();
                    DrawGrid();
                    break;
                case MeteoDataType.GrADS_Station:
                    _gridData = ((GrADSDataInfo)_meteoDataInfo.DataInfo).GetGridData_Station(CB_Vars.SelectedIndex, CLB_Stations.CheckedItems[0].ToString());
                    DrawGrid();
                    break;
            }

            if (!_useSameLegendScheme)
            {
                MapLayer aLayer = _mapLayout.ActiveMapFrame.MapView.GetLayerFromHandle(_lastAddedLayerHandle);
                if (aLayer != null)
                {
                    VectorLayer sLayer = (VectorLayer)aLayer;
                    if (sLayer.Extent.maxX > sLayer.Extent.minX && sLayer.Extent.maxY > sLayer.Extent.minY)
                    {
                        ZoomToExtent(sLayer.Extent);
                    }
                }
            }

            //TSB_Draw.Enabled = false;
            TSB_DrawSetting.Enabled = true;
            if (!CHB_Time.Checked)
            {
                if (CB_Time1.Items.Count > 1)
                {
                    TSB_NextTime.Enabled = true;
                    TSB_PreTime.Enabled = true;
                    TSB_Animitor.Enabled = true;

                }
            }                                      
                        
            this.Cursor = Cursors.Default; 
        }

        private void GetGridData()
        {
            if (CHB_NewVariable.Checked)
            {
                MathParser mathParser = new MathParser(_meteoDataInfo);
                _gridData = (GridData)mathParser.Evaluate(TB_NewVariable.Text);
            }
            else
                _gridData = _meteoDataInfo.GetGridData(CB_Vars.Text);
        }

        private void DrawGrid()
        {
            //Set X/Y coordinates to integer 1 to x/y number
            SetXYCoords(ref _gridData);
            
            if (CHB_YReverse.Visible && CHB_YReverse.Checked)
            {
                double[,] aGD = (double[,])_gridData.Data.Clone();
                int yn = _gridData.YNum;
                int xn = _gridData.XNum;
                for (int i = 0; i < yn; i++)
                {
                    for (int j = 0; j < xn; j++)
                    {
                        _gridData.Data[i, j] = aGD[yn - i - 1, j];
                    }
                }
            }

            if (!_useSameLegendScheme)
            {
                CreateLegendScheme();
            }
            DrawMeteoMap(true, _legendScheme);
        }

        //private void DrawGrADSGrid()
        //{
        //    m_GridData = GetGrADSGridData(CB_Vars.SelectedIndex);

        //    if (!_useSameLegendScheme)
        //    {
        //        CreateLegendScheme();
        //    }
        //    DrawMeteoMap(true, _legendScheme);
        //}
                
        //private void InterpolateGridData(double[,] S)
        //{
        //    switch (m_GridInterp.GridInterMethodV)
        //    {
        //        case GridInterMethod.IDW_Radius:
        //            m_GridData = ContourDraw.InterpolateDiscreteData_Radius(S,
        //                _X, _Y, m_GridInterp.MinPointNum, m_GridInterp.Radius, _meteoDataInfo.MissingValue).Data;
        //            break;
        //        case GridInterMethod.IDW_Neighbors:
        //            m_GridData = ContourDraw.InterpolateDiscreteData_Neighbor(S, _X, _Y,
        //                m_GridInterp.MinPointNum, _meteoDataInfo.MissingValue).Data;
        //            break;
        //    }
        //}

        private void CreateLegendScheme()
        {
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.UniqueValue, ShapeTypes.Polyline, ref m_HasNoData);
                    break;
                case DrawType2D.Shaded:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref m_HasNoData);
                    break;
                case DrawType2D.Grid_Fill:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref m_HasNoData);
                    break;
                case DrawType2D.Grid_Point:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Point, ref m_HasNoData);
                    break;
                case DrawType2D.Vector:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                            LegendType.GraduatedColor, ShapeTypes.Point, ref m_HasNoData);
                        PointBreak aPB = new PointBreak();
                        for (int i = 0; i < _legendScheme.LegendBreaks.Count; i++)
                        {
                            aPB = (PointBreak)_legendScheme.LegendBreaks[i];
                            aPB.Size = 10;
                            _legendScheme.LegendBreaks[i] = aPB;
                        }
                    }
                    else
                    {
                        _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 10);
                    }
                    break;
                case DrawType2D.Barb:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                            LegendType.GraduatedColor, ShapeTypes.Point, ref m_HasNoData);
                        PointBreak aPB = new PointBreak();
                        for (int i = 0; i < _legendScheme.LegendBreaks.Count; i++)
                        {
                            aPB = (PointBreak)_legendScheme.LegendBreaks[i];
                            aPB.Size = 10;
                            _legendScheme.LegendBreaks[i] = aPB;
                        }
                    }
                    else
                    {
                        _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 10);
                    }
                    break;
                case DrawType2D.Streamline:
                    _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Polyline, Color.Blue, 1);
                    break;
            }
        }

        private void CreateLegendScheme_Station()
        {
            switch (_2DDrawType)
            {
                //case clsMeteoData.DrawType2D.Station_Point:
                //    _legendScheme = CreateLegendSchemeFromDiscreteData(m_DiscreteData,
                //        clsShape.ShapeTypes.Point, ref m_HasNoData);
                //    break;
                case DrawType2D.Contour:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.UniqueValue, ShapeTypes.Polyline, ref m_HasNoData);
                    break;
                case DrawType2D.Shaded:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref m_HasNoData);
                    break;
                case DrawType2D.Barb:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                            LegendType.GraduatedColor, ShapeTypes.Polyline, ref m_HasNoData);
                    }
                    else
                    {
                        _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Polyline, Color.Blue, 1.0F);
                    }
                    break;
                case DrawType2D.Weather_Symbol:
                    _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 15);
                    break;
            }
        }

        private LegendScheme CreateLegendSchemeFromDiscreteData(double[,] discreteData,
            ShapeTypes aST, ref Boolean hasNoData)
        {
            LegendScheme aLS = new LegendScheme(aST);
            double[] CValues;
            Color[] colors;

            hasNoData = ContourDraw.GetMaxMinValueFDiscreteData(discreteData, _meteoDataInfo.MissingValue, ref m_MinData, ref m_MaxData);
            lab_Min.Text = "Min: " + m_MinData.ToString("e1");
            lab_Max.Text = "Max: " + m_MaxData.ToString("e1");
            CValues = LegendManage.CreateContourValues(m_MinData, m_MaxData);
            colors = LegendManage.CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme            
            aLS = LegendManage.CreateGraduatedLegendScheme(CValues, colors,
                aST, m_MinData, m_MaxData, hasNoData, _meteoDataInfo.MissingValue);

            return aLS;
        }

        private LegendScheme CreateLegendSchemeFromData(double[,] GridData,
            LegendType aLT, ShapeTypes aST, ref Boolean hasNoData)
        {
            LegendScheme aLS = new LegendScheme(aST);
            double[] CValues;
            Color[] colors;

            hasNoData = ContourDraw.GetMaxMinValue(GridData, _meteoDataInfo.MissingValue, ref m_MinData, ref m_MaxData);
            lab_Min.Text = "Min: " + m_MinData.ToString("e1");
            lab_Max.Text = "Max: " + m_MaxData.ToString("e1");
            CValues = LegendManage.CreateContourValues(m_MinData, m_MaxData);
            colors = LegendManage.CreateRainBowColors(CValues.Length + 1);

            //Generate lengendscheme  
            if (aLT == LegendType.UniqueValue)
            {
                aLS = LegendManage.CreateUniqValueLegendScheme(CValues, colors,
                    aST, m_MinData, m_MaxData, hasNoData, _meteoDataInfo.MissingValue);
            }
            else
            {
                aLS = LegendManage.CreateGraduatedLegendScheme(CValues, colors,
                    aST, m_MinData, m_MaxData, hasNoData, _meteoDataInfo.MissingValue);
            }

            return aLS;
        }
                
        private void DrawMICAPS4()
        {
            if (!_useSameLegendScheme)
            {
                CreateLegendScheme();
            }
            DrawMeteoMap_Grid(true, _legendScheme);
        }

        //private void DrawHYSPLITConc()
        //{
        //    m_GridData = GetHYSPLITConcData(CB_Vars.SelectedIndex);

        //    if (!_useSameLegendScheme)
        //    {
        //        CreateLegendScheme();
        //    }
        //    DrawMeteoMap(true, _legendScheme);
        //}

        //private void DrawARLGrid()
        //{
        //    m_GridData = GetARLGridData(CB_Vars.SelectedIndex);

        //    if (!_useSameLegendScheme)
        //    {
        //        CreateLegendScheme();
        //    }
        //    DrawMeteoMap(true, _legendScheme);
        //}

        //private void DrawNetCDFGrid()
        //{
        //    m_GridData = GetNetCDFGridData(CB_Vars.SelectedIndex);

        //    if (!_useSameLegendScheme)
        //    {
        //        CreateLegendScheme();
        //    }
        //    DrawMeteoMap(true, _legendScheme);
        //}

        private void SetXYCoords(ref GridData aGridData)
        {
            int i;
            int xNum = 0, yNum = 0;
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lon:
                    xNum = CB_Lon1.Items.Count;
                    yNum = CB_Time1.Items.Count;
                    aGridData.X = new double[xNum];
                    aGridData.Y = new double[yNum];
                    break;
                case PlotDimension.Time_Lat:
                    xNum = CB_Lat1.Items.Count;
                    yNum = CB_Time1.Items.Count;
                    aGridData.X = new double[xNum];
                    aGridData.Y = new double[yNum];
                    break;
                case PlotDimension.Level_Lat:
                    xNum = CB_Lat1.Items.Count;
                    yNum = CB_Level1.Items.Count;
                    aGridData.X = new double[xNum];
                    aGridData.Y = new double[yNum];
                    break;
                case PlotDimension.Level_Lon:
                    xNum = CB_Lon1.Items.Count;
                    yNum = CB_Level1.Items.Count;
                    aGridData.X = new double[xNum];
                    aGridData.Y = new double[yNum];
                    break;
                case PlotDimension.Level_Time:
                    xNum = CB_Time1.Items.Count;
                    yNum = CB_Level1.Items.Count;
                    aGridData.X = new double[xNum];
                    aGridData.Y = new double[yNum];
                    break;
            }

            for (i = 0; i < xNum; i++)
            {
                aGridData.X[i] = i;
            }
            for (i = 0; i < yNum; i++)
            {
                aGridData.Y[i] = i;
            }            
        }

        private void GetXYCoords()
        {
            int i;
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lon:                    
                    _XNum = CB_Lon1.Items.Count;
                    _YNum = CB_Time1.Items.Count;
                    _X = new double[_XNum];
                    _Y = new double[_YNum];                    
                    break;
                case PlotDimension.Time_Lat:
                    _XNum = CB_Lat1.Items.Count;
                    _YNum = CB_Time1.Items.Count;
                    _X = new double[_XNum];
                    _Y = new double[_YNum];
                    break;
                case PlotDimension.Level_Lat:
                    _XNum = CB_Lat1.Items.Count;
                    _YNum = CB_Level1.Items.Count;
                    _X = new double[_XNum];
                    _Y = new double[_YNum];
                    break;
                case PlotDimension.Level_Lon:
                    _XNum = CB_Lon1.Items.Count;
                    _YNum = CB_Level1.Items.Count;
                    _X = new double[_XNum];
                    _Y = new double[_YNum];
                    break;
                case PlotDimension.Level_Time:
                    _XNum = CB_Time1.Items.Count;
                    _YNum = CB_Level1.Items.Count;
                    _X = new double[_XNum];
                    _Y = new double[_YNum];
                    break;
            }

            for (i = 0; i < _XNum; i++)
            {
                _X[i] = i;
            }
            for (i = 0; i < _YNum; i++)
            {
                _Y[i] = i;
            }
            //_XDelt = 1;
            //_YDelt = 1;
        }

        //private double[,] GetGrADSGridData(int vIdx)
        //{
        //    double[,] GridData = new double[0, 0];
        //    GrADSData CGrADSData = new GrADSData();
        //    GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
                        
        //    //Read grid data            
        //    int lonIdx, latIdx, lIdx, tIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time_Lon:                    
        //            latIdx = CB_Lat1.SelectedIndex;                    
        //            lIdx = CB_Level1.SelectedIndex;
        //            GridData = CGrADSData.ReadGrADSData_Grid_TimeLon(aDataInfo.DSET, latIdx, vIdx,
        //                lIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //        case PlotDimension.Time_Lat:                    
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            GridData = CGrADSData.ReadGrADSData_Grid_TimeLat(aDataInfo.DSET, lonIdx, vIdx,
        //                lIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //        case PlotDimension.Level_Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            GridData = CGrADSData.ReadGrADSData_Grid_LevelLat(aDataInfo.DSET, lonIdx, vIdx,
        //                tIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //        case PlotDimension.Level_Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            GridData = CGrADSData.ReadGrADSData_Grid_LevelLon(aDataInfo.DSET, latIdx, vIdx,
        //                tIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //        case PlotDimension.Level_Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            GridData = CGrADSData.ReadGrADSData_Grid_LevelTime(aDataInfo.DSET, latIdx, vIdx,
        //                lonIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //    }                       

        //    return GridData;
        //}

        private double[,] GetGRIB1GridData(int vIdx)
        {
            double[,] GridData = new double[0, 0];            
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;

            //Read grid data            
            int lonIdx, latIdx, lIdx, tIdx;
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_TimeLon(latIdx, vIdx, lIdx).Data;
                    break;
                case PlotDimension.Time_Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_TimeLat(lonIdx, vIdx, lIdx).Data;
                    break;
                case PlotDimension.Level_Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelLat(lonIdx, vIdx, tIdx).Data;
                    break;
                case PlotDimension.Level_Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelLon(latIdx, vIdx, tIdx).Data;
                    break;
                case PlotDimension.Level_Time:
                    latIdx = CB_Lat1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelTime(latIdx, vIdx, lonIdx).Data;
                    break;
            }

            return GridData;
        }

        private double[,] GetGRIB2GridData(int vIdx)
        {
            double[,] GridData = new double[0, 0];
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;

            //Read grid data            
            int lonIdx, latIdx, lIdx, tIdx;
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_TimeLon(latIdx, vIdx, lIdx).Data;
                    break;
                case PlotDimension.Time_Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_TimeLat(lonIdx, vIdx, lIdx).Data;
                    break;
                case PlotDimension.Level_Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelLat(lonIdx, vIdx, tIdx).Data;
                    break;
                case PlotDimension.Level_Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelLon(latIdx, vIdx, tIdx).Data;
                    break;
                case PlotDimension.Level_Time:
                    latIdx = CB_Lat1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    GridData = aDataInfo.GetGridData_LevelTime(latIdx, vIdx, lonIdx).Data;
                    break;
            }

            return GridData;
        }

        //private ArrayList GetGrADSStationData()
        //{
        //    ArrayList StationData = new ArrayList();
        //    int tIdx, vNum;
        //    tIdx = CB_Time1.SelectedIndex;
        //    vNum = CB_Vars.Items.Count;
        //    GrADSData CGrADSData = new GrADSData();
        //    StationData = CGrADSData.ReadGrADSData_Station(((GrADSDataInfo)_meteoDataInfo.DataInfo).DSET, tIdx, vNum,
        //        ((GrADSDataInfo)_meteoDataInfo.DataInfo));

        //    return StationData;
        //}

        //private double[,] GetHYSPLITConcData(int vIdx)
        //{
        //    double[,] GridData;
        //    HYSPLITData cHYSPLITData = new HYSPLITData();

        //    //Read grid data
        //    int tIdx, lIdx;
        //    tIdx = CB_Time1.SelectedIndex;            
        //    lIdx = CB_Level1.SelectedIndex;
        //    int aFactor;
        //    aFactor = Convert.ToInt32(frmMeteoData.pCurrenWin.TB_Multiplier.Text);
        //    GridData = cHYSPLITData.ReadConcData(tIdx, vIdx, lIdx, (HYSPLITConcDataInfo)_meteoDataInfo.DataInfo, aFactor);

        //    return GridData;
        //}

        //private double[,] GetARLGridData(int cvIdx)
        //{
        //    double[,] GridData = new double[0, 0];
        //    ARLMeteoData CARLMeteoData = new ARLMeteoData();
        //    ARLDataInfo aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;
                             
        //    //Read grid data            
        //    int lonIdx, latIdx, lIdx, tIdx, vIdx, clIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time_Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            clIdx = CB_Level1.SelectedIndex;
        //            lIdx = aDataInfo.varLevList[cvIdx].LevelIdxs[clIdx];
        //            vIdx = aDataInfo.varLevList[cvIdx].VarInLevelIdxs[clIdx];
        //            GridData = CARLMeteoData.GetARLGridData_TimeLon(latIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Time_Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            clIdx = CB_Level1.SelectedIndex;
        //            lIdx = aDataInfo.varLevList[cvIdx].LevelIdxs[clIdx];
        //            vIdx = aDataInfo.varLevList[cvIdx].VarInLevelIdxs[clIdx];
        //            GridData = CARLMeteoData.GetARLGridData_TimeLat(lonIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            GridData = CARLMeteoData.GetARLGridData_LevelLat(lonIdx, cvIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;                    
        //            GridData = CARLMeteoData.GetARLGridData_LevelLon(latIdx, cvIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            GridData = CARLMeteoData.GetARLGridData_LevelTime(latIdx, cvIdx, lonIdx, aDataInfo);
        //            break;
        //    }     

        //    return GridData;
        //}

        //private double[,] GetNetCDFGridData(int vIdx)
        //{
        //    double[,] GridData = new double[0, 0];
        //    NetCDFData CNetCDFData = new NetCDFData();
        //    NetCDFDataInfo aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;

        //    //Read grid data                      
        //    int lonIdx, latIdx, lIdx, tIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time_Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            GridData = CNetCDFData.GetNetCDFGridData_TimeLon(latIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Time_Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            GridData = CNetCDFData.GetNetCDFGridData_TimeLat(lonIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            GridData = CNetCDFData.GetNetCDFGridData_LevelLat(lonIdx, vIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            GridData = CNetCDFData.GetNetCDFGridData_LevelLon(latIdx, vIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level_Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            GridData = CNetCDFData.GetNetCDFGridData_LevelTime(latIdx, vIdx, lonIdx, aDataInfo);
        //            break;
        //    }  

        //    return GridData;
        //}               

        public void DrawMeteoMap(Boolean isNew, LegendScheme aLS)
        {
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:
                case MeteoDataType.GrADS_Station:
                case MeteoDataType.MICAPS_4:
                case MeteoDataType.HYSPLIT_Conc:
                case MeteoDataType.ARL_Grid:
                case MeteoDataType.NetCDF:
                case MeteoDataType.GRIB1:
                case MeteoDataType.GRIB2:
                    DrawMeteoMap_Grid(isNew, aLS);
                    break;
            }
        }

        private void DrawMeteoMap_Grid(Boolean isNew, LegendScheme aLS)
        {
            VectorLayer aLayer = null;
            string LName = GetLayerName(_plotDimension);
            string fieldName = CB_Vars.Text;
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                    //LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                    //aLayer = DrawMeteoData.CreateContourLayer(m_GridData, _X, _Y, _CValues, m_HasNoData,
                    //    _meteoDataInfo.MissingValue, aLS, LName);
                    aLayer = DrawMeteoData.CreateContourLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Shaded:
                    //LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                    //aLayer = DrawMeteoData.CreateShadedLayer(m_GridData, _X, _Y, _CValues, _colors,
                    //    m_MaxData, m_MinData, m_HasNoData, _meteoDataInfo.MissingValue, aLS, LName);
                    aLayer = DrawMeteoData.CreateShadedLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Grid_Fill:
                    //LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                    //aLayer = DrawMeteoData.CreateGridFillLayer(m_GridData, _X, _Y, _XDelt, _YDelt, aLS, LName);
                    aLayer = DrawMeteoData.CreateGridFillLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Grid_Point:
                    //LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                    //aLayer = DrawMeteoData.CreateGridPointLayer(m_GridData, _X, _Y, aLS, LName);
                    aLayer = DrawMeteoData.CreateGridPointLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Vector:
                    GridData UData = new GridData();
                    GridData VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        SetXYCoords(ref UData);
                        SetXYCoords(ref VData);
                        if (CHB_ColorVar.Checked)
                        {
                            LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                        }
                        aLayer = DrawMeteoData.CreateGridVectorLayer(UData, VData, _gridData, aLS, CHB_ColorVar.Checked,
                            LName, _meteoDataInfo.MeteoUVSet.IsUV);                        
                    }                    
                    break;
                case DrawType2D.Barb:
                    UData = new GridData();
                    VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        SetXYCoords(ref UData);
                        SetXYCoords(ref VData);
                        if (CHB_ColorVar.Checked)
                        {
                            LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                        }
                        aLayer = DrawMeteoData.CreateGridBarbLayer(UData, VData, _gridData, aLS, 
                            CHB_ColorVar.Checked, LName, _meteoDataInfo.MeteoUVSet.IsUV);
                    }                    
                    break;
                case DrawType2D.Streamline:
                    UData = new GridData();
                    VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        SetXYCoords(ref UData);
                        SetXYCoords(ref VData);
                        if (CHB_ColorVar.Checked)
                        {
                            LegendManage.SetContoursAndColors(aLS, ref _CValues, ref _colors);
                        }
                        aLayer = DrawMeteoData.CreateStreamlineLayer(UData, VData, _StrmDensity, aLS, 
                            LName, _meteoDataInfo.MeteoUVSet.IsUV);
                    }                    
                    break;
            }

            if (aLayer != null)
            {
                if (aLayer.ShapeType == ShapeTypes.Polygon)
                {
                    _lastAddedLayerHandle = G_LayersLegend.ActiveMapFrame.InsertPolygonLayer(aLayer);
                }
                else
                {
                    _lastAddedLayerHandle = G_LayersLegend.ActiveMapFrame.InsertPolylineLayer(aLayer);
                }
            }
            else
                MessageBox.Show("There is no layer created!", "Alarm");
        }

        private bool GetUVGridData(ref GridData Udata, ref GridData Vdata)
        {
            //Get U/V data
            int i;
            //int vIdx;
            List<string> vList = new List<string>();
            for (i = 0; i < CB_Vars.Items.Count; i++)
            {
                vList.Add(CB_Vars.Items[i].ToString());
            }
            if (!_meteoDataInfo.MeteoUVSet.IsFixUVStr)
            {
                if (!_meteoDataInfo.MeteoUVSet.AutoSetUVStr(vList))
                {
                    frmUVSet aFrmUVSet = new frmUVSet();
                    aFrmUVSet.SetUVItems(vList);
                    if (aFrmUVSet.ShowDialog() == DialogResult.OK)
                    {
                        string UStr, VStr;
                        UStr = "";
                        VStr = "";
                        aFrmUVSet.GetUVItems(ref UStr, ref VStr);
                        _meteoDataInfo.MeteoUVSet.IsUV = aFrmUVSet.RB_U_V.Checked;
                        _meteoDataInfo.MeteoUVSet.UStr = UStr;
                        _meteoDataInfo.MeteoUVSet.VStr = VStr;
                        _meteoDataInfo.MeteoUVSet.IsFixUVStr = true;
                        _meteoDataInfo.MeteoUVSet.SkipX = (int)aFrmUVSet.NUD_XSkip.Value;
                        _meteoDataInfo.MeteoUVSet.SkipY = (int)aFrmUVSet.NUD_YSkip.Value;
                    }
                    else
                    {
                        MessageBox.Show("U/V variables were not set!", "Error");
                        return false;
                    }
                }
            }

            if (_meteoDataInfo.IsGridData)
            {
                Udata = _meteoDataInfo.GetGridData(_meteoDataInfo.MeteoUVSet.UStr);
                Vdata = _meteoDataInfo.GetGridData(_meteoDataInfo.MeteoUVSet.VStr);

                //Skip the grid data
                if (_meteoDataInfo.MeteoUVSet.SkipY != 1 || _meteoDataInfo.MeteoUVSet.SkipX != 1)
                {
                    Udata = Udata.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
                    Vdata = Vdata.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
                }
            }
            else
            {
                switch (_meteoDataInfo.DataType)
                {
                    case MeteoDataType.GrADS_Station:
                        GrADSDataInfo aGDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
                        int uIdx = aGDataInfo.UpperVariableNames.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
                        int vIdx = aGDataInfo.UpperVariableNames.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
                        Udata = aGDataInfo.GetGridData_Station(uIdx, CLB_Stations.CheckedItems[0].ToString());
                        Vdata = aGDataInfo.GetGridData_Station(vIdx, CLB_Stations.CheckedItems[0].ToString());
                        break;
                }
            }

            return true;
        }

        //private bool GetUVGridData(ref double[,] Udata, ref double[,] Vdata)
        //{
        //    //Get U/V data
        //    int i;            
        //    //int vIdx;
        //    List<string> vList = new List<string>();
        //    for (i = 0; i < CB_Vars.Items.Count; i++)
        //    {
        //        vList.Add(CB_Vars.Items[i].ToString());
        //    }
        //    if (!_meteoDataInfo.MeteoUVSet.IsFixUVStr)
        //    {
        //        if (!_meteoDataInfo.MeteoUVSet.AutoSetUVStr(vList))
        //        {
        //            frmUVSet aFrmUVSet = new frmUVSet();
        //            aFrmUVSet.SetUVItems(vList);
        //            if (aFrmUVSet.ShowDialog() == DialogResult.OK)
        //            {
        //                string UStr, VStr;
        //                UStr = "";
        //                VStr = "";
        //                aFrmUVSet.GetUVItems(ref UStr, ref VStr);
        //                _meteoDataInfo.MeteoUVSet.UStr = UStr;
        //                _meteoDataInfo.MeteoUVSet.VStr = VStr;                        
        //                _meteoDataInfo.MeteoUVSet.IsFixUVStr = true;
        //            }
        //            else
        //            {
        //                MessageBox.Show("U/V variables were not set!", "Error");
        //                return false;
        //            }
        //        }
        //    }

        //    int uIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
        //    int vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.GrADS_Grid:                    
        //            Udata = GetGrADSGridData(uIdx);                    
        //            Vdata = GetGrADSGridData(vIdx);
        //            break;
        //        case MeteoDataType.ARL_Grid:                                        
        //            Udata = GetARLGridData(uIdx);                    
        //            Vdata = GetARLGridData(vIdx);
        //            break;
        //        case MeteoDataType.GRIB1:
        //            Udata = GetGRIB1GridData(uIdx);
        //            Vdata = GetGRIB1GridData(vIdx);
        //            break;
        //        case MeteoDataType.GRIB2:
        //            Udata = GetGRIB2GridData(uIdx);
        //            Vdata = GetGRIB2GridData(vIdx);
        //            break;
        //        case MeteoDataType.NetCDF:
        //            Udata = GetNetCDFGridData(uIdx);
        //            Vdata = GetNetCDFGridData(vIdx);
        //            break;
        //        case MeteoDataType.HYSPLIT_Conc:
        //            Udata = GetHYSPLITConcData(uIdx);
        //            Vdata = GetHYSPLITConcData(vIdx);
        //            break;
        //    }

        //    return true;
        //}
                        
        private string GetLayerName(PlotDimension aPD)
        {
            string layerName;
            layerName = CB_DrawType.Text + "_" +CB_Vars.Text;
            if (CB_Level1.Enabled)
            {
                layerName += "_" + CB_Level1.Text;
            }
            if (CB_Lon1.Enabled)
            {
                layerName += "_" + CB_Lon1.Text;
            }
            if (CB_Lat1.Enabled)
            {
                layerName += "_" + CB_Lat1.Text;
            }
            if (CB_Time1.Enabled)
            {
                layerName += "_" + CB_Time1.Text;
            }

            return layerName;
        }
                                        
        private void GetXYGridStrs()
        {
            _XGridStrs.Clear();
            _YGridStrs.Clear();
            int i;
            
            switch (_plotDimension)
            {
                case PlotDimension.Time_Lat:
                    _XGridStrs = GetLatGridStr();
                    _YGridStrs = GetTimeGridStr();
                    break;
                case PlotDimension.Time_Lon:
                    _XGridStrs = GetLonGridStr();
                    _YGridStrs = GetTimeGridStr();
                    break;
                case PlotDimension.Level_Lat:
                    _XGridStrs = GetLatGridStr();
                    for (i = 0; i < CB_Level1.Items.Count; i++)
                    {
                        _YGridStrs.Add(CB_Level1.Items[i].ToString());
                    }
                    break;
                case PlotDimension.Level_Lon:
                    _XGridStrs = GetLonGridStr();
                    for (i = 0; i < CB_Level1.Items.Count; i++)
                    {
                        _YGridStrs.Add(CB_Level1.Items[i].ToString());
                    }
                    break;
                case PlotDimension.Level_Time:
                    _XGridStrs = GetTimeGridStr();
                    for (i = 0; i < CB_Level1.Items.Count; i++)
                    {
                        _YGridStrs.Add(CB_Level1.Items[i].ToString());
                    }
                    break;
            }

            //Set if y reverse
            if (CHB_YReverse.Visible && CHB_YReverse.Checked)
                _YGridStrs.Reverse();
        }

        private List<string> GetLonGridStr()
        {
            List<string> GStrList = new List<string>();
            string drawStr;
            int i;

            if (_meteoDataInfo.IsLonLat)
            {
                Single lon;
                for (i = 0; i < CB_Lon1.Items.Count; i++)
                {
                    lon = Single.Parse(CB_Lon1.Items[i].ToString());
                    if (lon < 0)
                    {
                        lon = lon + 360;
                    }
                    if (lon > 0 && lon <= 180)
                    {
                        drawStr = lon.ToString() + "E";
                    }
                    else if (lon == 0 || lon == 360)
                    {
                        drawStr = "0";
                    }
                    else if (lon == 180)
                    {
                        drawStr = "180";
                    }
                    else
                    {
                        drawStr = (360 - lon).ToString() + "W";
                    }
                    GStrList.Add(drawStr);
                }
            }
            else
            {
                for (i = 0; i < CB_Lon1.Items.Count; i++)
                {
                    drawStr = CB_Lon1.Items[i].ToString();
                    GStrList.Add(drawStr);
                }
            }

            return GStrList;
        }

        private List<string> GetLatGridStr()
        {
            List<string> GStrList = new List<string>();
            string drawStr;
            int i;

            if (_meteoDataInfo.IsLonLat)
            {
                Single lat;
                for (i = 0; i < CB_Lat1.Items.Count; i++)
                {
                    lat = Single.Parse(CB_Lat1.Items[i].ToString());
                    if (lat > 0)
                    {
                        drawStr = lat.ToString() + "N";
                    }
                    else if (lat < 0)
                    {
                        drawStr = (-lat).ToString() + "S";
                    }
                    else
                    {
                        drawStr = "EQ";
                    }
                    GStrList.Add(drawStr);
                }
            }
            else
            {
                for (i = 0; i < CB_Lat1.Items.Count; i++)
                {
                    drawStr = CB_Lat1.Items[i].ToString();
                    GStrList.Add(drawStr);
                }
            }

            return GStrList;
        }

        private List<string> GetTimeGridStr()
        {
            List<string> GStrList = new List<string>();
            int i;
            List<DateTime> DTList = new List<DateTime>();
            for (i = 0; i < CB_Time1.Items.Count; i++)
            {
                DTList.Add(DateTime.Parse(CB_Time1.Items[i].ToString()));
            }

            string timeFormat;
            if ((DTList[1] - DTList[0]).Duration().Days >= 1)
            {
                timeFormat = "yyyy-MM-dd";
            }
            else if ((DTList[1] - DTList[0]).Duration().Hours >= 1)
            {
                timeFormat = "yyyy-MM-dd HH";
            }
            else
            {
                timeFormat = "yyyy-MM-dd HH:mm";
            }

            if (DTList[0].Year == DTList[DTList.Count - 1].Year)
            {
                timeFormat = timeFormat.Substring(5);
                if (DTList[0].Month == DTList[DTList.Count - 1].Month)
                {
                    timeFormat = timeFormat.Substring(3);
                }
            }

            for (i = 0; i < DTList.Count; i++)
            {
                GStrList.Add(DTList[i].ToString(timeFormat));
            }

            return GStrList;
        }
        
        private void ZoomToExtent(Extent aExtent)
        {
            _mapLayout.ActiveMapFrame.MapView.ZoomToExtent(aExtent);
        }

        #endregion

        #region Events

        #region GUI Events

        private void CB_DrawType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _2DDrawType = (DrawType2D)Enum.Parse(typeof(DrawType2D),
                CB_DrawType.Text, true);
            TSB_Draw.Enabled = true;
            _useSameLegendScheme = false;

            //m_UseSameGridInterSet = false;

            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;

            //Set CHB_ColorVar visible
            switch (_2DDrawType)
            {
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                    CHB_ColorVar.Visible = true;
                    break;
                default:
                    CHB_ColorVar.Visible = false;
                    break;
            }
        }

        private void TSB_FullExent_Click(object sender, EventArgs e)
        {
            Extent aExtent = _mapLayout.ActiveMapFrame.MapView.Extent;
            ZoomToExtent(aExtent);
        }        

        private void TSB_ClearDrawing_Click(object sender, EventArgs e)
        {
            //Remove last layer
            G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
            TSB_Draw.Enabled = true;
        }

        private void TSB_ZoomToLayer_Click(object sender, EventArgs e)
        {
            if (G_LayersLegend.SelectedNode.NodeType == NodeTypes.LayerNode)
            {
                LayerNode aLN = (LayerNode)G_LayersLegend.SelectedNode;
                MapLayer aLayer = aLN.MapFrame.MapView.GetLayerFromHandle(aLN.LayerHandle);
                Extent aExtent = new Extent();

                VectorLayer sLayer = (VectorLayer)aLayer;
                aExtent = sLayer.Extent;

                ZoomToExtent(aExtent);
            }
        }

        private void TSB_ZoomIn_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            //_mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.Zoom_In;
            _mapLayout.MouseMode = MouseMode.Map_ZoomIn;
        }

        private void TSB_None_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            //_mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.None;
            _mapLayout.MouseMode = MouseMode.Select;
        }

        private void TSB_ZoomOut_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            //_mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.Zoom_Out;
            _mapLayout.MouseMode = MouseMode.Map_ZoomOut;
        }

        private void TSB_Pan_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            //_mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.Pan;
            _mapLayout.MouseMode = MouseMode.Map_Pan;
        }
                       
        private void TSB_DrawSetting_Click(object sender, EventArgs e)
        {
            MapLayer aLayer = G_LayersLegend.ActiveMapFrame.MapView.GetLayerFromHandle(_lastAddedLayerHandle);
            frmLegendSet aFrmLS = new frmLegendSet(true, aLayer, G_LayersLegend);
            aFrmLS.SetLegendScheme(_legendScheme);            
            if (aFrmLS.ShowDialog() == DialogResult.OK)
            {
                G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
                this._legendScheme = aFrmLS.GetLegendScheme();
                DrawMeteoMap(true, _legendScheme);                
            }
            else
            {
                if (aFrmLS.GetIsApplied())
                {
                    G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
                    DrawMeteoMap(true, _legendScheme);                    
                }
            }

            //frmLegendEdit aFrmLE = new frmLegendEdit(true, _lastAddedLayerHandle);
            //aFrmLE.SetLegendScheme(_legendScheme);
            //aFrmLE.ShowDialog();
        }

        private void CB_PlotDims_SelectedValueChanged(object sender, EventArgs e)
        {            
            _plotDimension = (PlotDimension)Enum.Parse(typeof(PlotDimension),
                CB_PlotDims.Text, true);

            _meteoDataInfo.DimensionSet = _plotDimension;
            switch (_plotDimension)
            {
                case PlotDimension.Level_Lat:
                case PlotDimension.Level_Lon:
                case PlotDimension.Level_Time:
                    CHB_YReverse.Visible = true;
                    break;
                default:
                    CHB_YReverse.Visible = false;
                    break;
            }

            SetDimensions();

            if (!_isSamePlotDim)
            {
                if (_mapLayout.ActiveMapFrame.MapView.LayerSet.LayerNum > 0)
                {
                    G_LayersLegend.ActiveMapFrame.RemoveMeteoLayers();
                }
            }

            TSB_Draw.Enabled = true;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
        }

        private void TSB_PreTime_Click(object sender, EventArgs e)
        {
            _useSameLegendScheme = true;            
            if (CB_Time1.SelectedIndex > 0)
            {
                CB_Time1.SelectedIndex = CB_Time1.SelectedIndex - 1;
            }
            else
            {
                CB_Time1.SelectedIndex = CB_Time1.Items.Count - 1;
            }
            _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = true;
            G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
            _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = false;
            TSB_Draw.PerformClick();
        }

        private void TSB_NextTime_Click(object sender, EventArgs e)
        {
            _useSameLegendScheme = true;            
            if (CB_Time1.SelectedIndex < CB_Time1.Items.Count - 1)
            {
                CB_Time1.SelectedIndex = CB_Time1.SelectedIndex + 1;
            }
            else
            {
                CB_Time1.SelectedIndex = 0;
            }

            //Remove last layer
            _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = true;
            G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
            _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = false;
            TSB_Draw.PerformClick();
        }

        private void TSB_Animitor_Click(object sender, EventArgs e)
        {
            if (CB_Time1.Items.Count > 1)
            {
                _useSameLegendScheme = true;                
                for (int i = 0; i < CB_Time1.Items.Count; i++)
                {
                    _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = true;
                    G_LayersLegend.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
                    _mapLayout.ActiveMapFrame.MapView.LockViewUpdate = false;
                    CB_Time1.SelectedIndex = i;
                    TSB_Draw.PerformClick();
                    System.Threading.Thread.Sleep(500);
                }
            }      
        }
        
        private void TSB_SavePicture_Click(object sender, EventArgs e)
        {
            SaveFileDialog aDlg = new SaveFileDialog();
            aDlg.Filter = "Gif Image (*.gif)|*.gif|Jpeg Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|Tif Image (*.tif)|*.tif|Windows Meta File (*.wmf)|*.wmf";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                _mapLayout.ExportToPicture(aDlg.FileName);
            }
        }

        private void CB_PlotDims_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //_plotDimension = (clsMeteoData.PlotDimension)Enum.Parse(typeof(clsMeteoData.PlotDimension),
            //    CB_PlotDims.Text, true);
            //SetDimensions();
            //GetXYGridStrs();
            //if (G_LayersLegend.m_Layers.layerNum > 0)
            //{
            //    G_LayersLegend.RemoveMeteoLayers();
            //    pictureBox1.Refresh();
            //}  
        }

        private void CB_Level1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TSB_Draw.Enabled = true;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
            _useSameLegendScheme = false;
            _meteoDataInfo.LevelIndex = CB_Level1.SelectedIndex;

            //if (_meteoDataInfo.DataType == clsMeteoData.MeteoDataType.ARL_Grid)
            //{
            //    CB_Vars.Items.Clear();
            //    int i;
            //    clsARLMeteoData.ARLDataInfo aDataInfo = new clsARLMeteoData.ARLDataInfo();
            //    aDataInfo = (clsARLMeteoData.ARLDataInfo)_meteoDataInfo.DataInfo;
            //    for (i = 0; i < aDataInfo.Variables[CB_Level1.SelectedIndex].Count; i++)
            //    {
            //        CB_Vars.Items.Add(aDataInfo.Variables[CB_Level1.SelectedIndex][i]);
            //    }
            //    CB_Vars.SelectedIndex = 0;
            //}
        }        

        private void TSMI_LayoutProp_Click(object sender, EventArgs e)
        {
            //frmProperty aFrmPT = new frmProperty();
            //aFrmPT.Text = "Layout Property Set";
            //aFrmPT.SetObject(_mapLayout.LayoutProperty);
            //aFrmPT.ShowDialog();

            _mapLayout.SetProperties();
        }

        private void TSMI_MapProp_Click(object sender, EventArgs e)
        {
            //frmProperty aFrmPT = new frmProperty();
            //aFrmPT.Text = "Map Property Set";
            //aFrmPT.SetObject(_mapLayout.ActiveMapFrame.MapView.MapPropertyV);
            //aFrmPT.ShowDialog();

            _mapLayout.ActiveMapFrame.MapView.SetProperties();
        }

        private void TSB_Identifer_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            //Cursor myCursor = new Cursor(this.GetType().Assembly.
            //        GetManifestResourceStream("MeteoInfo.Resources.cursor.cur"));

            //_mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.None;
            //_mapLayout.ActiveMapFrame.MapView.Cursor = myCursor;
            _mapLayout.MouseMode = MouseMode.Map_Identifer;
        }

        private void TSB_LabelSet_Click(object sender, EventArgs e)
        {
            if (G_LayersLegend.SelectedNode.NodeType == NodeTypes.LayerNode)
            {
                LayerNode aLN = (LayerNode)G_LayersLegend.SelectedNode;
                MapLayer aMLayer = aLN.MapFrame.MapView.GetLayerFromHandle(aLN.LayerHandle);
                if (aMLayer.LayerType == LayerTypes.VectorLayer)
                {
                    VectorLayer aLayer = (VectorLayer)aMLayer;
                    if (aLayer.ShapeNum > 0)
                    {
                        frmLabelSet aFrmLabel = new frmLabelSet(G_LayersLegend.ActiveMapFrame.MapView);
                        aFrmLabel.Layer = aLayer;
                        aFrmLabel.Show(this);
                    }
                }
            }
        }

        private void TSMI_Legend_Click(object sender, EventArgs e)
        {
            _mapLayout.AddLegend(100, 100);
            _mapLayout.PaintGraphics();
        }

        private void TSB_SelectElements_Click(object sender, EventArgs e)
        {
            if (!(_currentTool == null))
            {
                _currentTool.Checked = false;
            }
            _currentTool = (ToolStripButton)sender;
            _currentTool.Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            _mapLayout.ActiveMapFrame.MapView.MouseTool = MouseTools.None;
        }        

        private void TSB_PageSet_Click(object sender, EventArgs e)
        {
            frmPageSetup aFrmPageSetup = new frmPageSetup(_mapLayout.PaperSize, _mapLayout.Landscape);
            if (aFrmPageSetup.ShowDialog() == DialogResult.OK)
            {
                _mapLayout.PaperSize = aFrmPageSetup.PaperSizeV;
                _mapLayout.Landscape = aFrmPageSetup.Landscape;
                _mapLayout.Refresh();
            }
        }

        private void CHB_NewVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (CHB_NewVariable.Checked)
            {
                Lab_Var.Visible = false;
                CB_Vars.Visible = false;
                TB_NewVariable.Visible = true;
            }
            else
            {
                Lab_Var.Visible = true;
                CB_Vars.Visible = true;
                TB_NewVariable.Visible = false;
            }
        }

        #endregion

        #region Control event       

        private void ActiveMapFrameChanged(object sender, EventArgs e)
        {
            _mapLayout.SetActiveMapFrame(G_LayersLegend.ActiveMapFrame);
        }

        private void MapFramesUpdated(object sender, EventArgs e)
        {
            foreach (MapFrame aMF in G_LayersLegend.MapFrames)
            {
                aMF.IsFireMapViewUpdate = true;
                aMF.MapView.IsGeoMap = false;
            }

            _mapLayout.UpdateMapFrames(G_LayersLegend.MapFrames);
            
            _mapLayout.PaintGraphics();
        }

        private void Layout_ActiveMapFrameChanged(object sender, EventArgs e)
        {
            G_LayersLegend.SetActiveMapFrame(_mapLayout.ActiveMapFrame);
        }

        private void Layout_MapFramesUpdated(object sender, EventArgs e)
        {
            G_LayersLegend.MapFrames = _mapLayout.MapFrames;
            G_LayersLegend.Refresh();
        }

        #endregion

        private void TSMI_InsertText_Click(object sender, EventArgs e)
        {
            _mapLayout.AddText("Text", _mapLayout.Width / 2, 200);
            _mapLayout.PaintGraphics();
        }

        #endregion

        private void TSB_ViewData_Click(object sender, EventArgs e)
        {
            if (_meteoDataInfo.IsGridData)
                ViewGridData();            
        }

        private void ViewGridData()
        {
            if (_gridData == null)
                return;

            if (_gridData.Data == null)
                return;

            if (_gridData.Data.GetLength(0) == 0 || _gridData.Data.GetLength(1) == 0)
                return;

            double min = _gridData.GetMinValue();
            int dNum = MIMath.GetDecimalNum(min);
            string dFormat = "f" + dNum.ToString();

            frmDataGridViewData aForm = new frmDataGridViewData();
            aForm.DataType = "GridData";
            aForm.Data = _gridData;
            aForm.TSB_ToStation.Visible = true;
            aForm.MissingValue = _meteoDataInfo.MissingValue;
            int i, j, colNum, rowNum;

            colNum = _gridData.XNum;
            rowNum = _gridData.YNum;
            aForm.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewColumn[] columns = new DataGridViewColumn[colNum];
            for (i = 0; i < columns.Length; ++i)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.FillWeight = 1;
                columns[i] = column;
            }
            aForm.dataGridView1.Columns.AddRange(columns);
            //aForm.dataGridView1.ColumnCount = colNum;
            aForm.dataGridView1.RowCount = rowNum;
            for (i = 0; i < colNum; i++)
            {
                aForm.dataGridView1.Columns[i].Name = i.ToString();
                aForm.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (j = 0; j < rowNum; j++)
                    aForm.dataGridView1[i, j].Value = _gridData.Data[rowNum - j - 1, i].ToString(dFormat);

                aForm.dataGridView1.Columns[i].Frozen = false;
                //aForm.dataGridView1.Columns[i].FillWeight = 10;
            }
            aForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            aForm.dataGridView1.MultiSelect = true;
            aForm.dataGridView1.AllowUserToOrderColumns = true;

            aForm.Text = "Grid Data";
            aForm.Show(this);
        }

        private void CLB_Stations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked) return;
            for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
            {
                ((CheckedListBox)sender).SetItemChecked(i, false);
            }
            e.NewValue = CheckState.Checked;
        }

        private void TSB_PageZoomIn_Click(object sender, EventArgs e)
        {
            _mapLayout.Zoom = _mapLayout.Zoom * 1.5F;
            _mapLayout.PaintGraphics();
        }

        private void TSB_PageZoomOut_Click(object sender, EventArgs e)
        {
            _mapLayout.Zoom = _mapLayout.Zoom * 0.8F;
            _mapLayout.PaintGraphics();
        }

        private void TSB_FitToScreen_Click(object sender, EventArgs e)
        {
            float zoomX = (float)_mapLayout.Width / _mapLayout.PageBounds.Width;
            float zoomY = (float)_mapLayout.Height / _mapLayout.PageBounds.Height;
            float zoom = Math.Min(zoomX, zoomY);
            PointF aP = new PointF(0, 0);
            _mapLayout.PageLocation = aP;
            _mapLayout.Zoom = zoom;
            _mapLayout.PaintGraphics();
        }

        private void TSCB_PageZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoomStr = TSCB_PageZoom.Text.Trim();
            zoomStr = zoomStr.Trim('%');
            try
            {
                float zoom = float.Parse(zoomStr);
                _mapLayout.Zoom = zoom / 100;
                _mapLayout.PaintGraphics();
            }
            catch
            {

            }
        }

        private void TSCB_PageZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string zoomStr = TSCB_PageZoom.Text.Trim();
                zoomStr = zoomStr.Trim('%');
                try
                {
                    float zoom = float.Parse(zoomStr);
                    _mapLayout.Zoom = zoom / 100;
                    _mapLayout.PaintGraphics();
                }
                catch
                {

                }
            }
        }

        private void CB_Time1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meteoDataInfo.TimeIndex = CB_Time1.SelectedIndex;
        }

        private void CB_Lat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meteoDataInfo.LatIndex = CB_Lat1.SelectedIndex;
        }

        private void CB_Lon1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meteoDataInfo.LonIndex = CB_Lon1.SelectedIndex;
        }       

    }
}
