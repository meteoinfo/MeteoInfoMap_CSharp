using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MeteoInfoC.Data.MeteoData;

namespace MeteoInfo.Forms
{
    public partial class frmMICAPS4To : Form
    {
        private string[] _M4Files = new string[0];

        public frmMICAPS4To()
        {
            InitializeComponent();
        }

        private void frmMICAPS4To_Load(object sender, EventArgs e)
        {
            CB_OutputFormat.Items.Add("NetCDF");
            CB_OutputFormat.SelectedIndex = 0;

            toolStripProgressBar1.Visible = false;
        }

        private void B_OpenFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Multiselect = true;
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                _M4Files = aDlg.FileNames;

                LB_SelectedFiles.Items.Clear();
                foreach (string aFile in _M4Files)
                {
                    LB_SelectedFiles.Items.Add(Path.GetFileName(aFile));
                }
            }
        }

        private void B_Convert_Click(object sender, EventArgs e)
        {
            //Check number of selected files
            int fNum = _M4Files.Length;
            if (fNum == 0)
            {                
                return;
            }

            SaveFileDialog aDlg = new SaveFileDialog();
            aDlg.Filter = "NetCDF File (*.nc)|*.nc";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                string outFile = aDlg.FileName;

                //Show progressbar
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;
                this.Cursor = Cursors.WaitCursor;

                //File loop            
                int i;
                //NetCDFData CNetCDFData = new NetCDFData();
                NetCDFDataInfo outDataInfo = new NetCDFDataInfo();
                List<string> varList = new List<string>();
                MICAPS4DataInfo inDataInfo = new MICAPS4DataInfo();
                inDataInfo.ReadDataInfo(_M4Files[0]); 

                //Set data info    
                outDataInfo.FileName = outFile;
                outDataInfo.IsGlobal = false;
                outDataInfo.isLatLon = true;
                outDataInfo.MissingValue = inDataInfo.MissingValue;
                outDataInfo.unlimdimid = 2;

                //Add dimensions: lon, lat, time
                Dimension lonDim = outDataInfo.AddDimension("lon", inDataInfo.XNum);
                Dimension latDim = outDataInfo.AddDimension("lat", inDataInfo.YNum);
                Dimension timeDim = outDataInfo.AddDimension("time", -1);  
              
                //Add variables
                outDataInfo.AddVariable("lon", NetCDF4.NcType.NC_DOUBLE, new Dimension[] { lonDim });
                outDataInfo.AddVariableAttribute("lon", "units", "degrees_east");
                outDataInfo.AddVariableAttribute("lon", "long_name", "longitude");
                outDataInfo.AddVariable("lat", NetCDF4.NcType.NC_DOUBLE, new Dimension[] { latDim });
                outDataInfo.AddVariableAttribute("lat", "units", "degrees_north");
                outDataInfo.AddVariableAttribute("lat", "long_name", "latitude");
                outDataInfo.AddVariable("time", NetCDF4.NcType.NC_DOUBLE, new Dimension[] { timeDim });
                outDataInfo.AddVariableAttribute("time", "units", "days since 1-1-1 00:00:00");
                outDataInfo.AddVariableAttribute("time", "long_name", "time");
                //outDataInfo.AddVariableAttribute("time", "delta_t", "0000-00-01 00:00:00");
                outDataInfo.AddVariable("dust", NetCDF4.NcType.NC_DOUBLE, new Dimension[] { timeDim, latDim, lonDim });
                outDataInfo.AddVariableAttribute("dust", "units", "ug/m-3");
                outDataInfo.AddVariableAttribute("dust", "long_name", "dust concentration");
                outDataInfo.AddVariableAttribute("dust", "missing_value", 9999);
                                
                //Add global attributes
                outDataInfo.AddGlobalAttribute("title", "Asian dust storm forecast");
                outDataInfo.AddGlobalAttribute("model", "CUACE/Dust");
                outDataInfo.AddGlobalAttribute("institute", "Chinese Academy of Meteological Sciences");

                //Creat NetCDF file
                outDataInfo.CreateNCFile(outFile);

                DateTime sTime = DateTime.Parse("0001-1-1 00:00:00");

                for (i = 0; i < fNum; i++)
                {
                    string aFile = _M4Files[i];
                    if (i > 0)
                    {
                        inDataInfo = new MICAPS4DataInfo();
                        inDataInfo.ReadDataInfo(aFile);
                    }
                    else
                    {
                        //Write lon,lat data                              
                        outDataInfo.WriteVar("lon", inDataInfo.X);
                        outDataInfo.WriteVar("lat", inDataInfo.Y);
                    }

                    //Write time data
                    object[] tData = new object[1];
                    tData[0] = inDataInfo.DateTime.Subtract(sTime).TotalDays;
                    int[] start =  new int[1];
                    int[] count = new int[1];
                    start[0] = i;
                    count[0] = 1;
                    outDataInfo.WriteVara ("time", start, count, tData);

                    //Write dust data
                    object[] dustData = new object[inDataInfo.XNum * inDataInfo.YNum];
                    for (int m = 0; m < inDataInfo.YNum; m++)
                    {
                        for (int n = 0; n < inDataInfo.XNum; n++)
                        {
                            dustData[m * inDataInfo.XNum + n] = inDataInfo.GridData[m, n];
                        }
                    }
                    start = new int[3];
                    start[0] = i;
                    start[1] = 0;
                    start[2] = 0;
                    count = new int[3];
                    count[0] = 1;
                    count[1] = latDim.DimLength;
                    count[2] = lonDim.DimLength;
                    outDataInfo.WriteVara("dust", start, count, dustData);                                                           

                    //Set progressbar value
                    toolStripProgressBar1.Value = (int)((double)(i + 1) / (double)fNum * 100);
                    Application.DoEvents();
                }
                outDataInfo.CloseNCFile();

                //Hide progressbar
                toolStripProgressBar1.Visible = false;
                toolStripProgressBar1.Value = 0;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
