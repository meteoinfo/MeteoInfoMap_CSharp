using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MeteoInfo.Classes;
using System.Collections;
using System.IO;
using MeteoInfo.Forms;
using MeteoInfoC.Legend;
using MeteoInfoC.Data;
using MeteoInfoC.Data.MeteoData;
using MeteoInfoC.Global;
using MeteoInfoC.Shape;
using MeteoInfoC.Layer;
using MeteoInfoC.Drawing;
using MeteoInfoC.Projections;

namespace MeteoInfo
{
    /// <summary>
    /// MeteoData form
    /// </summary>
    public partial class frmMeteoData : Form
    {
        #region Variables
        public static frmMeteoData pCurrenWin = null;
        public LegendScheme _legendScheme = null;

        int _lastAddedLayerHandle;
        List<MeteoDataInfo> _dataInfoList = new List<MeteoDataInfo>();
        MeteoDataInfo _meteoDataInfo = new MeteoDataInfo();        
        double[] _cValues;
        Color[] _cColors;
        double[] m_X, m_Y;             
        double m_MinData, m_MaxData;
        DrawType2D _2DDrawType = new DrawType2D();        
        private GridData _gridData = new GridData();
        private StationData _stationData = new StationData();
        List<STData> m_GrADSSTData = new List<STData>();
        Boolean _hasUndefData;
        bool m_IfInterpolateGrid = false;
        Boolean _useSameLegendScheme = false;
        Boolean _useSameGridInterSet = false;
        InterpolationSetting _gridInterp = new InterpolationSetting();              
        MeteoDataDrawSet m_MeteoDataDrawSet = new MeteoDataDrawSet();
        private int _StrmDensity = 4;
        //private int _meteoDataInfo.MeteoUVSet.SkipY = 1;
        //private int _meteoDataInfo.MeteoUVSet.SkipX = 1;

        //Thread _animatorThread;
        private bool _enableAnimation = false;
        private bool _inAnimation = false;

        //private int _VarIdx = 0;
        //private int _TimeIdx = 0;
        //private int _LevelIdx = 0;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmMeteoData()
        {
            InitializeComponent();
            pCurrenWin = this;

            Initialize();
        }

        private void Initialize()
        {
            //Set position
            Lab_Time.Left = CB_Time.Left - Lab_Time.Width;
            Lab_Level.Left = CB_Level.Left - Lab_Level.Width;
            Lab_Variable.Left = CB_Variable.Left - Lab_Variable.Width;
            Lab_DrawType.Left = CB_DrawType.Left - Lab_DrawType.Width;
            LB_DataFiles.Height = B_RemoveAllData.Top - LB_DataFiles.Top - 5;

            //Clear items
            CB_Variable.Items.Clear();
            CB_DrawType.Items.Clear();
            CB_Time.Items.Clear();
            CB_Level.Items.Clear();

            //Control enable set
            foreach (ToolStripItem aItem in toolStrip2.Items)
            {
                aItem.Enabled = false;
            }
            toolStrip2.Items[0].Enabled = true;
            this.splitContainer1.Panel2.Enabled = false;
            CHB_ColorVar.Visible = false;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get data info list
        /// </summary>
        public List<MeteoDataInfo> MeteoDataList
        {
            get { return _dataInfoList; }
        }

        /// <summary>
        /// Get or set active meteo data
        /// </summary>
        public MeteoDataInfo ActiveMeteoData
        {
            get { return _meteoDataInfo; }
            set
            {
                int idx = _dataInfoList.IndexOf(value);
                if (idx >= 0)
                    LB_DataFiles.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// Get or set draw type
        /// </summary>
        public DrawType2D DrawType
        {
            get { return _2DDrawType; }
            set { _2DDrawType = value; }
        }

        /// <summary>
        /// Get last added layer handle
        /// </summary>
        public int LastAddedLayerHandle
        {
            get { return _lastAddedLayerHandle; }
        }

        /// <summary>
        /// Get or set grid interpolation setting
        /// </summary>
        public InterpolationSetting InterpolationSet
        {
            get { return _gridInterp; }
            set { _gridInterp = value; }
        }

        /// <summary>
        /// Get or set legend scheme
        /// </summary>
        public LegendScheme LegendScheme
        {
            get { return _legendScheme; }
            set { _legendScheme = value; }
        }

        /// <summary>
        /// Get or set if use default interpolation setting
        /// </summary>
        public Boolean UseDefaultInterpSet
        {
            get { return _useSameGridInterSet; }
            set { _useSameGridInterSet = value; }
        }

        /// <summary>
        /// Get or set if use default legend scheme
        /// </summary>
        public Boolean UseDefaultLegendScheme
        {
            get { return _useSameLegendScheme; }
            set { _useSameLegendScheme = value; }
        }

        #endregion

        #region Main Methods

        #region MeteoDataInfo
        /// <summary>
        /// Create a new MeteoDataInfo
        /// </summary>
        /// <returns>Meteo data info</returns>
        public MeteoDataInfo NewMeteoData()
        {
            MeteoDataInfo aDataInfo = new MeteoDataInfo();
            //_dataInfoList.Add(aDataInfo);
            return aDataInfo;
        }

        /// <summary>
        /// Add a meteo data
        /// </summary>
        /// <param name="aDataInfo">a meteo data</param>
        public void AddMeteoData(MeteoDataInfo aDataInfo)
        {
            _dataInfoList.Add(aDataInfo);
            LB_DataFiles.Items.Add(Path.GetFileName(aDataInfo.GetFileName()));
            LB_DataFiles.SelectedIndex = LB_DataFiles.Items.Count - 1;
        }

        /// <summary>
        /// Set active meteo data
        /// </summary>
        /// <param name="idx">index</param>
        public void SetActiveMeteoData(int idx)
        {
            if (idx >= 0 && idx < LB_DataFiles.Items.Count)
                LB_DataFiles.SelectedIndex = idx;
        }

        /// <summary>
        /// Remove a meteo data by index
        /// </summary>
        /// <param name="idx">index</param>
        public void RemoveMeteoData(int idx)
        {
            _dataInfoList.RemoveAt(idx);
            if (LB_DataFiles.Items.Count > 1)
            {
                if (idx >= 1)
                    LB_DataFiles.SelectedIndex = idx - 1;
                else
                    LB_DataFiles.SelectedIndex = 0;
            }
            LB_DataFiles.Items.RemoveAt(idx);

            if (_dataInfoList.Count == 0)
                Initialize();
        }

        /// <summary>
        /// Remove a meteo data
        /// </summary>
        /// <param name="aDataInfo">a meteo data</param>
        public void RemoveMeteoData(MeteoDataInfo aDataInfo)
        {
            _dataInfoList.Remove(aDataInfo);
            if (_dataInfoList.Count == 0)
                Initialize();
        }

        /// <summary>
        /// Remove all meteo data
        /// </summary>
        public void RemoveAllMeteoData()
        {
            _dataInfoList.Clear();
            LB_DataFiles.Items.Clear();
            Initialize();
        }

        #endregion

        #region Setting
        /// <summary>
        /// Set draw type
        /// </summary>
        /// <param name="drawType">draw type</param>
        public void SetDrawType(string drawType)
        {
            Boolean isIn = false;
            if (CB_DrawType.Enabled && CB_DrawType.Items.Count > 0)
            {
                foreach (string type in CB_DrawType.Items)
                {
                    if (type.ToLower() == drawType.ToLower())
                    {
                        CB_DrawType.Text = type;
                        isIn = true;
                        break;
                    }
                }
            }

            if (!isIn)
                _2DDrawType = (DrawType2D)Enum.Parse(typeof(DrawType2D), drawType, true);

            //switch (drawType.ToLower())
            //{
            //    case "contour":
            //        _2DDrawType = DrawType2D.Contour;
            //        break;
            //    case "shaded":
            //        _2DDrawType = DrawType2D.Shaded;
            //        break;
            //    case "grid_fill":
            //        _2DDrawType = DrawType2D.Grid_Fill;
            //        break;
            //    case "grid_point":
            //        _2DDrawType = DrawType2D.Grid_Point;
            //        break;
            //    case "vector":
            //        _2DDrawType = DrawType2D.Vector;
            //        break;
            //    case "barb":
            //        _2DDrawType = DrawType2D.Barb;
            //        break;
            //    case "streamline":
            //        _2DDrawType = DrawType2D.Streamline;
            //        break;
            //    case "station_point":
            //        _2DDrawType = DrawType2D.Station_Point;
            //        break;
            //    case "weather_symbol":
            //        _2DDrawType = DrawType2D.Weather_Symbol;
            //        break;
            //    case "station_model":
            //        _2DDrawType = DrawType2D.Station_Model;
            //        break;
            //    case "station_info":
            //        _2DDrawType = DrawType2D.Station_Info;
            //        break;
            //    case "traj_line":
            //        _2DDrawType = DrawType2D.Traj_Line;
            //        break;
            //    case "traj_point":
            //        _2DDrawType = DrawType2D.Traj_Point;
            //        break;
            //    case "traj_startpoint":
            //        _2DDrawType = DrawType2D.Traj_StartPoint;
            //        break;
            //    case "image":
            //        _2DDrawType = DrawType2D.Image;
            //        break;
            //    case "raster":
            //        _2DDrawType = DrawType2D.Raster;
            //        break;
            //}
        }

        /// <summary>
        /// Set if using color wind
        /// </summary>
        /// <param name="isColor">if using color wind</param>
        public void SetColorWind(bool isColor)
        {
            CHB_ColorVar.Checked = isColor;
        }

        /// <summary>
        /// Set variable name
        /// </summary>
        /// <param name="vName">variable name</param>
        public void SetVariable(string vName)
        {
            this.CB_Variable.Text = vName;            
        }

        /// <summary>
        /// Set variable by index
        /// </summary>
        /// <param name="idx">index</param>
        public void SetVariable(int idx)
        {
            if (idx >= 0 && idx < CB_Variable.Items.Count)
                CB_Variable.SelectedIndex = idx;
        }

        /// <summary>
        /// Set Time name
        /// </summary>
        /// <param name="tStr">time string</param>
        public void SetTime(string tStr)
        {
            DateTime aTime;
            if (DateTime.TryParse(tStr, out aTime))
            {
                foreach (string v in CB_Time.Items)
                {
                    if (aTime.ToString("yyyy-MM-dd HH:mm") == v)
                    {
                        CB_Time.Text = v;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Set variable by index
        /// </summary>
        /// <param name="idx">index</param>
        public void SetTime(int idx)
        {
            if (idx >= 0 && idx < CB_Time.Items.Count)
                CB_Time.SelectedIndex = idx;
        }

        /// <summary>
        /// Set Level
        /// </summary>
        /// <param name="lStr">level string</param>
        public void SetLevel(string lStr)
        {
            foreach (string l in CB_Level.Items)
            {
                if (l == lStr)
                {
                    CB_Level.Text = l;
                    break;
                }
            }
        }

        /// <summary>
        /// Set level by index
        /// </summary>
        /// <param name="idx">index</param>
        public void SetLevel(int idx)
        {
            if (idx >= 0 && idx < CB_Level.Items.Count)
                CB_Level.SelectedIndex = idx;
        }

        #endregion

        #region Display - Create Data Layer
        /// <summary>
        /// Display - Draw meteo data
        /// </summary>
        public MapLayer Display()
        {
            this.Cursor = Cursors.WaitCursor;

            TSB_Draw.Enabled = false;
            TSB_ViewData.Enabled = true;
            TSB_DrawSetting.Enabled = true;
            if (CB_Time.Items.Count > 1)
            {
                TSB_NextTime.Enabled = true;
                TSB_PreTime.Enabled = true;
                TSB_Animitor.Enabled = true;
                TSB_CreateAnimatorFile.Enabled = true;
            }
            TSB_Setting.Enabled = true;

            _meteoDataInfo.DimensionSet = PlotDimension.Lat_Lon;

            MapLayer aLayer = null;
            string fieldName = CB_Variable.Text;

            if (_meteoDataInfo.IsGridData)
                aLayer = DrawGrid(fieldName);
            else if (_meteoDataInfo.IsStationData || _meteoDataInfo.IsSWATHData)
                aLayer = DrawStation(fieldName);
            else if (_meteoDataInfo.IsTrajData)
            {
                aLayer = DrawTraj();
                TSB_DrawSetting.Enabled = false;
                TSB_NextTime.Enabled = false;
                TSB_PreTime.Enabled = false;
            }
            

            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_1:
                case MeteoDataType.MICAPS_2:
                case MeteoDataType.MICAPS_3:
                case MeteoDataType.MICAPS_4:
                case MeteoDataType.MICAPS_7:
                case MeteoDataType.MICAPS_11:
                case MeteoDataType.MICAPS_13:
                    TSB_NextTime.Enabled = true;
                    TSB_PreTime.Enabled = true;
                    break;
            }

            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();                                                

            this.Cursor = Cursors.Default;

            return aLayer;
        }

        /// <summary>
        /// Display - variable name
        /// </summary>
        /// <param name="vName">variable name</param>
        public MapLayer Display(string vName)
        {
            if (_meteoDataInfo.GetVariableNames().Contains(vName))
            {
                GridData gridData = _meteoDataInfo.GetGridData(vName);
                return Display(gridData);
            }
            else
            {
                MathParser mathParser = new MathParser(_meteoDataInfo);
                GridData gridData = (GridData)mathParser.Evaluate(vName);
                return Display(gridData);
            }
        }        

        /// <summary>
        /// Display - grid data
        /// </summary>
        /// <param name="gridData">grid data</param>
        public MapLayer Display(GridData gridData)
        {
            if (gridData == null)
                return null;

            _gridData = gridData;
            if (_useSameLegendScheme)
            {
                if (_legendScheme.LegendType == LegendType.GraduatedColor)
                {
                    double minValue = _gridData.GetMinValue();
                    double maxValue = _gridData.GetMaxValue();
                    if (double.Parse(_legendScheme.LegendBreaks[0].StartValue.ToString()) <
                        double.Parse(_legendScheme.LegendBreaks[0].EndValue.ToString()))
                    {
                        if (minValue < double.Parse(_legendScheme.LegendBreaks[0].StartValue.ToString()))
                            _legendScheme.LegendBreaks[0].StartValue = minValue;
                        if (maxValue > double.Parse(_legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].EndValue.ToString()))
                            _legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].EndValue = maxValue;
                    }
                    else
                    {
                        if (maxValue > double.Parse(_legendScheme.LegendBreaks[0].EndValue.ToString()))
                            _legendScheme.LegendBreaks[0].EndValue = maxValue;
                        if (minValue < double.Parse(_legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].StartValue.ToString()))
                            _legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].StartValue = minValue;
                    }
                }
            }
            else
                CreateLegendScheme_Grid();

            return DrawMeteoMap_Grid(true, _legendScheme, "Data");
        }

        /// <summary>
        /// Display U/V grid data
        /// </summary>
        /// <param name="UGridData">U grid data</param>
        /// <param name="VGridData">V grid data</param>
        public MapLayer Display(GridData UGridData, GridData VGridData)
        {
            _gridData = UGridData;
            //Create legend scheme
            LegendScheme aLS = null;
            switch (_2DDrawType)
            {
                case DrawType2D.Streamline:
                    aLS = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Polyline, Color.Blue, 1);
                    break;
                default:
                    aLS = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 10);
                    break;
            }

            //Create meteo data layer
            if (aLS != null)
                return DrawMeteoMap_Grid(UGridData, VGridData, false, aLS);
            else
                return null;
        }

        /// <summary>
        /// Display color U/V grid data
        /// </summary>
        /// <param name="UGridData">U grid data</param>
        /// <param name="VGridData">V grid data</param>
        /// <param name="XGridData">Color grid data</param>
        public MapLayer Display(GridData UGridData, GridData VGridData, GridData XGridData)
        {
            _gridData = XGridData;
            //Create legend scheme
            LegendScheme aLS = LegendManage.CreateLegendSchemeFromGridData(_gridData,
                LegendType.GraduatedColor, ShapeTypes.Point);
            PointBreak aPB = new PointBreak();
            for (int i = 0; i < aLS.LegendBreaks.Count; i++)
            {
                aPB = (PointBreak)aLS.LegendBreaks[i];
                aPB.Size = 10;
                aLS.LegendBreaks[i] = aPB;
            }

            //Create meteo data layer
            return DrawMeteoMap_Grid(UGridData, VGridData, true, aLS);
        }        

        /// <summary>
        /// Display - station data
        /// </summary>
        /// <param name="stData">station data</param>
        public MapLayer Display(StationData stData)
        {
            _stationData = stData;

            Extent aExtent = _stationData.DataExtent;

            if (!_useSameGridInterSet)
            {
                GridDataSetting aGDP = _gridInterp.GridDataSet;
                aGDP.DataExtent = aExtent;
                _gridInterp.GridDataSet = aGDP;
                _gridInterp.Radius = Convert.ToSingle((_gridInterp.GridDataSet.DataExtent.maxX -
                    _gridInterp.GridDataSet.DataExtent.minX) / _gridInterp.GridDataSet.XNum * 2);
                ContourDraw.CreateGridXY(_gridInterp.GridDataSet, ref m_X, ref m_Y);
                _useSameGridInterSet = true;
            }

            switch (_2DDrawType)
            {
                case DrawType2D.Station_Point:
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                case DrawType2D.Weather_Symbol:
                case DrawType2D.Station_Model:
                    if (!_useSameLegendScheme)
                    {
                        CreateLegendScheme_Station();
                    }
                    return DrawMeteoMap_Station(true, _legendScheme, "Data");
                default:
                    _gridData = InterpolateGridData(_stationData, _gridInterp);
                    if (!_useSameLegendScheme)
                    {
                        CreateLegendScheme_Station();
                    }
                    return DrawMeteoMap_Station(true, _legendScheme, "Data");
            }
        }

        /// <summary>
        /// Display station vector/barb/streamline data
        /// </summary>
        /// <param name="UStData">U/WindDirection station data</param>
        /// <param name="VStData">V/WindSpeed station data</param>
        /// <returns>Map layer</returns>
        public MapLayer Display(StationData UStData, StationData VStData)
        {
            return Display(UStData, VStData, true);
        }

        /// <summary>
        /// Display station vector/barb/streamline data
        /// </summary>
        /// <param name="UStData">U/WindDirection station data</param>
        /// <param name="VStData">V/WindSpeed station data</param>
        /// <param name="isUV">if is U/V</param>
        /// <returns>Map layer</returns>
        public MapLayer Display(StationData UStData, StationData VStData, bool isUV)
        {
            return Display(UStData, VStData, isUV, "Wind_Station", 
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo);
        }

