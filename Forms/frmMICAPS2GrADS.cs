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
    public partial class frmMICAPS2GrADS : Form
    {
        public frmMICAPS2GrADS()
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

        private void frmMICAPS2GrADS_Load(object sender, EventArgs e)
        {
            TB_DataFolder.ReadOnly = true;
            toolStripProgressBar1.Visible = false;

            string[] items = new string[] {"CloudCover","WindDirection","WindSpeed","Pressure",
                "PressVar3h","WeatherPast1","WeatherPast2","Precipitation6h", "LowCloudShape",
                "LowCloudAmount","LowCloudHeight","DewPoint","Visibility","WeatherNow",
                "Temperature","MiddleCloudShape","HighCloudShape"};
            CLB_Variables.Items.AddRange(items);

            TB_Increment.Text = "3";
            CB_Increment.Items.Clear();
            CB_Increment.Items.Add("hr");
            CB_Increment.Items.Add("dy");
            CB_Increment.SelectedIndex = 0;

            DTP_Start.Value = DateTime.Parse("2008-01-01 02:00");
            DTP_End.Value = DateTime.Parse("2008-01-31 23:00");

            B_SelAll.PerformClick();
        }

        private void B_SelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLB_Variables.Items.Count; i++)
            {
                CLB_Variables.SetItemChecked(i, true);
            }
        }

        private void B_SelNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLB_Variables.Items.Count; i++)
            {
                CLB_Variables.SetItemChecked(i, false);
            }
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

            //Check variable list
            if (CLB_Variables.CheckedItems.Count == 0)
            {
                MessageBox.Show("No variable was selected!", "Error");
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

            //Set output file
            SaveFileDialog aDLG = new SaveFileDialog();
            aDLG.Filter = "GrADS binary (*.bin)|*.bin";
            if (aDLG.ShowDialog() == DialogResult.OK)
            {
                string outFile = aDLG.FileName;
                
                FileStream fs = new FileStream(outFile, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                List<int> varIdxList = new List<int>();
                List<string> varNameList = new List<string>();
                for (int i = 0; i < CLB_Variables.CheckedItems.Count; i++)
                {
                    varIdxList.Add(CLB_Variables.Items.IndexOf(CLB_Variables.CheckedItems[i]));
                    varNameList.Add(CLB_Variables.CheckedItems[i].ToString());
                }

                //Time loop
                int tIncrement = int.Parse(TB_Increment.Text);
                string incrementUnit = CB_Increment.Text;
                DateTime aTime = sTime;
                string inFile;
                int fNum = 0;
                MICAPSData CMICAPSData = new MICAPSData();
                MICAPS1DataInfo aMDataInfo = new MICAPS1DataInfo();
                GrADSDataInfo CGrADSData = new GrADSDataInfo();
                int timeNum = 0;
                while (aTime <= eTime)
                {
                    inFile = Path.Combine(dataFolder, aTime.ToString("yyMMddHH") + ".000");
                    if (File.Exists(inFile))
                    {
                        fNum += 1;
                        aMDataInfo = CMICAPSData.ReadMicaps1(inFile);
                        CGrADSData.WriteGrADSStationData(bw, aMDataInfo, varIdxList);
                    }
                    else
                    {
                        CGrADSData.WriteGrADSStationDataNull(bw);
                    }

                    if (incrementUnit == "hr")
                    {
                        aTime = aTime.AddHours(tIncrement);
                    }
                    else
                    {
                        aTime = aTime.AddDays(tIncrement);
                    }
                    timeNum += 1;
                }

                bw.Close();
                fs.Close();

                //Write ctl file

                CGrADSData.WriteGrADSCtlFile_Station(outFile, varNameList, aMDataInfo.MissingValue,
                    tIncrement.ToString() + incrementUnit, sTime, timeNum);
            }
        }
    }
}
