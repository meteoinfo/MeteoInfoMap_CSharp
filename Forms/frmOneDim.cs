using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using MeteoInfo.Classes;
using MeteoInfoC;
using MeteoInfoC.Data.MeteoData;
using MeteoInfoC.Global;
using MeteoInfoC.Data;

namespace MeteoInfo.Forms
{
    public partial class frmOneDim : Form
    {
        MeteoDataInfo _meteoDataInfo = new MeteoDataInfo();
        PlotDimension _plotDimension = new PlotDimension();
        ZedGraphType _ZedGraphType = new ZedGraphType();

        List<MeteoInfoC.PointD> _pointList = new List<MeteoInfoC.PointD>();

        enum ZedGraphType
        {
            Line,
            Bar
        }

        public frmOneDim()
        {
            InitializeComponent();

            TB_NewVariable.Visible = false;
        }

        private void frmOneDim_Load(object sender, EventArgs e)
        {
            //Set label position            
            Lab_PlotDims.Left = CB_PlotDims.Left - Lab_PlotDims.Width;
            Lab_Var.Left = CB_Vars.Left - Lab_Var.Width;
            Lab_DrawType.Left = CB_DrawType.Left - Lab_DrawType.Width;

            //Set dimensions
            CB_Lat2.Visible = false;
            CB_Level2.Visible = false;
            CB_Lon2.Visible = false;
            CB_Time2.Visible = false;
            CHB_Time.Enabled = false;
            CHB_Level.Enabled = false;
            CHB_Lon.Enabled = false;
            CHB_Lat.Enabled = false;

            UpdateDimensions();

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add(ZedGraphType.Line.ToString());
            CB_DrawType.Items.Add(ZedGraphType.Bar.ToString());           
            CB_DrawType.SelectedIndex = 0;   
            
        }

        public void SetMeteoDataInfo(MeteoDataInfo aMDataInfo)
        {
            _meteoDataInfo = aMDataInfo;
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
                case MeteoDataType.MICAPS_1:
                    UpdateParasMICAPS1();
                    break;
                case MeteoDataType.MICAPS_4:
                    UpdateParasMICAPS4();
                    break;
                case MeteoDataType.GRIB1:
                    UpdateParasGRIB1Grid();
                    break;
                case MeteoDataType.GRIB2:
                    UpdateParasGRIB2Grid();
                    break;
            }
        }

        private void UpdateParasGrADSGrid()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;            