        /// <summary>
        /// Display station vector/barb/streamline data
        /// </summary>
        /// <param name="UStData">U/WindDirection station data</param>
        /// <param name="VStData">V/WindSpeed station data</param>
        /// <param name="stData">Station data for color</param>
        /// <param name="isUV">if is U/V</param>
        /// <returns>Map layer</returns>
        public MapLayer Display(StationData UStData, StationData VStData, StationData stData, bool isUV)
        {
            return Display(UStData, VStData, stData, isUV, "Wind_Station", 
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo);
        }

        /// <summary>
        /// Display station vector/barb/streamline data
        /// </summary>
        /// <param name="UStData">U/WindDirection station data</param>
        /// <param name="VStData">V/WindSpeed station data</param>
        /// <param name="stData">Station data for color</param>
        /// <param name="isUV">if is U/V</param>
        /// <param name="layerName">Layer name</param>
        /// <param name="projInfo">ProjectionInfo</param>
        /// <returns>Map layer</returns>
        public MapLayer Display(StationData UStData, StationData VStData, StationData stData, 
            bool isUV, string layerName, ProjectionInfo projInfo)
        {
            //Get legend scheme
            LegendScheme aLS = null;
            aLS = LegendManage.CreateLegendSchemeFromStationData(stData,
                    LegendType.GraduatedColor, ShapeTypes.Point);

            //Create meteo data layer
            MapLayer aLayer = null;            
            switch (_2DDrawType)
            {
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                case DrawType2D.Streamline:
                    switch (_2DDrawType)
                    {
                        case DrawType2D.Vector:
                        case DrawType2D.Barb:
                            if (_2DDrawType == DrawType2D.Vector)
                            {
                                aLayer = DrawMeteoData.CreateSTVectorLayer(UStData, VStData, stData,
                                    aLS, layerName, isUV);
                            }
                            else
                            {
                                aLayer = DrawMeteoData.CreateSTBarbLayer(UStData, VStData, stData,
                                    aLS, layerName, isUV);
                            }
                            break;
                        case DrawType2D.Streamline:
                            StationData nstUData = new StationData();
                            StationData nstVData = new StationData();
                            if (isUV)
                            {
                                nstUData = UStData;
                                nstVData = VStData;
                            }
                            else
                                DataMath.GetUVFromDS(UStData, VStData, ref nstUData, ref nstVData);

                            GridData UData = InterpolateData(nstUData);
                            GridData VData = InterpolateData(nstVData);
                            aLayer = DrawMeteoData.CreateStreamlineLayer(UData, VData, _StrmDensity, aLS, layerName,
                                true);
                            break;

                    }
                    break;
            }

            if (aLayer != null)
            {
                aLayer.IsMaskout = true;
                aLayer.ProjInfo = projInfo;
                _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                return aLayer;
            }
            else
                return null;
        }

        /// <summary>
        /// Display station vector/barb/streamline data
        /// </summary>
        /// <param name="UStData">U/WindDirection station data</param>
        /// <param name="VStData">V/WindSpeed station data</param>
        /// <param name="isUV">if is U/V</param>
        /// <param name="layerName">Layer name</param>
        /// <param name="projInfo">ProjectionInfo</param>
        /// <returns>Map layer</returns>
        public MapLayer Display(StationData UStData, StationData VStData, bool isUV, string layerName,
            ProjectionInfo projInfo)
        {
            //Get legend scheme
            LegendScheme aLS = null;
            aLS = CreateLegendScheme_Station();

            //Create meteo data layer
            MapLayer aLayer = null;
            switch (_2DDrawType)
            {
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                case DrawType2D.Streamline:
                    switch (_2DDrawType)
                    {
                        case DrawType2D.Vector:
                        case DrawType2D.Barb:
                            if (_2DDrawType == DrawType2D.Vector)
                            {
                                aLayer = DrawMeteoData.CreateSTVectorLayer(UStData, VStData,
                                    aLS, layerName, isUV);
                            }
                            else
                            {
                                aLayer = DrawMeteoData.CreateSTBarbLayer(UStData, VStData,
                                    aLS, layerName, isUV);
                            }
                            break;
                        case DrawType2D.Streamline:
                            StationData nstUData = new StationData();
                            StationData nstVData = new StationData();
                            if (_meteoDataInfo.MeteoUVSet.IsUV)
                            {
                                nstUData = UStData;
                                nstVData = VStData;
                            }
                            else
                                DataMath.GetUVFromDS(UStData, VStData, ref nstUData, ref nstVData);

                            GridData UData = InterpolateData(nstUData);
                            GridData VData = InterpolateData(nstVData);
                            aLayer = DrawMeteoData.CreateStreamlineLayer(UData, VData, _StrmDensity, aLS, layerName,
                                true);
                            break;

                    }
                    break;
            }

            if (aLayer != null)
            {
                aLayer.IsMaskout = true;
                aLayer.ProjInfo = projInfo;
                _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                return aLayer;
            }
            else
                return null;
        }

        /// <summary>
        /// Display
        /// </summary>
        /// <param name="U">U name</param>
        /// <param name="V">V name</param>
        public MapLayer Display(string U, string V)
        {
            if (_meteoDataInfo.IsGridData)
            {
                GridData UGridData = _meteoDataInfo.GetGridData(U);
                GridData VGridData = _meteoDataInfo.GetGridData(V);

                if (UGridData != null && VGridData != null)
                {
                    return Display(UGridData, VGridData);
                }
                else
                    return null;
            }
            else if (_meteoDataInfo.IsStationData)
            {
                StationData UStData = _meteoDataInfo.GetStationData(U);
                StationData VStData = _meteoDataInfo.GetStationData(V);

                if (UStData != null && VStData != null)
                    return Display(UStData, VStData);
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Display
        /// </summary>
        /// <param name="U">U name</param>
        /// <param name="V">V name</param>
        /// <param name="varName">varible name</param>
        public MapLayer Display(string U, string V, string varName)
        {
            if (_meteoDataInfo.IsGridData)
            {
                GridData UGridData = _meteoDataInfo.GetGridData(U);
                GridData VGridData = _meteoDataInfo.GetGridData(V);
                _gridData = _meteoDataInfo.GetGridData(varName);

                if (UGridData != null && VGridData != null && _gridData != null)
                {
                    return Display(UGridData, VGridData, _gridData);
                }
                else
                    return null;
            }
            else if (_meteoDataInfo.IsStationData)
            {
                StationData UStData = _meteoDataInfo.GetStationData(U);
                StationData VStData = _meteoDataInfo.GetStationData(V);
                _stationData = _meteoDataInfo.GetStationData(varName);

                if (UStData != null && VStData != null && _stationData != null)
                    return Display(UStData, VStData, true);
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Display trajectory
        /// </summary>
        public MapLayer DisplayTraj()
        {
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.HYSPLIT_Traj:
                    HYSPLITTrajectoryInfo aDataInfo = (HYSPLITTrajectoryInfo)_meteoDataInfo.DataInfo;
                    VectorLayer aLayer = null;
                    switch (_2DDrawType)
                    {
                        case DrawType2D.Traj_Line:
                            aLayer = aDataInfo.CreateTrajLineLayer();
                            if (_useSameLegendScheme)
                                aLayer.LegendScheme = _legendScheme;
                            else
                            {
                                PolyLineBreak aPLB = new PolyLineBreak();
                                for (int i = 0; i < aLayer.LegendScheme.BreakNum; i++)
                                {
                                    aPLB = (PolyLineBreak)aLayer.LegendScheme.LegendBreaks[i];
                                    aPLB.Size = 2;
                                    aLayer.LegendScheme.LegendBreaks[i] = aPLB;
                                }
                            }
                            _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                            break;
                        case DrawType2D.Traj_StartPoint:
                            aLayer = aDataInfo.CreateTrajStartPointLayer();
                            PointBreak aPB = (PointBreak)aLayer.LegendScheme.LegendBreaks[0];
                            aPB.Style = PointStyle.UpTriangle;
                            aLayer.LegendScheme.LegendBreaks[0] = aPB;
                            _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                            break;
                        case DrawType2D.Traj_Point:
                            aLayer = aDataInfo.CreateTrajPointLayer();
                            _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                            break;
                    }

                    return aLayer;
                default:
                    return null;
            }
        }       

        private MapLayer DrawGrid(string fieldName)
        {
            string vName = this.CB_Variable.Text;
            _gridData = _meteoDataInfo.GetGridData(vName);

            if (_gridData == null)
                return null;

            if (_useSameLegendScheme)
            {
                if (_legendScheme.LegendType == LegendType.GraduatedColor)
                {
                    double minValue = _gridData.GetMinValue();
                    double maxValue = _gridData.GetMaxValue();
                    if (double.Parse(_legendScheme.LegendBreaks[0].StartValue.ToString()) <
                        double.Parse(_legendScheme.LegendBreaks[0].EndValue.ToString()))
                    {
                        if (minValue < double.Parse(_legendScheme.LegendBreaks[0].StartValue.ToString()))
                            _legendScheme.LegendBreaks[0].StartValue = minValue;
                        if (maxValue > double.Parse(_legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].EndValue.ToString()))
                            _legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].EndValue = maxValue;
                    }
                    else
                    {
                        if (maxValue > double.Parse(_legendScheme.LegendBreaks[0].EndValue.ToString()))
                            _legendScheme.LegendBreaks[0].EndValue = maxValue;
                        if (minValue < double.Parse(_legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].StartValue.ToString()))
                            _legendScheme.LegendBreaks[_legendScheme.BreakNum - 1].StartValue = minValue;
                    }
                }
            }
            else
                CreateLegendScheme_Grid();

            return DrawMeteoMap_Grid(true, _legendScheme, fieldName);
        }

        private MapLayer DrawStation(string fieldName)
        {
            _stationData = _meteoDataInfo.GetStationData(CB_Variable.Text);

            Extent aExtent = _stationData.DataExtent;

            if (_stationData.StNum > 5 && aExtent.Width > 0 && aExtent.Height > 0)
            {
                if (!_useSameGridInterSet)
                {
                    GridDataSetting aGDP = _gridInterp.GridDataSet;
                    aGDP.DataExtent = aExtent;
                    _gridInterp.GridDataSet = aGDP;
                    _gridInterp.Radius = Convert.ToSingle((_gridInterp.GridDataSet.DataExtent.maxX -
                        _gridInterp.GridDataSet.DataExtent.minX) / _gridInterp.GridDataSet.XNum * 2);
                    ContourDraw.CreateGridXY(_gridInterp.GridDataSet, ref m_X, ref m_Y);
                    _useSameGridInterSet = true;
                }
            }

            switch (_2DDrawType)
            {
                case DrawType2D.Station_Point:
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                case DrawType2D.Weather_Symbol:
                case DrawType2D.Station_Model:
                case DrawType2D.Station_Info:
                    if (!_useSameLegendScheme)
                    {
                        CreateLegendScheme_Station();
                    }
                    return DrawMeteoMap_Station(true, _legendScheme,fieldName);
                default:
                    _gridData = InterpolateGridData(_stationData, _gridInterp);
                    if (!_useSameLegendScheme)
                    {
                        CreateLegendScheme_Station();
                    }
                    return DrawMeteoMap_Station(true, _legendScheme, fieldName);
            }

        }

        private MapLayer DrawHDFData()
        {
            string fieldName = CB_Variable.Text;
            if (((HDF5DataInfo)_meteoDataInfo.DataInfo).Variables[CB_Variable.SelectedIndex].IsSwath)
                return DrawStation(fieldName);
            else
                return DrawGrid(fieldName);
        }

        private MapLayer DrawMICAPS7()
        {
            MICAPS7DataInfo aDataInfo = (MICAPS7DataInfo)_meteoDataInfo.DataInfo;
            VectorLayer aLayer = null;
            switch (_2DDrawType)
            {
                case DrawType2D.Traj_Line:
                    aLayer = aDataInfo.CreateTrajLineLayer();
                    PolyLineBreak aPLB = new PolyLineBreak();
                    for (int i = 0; i < aLayer.LegendScheme.BreakNum; i++)
                    {
                        aPLB = (PolyLineBreak)aLayer.LegendScheme.LegendBreaks[i];
                        aPLB.Size = 2;
                        aLayer.LegendScheme.LegendBreaks[i] = aPLB;
                    }
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
                case DrawType2D.Traj_StartPoint:
                    aLayer = aDataInfo.CreateTrajStartPointLayer();
                    PointBreak aPB = (PointBreak)aLayer.LegendScheme.LegendBreaks[0];
                    aPB.Style = PointStyle.UpTriangle;
                    aLayer.LegendScheme.LegendBreaks[0] = aPB;
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
                case DrawType2D.Traj_Point:
                    aLayer = aDataInfo.CreateTrajPointLayer();
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
            }

            return aLayer;
        }

        private MapLayer DrawMICAPS11()
        {
            //int varIdx = CB_Vars.SelectedIndex;
            _gridData = _meteoDataInfo.GetGridData(CB_Variable.Text);

            if (!_useSameLegendScheme)
            {
                CreateLegendScheme_Grid();
            }
            string fieldName = CB_Variable.Text;
            return DrawMeteoMap_Grid(true, _legendScheme, fieldName);
        }

        private MapLayer DrawTraj()
        {
            DataInfo aDataInfo = _meteoDataInfo.DataInfo;
            VectorLayer aLayer = null;
            switch (_2DDrawType)
            {
                case DrawType2D.Traj_Line:
                    aLayer = ((ITrajDataInfo)aDataInfo).CreateTrajLineLayer();
                    PolyLineBreak aPLB = new PolyLineBreak();
                    for (int i = 0; i < aLayer.LegendScheme.BreakNum; i++)
                    {
                        aPLB = (PolyLineBreak)aLayer.LegendScheme.LegendBreaks[i];
                        aPLB.Size = 2;
                        aLayer.LegendScheme.LegendBreaks[i] = aPLB;
                    }
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
                case DrawType2D.Traj_StartPoint:
                    aLayer = ((ITrajDataInfo)aDataInfo).CreateTrajStartPointLayer();
                    PointBreak aPB = (PointBreak)aLayer.LegendScheme.LegendBreaks[0];
                    aPB.Style = PointStyle.UpTriangle;
                    aLayer.LegendScheme.LegendBreaks[0] = aPB;
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
                case DrawType2D.Traj_Point:
                    aLayer = ((ITrajDataInfo)aDataInfo).CreateTrajPointLayer();
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                    break;
            }

            return aLayer;
        }

        public MapLayer DrawMeteoMap(Boolean isNew, LegendScheme aLS, string fieldName)
        {
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:
                case MeteoDataType.HYSPLIT_Conc:
                case MeteoDataType.MICAPS_4:
                case MeteoDataType.ARL_Grid:
                case MeteoDataType.NetCDF:
                case MeteoDataType.MICAPS_11:
                case MeteoDataType.ASCII_Grid:
                case MeteoDataType.Sufer_Grid:
                case MeteoDataType.GRIB1:
                case MeteoDataType.GRIB2:
                    return DrawMeteoMap_Grid(isNew, aLS, fieldName);
                case MeteoDataType.GrADS_Station:
                case MeteoDataType.MICAPS_1:
                case MeteoDataType.MICAPS_3:
                case MeteoDataType.ISH:
                case MeteoDataType.LonLatStation:
                case MeteoDataType.METAR:
                    return DrawMeteoMap_Station(isNew, aLS, fieldName);
                case MeteoDataType.AWX:
                    AWXDataInfo aDataInfo = (AWXDataInfo)_meteoDataInfo.DataInfo;
                    switch (aDataInfo.ProductType)
                    {
                        case 1:
                        case 3:
                            return DrawMeteoMap_Grid(isNew, aLS, fieldName);
                        case 4:
                            return DrawMeteoMap_Station(isNew, aLS, fieldName);
                        default:
                            return null;
                    }                    
                default:
                    return null;
            }
        }

