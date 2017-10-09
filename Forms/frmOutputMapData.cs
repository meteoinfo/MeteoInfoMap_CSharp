using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfo.Classes;
using System.IO;
using MeteoInfoC.Layer;
using MeteoInfoC.Shape;
using MeteoInfoC.Data.MapData;
using MeteoInfoC.Global;
using MeteoInfoC.Data.MeteoData;

namespace MeteoInfo.Forms
{
    public partial class frmOutputMapData : Form
    {
        List<VectorLayer> m_MapLayers = new List<VectorLayer>();
        VectorLayer _currentLayer;
        //clsGlobal.Extent m_Extent = new clsGlobal.Extent();

        public frmOutputMapData()
        {
            InitializeComponent();
        }

        private void frmConvertMapLayer_Load(object sender, EventArgs e)
        {            
            toolStripProgressBar1.Visible = false;

            //Get map layer list
            m_MapLayers.Clear();
            int i;
            for (i = 0; i < frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers.Count; i++)
            {
                if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i].GetType() == typeof(VectorLayer))
                {
                    VectorLayer aLayer = (VectorLayer)frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i];
                    if (aLayer.LayerType == LayerTypes.VectorLayer)
                        m_MapLayers.Add(aLayer);
                    //if (aLayer.LayerDrawType == LayerDrawType.Map)
                    //{
                    //    m_MapLayers.Add(aLayer);
                    //}
                }
            }

            CB_MapLayers.Items.Clear();
            if (m_MapLayers.Count > 0)
            {                
                for (i = 0; i < m_MapLayers.Count; i++)
                {
                    CB_MapLayers.Items.Add(m_MapLayers[i].LayerName);
                }
                CB_MapLayers.SelectedIndex = 0;
            }
        }

        private void CB_MapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {            
            _currentLayer = m_MapLayers[CB_MapLayers.SelectedIndex];                       
            
            int i;
            string str = CB_OutputFormat.Text;
            CB_OutputFormat.Items.Clear();
            CB_OutputFormat.Items.Add("ASCII wmp File");
            CB_OutputFormat.Items.Add("Shape File");
            CB_OutputFormat.Items.Add("KML File");
            List<int> selIndexes = _currentLayer.GetSelectedShapeIndexes();
            switch (_currentLayer.ShapeType)
            {
                case ShapeTypes.Polygon:
                    CB_OutputFormat.Items.Add("GrADS Map File");
                    CB_OutputFormat.Items.Add("GrADS Maskout File");
                    CB_OutputFormat.Items.Add("Surfer BLN File");                  
                    break;
                case ShapeTypes.Polyline:
                    CB_OutputFormat.Items.Add("GrADS Map File");
                    CB_OutputFormat.Items.Add("Surfer BLN File");  
                    break;                                    
            }

            int idx = CB_OutputFormat.Items.IndexOf(str);
            if (idx >= 0)
                CB_OutputFormat.SelectedIndex = idx;
            else
                CB_OutputFormat.SelectedIndex = 0;
            
        }

        private void B_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }             
                
        private void B_Convert_Click(object sender, EventArgs e)
        {
            switch (CB_OutputFormat.Text)
            {
                case "ASCII wmp File":
                    SaveWMPFile();
                    break;
                case "GrADS Map File":
                    SaveGrADSMapFile();
                    break;
                case "GrADS Maskout File":
                    SavGrADSMaskoutFile();
                    break;
                case "Shape File":
                    SaveShapeFile();
                    break;
                case "Surfer BLN File":
                    SaveBLNMapFile();
                    break;
                case "KML File":
                    SaveKMLFile();
                    break;
            }
            
        }

        private void SaveWMPFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            SFDlg.Filter = "wmp File (*.wmp)|*.wmp";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                //ProgressBar
                toolStripProgressBar1.Visible = true;
                this.Cursor = Cursors.WaitCursor;

                string aFile = SFDlg.FileName;
                StreamWriter sw = new StreamWriter(aFile);
                List<int> selIndexes = _currentLayer.GetSelectedShapeIndexes();
                bool hasSelShape = _currentLayer.HasSelectedShapes();
                int shpNum = _currentLayer.ShapeNum;
                if (hasSelShape)
                    shpNum = selIndexes.Count;
                int i;
                switch (_currentLayer.ShapeType)
                {
                    case ShapeTypes.Point:
                        sw.WriteLine("Point");
                        sw.WriteLine(shpNum.ToString());                        
                        PointShape aPS = new PointShape();
                        if (hasSelShape)
                        {
                            for (i = 0; i < _currentLayer.ShapeNum; i++)
                            {
                                aPS = (PointShape)_currentLayer.ShapeList[i];
                                if (aPS.Selected)
                                    sw.WriteLine(aPS.Point.X.ToString() + "," + aPS.Point.Y.ToString());
                                this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                            }
                        }
                        else
                        {
                            for (i = 0; i < _currentLayer.ShapeNum; i++)
                            {
                                aPS = (PointShape)_currentLayer.ShapeList[i];
                                sw.WriteLine(aPS.Point.X.ToString() + "," + aPS.Point.Y.ToString());
                                this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                            }
                        }
                        break;
                    case ShapeTypes.Polyline:
                        sw.WriteLine("Polyline");                        
                        int shapeNum = 0;
                        PolylineShape aPLS = new PolylineShape();
                        if (hasSelShape)
                        {
                            for (i = 0; i < _currentLayer.ShapeNum; i++)
                            {
                                aPLS = (PolylineShape)_currentLayer.ShapeList[i];
                                if (aPLS.Selected)
                                    shapeNum += aPLS.PartNum;
                            }
                        }
                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPLS = (PolylineShape)_currentLayer.ShapeList[i];
                            shapeNum += aPLS.PartNum;
                        }
                        sw.WriteLine(shpNum.ToString());

                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPLS = (PolylineShape)_currentLayer.ShapeList[i];
                            if (hasSelShape)
                            {
                                if (!aPLS.Selected)
                                    continue;
                            }
                            MeteoInfoC.PointD[] Pointps;
                            for (int p = 0; p < aPLS.PartNum; p++)
                            {
                                if (p == aPLS.PartNum - 1)
                                {
                                    Pointps = new MeteoInfoC.PointD[aPLS.PointNum - aPLS.parts[p]];
                                    for (int pp = aPLS.parts[p]; pp < aPLS.PointNum; pp++)
                                    {
                                        Pointps[pp - aPLS.parts[p]] = (MeteoInfoC.PointD)aPLS.Points[pp];
                                    }
                                }
                                else
                                {
                                    Pointps = new MeteoInfoC.PointD[aPLS.parts[p + 1] - aPLS.parts[p]];
                                    for (int pp = aPLS.parts[p]; pp < aPLS.parts[p + 1]; pp++)
                                    {
                                        Pointps[pp - aPLS.parts[p]] = (MeteoInfoC.PointD)aPLS.Points[pp];
                                    }
                                }
                                sw.WriteLine(Pointps.Length.ToString());
                                foreach (MeteoInfoC.PointD aPoint in Pointps)
                                {
                                    sw.WriteLine(aPoint.X.ToString() + "," + aPoint.Y.ToString());
                                }
                                shapeNum += 1;
                            }
                            Application.DoEvents();
                            this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                        }
                        break;
                    case ShapeTypes.Polygon:
                        sw.WriteLine("Polygon");                        
                        shapeNum = 0;
                        PolygonShape aPGS = new PolygonShape();
                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPGS = (PolygonShape)_currentLayer.ShapeList[i];
                            if (hasSelShape)
                            {
                                if (!aPGS.Selected)
                                    continue;
                            }
                            shapeNum += aPGS.PartNum;
                        }
                        sw.WriteLine(shapeNum.ToString());
                       
                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPGS = (PolygonShape)_currentLayer.ShapeList[i];
                            if (hasSelShape)
                            {
                                if (!aPGS.Selected)
                                    continue;
                            }

                            MeteoInfoC.PointD[] Pointps;
                            for (int p = 0; p < aPGS.PartNum; p++)
                            {
                                if (p == aPGS.PartNum - 1)
                                {
                                    Pointps = new MeteoInfoC.PointD[aPGS.PointNum - aPGS.parts[p]];
                                    for (int pp = aPGS.parts[p]; pp < aPGS.PointNum; pp++)
                                    {
                                        Pointps[pp - aPGS.parts[p]] = (MeteoInfoC.PointD)aPGS.Points[pp];
                                    }
                                }
                                else
                                {
                                    Pointps = new MeteoInfoC.PointD[aPGS.parts[p + 1] - aPGS.parts[p]];
                                    for (int pp = aPGS.parts[p]; pp < aPGS.parts[p + 1]; pp++)
                                    {
                                        Pointps[pp - aPGS.parts[p]] = (MeteoInfoC.PointD)aPGS.Points[pp];
                                    }
                                }
                                sw.WriteLine(Pointps.Length.ToString());                                
                                foreach (MeteoInfoC.PointD aPoint in Pointps)
                                {
                                    sw.WriteLine(aPoint.X.ToString() + "," + aPoint.Y.ToString());                                    
                                }
                                shapeNum += 1;
                            }
                            Application.DoEvents();
                            this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                        }                        
                        break;
                }

                sw.Close();

                //Progressbar
                this.toolStripProgressBar1.Value = 0;
                this.toolStripProgressBar1.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveBLNMapFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            SFDlg.Filter = "BLN File (*.bln)|*.bln";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                //ProgressBar
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                this.Cursor = Cursors.WaitCursor;

                string aFile = SFDlg.FileName;
                List<Shape> shapes = new List<Shape>();
                bool hasSelShape = _currentLayer.HasSelectedShapes();
                for (int i = 0; i < _currentLayer.ShapeNum; i++)
                {
                    Shape aShape = _currentLayer.ShapeList[i];
                    if (hasSelShape)
                    {
                        if (!aShape.Selected)
                            continue;
                    }
                    shapes.Add(aShape);
                }
                MapDataManage.WriteMapFile_BLN(aFile, shapes);

                //Progressbar
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripProgressBar1.Value = 0;
                this.toolStripProgressBar1.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveGrADSMapFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            //SFDlg.Filter = "wmp File (*.wmp)|*.wmp";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                //ProgressBar
                toolStripProgressBar1.Visible = true;
                this.Cursor = Cursors.WaitCursor;

                string aFile = SFDlg.FileName;
                bool hasSelShape = _currentLayer.HasSelectedShapes();
                int shpNum = _currentLayer.ShapeNum;
                if (hasSelShape)
                    shpNum = _currentLayer.GetSelectedShapeIndexes().Count;                
                int i;                
                switch (_currentLayer.ShapeType)
                {                    
                    case ShapeTypes.Polyline:
                        List<PolylineShape> polylines = new List<PolylineShape>();
                        PolylineShape aPLS = new PolylineShape();
                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPLS = (PolylineShape)_currentLayer.ShapeList[i];
                            if (hasSelShape)
                            {
                                if (!aPLS.Selected)
                                    continue;
                            }

                            if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
                            {
                                if (aPLS.Extent.minX < 0 && aPLS.Extent.maxX > 0)
                                {
                                    polylines.AddRange(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.CutPolyLineShapeLon(0, aPLS));
                                }
                                else
                                {
                                    polylines.Add(aPLS);
                                }
                            }
                            else
                                polylines.Add(aPLS);

                            Application.DoEvents();
                            this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                        }
                        MapDataManage.WriteMapFile_GrADS(aFile, polylines);
                        break;
                    case ShapeTypes.Polygon:
                        List<PolygonShape> polygons = new List<PolygonShape>();
                        PolygonShape aPGS = new PolygonShape();
                        for (i = 0; i < _currentLayer.ShapeNum; i++)
                        {
                            aPGS = (PolygonShape)_currentLayer.ShapeList[i];
                            if (hasSelShape)
                            {
                                if (!aPGS.Selected)
                                    continue;
                            }
                            if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
                            {
                                if (aPGS.Extent.minX < 0 && aPGS.Extent.maxX > 0)
                                {
                                    polygons.AddRange(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.CutPolygonShapeLon(0, aPGS));
                                }
                                else
                                {
                                    polygons.Add(aPGS);
                                }
                            }
                            else
                                polygons.Add(aPGS);

                            Application.DoEvents();
                            this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                        }
                        MapDataManage.WriteMapFile_GrADS(aFile, polygons);
                        break;
                }
                
                //Progressbar
                this.toolStripProgressBar1.Value = 0;
                this.toolStripProgressBar1.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void SavGrADSMaskoutFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            SFDlg.Filter = "GrADS File (*.ctl)|*.ctl";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                string aFile = SFDlg.FileName;
                int i;
                bool hasSelShape = _currentLayer.HasSelectedShapes();            

                //Get grid set
                PolygonShape aPGS = new PolygonShape();
                Extent aExtent = new Extent();
                int n = 0;
                for (i = 0; i < _currentLayer.ShapeNum; i++)
                {
                    aPGS = (PolygonShape)_currentLayer.ShapeList[i];
                    if (hasSelShape)
                    {
                        if (!aPGS.Selected)
                            continue;
                    }
                    if (n == 0)
                    {
                        aExtent = aPGS.Extent;
                    }
                    else
                    {
                        aExtent = MIMath.GetLagerExtent(aExtent, aPGS.Extent);
                    }
                    n += 1;
                }

                GridDataSetting aGDP = new GridDataSetting();
                aGDP.DataExtent.minX = Math.Floor(aExtent.minX);
                aGDP.DataExtent.maxX = Math.Ceiling(aExtent.maxX);
                aGDP.DataExtent.minY = Math.Floor(aExtent.minY);
                aGDP.DataExtent.maxY = Math.Ceiling(aExtent.maxY);
                aGDP.XNum = 20;
                aGDP.YNum = 20;

                frmGridSet aFrmGS = new frmGridSet();
                aFrmGS.SetParameters(aGDP);
                if (aFrmGS.ShowDialog() == DialogResult.OK)
                {
                    aFrmGS.GetParameters(ref aGDP);

                    //Show progressbar
                    this.toolStripProgressBar1.Visible = true;
                    this.toolStripProgressBar1.Value = 0;
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    //Get grid data
                    double[,] gridData = new double[aGDP.YNum, aGDP.XNum];
                    int j, p;
                    MeteoInfoC.PointD aPoint = new MeteoInfoC.PointD();                    
                    double xSize, ySize;
                    xSize = (aGDP.DataExtent.maxX - aGDP.DataExtent.minX) / (aGDP.XNum - 1);
                    ySize = (aGDP.DataExtent.maxY - aGDP.DataExtent.minY) / (aGDP.YNum - 1);
                    bool isIn = false;
                    for (i = 0; i < aGDP.YNum; i++)
                    {
                        aPoint.Y = aGDP.DataExtent.minY + i * ySize;
                        for (j = 0; j < aGDP.XNum; j++)
                        {                            
                            aPoint.X = aGDP.DataExtent.minX + j * xSize;
                            isIn = false;
                            for (p = 0; p < _currentLayer.ShapeNum; p++)
                            {                                
                                aPGS = (PolygonShape)_currentLayer.ShapeList[p];
                                if (hasSelShape)
                                {
                                    if (!aPGS.Selected)
                                        continue;
                                }
                                if (MIMath.PointInPolygon(aPGS, aPoint))
                                {
                                    isIn = true;
                                    break;
                                }                                
                            }
                            if (isIn)
                            {
                                gridData[i, j] = 1;
                            }
                            else
                            {
                                gridData[i, j] = -1;
                            }
                        }
                        this.toolStripProgressBar1.Value = (i + 1) * 100 / aGDP.YNum;
                        Application.DoEvents();
                    }

                    //Get GrADS data info
                    string dFile = Path.ChangeExtension(aFile, ".dat");
                    GrADSDataInfo aDataInfo = new GrADSDataInfo();
                    aDataInfo.FileName = aFile;
                    aDataInfo.TITLE = "Mask data";
                    aDataInfo.DSET = dFile;
                    aDataInfo.DTYPE = "GRIDDED";
                    aDataInfo.XDEF.Type = "LINEAR";
                    aDataInfo.XDEF.XNum = aGDP.XNum;
                    aDataInfo.XDEF.XMin = (Single)aGDP.DataExtent.minX;
                    aDataInfo.XDEF.XDelt = (Single)(xSize);
                    aDataInfo.YDEF.Type = "LINEAR";
                    aDataInfo.YDEF.YNum = aGDP.YNum;
                    aDataInfo.YDEF.YMin = (Single)aGDP.DataExtent.minY;
                    aDataInfo.YDEF.YDelt = (Single)(ySize);
                    aDataInfo.ZDEF.Type = "LINEAR";
                    aDataInfo.ZDEF.ZNum = 1;
                    aDataInfo.ZDEF.SLevel = 1;
                    aDataInfo.ZDEF.ZDelt = 1;
                    aDataInfo.TDEF.Type = "LINEAR";
                    aDataInfo.TDEF.TNum = 1;
                    aDataInfo.TDEF.STime = DateTime.Now;
                    aDataInfo.TDEF.TDelt = "1mo";
                    Variable aVar = new Variable();
                    aVar.Name = "mask";
                    //aVar.LevelNum = 0;
                    aVar.Units = "99";
                    aVar.Description = "background mask data";
                    aDataInfo.VARDEF.AddVar(aVar);

                    //Save files                    
                    aDataInfo.WriteGrADSCTLFile();
                    aDataInfo.WriteGrADSData_Grid(dFile, gridData);

                    //Hide progressbar
                    this.toolStripProgressBar1.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void SaveShapeFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            SFDlg.Filter = "Shape File (*.shp)|*.shp";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                //ProgressBar
                toolStripProgressBar1.Visible = true;
                this.Cursor = Cursors.WaitCursor;

                string aFile = SFDlg.FileName;

                //Create a VectorLayer with selected shapes
                int i, j;
                VectorLayer aLayer = new VectorLayer(_currentLayer.ShapeType);
                for (i = 0; i < _currentLayer.NumFields; i++)
                {
                    aLayer.EditAddField(_currentLayer.Fields[i].ColumnName, _currentLayer.Fields[i].DataType);
                }
                bool hasSelShape = _currentLayer.HasSelectedShapes();                
                
                for (i = 0; i < _currentLayer.ShapeNum; i++)
                {                    
                    Shape aPS = _currentLayer.ShapeList[i];
                    if (hasSelShape)
                    {
                        if (!aPS.Selected)
                            continue;
                    }
                    int sNum = aLayer.ShapeNum;
                    if (aLayer.EditInsertShape(aPS, sNum))
                    {
                        for (j = 0; j < aLayer.NumFields; j++)
                            aLayer.EditCellValue(j, sNum, _currentLayer.GetCellValue(j, i));
                    }

                    Application.DoEvents();
                    this.toolStripProgressBar1.Value = (i + 1) * 100 / _currentLayer.ShapeNum;
                }

                aLayer.ProjInfo = _currentLayer.ProjInfo;
                aLayer.SaveFile(aFile);

                //Progressbar
                this.toolStripProgressBar1.Value = 0;
                this.toolStripProgressBar1.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveKMLFile()
        {
            SaveFileDialog SFDlg = new SaveFileDialog();
            SFDlg.Filter = "KML File (*.kml)|*.kml";
            if (SFDlg.ShowDialog() == DialogResult.OK)
            {
                //ProgressBar            
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;

                string aFile = SFDlg.FileName;
                _currentLayer.SaveAsKMLFile(aFile);                

                //Progressbar                
                this.Cursor = Cursors.Default;
            }
        }

    }
}
