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
    public partial class frmOMIAI2GrADS : Form
    {
        public frmOMIAI2GrADS()
        {
            InitializeComponent();
        }

        private void B_DataFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog aFBD = new FolderBrowserDialog();
            if (aFBD.ShowDialog() == DialogResult.OK)
            {
                TB_DataFolder.Text = aFBD.SelectedPath;
            }
        }

        private void frmOMIAI2GrADS_Load(object sender, EventArgs e)
        {
            TB_DataFolder.ReadOnly = true;
            toolStripProgressBar1.Visible = false;            

            TB_Increment.Text = "1";
            CB_Increment.Items.Clear();
            CB_Increment.Items.Add("dy");
            CB_Increment.SelectedIndex = 0;

            DTP_Start.Value = DateTime.Parse("2008-01-01");
            DTP_End.Value = DateTime.Parse("2008-12-31");
        }

        private void B_Convert_Click(object sender, EventArgs e)
        {
            //Check data folder
            string dataFolder = TB_DataFolder.Text;
            if (!Directory.Exists(dataFolder))
            {
                MessageBox.Show("The data folder is not exist!" + Environment.NewLine +
                    dataFolder, "Error");
                return;
            }

            //Check start/end time
            DateTime sTime = DTP_Start.Value;
            DateTime eTime = DTP_End.Value;
            if (eTime < sTime)
            {
                MessageBox.Show("End time is early than start time!", "Error");
                return;
            }
            int tIncrement = int.Parse(TB_Increment.Text);
            string incrementUnit = CB_Increment.Text;
            int timeNum = (eTime - sTime).Days + 1;

            //Set output file
            SaveFileDialog aDLG = new SaveFileDialog();
            aDLG.Filter = "GrADS Control File (*.ctl)|*.ctl";
            if (aDLG.ShowDialog() == DialogResult.OK)
            {
                string cFile = aDLG.FileName;
                string dFile = Path.ChangeExtension(cFile, ".dat");
               
                GrADSDataInfo aDataInfo = new GrADSDataInfo();                
                aDataInfo.FileName = cFile;
                aDataInfo.DSET = dFile;
                aDataInfo.TITLE = "OMI AI data";
                aDataInfo.DTYPE = "GRIDDED";
                aDataInfo.XDEF.Type = "LINEAR";
                aDataInfo.XDEF.XNum = 360;
                aDataInfo.XDEF.XMin = -179.5f;
                aDataInfo.XDEF.XDelt = 1.0f;
                aDataInfo.YDEF.Type = "LINEAR";
                aDataInfo.YDEF.YNum = 180;
                aDataInfo.YDEF.YMin = -89.5f;
                aDataInfo.YDEF.YDelt = 1.0f;
                aDataInfo.ZDEF.Type = "LINEAR";
                aDataInfo.ZDEF.ZNum = 1;
                aDataInfo.ZDEF.SLevel = 1;
                aDataInfo.ZDEF.ZDelt = 1;
                aDataInfo.TDEF.Type = "LINEAR";
                aDataInfo.TDEF.TNum = timeNum;
                aDataInfo.TDEF.STime = sTime;
                aDataInfo.TDEF.TDelt = "1dy";
                Variable aVar = new Variable();
                aVar.Name = "AI";
                //aVar.LevelNum = 0;
                aVar.Units = "99";
                aVar.Description = "OMI AI";
                aDataInfo.VARDEF.AddVar(aVar);
                aDataInfo.MissingValue = -999;

                FileStream fs = new FileStream(dFile, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                //Time loop                
                DateTime aTime = sTime;
                string inFile;
                int fNum = 0;                                               
                while (aTime.Date <= eTime.Date)
                {
                    inFile = Path.Combine(dataFolder, "L3_aersl_omi_" + aTime.ToString("yyyyMMdd") + ".txt");
                    if (File.Exists(inFile))
                    {
                        fNum += 1;

                        //Read grid data from OMI AI data file
                        double[,] gridData = new double[180, 360];
                        StreamReader sr = new StreamReader(inFile);
                        sr.ReadLine();
                        sr.ReadLine();
                        sr.ReadLine();
                        string aLine = sr.ReadLine();
                        string dataStr = string.Empty;
                        int yNum = 0;
                        while (aLine != null)
                        {
                            aLine = aLine.Substring(1);
                            dataStr = dataStr + aLine;
                            if (aLine.Contains("lat"))
                            {                                
                                for (int i = 0; i < 360; i++)
                                {
                                    string dStr = dataStr.Substring(i * 3, 3);
                                    if (dStr == "888" || dStr == "999")
                                        gridData[yNum, i] = aDataInfo.MissingValue;
                                    else
                                        gridData[yNum, i] = double.Parse(dStr) / 10;
                                }
                                dataStr = string.Empty;
                                yNum++;
                            }

                            aLine = sr.ReadLine();
                        }

                        sr.Close();

                        //Write grid data to GrADS data file
                        aDataInfo.WriteGrADSData_Grid(bw, gridData);
                    }
                    else
                    {
                        aDataInfo.WriteGrADSData_Grid_Null(bw);
                    }

                    aTime = aTime.AddDays(tIncrement);           
                }

                bw.Close();
                fs.Close();

                //Write ctl file
                aDataInfo.WriteGrADSCTLFile();
            }
        }

    }
}
