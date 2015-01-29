using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using MeteoInfo.Properties;

using MeteoInfoC;
using MeteoInfoC.Map;
using MeteoInfoC.Layout;
using MeteoInfoC.Legend;
using MeteoInfoC.Data.MapData;
using MeteoInfoC.Data.MeteoData;
using MeteoInfoC.Layer;
using MeteoInfo.Classes;
using MeteoInfo.Forms;
using MeteoInfoC.Global;
using MeteoInfoC.Shape;
using MeteoInfoC.Drawing;
using MeteoInfoC.Projections;
using MeteoInfoC.Plugin;
using MeteoInfoC.Geoprocess;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace MeteoInfo
{
    public partial class frmMain : Form, IApplication
    {
        //[DllImport("gdi32")]
        //public static extern int GetEnhMetaFileBits(int hemf, int cbBuffer, byte[] lpbBuffer); 

        #region Events definition
        /// <summary>
        /// Ocurrs after current tool changed
        /// </summary>
        public event EventHandler CurrentToolChanged;

        #endregion

        #region Variables

        public static frmMain CurrentWin = null;
        //public static LayersLegend MapDocument;
        public LayersLegend _mapDocument;
        //public static MapLayout MapDocument.MapLayout;
        //public static frmIdentifer G_FrmIdentifer = null;
        //public static frmIdentiferGrid G_FrmIdentiferGrid = null;

        private MapView _mapView;
        private ToolStripItem _currentTool;
        Label Lab_Head = new Label();
        string m_Title = "";
        clsProjectFile _projectFile = new clsProjectFile();

        //private Point _mouseDownPoint = new Point();
        //private bool _startNewGraphic = true;
        //private List<PointF> _graphicPoints = new List<PointF>();

        private PluginCollection _plugins = new PluginCollection();
        private MeteoInfo.Classes.Options _options = new MeteoInfo.Classes.Options();

        //private bool _ShowLonLat = true;

        private bool _isEditingVertices = false;

        //private bool _startNewGraphic = true;
        private List<PointF> _graphicPoints = new List<PointF>();
        //private frmMeasurement _frmMeasure = new MeteoInfo.Forms.frmMeasurement();

        private frmMeteoData _frmMeteoData;
        public bool IsClosed = false;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            ////Set language
            //string aFile = Application.StartupPath + "\\Default.mip";
            //if (!File.Exists(aFile))
            //{
            //    aFile = Application.StartupPath + "/Default.mip";
            //}
            //if (File.Exists(aFile))
            //{
            //    //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            //    clsProjectFile CProjectFile = new clsProjectFile();
            //    CProjectFile.LoadProjFile(aFile);
            //}
            System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("en-US", false);
            System.Threading.Thread.CurrentThread.CurrentCulture = myCI;

            _mapDocument = new LayersLegend();
            _mapDocument.MapLayout = new MapLayout();
            _mapView = new MapView();
            //_frmMeteoData = new frmMeteoData();

            InitializeComponent();
            this.TSB_SelectFeatures.SyncToDefault();

            CurrentWin = this;

            LoadForm();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="forScript">is for script</param>
        public frmMain(bool forScript)
        {
            _mapDocument = new LayersLegend();
            _mapDocument.MapLayout = new MapLayout();
            _mapView = new MapView();
            //_frmMeteoData = new frmMeteoData();

            InitializeComponent();

            CurrentWin = this;

            LoadForm_Script();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get MapView
        /// </summary>
        public IMapView MapView
        {
            get { return _mapDocument.ActiveMapFrame.MapView; }
        }

        /// <summary>
        /// Get MapDocument
        /// </summary>
        public LayersLegend MapDocument
        {
            get { return _mapDocument; }
        }

        /// <summary>
        /// Get main menu strip
        /// </summary>
        public new MenuStrip MainMenuStrip
        {
            get { return this.MenuStrip_Main; }
        }

        /// <summary>
        /// Get MeteoData
        /// </summary>
        public frmMeteoData MeteoDataset
        {
            get { return _frmMeteoData; }
        }

        /// <summary>
        /// Get main tool strip container
        /// </summary>
        public ToolStripContainer MainToolStripContainer
        {
            get { return this.toolStripContainer_Main; }
        }

        /// <summary>
        /// Get or set plugins
        /// </summary>
        public PluginCollection Plugins
        {
            get { return _plugins; }
            set { _plugins = value; }
        }

        /// <summary>
        /// Get or set options
        /// </summary>
        public MeteoInfo.Classes.Options Options
        {
            get { return _options; }
            set { _options = value; }
        }

        /// <summary>
        /// Get or set current tool
        /// </summary>
        public ToolStripItem CurrentTool
        {
            get { return _currentTool; }
            set { _currentTool = value; }
        }

        /// <summary>
        /// Get main ToolStripProgressBar
        /// </summary>
        public ToolStripProgressBar MainToolStripProgressBar
        {
            get { return this.TSPB_Main; }
        }

        /// <summary>
        /// Get main ToolStripStatusLabel
        /// </summary>
        public ToolStripStatusLabel MainToolStripStatusLabel
        {
            get { return this.TSSL_Main; }
        }        

        #endregion

        #region Events

        private void MapView_MouseMove(object sender, MouseEventArgs e)
        {
            double ProjX, ProjY;
            ProjX = 0;
            ProjY = 0;
            MapDocument.ActiveMapFrame.MapView.ScreenToProj(ref ProjX, ref ProjY, e.X, e.Y);
            if (MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
            {
                this.TSSL_Coord.Text = "Lon: " + ProjX.ToString("0.00") + "; Lat: " + ProjY.ToString("0.00");
            }
            else
            {
                string theText = this.TSSL_Coord.Text = "X: " + ProjX.ToString("0.0") + "; Y: " + ProjY.ToString("0.0");
                if (MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Robinson)
                    return;

                ProjectionInfo toProj = KnownCoordinateSystems.Geographic.World.WGS1984;
                ProjectionInfo fromProj = MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
                double[][] points = new double[1][];
                points[0] = new double[] { ProjX, ProjY };
                //double[] Z = new double[1];
                try
                {
                    Reproject.ReprojectPoints(points, fromProj, toProj, 0, 1);
                    this.TSSL_Coord.Text = theText + " (Lon: " + points[0][0].ToString("0.00") + "; Lat: " +
                        points[0][1].ToString("0.00") + ")";
                }
                catch
                {
                    //this.TSSL_Coord.Text = "X: " + ProjX.ToString("0.0") + "; Y: " + ProjY.ToString("0.0"); 
                }
            }

            //if (_currentTool.Name == "TSB_Measurement" && _frmMeasure.IsHandleCreated)
            //{
            //    switch (_frmMeasure.MeasureType)
            //    {
            //        case MeasureType.Length:
            //        case MeasureType.Area:
            //            if (!_startNewGraphic)
            //            {
            //                //Draw graphic
            //                Graphics g = MapDocument.ActiveMapFrame.MapView.CreateGraphics();
            //                g.SmoothingMode = SmoothingMode.AntiAlias;
            //                MapDocument.ActiveMapFrame.MapView.Refresh();
            //                PointF[] points = _graphicPoints.ToArray();
            //                Array.Resize(ref points, _graphicPoints.Count + 1);
            //                points[_graphicPoints.Count] = new PointF(e.X, e.Y);

            //                if (_frmMeasure.MeasureType == MeasureType.Length)
            //                    g.DrawLines(new Pen(Color.Red, 2), points);
            //                else
            //                {
            //                    Color aColor = Color.FromArgb(100, Color.Blue);
            //                    g.FillPolygon(new SolidBrush(aColor), points);
            //                    g.DrawPolygon(new Pen(Color.Red, 2), points);
            //                }

            //                g.Dispose();

            //                //Calculate                    
            //                if (_frmMeasure.MeasureType == MeasureType.Length)
            //                {
            //                    double pProjX = 0, pProjY = 0;
            //                    MapDocument.ActiveMapFrame.MapView.ScreenToProj(ref pProjX, ref pProjY,
            //                        _mouseDownPoint.X, _mouseDownPoint.Y);
            //                    double dx = Math.Abs(ProjX - pProjX);
            //                    double dy = Math.Abs(ProjY - pProjY);
            //                    double dist;
            //                    if (_mapView.Projection.IsLonLatMap)
            //                    {
            //                        double y = (ProjY + pProjY) / 2;
            //                        double factor = Math.Cos(y * Math.PI / 180);
            //                        dx *= factor;
            //                        dist = Math.Sqrt(dx * dx + dy * dy);
            //                        dist = dist * 111319.5;
            //                    }
            //                    else
            //                    {
            //                        dist = Math.Sqrt(dx * dx + dy * dy);
            //                        dist *= _mapView.Projection.ProjInfo.Unit.Meters;
            //                    }

            //                    _frmMeasure.CurrentValue = dist;
            //                }
            //                else
            //                {
            //                    List<PointD> mPoints = new List<PointD>();
            //                    for (int i = 0; i < points.Length; i++)
            //                    {
            //                        MapDocument.ActiveMapFrame.MapView.ScreenToProj(ref ProjX, ref ProjY,
            //                            points[i].X, points[i].Y);
            //                        mPoints.Add(new PointD(ProjX, ProjY));
            //                    }
            //                    double area = GeoComputation.GetArea(mPoints);
            //                    if (_mapView.Projection.IsLonLatMap)
            //                    {
            //                        area = area * 111319.5 * 111319.5;
            //                    }
            //                    else
            //                    {
            //                        area *= _mapView.Projection.ProjInfo.Unit.Meters * _mapView.Projection.ProjInfo.Unit.Meters;
            //                    }
            //                    _frmMeasure.CurrentValue = area;
            //                }
            //            }
            //            break;
            //    }
            //}
        }

        private void MapView_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && MapDocument.ActiveMapFrame.MapView.MouseTool == MouseTools.None)
            //{
            //    _mouseDownPoint = new Point(e.X, e.Y);

            //    if (_currentTool.Name == "TSB_Measurement" && _frmMeasure.IsHandleCreated)
            //    {
            //        switch (_frmMeasure.MeasureType)
            //        {
            //            case MeasureType.Length:
            //            case MeasureType.Area:
            //                if (_startNewGraphic)
            //                {
            //                    _graphicPoints = new List<PointF>();
            //                    _startNewGraphic = false;
            //                }
            //                _frmMeasure.PreviousValue = _frmMeasure.TotalValue;
            //                _graphicPoints.Add(new PointF(e.X, e.Y));
            //                break;
            //            case MeasureType.Feature:
            //                MapLayer aMLayer = MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(MapDocument.ActiveMapFrame.MapView.SelectedLayer);
            //                if (aMLayer != null)
            //                {
            //                    if (aMLayer.LayerType == LayerTypes.VectorLayer)
            //                    {
            //                        VectorLayer aLayer = (VectorLayer)aMLayer;
            //                        if (aLayer.ShapeType != ShapeTypes.Point)
            //                        {
            //                            List<int> SelectedShapes = new List<int>();
            //                            PointF aPoint = new PointF(e.X, e.Y);
            //                            if (MapDocument.ActiveMapFrame.MapView.SelectShapes(aLayer, aPoint, ref SelectedShapes))
            //                            {
            //                                Shape aShape = aLayer.ShapeList[SelectedShapes[0]];
            //                                MapDocument.ActiveMapFrame.MapView.Refresh();
            //                                MapDocument.ActiveMapFrame.MapView.DrawIdShape(
            //                                    MapDocument.ActiveMapFrame.MapView.CreateGraphics(),
            //                                    aShape);
            //                                double value = 0.0;
            //                                switch (aShape.ShapeType)
            //                                {
            //                                    case ShapeTypes.Polyline:
            //                                    case ShapeTypes.PolylineZ:
            //                                        _frmMeasure.IsArea = false;
            //                                        if (_mapView.Projection.IsLonLatMap)
            //                                            value = GeoComputation.GetDistance(((PolylineShape)aShape).Points, true);
            //                                        else
            //                                        {
            //                                            value = ((PolylineShape)aShape).Length;
            //                                            value *= _mapView.Projection.ProjInfo.Unit.Meters;
            //                                        }
            //                                        break;
            //                                    case ShapeTypes.Polygon:
            //                                        _frmMeasure.IsArea = true;
            //                                        if (_mapView.Projection.IsLonLatMap)
            //                                        {
            //                                            ProjectionInfo fromProj = KnownCoordinateSystems.Geographic.World.WGS1984;
            //                                            string toProjStr = "+proj=aea +lat_1=25 +lat_2=47 +lat_0=40 +lon_0=105";
            //                                            ProjectionInfo toProj = new ProjectionInfo(toProjStr);
            //                                            PolygonShape aPGS = (PolygonShape)((PolygonShape)aShape).Clone();
            //                                            aPGS = _mapView.Projection.ProjectPolygonShape(aPGS, fromProj, toProj);
            //                                            value = aPGS.Area;
            //                                        }
            //                                        else
            //                                            value = ((PolygonShape)aShape).Area;
            //                                        value *= _mapView.Projection.ProjInfo.Unit.Meters * _mapView.Projection.ProjInfo.Unit.Meters;
            //                                        break;
            //                                }
            //                                _frmMeasure.CurrentValue = value;
            //                            }
            //                        }
            //                    }
            //                }
            //                break;
            //        }
            //    }
            //}
            //else if (e.Button == MouseButtons.Right && MapDocument.ActiveMapFrame.MapView.MouseTool == MouseTools.None)
            //{
            //    if (_currentTool.Name == "TSB_Measurement" && _frmMeasure.IsHandleCreated)
            //    {
            //        switch (_frmMeasure.MeasureType)
            //        {
            //            case MeasureType.Length:
            //            case MeasureType.Area:
            //                _startNewGraphic = true;
            //                _frmMeasure.TotalValue = 0;
            //                break;
            //        }
            //    }
            //}
        }

        private void Layout_MouseMove(object sender, MouseEventArgs e)
        {
            Point pageP = MapDocument.MapLayout.ScreenToPage(e.X, e.Y);
            foreach (MapFrame mf in MapDocument.MapFrames)
            {
                Rectangle rect = mf.LayoutBounds;
                if (MIMath.PointInRectangle(pageP, rect))
                {
                    double ProjX, ProjY;
                    ProjX = 0;
                    ProjY = 0;
                    mf.MapView.ScreenToProj(ref ProjX, ref ProjY, pageP.X - rect.Left, pageP.Y - rect.Top, MapDocument.MapLayout.Zoom);
                    //ProjX = ProjX * MapDocument.MapLayout.Zoom;
                    //ProjY = ProjY / MapDocument.MapLayout.Zoom;
                    if (mf.MapView.Projection.IsLonLatMap)
                    {
                        this.TSSL_Coord.Text = "Lon: " + ProjX.ToString("0.00") + "; Lat: " + ProjY.ToString("0.00");
                    }
                    else
                    {
                        string theText = this.TSSL_Coord.Text = "X: " + ProjX.ToString("0.0") + "; Y: " + ProjY.ToString("0.0");
                        if (mf.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Robinson)
                            return;

                        ProjectionInfo toProj = KnownCoordinateSystems.Geographic.World.WGS1984;
                        ProjectionInfo fromProj = mf.MapView.Projection.ProjInfo;
                        double[][] points = new double[1][];
                        points[0] = new double[] { ProjX, ProjY };
                        //double[] Z = new double[1];
                        try
                        {
                            Reproject.ReprojectPoints(points, fromProj, toProj, 0, 1);
                            this.TSSL_Coord.Text = theText + " (Lon: " + points[0][0].ToString("0.00") + "; Lat: " +
                                points[0][1].ToString("0.00") + ")";
                        }
                        catch
                        {
                            //this.TSSL_Coord.Text = "X: " + ProjX.ToString("0.0") + "; Y: " + ProjY.ToString("0.0"); 
                        }
                    }

                    break;
                }
            }
        }

        private void Layout_ElementSelected(object sender, EventArgs e)
        {
            if (MapDocument.MapLayout.SelectedElements[0].ElementType == ElementType.LayoutGraphic)
            {
                switch (((LayoutGraphic)MapDocument.MapLayout.SelectedElements[0]).Graphic.Shape.ShapeType)
                {
                    case ShapeTypes.Polyline:
                    case ShapeTypes.Polygon:
                    case ShapeTypes.CurveLine:
                    case ShapeTypes.CurvePolygon:
                        TSB_EditVertices.Enabled = true;
                        break;
                    default:
                        TSB_EditVertices.Enabled = false;
                        break;
                }
            }
            else
                TSB_EditVertices.Enabled = false;
        }

        private void MapView_GraphicSelected(object sender, EventArgs e)
        {
            switch (_mapView.SelectedGraphics.GraphicList[0].Shape.ShapeType)
            {
                case ShapeTypes.Polyline:
                case ShapeTypes.CurveLine:
                case ShapeTypes.Polygon:
                case ShapeTypes.CurvePolygon:
                    TSB_EditVertices.Enabled = true;
                    break;
                default:
                    TSB_EditVertices.Enabled = false;
                    break;
            }
        }

        private void ActiveMapFrameChanged(object sender, EventArgs e)
        {
            _mapView = MapDocument.ActiveMapFrame.MapView;
            //MapDocument.ActiveMapFrame.MapView = _mapView;
            SetMapView();
            if (tabControl1.SelectedIndex == 0)
                _mapView.PaintLayers();
        }

        //private void MapFramesUpdated(object sender, EventArgs e)
        //{
        //    MapDocument.MapLayout.UpdateMapFrames(MapDocument.MapFrames);
        //    if (tabControl1.SelectedIndex == 1)
        //    {
        //        foreach (MapFrame aMF in MapDocument.MapLayout.MapFrames)
        //            aMF.IsFireMapViewUpdate = true;

        //        MapDocument.MapLayout.PaintGraphics();
        //    }
        //}

        //private void Layout_ActiveMapFrameChanged(object sender, EventArgs e)
        //{
        //    MapDocument.SetActiveMapFrame(MapDocument.MapLayout.ActiveMapFrame);
        //}

        //private void Layout_MapFramesUpdated(object sender, EventArgs e)
        //{
        //    MapDocument.MapFrames = MapDocument.MapLayout.MapFrames;
        //    MapDocument.Refresh();
        //}

        /// <summary>
        /// Fire the CurrentToolChanged event
        /// </summary>
        protected virtual void OnCurrentToolChanged()
        {
            if (CurrentToolChanged != null) CurrentToolChanged(this, new EventArgs());

            if (_currentTool.Name != "TSB_EditVertices")
            {
                TSB_EditVertices.Enabled = false;
                if (_isEditingVertices)
                {
                    if (tabControl1.SelectedIndex == 0)
                        _mapView.PaintLayers();
                    else
                        MapDocument.MapLayout.PaintGraphics();

                    _isEditingVertices = false;
                }
            }
        }

        private void MapLayout_ZoomChanged(object sender, EventArgs e)
        {
            TSCB_PageZoom.Text = (MapDocument.MapLayout.Zoom * 100).ToString("0") + "%";
        }

        private void frmMain_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Focused)
                this.Focus();
        }
        #endregion

        #region Methods

        #region Loading
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Set location and size
            this.Location = _options.MainFormLocation;
            this.Size = _options.MainFormSize;
            splitContainer1.SplitterDistance = 230;

            //Open MeteoData form
            if (_options.ShowStartMeteoDataDlg)
            {
                _frmMeteoData.Show(this);
                _frmMeteoData.Left = this.Left + 10;
                _frmMeteoData.Top = this.Bottom - _frmMeteoData.Height - 40;
            }

            //Set MapView properties
            _mapView.LockViewUpdate = true;
            MapDocument.IsLayoutView = false;
            _mapView.IsLayoutMap = false;
            _mapView.ZoomToExtent(_mapView.ViewExtent);
            _mapView.LockViewUpdate = false;
        }

        private void LoadForm_Script()
        {
            //Set window size            
            //this.Width = 1000;
            //this.Height = 650;
            //Set location and size
            this.Location = _options.MainFormLocation;
            this.Size = _options.MainFormSize;
            splitContainer1.SplitterDistance = 230;

            //Set controls
            TSMI_Edit.Visible = false;
            TSMI_LayoutProperty.Enabled = false;
            TSMI_InsertLegend.Enabled = false;
            TSMI_InsertTitle.Enabled = false;
            TSMI_InsertText.Enabled = false;
            TSMI_InsertNorthArrow.Enabled = false;
            TSMI_InsertScaleBar.Enabled = false;
            TSMI_InsertWindArraw.Enabled = false;
            ToolStrip_Layout.Enabled = false;
            TSB_EditVertices.Enabled = false;

            //View map and legend      
            TSMI_Layers.Checked = true;
            SetMapLayerPanel();

            //Set map title
            m_Title = Resources.GlobalResource.ResourceManager.GetString("Map_Title");

            //Set layout zoom combobox
            TSCB_PageZoom.Items.Clear();
            TSCB_PageZoom.Items.AddRange(new string[] { "20%", "50%", "75%", "100%", "150%", "200%", "300%" });
            TSCB_PageZoom.Text = (MapDocument.MapLayout.Zoom * 100).ToString() + "%";

            //Set maskout status label
            TSSL_MaskOut.Text = "MaskOut: No";

            //Set progressbar
            TSPB_Main.Visible = false;

            //Load default project file
            //this.Activate();
            //Application.DoEvents();
            //LoadDefaultPojectFile();
            //MapDocument.ActiveMapFrame.MapView = _mapView;
            _mapView = MapDocument.ActiveMapFrame.MapView;
            SetMapView();
            //MapDocument.MapLayout.MapView = _mapView;
            //SetMapLayout();

            //Set language menu
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                this.TSMI_LanEn.Enabled = false;
            }
            else
            {
                this.TSMI_LanCh.Enabled = false;
            }

            //Set current tool
            TSB_None.PerformClick();

            //Open MeteoData form
            _frmMeteoData = new frmMeteoData();
            //_frmMeteoData.Show(this);
            //_frmMeteoData.Left = this.Left + 10;
            //_frmMeteoData.Top = this.Bottom - _frmMeteoData.Height - 40;
        }

        private void LoadForm()
        {
            //Load options
            string optionFile = Path.Combine(Application.StartupPath, "Options.xml");
            _options.FileName = optionFile;
            _options.LoadFile(optionFile);

            ////Set toolstrips
            //ToolStrip_Main.Left = toolStripContainer1.Left;
            //ToolStrip_Main.Top = toolStripContainer1.Top;
            //ToolStrip_Layout.Left = ToolStrip_Main.Right;
            //ToolStrip_Edit.Left = ToolStrip_Layout.Right;

            //Set window size            
            //this.Width = 1000;
            //this.Height = 650;            

            //Set controls
            TSMI_Edit.Visible = false;
            TSMI_LayoutProperty.Enabled = false;
            TSMI_InsertLegend.Enabled = false;
            TSMI_InsertTitle.Enabled = false;
            TSMI_InsertText.Enabled = false;
            TSMI_InsertNorthArrow.Enabled = false;
            TSMI_InsertScaleBar.Enabled = false;
            TSMI_InsertWindArraw.Enabled = false;
            ToolStrip_Layout.Enabled = false;
            TSB_EditVertices.Enabled = false;

            //View map and legend      
            TSMI_Layers.Checked = true;
            SetMapLayerPanel();

            //Set map title
            m_Title = Resources.GlobalResource.ResourceManager.GetString("Map_Title");

            //Set layout zoom combobox
            TSCB_PageZoom.Items.Clear();
            TSCB_PageZoom.Items.AddRange(new string[] { "20%", "50%", "75%", "100%", "150%", "200%", "300%" });
            TSCB_PageZoom.Text = (MapDocument.MapLayout.Zoom * 100).ToString() + "%";

            //Set maskout status label
            TSSL_MaskOut.Text = "MaskOut: No";

            //Set progressbar
            TSPB_Main.Visible = false;

            //Load default project file
            this.Activate();
            Application.DoEvents();
            LoadDefaultPojectFile();
            ApplyResource(false);

            _mapView = MapDocument.ActiveMapFrame.MapView;
            SetMapView();            

            //Set language menu
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                this.TSMI_LanEn.Enabled = false;
            }
            else
            {
                this.TSMI_LanCh.Enabled = false;
            }

            //Set current tool
            TSB_None.PerformClick();

            //Open MeteoData form
            _frmMeteoData = new frmMeteoData();
            
            //Load plugins           
            string pluginConfigFile = Path.Combine(Application.StartupPath, "Plugin");
            pluginConfigFile = Path.Combine(pluginConfigFile, "Plugins.xml");
            _plugins.ConfigFile = pluginConfigFile;
            _plugins.LoadConfigFile(pluginConfigFile);
            this.LoadPlugins(_plugins);
        }

        private void SetMapLayerPanel()
        {
            //toolStripContainer1.ContentPanel.Controls.Add(splitContainer1);
            //splitContainer1.Dock = DockStyle.Fill;

            Panel m_PanelLayersHead = new Panel();
            Button B_Head = new Button();

            //m_PanelLayersHead.SuspendLayout();
            m_PanelLayersHead.BorderStyle = BorderStyle.None;
            m_PanelLayersHead.Controls.Add(B_Head);
            m_PanelLayersHead.Controls.Add(Lab_Head);
            m_PanelLayersHead.Dock = DockStyle.Top;
            m_PanelLayersHead.Height = Lab_Head.Height;
            m_PanelLayersHead.BackColor = Lab_Head.BackColor;

            Lab_Head.Dock = DockStyle.Left;
            Lab_Head.Font = new Font("SimSun", 9F);
            //Lab_Head.Text = "Layers";
            Lab_Head.Text = Resources.GlobalResource.ResourceManager.GetString("Lab_Head_Text");
            //Lab_Head.Height = 24;

            B_Head.Text = "X";
            B_Head.Font = new Font("Verdana", 6F);
            B_Head.FlatStyle = FlatStyle.Popup;
            B_Head.Dock = DockStyle.Right;
            B_Head.Width = Lab_Head.Height;
            //B_Head.Size = new Size(24, 24);
            B_Head.Click += new EventHandler(B_Head_Click);

            //m_PanelLayers.Controls.Add(m_PanelLayersHead);
            //m_PanelLayers.Controls.Add(G_LayersTV);
            splitContainer1.Panel1.Controls.Add(m_PanelLayersHead);
            splitContainer1.Panel1.Controls.Add(MapDocument);
            MapDocument.Top = splitContainer1.Panel1.Top + 24;
            MapDocument.Width = splitContainer1.Panel1.Width;
            MapDocument.Height = splitContainer1.Panel1.Height - 24;
            MapDocument.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            MapDocument.Select();
            MapFrame mf = new MapFrame();
            mf.Active = true;
            MapDocument.AddMapFrame(mf);
            //MapDocument.MapLayout = MapDocument.MapLayout;
            MapDocument.ActiveMapFrameChanged += new EventHandler(ActiveMapFrameChanged);
            //MapDocument.MapFramesUpdated += new EventHandler(MapFramesUpdated);

            //Add map view
            //SetMapView();

            //Add map layout            
            tabControl1.TabPages[1].Controls.Add(MapDocument.MapLayout);
            MapDocument.MapLayout.Dock = DockStyle.Fill;
            MapDocument.MapLayout.MouseMove += new MouseEventHandler(this.Layout_MouseMove);
            MapDocument.MapLayout.ElementSeleted += new EventHandler(this.Layout_ElementSelected);
            MapDocument.MapLayout.ZoomChanged += new EventHandler(this.MapLayout_ZoomChanged);
            //MapDocument.MapLayout.ActiveMapFrameChanged += new EventHandler(Layout_ActiveMapFrameChanged);
            //MapDocument.MapLayout.MapFramesUpdated += new EventHandler(Layout_MapFramesUpdated);            
        }

        private void SetMapView()
        {
            //Add map view
            //splitContainer1.Panel2.Controls.Add(_mapView); 
            tabControl1.TabPages[0].Controls.Clear();
            tabControl1.TabPages[0].Controls.Add(_mapView);
            _mapView.Dock = DockStyle.Fill;
            //_mapView.RefreshXYScale();
            _mapView.MouseMove += new MouseEventHandler(this.MapView_MouseMove);
            _mapView.MouseDown += new MouseEventHandler(this.MapView_MouseDown);
            _mapView.GraphicSeleted += new EventHandler(this.MapView_GraphicSelected);
            //_mapView.MouseUp += new MouseEventHandler(this.MapView_MouseUp);
            //_mapView.MouseDoubleClick += new MouseEventHandler(this.MapView_MouseDoubleClick);
            //_mapView.KeyDown += new KeyEventHandler(this.MapView_KeyDown);
        }

        private void SetMapLayout()
        {
            MapDocument.MapLayout.MapFrames = MapDocument.MapFrames;
        }

        public void LoadDefaultPojectFile()
        {
            //Open default project file            
            string aFile = Application.StartupPath + "\\Default.mip";
            if (!File.Exists(aFile))
            {
                aFile = Application.StartupPath + "/Default.mip";
            }
            if (File.Exists(aFile))
            {
                _projectFile.LoadProjFile(aFile);
                this.Text = "MeteoInfo - " + Path.GetFileNameWithoutExtension(aFile);
            }
        }

        public void LoadProjectFile(string pFile)
        {
            if (File.Exists(pFile))
            {
                _projectFile.LoadProjFile(pFile);
                this.Text = "MeteoInfo - " + Path.GetFileNameWithoutExtension(pFile);
            }
        }

        private void PluginLoaded(object sender, System.EventArgs e)
        {
            ToolStripMenuItem aMenuItem = (ToolStripMenuItem)sender;
            string pluginName = aMenuItem.Text;
            for (int i = 0; i < _plugins.Count; i++)
            {
                if (_plugins[i] == null) return;
                if (_plugins[i].Name == pluginName)
                {
                    Plugin plugin = _plugins[i];
                    if (!plugin.Loaded)
                    {
                        //Assembly ass = Assembly.LoadFile(plugin.DllFileName);
                        //Type type = ass.GetType(plugin.ClassName);
                        //IPlugin pluginObj = (IPlugin)Activator.CreateInstance(type);
                        //pluginObj.Application = this;
                        //plugin.PluginObject = pluginObj;
                        //plugin.Load();
                        //plugin.Loaded = true;
                        //aMenuItem.Checked = true;
                        this.LoadPlugin(plugin);
                    }
                    else
                    {
                        //plugin.Unload();
                        //plugin.Loaded = false;
                        //aMenuItem.Checked = false;
                        this.UnloadPlugin(plugin);
                    }
                    break;
                }
            }
        }

        private void LoadPlugins()
        {
            //Search all dll   
            string path = Application.StartupPath + "\\Plugin";
            if (!Directory.Exists(path))
                return;

            List<string> pluginFiles = GlobalUtil.GetAllFilesByFolder(path, "*.dll", true);            
            for (int i = 0; i < pluginFiles.Count; i++)
            {
                //No suffix  
                string args = pluginFiles[i];
                try
                {
                    //Load plugin   
                    Assembly ass = Assembly.LoadFile(args);
                    if (ass != null)
                    {
                        //Search plugin type   
                        Type[] types = ass.GetTypes();
                        foreach (Type type in types)
                        {
                            if (type.GetInterface("IPlugin") != null)
                            {
                                IPlugin aPlugin = (IPlugin)Activator.CreateInstance(type);                                
                                aPlugin.Application = this;
                                Plugin plugin = new Plugin();
                                plugin.DllFileName = args;
                                plugin.PluginObject = aPlugin;
                                plugin.ClassName = type.FullName;
                                _plugins.Add(plugin);
                                ToolStripMenuItem aMenuItem = new ToolStripMenuItem();
                                aMenuItem.Text = aPlugin.Name;
                                aMenuItem.Click += new EventHandler(PluginLoaded);
                                ((ToolStripMenuItem)MenuStrip_Main.Items["TSMI_Plugin"]).DropDownItems.Add(aMenuItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LoadPlugins(List<Plugin> plugins)
        {
            foreach (Plugin plugin in plugins)
            {
                this.AddPlugin(plugin);
            }
        }

        /// <summary>
        /// Read a plugin from a dll file
        /// </summary>
        /// <param name="dllFileName">The dll file name</param>
        /// <returns>The plugin</returns>
        public Plugin ReadPlugin(string dllFileName)
        {
            Plugin plugin = null;
            //Load plugin   
            try
            {
                Assembly ass = Assembly.LoadFile(dllFileName);
                if (ass != null)
                {
                    //Search plugin type   
                    Type[] types = ass.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.GetInterface("IPlugin") != null)
                        {
                            IPlugin aPlugin = (IPlugin)Activator.CreateInstance(type);
                            aPlugin.Application = this;
                            plugin = new Plugin();
                            plugin.DllFileName = dllFileName;
                            plugin.PluginObject = aPlugin;
                            plugin.ClassName = type.FullName;
                            //_plugins.Add(plugin);
                            //ToolStripMenuItem aMenuItem = new ToolStripMenuItem();
                            //aMenuItem.Text = aPlugin.Name;
                            //aMenuItem.Click += new EventHandler(PluginLoaded);
                            //((ToolStripMenuItem)MenuStrip_Main.Items["TSMI_Plugin"]).DropDownItems.Add(aMenuItem);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }            

            return plugin;
        }

        /// <summary>
        /// Load a plugin
        /// </summary>
        /// <param name="plugin">The plugin</param>
        public void LoadPlugin(Plugin plugin)
        {            
            try
            {
                //Load plugin   
                Assembly ass = Assembly.LoadFile(plugin.DllFileName);
                if (ass != null)
                {
                    //Search plugin type   
                    Type type = ass.GetType(plugin.ClassName);
                    IPlugin pluginObj = (IPlugin)Activator.CreateInstance(type);
                    pluginObj.Application = this;                    
                    plugin.PluginObject = pluginObj;
                    pluginObj.Load();
                    plugin.Loaded = true;

                    ToolStripMenuItem aMenuItem = FindPluginMenuItem(plugin.Name);
                    //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    Stream myStream = this.GetType().Assembly.GetManifestResourceStream("MeteoInfo.Resources.plugin_green.png");
                    Bitmap image = new Bitmap(myStream);
                    //aMenuItem.Checked = true;
                    aMenuItem.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Add a plugin
        /// </summary>
        /// <param name="plugin">The plugin</param>
        public void AddPlugin(Plugin plugin)
        {
            if (plugin == null)
                return;

            //Add menu item
            ToolStripMenuItem aMenuItem = new ToolStripMenuItem();
            aMenuItem.Text = plugin.Name;
            //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = this.GetType().Assembly.GetManifestResourceStream("MeteoInfo.Resources.plugin_unsel.png");
            Bitmap image = new Bitmap(myStream);
            aMenuItem.Image = image;
            aMenuItem.Click += new EventHandler(PluginLoaded);
            ((ToolStripMenuItem)MenuStrip_Main.Items["TSMI_Plugin"]).DropDownItems.Add(aMenuItem);

            //Load plugin
            if (plugin.Loaded)
                this.LoadPlugin(plugin);
        }

        /// <summary>
        /// Remove a plugin
        /// </summary>
        /// <param name="plugin">The plugin</param>
        public void RemovePlugin(Plugin plugin)
        {
            if (plugin.Loaded)
                UnloadPlugin(plugin);

            ToolStripMenuItem aMI = this.FindPluginMenuItem(plugin.Name);
            if (aMI != null)
            {
                MenuStrip_Main.Items.Remove(aMI);
            }
        }

        /// <summary>
        /// Unload a plugin
        /// </summary>
        /// <param name="plugin">The plugin</param>
        public void UnloadPlugin(Plugin plugin)
        {           
            plugin.PluginObject.UnLoad();
            plugin.Loaded = false;

            ToolStripMenuItem aMenuItem = FindPluginMenuItem(plugin.Name);
            //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = this.GetType().Assembly.GetManifestResourceStream("MeteoInfo.Resources.plugin_unsel.png");
            Bitmap image = new Bitmap(myStream);
            aMenuItem.Image = image;
            //aMenuItem.Checked = false; 
        }

        private ToolStripMenuItem FindPluginMenuItem(string name)
        {
            ToolStripMenuItem pluginMI = ((ToolStripMenuItem)MenuStrip_Main.Items["TSMI_Plugin"]);
            for (int i = 0; i < pluginMI.DropDownItems.Count; i++)
            {
                if (pluginMI.DropDownItems[i].Text == name)
                    return (ToolStripMenuItem)pluginMI.DropDownItems[i];
            }

            return null;
        }

        /// <summary>
        /// Apply language resource
        /// </summary>
        /// <param name="setMeteoDataForm">if set meteo data form</param>
        public void ApplyResource(bool setMeteoDataForm)
        {
            ComponentResourceManager res = new ComponentResourceManager(typeof(frmMain));

            //Controls
            foreach (Control ctl in Controls)
            {
                res.ApplyResources(ctl, ctl.Name);                
            }

            Lab_Head.Text = Resources.GlobalResource.ResourceManager.GetString("Lab_Head_Text");
            tabControl1.TabPages[0].Text = Resources.GlobalResource.ResourceManager.GetString("Map_Text");
            tabControl1.TabPages[1].Text = Resources.GlobalResource.ResourceManager.GetString("Layout_Text");

            //Menuitems            
            foreach (ToolStripMenuItem item in this.MenuStrip_Main.Items)
            {
                res.ApplyResources(item, item.Name);
                if (item.DropDownItems.Count > 0)
                {
                    for (int i = 0; i < item.DropDownItems.Count; i++)
                    {
                        if (item.DropDownItems[i].GetType() == typeof(System.Windows.Forms.ToolStripMenuItem))
                        {
                            ToolStripMenuItem aMI = (ToolStripMenuItem)item.DropDownItems[i];
                            res.ApplyResources(aMI, aMI.Name);
                            if (aMI.DropDownItems.Count > 0)
                            {
                                foreach (ToolStripMenuItem bMI in aMI.DropDownItems)
                                {
                                    res.ApplyResources(bMI, bMI.Name);
                                }
                            }
                        }
                    }
                }
            }

            //Toolstrip
            foreach (ToolStripItem ctl in this.ToolStrip_Main.Items)
            {
                res.ApplyResources(ctl, ctl.Name);                
                if (ctl.GetType() == typeof(MeteoInfoC.Global.ToolStripSplitButtonCheckable))
                {
                    ToolStripSplitButtonCheckable splitButton = (ToolStripSplitButtonCheckable)ctl;
                    foreach (ToolStripItem tsmi in splitButton.DropDownItems)
                    {
                        res.ApplyResources(tsmi, tsmi.Name);
                    }
                }
            }
            foreach (ToolStripItem ctl in this.ToolStrip_Layout.Items)
                res.ApplyResources(ctl, ctl.Name);
            foreach (ToolStripItem ctl in this.ToolStrip_Edit.Items)
                res.ApplyResources(ctl, ctl.Name);

            //Caption
            //res.ApplyResources(this, "$this");

            ////Set window size
            //this.Width = 1000;
            //this.Height = 650;

            ////Set Coordinate
            //m_LonLatScreenSet.SetCoordinate(0, 360, -90, 90, PicBox_Layout.Width, PicBox_Layout.Height);

            //Set map title
            m_Title = Resources.GlobalResource.ResourceManager.GetString("Map_Title");

            //Set meteo data form
            res = new ComponentResourceManager(typeof(frmMeteoData));
            if (setMeteoDataForm)
            {
                //MeteoData form
                if (_frmMeteoData != null)
                {
                    //_frmMeteoData.ApplyResource();
                    _frmMeteoData.Dispose();
                    _frmMeteoData = new frmMeteoData();
                    _frmMeteoData.Show(this);
                }
            }
        }

        #endregion

        #region Scripting
        /// <summary>
        /// Run script
        /// </summary>
        /// <param name="aFile">script file</param>
        public void RunScript(string aFile)
        {
            ScriptEngine scriptEngine = Python.CreateEngine();
            ScriptScope pyScope = scriptEngine.CreateScope();

            //Set path
            string path = Assembly.GetExecutingAssembly().Location;
            string rootDir = Directory.GetParent(path).FullName;
            List<string> paths = new List<string>();
            paths.Add(rootDir);
            paths.Add(Path.Combine(rootDir, "Lib"));
            scriptEngine.SetSearchPaths(paths.ToArray());

            pyScope.SetVariable("mipy", this);                    

            //Run python script
            try
            {
                //ScriptSource sourceCode = scriptEngine.CreateScriptSourceFromString(text, SourceCodeKind.Statements);
                ScriptSource sourceCode = scriptEngine.CreateScriptSourceFromFile(aFile);
                //sourceCode.Execute(pyScope);
                //CompiledCode compiled = sourceCode.Compile();
                //compiled.Execute(pyScope); 
                sourceCode.ExecuteProgram();
            }
            catch (Exception e)
            {
                ExceptionOperations eo = scriptEngine.GetService<ExceptionOperations>();
                Console.Write(eo.FormatException(e));
            }
        }

        #endregion

        #region Other

        private void B_Head_Click(object sender, System.EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            TSMI_Layers.Checked = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ifSave = MessageBox.Show("If save the project?", "Confirm", MessageBoxButtons.YesNoCancel);
            switch (ifSave)
            {
                case DialogResult.Yes:
                case DialogResult.No:
                    if (ifSave == DialogResult.Yes)
                    {
                        _projectFile.SaveProjFile(_projectFile.FileName);
                    }
                    _options.MainFormLocation = this.Location;
                    _options.MainFormSize = this.Size;
                    _options.SaveFile();
                    _plugins.SaveConfigFile(_plugins.ConfigFile);
                    IsClosed = true;                    
                    this.Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    _frmMeteoData.Show(this);
                    break;
            }            
        }        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)    //Map Layout
            {
                MapDocument.IsLayoutView = true;

                //foreach (MapFrame aMF in MapDocument.MapFrames)
                //    aMF.IsFireMapViewUpdate = true;
                //if (MapDocument.MapLayout.HasLegendElement)
                //    MapDocument.MapLayout.ActiveLayoutMap.FireMapViewUpdatedEvent();

                ToolStrip_Layout.Enabled = true;
                TSMI_LayoutProperty.Enabled = true;
                TSMI_InsertLegend.Enabled = true;
                TSMI_InsertTitle.Enabled = true;
                TSMI_InsertText.Enabled = true;
                TSMI_InsertNorthArrow.Enabled = true;
                TSMI_InsertScaleBar.Enabled = true;
                TSMI_InsertWindArraw.Enabled = true;

                MapDocument.MapLayout.PaintGraphics();
                MapDocument.MapLayout.Refresh();
            }
            else if (tabControl1.SelectedIndex == 0)    //Map view
            {
                MapDocument.IsLayoutView = false;

                //foreach (MapFrame aMF in MapDocument.MapFrames)
                //    aMF.IsFireMapViewUpdate = false;

                ToolStrip_Layout.Enabled = false;
                TSMI_LayoutProperty.Enabled = false;
                TSMI_InsertLegend.Enabled = false;
                TSMI_InsertTitle.Enabled = false;
                TSMI_InsertText.Enabled = false;
                TSMI_InsertNorthArrow.Enabled = false;
                TSMI_InsertScaleBar.Enabled = false;
                TSMI_InsertWindArraw.Enabled = false;

                _mapView.IsLayoutMap = false;
                _mapView.ZoomToExtent(_mapView.ViewExtent);
            }
        }

        #endregion

        #region ToolStrip_Main
        private void TSB_None_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.SelectElements;
            MapDocument.MapLayout.MouseMode = MouseMode.Select;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_ZoomIn_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.Zoom_In;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_ZoomIn;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_ZoomOut_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.Zoom_Out;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_ZoomOut;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_Pan_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.Pan;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_Pan;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_FullExent_Click(object sender, EventArgs e)
        {
            Extent aExtent = _mapView.Extent;
            //if (MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
            //{
            //    aExtent.minX = 0;
            //    aExtent.maxX = 360;
            //    aExtent.minY = -90;
            //    aExtent.maxY = 90;
            //}

            _mapView.ZoomToExtent(aExtent);
        }

        private void TSB_ZoomToLayer_Click(object sender, EventArgs e)
        {
            if (MapDocument.SelectedNode == null)
                return;

            if (MapDocument.SelectedNode.NodeType == NodeTypes.LayerNode)
            {
                MapFrame aMF = MapDocument.CurrentMapFrame;
                MapLayer aLayer = ((LayerNode)MapDocument.SelectedNode).MapLayer;
                if (aLayer != null)
                    aMF.MapView.ZoomToExtent(aLayer.Extent);
            }
        }

        private void TSB_SavePicture_Click(object sender, EventArgs e)
        {
            SaveFileDialog aDlg = new SaveFileDialog();
            aDlg.Filter = "Png Image (*.png)|*.png|Gif Image (*.gif)|*.gif|Jpeg Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|Tif Image (*.tif)|*.tif|Windows Meta File (*.wmf)|*.wmf";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    _mapView.ExportToPicture(aDlg.FileName);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    //MapDocument.MapLayout.ExportToPicture(aDlg.FileName, 600);
                    MapDocument.MapLayout.ExportToPicture(aDlg.FileName);
                }
            }
        }

        private void TSB_OpenMap_Click(object sender, EventArgs e)
        {
            frmOpenMap frmOM = new frmOpenMap();
            frmOM.Show(this);
        }

        private void TSB_OpenData_Click(object sender, EventArgs e)
        {
            if (!_frmMeteoData.Visible)
            {
                _frmMeteoData.Show(this);
                _frmMeteoData.Left = this.Left + 10;
                _frmMeteoData.Top = this.Bottom - _frmMeteoData.Height - 40;
                _options.ShowStartMeteoDataDlg = true;
            }
            //frmMeteoData aFrmMD = new frmMeteoData();
            //aFrmMD.Show(this);
        }

        private void TSB_RemoveDataLayes_Click(object sender, EventArgs e)
        {
            MapDocument.ActiveMapFrame.RemoveMeteoLayers();
        }

        private void TSB_Identifer_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.Identifer;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_Identifer;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_SelectFeatures_Click(object sender, EventArgs e)
        {
            SetCurrentTool((ToolStripSplitButtonCheckable)sender);
        }

        private void TSMI_SelByRectangle_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.SelectFeatures_Rectangle;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_SelectFeatures_Rectangle;

            //SetCurrentTool((ToolStripSplitButtonCheckable)sender);
        }

        private void TSMI_SelByPolygon_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.SelectFeatures_Polygon;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_SelectFeatures_Polygon;
        }

        private void TSMI_SelByLasso_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.SelectFeatures_Lasso;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_SelectFeatures_Lasso;
        }

        private void TSMI_SelByCircle_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.SelectFeatures_Circle;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_SelectFeatures_Circle;
        }

        private void TSB_ZoomToExtent_Click(object sender, EventArgs e)
        {
            frmZoomToExtent aFrmZTE = new frmZoomToExtent(!(tabControl1.SelectedIndex == 0));
            aFrmZTE.Show(this);
        }

        private void TSB_LabelSet_Click(object sender, EventArgs e)
        {
            if (MapDocument.SelectedNode == null)
                return;

            if (MapDocument.SelectedNode.NodeType == NodeTypes.LayerNode)
            {
                LayerNode aLN = (LayerNode)MapDocument.SelectedNode;
                MapLayer aMLayer = aLN.MapFrame.MapView.GetLayerFromHandle(aLN.LayerHandle);
                if (aMLayer.LayerType == LayerTypes.VectorLayer)
                {
                    VectorLayer aLayer = (VectorLayer)aMLayer;
                    if (aLayer.ShapeNum > 0)
                    {
                        frmLabelSet aFrmLabel = new frmLabelSet(MapDocument.ActiveMapFrame.MapView);
                        aFrmLabel.Layer = aLayer;
                        aFrmLabel.Show(this);
                    }
                }
            }
        }

        #endregion

        #region MainMenu
        private void TSMI_LanEn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en");
            ApplyResource(true);

            TSMI_LanEn.Enabled = false;
            TSMI_LanCh.Enabled = true;
        }

        private void TSMI_LanCh_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("zh-CN");
            ApplyResource(true);

            TSMI_LanCh.Enabled = false;
            TSMI_LanEn.Enabled = true;
        }

        private void TSMI_SaveProjetFile_Click(object sender, EventArgs e)
        {
            string aFile = _projectFile.FileName;

            _projectFile.SaveProjFile(aFile);
        }

        private void TSMI_Layers_Click(object sender, EventArgs e)
        {
            if (TSMI_Layers.Checked)
            {
                this.splitContainer1.Panel1Collapsed = true;
                TSMI_Layers.Checked = false;
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = false;
                TSMI_Layers.Checked = true;
            }
        }

        private void TSMI_OutputMapData_Click(object sender, EventArgs e)
        {
            frmOutputMapData aFrmCS = new frmOutputMapData();

            aFrmCS.Show(this);
        }

        private void TSMI_About_Click(object sender, EventArgs e)
        {
            frmAbout aboutFrm = new frmAbout();
            aboutFrm.ShowDialog();
        }

        private void TSMI_Help_Click(object sender, EventArgs e)
        {
            string aFile;
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                aFile = Application.StartupPath + "\\Help\\MeteoInfo_Help.chm";
                if (!File.Exists(aFile))
                {
                    aFile = Application.StartupPath + "/Help/MeteoInfo_Help.chm";
                }
            }
            else
            {
                aFile = Application.StartupPath + "\\Help\\MeteoInfo_Help_Chinese.chm";
                if (!File.Exists(aFile))
                {
                    aFile = Application.StartupPath + "/Help/MeteoInfo_Help_Chinese.chm";
                }
            }
            if (File.Exists(aFile))
            {
                System.Diagnostics.Process.Start(aFile);
            }
        }

        private void TSMI_Update_Click(object sender, EventArgs e)
        {
            frmMeteoInfoWeb aFrmWeb = new frmMeteoInfoWeb();
            aFrmWeb.Show();
        }

        private void TSMI_JoinNetCDFData_Click(object sender, EventArgs e)
        {
            frmJoinData aFJD = new frmJoinData();
            aFJD.Show(this);
        }

        private void TSMI_MICAPS2GrADS_Click(object sender, EventArgs e)
        {
            frmMICAPS2GrADS aFM2G = new frmMICAPS2GrADS();
            aFM2G.Show(this);
        }

        private void TSMI_SaveAsProject_Click(object sender, EventArgs e)
        {
            SaveFileDialog aDlg = new SaveFileDialog();
            aDlg.Filter = "MeteoInfo Project File (*.mip)|*.mip";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                string aFile = aDlg.FileName;

                _projectFile.SaveProjFile(aFile);
                this.Text = "MeteoInfo - " + Path.GetFileNameWithoutExtension(aFile);
            }
        }

        private void TSMI_OpenProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "MeteoInfo Project File (*.mip)|*.mip";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                string aFile = aDlg.FileName;
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;                 
                OpenProjectFile(aFile);
                if (ci.Equals(System.Threading.Thread.CurrentThread.CurrentCulture))
                {
                    ApplyResource(false);
                }
                else
                {
                    ApplyResource(true);
                }
            }
        }

        /// <summary>
        /// Open project file
        /// </summary>
        /// <param name="projFile">project file</param>
        public void OpenProjectFile(string projFile)
        {
            foreach (MapFrame mf in MapDocument.MapFrames)
            {
                if (mf.MapView.LayerSet.Layers.Count > 0)
                    mf.RemoveAllLayers();
            }
            Application.DoEvents();
            _projectFile.LoadProjFile(projFile);
            _mapView = MapDocument.ActiveMapFrame.MapView;
            SetMapView();
            SetMapLayout();

            this.Text = "MeteoInfo - " + Path.GetFileNameWithoutExtension(projFile);
            MapDocument.ActiveMapFrame.MapView.ZoomToExtent(MapDocument.ActiveMapFrame.MapView.ViewExtent);
            MapDocument.Invalidate();
        }

        /// <summary>
        /// Create a new Project
        /// </summary>
        public void NewProject()
        {
            MapDocument.Initialize();
            this.Text = "MeteoInfo - New Project";
        }

        private void TSMI_AttriData_Click(object sender, EventArgs e)
        {
            if (MapDocument.SelectedNode == null)
                return;

            if (MapDocument.SelectedNode.NodeType == NodeTypes.LayerNode)
            {
                LayerNode aLN = (LayerNode)MapDocument.SelectedNode;
                MapLayer aLayer = aLN.MapFrame.MapView.GetLayerFromHandle(aLN.LayerHandle);
                //MapLayer aLayer = MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(MapDocument.SelectedLayer);
                if (aLayer.LayerType == LayerTypes.VectorLayer)
                {
                    if (((VectorLayer)aLayer).ShapeNum > 0)
                    {
                        frmAttriData aFrmData = new frmAttriData((VectorLayer)aLayer);
                        aFrmData.Show(this);
                    }
                }
            }
        }

        private void TSMI_SPlitHourly_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog aDlg = new FolderBrowserDialog();
            aDlg.Description = "Select data input folder:";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                FolderBrowserDialog bDlg = new FolderBrowserDialog();
                bDlg.Description = "Select data output folder:";
                if (bDlg.ShowDialog() == DialogResult.OK)
                {
                    //Show progressbar
                    Application.DoEvents();
                    this.TSPB_Main.Value = 0;
                    this.TSPB_Main.Visible = true;
                    this.Cursor = Cursors.WaitCursor;

                    //Read and write file                
                    string aDir = aDlg.SelectedPath;
                    string[] files = Directory.GetFiles(aDir);
                    string bDir = bDlg.SelectedPath;
                    for (int i = 0; i < files.Length; i++)
                    {
                        string aFile = files[i];
                        StreamReader sr = new StreamReader(aFile);
                        string aLine = sr.ReadLine();
                        int n = 0;
                        int fNum = 1;
                        string tStr, preTStr = "";
                        string outFile;
                        tStr = aLine.Substring(15, 10);
                        preTStr = tStr;
                        //outFile = bDir + "\\ISH_" + preTStr + ".txt";
                        outFile = Path.Combine(bDir, "ISH_" + preTStr + ".txt");
                        StreamWriter sw = new StreamWriter(outFile, true);
                        sw.WriteLine(aLine);
                        aLine = sr.ReadLine();
                        while (aLine != null)
                        {
                            if (aLine == "")
                            {
                                aLine = sr.ReadLine();
                                continue;
                            }

                            tStr = aLine.Substring(15, 10);
                            if (tStr != preTStr)
                            {
                                sw.Close();
                                //if (fNum == 10)
                                //    break;

                                preTStr = tStr;
                                //outFile = bDir + "\\ISH_" + preTStr + ".txt";
                                outFile = Path.Combine(bDir, "ISH_" + preTStr + ".txt");
                                sw = new StreamWriter(outFile, true);
                                sw.WriteLine(aLine);
                                fNum += 1;
                            }
                            else
                            {
                                sw.WriteLine(aLine);
                            }
                            aLine = sr.ReadLine();
                            n += 1;
                        }
                        sw.Close();
                        sr.Close();

                        this.TSPB_Main.Value = (int)((double)(i + 1) / (double)files.Length * 100);
                        Application.DoEvents();
                    }

                    //Hide progressbar
                    this.TSPB_Main.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void TSMI_Legend_Click(object sender, EventArgs e)
        {
            //object propObj = MapDocument.MapLayout.DefaultLegend.GetPropertyObject();
            //MeteoInfoC.frmProperty aFrmPT = new MeteoInfoC.frmProperty(true);
            //aFrmPT.Text = "Legend Set";
            //aFrmPT.SetObject(propObj);
            //aFrmPT.SetParent(MapDocument.MapLayout);
            //aFrmPT.ShowDialog();

            MapDocument.MapLayout.AddLegend(100, 100);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSMI_ConvertFromOMIAI_Click(object sender, EventArgs e)
        {
            frmOMIAI2GrADS aFrmConvert = new frmOMIAI2GrADS();
            aFrmConvert.ShowDialog(this);
        }

        private void TSMI_MICAPS4To_Click(object sender, EventArgs e)
        {
            frmMICAPS4To aFrm = new frmMICAPS4To();
            aFrm.Show(this);
        }

        private void TSMI_Title_Click(object sender, EventArgs e)
        {
            LayoutGraphic title = MapDocument.MapLayout.AddText("Map Title", MapDocument.MapLayout.Width / 2, 20, 12);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSMI_NewPointLayer_Click(object sender, EventArgs e)
        {
            MeteoInfo.Forms.frmInputBox inputBox = new MeteoInfo.Forms.frmInputBox("Please Input Layer's Name:", "Layer Name", "NewPointLayer");
            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                string layerName = inputBox.Value;
                VectorLayer aLayer = new VectorLayer(ShapeTypes.Point);
                aLayer.LayerName = layerName;
                aLayer.LegendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Black, 5);
                MapDocument.ActiveMapFrame.AddLayer(aLayer);
            }
        }

        private void TSMI_NewPolylineLayer_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_NewPolygonLayer_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_AddXYData_Click(object sender, EventArgs e)
        {
            frmAddXYLayer aFrmAddXYLayer = new frmAddXYLayer();
            aFrmAddXYLayer.ShowDialog();
        }

        private void TSMI_LoadLayoutFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "Layout File (*.lay)|*.lay";
            //aDlg.InitialDirectory = Path.Combine(Application.StartupPath, "Config");
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MapDocument.MapLayout.LoadXMLFile(aDlg.FileName);
                if (tabControl1.SelectedIndex == 1)
                    MapDocument.MapLayout.PaintGraphics();
            }
        }

        private void TSMI_SaveLayoutFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog aDlg = new SaveFileDialog();
            aDlg.Filter = "Layout File (*.lay)|*.lay";
            //aDlg.InitialDirectory = Path.Combine(Application.StartupPath, "Config");
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MapDocument.MapLayout.SaveXMLFile(aDlg.FileName);
            }
        }

        private void TSMI_Test_Click(object sender, EventArgs e)
        {
            VectorLayer aLayer = _mapView.LonLatProjLayer;
            //VectorLayer aLayer = (VectorLayer)MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(MapDocument.SelectedLayer);
            //ClipLine clipLine = new ClipLine();
            //clipLine.IsLon = false;
            //clipLine.Value = -80;
            //clipLine.IsLeftOrTop = true;

            VectorLayer newLayer = GeoComputation.ClipLayer(aLayer, _mapView.ViewExtent);
            MapDocument.ActiveMapFrame.AddLayer(newLayer);
            MapDocument.ActiveMapFrame.MapView.PaintLayers();
        }

        private void TSMI_SelectByAttributes_Click(object sender, EventArgs e)
        {
            frmSelectByAttributes aFrm = new frmSelectByAttributes();
            aFrm.Show(this);
        }

        private void TSMI_SelectByLocation_Click(object sender, EventArgs e)
        {
            frmSelectByLocation aFrm = new frmSelectByLocation();
            aFrm.Show(this);
        }

        private void TSMI_ClearSelectedFeatures_Click(object sender, EventArgs e)
        {
            MapLayer aLayer = MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(MapDocument.ActiveMapFrame.MapView.SelectedLayer);
            if (aLayer.LayerType == LayerTypes.VectorLayer)
            {
                ((VectorLayer)aLayer).ClearSelectedShapes();
            }

            MapDocument.ActiveMapFrame.MapView.PaintLayers();
        }

        private void TSMI_InsertMapFrame_Click(object sender, EventArgs e)
        {
            MapFrame aMF = new MapFrame();
            aMF.Text = MapDocument.GetNewMapFrameName();
            MapDocument.AddMapFrame(aMF);
            MapDocument.Refresh();
        }

        private void TSMI_LayoutProperty_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.SetProperties();
        }

        private void TSMI_InsertText_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.AddText("Text", MapDocument.MapLayout.Width / 2, 200);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSMI_MaskOut_Click(object sender, EventArgs e)
        {
            //Get polygon map layer list
            List<string> aList = new List<string>();
            for (int i = 0; i < MapDocument.ActiveMapFrame.MapView.LayerSet.Layers.Count; i++)
            {
                if (MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i].GetType() == typeof(VectorLayer))
                {
                    VectorLayer aLayer = (VectorLayer)MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i];
                    if (aLayer.LayerDrawType == LayerDrawType.Map && aLayer.ShapeType == ShapeTypes.Polygon)
                    {
                        if (aLayer.ShapeNum < 1000)
                        {
                            aList.Add(aLayer.LayerName);
                        }
                    }
                }
            }
            MapDocument.ActiveMapFrame.MapView.MaskOut.PolygonMapLayerList = aList;

            //Show maskout property form
            frmProperty aFrmPT = new frmProperty();
            aFrmPT.Text = "Map Mask Out Set";
            aFrmPT.SetObject(MapDocument.ActiveMapFrame.MapView.MaskOut);
            aFrmPT.ShowDialog();

            //Change maskout status label
            if (MapDocument.ActiveMapFrame.MapView.MaskOut.SetMaskLayer)
            {
                TSSL_MaskOut.Text = "MaskOut: " + MapDocument.ActiveMapFrame.MapView.MaskOut.MaskLayer;
            }
            else
            {
                TSSL_MaskOut.Text = "MaskOut: No";
            }
        }

        private void TSMI_MapProperty_Click(object sender, EventArgs e)
        {
            MapDocument.ActiveMapFrame.MapView.SetProperties();
        }

        private void TSMI_Projection_Click(object sender, EventArgs e)
        {
            frmProjection aFrmProj = new frmProjection();
            aFrmProj.Show(this);
        }

        private void TSMI_InsertWindArraw_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.AddWindArrow(100, 100);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSB_Measurement_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.Measurement;
            MapDocument.MapLayout.MeasurementForm = _mapView.MeasurementForm;
            MapDocument.MapLayout.MouseMode = MouseMode.Map_Measurement;
            
            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSMI_InsertScaleBar_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.AddScaleBar(100, 100);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSMI_InsertNorthArrow_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.AddNorthArrow(200, 100);
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSMI_Clipping_Click(object sender, EventArgs e)
        {
            frmClipping aFrm = new frmClipping();
            aFrm.Show(this);
        }

        private void TSMI_PointProj_Click(object sender, EventArgs e)
        {
            frmProjPoint aFrm = new frmProjPoint();
            aFrm.Show(this);
        }

        private void TSMI_Script_Click(object sender, EventArgs e)
        {
            //ScriptEngine scriptEngine = Python.CreateEngine();
            //ScriptScope pyScope = scriptEngine.CreateScope();

            ////Set path
            //string path = Assembly.GetExecutingAssembly().Location;
            //string rootDir = Directory.GetParent(path).FullName;
            //List<string> paths = new List<string>();
            //paths.Add(rootDir);
            //paths.Add(Path.Combine(rootDir, "Lib"));
            //scriptEngine.SetSearchPaths(paths.ToArray());

            //pyScope.SetVariable("mipy", this);

            //frmScript aForm = new frmScript();
            //aForm.Show(this);

            frmTextEditor aForm = new frmTextEditor();
            aForm.Show();

            //string code = "aLayer = mipy.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[0]";
            //code = code + Environment.NewLine + "aLayer.Visible = False";
            //ScriptSource source = scriptEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            //source.Execute(pyScope); 
        }

        private void TSMI_PluginManager_Click(object sender, EventArgs e)
        {
            frmPluginManager frm = new frmPluginManager(this);
            frm.ShowDialog();
        }            

        #endregion

        #region ToolStrip_Layout

        private void TSB_PageSet_Click(object sender, EventArgs e)
        {
            frmPageSetup aFrmPageSetup = new frmPageSetup(MapDocument.MapLayout.PaperSize, MapDocument.MapLayout.Landscape);
            if (aFrmPageSetup.ShowDialog() == DialogResult.OK)
            {
                MapDocument.MapLayout.PaperSize = aFrmPageSetup.PaperSizeV;
                MapDocument.MapLayout.Landscape = aFrmPageSetup.Landscape;
                MapDocument.MapLayout.PaintGraphics();
            }
        }

        private void TSB_PageZoomIn_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.Zoom = MapDocument.MapLayout.Zoom * 1.5F;
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSB_PageZoomOut_Click(object sender, EventArgs e)
        {
            MapDocument.MapLayout.Zoom = MapDocument.MapLayout.Zoom * 0.8F;
            MapDocument.MapLayout.PaintGraphics();
        }

        private void TSCB_PageZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoomStr = TSCB_PageZoom.Text.Trim();
            zoomStr = zoomStr.Trim('%');
            try
            {
                float zoom = float.Parse(zoomStr);
                MapDocument.MapLayout.Zoom = zoom / 100;
                MapDocument.MapLayout.PaintGraphics();
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
                    MapDocument.MapLayout.Zoom = zoom / 100;
                    MapDocument.MapLayout.PaintGraphics();
                }
                catch
                {

                }
            }
        }

        private void TSB_FitToScreen_Click(object sender, EventArgs e)
        {
            float zoomX = (float)MapDocument.MapLayout.Width / MapDocument.MapLayout.PageBounds.Width;
            float zoomY = (float)MapDocument.MapLayout.Height / MapDocument.MapLayout.PageBounds.Height;
            float zoom = Math.Min(zoomX, zoomY);
            PointF aP = new PointF(0, 0);
            MapDocument.MapLayout.PageLocation = aP;
            MapDocument.MapLayout.Zoom = zoom;
            MapDocument.MapLayout.PaintGraphics();
        }

        #endregion

        #region ToolStrip_Edit
        private void TSB_AddPoint_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Point;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Point;
            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewLabel_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Label;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Label;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewPolyline_Click(object sender, EventArgs e)
        {
            _mapView.MeasurementForm = MapDocument.MapLayout.MeasurementForm;
            _mapView.MouseTool = MouseTools.New_Polyline;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Polyline;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewCurve_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Curve;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Curve;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewFreehand_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Freehand;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Freehand;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewPolylgon_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Polygon;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Polygon;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewCurvePolygon_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_CurvePolygon;
            MapDocument.MapLayout.MouseMode = MouseMode.New_CurvePolygon;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewRectangle_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Rectangle;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Rectangle;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewEllipse_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Ellipse;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Ellipse;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_NewCircle_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.New_Circle;
            MapDocument.MapLayout.MouseMode = MouseMode.New_Circle;

            SetCurrentTool((ToolStripButton)sender);
        }

        private void TSB_EditVertices_Click(object sender, EventArgs e)
        {
            _mapView.MouseTool = MouseTools.EditVertices;
            MapDocument.MapLayout.MouseMode = MouseMode.EditVertices;

            SetCurrentTool((ToolStripButton)sender);

            _isEditingVertices = true;
            if (tabControl1.SelectedIndex == 0)
                _mapView.PaintLayers();
            else
                MapDocument.MapLayout.PaintGraphics();
        }

        #endregion

        private void SetCurrentTool(ToolStripItem currentTool)
        {
            if (!(_currentTool == null))
            {
                if (_currentTool.GetType() == typeof(ToolStripButton))
                    ((ToolStripButton)_currentTool).Checked = false;
                else
                    ((ToolStripSplitButtonCheckable)_currentTool).Checked = false;
            }
            _currentTool = currentTool;
            if (_currentTool.GetType() == typeof(ToolStripButton))
                ((ToolStripButton)_currentTool).Checked = true;
            else
                ((ToolStripSplitButtonCheckable)_currentTool).Checked = true;
            TSSL_Status.Text = _currentTool.ToolTipText;

            OnCurrentToolChanged();
        }

        /// <summary>
        /// Set cursor
        /// </summary>
        /// <param name="cursor">Cursor</param>
        public void SetCursor(Cursor cursor)
        {
            this.Cursor = cursor;
        }

        #endregion                                                
            
    }
}