        private MapLayer DrawMeteoMap_Grid(Boolean isNew, LegendScheme aLS, string fieldName)
        {
            MapLayer aLayer = new MapLayer();
            //VectorLayer aLayer = new VectorLayer(ShapeTypes.Point);
            string varName = CB_Variable.Text;

            string LNameS = CB_Level.Text + "_" + CB_Time.Text;
            string LNameM = varName + "_" + LNameS;
            string LName = LNameM;            
            bool ifAddLayer = true;

            //Interpolate grid if set  
            if (isNew)
            {
                switch (_2DDrawType)
                {
                    case DrawType2D.Contour:
                    case DrawType2D.Shaded:
                        if (m_IfInterpolateGrid)
                        {
                            _gridData = ContourDraw.Interpolate_Grid(_gridData);
                        }
                        break;
                }
            }

            //Extent to global if the data is global
            if (_gridData.IsGlobal)
            {
                //_gridData.ShiftTo180();
                _gridData.ExtendToGlobal();                
            }

            //Create layer
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                    LName = "Contour_" + LName;
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    aLayer = DrawMeteoData.CreateContourLayer(_gridData, aLS, LName, fieldName);
                    if (aLayer != null)
                    {
                        ((VectorLayer)aLayer).LabelSet.ShadowColor = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.BackColor;
                        ((VectorLayer)aLayer).AddLabelsContourDynamic(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ViewExtent);
                    }
                    break;
                case DrawType2D.Shaded:
                    LName = "Shaded_" + LName;
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    aLayer = DrawMeteoData.CreateShadedLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Grid_Fill:
                    LName = "GridFill_" + LName;
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    aLayer = DrawMeteoData.CreateGridFillLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Grid_Point:
                    LName = "GridPoint_" + LName;
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    aLayer = DrawMeteoData.CreateGridPointLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Vector:
                    GridData UData = new GridData();
                    GridData VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        if (CHB_ColorVar.Checked)
                        {
                            LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                        }
                        else
                        {
                            LName = LNameS;
                        }
                        LName = "Vector_" + LName;
                        if (CHB_ColorVar.Checked)
                        {
                            if (_meteoDataInfo.MeteoUVSet.SkipX != 1 || _meteoDataInfo.MeteoUVSet.SkipY != 1)
                                _gridData = _gridData.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
                        }
                        aLayer = DrawMeteoData.CreateGridVectorLayer(UData, VData, _gridData, aLS, CHB_ColorVar.Checked,
                            LName, _meteoDataInfo.MeteoUVSet.IsUV);
                    }
                    else
                    {
                        ifAddLayer = false;
                    }
                    break;
                case DrawType2D.Streamline:
                    UData = new GridData();
                    VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        LName = LNameS;
                        LName = "Streamline_" + LName;
                        //aLayer = DrawMeteoData.CreateStreamlineLayer(UData.Data, VData.Data, UData.X, UData.Y,
                        //    UData.MissingValue, _StrmDensity, aLS, LName);
                        aLayer = DrawMeteoData.CreateStreamlineLayer(UData, VData, _StrmDensity, aLS, LName, true);
                    }
                    else
                    {
                        ifAddLayer = false;
                    }
                    break;
                case DrawType2D.Barb:
                    UData = new GridData();
                    VData = new GridData();
                    if (GetUVGridData(ref UData, ref VData))
                    {
                        if (CHB_ColorVar.Checked)
                        {
                            LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                        }
                        else
                        {
                            LName = LNameS;
                        }
                        LName = "Barb_" + LName;
                        if (CHB_ColorVar.Checked)
                        {
                            if (_meteoDataInfo.MeteoUVSet.SkipX != 1 || _meteoDataInfo.MeteoUVSet.SkipY != 1)
                                _gridData = _gridData.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
                        }
                        //aLayer = DrawMeteoData.CreateGridBarbLayer(UData.Data, VData.Data, _gridData.Data, UData.X, UData.Y,
                        //    UData.MissingValue, aLS, CHB_ColorVar.Checked, LName);
                        aLayer = DrawMeteoData.CreateGridBarbLayer(UData, VData, _gridData, aLS, CHB_ColorVar.Checked, LName, true);
                    }
                    else
                    {
                        ifAddLayer = false;
                    }
                    break;
                case DrawType2D.Raster:
                    LName = "Raster_" + LName;
                    if (_meteoDataInfo.DataType == MeteoDataType.MICAPS_13)
                    {
                        string aFile = Application.StartupPath + "\\pal\\I-01.pal";
                        aLayer = DrawMeteoData.CreateRasterLayer(_gridData, LName, aFile);
                    }
                    else
                        aLayer = DrawMeteoData.CreateRasterLayer(_gridData, LName, aLS);
                    break;
            }

            if (!ifAddLayer || aLayer == null)
                return null;

            aLayer.ProjInfo = _meteoDataInfo.ProjInfo;
            if (aLayer.GetType() == typeof(VectorLayer))
            {
                VectorLayer aVLayer = (VectorLayer)aLayer;
                aVLayer.IsMaskout = true;
                ProjectionInfo aProjInfo = _meteoDataInfo.ProjInfo;
                if (aVLayer.ShapeType == ShapeTypes.Polygon)
                {
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolygonLayer(aVLayer);
                }
                else
                {
                    if (_2DDrawType == DrawType2D.Vector || _2DDrawType == DrawType2D.Barb)
                    {
                        _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddWindLayer(aVLayer,
                            _meteoDataInfo.EarthWind);
                    }
                    else
                    {
                        _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aVLayer);
                    }
                }
            }
            else
            {
                RasterLayer aILayer = (RasterLayer)aLayer;
                _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertImageLayer(aILayer);
            }

            return aLayer;
        }

        private MapLayer DrawMeteoMap_Grid(GridData UGridData, GridData VGridData, bool ifColor, LegendScheme aLS)
        {
            VectorLayer aLayer = new VectorLayer(ShapeTypes.Point);
            string LName = "UV_" + _meteoDataInfo.LevelIndex.ToString() + "_" + _meteoDataInfo.TimeIndex.ToString();
            bool ifAddLayer = true;

            //if (UGridData.YNum != _Y.Length)
            //{
            //    _X = _meteoDataInfo.X;
            //    _Y = _meteoDataInfo.Y;
            //}

            //Create layer
            switch (_2DDrawType)
            {
                case DrawType2D.Barb:
                    aLayer = DrawMeteoData.CreateGridBarbLayer(UGridData, VGridData, _gridData, aLS, ifColor, LName, true);
                    break;
                case DrawType2D.Streamline:
                    aLayer = DrawMeteoData.CreateStreamlineLayer(UGridData, VGridData, _StrmDensity, aLS, LName, true);
                    break;
                default:
                    aLayer = DrawMeteoData.CreateGridVectorLayer(UGridData, VGridData, _gridData, aLS, ifColor, LName, true);
                    break;
            }

            aLayer.IsMaskout = true;
            aLayer.ProjInfo = _meteoDataInfo.ProjInfo;
            if (ifAddLayer)
            {
                _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                return aLayer;
            }
            else
                return null;
        }

        public MapLayer DrawMeteoMap_Station(Boolean isNew, LegendScheme aLS, string fieldName)
        {
            Boolean hasNoData = _hasUndefData;
            MapLayer aLayer = new MapLayer();
            string LNameS = CB_Level.Text + "_" + CB_Time.Text;
            string LNameM = CB_Variable.Text + "_" + LNameS;
            string LName = LNameM;
            if (_gridInterp.InterpolationMethod == InterpolationMethods.IDW_Neighbors)
            {
                hasNoData = false;
            }
            bool ifAddLayer = true;
            switch (_2DDrawType)
            {
                case DrawType2D.Station_Point:
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    LName = "StationPoint_" + LName;
                    aLayer = DrawMeteoData.CreateSTPointLayer(_stationData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Grid_Point:
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    LName = "GridPoint_" + LName;
                    aLayer = DrawMeteoData.CreateGridPointLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Contour:
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    LName = "Contour_" + LName;
                    aLayer = DrawMeteoData.CreateContourLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Shaded:
                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                    LName = "Shaded_" + LName;
                    aLayer = DrawMeteoData.CreateShadedLayer(_gridData, aLS, LName, fieldName);
                    break;
                case DrawType2D.Raster:
                    LName = "Raster_" + LName;
                    aLayer = DrawMeteoData.CreateRasterLayer(_gridData, LName, aLS);
                    break;
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                case DrawType2D.Streamline:
                    StationData stUData = new StationData();
                    StationData stVData = new StationData();
                    if (GetUVStationData(ref stUData, ref stVData))
                    {
                        switch (_2DDrawType)
                        {
                            case DrawType2D.Vector:
                            case DrawType2D.Barb:
                                if (CHB_ColorVar.Checked)
                                {
                                    LegendManage.SetContoursAndColors(aLS, ref _cValues, ref _cColors);
                                }
                                else
                                {
                                    LName = LNameS;
                                }
                                if (_2DDrawType == DrawType2D.Vector)
                                {
                                    LName = "Vector_" + LName;
                                    if (CHB_ColorVar.Checked)
                                        aLayer = DrawMeteoData.CreateSTVectorLayer(stUData, stVData, _stationData,
                                            aLS, LName, _meteoDataInfo.MeteoUVSet.IsUV);
                                    else
                                        aLayer = DrawMeteoData.CreateSTVectorLayer(stUData, stVData, aLS, LName,
                                            _meteoDataInfo.MeteoUVSet.IsUV);
                                }
                                else
                                {
                                    LName = "Barb_" + LName;
                                    if (CHB_ColorVar.Checked)
                                        aLayer = DrawMeteoData.CreateSTBarbLayer(stUData, stVData, _stationData,
                                            aLS, LName, _meteoDataInfo.MeteoUVSet.IsUV);
                                    else
                                        aLayer = DrawMeteoData.CreateSTBarbLayer(stUData, stVData, aLS, LName,
                                            _meteoDataInfo.MeteoUVSet.IsUV);
                                }
                                break;
                            case DrawType2D.Streamline:
                                StationData nstUData = new StationData();
                                StationData nstVData = new StationData();
                                if (_meteoDataInfo.MeteoUVSet.IsUV)
                                {
                                    nstUData = stUData;
                                    nstVData = stVData;
                                }
                                else
                                    DataMath.GetUVFromDS(stUData, stVData, ref nstUData, ref nstVData);

                                GridData UData = InterpolateGridData(nstUData, _gridInterp);
                                GridData VData = InterpolateGridData(nstVData, _gridInterp);
                                LName = "Streamline_" + LName;
                                aLayer = DrawMeteoData.CreateStreamlineLayer(UData, VData, _StrmDensity, aLS, LName,
                                    true);
                                break;

                        }
                    }
                    else
                        ifAddLayer = false;
                    break;
                case DrawType2D.Weather_Symbol:
                    LName = LNameS;
                    LName = "Weather_" + LName;
                    aLayer = DrawMeteoData.CreateWeatherSymbolLayer(_stationData,
                        m_MeteoDataDrawSet.WeatherType, LName);
                    break;
                case DrawType2D.Station_Model:
                    //Extent aExtent = new Extent();
                    StationModelData stationModelData = _meteoDataInfo.GetStationModelData();
                    LName = LNameS;
                    LName = "StationModel_" + LName;
                    bool isSurface = true;
                    if (_meteoDataInfo.DataType == MeteoDataType.MICAPS_2)
                        isSurface = false;
                    aLayer = DrawMeteoData.CreateStationModelLayer(stationModelData,
                        _meteoDataInfo.MissingValue, aLS, LName, isSurface);
                    //if (GetStationModelData(ref stationModelData))
                    //{
                    //    LName = LNameS;
                    //    LName = "StationModel_" + LName;
                    //    aLayer = DrawMeteoData.CreateStationModelLayer(stationModelData,
                    //        _meteoDataInfo.MissingValue, aLS, LName);
                    //}
                    //else
                    //{
                    //    ifAddLayer = false;
                    //}
                    break;
                case DrawType2D.Station_Info:
                    StationInfoData stInfoData;
                    if (_meteoDataInfo.DataType == MeteoDataType.GrADS_Station)
                        stInfoData = _meteoDataInfo.GetStationInfoData(CB_Time.SelectedIndex);
                    else
                        stInfoData = _meteoDataInfo.GetStationInfoData();
                    if (stInfoData != null)
                    {
                        aLS.MissingValue = _meteoDataInfo.MissingValue;
                        LName = LNameS;
                        LName = "StationInfo_" + LName;
                        aLayer = DrawMeteoData.CreateSTInfoLayer(stInfoData, aLS, LName);
                    }
                    else
                        ifAddLayer = false;
                    break;
            }

            if (aLayer == null)
                return null;

            aLayer.IsMaskout = true;
            aLayer.ProjInfo = _meteoDataInfo.ProjInfo;
            if (aLayer.LayerType == LayerTypes.VectorLayer)
            {
                if (ifAddLayer)
                {
                    if (aLayer.ShapeType == ShapeTypes.Polygon)
                    {
                        _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolygonLayer(aLayer);
                    }
                    else
                    {
                        _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer((VectorLayer)aLayer);
                    }
                }
            }
            else
            {
                if (ifAddLayer)
                {
                    RasterLayer aILayer = (RasterLayer)aLayer;
                    _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertImageLayer(aILayer);
                }
            }

            return aLayer;
        }

        private void InterpolateData()
        {
            _gridData = InterpolateData(_stationData);
        }

        private GridData InterpolateData(StationData aStData)
        {
            GridData aGridData = new GridData();
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                case DrawType2D.Shaded:
                    double[] X = new double[1];
                    double[] Y = new double[1];
                    if (!_useSameGridInterSet)
                    {
                        GridDataSetting aGDP = _gridInterp.GridDataSet;
                        aGDP.DataExtent = GetDiscretedDataExtent(aStData.Data);
                        _gridInterp.GridDataSet = aGDP;
                        _gridInterp.Radius = Convert.ToSingle((_gridInterp.GridDataSet.DataExtent.maxX -
                            _gridInterp.GridDataSet.DataExtent.minX) / _gridInterp.GridDataSet.XNum * 2);
                        ContourDraw.CreateGridXY(_gridInterp.GridDataSet, ref X, ref Y);
                        _useSameGridInterSet = true;
                    }

                    double[,] S = aStData.Data;
                    S = ContourDraw.FilterDiscreteData_Radius(S, _gridInterp.Radius,
                        _gridInterp.GridDataSet.DataExtent, _meteoDataInfo.MissingValue);
                    switch (_gridInterp.InterpolationMethod)
                    {
                        case InterpolationMethods.IDW_Radius:
                            aGridData = ContourDraw.InterpolateDiscreteData_Radius(S,
                                X, Y, _gridInterp.MinPointNum, _gridInterp.Radius, _meteoDataInfo.MissingValue);
                            break;
                        case InterpolationMethods.IDW_Neighbors:
                            aGridData = ContourDraw.InterpolateDiscreteData_Neighbor(S, X, Y,
                                _gridInterp.MinPointNum, _meteoDataInfo.MissingValue);
                            break;
                        case InterpolationMethods.Cressman:
                            aGridData = ContourDraw.InterpolateDiscreteData_Cressman(S, X, Y,
                                _meteoDataInfo.MissingValue, _gridInterp.RadList);
                            break;
                    }
                    break;
            }

            return aGridData;
        }

        private Extent GetDiscretedDataExtent(double[,] discretedData)
        {
            Extent dataExtent = new Extent();
            double minX, maxX, minY, maxY;
            minX = 0;
            maxX = 0;
            minY = 0;
            maxY = 0;
            for (int i = 0; i < discretedData.GetLength(1); i++)
            {
                if (i == 0)
                {
                    minX = discretedData[0, i];
                    maxX = minX;
                    minY = discretedData[1, i];
                    maxY = minY;
                }
                else
                {
                    if (minX > discretedData[0, i])
                    {
                        minX = discretedData[0, i];
                    }
                    else if (maxX < discretedData[0, i])
                    {
                        maxX = discretedData[0, i];
                    }
                    if (minY > discretedData[1, i])
                    {
                        minY = discretedData[1, i];
                    }
                    else if (maxY < discretedData[1, i])
                    {
                        maxY = discretedData[1, i];
                    }
                }
            }
            dataExtent.minX = minX;
            dataExtent.maxX = maxX;
            dataExtent.minY = minY;
            dataExtent.maxY = maxY;

            return dataExtent;
        }

        #endregion

        #region Updata Parameters

        private void UpdateProjection()
        {
            if (_meteoDataInfo.ProjInfo.ToProj4String() != frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ToProj4String())
            {
                if (MessageBox.Show("Different projection! If project?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(_meteoDataInfo.ProjInfo);
            }
        }

        private void updateParameters()
        {
            this.Text = this.Text.Split('-')[0].Trim() + " - " + _meteoDataInfo.DataType.ToString();

            int i;
            DataInfo aDataInfo = _meteoDataInfo.DataInfo;

            foreach (ToolStripItem aItem in toolStrip2.Items)
            {
                aItem.Enabled = true;
            }
            TSB_DrawSetting.Enabled = false;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;

            //Projection
            UpdateProjection();

            //Set draw type
            string dtStr = CB_DrawType.Text;
            CB_DrawType.Items.Clear();
            if (_meteoDataInfo.IsGridData)
            {
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                if (_meteoDataInfo.DataInfo.VariableNum >= 2)
                {
                    CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
                }
            }
            else if (_meteoDataInfo.IsStationData)
            {
                switch (_meteoDataInfo.DataType)
                {
                    case MeteoDataType.HYSPLIT_Particle:
                        CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                        break;
                    default:
                        CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                        CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                        break;
                }
            }
            else if (_meteoDataInfo.IsSWATHData)
            {
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            }
            else
            {
                CB_DrawType.Items.Add(DrawType2D.Traj_Line.ToString());
                CB_DrawType.Items.Add(DrawType2D.Traj_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Traj_StartPoint.ToString());
            }

            int idx = CB_DrawType.Items.IndexOf(dtStr);
            if (idx >= 0)
                CB_DrawType.SelectedIndex = idx;
            else
                this.CB_DrawType.SelectedIndex = 0;

            //Set vars
            string varStr = CB_Variable.Text;
            this.CB_Variable.Enabled = true;
            this.CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.VariableNum; i++)
            {
                Variable var = aDataInfo.Variables[i];
                if (var.IsPlottable)
                {
                    this.CB_Variable.Items.Add(var.Name);
                }
            }

            if (_meteoDataInfo.VariableIndex < this.CB_Variable.Items.Count)
            {
                this.CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
            }
            else
            {
                if (this.CB_Variable.Items.Count > 0)
                {
                    idx = CB_Variable.Items.IndexOf(varStr);
                    if (idx >= 0)
                        CB_Variable.SelectedIndex = idx;
                    else
                        this.CB_Variable.SelectedIndex = 0;
                }
            }
        }

        ///// <summary>
        ///// Update form
        ///// </summary>
        //public void UpdateForm()
        //{
        //    this.Text = this.Text.Split('-')[0].Trim() + " - " + _meteoDataInfo.DataType.ToString();
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.GrADS_Grid:
        //            UpdateParasGrADSGrid();
        //            break;
        //        case MeteoDataType.GrADS_Station:
        //            UpdateParasGrADSStation();
        //            break;
        //        case MeteoDataType.ASCII_Grid:
        //            UpdateParasASCIIGrid();
        //            break;
        //        case MeteoDataType.ARL_Grid:
        //            UpdateParasARLGrid();
        //            break;
        //        case MeteoDataType.AWX:
        //            UpdateParasAWX();
        //            break;
        //        case MeteoDataType.GRIB1:
        //            UpdateParasGRIB1();
        //            break;
        //        case MeteoDataType.GRIB2:
        //            UpdateParasGRIB2();
        //            break;
        //        case MeteoDataType.HDF:
        //            UpdateParasHDFGrid();
        //            break;
        //        case MeteoDataType.HRIT:
        //            UpdateParasHRIT();
        //            break;
        //        case MeteoDataType.HYSPLIT_Conc:
        //            UpdateParasHYSPLITConc();
        //            break;
        //        case MeteoDataType.HYSPLIT_Particle:
        //            UpdateParasHYSPLITParticle();
        //            break;
        //        case MeteoDataType.HYSPLIT_Traj:
        //            UpdateParasHYSPLITTraj();
        //            break;
        //        case MeteoDataType.ISH:
        //            //UpdateParasISH();
        //            break;
        //        case MeteoDataType.LonLatStation:
        //            UpdateParasLonLatStation();
        //            break;
        //        case MeteoDataType.METAR:
        //            UpdateParasMETAR();
        //            break;
        //        case MeteoDataType.MICAPS_1:
        //            UpdateParasMICAPS1(false);
        //            break;
        //        case MeteoDataType.MICAPS_11:
        //            UpdateParasMICAPS11();
        //            break;
        //        case MeteoDataType.MICAPS_13:
        //            UpdateParasMICAPS13();
        //            break;
        //        case MeteoDataType.MICAPS_2:
        //            UpdateParasMICAPS2(false);
        //            break;
        //        case MeteoDataType.MICAPS_3:
        //            UpdateParasMICAPS3(false);
        //            break;
        //        case MeteoDataType.MICAPS_4:
        //            UpdateParasMICAPS4();
        //            break;
        //        case MeteoDataType.MICAPS_7:
        //            UpdateParasMICAPS7();
        //            break;
        //        case MeteoDataType.NetCDF:
        //            UpdateParasNetCDFGrid();
        //            break;
        //        case MeteoDataType.Sufer_Grid:
        //            UpdateParasASCIIGrid();
        //            break;
        //        case MeteoDataType.SYNOP:
        //            UpdateParasSYNOP();
        //            break;
        //    }
        //}

        private void UpdateParasGrADSGrid()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;

            foreach (ToolStripItem aItem in toolStrip2.Items)
            {
                aItem.Enabled = true;
            }
            TSB_DrawSetting.Enabled = false;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
            this.splitContainer1.Panel2.Enabled = true;

            TSB_Setting.Enabled = true;

            //Projection
            UpdateProjection();

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            for (i = 0; i < aDataInfo.TDEF.TNum; i++)
            {
                CB_Time.Items.Add(aDataInfo.TDEF.times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.VARDEF.VNum; i++)
            {
                CB_Variable.Items.Add(aDataInfo.VARDEF.Vars[i].Name);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
        }

        private void UpdateParasGrADSStation()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;

            foreach (ToolStripItem aItem in toolStrip2.Items)
            {
                aItem.Enabled = true;
            }
            TSB_DrawSetting.Enabled = false;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
            this.splitContainer1.Panel2.Enabled = true;

            if (aDataInfo.UpperVariables.Count == 0)
            {
                TSB_Setting.Visible = true;
                TSB_Setting.Enabled = false;
                TSB_SectionPlot.Enabled = false;
                TSB_1DPlot.Enabled = false;
            }
            _gridInterp.UnDefData = aDataInfo.MissingValue;
            _useSameGridInterSet = false;
            //UpdateParasGrADSStation();

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());

            //ArrayList varList = new ArrayList();
            //for (i = 0; i < aDataInfo.VARDEF.Vars.Length; i++)
            //{
            //    varList.Add(aDataInfo.VARDEF.Vars[i].VName.ToUpper());
            //}
            //if (varList.Contains("U") && varList.Contains("V"))
            //{
            //    CB_DrawType.Items.Add("Barb");                
            //}
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            for (i = 0; i < aDataInfo.TDEF.TNum; i++)
            {
                CB_Time.Items.Add(aDataInfo.TDEF.times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.VARDEF.VNum; i++)
            {
                CB_Variable.Items.Add(aDataInfo.VARDEF.Vars[i].Name);
            }
            CB_Variable.SelectedIndex = 0;
        }

        private void UpdateParasMICAPS1(bool OnlyTime)
        {
            MICAPS1DataInfo aDataInfo = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            if (!OnlyTime)
            {
                //Set draw type
                string aDrawType = CB_DrawType.Text;
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
                if (CB_DrawType.Items.Contains(aDrawType))
                    CB_DrawType.SelectedIndex = CB_DrawType.Items.IndexOf(aDrawType);
                else
                    CB_DrawType.SelectedIndex = 0;

                //Set vars
                CB_Variable.Enabled = true;
                string aVar = CB_Variable.Text;
                CB_Variable.Items.Clear();
                for (int i = 0; i < aDataInfo.VarList.Count; i++)
                {
                    CB_Variable.Items.Add(aDataInfo.VarList[i]);
                }
                if (aDataInfo.hasAllCols)
                {
                    CB_Variable.Items.Add("TempVar24h");
                    CB_Variable.Items.Add("PressVar24h");
                }
                if (CB_Variable.Items.Contains(aVar))
                    CB_Variable.SelectedIndex = CB_Variable.Items.IndexOf(aVar);
                else
                    CB_Variable.SelectedIndex = 0;

                //Set level
                CB_Level.Items.Clear();
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
        }

        private void UpdateParasMICAPS2(bool OnlyTime)
        {
            MICAPS2DataInfo aDataInfo = (MICAPS2DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            if (!OnlyTime)
            {
                //Set draw type
                string aDrawType = CB_DrawType.Text;
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
                if (CB_DrawType.Items.Contains(aDrawType))
                    CB_DrawType.SelectedIndex = CB_DrawType.Items.IndexOf(aDrawType);
                else
                    CB_DrawType.SelectedIndex = 0;

                //Set vars
                CB_Variable.Enabled = true;
                string aVar = CB_Variable.Text;
                CB_Variable.Items.Clear();
                for (int i = 0; i < aDataInfo.VarList.Count; i++)
                {
                    CB_Variable.Items.Add(aDataInfo.VarList[i]);
                }
                if (CB_Variable.Items.Contains(aVar))
                    CB_Variable.SelectedIndex = CB_Variable.Items.IndexOf(aVar);
                else
                    CB_Variable.SelectedIndex = 0;

                //Set level
                CB_Level.Items.Clear();
                CB_Level.Items.Add(aDataInfo.Level.ToString());
                CB_Level.SelectedIndex = 0;
            }
        }

        private void UpdateParasMICAPS3(bool OnlyTime)
        {
            MICAPS3DataInfo aDataInfo = (MICAPS3DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            if (!OnlyTime)
            {
                //Set draw type
                string aDrawType = CB_DrawType.Text;
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
                //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                //CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
                //CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
                if (CB_DrawType.Items.Contains(aDrawType))
                    CB_DrawType.SelectedIndex = CB_DrawType.Items.IndexOf(aDrawType);
                else
                    CB_DrawType.SelectedIndex = 0;

                //Set vars
                CB_Variable.Enabled = true;
                string aVar = CB_Variable.Text;
                CB_Variable.Items.Clear();
                for (int i = 0; i < aDataInfo.VarList.Count; i++)
                {
                    CB_Variable.Items.Add(aDataInfo.VarList[i]);
                }
                if (CB_Variable.Items.Contains(aVar))
                    CB_Variable.SelectedIndex = CB_Variable.Items.IndexOf(aVar);
                else
                    CB_Variable.SelectedIndex = 0;

                //Set level
                CB_Level.Items.Clear();
                switch (aDataInfo.Level)
                {
                    case -1:
                        CB_Level.Items.Add("Rain6h");
                        break;
                    case -2:
                        CB_Level.Items.Add("Rain24h");
                        break;
                    case -3:
                        CB_Level.Items.Add("Temperature");
                        break;
                    default:
                        CB_Level.Items.Add(aDataInfo.Level.ToString());
                        break;
                }
                CB_Level.SelectedIndex = 0;
            }
        }

        private void UpdateParasMICAPS4()
        {
            MICAPS4DataInfo aDataInfo = (MICAPS4DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Barb.ToString()); 
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add(aDataInfo.VariableNames[0]);
            CB_Variable.SelectedIndex = 0;
            //CB_Vars.Enabled = false;

            //Set level
            CB_Level.Items.Clear();
            CB_Level.Items.Add(aDataInfo.level.ToString());
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasMICAPS7()
        {
            MICAPS7DataInfo aDataInfo = (MICAPS7DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Traj_Line.ToString());
            CB_DrawType.Items.Add(DrawType2D.Traj_StartPoint.ToString());
            CB_DrawType.Items.Add(DrawType2D.Traj_Point.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set trajs
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Traj_Text");
            CB_Time.Items.Clear();
            for (int i = 0; i < aDataInfo.TrajeoryNumber; i++)
            {
                CB_Time.Items.Add((i + 1).ToString());
            }
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add("Trajectory");
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("All");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasMICAPS11()
        {
            MICAPS11DataInfo aDataInfo = (MICAPS11DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.Text = DrawType2D.Vector.ToString();

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (int i = 0; i < aDataInfo.VariableNames.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.VariableNames[i]);
            }
            CB_Variable.SelectedIndex = 0;

            //Set level
            CB_Level.Items.Clear();
            CB_Level.Items.Add(aDataInfo.level.ToString());
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasMICAPS13()
        {
            MICAPS13DataInfo aDataInfo = (MICAPS13DataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set time
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.Time.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add("var");
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("undef");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasAWX()
        {
            AWXDataInfo aDataInfo = (AWXDataInfo)_meteoDataInfo.DataInfo;

            //Projection
            UpdateProjection();
            //if (aDataInfo.ProjInfo.ToProj4String() != frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ToProj4String())
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

            //UpdateParasAWX();
            TSB_DataInfo.Enabled = true;
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            switch (aDataInfo.ProductType)
            {
                case 1:
                case 3:
                    CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                    break;
                case 4:
                    CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                    CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                    break;
            }
            CB_DrawType.SelectedIndex = 0;

            //Set time
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.STime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            foreach (string vName in aDataInfo.VarList)
                CB_Variable.Items.Add(vName);
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("Undef");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasHRIT()
        {
            HRITDataInfo aDataInfo = (HRITDataInfo)_meteoDataInfo.DataInfo;

            //Projection
            UpdateProjection();
            //HRITDataInfo aDataInfo = (HRITDataInfo)aDataInfo.DataInfo;
            //if (aDataInfo.ProjInfo.ToProj4String() != frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ToProj4String())
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

            //UpdateParasHRIT();
            TSB_DataInfo.Enabled = true;
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set time
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.STime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            foreach (string vName in aDataInfo.varList)
                CB_Variable.Items.Add(vName);
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("var");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasHYSPLITConc()
        {
            int i;
            HYSPLITConcDataInfo aDataInfo = new HYSPLITConcDataInfo();
            aDataInfo = (HYSPLITConcDataInfo)_meteoDataInfo.DataInfo;

            //UpdateParasHYSPLITConc();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Visible = false;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            for (i = 0; i < aDataInfo.time_num; i++)
            {
                CB_Time.Items.Add(aDataInfo.sample_start[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;

            //Set vars
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.pollutant_num; i++)
            {
                CB_Variable.Items.Add(aDataInfo.pollutants[i]);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;

            //Set levels
            CB_Level.Items.Clear();
            for (i = 0; i < aDataInfo.level_num; i++)
            {
                CB_Level.Items.Add(aDataInfo.heights[i].ToString());
            }
            CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
        }

        private void UpdateParasHYSPLITParticle()
        {
            HYSPLITParticleInfo aDataInfo = (HYSPLITParticleInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;

            //UpdateParasHYSPLITParticle();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;
            TSB_SectionPlot.Enabled = true;
            TSB_1DPlot.Enabled = false;

            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());            
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            foreach (DateTime t in aDataInfo.Times)
                CB_Time.Items.Add(t.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add("Particle");
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("All");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasHYSPLITTraj()
        {
            HYSPLITTrajectoryInfo aDataInfo = (HYSPLITTrajectoryInfo)_meteoDataInfo.DataInfo;

            //UpdateParasHYSPLITTraj();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_DrawSetting.Enabled = false;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;
            TSB_SectionPlot.Enabled = false;
            TSB_1DPlot.Enabled = false;

            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Traj_Line.ToString());
            CB_DrawType.Items.Add(DrawType2D.Traj_StartPoint.ToString());
            CB_DrawType.Items.Add(DrawType2D.Traj_Point.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set trajs
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Traj_Text");
            CB_Time.Items.Clear();
            for (int i = 0; i < aDataInfo.TrajeoryNumber; i++)
            {
                CB_Time.Items.Add((i + 1).ToString());
            }
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add("Trajectory");
            CB_Variable.SelectedIndex = 0;

            //Set Levels
            CB_Level.Items.Clear();
            CB_Level.Items.Add("All");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasARLGrid()
        {
            int i;
            ARLDataInfo aDataInfo = new ARLDataInfo();
            aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;

            //UpdateParasASCIIGrid();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;

            //Projection
            UpdateProjection();

            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;
            TSB_SectionPlot.Enabled = true;
            TSB_1DPlot.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            for (i = 0; i < aDataInfo.TimeNum; i++)
            {
                CB_Time.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;

            //Set vars
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;                               
        }

        //private void UpdateParasNetCDFGrid()
        //{
        //    int i;
        //    NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
        //    aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;

        //    //Projection
        //    UpdateProjection();
        //    //if (aDataInfo.ProjInfo.Transform.Proj4Name != "lonlat")
        //    //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

        //    //Update
        //    //UpdateParasNetCDFGrid();
        //    GB_DataSet.Enabled = true;
        //    TSB_Setting.Enabled = true;
        //    TSB_ClearDrawing.Enabled = true;
        //    TSB_DataInfo.Enabled = true;
        //    TSB_1DPlot.Enabled = true;
        //    TSB_SectionPlot.Enabled = true;

        //    //Set draw type
        //    CB_DrawType.Items.Clear();
        //    CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
        //    CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
        //    CB_DrawType.SelectedIndex = 0;

        //    //Set times
        //    Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
        //    CB_Time.Items.Clear();
        //    if (aDataInfo.times.Count > 0)
        //    {
        //        for (i = 0; i < aDataInfo.times.Count; i++)
        //        {
        //            CB_Time.Items.Add(aDataInfo.times[i].ToString("yyyy-MM-dd HH:mm"));
        //        }
        //        CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;
        //    }

        //    //Set vars
        //    CB_Variable.Items.Clear();
        //    for (i = 0; i < aDataInfo.varList.Count; i++)
        //    {
        //        if (aDataInfo.varList[i].DimNumber > 1)
        //            CB_Variable.Items.Add(aDataInfo.varList[i].Name);
        //    }

        //    if (_meteoDataInfo.VariableIndex < CB_Variable.Items.Count)
        //        CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
        //    else
        //        CB_Variable.SelectedIndex = 0;
        //}

        private void UpdateParasHDFGrid()
        {
            int i;
            HDF5DataInfo aDataInfo = (HDF5DataInfo)_meteoDataInfo.DataInfo;

            UpdateProjection();

            //UpdateParasHDFGrid();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;
            TSB_1DPlot.Enabled = false;
            TSB_SectionPlot.Enabled = false;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());            
            //CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            if (aDataInfo.Times.Count > 0)
            {
                for (i = 0; i < aDataInfo.Times.Count; i++)
                {
                    CB_Time.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
                }
                CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;
            }
            else
            {
                CB_Time.Items.Add("Undef");
                CB_Time.SelectedIndex = 0;
            }

            //Set vars
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
        }

        private void UpdateParasSYNOP()
        {
            int i;
            SYNOPDataInfo aDataInfo = (SYNOPDataInfo)_meteoDataInfo.DataInfo;

            _gridInterp.UnDefData = aDataInfo.MissingValue;
            _useSameGridInterSet = false;

            //UpdateParasSYNOP();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
            CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.DateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.VarList.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.VarList[i]);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;

            //Set Level
            CB_Level.Items.Clear();
            CB_Level.Items.Add("Surface");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasMETAR()
        {
            int i;
            METARDataInfo aDataInfo = (METARDataInfo)_meteoDataInfo.DataInfo;

            _gridInterp.UnDefData = aDataInfo.MissingValue;
            _useSameGridInterSet = false;

            //UpdateParasMETAR();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
            CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.dateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.varList.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.varList[i]);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;

            //Set Level
            CB_Level.Items.Clear();
            CB_Level.Items.Add("Surface");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasISH(bool OnlyUpdateTime)
        {
            int i;
            ISHDataInfo aDataInfo = (ISHDataInfo)_meteoDataInfo.DataInfo;

            _useSameGridInterSet = false;

            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            TSB_DataInfo.Enabled = true;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add(aDataInfo.dateTime.ToString("yyyy-MM-dd HH:mm"));
            CB_Time.SelectedIndex = 0;

            if (!OnlyUpdateTime)
            {
                //Set draw type
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
                CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
                CB_DrawType.SelectedIndex = 0;

                //Set vars
                CB_Variable.Enabled = true;
                CB_Variable.Items.Clear();
                for (i = 0; i < aDataInfo.varList.Count; i++)
                {
                    CB_Variable.Items.Add(aDataInfo.varList[i]);
                }
                CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;

                //Set Level
                CB_Level.Items.Clear();
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
        }

        private void UpdateParasLonLatStation()
        {
            int i;
            LonLatStationDataInfo aDataInfo = (LonLatStationDataInfo)_meteoDataInfo.DataInfo;

            _gridInterp.UnDefData = aDataInfo.MissingValue;
            _useSameGridInterSet = false;

            //UpdateParasLonLatStation();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = false;
            TSB_ClearDrawing.Enabled = true;

            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Station_Info.ToString());
            if (aDataInfo.VariableNum > 0)
            {
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
                CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            }
            //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Weather_Symbol.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Station_Model.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            //Lab_Time.Text = "Time:";
            CB_Time.Items.Clear();
            CB_Time.Items.Add("Undef");
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Enabled = true;
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.VariableNum; i++)
            {
                CB_Variable.Items.Add(aDataInfo.VariableNames[i]);
            }
            if (CB_Variable.Items.Count > 0)
                CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;

            //Set Level
            CB_Level.Items.Clear();
            CB_Level.Items.Add("Undef");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasASCIIGrid()
        {
            //UpdateParasASCIIGrid();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            if (_meteoDataInfo.DataType != MeteoDataType.Sufer_Grid)
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());    
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            CB_Time.Items.Add("Undef");
            CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Items.Clear();
            CB_Variable.Items.Add("var");
            CB_Variable.SelectedIndex = 0;
            //CB_Vars.Enabled = false;

            //Set level
            CB_Level.Items.Clear();
            CB_Level.Items.Add("Undef");
            CB_Level.SelectedIndex = 0;
        }

        private void UpdateParasGRIB1()
        {
            int i;
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;

            //Projection
            UpdateProjection();
            //if (aDataInfo.ProjInfo.Transform.Proj4Name != "lonlat")
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

            //UpdateParasGRIB1();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;
            TSB_1DPlot.Enabled = true;
            TSB_SectionPlot.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            //CB_Time.Items.Clear();
            //for (i = 0; i < aDataInfo.TimeList.Count; i++)
            //{
            //    CB_Time.Items.Add(aDataInfo.TimeList[i].ToString("yyyy-MM-dd HH:mm"));
            //}
            //CB_Time.SelectedIndex = 0;

            //Set vars
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
        }

        private void UpdateParasGRIB2()
        {
            int i;
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;

            //Projection
            UpdateProjection();
            //if (aDataInfo.ProjInfo.Transform.Proj4Name != "lonlat")
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

            //UpdateParasGRIB2();
            this.splitContainer1.Panel2.Enabled = true;
            TSB_Setting.Enabled = true;
            TSB_ClearDrawing.Enabled = true;
            TSB_DataInfo.Enabled = true;
            TSB_1DPlot.Enabled = true;
            TSB_SectionPlot.Enabled = true;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
            CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
            CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
            CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
            CB_DrawType.Items.Add(DrawType2D.Barb.ToString());
            CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            CB_DrawType.SelectedIndex = 0;

            //Set times
            Lab_Time.Text = Resources.GlobalResource.ResourceManager.GetString("Time_Text");
            CB_Time.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time.Items.Add(aDataInfo.Times[i].ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;

            //Set vars
            CB_Variable.Items.Clear();
            for (i = 0; i < aDataInfo.Variables.Count; i++)
            {
                CB_Variable.Items.Add(aDataInfo.Variables[i].Name);
            }
            CB_Variable.SelectedIndex = _meteoDataInfo.VariableIndex;
        }

        #endregion        

        #endregion

        #region Methods
        /// <summary>
        /// Apply resource
        /// </summary>
        public void ApplyResource()
        {
            ComponentResourceManager res = new ComponentResourceManager(typeof(frmMeteoData));
            //Controls
            foreach (Control ctl in this.Controls)
            {
                res.ApplyResources(ctl, ctl.Name, System.Threading.Thread.CurrentThread.CurrentUICulture);
            }

            foreach (Control ctl in this.splitContainer1.Panel2.Controls)
                res.ApplyResources(ctl, ctl.Name, System.Threading.Thread.CurrentThread.CurrentUICulture);

            //Toolstrip
            foreach (ToolStripItem ctl in this.toolStrip2.Items)
                res.ApplyResources(ctl, ctl.Name, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }                        

        private void frmMeteoData_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Set default legend scheme
        /// </summary>
        /// <param name="aLS">legend scheme</param>
        public void SetLegendScheme(LegendScheme aLS)
        {
            _legendScheme = aLS;            
        }        
                
        private void TSB_Draw_Click(object sender, EventArgs e)
        {
            string fieldName = CB_Variable.Text;
            Display();      
        }                

        ///// <summary>
        ///// Get grid data
        ///// </summary>
        ///// <param name="varName">variable name</param>
        ///// <returns>grid data</returns>
        //private GridData GetGridData(string varName)
        //{
        //    GridData aGridData = null;
        //    _TimeIdx = CB_Time.SelectedIndex;
        //    _LevelIdx = CB_Level.SelectedIndex;
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.GrADS_Grid:
        //            GrADSDataInfo aGDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
        //            int i = 0;
        //            _VarIdx = -1;
        //            foreach (VAR aVar in aGDataInfo.VARDEF.Vars)
        //            {
        //                if (aVar.VName == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aGDataInfo.GetGridData_LonLat(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //        case MeteoDataType.ARL_Grid:
        //            ARLDataInfo aARLDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aARLDataInfo.varList[_LevelIdx])
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aARLDataInfo.GetGridData_LonLat(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //        case MeteoDataType.ASCII_Grid:
        //            ASCIIGRIDDataInfo aGridDataInfo = (ASCIIGRIDDataInfo)_meteoDataInfo.DataInfo;
        //            aGridData = aGridDataInfo.GetGridData();
        //            break;
        //        case MeteoDataType.HYSPLIT_Conc:
        //            HYSPLITConcDataInfo aHCDataInfo = (HYSPLITConcDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aHCDataInfo.pollutants)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aHCDataInfo.GetGridData(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //        case MeteoDataType.MICAPS_4:
        //            MICAPS4DataInfo aM4DataInfo = (MICAPS4DataInfo)_meteoDataInfo.DataInfo;
        //            aGridData = aM4DataInfo.GetGridData();
        //            break;
        //        case MeteoDataType.MICAPS_11:
        //            MICAPS11DataInfo aM11DataInfo = (MICAPS11DataInfo)_meteoDataInfo.DataInfo;
        //            if (varName.ToLower() == "u")
        //                _VarIdx = 0;
        //            else if (varName.ToLower() == "v")
        //                _VarIdx = 1;
        //            else
        //                _VarIdx = -1;

        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aM11DataInfo.GetGridData(_VarIdx);
        //            break;
        //        case MeteoDataType.NetCDF:
        //            NetCDFDataInfo aNetCDFDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (Variable aVar in aNetCDFDataInfo.varList)
        //            {
        //                if (aVar.Name == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aNetCDFDataInfo.GetGridData_LonLat(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //        case MeteoDataType.Sufer_Grid:
        //            SurferGridDataInfo aSGridDataInfo = (SurferGridDataInfo)_meteoDataInfo.DataInfo;
        //            aGridData = aSGridDataInfo.GetGridData();
        //            break;
        //        case MeteoDataType.GRIB1:
        //            GRIB1DataInfo aGRIB1DataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;                    
        //            _VarIdx = -1;
        //            i = 0;
        //            foreach (Variable aVar in aGRIB1DataInfo.Variables)
        //            {
        //                if (aVar.Name == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aGRIB1DataInfo.GetGridData_LonLat(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //        case MeteoDataType.GRIB2:
        //            GRIB2DataInfo aGRIB2DataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;
        //            _VarIdx = -1;
        //            i = 0;
        //            foreach (Parameter aVar in aGRIB2DataInfo.Variables)
        //            {
        //                if (aVar.Name == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aGridData = aGRIB2DataInfo.GetGridData_LonLat(_TimeIdx, _VarIdx, _LevelIdx);
        //            break;
        //    }

        //    return aGridData;
        //}

        ///// <summary>
        ///// Get station data
        ///// </summary>
        ///// <param name="varName">variable index</param>
        ///// <returns>station data</returns>
        //private StationData GetStationData(string varName)
        //{
        //    StationData aStData = null;
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.MICAPS_1:
        //            MICAPS1DataInfo aM1DataInfo = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;
        //            int i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aM1DataInfo.VarList)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aStData = aM1DataInfo.GetStationData(_VarIdx);
        //            break;
        //        case MeteoDataType.MICAPS_3:
        //            MICAPS3DataInfo aM3DataInfo = (MICAPS3DataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aM3DataInfo.VarList)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aStData = aM3DataInfo.GetStationData(_VarIdx);
        //            break;
        //        case MeteoDataType.GrADS_Station:
        //            GrADSDataInfo aGDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (VAR aVar in aGDataInfo.VARDEF.Vars)
        //            {
        //                if (aVar.VName == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            List<STData> stationData = aGDataInfo.ReadGrADSData_Station(_TimeIdx);
        //            aStData = aGDataInfo.GetGroundStationData(stationData, _VarIdx);
        //            break;
        //        case MeteoDataType.HYSPLIT_Particle:
        //            HYSPLITParticleInfo aHPDataInfo = (HYSPLITParticleInfo)_meteoDataInfo.DataInfo;
        //            aStData = aHPDataInfo.GetStationData(_TimeIdx);
        //            break;
        //        case MeteoDataType.ISH:
        //            ISHDataInfo aISHDataInfo = (ISHDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aISHDataInfo.varList)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aStData = aISHDataInfo.GetStationData(_VarIdx);
        //            break;
        //        case MeteoDataType.LonLatStation:
        //            LonLatStationDataInfo aLLDataInfo = (LonLatStationDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aLLDataInfo.VarList)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aStData = aLLDataInfo.GetStationData(_VarIdx);
        //            break;
        //        case MeteoDataType.METAR:
        //            METARDataInfo aMETARDataInfo = (METARDataInfo)_meteoDataInfo.DataInfo;
        //            i = 0;
        //            _VarIdx = -1;
        //            foreach (string aVar in aMETARDataInfo.varList)
        //            {
        //                if (aVar == varName)
        //                {
        //                    _VarIdx = i;
        //                    break;
        //                }
        //                i++;
        //            }
        //            if (_VarIdx == -1)
        //                return null;

        //            aStData = aMETARDataInfo.GetStationData(_VarIdx);
        //            break;
        //    }

        //    return aStData;
        //}

        private bool GetUVString()
        {
            List<string> vList = new List<string>();
            for (int i = 0; i < CB_Variable.Items.Count; i++)
            {
                vList.Add(CB_Variable.Items[i].ToString());
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
                        _meteoDataInfo.MeteoUVSet.UStr = UStr;
                        _meteoDataInfo.MeteoUVSet.VStr = VStr;
                        _meteoDataInfo.MeteoUVSet.IsFixUVStr = true;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("U/V variables were not set!", "Error");
                        return false;
                    }
                }
                else
                    return true;
            }
            else
                return true;
        }

        //private bool GetUVGridData(ref double[,] Udata, ref double[,] Vdata)
        //{
        //    int i;            
        //    int tIdx, vIdx, lIdx;
        //    tIdx = CB_Time.SelectedIndex;
        //    lIdx = CB_Level.SelectedIndex;
        //    List<string> vList = new List<string>();
        //    for (i = 0; i < CB_Variable.Items.Count; i++)
        //    {
        //        vList.Add(CB_Variable.Items[i].ToString());
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
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.GrADS_Grid:
        //            //GrADSData CGrADSData = new GrADSData();
        //            GrADSDataInfo aGrADSDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
        //            Udata = aGrADSDataInfo.GetGridData_LonLat.ReadGrADSData_Grid(((GrADSDataInfo)_meteoDataInfo.DataInfo).DSET, tIdx, vIdx,
        //                lIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
        //            Vdata = CGrADSData.ReadGrADSData_Grid(((GrADSDataInfo)_meteoDataInfo.DataInfo).DSET, tIdx, vIdx,
        //                lIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
        //            break;
        //        case MeteoDataType.NetCDF:
        //            NetCDFDataInfo aNCDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
        //            Udata = aNCDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
        //            Vdata = aNCDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            break;
        //        case MeteoDataType.MICAPS_11:
        //            MICAPS11DataInfo aM11DataInfo = (MICAPS11DataInfo)_meteoDataInfo.DataInfo;
        //            Udata = aM11DataInfo.UGridData;
        //            Vdata = aM11DataInfo.VGridData;
        //            break;
        //        case MeteoDataType.ARL_Grid:
        //            ARLMeteoData CARLData = new ARLMeteoData();
        //            ARLDataInfo aARLDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
        //            Udata = aARLDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
        //            Vdata = aARLDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            break;
        //        case MeteoDataType.GRIB1:
        //            GRIB1DataInfo aG1DataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
        //            Udata = aG1DataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            vIdx = CB_Variable.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
        //            Vdata = aG1DataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx).Data;
        //            break;
        //        default:
        //            return false;
        //            //break;
        //    }

        //    return true;
        //}

        private bool GetUVGridData(ref GridData Udata, ref GridData Vdata)
        {
            int i;            
            
            //Set U/V strings
            if (!_meteoDataInfo.MeteoUVSet.IsFixUVStr)
            {
                List<string> vList = new List<string>();
                for (i = 0; i < CB_Variable.Items.Count; i++)
                {
                    vList.Add(CB_Variable.Items[i].ToString());
                }

                if (!_meteoDataInfo.MeteoUVSet.AutoSetUVStr(vList))
                {
                    frmUVSet aFrmUVSet = new frmUVSet();
                    aFrmUVSet.SetUVItems(vList);
                    aFrmUVSet.RB_U_V.Checked = true;
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

            //Get U/V grid data            
            Udata = _meteoDataInfo.GetGridData(_meteoDataInfo.MeteoUVSet.UStr);
            Vdata = _meteoDataInfo.GetGridData(_meteoDataInfo.MeteoUVSet.VStr);            

            //int tIdx, vIdx, lIdx;
            //tIdx = CB_Time.SelectedIndex;
            //lIdx = CB_Level.SelectedIndex;
            //switch (_meteoDataInfo.DataType)
            //{
            //    case MeteoDataType.GrADS_Grid:
            //        GrADSData CGrADSData = new GrADSData();
            //        GrADSDataInfo aGrADSDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
            //        Udata = aGrADSDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
            //        Vdata = aGrADSDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        break;
            //    case MeteoDataType.NetCDF:
            //        NetCDFDataInfo aNCDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
            //        Udata = aNCDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
            //        Vdata = aNCDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        break;
            //    case MeteoDataType.MICAPS_11:
            //        MICAPS11DataInfo aM11DataInfo = (MICAPS11DataInfo)_meteoDataInfo.DataInfo;
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
            //        Udata = aM11DataInfo.GetGridData(vIdx);
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
            //        Vdata = aM11DataInfo.GetGridData(vIdx);
            //        break;
            //    case MeteoDataType.ARL_Grid:
            //        ARLDataInfo arlDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
            //        Udata = arlDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
            //        Vdata = arlDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        break;
            //    case MeteoDataType.GRIB1:
            //        GRIB1DataInfo aG1DataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.UStr);
            //        Udata = aG1DataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        vIdx = CB_Vars.Items.IndexOf(_meteoDataInfo.MeteoUVSet.VStr);
            //        Vdata = aG1DataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            //        break;
            //    default:
            //        return false;
            //    //break;
            //}

            if (Udata == null || Vdata == null)
                return false;

            //Un stag
            if (Udata.XStag)
                Udata = Udata.UnStagger_X();
            if (Vdata.YStag)
                Vdata = Vdata.UnStagger_Y();

            //Skip the grid data
            if (_meteoDataInfo.MeteoUVSet.SkipY != 1 || _meteoDataInfo.MeteoUVSet.SkipX != 1)
            {
                Udata = Udata.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
                Vdata = Vdata.Skip(_meteoDataInfo.MeteoUVSet.SkipY, _meteoDataInfo.MeteoUVSet.SkipX);
            }

            return true;
        }

        private bool GetUVStationData(ref StationData Udata, ref StationData VData)
        {
            int i;

            //Set U/V strings
            if (!_meteoDataInfo.MeteoUVSet.IsFixUVStr)
            {
                List<string> vList = new List<string>();
                for (i = 0; i < CB_Variable.Items.Count; i++)
                {
                    vList.Add(CB_Variable.Items[i].ToString());
                }

                if (!_meteoDataInfo.MeteoUVSet.AutoSetUVStr(vList))
                {
                    frmUVSet aFrmUVSet = new frmUVSet();
                    aFrmUVSet.SetUVItems(vList);
                    aFrmUVSet.RB_Dir_Speed.Checked = true;
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

            //Get U/V station data
            Udata = _meteoDataInfo.GetStationData(_meteoDataInfo.MeteoUVSet.UStr);
            VData = _meteoDataInfo.GetStationData(_meteoDataInfo.MeteoUVSet.VStr);

            if (Udata == null || VData == null)
                return false;
            else
                return true;
        }

        //private bool GetWindDirSpeedData(ref double[,] windDirData, ref double[,] windSpeedData)
        //{
        //    int vIdx;
        //    bool ifTrue = false;
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.MICAPS_1:
        //            //Get wind direction and speed data
        //            MICAPSData aMICAPSData = new MICAPSData();
        //            MICAPS1DataInfo aMICAPS1DF = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;
        //            Extent aExtent = new Extent();                    
        //            vIdx = CB_Variable.Items.IndexOf("WindDirection") + 5;
        //            windDirData = aMICAPSData.GetDiscreteData_M1(aMICAPS1DF,
        //                vIdx, ref aExtent);
        //            vIdx = CB_Variable.Items.IndexOf("WindSpeed") + 5;
        //            windSpeedData = aMICAPSData.GetDiscreteData_M1(aMICAPS1DF,
        //                vIdx, ref aExtent);
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.METAR:
        //            METARData CMETARData = new METARData();
        //            METARDataInfo aMETARDF = (METARDataInfo)_meteoDataInfo.DataInfo;
        //            aExtent = new Extent();
        //            vIdx = CB_Variable.Items.IndexOf("WindDirection");
        //            windDirData = CMETARData.GetDiscreteData(aMETARDF,
        //                vIdx, ref aExtent);
        //            vIdx = CB_Variable.Items.IndexOf("WindSpeed");
        //            windSpeedData = CMETARData.GetDiscreteData(aMETARDF,
        //                vIdx, ref aExtent);
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.ISH:
        //            ISHDataInfo aISHDF = (ISHDataInfo)_meteoDataInfo.DataInfo;
        //            aExtent = new Extent();
        //            List<string> stIDList = new List<string>();
        //            vIdx = CB_Variable.Items.IndexOf("WindDirection");
        //            windDirData = aISHDF.GetDiscreteData(vIdx, ref stIDList, ref aExtent);
        //            vIdx = CB_Variable.Items.IndexOf("WindSpeed");
        //            windSpeedData = aISHDF.GetDiscreteData(vIdx, ref stIDList, ref aExtent);
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.AWX:
        //            AWXDataInfo aAWXDF = (AWXDataInfo)_meteoDataInfo.DataInfo;
        //            aExtent = new Extent();
        //            vIdx = CB_Variable.Items.IndexOf("WindDirection");
        //            windDirData = aAWXDF.GetStationData(vIdx).Data;
        //            vIdx = CB_Variable.Items.IndexOf("WindSpeed");
        //            windSpeedData = aAWXDF.GetStationData(vIdx).Data;
        //            ifTrue = true;
        //            break;
        //    }

        //    return ifTrue;
        //}

        private void GetWindDirSpeedData(ref StationData windDirData, ref StationData windSpeedData)
        {
            windDirData = _meteoDataInfo.GetStationData("WindDirection");
            windSpeedData = _meteoDataInfo.GetStationData("WindSpeed");            
        }

        //private bool GetWeatherData(ref double[,] weatherData)
        //{
        //    int vIdx;
        //    bool ifTrue = false;     
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.MICAPS_1:
        //            //Get weather data
        //            MICAPS1DataInfo aM1DataInfo = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;                    
        //            Extent aExtent = new Extent();
        //            vIdx = CB_Variable.Items.IndexOf("WeatherNow");
        //            weatherData = aM1DataInfo.GetDiscreteData(vIdx, ref aExtent);                                      
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.METAR:
        //            METARData CMETARData = new METARData();
        //            aExtent = new Extent();
        //            vIdx = CB_Variable.Items.IndexOf("Weather");
        //            weatherData = CMETARData.GetDiscreteData((METARDataInfo)_meteoDataInfo.DataInfo,
        //                vIdx, ref aExtent);
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.SYNOP:
        //            SYNOPDataInfo aSYNOPDataInfo = (SYNOPDataInfo)_meteoDataInfo.DataInfo;
        //            vIdx = CB_Variable.Items.IndexOf("Weather");
        //            weatherData = aSYNOPDataInfo.GetStationData(vIdx).Data;
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.ISH:
        //            ISHDataInfo aISHDF = (ISHDataInfo)_meteoDataInfo.DataInfo;
        //            aExtent = new Extent();
        //            List<string> stIDList = new List<string>();
        //            vIdx = CB_Variable.Items.IndexOf("Weather");
        //            weatherData = aISHDF.GetDiscreteData(vIdx, ref stIDList, ref aExtent);                    
        //            ifTrue = true;
        //            break;
        //    }

        //    return ifTrue;
        //}

        //private StationModelData GetStationModelData()
        //{
        //    return _meteoDataInfo.GetStationModelData();            
        //}

        //private bool GetStationInfoData(ref List<List<string>> stationInfoData, ref List<string> fieldList,
        //    ref List<string> varList)
        //{
        //    bool ifTrue = false;
        //    switch (_meteoDataInfo.DataType)
        //    {
        //        case MeteoDataType.LonLatStation:
        //            LonLatStationDataInfo aLLSDataInfo = (LonLatStationDataInfo)_meteoDataInfo.DataInfo;
        //            stationInfoData = aLLSDataInfo.DataList;
        //            fieldList = aLLSDataInfo.FieldList;
        //            varList = aLLSDataInfo.VarList;
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.MICAPS_3:
        //            MICAPS3DataInfo aM3DataInfo = (MICAPS3DataInfo)_meteoDataInfo.DataInfo;
        //            stationInfoData = aM3DataInfo.DataList;
        //            fieldList = aM3DataInfo.FieldList;
        //            varList = aM3DataInfo.VarList;
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.MICAPS_1:
        //            MICAPS1DataInfo aM1DataInfo = (MICAPS1DataInfo)_meteoDataInfo.DataInfo;
        //            stationInfoData = aM1DataInfo.DataList;
        //            fieldList = aM1DataInfo.FieldList;
        //            varList = aM1DataInfo.VarList;
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.MICAPS_2:
        //            MICAPS2DataInfo aM2DataInfo = (MICAPS2DataInfo)_meteoDataInfo.DataInfo;
        //            stationInfoData = aM2DataInfo.DataList;
        //            fieldList = aM2DataInfo.FieldList;
        //            varList = aM2DataInfo.VarList;
        //            ifTrue = true;
        //            break;
        //        case MeteoDataType.AWX:
        //            AWXDataInfo aAWXDataInfo = (AWXDataInfo)_meteoDataInfo.DataInfo;
        //            stationInfoData = aAWXDataInfo.GetStationInfoData();
        //            fieldList = aAWXDataInfo.FieldList;
        //            varList = aAWXDataInfo.varList;
        //            ifTrue = true;
        //            break;
        //    }

        //    return ifTrue;
        //}

        private GridData GetGrADSGridData()
        {
            GridData gridData = new GridData();

            //Read grid data
            int tIdx, vIdx, lIdx;
            tIdx = CB_Time.SelectedIndex;
            vIdx = CB_Variable.SelectedIndex;
            lIdx = CB_Level.SelectedIndex;
            //GrADSData CGrADSData = new GrADSData();
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;
            //GridData = CGrADSData.ReadGrADSData_Grid(((GrADSDataInfo)_meteoDataInfo.DataInfo).DSET, tIdx, vIdx,
            //    lIdx, (GrADSDataInfo)_meteoDataInfo.DataInfo);
            //GridData = m_GrADSData.ReadGrADSData_Grid(m_GrADSDataInfo.DSET, tIdx, vIdx,
            //    lIdx, m_XNum, m_YNum, m_GridNumXY, m_GridNumTime, m_GrADSDataInfo, m_IsWorld);
            gridData = aDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);
            
            return gridData;
        }

        //private List<STData> GetGrADSStationData()
        //{
        //    List<STData> StationData = new List<STData>();
        //    int tIdx, vNum;
        //    tIdx = CB_Time.SelectedIndex;                      
        //    vNum = CB_Variable.Items.Count;
        //    GrADSData CGrADSData = new GrADSData();
        //    //StationData = CGrADSData.ReadGrADSData_Station(((GrADSDataInfo)_meteoDataInfo.DataInfo).DSET, tIdx, vNum,
        //    //    ((GrADSDataInfo)_meteoDataInfo.DataInfo));
        //    StationData = ((GrADSDataInfo)_meteoDataInfo.DataInfo).ReadGrADSData_Station(tIdx);

        //    return StationData;
        //}

        //private GridData GetHYSPLITConcData()
        //{
        //    GridData gridData;            
        //    HYSPLITConcDataInfo aDataInfo = (HYSPLITConcDataInfo)_meteoDataInfo.DataInfo;

        //    //Read grid data
        //    int tIdx, vIdx, lIdx;
        //    tIdx = CB_Time.SelectedIndex;
        //    vIdx = CB_Vars.SelectedIndex;
        //    lIdx = CB_Level.SelectedIndex;
        //    int aFactor;
        //    aFactor = Convert.ToInt32(TB_Multiplier.Text);
        //    gridData = aDataInfo.GetGridData(tIdx, vIdx, lIdx, aFactor);
                        
        //    return gridData;
        //}

        private GridData GetARLGridData()
        {
            GridData gridData = new GridData();
            ARLDataInfo aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;            

            //Read grid data
            int tIdx, vIdx, lIdx;
            tIdx = CB_Time.SelectedIndex;
            vIdx = CB_Variable.SelectedIndex;
            lIdx = CB_Level.SelectedIndex;           
            gridData = aDataInfo.GetGridData_LonLat(tIdx, vIdx, lIdx);

            return gridData;
        }

        private GridData GetNetCDFGridData()
        {            
            //Read grid data
            int tIdx, vIdx, lIdx;
            tIdx = CB_Time.SelectedIndex;
            vIdx = CB_Variable.SelectedIndex;
            lIdx = CB_Level.SelectedIndex;
            GridData aGridData = ((NetCDFDataInfo)_meteoDataInfo.DataInfo).GetGridData_LonLat(tIdx, vIdx, lIdx);
            _meteoDataInfo.MissingValue = ((NetCDFDataInfo)_meteoDataInfo.DataInfo).MissingValue;

            return aGridData;
        }

        //private void InterpolateGridData(double[,] S)
        //{
        //    switch (_gridInterp.GridInterMethodV)
        //    {
        //        case GridInterMethod.IDW_Radius:
        //            S = ContourDraw.FilterDiscreteData_Radius(S, _gridInterp.Radius,
        //                    _gridInterp.GridDataParaV.dataExtent, _meteoDataInfo.MissingValue);
        //            _gridData = ContourDraw.InterpolateDiscreteData_Radius(S,
        //                m_X, m_Y, _gridInterp.MinPointNum, _gridInterp.Radius, _meteoDataInfo.MissingValue);
        //            break;
        //        case GridInterMethod.IDW_Neighbors:
        //            S = ContourDraw.FilterDiscreteData_Radius(S, _gridInterp.Radius,
        //                    _gridInterp.GridDataParaV.dataExtent, _meteoDataInfo.MissingValue);
        //            _gridData = ContourDraw.InterpolateDiscreteData_Neighbor(S, m_X, m_Y,
        //                _gridInterp.MinPointNum, _meteoDataInfo.MissingValue);
        //            break;
        //        case GridInterMethod.Cressman:
        //            S = ContourDraw.FilterDiscreteData_Radius(S, 0,
        //                    _gridInterp.GridDataParaV.dataExtent, _meteoDataInfo.MissingValue);
        //            _gridData = ContourDraw.InterpolateDiscreteData_Cressman(S, m_X, m_Y, _meteoDataInfo.MissingValue, _gridInterp.RadList);
        //            break;
        //    }
        //}

        private GridData InterpolateGridData(StationData stData, InterpolationSetting aGridInterp)
        {
            GridData aGridData = null;
            double[,] S = stData.Data;
            double[] X = new double[0];
            double[] Y = new double[0];
            ContourDraw.CreateGridXY(aGridInterp.GridDataSet, ref X, ref Y);
            switch (_gridInterp.InterpolationMethod)
            {
                case InterpolationMethods.IDW_Radius:
                    S = ContourDraw.FilterDiscreteData_Radius(S, aGridInterp.Radius,
                            aGridInterp.GridDataSet.DataExtent, stData.MissingValue);
                    aGridData = ContourDraw.InterpolateDiscreteData_Radius(S,
                        X, Y, aGridInterp.MinPointNum, aGridInterp.Radius, stData.MissingValue);
                    break;
                case InterpolationMethods.IDW_Neighbors:
                    S = ContourDraw.FilterDiscreteData_Radius(S, aGridInterp.Radius,
                            aGridInterp.GridDataSet.DataExtent, stData.MissingValue);
                    aGridData = ContourDraw.InterpolateDiscreteData_Neighbor(S, X, Y,
                        aGridInterp.MinPointNum, stData.MissingValue);
                    break;
                case InterpolationMethods.Cressman:
                    S = ContourDraw.FilterDiscreteData_Radius(S, 0,
                            aGridInterp.GridDataSet.DataExtent, stData.MissingValue);
                    aGridData = ContourDraw.InterpolateDiscreteData_Cressman(S, X, Y, stData.MissingValue, aGridInterp.RadList);
                    break;
                case InterpolationMethods.AssignPointToGrid:
                    S = ContourDraw.FilterDiscreteData_Radius(S, 0,
                            aGridInterp.GridDataSet.DataExtent, stData.MissingValue);
                    aGridData = ContourDraw.AssignPointToGrid(S, X, Y, stData.MissingValue);
                    break;
            }

            return aGridData;
        }

        private void CreateLegendScheme_Grid()
        {
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, 
                        LegendType.UniqueValue, ShapeTypes.Polyline, ref _hasUndefData);                                       
                    break;
                case DrawType2D.Shaded:
                case DrawType2D.Grid_Fill:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref _hasUndefData);                                        
                    break;                
                case DrawType2D.Grid_Point:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData,
                         LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);                                      
                    break;
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData,
                            LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData); 
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
                case DrawType2D.Raster:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, LegendType.GraduatedColor,
                         ShapeTypes.Image, ref _hasUndefData);                                       
                    break;
            }
        }

        private void CreateLegendScheme_back()
        {
            switch (_2DDrawType)
            {
                case DrawType2D.Contour:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.UniqueValue, ShapeTypes.Polyline, ref _hasUndefData);
                    break;
                case DrawType2D.Shaded:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref _hasUndefData);
                    break;
                case DrawType2D.Grid_Fill:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref _hasUndefData);
                    break;
                case DrawType2D.Grid_Point:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);
                    break;
                case DrawType2D.Vector:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                            LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);
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
                            LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);
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
                case DrawType2D.Raster:
                    _legendScheme = CreateLegendSchemeFromData(_gridData.Data,
                        LegendType.GraduatedColor, ShapeTypes.Image, ref _hasUndefData);
                    break;
            }
        }

        private LegendScheme CreateLegendScheme_Station()
        {
            switch (_2DDrawType)
            {
                case DrawType2D.Station_Point:
                    _legendScheme = LegendManage.CreateLegendSchemeFromStationData(_stationData, 
                        LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);
                    if (_meteoDataInfo.DataType == MeteoDataType.HDF)
                    {
                        for (int i = 0; i < _legendScheme.BreakNum; i++)
                            ((PointBreak)_legendScheme.LegendBreaks[i]).DrawOutline = false;
                    }
                    break;
                case DrawType2D.Grid_Point:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, 
                        LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);                    
                    break;
                case DrawType2D.Contour:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, 
                        LegendType.UniqueValue, ShapeTypes.Polyline, ref _hasUndefData);                                                                       
                    break;
                case DrawType2D.Shaded:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, 
                        LegendType.GraduatedColor, ShapeTypes.Polygon, ref _hasUndefData);                                                           
                    break;
                case DrawType2D.Raster:
                    _legendScheme = LegendManage.CreateLegendSchemeFromGridData(_gridData, LegendType.GraduatedColor,
                         ShapeTypes.Image, ref _hasUndefData);
                    break;
                case DrawType2D.Vector:
                case DrawType2D.Barb:
                    if (CHB_ColorVar.Checked)
                    {
                        _legendScheme = LegendManage.CreateLegendSchemeFromStationData(_stationData, 
                            LegendType.GraduatedColor, ShapeTypes.Point, ref _hasUndefData);                        
                        for (int i = 0; i < _legendScheme.LegendBreaks.Count; i++)
                        {
                            PointBreak aPB = (PointBreak)_legendScheme.LegendBreaks[i];
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
                case DrawType2D.Weather_Symbol:
                    _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 12);
                    break;
                case DrawType2D.Station_Model:
                    _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Blue, 12);
                    break;
                case DrawType2D.Station_Info:
                    _legendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Red, 6);
                    break;
            }

            return _legendScheme;
        }

        private LegendScheme CreateLegendSchemeFromDiscreteData(double[,] discreteData,
            ShapeTypes aST, ref Boolean hasNoData)
        {
            LegendScheme aLS = new LegendScheme(aST);
            double[] CValues;
            Color[] colors;

            hasNoData = ContourDraw.GetMaxMinValueFDiscreteData(discreteData, _meteoDataInfo.MissingValue, ref m_MinData, ref m_MaxData);
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
                
        private void SetGrADSVar()
        {
            int aIdx = CB_Variable.SelectedIndex;
            int i;
            int levelIdx = CB_Level.SelectedIndex;

            CB_Level.Items.Clear();
            if (((GrADSDataInfo)_meteoDataInfo.DataInfo).VARDEF.Vars[aIdx].LevelNum == 0)
            {
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
            else
            {
                for (i = 0; i < ((GrADSDataInfo)_meteoDataInfo.DataInfo).VARDEF.Vars[aIdx].LevelNum; i++)
                {
                    if (((GrADSDataInfo)_meteoDataInfo.DataInfo).ZDEF.ZNum <= i)
                        break;

                    CB_Level.Items.Add(Convert.ToString(((GrADSDataInfo)_meteoDataInfo.DataInfo).ZDEF.ZLevels[i]));
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }

        private void SetARLVar()
        {
            int aIdx = CB_Variable.SelectedIndex;
            int i;
            int levelIdx = CB_Level.SelectedIndex;
            ARLDataInfo aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;

            CB_Level.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 1)
            {
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level.Items.Add(((float)aDataInfo.levels[aDataInfo.Variables[aIdx].LevelIdxs[i]]).ToString());
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }

        private void SetGRIB1Var()
        {
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;
            int aIdx = CB_Variable.SelectedIndex;
            Variable aVar = aDataInfo.Variables[aIdx];
            int i;
            int levelIdx = CB_Level.SelectedIndex;

            CB_Time.Items.Clear();
            for (i = 0; i < aVar.TDimension.DimLength; i++)
            {
                DateTime aTime = DataConvert.ToDateTime(aVar.TDimension.DimValue[i]);
                CB_Time.Items.Add(aTime.ToString("yyyy-MM-dd HH:mm"));
            }
            CB_Time.SelectedIndex = 0;

            CB_Level.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level.Items.Add(aDataInfo.Variables[aIdx].Levels[i].ToString());
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }
        
        private void TSB_PreTime_Click(object sender, EventArgs e)
        {
            _useSameLegendScheme = true;
            _useSameGridInterSet = true;
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_1:
                case MeteoDataType.MICAPS_2:
                case MeteoDataType.MICAPS_3:
                case MeteoDataType.MICAPS_4:
                case MeteoDataType.MICAPS_7:
                case MeteoDataType.MICAPS_11:
                case MeteoDataType.MICAPS_13:
                    DateTime aTime = _meteoDataInfo.GetTime().AddHours(-3);
                    string aFile = _meteoDataInfo.GetFileName(); ;
                    for (int i = 0; i < 100; i++)
                    {
                        aFile = Path.Combine(Path.GetDirectoryName(aFile), aTime.ToString("yyMMddHH") + ".000");
                        if (File.Exists(aFile))
                        {
                            break;
                        }
                        aTime = aTime.AddHours(-3);
                    }
                    if (File.Exists(aFile))
                    {
                        _meteoDataInfo.OpenMICAPSData(aFile);
                        this.LB_DataFiles.Items[this.LB_DataFiles.SelectedIndex] = Path.GetFileName(_meteoDataInfo.GetFileName());
                    }
                    break;
                case MeteoDataType.ISH:
                    aTime = ((ISHDataInfo)_meteoDataInfo.DataInfo).dateTime.AddHours(-1);
                    aFile = ((ISHDataInfo)_meteoDataInfo.DataInfo).FileName;
                    for (int i = 0; i < 100; i++)
                    {
                        aFile = Path.Combine(Path.GetDirectoryName(aFile), "ISH_" + aTime.ToString("yyyyMMddHH") + ".txt");
                        if (File.Exists(aFile))
                        {
                            break;
                        }
                        aTime = aTime.AddHours(-1);
                    }
                    if (File.Exists(aFile))
                    {
                        OpenISHFile(aFile, true);
                    }
                    break;
                default:
                    if (CB_Time.SelectedIndex > 0)
                    {
                        CB_Time.SelectedIndex = CB_Time.SelectedIndex - 1;
                    }
                    else
                    {
                        CB_Time.SelectedIndex = CB_Time.Items.Count - 1;
                    }
                    break;
            }           

            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = true;
            //Remove last layer
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);

            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = false;
            TSB_Draw.PerformClick();
        }

        private void TSB_NextTime_Click(object sender, EventArgs e)
        {
            _useSameLegendScheme = true;
            _useSameGridInterSet = true;
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_1:
                case MeteoDataType.MICAPS_2:
                case MeteoDataType.MICAPS_3:
                case MeteoDataType.MICAPS_4:
                case MeteoDataType.MICAPS_7:
                case MeteoDataType.MICAPS_11:
                case MeteoDataType.MICAPS_13:
                    DateTime aTime = _meteoDataInfo.GetTime().AddHours(3);
                    string aFile = _meteoDataInfo.GetFileName();
                    for (int i = 0; i < 100; i++)
                    {
                        aFile = Path.Combine(Path.GetDirectoryName(aFile), aTime.ToString("yyMMddHH") + ".000");
                        if (File.Exists(aFile))
                        {
                            break;
                        }
                        aTime = aTime.AddHours(3);
                    }
                    if (File.Exists(aFile))
                    {
                        _meteoDataInfo.OpenMICAPSData(aFile);
                        this.LB_DataFiles.Items[this.LB_DataFiles.SelectedIndex] = Path.GetFileName(_meteoDataInfo.GetFileName());
                    }
                    break;
                case MeteoDataType.ISH:
                    aTime = ((ISHDataInfo)_meteoDataInfo.DataInfo).dateTime.AddHours(1);
                    aFile = ((ISHDataInfo)_meteoDataInfo.DataInfo).FileName;
                    for (int i = 0; i < 100; i++)
                    {
                        aFile = Path.Combine(Path.GetDirectoryName(aFile), "ISH_" + aTime.ToString("yyyyMMddHH") + ".txt");
                        if (File.Exists(aFile))
                        {
                            break;
                        }
                        aTime = aTime.AddHours(1);
                    }
                    if (File.Exists(aFile))
                    {
                        OpenISHFile(aFile, true);
                    }
                    break;
                default:
                    if (CB_Time.SelectedIndex < CB_Time.Items.Count - 1)
                    {
                        CB_Time.SelectedIndex = CB_Time.SelectedIndex + 1;
                    }
                    else
                    {
                        CB_Time.SelectedIndex = 0;
                    }
                    break;
            }            

            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = true;
            //Remove last layer            
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);

            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = false;
            TSB_Draw.PerformClick();
        }

        private void TSB_Animitor_Click(object sender, EventArgs e)
        {
            //if (_animatorThread == null)
            //{
            //    _enableAnimation = true;
            //    _animatorThread = new Thread(new ThreadStart(Run_Animation));
            //    _animatorThread.Start();
            //}
            //else
            //{
            //    _animatorThread.Abort();
            //    _animatorThread = null;
            //}

            if (_inAnimation)
            {
                _enableAnimation = false;
                _inAnimation = false;
                Application.DoEvents();
            }
            else
            {
                _enableAnimation = true;
                _inAnimation = true;
                Run_Animation(false);
            }            
        }

        private void Run_Animation()
        {
            Run_Animation(false);
        }

        private void Run_Animation(bool ifCreateFile)
        {
            string aFile = "";
            List<string> fileList = new List<string>();
            Bitmap aBitmap = new Bitmap(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Width,
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Height, PixelFormat.Format32bppPArgb);
            if (ifCreateFile)
            {
                SaveFileDialog aDlg = new SaveFileDialog();
                aDlg.Filter = "Gif File (*.gif)|*.gif";
                if (aDlg.ShowDialog() == DialogResult.OK)
                {
                    aFile = aDlg.FileName;
                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.DrawToBitmap(aBitmap, new Rectangle(0, 0, 
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Width,
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Height));
                    Application.DoEvents();
                }
                else
                    return;
            }
            Graphics bmG = Graphics.FromImage(aBitmap);
            
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_7:
                    MICAPS7DataInfo aM7DataInfo = (MICAPS7DataInfo)_meteoDataInfo.DataInfo;
                    List<List<object>> trajPoints = aM7DataInfo.GetATrajData(CB_Time.SelectedIndex);
                    Graphics g = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.CreateGraphics();
                    PointF prePoint = new PointF();
                    for (int i = 0; i < trajPoints.Count; i++)
                    {
                        if (!_enableAnimation)
                            return;

                        List<object> pList = trajPoints[i];                        
                        PointBreak aPB = new PointBreak();
                        aPB.Style = PointStyle.Circle;
                        aPB.Color = Color.Red;
                        aPB.OutlineColor = Color.Black;
                        aPB.Size = 10;
                        aPB.DrawOutline = true;
                        aPB.DrawFill = true;
                        PointF aPoint = new PointF();
                        MeteoInfoC.PointD aPD = (MeteoInfoC.PointD)pList[0];
                        float X = 0;
                        float Y = 0;
                        //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjToScreen(aPD.X, aPD.Y, ref X, ref Y, 0);
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LonLatToScreen(aPD.X, aPD.Y, ref X, ref Y);
                        aPoint.X = X;
                        aPoint.Y = Y;

                        SolidBrush aBrush = new SolidBrush(Color.Blue);
                        Font wFont = new Font("Weather", 12);
                        //string text = ((char)(170)).ToString();
                        string text = ((char)(186)).ToString();
                        SizeF sf = g.MeasureString(text, wFont);
                        PointF sPoint = aPoint;
                        sPoint.X = aPoint.X - sf.Width / 2;
                        sPoint.Y = aPoint.Y - sf.Height / 2;
                        g.DrawString(text, wFont, aBrush, sPoint);
                        if (ifCreateFile)
                            bmG.DrawString(text, wFont, aBrush, sPoint);

                        if (i > 0)
                        {
                            Pen aPen = new Pen(Color.Red);
                            aPen.Width = 2;
                            g.DrawLine(aPen, prePoint, aPoint);
                            if (ifCreateFile)
                                bmG.DrawLine(aPen, prePoint, aPoint);
                        }

                        prePoint = aPoint;

                        if (ifCreateFile)
                        {
                            string pFile = aFile.Replace(".gif", i.ToString() + ".gif");
                            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExportToPicture(pFile);
                            aBitmap.Save(pFile, System.Drawing.Imaging.ImageFormat.Gif);
                            fileList.Add(pFile);
                        }
                        else
                            System.Threading.Thread.Sleep(500);
                    }
                    break;
                case MeteoDataType.HYSPLIT_Traj:
                    HYSPLITTrajectoryInfo aTrajDataInfo = (HYSPLITTrajectoryInfo)_meteoDataInfo.DataInfo;
                    trajPoints = aTrajDataInfo.GetATrajData(CB_Time.SelectedIndex);
                    g = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.CreateGraphics();
                    prePoint = new PointF();
                    for (int i = 0; i < trajPoints.Count; i++)
                    {
                        if (!_enableAnimation)
                            return;

                        List<object> pList = trajPoints[i];                        
                        PointBreak aPB = new PointBreak();
                        aPB.Style = PointStyle.Circle;
                        aPB.Color = Color.Red;
                        aPB.OutlineColor = Color.Black;
                        aPB.Size = 8;
                        aPB.DrawOutline = true;
                        aPB.DrawFill = true;
                        PointF aPoint = new PointF();
                        MeteoInfoC.PointD aPD = (MeteoInfoC.PointD)pList[0];
                        float X = 0;
                        float Y = 0;
                        //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjToScreen(aPD.X, aPD.Y, ref X, ref Y, 0);
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LonLatToScreen(aPD.X, aPD.Y, ref X, ref Y);
                        aPoint.X = X;
                        aPoint.Y = Y;
                        Draw.DrawPoint(aPB.Style, aPoint, aPB.Color,
                                aPB.OutlineColor, aPB.Size, aPB.DrawOutline,
                                aPB.DrawFill, g);
                        if (ifCreateFile)
                            Draw.DrawPoint(aPB.Style, aPoint, aPB.Color,
                                aPB.OutlineColor, aPB.Size, aPB.DrawOutline,
                                aPB.DrawFill, bmG);

                        if (i > 0)
                        {
                            Pen aPen = new Pen(Color.Red);
                            aPen.Width = 2;
                            g.DrawLine(aPen, prePoint, aPoint);
                            if (ifCreateFile)
                                bmG.DrawLine(aPen, prePoint, aPoint);
                        }

                        prePoint = aPoint;

                        if (ifCreateFile)
                        {
                            string pFile = aFile.Replace(".gif", i.ToString() + ".gif");
                            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExportToPicture(pFile);
                            aBitmap.Save(pFile, System.Drawing.Imaging.ImageFormat.Gif);
                            fileList.Add(pFile);
                        }
                        else
                            System.Threading.Thread.Sleep(500);
                    }
                    break;
                default:
                    if (CB_Time.Items.Count > 1)
                    {
                        _useSameLegendScheme = true;
                        _useSameGridInterSet = true;
                        for (int i = 0; i < CB_Time.Items.Count; i++)
                        {                            
                            if (!_enableAnimation)
                                return;

                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = true;
                            //Remove last layer
                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);

                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = false;
                            CB_Time.SelectedIndex = i;
                            TSB_Draw.PerformClick();

                            if (ifCreateFile)
                            {
                                string pFile = aFile.Replace(".gif", i.ToString() + ".gif");
                                if (frmMain.CurrentWin.tabControl1.SelectedIndex == 0)
                                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExportToPicture(pFile);
                                else
                                    frmMain.CurrentWin.MapDocument.MapLayout.ExportToPicture(pFile);
                                fileList.Add(pFile);
                            }
                            else
                                System.Threading.Thread.Sleep(500);
                        }
                    }
                    break;
            }

            _inAnimation = false;

            if (ifCreateFile)
            {
                MakeGifAnimation (aFile ,fileList );
            }
        }

        private void MakeGifAnimation(string aFile, List<string> fileList)
        {
            string aCommand, aBatchF;
            int i;

            aBatchF = Application.StartupPath + @"\Batch\GifAnimation.bat";
            StreamWriter sw = new StreamWriter(aBatchF);

            if (fileList.Count == 0)
            {
                MessageBox.Show("No figure!" + Environment.NewLine + aFile);
                return;
            }
            string runFile = Application.StartupPath + @"\Batch\gifsicle.exe";
            aCommand = "gifsicle.exe --delay 50 --loopcount=forever";
            for (i = 0; i < fileList.Count; i++)
                aCommand = aCommand + " " + fileList[i];

            aCommand = aCommand + " > " + aFile;
            sw.WriteLine(aCommand);
            sw.Flush();
            sw.Close();

            //Run batch file            
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = aBatchF;
            //proc.StartInfo.Arguments = aCommand;                   
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.StartInfo.WorkingDirectory = Application.StartupPath + "\\Batch";
            try
            {
                proc.Start();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                Console.WriteLine("Can not find the file \r{0}", e);
            }
            proc.WaitForExit();
            proc.Close();

            //Delete figures
            for (i = 0; i < fileList.Count; i++)
                File.Delete(fileList[i]);
        }

        private void frmMeteoData_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmMain.m_MDForm = null;            
        }

        private void frmMeteoData_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (!frmMain.CurrentWin.IsClosed)
            {
                this.Hide();
                e.Cancel = true;                
            }

            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                frmMain.CurrentWin.Options.ShowStartMeteoDataDlg = false;
            }
        }
                
        private void TSB_ClearDrawing_Click(object sender, EventArgs e)
        {
            //Remove last layer            
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
            TSB_Draw.Enabled = true;
        }               
        
        private void TSB_DataInfo_Click(object sender, EventArgs e)
        {            
            frmDataInfo aFrmDI = new frmDataInfo();
            aFrmDI.SetTextBox(_meteoDataInfo.InfoText);
            aFrmDI.Show(this);            
        }

        private void TSB_DrawSetting_Click(object sender, EventArgs e)
        {
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_13:
                //case MeteoDataType.AWX:
                    MapLayer aLayer = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(_lastAddedLayerHandle);
                    if (aLayer == null)
                        return;

                    OpenFileDialog aDlg = new OpenFileDialog();
                    aDlg.Filter = "Palette File (*.pal)|*.pal";
                    aDlg.InitialDirectory=Application .StartupPath +"\\pal";
                    if (aDlg.ShowDialog() == DialogResult.OK)
                    {
                        RasterLayer aILayer = (RasterLayer)aLayer;
                        aILayer.SetPalette(aDlg.FileName);
                        //DrawMeteoData.SetPalette(aDlg.FileName, aILayer.Image);
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
                    }
                    break;
                default:
                    MapLayer mLayer = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.GetLayerFromHandle(_lastAddedLayerHandle);
                    frmLegendSet aFrmLS = new frmLegendSet(true, mLayer, frmMain.CurrentWin.MapDocument);
                    aFrmLS.SetFrmMeteoData(this);
                    aFrmLS.SetLegendScheme(_legendScheme);
                    string fieldName = CB_Variable.Text;
                    if (aFrmLS.ShowDialog() == DialogResult.OK)
                    {
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
                        this._legendScheme = aFrmLS.GetLegendScheme();
                        DrawMeteoMap(false, _legendScheme, fieldName);
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
                    }
                    else
                    {
                        if (aFrmLS.GetIsApplied())
                        {
                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.RemoveLayerHandle(_lastAddedLayerHandle);
                            DrawMeteoMap(false, _legendScheme, fieldName);
                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
                        }
                    }

                    //frmLegendEdit aFrmLE = new frmLegendEdit(true, _lastAddedLayerHandle);
                    //aFrmLE.SetLegendScheme(_legendScheme);
                    //aFrmLE.ShowDialog();
                    break;
            }
        }

        private void TSB_Setting_Click(object sender, EventArgs e)
        {
            if (_meteoDataInfo.IsStationData)
            {
                switch (_2DDrawType)
                {
                    case DrawType2D.Contour:
                    case DrawType2D.Shaded:
                    case DrawType2D.Grid_Point:
                    case DrawType2D.Raster:
                        frmInterpolate frmInter = new frmInterpolate();
                        frmInter.SetParameters(_gridInterp);
                        if (frmInter.ShowDialog() == DialogResult.OK)
                        {
                            frmInter.GetParameters(ref _gridInterp);
                            ContourDraw.CreateGridXY(_gridInterp.GridDataSet, ref m_X,
                                ref m_Y);
                            TSB_Draw.Enabled = true;
                            _useSameGridInterSet = true;
                        }
                        break;
                    case DrawType2D.Weather_Symbol:
                        frmComboBox frmCB = new frmComboBox();
                        frmCB.Text = "Weather Symbol Set";
                        string[] symbols = new string[] { "All Weather", "SDS", "SDS, Haze", "Smoke", "Haze", "Mist", "Smoke, Haze, Mist", "Fog" };
                        frmCB.SetComboBox(symbols);
                        if (frmCB.ShowDialog() == DialogResult.OK)
                        {
                            m_MeteoDataDrawSet.WeatherType = frmCB.GetSelectedItem();
                            VectorLayer aLayer = new VectorLayer(ShapeTypes.Point);
                            string LName = CB_Variable.Text + "_" + CB_Level.Text + "_" + CB_Time.Text;
                            aLayer = DrawMeteoData.CreateWeatherSymbolLayer(_stationData,
                                    m_MeteoDataDrawSet.WeatherType, LName);
                            frmMain.CurrentWin.MapDocument.ActiveMapFrame.RemoveLayerByHandle(_lastAddedLayerHandle);
                            aLayer.ProjInfo = _meteoDataInfo.ProjInfo;
                            _lastAddedLayerHandle = frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer(aLayer);
                        }
                        break;
                    case DrawType2D.Vector:
                    case DrawType2D.Barb:
                    case DrawType2D.Streamline:
                        frmUVSet aFrmUVSet = new frmUVSet();
                        List<string> vList = new List<string>();
                        for (int i = 0; i < CB_Variable.Items.Count; i++)
                        {
                            vList.Add(CB_Variable.Items[i].ToString());
                        }
                        if (_meteoDataInfo.MeteoUVSet.IsUV)
                            aFrmUVSet.RB_U_V.Checked = true;
                        else
                            aFrmUVSet.RB_Dir_Speed.Checked = true;
                        aFrmUVSet.SetUVItems(vList);
                        aFrmUVSet.CB_U.Text = _meteoDataInfo.MeteoUVSet.UStr;
                        aFrmUVSet.CB_V.Text = _meteoDataInfo.MeteoUVSet.VStr;
                        aFrmUVSet.NUD_XSkip.Value = _meteoDataInfo.MeteoUVSet.SkipX;
                        aFrmUVSet.NUD_YSkip.Value = _meteoDataInfo.MeteoUVSet.SkipY;
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
                            MessageBox.Show("U/V (Direction/Speed) variables were not set!", "Error");
                        }                        
                        break;
                }
            }
            else if (_meteoDataInfo.IsGridData)
            {
                switch (_2DDrawType)
                {
                    case DrawType2D.Contour:
                    case DrawType2D.Grid_Fill:
                    case DrawType2D.Grid_Point:
                    case DrawType2D.Shaded:
                    case DrawType2D.Raster:
                        frmGridViewSet aFrmGVS = new frmGridViewSet();
                        aFrmGVS.SetParameters(m_IfInterpolateGrid);
                        if (aFrmGVS.ShowDialog() == DialogResult.OK)
                        {
                            m_IfInterpolateGrid = aFrmGVS.GetParameters();
                        }
                        break;
                    case DrawType2D.Streamline:
                        frmInputBox aFrmStrm = new frmInputBox("Streamline Density", "Setting", _StrmDensity.ToString());
                        if (aFrmStrm.ShowDialog() == DialogResult.OK)
                        {
                            int den = int.Parse(aFrmStrm.TB_Value.Text);
                            if (den < 1 || den > 10)
                                MessageBox.Show("The streamline density must be set between 1 - 10", "Error");
                            else
                                _StrmDensity = den;
                        }
                        break;
                    case DrawType2D.Vector:
                    case DrawType2D.Barb:
                        frmUVSet aFrmUVSet = new frmUVSet();
                        List<string> vList = new List<string>();
                        for (int i = 0; i < CB_Variable.Items.Count; i++)
                        {
                            vList.Add(CB_Variable.Items[i].ToString());
                        }
                        if (_meteoDataInfo.MeteoUVSet.IsUV)
                            aFrmUVSet.RB_U_V.Checked = true;
                        else
                            aFrmUVSet.RB_Dir_Speed.Checked = true;
                        aFrmUVSet.SetUVItems(vList);
                        aFrmUVSet.CB_U.Text = _meteoDataInfo.MeteoUVSet.UStr;
                        aFrmUVSet.CB_V.Text = _meteoDataInfo.MeteoUVSet.VStr;
                        aFrmUVSet.NUD_XSkip.Value = _meteoDataInfo.MeteoUVSet.SkipX;
                        aFrmUVSet.NUD_YSkip.Value = _meteoDataInfo.MeteoUVSet.SkipY;
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
                            MessageBox.Show("U/V (Direction/Speed) variables were not set!", "Error");
                        }
                        break;
                }  
            }            
        }

        private void TSMI_GrADSData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            aDlg.Filter = "GrADS Data (*.ctl)|*.ctl";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenGrADSData(aFile);

                //foreach (ToolStripItem aItem in toolStrip2.Items)
                //{
                //    aItem.Enabled = true;
                //}
                //TSB_DrawSetting.Enabled = false;
                //TSB_Animitor.Enabled = false;
                //TSB_PreTime.Enabled = false;
                //TSB_NextTime.Enabled = false;
                //GB_DataSet.Enabled = true;

                //if (aDataInfo.DataType == MeteoDataType.GrADS_Grid)
                //{
                //    TSB_Setting.Enabled = true;

                //    //Projection
                //    UpdateProjection();
                //    //if (aDataInfo.ProjInfo.Transform.Proj4Name != "lonlat")
                //    //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);

                //    //UpdateParasGrADSGrid();
                //}
                //else
                //{
                //    aDataInfo.DataType = MeteoDataType.GrADS_Station;
                //    if (((GrADSDataInfo)aDataInfo.DataInfo).UpperVariables.Count == 0)
                //    {
                //        TSB_Setting.Visible = true;
                //        TSB_Setting.Enabled = false;
                //        TSB_SectionPlot.Enabled = false;
                //        TSB_1DPlot.Enabled = false;
                //    }
                //    _gridInterp.UnDefData = aDataInfo.MissingValue;
                //    _useSameGridInterSet = false;
                //    //UpdateParasGrADSStation();
                //}

                AddMeteoData(aDataInfo);
            }  
        }

        private void TSMI_MICAPSData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            aDlg.Filter = "MICAPS Data (*.*)|*.*";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                aFile = aDlg.FileName;

                MeteoDataInfo aDataInfo = OpenMICAPSFile(aFile, false);
                if (aDataInfo != null)
                {                    
                    AddMeteoData(aDataInfo);
                }
            }
        }

        private MeteoDataInfo OpenMICAPSFile(string aFile, bool OnlyUpdateTime)
        {
            MeteoDataInfo aDataInfo = new MeteoDataInfo();
            aDataInfo.OpenMICAPSData(aFile);
            switch (aDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_1:
                    _gridInterp.UnDefData = aDataInfo.MissingValue;                    

                    //UpdateParasMICAPS1(OnlyUpdateTime);
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Visible = true;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;
                    break;
                case MeteoDataType.MICAPS_2:
                    _gridInterp.UnDefData = aDataInfo.MissingValue;
                    //UpdateParasMICAPS2(OnlyUpdateTime);
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Visible = true;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;
                    break;
                case MeteoDataType.MICAPS_3:
                    _gridInterp.UnDefData = aDataInfo.MissingValue;

                    //UpdateParasMICAPS3(OnlyUpdateTime);
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Visible = true;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;
                    break;
                case MeteoDataType.MICAPS_4:                    
                    //UpdateParasMICAPS4();
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Enabled = true;
                    TSB_ClearDrawing.Enabled = true;                    
                    break;
                case MeteoDataType.MICAPS_7:
                    //UpdateParasMICAPS7();
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Animitor.Enabled = true;
                    TSB_DrawSetting.Enabled = false;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;
                    TSB_SectionPlot.Enabled = false;
                    TSB_1DPlot.Enabled = false;                
                    break;
                case MeteoDataType.MICAPS_11:
                    //UpdateParasMICAPS11();
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;                    
                    break;
                case MeteoDataType.MICAPS_13:
                    //Projection
                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(aDataInfo.ProjInfo);                    

                    //UpdateParasMICAPS13();
                    this.splitContainer1.Panel2.Enabled = true;
                    TSB_Setting.Enabled = false;
                    TSB_ClearDrawing.Enabled = true;
                    break;
                default:
                    MessageBox.Show("The data were not supported at present! " + aDataInfo.DataType.ToString(),
                            "Error");
                    aDataInfo = null;
                    break;
            }

            return aDataInfo;
        }

        private void UpdateMICAPSFile(string aFile)
        {
            _meteoDataInfo.OpenMICAPSData(aFile);
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_1:
                    UpdateParasMICAPS1(true);
                    break;
                case MeteoDataType.MICAPS_2:
                    UpdateParasMICAPS2(true);
                    break;
                case MeteoDataType.MICAPS_3:
                    UpdateParasMICAPS3(true);
                    break;
                case MeteoDataType.MICAPS_4:
                    UpdateParasMICAPS4();
                    break;
                case MeteoDataType.MICAPS_7:
                    UpdateParasMICAPS7();
                    break;
                case MeteoDataType.MICAPS_11:
                    UpdateParasMICAPS11();
                    break;
                case MeteoDataType.MICAPS_13:
                    UpdateParasMICAPS13();
                    break;
            }

            this.LB_DataFiles.Items[this.LB_DataFiles.SelectedIndex] = Path.GetFileName(_meteoDataInfo.GetFileName());
            //this.LB_DataFiles.SelectedValue = Path.GetFileName(_meteoDataInfo.GetFileName());
        }

        private void TSMI_ARLData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenARLData(aFile);                

                AddMeteoData(aDataInfo);

                this.Cursor = Cursors.Default;
            }
        }

        private void TSMI_NetCDFData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;
            aDlg.Filter = "NetCDF Data (*.nc; *.ncf)|*.nc;*.ncf|NetCDF Data (*.*)|*.*";

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;

                string errorStr = "Can not open data file: " + aFile;                
                if (aDataInfo.OpenNCData(aFile))
                {
                    if (((NetCDFDataInfo)aDataInfo.DataInfo).X != null &&
                        ((NetCDFDataInfo)aDataInfo.DataInfo).Y != null)
                    {                                               
                        AddMeteoData(aDataInfo);
                    }
                    else
                    {
                        MessageBox.Show("No X/Y Coordinate!", "Error");
                        TSB_DataInfo.Enabled = true;
                        //Set form text
                        this.Text = this.Text.Split('-')[0].Trim() + " - " + aDataInfo.DataType.ToString();
                        frmDataInfo aFrmDI = new frmDataInfo();
                        aFrmDI.SetTextBox(aDataInfo.InfoText);
                        aFrmDI.Show(this);
                    }
                }
                else
                {
                    MessageBox.Show(errorStr, "Error");
                }    
                

                this.Cursor = Cursors.Default;
            }
        }

        private void CB_Variable_SelectedIndexChanged(object sender, EventArgs e)
        {
            _useSameLegendScheme = false;
            _meteoDataInfo.VariableIndex = CB_Variable.SelectedIndex;
            //_useSameGridInterSet = false;
            DataInfo aDataInfo = _meteoDataInfo.DataInfo;
            Variable var = aDataInfo.GetVariable(this.CB_Variable.Text);
            int i;

            //Set times
            this.CB_Time.Items.Clear();
            if (var.TDimension != null)
            {
                List<DateTime> times = var.GetTimes();
                for (i = 0; i < times.Count; i++)
                    this.CB_Time.Items.Add(times[i].ToString("yyyy-MM-dd HH:mm"));
                if (this.CB_Time.Items.Count > _meteoDataInfo.TimeIndex)
                    this.CB_Time.SelectedIndex = _meteoDataInfo.TimeIndex;
            }

            //Set levels
            this.CB_Level.Items.Clear();
            if (var.ZDimension == null)
            {
                this.CB_Level.Items.Add("Surface");
                this.CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < var.ZDimension.DimLength; i++)
                    this.CB_Level.Items.Add(var.ZDimension.DimValue[i].ToString());
                if (this.CB_Level.Items.Count > _meteoDataInfo.LevelIndex)
                    this.CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }                              

        private void SetGRIB2Var()
        {
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;
            int aIdx = CB_Variable.SelectedIndex;
            int i;
            int levelIdx = CB_Level.SelectedIndex;

            CB_Level.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level.Items.Add(aDataInfo.Variables[aIdx].Levels[i].ToString());
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }

        private void SetNetCDFVar()
        {
            NetCDFDataInfo aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;
            int aIdx = CB_Variable.SelectedIndex;
            int i;
            int levelIdx = CB_Level.SelectedIndex;

            string varName = CB_Variable.Text;
            aIdx = aDataInfo.VariableNames.IndexOf(varName);
            CB_Level.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level.Items.Add("Surface");
                CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level.Items.Add(aDataInfo.Variables[aIdx].Levels[i].ToString());
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }
        }

        private void SetHDFVar()
        {
            HDF5DataInfo aDataInfo = (HDF5DataInfo)_meteoDataInfo.DataInfo;
            int aIdx = CB_Variable.SelectedIndex;
            int i;
            int levelIdx = CB_Level.SelectedIndex;

            string varName = CB_Variable.Text;
            aIdx = aDataInfo.VariableNames.IndexOf(varName);
            CB_Level.Items.Clear();
            if (aDataInfo.Variables[aIdx].LevelNum == 0)
            {
                CB_Level.Items.Add("Undef");
                CB_Level.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < aDataInfo.Variables[aIdx].LevelNum; i++)
                {
                    CB_Level.Items.Add(aDataInfo.Variables[aIdx].Levels[i].ToString());
                }
                CB_Level.SelectedIndex = _meteoDataInfo.LevelIndex;
            }

            //Set draw type
            aDataInfo.CurrentVariable = aDataInfo.Variables[aIdx];
            if (aDataInfo.Variables[aIdx].IsSwath)
            {
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Station_Point.ToString());
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
            }
            else
            {               
                CB_DrawType.Items.Clear();
                CB_DrawType.Items.Add(DrawType2D.Raster.ToString());
                CB_DrawType.Items.Add(DrawType2D.Contour.ToString());
                CB_DrawType.Items.Add(DrawType2D.Shaded.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Fill.ToString());
                CB_DrawType.Items.Add(DrawType2D.Grid_Point.ToString());
                //CB_DrawType.Items.Add(DrawType2D.Vector.ToString());
                //CB_DrawType.Items.Add(DrawType2D.Barb.ToString());            
                //CB_DrawType.Items.Add(DrawType2D.Streamline.ToString());
            }
            CB_DrawType.SelectedIndex = 0;
        }

        private void CB_DrawType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _2DDrawType = (DrawType2D)Enum.Parse(typeof(DrawType2D), CB_DrawType.Text, true);
            TSB_Draw.Enabled = true;
            _useSameLegendScheme = false;

            //_useSameGridInterSet = false;

            TSB_Animitor.Enabled = false;
            TSB_CreateAnimatorFile.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;

            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.MICAPS_7:
                case MeteoDataType.HYSPLIT_Traj:
                    TSB_Animitor.Enabled = true;
                    TSB_CreateAnimatorFile.Enabled = true;
                    break;
            }

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

        private void CB_Level_SelectedIndexChanged(object sender, EventArgs e)
        {
            TSB_Draw.Enabled = true;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
            _useSameLegendScheme = false;
            _meteoDataInfo.LevelIndex = CB_Level.SelectedIndex;
        }

        private void CB_Time_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meteoDataInfo.TimeIndex = CB_Time.SelectedIndex;
            TSB_Draw.Enabled = true;
        }
                
        private void TSB_ProfilePlot_Click(object sender, EventArgs e)
        {
            frmSectionPlot aFrmPP = new frmSectionPlot(_meteoDataInfo);            
            aFrmPP.Show();
        }

        private void TSB_1DPlot_Click(object sender, EventArgs e)
        {
            frmOneDim aFrmOD = new frmOneDim();
            aFrmOD.SetMeteoDataInfo(_meteoDataInfo);
            aFrmOD.Show();
        }

        private void CHB_ColorVar_CheckedChanged(object sender, EventArgs e)
        {
            TSB_Draw.Enabled = true;
            _useSameLegendScheme = false;

            //_useSameGridInterSet = false;

            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
        }

        private void TB_Multiplier_TextChanged(object sender, EventArgs e)
        {
            _useSameLegendScheme = false;
        }

        private void TSMI_HYSPLITConc_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenHYSPLITConc(aFile);                                
                AddMeteoData(aDataInfo);
            }  
        }

        private void TSMI_HYSPLITParticle_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenHYSPLITParticle(aFile);

                _gridInterp.UnDefData = aDataInfo.MissingValue;                
                AddMeteoData(aDataInfo);
            }  
        }

        private void TSMI_HYSPLITTraj_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Multiselect = true;            

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenHYSPLITTraj(aDlg.FileNames);                                
                AddMeteoData(aDataInfo);
            }  
        }        

        private void TSMI_LonLatStations_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            aDlg.Filter = "CSV File (*.csv)|*.csv|Text File (*.txt)|*.txt";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                aFile = aDlg.FileName;
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenLonLatData(aFile);
                if (!frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
                {
                    if (MessageBox.Show("If project station data?", "Alarm", MessageBoxButtons.YesNo) == DialogResult.No)
                        aDataInfo.ProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
                }
                
                AddMeteoData(aDataInfo);
            }
        }

        private void TSMI_ISHData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            aDlg.Filter = "ISH Data (*.*)|*.*";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                aFile = aDlg.FileName;
                MeteoDataInfo aDataInfo = OpenISHFile(aFile, false);                
                AddMeteoData(aDataInfo);
            }
        }

        private MeteoDataInfo OpenISHFile(string aFile, bool OnlyUpdateTime)
        {
            MeteoDataInfo aDataInfo = new MeteoDataInfo();
            aDataInfo.OpenISHData(aFile);

            _gridInterp.UnDefData = aDataInfo.MissingValue;            

            UpdateParasISH(OnlyUpdateTime);

            return aDataInfo;
        }

        private void TSMI_METARData_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            aDlg.Filter = "METAR Data (*.*)|*.*";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                aFile = aDlg.FileName;
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenMETARData(aFile);                
                AddMeteoData(aDataInfo);
            }
        }

        private void TSB_CreateAnimatorFile_Click(object sender, EventArgs e)
        {
            Run_Animation(true);
        }

        private void TSMI_ASCIIGrid_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenASCIIGridData(aFile);                

                AddMeteoData(aDataInfo);
            }
        }

        private void TSMI_SuferGrid_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenSuferGridData(aFile);
                aDataInfo.ProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;                                
                AddMeteoData(aDataInfo);
            }
        }

        private void TSB_ViewData_Click(object sender, EventArgs e)
        {
            if (_meteoDataInfo.IsGridData)
                ViewGridData();
            else if (_meteoDataInfo.IsStationData)
            {
                ViewStationData();
                if (_gridData.X != null && _gridData.Y != null)
                {
                    if (_gridData.XNum > 0 && _gridData.YNum > 0)
                        ViewGridData();
                }
            }            
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
            aForm.ProjInfo = _meteoDataInfo.ProjInfo;
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

        private void ViewStationData()
        {
            if (_stationData.Data == null)
                return;

            if (_stationData.Data.GetLength(0) == 0 || _stationData.Data.GetLength(1) == 0)
                return;

            double min = _stationData.GetMinValue();
            int dNum = MIMath.GetDecimalNum(min);
            string dFormat = "f" + dNum.ToString();

            frmDataGridViewData aForm = new frmDataGridViewData();
            aForm.MissingValue = _stationData.MissingValue;
            aForm.DataType = "StationData";
            aForm.Data = _stationData;
            aForm.TSB_ToStation.Visible = false;
            int i, j, colNum, rowNum;

            colNum = _stationData.Data.GetLength(0) + 1;
            rowNum = _stationData.Data.GetLength(1);
            aForm.dataGridView1.ColumnCount = colNum;
            aForm.dataGridView1.RowCount = rowNum;
            string[] colNames = new string[] { "Stid", "Longitude", "Latitude", CB_Variable.Text };
            for (i = 0; i < colNum; i++)
            {
                aForm.dataGridView1.Columns[i].Name = colNames[i];
                aForm.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (j = 0; j < rowNum; j++)
                {
                    if (i == 0)
                        aForm.dataGridView1[i, j].Value = _stationData.Stations[j];
                    else if (i == 1 || i == 2)
                        aForm.dataGridView1[i, j].Value = _stationData.Data[i - 1, j].ToString("0.00");
                    else
                        aForm.dataGridView1[i, j].Value = _stationData.Data[i - 1, j].ToString(dFormat);
                }

                aForm.dataGridView1.Columns[i].Frozen = false;
            }            
            aForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            aForm.dataGridView1.MultiSelect = true;
            aForm.dataGridView1.AllowUserToOrderColumns = true;

            aForm.Text = "Station Data";
            aForm.Show(this);
        }

        private void TSMI_GRIBData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;
                aDataInfo.OpenGRIBData(aFile);
                if (aDataInfo.DataInfo != null)
                    AddMeteoData(aDataInfo);              

                this.Cursor = Cursors.Default;
            }
        }

        private void TSMI_HDFData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "HDF5 EOS Data (*.he5)|*.he5";
            string aFile;

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                //this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aFile = aDlg.FileName;

                if (aDataInfo.OpenHDFData(aFile))
                {                    
                    AddMeteoData(aDataInfo);
                }
                else
                {
                    MessageBox.Show("Open file error of there is no grid data!");
                }
            }
        }

        private void TSMI_AWXData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "AWX Data (*.AWX)|*.AWX";

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenAWXData(aDlg.FileName);                                

                AddMeteoData(aDataInfo);
            }
        }

        private void TSMI_SYNOPData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                string stFile = Application.StartupPath + "\\Station\\SYNOP_Stations.csv";
                aDataInfo.OpenSYNOPData(aDlg.FileName, stFile);                                
                AddMeteoData(aDataInfo);
            }
        }

        private void TSMI_GeoTiffData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "GeoTiff Data (*.tif)|*.tif";

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenGeoTiffData(aDlg.FileName);
                AddMeteoData(aDataInfo);
            }
        }

        private void TSMI_HRITData_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();

            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                MeteoDataInfo aDataInfo = new MeteoDataInfo();
                aDataInfo.OpenHRITData(aDlg.FileName);                
                AddMeteoData(aDataInfo);
            }
        }        

        private void LB_DataFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dataInfoList.Count == 0)
                return;

            if (LB_DataFiles.SelectedIndex < 0)
                return;

            _meteoDataInfo = _dataInfoList[LB_DataFiles.SelectedIndex];
            //UpdateForm();
            updateParameters();
        }

        private void OnRemoveDataClick(object sender, EventArgs e)
        {
            int selIdx = LB_DataFiles.SelectedIndex;
            RemoveMeteoData(selIdx);            
        }

        private void LB_DataFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = LB_DataFiles.IndexFromPoint(new Point(e.X, e.Y));

                if (posindex >= 0 && posindex < LB_DataFiles.Items.Count)
                {

                    LB_DataFiles.SelectedIndex = posindex;
                    ContextMenuStrip mnuLayer = new ContextMenuStrip();
                    mnuLayer.Items.Add("Remove Data File");
                    mnuLayer.Items[mnuLayer.Items.Count - 1].Click += new EventHandler(OnRemoveDataClick);

                    Point aPoint = new Point(0, 0);
                    aPoint.X = e.X;
                    aPoint.Y = e.Y;
                    mnuLayer.Show(this, aPoint);
                }
            }
        }        

        private void LB_DataFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveMeteoData(LB_DataFiles.SelectedIndex);
        }

        private void B_RemoveAllData_Click(object sender, EventArgs e)
        {
            RemoveAllMeteoData();
        }

        private void frmMeteoData_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Focused)
                this.Focus();
        }

        private void frmMeteoData_MouseLeave(object sender, EventArgs e)
        {
            if (this.Focused)
                this.Parent.Focus();
        }

        #endregion                
                       
    }
}