            //Set times
            string timeFormat = "yyyy-MM-dd HH:mm";
            if (aDataInfo.TDEF.times.Length > 1)
            {
                if ((aDataInfo.TDEF.times[1] - aDataInfo.TDEF.times[0]).Duration().Days >= 1)
                {
                    timeFormat = "yyyy-MM-dd";
                }
                else if ((aDataInfo.TDEF.times[1] - aDataInfo.TDEF.times[0]).Duration().Hours >= 1)
                {
                    timeFormat = "yyyy-MM-dd HH";
                }
                else
                {
                    timeFormat = "yyyy-MM-dd HH:mm";
                }
            }
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.TDEF.TNum; i++)
            {
                CB_Time1.Items.Add(aDataInfo.TDEF.times[i].ToString(timeFormat));
            }
            CB_Time1.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            for (i = 0; i < aDataInfo.XDEF.XNum; i++)
            {
                CB_Lon1.Items.Add(aDataInfo.XDEF.X[i].ToString());
            }
            CB_Lon1.SelectedIndex = 0;

            CB_Lat1.Items.Clear();
            for (i = 0; i < aDataInfo.YDEF.YNum; i++)
            {
                CB_Lat1.Items.Add(aDataInfo.YDEF.Y[i].ToString());
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

        private void UpdateParasGRIB1Grid()
        {
            int i;
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;

            //Set times
            string timeFormat = "yyyy-MM-dd HH:mm";
            if (aDataInfo.Times.Count > 1)
            {
                if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Days >= 1)
                {
                    timeFormat = "yyyy-MM-dd";
                }
                else if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Hours >= 1)
                {
                    timeFormat = "yyyy-MM-dd HH";
                }
                else
                {
                    timeFormat = "yyyy-MM-dd HH:mm";
                }
            }
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString(timeFormat));
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

        private void UpdateParasGRIB2Grid()
        {
            int i;
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;

            //Set times
            string timeFormat = "yyyy-MM-dd HH:mm";
            if (aDataInfo.TimeNum > 1)
            {
                if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Days >= 1)
                {
                    timeFormat = "yyyy-MM-dd";
                }
                else if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Hours >= 1)
                {
                    timeFormat = "yyyy-MM-dd HH";
                }
                else
                {
                    timeFormat = "yyyy-MM-dd HH:mm";
                }
            }
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString(timeFormat));
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

        private void UpdateParasGrADSStation()
        {
            int i;
            GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;

            //Set draw type
            CB_DrawType.Items.Clear();
            CB_DrawType.Items.Add("Station Point");
            CB_DrawType.Items.Add("Contour");
            CB_DrawType.Items.Add("Shaded");

            ArrayList varList = new ArrayList();
            for (i = 0; i < aDataInfo.VARDEF.VNum; i++)
            {
                varList.Add(aDataInfo.VARDEF.Vars[i].Name.ToUpper());
            }
            if (varList.Contains("U") && varList.Contains("V"))
            {
                CB_DrawType.Items.Add("Barb");
            }
            CB_DrawType.SelectedIndex = 0;

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
            for (i = 0; i < aDataInfo.VARDEF.VNum; i++)
            {
                CB_Vars.Items.Add(aDataInfo.VARDEF.Vars[i].Name);
            }
            CB_Vars.SelectedIndex = 0;

            //Set lon/lat
            CB_Lon1.Items.Clear();
            CB_Lat1.Items.Clear();

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
            string timeFormat = "yyyy-MM-dd HH";
            if (aDataInfo.Times.Count > 1)
            {
                if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Days >= 1)
                {
                    timeFormat = "yyyy-MM-dd";
                }
                else if ((aDataInfo.Times[1] - aDataInfo.Times[0]).Duration().Hours >= 1)
                {
                    timeFormat = "yyyy-MM-dd HH";
                }
                else
                {
                    timeFormat = "yyyy-MM-dd HH:mm";
                }
            }
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.Times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.Times[i].ToString(timeFormat));
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
            string timeFormat = "yyyy-MM-dd HH";
            if (aDataInfo.times.Count > 1)
            {
                if ((aDataInfo.times[1] - aDataInfo.times[0]).Duration().Days >= 1)
                {
                    timeFormat = "yyyy-MM-dd";
                }
                else if ((aDataInfo.times[1] - aDataInfo.times[0]).Duration().Hours >= 1)
                {
                    timeFormat = "yyyy-MM-dd HH";
                }
                else
                {
                    timeFormat = "yyyy-MM-dd HH:mm";
                }
            }
            CB_Time1.Items.Clear();
            for (i = 0; i < aDataInfo.times.Count; i++)
            {
                CB_Time1.Items.Add(aDataInfo.times[i].ToString(timeFormat));
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
                CB_Vars.Items.Add(aDataInfo.Variables[i].Name);

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
            switch (_meteoDataInfo.DataType)
            {
                case MeteoDataType.GrADS_Grid:
                    SetGrADSVar();                    
                    break;
                case MeteoDataType.ARL_Grid:
                    SetARLVar();                    
                    break;
                case MeteoDataType.GrADS_Station:
                    CB_Level1.Items.Clear();
                    CB_Level1.Items.Add("Surface");
                    CB_Level1.SelectedIndex = 0;                    
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
            if (CB_Level1.Items.Count > 1)
            {
                CB_PlotDims.Items.Add(PlotDimension.Level.ToString());                
            }
            if (CB_Time1.Items.Count > 1)
            {
                CB_PlotDims.Items.Add(PlotDimension.Time.ToString());                
            }
            if (CB_Lon1.Items.Count > 1)
            {
                CB_PlotDims.Items.Add(PlotDimension.Lon.ToString());
            }
            if (CB_Lat1.Items.Count > 1)
            {
                CB_PlotDims.Items.Add(PlotDimension.Lat.ToString());
            }
            
            if (pdStr != "" && CB_PlotDims.Items.Contains(pdStr))
            {
                CB_PlotDims.SelectedItem = pdStr;
            }
            else
            {
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

            CB_Level1.Items.Clear();
            if (((GrADSDataInfo)_meteoDataInfo.DataInfo).VARDEF.Vars[aIdx].LevelNum == 0)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < ((GrADSDataInfo)_meteoDataInfo.DataInfo).VARDEF.Vars[aIdx].LevelNum; i++)
                {
                    CB_Level1.Items.Add(Convert.ToString(((GrADSDataInfo)_meteoDataInfo.DataInfo).ZDEF.ZLevels[i]));
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
            Variable var = aDataInfo.GetVariable(varName);
            CB_Level1.Items.Clear();
            if (var.ZDimension == null)
            {
                CB_Level1.Items.Add("Surface");
                CB_Level1.SelectedIndex = 0;
            }
            else
            {
                for (i = 0; i < var.ZDimension.DimLength; i++)
                {
                    CB_Level1.Items.Add(var.ZDimension.DimValue[i].ToString());
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

        private void CB_PlotDims_SelectedValueChanged(object sender, EventArgs e)
        {
            _plotDimension = (PlotDimension)Enum.Parse(typeof(PlotDimension),
                CB_PlotDims.Text, true);
            _meteoDataInfo.DimensionSet = _plotDimension;
            SetDimensions();

            if (zedGraphControl1.GraphPane.CurveList.Count > 0)
            {
                zedGraphControl1.GraphPane.CurveList.Clear();
            }

            TSB_Draw.Enabled = true;
            TSB_Animitor.Enabled = false;
            TSB_PreTime.Enabled = false;
            TSB_NextTime.Enabled = false;
        }

        private void SetDimensions()
        {
            switch (_plotDimension)
            {
                case PlotDimension.Time:
                    CHB_Lat.Checked = false;
                    CHB_Level.Checked = false;
                    CHB_Time.Checked = true;
                    CHB_Lon.Checked = false;
                    break;
                case PlotDimension.Level:
                    CHB_Level.Checked = true;
                    CHB_Lon.Checked = false;
                    CHB_Time.Checked = false;
                    CHB_Lat.Checked = false;
                    break;
                case PlotDimension.Lon:
                    CHB_Lon.Checked = true;
                    CHB_Lat.Checked = false;
                    CHB_Time.Checked = false;
                    CHB_Level.Checked = false;
                    break;
                case PlotDimension.Lat:
                    CHB_Lat.Checked = true;
                    CHB_Time.Checked = false;
                    CHB_Level.Checked = false;
                    CHB_Lon.Checked = false;
                    break;                
            }
        }

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

        private void TSB_DataInfo_Click(object sender, EventArgs e)
        {
            frmDataInfo aFrmDI = new frmDataInfo();
            aFrmDI.SetTextBox(_meteoDataInfo.InfoText);
            aFrmDI.Show(this);
        }

        private void CreateZedGraph(ZedGraphControl zgc, List<MeteoInfoC.PointD> pointList)
        {
            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            string varStr = CB_Vars.Text;
            if (CHB_NewVariable.Checked)
            {
                varStr = TB_NewVariable.Text;
            }
            myPane.Title.Text = CB_Vars.Text + "_" + CB_PlotDims.Text + " Graph";
            myPane.XAxis.Title.Text = CB_PlotDims.Text;
            myPane.YAxis.Title.Text = CB_Vars.Text;

            // Make up some data points
            PointPairList list = new PointPairList();            
            int i;
            string[] labels = new string[pointList.Count];
            switch (_plotDimension)
            {
                case PlotDimension.Time:
                    for (i = 0; i < pointList.Count; i++)
                    {
                        list.Add(DataConvert.ToOADate(pointList[i].X), pointList[i].Y);
                    }
                    break;
                default:
                    for (i = 0; i < pointList.Count; i++)
                    {
                        list.Add(pointList[i].X, pointList[i].Y);
                    }
                    break;
            }            

            
            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
            string layerName = GetLayerName();
            Color aColor = GetGraphColor();
            SymbolType aST = GetGraphSymbolType();
            switch (_ZedGraphType)
            {
                case ZedGraphType.Line:
                    LineItem myCurve = myPane.AddCurve(layerName, list, aColor, aST);

                    // Fill the area under the curve with a white-red gradient at 45 degrees
                    //myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);

                    // Make the symbols opaque by filling them with white
                    myCurve.Symbol.Fill = new Fill(Color.White);
                    break;
                case ZedGraphType.Bar:
                    BarItem myBar = myPane.AddBar(layerName, list, aColor);
                    if (_plotDimension == PlotDimension.Level)
                    {
                        // Set BarBase to the YAxis for horizontal bars
                        myPane.BarSettings.Base = BarBase.Y;
                    }
                    break;
            }            
                                   
            // Set the XAxis to date type   
            switch (_plotDimension)
            {
                case PlotDimension.Time:
                    labels = GetTimeGridStr(pointList);
                    myPane.XAxis.Type = AxisType.Date;
                    myPane.YAxis.Scale.IsReverse = false;
                    myPane.XAxis.Scale.TextLabels = labels;
                    string formatStr = GetTimeFormatStr(DataConvert.ToDateTime(pointList[1].X), DataConvert.ToDateTime(pointList[0].X));
                    myPane.XAxis.Scale.Format = formatStr;
                    break;
                case PlotDimension.Level:
                    myPane.XAxis.Type = AxisType.Linear;
                    myPane.XAxis.Scale.FormatAuto = true;
                    myPane.YAxis.Scale.IsReverse = true;
                    myPane.YAxis.Title.Text = CB_PlotDims.Text;
                    myPane.XAxis.Title.Text = CB_Vars.Text;                    
                    break;
                case PlotDimension.Lon:
                    labels = GetLonGridStr(pointList);
                    myPane.XAxis.Type = AxisType.Text;
                    myPane.YAxis.Scale.IsReverse = false;
                    myPane.XAxis.Scale.TextLabels = labels;
                    break;
                case PlotDimension.Lat:
                    labels = GetLatGridStr(pointList);
                    myPane.XAxis.Type = AxisType.Text;
                    myPane.YAxis.Scale.IsReverse = false;
                    myPane.XAxis.Scale.TextLabels = labels;
                    break;
            }            

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private Color GetGraphColor()
        {
            Color aColor;
            List<Color> colorList = new List<Color>();

            //colorList.Add(Color.FromArgb(160, 0, 200));
            //colorList.Add(Color.FromArgb(110, 0, 220));
            //colorList.Add(Color.FromArgb(30, 60, 255));
            //colorList.Add(Color.FromArgb(0, 160, 255));
            //colorList.Add(Color.FromArgb(0, 200, 200));
            //colorList.Add(Color.FromArgb(0, 210, 140));
            //colorList.Add(Color.FromArgb(0, 220, 0));
            //colorList.Add(Color.FromArgb(160, 230, 50));
            //colorList.Add(Color.FromArgb(230, 220, 50));
            //colorList.Add(Color.FromArgb(230, 175, 45));
            //colorList.Add(Color.FromArgb(240, 130, 40));
            //colorList.Add(Color.FromArgb(250, 60, 60));
            //colorList.Add(Color.FromArgb(240, 0, 130));
            colorList.Add(Color.Blue);
            colorList.Add(Color.Red);
            colorList.Add(Color.Green);
            colorList.Add(Color.Black);
            colorList.Add(Color.Purple);
            colorList.Add(Color.GreenYellow);
            colorList.Add(Color.Violet);

            GraphPane myPane = zedGraphControl1.GraphPane;
            int idx = myPane.CurveList.Count;
            while (idx >= colorList.Count)
            {
                idx = idx - colorList.Count;                
            }
            aColor = colorList[idx];

            return aColor;
        }

        private SymbolType GetGraphSymbolType()
        {
            SymbolType aST;
            GraphPane myPane = zedGraphControl1.GraphPane;
            int idx = myPane.CurveList.Count;            
            string[] STs = Enum.GetNames(typeof(SymbolType));
            while (idx >= STs.Length)
            {
                idx = idx - STs.Length;
            }
            aST = (SymbolType)Enum.Parse(typeof(SymbolType), STs[idx],true);

            return aST;
        }

        private string[] GetLonGridStr(List<MeteoInfoC.PointD> pointList)
        {
            string[] GStrList = new string[pointList.Count];
            string drawStr;
            int i;

            if (_meteoDataInfo.IsLonLat)
            {
                double lon = 0;
                for (i = 0; i < pointList.Count; i++)
                {
                    lon = pointList[i].X;
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
                    else
                    {
                        drawStr = (360 - lon).ToString() + "W";
                    }
                    GStrList[i] = drawStr;
                }
            }
            else
            {
                for (i = 0; i < pointList.Count; i++)
                {
                    drawStr = pointList[i].ToString();
                    GStrList[i] = (drawStr);
                }
            }

            return GStrList;
        }

        private string[] GetLatGridStr(List<MeteoInfoC.PointD> pointList)
        {
            string[] GStrList = new string[pointList.Count];
            string drawStr;
            int i;

            if (_meteoDataInfo.IsLonLat)
            {
                double lat = 0;
                for (i = 0; i < pointList.Count; i++)
                {
                    lat = pointList[i].X;
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
                    GStrList[i] = drawStr;
                }
            }
            else
            {
                for (i = 0; i < pointList.Count; i++)
                {
                    drawStr = pointList[i].X.ToString();
                    GStrList[i] = drawStr;
                }
            }

            return GStrList;
        }

        private string[] GetLevelGridStr(List<MeteoInfoC.PointD> pointList)
        {
            string[] GStrList = new string[pointList.Count];
            for (int i = 0; i < pointList.Count; i++)
            {
                GStrList[i] = pointList[i].X.ToString();
            }

            return GStrList;
        }

        private string GetTimeFormatStr(DateTime firstTime, DateTime secondTime)
        {
            string timeFormat;
            if ((secondTime - firstTime).Duration().Days >= 1)
            {
                timeFormat = "yyyy-MM-dd";
            }
            else if ((secondTime - firstTime).Duration().Hours >= 1)
            {
                timeFormat = "yyyy-MM-dd HH";
            }
            else
            {
                timeFormat = "yyyy-MM-dd HH:mm";
            }

            return timeFormat;
        }

        private string[] GetTimeGridStr(List<MeteoInfoC.PointD> pointList)
        {
            string[] GStrList = new string[pointList.Count];
            int i;
            List<DateTime> DTList = new List<DateTime>();
            for (i = 0; i < pointList.Count; i++)
            {
                DTList.Add(DataConvert.ToDateTime(pointList[i].X));
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

            //if (DTList[0].Year == DTList[DTList.Count - 1].Year)
            //{
            //    timeFormat = timeFormat.Substring(5);
            //    if (DTList[0].Month == DTList[DTList.Count - 1].Month)
            //    {
            //        timeFormat = timeFormat.Substring(3);
            //    }
            //}

            for (i = 0; i < DTList.Count; i++)
            {
                GStrList[i] = DTList[i].ToString(timeFormat);
            }

            return GStrList;
        }

        private string GetLayerName()
        {
            string layerName;
            layerName = CB_Vars.Text;
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

        private void TSB_Draw_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ////Get X/Y
            //GetXYCoords();

            //Get data
            _meteoDataInfo.LonIndex = CB_Lon1.SelectedIndex;
            _meteoDataInfo.LatIndex = CB_Lat1.SelectedIndex;
            _meteoDataInfo.LevelIndex = CB_Level1.SelectedIndex;
            _meteoDataInfo.TimeIndex = CB_Time1.SelectedIndex;
            string varName = CB_Vars.Text;

            GridData gData = null;
            if (CHB_NewVariable.Checked)
            {
                MathParser mathParser = new MathParser(_meteoDataInfo);
                gData = (GridData)mathParser.Evaluate(TB_NewVariable.Text);
            }
            else
                gData = _meteoDataInfo.GetGridData(varName);

            if (gData == null)
                return;

            _pointList = new List<MeteoInfoC.PointD>();
            MeteoInfoC.PointD aP;
            for (int i = 0; i < gData.XNum; i++)
            {
                if (_plotDimension == PlotDimension.Level)
                {
                    aP = new MeteoInfoC.PointD();
                    aP.X = gData.Data[0, i];
                    aP.Y = gData.X[i];
                }
                else
                {
                    aP = new MeteoInfoC.PointD();
                    aP.X = gData.X[i];
                    aP.Y = gData.Data[0, i];
                }
                _pointList.Add(aP);
            }
            
            //switch (_meteoDataInfo.DataType)
            //{
            //    case MeteoDataType.GrADS_Grid:
            //        _pointList = GetGrADSGridData();
            //        break;                                
            //    case MeteoDataType.ARL_Grid:
            //        _pointList = GetARLGridData();
            //        break;
            //    case MeteoDataType.NetCDF:
            //        _pointList = GetNetCDFData();
            //        break;
            //    case MeteoDataType.GRIB1:
            //        _pointList = GetGRIB1OneDimData();
            //        break;
            //    case MeteoDataType.GRIB2:
            //        _pointList = GetGRIB2OneDimData();
            //        break;
            //}
            if (_pointList.Count == 0)
            {
                MessageBox.Show("No data!", "Alarm");
                this.Cursor = Cursors.Default;
                return;
            }
            
            //Plot
            CreateZedGraph(zedGraphControl1, _pointList);
            zedGraphControl1.Refresh();

            //Enable time controls            
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

        //private List<MeteoInfoC.PointD> GetGrADSGridData()
        //{
        //    List<MeteoInfoC.PointD> pointList = new List<MeteoInfoC.PointD>();
        //    //GrADSData CGrADSData = new GrADSData();
        //    GrADSDataInfo aDataInfo = (GrADSDataInfo)_meteoDataInfo.DataInfo;

        //    //Read grid data
        //    int vIdx = CB_Vars.SelectedIndex;
        //    int lonIdx, latIdx, lIdx, tIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = CGrADSData.ReadGrADSData_Grid_Time(aDataInfo.DSET, latIdx, lonIdx,
        //                vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = CGrADSData.ReadGrADSData_Grid_Level(aDataInfo.DSET, latIdx, lonIdx,
        //                vIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = CGrADSData.ReadGrADSData_Grid_Lon(aDataInfo.DSET, latIdx, lIdx,
        //                vIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = CGrADSData.ReadGrADSData_Grid_Lat(aDataInfo.DSET, lonIdx, lIdx,
        //                vIdx, tIdx, aDataInfo);
        //            break;
        //    }

        //    return pointList;
        //}

        private List<MeteoInfoC.PointD> GetGRIB1OneDimData()
        {
            List<MeteoInfoC.PointD> pointList = new List<MeteoInfoC.PointD>();           
            GRIB1DataInfo aDataInfo = (GRIB1DataInfo)_meteoDataInfo.DataInfo;

            //Read grid data
            int vIdx = CB_Vars.SelectedIndex;
            int lonIdx, latIdx, lIdx, tIdx;
            switch (_plotDimension)
            {
                case PlotDimension.Time:
                    latIdx = CB_Lat1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Time(lonIdx, latIdx, vIdx, lIdx);
                    break;
                case PlotDimension.Level:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Level(lonIdx, latIdx, vIdx, tIdx);
                    break;
                case PlotDimension.Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Lon(tIdx, latIdx, vIdx, lIdx);
                    break;
                case PlotDimension.Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Lat(tIdx, lonIdx, vIdx, lIdx);
                    break;
            }

            return pointList;
        }

        private List<MeteoInfoC.PointD> GetGRIB2OneDimData()
        {
            List<MeteoInfoC.PointD> pointList = new List<MeteoInfoC.PointD>();
            GRIB2DataInfo aDataInfo = (GRIB2DataInfo)_meteoDataInfo.DataInfo;

            //Read grid data
            int vIdx = CB_Vars.SelectedIndex;
            int lonIdx, latIdx, lIdx, tIdx;
            switch (_plotDimension)
            {
                case PlotDimension.Time:
                    latIdx = CB_Lat1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Time(lonIdx, latIdx, vIdx, lIdx);
                    break;
                case PlotDimension.Level:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lonIdx = CB_Lon1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Level(lonIdx, latIdx, vIdx, tIdx);
                    break;
                case PlotDimension.Lon:
                    latIdx = CB_Lat1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Lon(tIdx, latIdx, vIdx, lIdx);
                    break;
                case PlotDimension.Lat:
                    lonIdx = CB_Lon1.SelectedIndex;
                    tIdx = CB_Time1.SelectedIndex;
                    lIdx = CB_Level1.SelectedIndex;
                    pointList = aDataInfo.GetOneDimData_Lat(tIdx, lonIdx, vIdx, lIdx);
                    break;
            }

            return pointList;
        }

        //private List<MeteoInfoC.PointD> GetARLGridData()
        //{
        //    List<MeteoInfoC.PointD> pointList = new List<MeteoInfoC.PointD>();
        //    //ARLMeteoData CARLMeteoData = new ARLMeteoData();
        //    ARLDataInfo aDataInfo = (ARLDataInfo)_meteoDataInfo.DataInfo;

        //    //Read grid data
        //    int vIdx = CB_Vars.SelectedIndex;
        //    int lonIdx, latIdx, lIdx, tIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = aDataInfo.GetGridData_Time(lonIdx, latIdx, vIdx, lIdx);
        //            break;
        //        case PlotDimension.Level:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = aDataInfo.GetARLGridData_Level(lonIdx, latIdx, vIdx, tIdx);
        //            break;
        //        case PlotDimension.Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = aDataInfo.GetARLGridData_Lon(tIdx, latIdx, vIdx, lIdx);
        //            break;
        //        case PlotDimension.Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = aDataInfo.GetARLGridData_Lat(tIdx, lonIdx, vIdx, lIdx);
        //            break;
        //    }

        //    return pointList;
        //}

        //private List<MeteoInfoC.PointD> GetNetCDFData()
        //{
        //    List<MeteoInfoC.PointD> pointList = new List<MeteoInfoC.PointD>();
        //    NetCDFData CNetCDFData = new NetCDFData();
        //    NetCDFDataInfo aDataInfo = (NetCDFDataInfo)_meteoDataInfo.DataInfo;

        //    //Read grid data
        //    int vIdx = CB_Vars.SelectedIndex;
        //    int lonIdx, latIdx, lIdx, tIdx;
        //    switch (_plotDimension)
        //    {
        //        case PlotDimension.Time:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = CNetCDFData.GetNetCDFGridData_Time(latIdx, lonIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Level:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            pointList = CNetCDFData.GetNetCDFGridData_Level(latIdx, lonIdx, vIdx, tIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Lon:
        //            latIdx = CB_Lat1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = CNetCDFData.GetNetCDFGridData_Lon(latIdx, tIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //        case PlotDimension.Lat:
        //            lonIdx = CB_Lon1.SelectedIndex;
        //            tIdx = CB_Time1.SelectedIndex;
        //            lIdx = CB_Level1.SelectedIndex;
        //            pointList = CNetCDFData.GetNetCDFGridData_Lat(tIdx, lonIdx, vIdx, lIdx, aDataInfo);
        //            break;
        //    }

        //    return pointList;
        //}

        private void TSB_ClearDrawing_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.Refresh();
        }

        private void CB_DrawType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ZedGraphType = (ZedGraphType)Enum.Parse(typeof(ZedGraphType), CB_DrawType.Text, true);
        }

        private void TSB_PreTime_Click(object sender, EventArgs e)
        {
            if (CB_Time1.SelectedIndex > 0)
            {
                CB_Time1.SelectedIndex = CB_Time1.SelectedIndex - 1;
            }
            else
            {
                CB_Time1.SelectedIndex = CB_Time1.Items.Count - 1;
            }
            zedGraphControl1.GraphPane.CurveList.Clear();
            TSB_Draw.PerformClick();
        }

        private void TSB_NextTime_Click(object sender, EventArgs e)
        {
            if (CB_Time1.SelectedIndex < CB_Time1.Items.Count - 1)
            {
                CB_Time1.SelectedIndex = CB_Time1.SelectedIndex + 1;
            }
            else
            {
                CB_Time1.SelectedIndex = 0;
            }
            zedGraphControl1.GraphPane.CurveList.Clear();
            TSB_Draw.PerformClick();
        }

        private void TSB_Animitor_Click(object sender, EventArgs e)
        {
            if (CB_Time1.Items.Count > 1)
            {                
                for (int i = 0; i < CB_Time1.Items.Count; i++)
                {
                    zedGraphControl1.GraphPane.CurveList.Clear();
                    CB_Time1.SelectedIndex = i;
                    TSB_Draw.PerformClick();
                    System.Threading.Thread.Sleep(500);
                }
            }   
        }

        private void CB_PlotDims_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void TSB_ViewData_Click(object sender, EventArgs e)
        {
            string dimStr = _plotDimension.ToString();
            string[] labels = zedGraphControl1.GraphPane.XAxis.Scale.TextLabels;
            int colNum = zedGraphControl1.GraphPane.CurveList.Count;
            int rowNum = labels.Length;

            frmDataGridViewData aForm = new frmDataGridViewData();
            aForm.DataType = "PointData";
            aForm.dataGridView1.ColumnCount = colNum + 1;
            aForm.dataGridView1.RowCount = rowNum;
            aForm.dataGridView1.Columns[0].Name = dimStr;
            aForm.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < colNum; i++)
            {
                aForm.dataGridView1.Columns[i + 1].Name = zedGraphControl1.GraphPane.CurveList[i].Label.Text;
                aForm.dataGridView1.Columns[i + 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < rowNum; i++)
            {
                aForm.dataGridView1[0, i].Value = labels[i];
                for (int j = 0; j < colNum; j++)
                    aForm.dataGridView1[j + 1, i].Value = zedGraphControl1.GraphPane.CurveList[j].Points[i].Y;
            }

            aForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            aForm.dataGridView1.MultiSelect = true;
            aForm.dataGridView1.AllowUserToOrderColumns = true;

            aForm.Text = "Point Data";
            aForm.Show(this);
        }

    }
}
