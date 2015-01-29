using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MeteoInfoC.Layer;
using MeteoInfoC.Data.MapData;
using MeteoInfoC.Shape;
using MeteoInfoC.Legend;

namespace MeteoInfo.Forms
{
    public partial class frmAddXYLayer : Form
    {
        private string m_Infile;

        public frmAddXYLayer()
        {
            InitializeComponent();
        }

        private void B_InFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();

            openDlg.Filter = "Supported Formats|*.csv;*.txt|CSV File (*.csv)|*.csv|Text File (*.txt)|*.txt";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                m_Infile = openDlg.FileName;
                TB_InFile.Text = m_Infile;
                StreamReader sr = new StreamReader(m_Infile, System.Text.Encoding.Default);
                try
                {
                    string aTitle;
                    string[] aTitleArray;
                    aTitle = sr.ReadLine();
                    aTitleArray = aTitle.Split(',');
                    if (aTitleArray.Length <= 1)
                    {
                        MessageBox.Show("File Format Error!", "Error");
                        return;
                    }
                    else
                    {
                        GB_SelField.Enabled = true;
                        CB_LonFld.Items.Clear();
                        CB_LatFld.Items.Clear();
                        for (int i = 0; i < aTitleArray.Length; i++)
                        {                            
                            CB_LatFld.Items.Add(aTitleArray[i]);
                            CB_LonFld.Items.Add(aTitleArray[i]);                            
                        }
                    }
                    sr.Close();
                }
                catch
                {
                    MessageBox.Show("Data file open failed!", "Error");
                }
            }
        }

        private void frmAddXYLayer_Load(object sender, EventArgs e)
        {
            GB_SelField.Enabled = false;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void B_AddData_Click(object sender, EventArgs e)
        {
            if (CB_LonFld.Text == "" || CB_LatFld.Text == "")
            {
                MessageBox.Show("All fields should be set!", "Alarm");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            string shpFile;
            saveDlg.Filter = "shp file (*.shp)|*.shp";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                shpFile = saveDlg.FileName;
                if (System.IO.File.Exists(shpFile))
                {
                    string lPathNoExt = System.IO.Path.GetDirectoryName(shpFile) + @"\" + System.IO.Path.GetFileNameWithoutExtension(shpFile);
                    System.IO.File.Delete(lPathNoExt + ".shp");
                    System.IO.File.Delete(lPathNoExt + ".shx");
                    System.IO.File.Delete(lPathNoExt + ".dbf");
                    System.IO.File.Delete(lPathNoExt + ".prj");
                }

                //New layer
                VectorLayer aLayer = new VectorLayer(ShapeTypes.Point);                
                aLayer.LayerDrawType = LayerDrawType.Map;
                aLayer.LayerName = System.IO.Path.GetFileName(shpFile);
                aLayer.FileName = shpFile;               
                aLayer.LegendScheme = LegendManage.CreateSingleSymbolLegendScheme(ShapeTypes.Point, Color.Black, 5);
                aLayer.Visible = true;

                int lonIdx, latIdx;                
                lonIdx = CB_LonFld.SelectedIndex;
                latIdx = CB_LatFld.SelectedIndex;                            
                double lon, lat;

                StreamReader sr = new StreamReader(m_Infile, System.Text.Encoding.UTF8);
                string[] dataArray;
                string aLine = sr.ReadLine();    //First line - title
                //Get field list
                List<string> fieldList = new List<string>();
                dataArray = aLine.Split(',');
                if (dataArray.Length < 3)
                {
                    MessageBox.Show("The data should have at least three fields!", "Error");
                    return;
                }
                fieldList = new List<string>(dataArray.Length);
                fieldList.AddRange(dataArray);

                //Judge field type
                List<string> varList = new List<string>();
                aLine = sr.ReadLine();    //First data line
                dataArray = aLine.Split(',');
                for (int i = 3; i < dataArray.Length; i++)
                {
                    if (MeteoInfoC.Global.MIMath.IsNumeric(dataArray[i]))
                        varList.Add(fieldList[i]);
                }


                //Add fields
                for (int i = 0; i < fieldList.Count; i++)
                {
                    DataColumn aDC = new DataColumn();
                    aDC.ColumnName = fieldList[i];
                    if (varList.Contains(fieldList[i]))
                        aDC.DataType = typeof(double);
                    else
                        aDC.DataType = typeof(string);
                    aLayer.EditAddField(aDC);
                }

                //Read data
                //aLine = sr.ReadLine();
                while (aLine != null)
                {
                    dataArray = aLine.Split(',');
                    if (dataArray.Length < 2)
                    {
                        aLine = sr.ReadLine();
                        continue;
                    }

                    MeteoInfoC.PointD aPoint = new MeteoInfoC.PointD();                    
                    lon = double.Parse(dataArray[lonIdx]);
                    lat = double.Parse(dataArray[latIdx]);                    
                    aPoint.X = lon;
                    aPoint.Y = lat;

                    //Add shape
                    PointShape aPS = new PointShape();
                    aPS.Point = aPoint;
                    int shapeNum = aLayer.ShapeNum;
                    if (aLayer.EditInsertShape(aPS, shapeNum))
                    {
                        //Edit record value
                        for (int j = 0; j < fieldList.Count; j++)
                        {
                            if (varList.Contains(fieldList[j]))
                                aLayer.EditCellValue(fieldList[j], shapeNum, double.Parse(dataArray[j]));
                            else
                                aLayer.EditCellValue(fieldList[j], shapeNum, dataArray[j]);
                        }                     
                    }

                    aLine = sr.ReadLine();
                }

                //Save shape file                
                ShapeFileManage.SaveShapeFile(shpFile, aLayer);

                //Add layer
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
            }
        }
    }
}
