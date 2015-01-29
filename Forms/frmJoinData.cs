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
    public partial class frmJoinData : Form
    {
        public frmJoinData()
        {
            InitializeComponent();
        }

        private void B_SelNone_Click(object sender, EventArgs e)
        {
            int aNum = LB_SelectedFiles.Items.Count;
            for (int i = 0; i < aNum; i++)
            {
                LB_SelectedFiles.Items.RemoveAt(0);
            }
        }

        private void B_DataFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog aFBD = new FolderBrowserDialog();
            if (aFBD.ShowDialog() == DialogResult.OK)
            {
                TB_DataFolder.Text = aFBD.SelectedPath;
                LB_AllFiles.Items.Clear();
                string aExtension;
                foreach (string aFile in Directory.GetFiles(TB_DataFolder.Text))
                {
                    aExtension = Path.GetExtension(aFile).ToLower();
                    if (aExtension == ".nc")
                    {
                        LB_AllFiles.Items.Add(Path.GetFileName(aFile));
                    }
                }
            }
        }

        private void frmJoinData_Load(object sender, EventArgs e)
        {
            TB_DataFolder.ReadOnly = true;
            toolStripProgressBar1.Visible = false;
        }

        private void B_Sel_Click(object sender, EventArgs e)
        {
            int i, selNum;
            selNum = LB_AllFiles.SelectedItems.Count;
            for (i = 0; i < selNum; i++)
            {
                if (!LB_SelectedFiles.Items.Contains(LB_AllFiles.SelectedItems[i]))
                {
                    LB_SelectedFiles.Items.Add(LB_AllFiles.SelectedItems[i]);
                }
            }
        }

        private void B_UnSel_Click(object sender, EventArgs e)
        {
            int selNum = LB_SelectedFiles.SelectedItems.Count;
            for (int i = 0; i < selNum; i++)
            {
                LB_SelectedFiles.Items.Remove(LB_SelectedFiles.SelectedItems[0]);
            }
        }

        private void B_SelAll_Click(object sender, EventArgs e)
        {
            int aNum = LB_AllFiles.Items.Count;
            LB_SelectedFiles.Items.Clear();
            for (int i = 0; i < aNum; i++)
            {
                LB_SelectedFiles.Items.Add(LB_AllFiles.Items[i]);
            }
        }

        private void B_Join_Click(object sender, EventArgs e)
        {
            JoinData();
        }

        private void JoinData()
        {
            //Check number of selected files
            int i;
            int fNum = LB_SelectedFiles.Items.Count;
            if (fNum < 2)
            {
                MessageBox.Show("There should be at least two selected files!", "Error");
                return;
            }

            List<string> inFiles = new List<string>();
            for (i = 0; i < fNum; i++)
            {
                string aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
                inFiles.Add(aFile);
            }

            //Join data
            SaveFileDialog aDLG = new SaveFileDialog();
            aDLG.Filter = "NetCDF File (*.nc)|*.nc";
            if (aDLG.ShowDialog() == DialogResult.OK)
            {
                //Show progressbar
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;
                this.Cursor = Cursors.WaitCursor;

                string outFile = aDLG.FileName;

                string tDimName = TB_TimeDimName.Text;
                NetCDFData.JoinDataFiles(inFiles, outFile, tDimName);

                //Hide progressbar
                toolStripProgressBar1.Visible = false;
                toolStripProgressBar1.Value = 0;
                this.Cursor = Cursors.Default;
            }
        }

        //private void JoinData_back()
        //{
        //    //Check number of selected files
        //    int fNum = LB_SelectedFiles.Items.Count;
        //    if (fNum < 2)
        //    {
        //        MessageBox.Show("There should be at least two selected files!", "Error");
        //        return;
        //    }

        //    //Check top two files to decide joining time or variables
        //    string aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[0].ToString());
        //    string bFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[1].ToString());

        //    NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
        //    NetCDFDataInfo bDataInfo = new NetCDFDataInfo();

        //    aDataInfo.ReadDataInfo(aFile);
        //    bDataInfo.ReadDataInfo(bFile);

        //    //If can be joined
        //    int dataJoinType = GetDataJoinType(aDataInfo, bDataInfo);
        //    if (dataJoinType == 0)
        //    {
        //        MessageBox.Show("Data dimensions are not same!", "Error");
        //        return;
        //    }

        //    //Join data
        //    SaveFileDialog aDLG = new SaveFileDialog();
        //    aDLG.Filter = "NetCDF File (*.nc)|*.nc";
        //    if (aDLG.ShowDialog() == DialogResult.OK)
        //    {
        //        //Show progressbar
        //        toolStripProgressBar1.Visible = true;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.WaitCursor;

        //        string outFile = aDLG.FileName;
        //        int i, j, res = 0;
        //        if (dataJoinType == 2)    //Join variables
        //        {
        //            //Copy first file to output file                   
        //            File.Copy(aDataInfo.fileName, outFile, true);

        //            //Open output file
        //            int ncid = 0;
        //            res = NetCDF4.nc_open(outFile, (int)NetCDF4.CreateMode.NC_WRITE, out ncid);
        //            if (res != 0) { goto ERROR; }

        //            //Add data
        //            for (i = 1; i < fNum; i++)
        //            {
        //                aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
        //                bDataInfo = new NetCDFDataInfo();
        //                bDataInfo.ReadDataInfo(aFile);
        //                if (GetDataJoinType(aDataInfo, bDataInfo) != 2)
        //                    continue;

        //                for (j = 0; j < bDataInfo.nvars; j++)
        //                {
        //                    VarStruct aVarS = (VarStruct)bDataInfo.varList[j].Clone();
        //                    if (aVarS.isDataVar && (!aDataInfo.Variables.Contains(aVarS.varName)))
        //                    {
        //                        aDataInfo.AddNewVariable(aVarS, ncid);

        //                        object[] varData = new object[1];
        //                        if (aVarS.DimNumber > 1)
        //                        {                                    
        //                            int[] start = new int[aVarS.DimNumber];
        //                            int[] count = new int[aVarS.DimNumber];
        //                            for (int v = 1; v < aVarS.DimNumber; v++)
        //                            {
        //                                start[v] = 0;
        //                                count[v] = aVarS.Dimensions[v].DimLength;
        //                            }
        //                            for (int d = 0; d < aVarS.Dimensions[0].DimLength; d++)
        //                            {
        //                                start[0] = d;
        //                                count[0] = 1;

        //                                bDataInfo.GetVaraData(bDataInfo.varList[j], start, count, ref varData);
        //                                aDataInfo.WriteVaraData(ncid, aVarS.varid, aVarS.ncType, start, count, varData);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            bDataInfo.GetVarData(bDataInfo.varList[j], ref varData);
        //                            aDataInfo.WriteVarData(ncid, aVarS.varid, aVarS.ncType, varData);
        //                        }
        //                    }
        //                }

        //                //Set progressbar value
        //                toolStripProgressBar1.Value = (int)((double)(i) / (double)(fNum - 1) * 100);
        //                Application.DoEvents();
        //            }

        //            //Close data file
        //            res = NetCDF4.nc_close(ncid);
        //            if (res != 0) { goto ERROR; }
        //        }
        //        else    //Join time
        //        {
        //            //Copy first file to output file                   
        //            File.Copy(aDataInfo.fileName, outFile, true);

        //            //Open output file
        //            int ncid = 0;
        //            res = NetCDF4.nc_open(outFile, (int)NetCDF4.CreateMode.NC_WRITE, out ncid);
        //            if (res != 0) { goto ERROR; }
        //            int tDimid = 0;
        //            NetCDF4.nc_inq_dimid(ncid, "time", out tDimid);

        //            //Add data
        //            for (i = 1; i < fNum; i++)
        //            {
        //                aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
        //                bDataInfo = new NetCDFDataInfo();
        //                bDataInfo.ReadDataInfo(aFile);
        //                if (GetDataJoinType(aDataInfo, bDataInfo) != 1)
        //                    continue;

        //                int tDimNum = 1;
        //                NetCDF4.nc_inq_dimlen(ncid, tDimid, out tDimNum);
        //                for (j = 0; j < bDataInfo.nvars; j++)
        //                {
        //                    VarStruct aVarS = bDataInfo.varList[j];
        //                    if (aVarS.isDataVar || aVarS.varName == "time")
        //                    {
        //                        if (Array.IndexOf(aVarS.dimids, tDimid) < 0)
        //                            continue;

        //                        object[] varData = new object[1];
        //                        bDataInfo.GetVarData(bDataInfo.varList[j], ref varData);

        //                        int[] start = new int[aVarS.DimNumber];
        //                        int[] count = new int[aVarS.DimNumber];
        //                        for (int v = 0; v < aVarS.DimNumber; v++)
        //                        {
        //                            start[v] = 0;
        //                            count[v] = bDataInfo.dimList[aVarS.dimids[v]].DimLength;
        //                        }
        //                        start[Array.IndexOf(aVarS.dimids, tDimid)] = tDimNum;
        //                        bDataInfo.WriteVaraData(ncid, aVarS.varid, aVarS.ncType, start, count, varData);
        //                    }
        //                }

        //                //Set progressbar value
        //                toolStripProgressBar1.Value = (int)((double)(i) / (double)(fNum - 1) * 100);
        //                Application.DoEvents();
        //            }

        //            //Close data file
        //            res = NetCDF4.nc_close(ncid);
        //            if (res != 0) { goto ERROR; }
        //        }

        //        //Hide progressbar
        //        toolStripProgressBar1.Visible = false;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.Default;

        //        return;
        //    ERROR:
        //        MessageBox.Show("Error: " + NetCDF4.nc_strerror(res), "Error");

        //        //Hide progressbar
        //        toolStripProgressBar1.Visible = false;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.Default;

        //        return;
        //    }
        //}

        //private void JoinData_Old()
        //{
        //    //Check number of selected files
        //    int fNum = LB_SelectedFiles.Items.Count;
        //    if (fNum < 2)
        //    {
        //        MessageBox.Show("There should be at least two selected files!", "Error");
        //        return;
        //    }

        //    //Check top two files to decide joining time or variables
        //    string aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[0].ToString());
        //    string bFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[1].ToString());

        //    NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
        //    NetCDFDataInfo bDataInfo = new NetCDFDataInfo();

        //    aDataInfo.ReadDataInfo(aFile);
        //    bDataInfo.ReadDataInfo(bFile);

        //    //If can be joined
        //    int dataJoinType = GetDataJoinType(aDataInfo, bDataInfo);
        //    if (dataJoinType == 0)
        //    {
        //        MessageBox.Show("Data dimensions are not same!", "Error");
        //        return;
        //    }

        //    //Join data
        //    SaveFileDialog aDLG = new SaveFileDialog();
        //    aDLG.Filter = "NetCDF File (*.nc)|*.nc";
        //    if (aDLG.ShowDialog() == DialogResult.OK)
        //    {
        //        //Show progressbar
        //        toolStripProgressBar1.Visible = true;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.WaitCursor;

        //        string outFile = aDLG.FileName;
        //        int i, j, res = 0;
        //        if (dataJoinType == 2)    //Join variables
        //        {
        //            //Copy first file to output file                   
        //            File.Copy(aDataInfo.fileName, outFile, true);

        //            //Open output file
        //            int ncid = 0;
        //            res = NetCDF4.nc_open(outFile, (int)NetCDF4.CreateMode.NC_WRITE, out ncid);
        //            if (res != 0) { goto ERROR; }

        //            //Add data
        //            for (i = 1; i < fNum; i++)
        //            {
        //                aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
        //                bDataInfo = new NetCDFDataInfo();
        //                bDataInfo.ReadDataInfo(aFile);
        //                if (GetDataJoinType(aDataInfo, bDataInfo) != 2)
        //                    continue;

        //                for (j = 0; j < bDataInfo.nvars; j++)
        //                {
        //                    VarStruct aVarS = bDataInfo.varList[j];
        //                    if (aVarS.isDataVar && (!aDataInfo.Variables.Contains(aVarS.varName)))
        //                    {
        //                        object[] varData = new object[1];
        //                        bDataInfo.GetVarData(bDataInfo.varList[j], ref varData);

        //                        aDataInfo.AddNewVariable(aVarS, ncid);
        //                        //int[] start = new int[aVarS.nDims];
        //                        //int[] count = new int[aVarS.nDims];
        //                        //for (int v = 0; v < aVarS.nDims; v++)
        //                        //{
        //                        //    start[v] = 0;
        //                        //    count[v] = bDataInfo.dimList[aVarS.dimids[v]].dimLen;
        //                        //}
        //                        aDataInfo.WriteVarData(ncid, aVarS.varid, aVarS.ncType, varData);
        //                        //bDataInfo.WriteVaraData(ncid, aVarS.varid, aVarS.ncType, start, count, varData);
        //                    }
        //                }

        //                //Set progressbar value
        //                toolStripProgressBar1.Value = (int)((double)(i) / (double)(fNum - 1) * 100);
        //                Application.DoEvents();
        //            }

        //            //Close data file
        //            res = NetCDF4.nc_close(ncid);
        //            if (res != 0) { goto ERROR; }
        //        }
        //        else    //Join time
        //        {
        //            //Copy first file to output file                   
        //            File.Copy(aDataInfo.fileName, outFile, true);

        //            //Open output file
        //            int ncid = 0;
        //            res = NetCDF4.nc_open(outFile, (int)NetCDF4.CreateMode.NC_WRITE, out ncid);
        //            if (res != 0) { goto ERROR; }
        //            int tDimid = 0;
        //            NetCDF4.nc_inq_dimid(ncid, "time", out tDimid);

        //            //Add data
        //            for (i = 1; i < fNum; i++)
        //            {
        //                aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
        //                bDataInfo = new NetCDFDataInfo();
        //                bDataInfo.ReadDataInfo(aFile);
        //                if (GetDataJoinType(aDataInfo, bDataInfo) != 1)
        //                    continue;

        //                int tDimNum = 1;
        //                NetCDF4.nc_inq_dimlen(ncid, tDimid, out tDimNum);
        //                for (j = 0; j < bDataInfo.nvars; j++)
        //                {
        //                    VarStruct aVarS = bDataInfo.varList[j];
        //                    if (aVarS.isDataVar || aVarS.varName == "time")
        //                    {
        //                        if (Array.IndexOf(aVarS.dimids, tDimid) < 0)
        //                            continue;

        //                        object[] varData = new object[1];
        //                        bDataInfo.GetVarData(bDataInfo.varList[j], ref varData);

        //                        int[] start = new int[aVarS.nDims];
        //                        int[] count = new int[aVarS.nDims];
        //                        for (int v = 0; v < aVarS.nDims; v++)
        //                        {
        //                            start[v] = 0;
        //                            count[v] = bDataInfo.dimList[aVarS.dimids[v]].dimLen;
        //                        }
        //                        start[Array.IndexOf(aVarS.dimids, tDimid)] = tDimNum;
        //                        bDataInfo.WriteVaraData(ncid, aVarS.varid, aVarS.ncType, start, count, varData);
        //                    }
        //                }

        //                //Set progressbar value
        //                toolStripProgressBar1.Value = (int)((double)(i) / (double)(fNum - 1) * 100);
        //                Application.DoEvents();
        //            }

        //            //Close data file
        //            res = NetCDF4.nc_close(ncid);
        //            if (res != 0) { goto ERROR; }
        //        }

        //        //Hide progressbar
        //        toolStripProgressBar1.Visible = false;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.Default;

        //        return;
        //    ERROR:
        //        MessageBox.Show("Error: " + NetCDF4.nc_strerror(res), "Error");

        //        //Hide progressbar
        //        toolStripProgressBar1.Visible = false;
        //        toolStripProgressBar1.Value = 0;
        //        this.Cursor = Cursors.Default;

        //        return;
        //    }
        //}

        //private int GetDataJoinType(NetCDFDataInfo aDataInfo, NetCDFDataInfo bDataInfo)
        //{
        //    //If same dimension number
        //    if (aDataInfo.ndims != bDataInfo.ndims)
        //    {                
        //        return 0;  //Can't be joined
        //    }

        //    //If same dimensions
        //    int i;
        //    bool IsSame = true;
        //    bool IsJoinVar = true;
        //    for (i = 0; i < aDataInfo.ndims; i++)
        //    {
        //        Dimension aDim = aDataInfo.dimList[i];
        //        DimStruct bDim = bDataInfo.dimList[i];
        //        if (aDim.dimName != bDim.dimName)
        //        {
        //            IsSame = false;
        //            break;
        //        }
        //        if (aDim.dimName.ToLower() == "time")
        //        {
        //            if (aDim.dimLen != bDim.dimLen)
        //                IsJoinVar = false;
                    
        //            for (int j = 0; j < aDataInfo.times.Count; j++)
        //            {
        //                if (aDataInfo.times[j] != bDataInfo.times[j])
        //                {
        //                    IsJoinVar = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (aDim.dimLen != bDim.dimLen)
        //            {
        //                IsSame = false;
        //                break;
        //            }
        //        }
        //    }
        //    if (!IsSame)
        //    {                
        //        return 0;    //Can't be joined
        //    }

        //    if (IsJoinVar)
        //        return 2;    //Can join variable
        //    else
        //    {
        //        if (aDataInfo.nvars != bDataInfo.nvars)
        //            return 0;

        //        IsSame = true;
        //        for (i = 0; i < aDataInfo.nvars; i++)
        //        {
        //            VarStruct aVarS = aDataInfo.varList[i];
        //            VarStruct bVarS = bDataInfo.varList[i];
        //            if (aVarS.varName != bVarS.varName || aVarS.DimNumber != bVarS.DimNumber)
        //                IsSame = false;
        //        }
        //        if (IsSame)
        //            return 1;    //Can join time
        //        else
        //            return 0;
        //    }
        //}

        private void B_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void B_AddTimeDim_Click(object sender, EventArgs e)
        {
            //Check number of selected files
            int fNum = LB_SelectedFiles.Items.Count;
            if (fNum == 0)
            {
                MessageBox.Show("No selected files!", "Error");
                return;
            }

            //Show progressbar
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = 0;
            this.Cursor = Cursors.WaitCursor;

            //File loop            
            int i, j;
            //NetCDFData CNetCDFData = new NetCDFData();
            NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
            List<string> varList = new List<string>();            
            for (i = 0; i < fNum; i++)
            {
                string aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
                aDataInfo = new NetCDFDataInfo();
                aDataInfo.ReadDataInfo(aFile);
                //CNetCDFData.ReadNetCDFDataInfo(aFile, ref aDataInfo);
                string outFile = Path.Combine(TB_DataFolder.Text, "T_" + LB_SelectedFiles.Items[i].ToString());
                NetCDFDataInfo bDataInfo = (NetCDFDataInfo)aDataInfo.Clone();
                //bDataInfo.CreatNCFile(outFile);

                //Check variables if time included
                varList.Clear();
                for (j = 0; j < aDataInfo.Variables.Count; j++)
                {
                    varList.Add(aDataInfo.Variables[j].Name.ToLower());
                }
                if (varList.Contains("time"))
                {
                    continue;
                }

                //Read time of the data
                string timeStr = LB_SelectedFiles.Items[i].ToString();
                timeStr = timeStr.Split('.')[1].TrimStart('A');
                int days = int.Parse(timeStr.Substring(4));
                DateTime aTime = DateTime.Parse(timeStr.Substring(0, 4) + "-01-01");
                aTime = aTime.AddDays(days - 1);
                DateTime sTime = DateTime.Parse("0001-1-1 00:00:00");

                //Set data info, Add time dimension and variable
                Dimension tDim = new Dimension();
                tDim.DimName = "time";
                tDim.DimLength = 1;
                tDim.DimId = bDataInfo.ndims;
                bDataInfo.AddDimension(tDim);

                Variable tVar = new Variable();
                List<AttStruct> attList = new List<AttStruct>();
                AttStruct aAtts = new AttStruct();
                aAtts.attName = "units";
                aAtts.NCType = NetCDF4.NcType.NC_CHAR;
                aAtts.attValue = "days since 1-1-1 00:00:00";
                aAtts.attLen = ((string)aAtts.attValue).Length;
                attList.Add(aAtts);
                aAtts = new AttStruct();
                aAtts.attName = "long_name";
                aAtts.NCType = NetCDF4.NcType.NC_CHAR;
                aAtts.attValue = "Time";
                aAtts.attLen = ((string)aAtts.attValue).Length;
                attList.Add(aAtts);
                aAtts = new AttStruct();
                aAtts.attName = "delta_t";
                aAtts.NCType = NetCDF4.NcType.NC_CHAR;
                aAtts.attValue = "0000-00-01 00:00:00";
                aAtts.attLen = ((string)aAtts.attValue).Length;
                attList.Add(aAtts);
                tVar.Attributes = attList;
                tVar.Dimensions.Add(tDim);               
                tVar.AttNumber = attList.Count;
                tVar.NCType = NetCDF4.NcType.NC_DOUBLE;
                //tVar.nDims = 1;
                tVar.VarId = bDataInfo.nvars;
                tVar.Name = "time";
                //tVar.isDataVar = false;
                bDataInfo.AddVariable(tVar);
                for (j = 0; j < bDataInfo.Variables.Count; j++)
                {
                    Variable aVarS = bDataInfo.Variables[j];
                    if (aVarS.DimNumber > 1)
                    {
                        aVarS.Dimensions.Add(tDim);
                        for (int d = 0; d < aVarS.DimNumber; d++)
                            aVarS.Dimensions.Add(aVarS.Dimensions[d]);
                        
                        //aVarS.nDims += 1;
                        bDataInfo.Variables[j] = aVarS;
                    }
                }

                //Get and set data array
                object[] dataArray = new object[0];
                int cLen = 0;
                for (j = 0; j < aDataInfo.nvars; j++)
                {
                    object[] varData = new object[1];
                    if (aDataInfo.GetVarData(aDataInfo.Variables[j], ref varData))
                    {
                        Array.Resize(ref dataArray, cLen + varData.Length);
                        Array.Copy(varData, 0, dataArray, cLen, varData.Length);
                        cLen = dataArray.Length;
                    }
                }
                Array.Resize(ref dataArray, cLen + 1);
                dataArray[cLen] = (aTime - sTime).Days;

                //Write NetCDF data file                                
                bDataInfo.WriteNetCDFData(outFile, dataArray);
                //CNetCDFData.WriteNetCDFData(outFile, bDataInfo, dataArray);
                
                //Set progressbar value
                toolStripProgressBar1.Value = (int)((double)(i + 1) / (double)fNum * 100);
                Application.DoEvents();
            }

            //Hide progressbar
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Value = 0;
            this.Cursor = Cursors.Default;                        
        }

        //private void Backup(int fNum)
        //{
        //    //File loop            
        //    int i, j;
        //    NetCDFData CNetCDFData = new NetCDFData();
        //    NetCDFDataInfo aDataInfo = new NetCDFDataInfo();
        //    List<string> varList = new List<string>();
        //    int res = 0;
        //    for (i = 0; i < fNum; i++)
        //    {
        //        string aFile = Path.Combine(TB_DataFolder.Text, LB_SelectedFiles.Items[i].ToString());
        //        aDataInfo = new NetCDFDataInfo();
        //        CNetCDFData.ReadNetCDFDataInfo(aFile, ref aDataInfo);

        //        //Check variables if time included
        //        varList.Clear();
        //        for (j = 0; j < aDataInfo.varList.Count; j++)
        //        {
        //            varList.Add(aDataInfo.varList[j].varName.ToLower());
        //        }
        //        if (varList.Contains("time"))
        //        {
        //            continue;
        //        }

        //        //Read time of the data
        //        string timeStr = LB_SelectedFiles.Items[i].ToString();
        //        timeStr = timeStr.Split('.')[1].TrimStart('A');
        //        int days = int.Parse(timeStr.Substring(4));
        //        DateTime aTime = DateTime.Parse(timeStr.Substring(0, 4) + "-01-01");
        //        aTime = aTime.AddDays(days - 1);
        //        DateTime sTime = DateTime.Parse("0001-1-1 00:00:00");

        //        //Write NetCDF data file
        //        int ncid, dimid;
        //        string outFile = Path.Combine(TB_DataFolder.Text, "T_" + LB_SelectedFiles.Items[i].ToString());
        //        res = NetCDF4.nc_create(outFile, (int)NetCDF4.CreateMode.NC_CLOBBER, out ncid);
        //        if (res != 0) { goto ERROR; }

        //        //Define dimensions  
        //        int londimId, latdimId, timedimId, lonId, latId, timeId;
        //        londimId = 0;
        //        latdimId = 1;
        //        timedimId = 2;
        //        lonId = 0;
        //        latId = 1;
        //        timeId = 2;
        //        int[] dimids = new int[aDataInfo.ndims + 1];
        //        for (j = 0; j < aDataInfo.ndims; j++)
        //        {
        //            res = NetCDF4.nc_def_dim(ncid, aDataInfo.dimList[j].dimName,
        //                aDataInfo.dimList[j].dimLen, out dimid);
        //            if (res != 0) { goto ERROR; }

        //            switch (aDataInfo.dimList[j].dimName.ToLower())
        //            {
        //                case "lon":
        //                    londimId = dimid;
        //                    break;
        //                case "lat":
        //                    latdimId = dimid;
        //                    break;
        //            }
        //        }
        //        res = NetCDF4.nc_def_dim(ncid, "time", NetCDF4.NC_UNLIMITED, out dimid);
        //        if (res != 0) { goto ERROR; }
        //        timedimId = dimid;
        //        dimids[0] = timedimId;
        //        dimids[1] = latdimId;
        //        dimids[2] = londimId;

        //        //Define variables
        //        List<VarStruct> newVarList = new List<VarStruct>();
        //        int varid;
        //        res = NetCDF4.nc_def_var(ncid, "time", (int)NetCDF4.NcType.NC_DOUBLE,
        //            1, new int[1] { timedimId }, out timeId);
        //        for (j = 0; j < aDataInfo.varList.Count; j++)
        //        {
        //            VarStruct aVarS = new VarStruct();
        //            aVarS.attList = aDataInfo.varList[j].attList;
        //            aVarS.dimids = aDataInfo.varList[j].dimids;
        //            aVarS.nAtts = aDataInfo.varList[j].nAtts;
        //            aVarS.ncType = aDataInfo.varList[j].ncType;
        //            aVarS.nDims = aDataInfo.varList[j].nDims;
        //            aVarS.varid = aDataInfo.varList[j].varid;
        //            aVarS.varName = aDataInfo.varList[j].varName;
        //            switch (aVarS.varName.ToLower())
        //            {
        //                case "lon":
        //                    res = NetCDF4.nc_def_var(ncid, "lon", (int)NetCDF4.NcType.NC_FLOAT,
        //                        1, new int[1] { londimId }, out varid);
        //                    lonId = varid;
        //                    break;
        //                case "lat":
        //                    res = NetCDF4.nc_def_var(ncid, "lat", (int)NetCDF4.NcType.NC_FLOAT,
        //                        1, new int[1] { latdimId }, out varid);
        //                    latId = varid;
        //                    break;
        //                default:
        //                    res = NetCDF4.nc_def_var(ncid, aVarS.varName, (int)aVarS.ncType,
        //                        aVarS.nDims + 1, dimids, out varid);
        //                    break;
        //            }
        //            aVarS.varid = varid;
        //            newVarList.Add(aVarS);
        //        }

        //        //Write attribute data                
        //        foreach (AttStruct aAttS in aDataInfo.gAttList)
        //        {
        //            CNetCDFData.WriteAtt(ncid, NetCDF4.NC_GLOBAL, aAttS);
        //        }
        //        for (j = 0; j < newVarList.Count; j++)
        //        {
        //            foreach (AttStruct aAttS in newVarList[j].attList)
        //            {
        //                CNetCDFData.WriteAtt(ncid, newVarList[j].varid, aAttS);
        //            }
        //        }
        //        List<AttStruct> attList = new List<AttStruct>();
        //        AttStruct aAtts = new AttStruct();
        //        aAtts.attName = "units";
        //        aAtts.ncType = NetCDF4.NcType.NC_CHAR;
        //        aAtts.attValue = "days since 1-1-1 00:00:00";
        //        aAtts.attLen = ((string)aAtts.attValue).Length;
        //        attList.Add(aAtts);
        //        aAtts = new AttStruct();
        //        aAtts.attName = "long_name";
        //        aAtts.ncType = NetCDF4.NcType.NC_CHAR;
        //        aAtts.attValue = "Time";
        //        aAtts.attLen = ((string)aAtts.attValue).Length;
        //        attList.Add(aAtts);
        //        aAtts = new AttStruct();
        //        aAtts.attName = "delta_t";
        //        aAtts.ncType = NetCDF4.NcType.NC_CHAR;
        //        aAtts.attValue = "0000-00-01 00:00:00";
        //        aAtts.attLen = ((string)aAtts.attValue).Length;
        //        attList.Add(aAtts);
        //        foreach (AttStruct aAttS in attList)
        //        {
        //            CNetCDFData.WriteAtt(ncid, timeId, aAttS);
        //        }

        //        //End define
        //        res = NetCDF4.nc_enddef(ncid);
        //        if (res != 0) { goto ERROR; }

        //        //Write variable data
        //        double[] aData;
        //        int[] start = new int[3];
        //        int[] count = new int[3];
        //        int xNum = aDataInfo.X.Length;
        //        int yNum = aDataInfo.Y.Length;
        //        for (j = 0; j < 3; j++)
        //        {
        //            start[j] = 0;
        //            count[j] = 1;
        //        }
        //        count[0] = 1;
        //        count[1] = yNum;
        //        count[2] = xNum;
        //        aData = new double[1];
        //        aData[0] = (aTime - sTime).Days;
        //        //CNetCDFData.WriteOneDimVarData(ncid, timeId, 1, NetCDF4.NcType.NC_DOUBLE, aData);
        //        NetCDF4.nc_put_vara_double(ncid, timeId, new int[1] { 0 }, new int[1] { 1 }, aData);
        //        for (j = 0; j < newVarList.Count; j++)
        //        {
        //            switch (newVarList[j].varName.ToLower())
        //            {
        //                case "lon":
        //                    CNetCDFData.WriteOneDimVarData(ncid, lonId, xNum, NetCDF4.NcType.NC_FLOAT, aDataInfo.X);
        //                    break;
        //                case "lat":
        //                    CNetCDFData.WriteOneDimVarData(ncid, latId, yNum, NetCDF4.NcType.NC_FLOAT, aDataInfo.Y);
        //                    break;
        //                default:
        //                    double[] dp = new double[yNum * xNum];
        //                    res = NetCDF4.nc_get_var_double(aDataInfo.ncid, aDataInfo.varList[j].varid, dp);

        //                    //NetCDF4.nc_put_var_double(ncid, newVarList[j].varid, dp);
        //                    NetCDF4.nc_put_vara_double(ncid, newVarList[j].varid, start, count, dp);
        //                    break;
        //            }
        //        }

        //        //Close data file
        //        res = NetCDF4.nc_close(aDataInfo.ncid);
        //        if (res != 0) { goto ERROR; }

        //        res = NetCDF4.nc_close(ncid);
        //        if (res != 0) { goto ERROR; }

        //        toolStripProgressBar1.Value = (int)((double)(i + 1) / (double)fNum * 100);
        //        Application.DoEvents();
        //    }

        //    //Hide progressbar
        //    toolStripProgressBar1.Visible = false;
        //    toolStripProgressBar1.Value = 0;
        //    this.Cursor = Cursors.Default;

        //    return;

        //ERROR:
        //    MessageBox.Show("Error: " + NetCDF4.nc_strerror(res), "Error");
        //    //Hide progressbar
        //    toolStripProgressBar1.Visible = false;
        //    toolStripProgressBar1.Value = 0;
        //    this.Cursor = Cursors.Default;
        //}

    }
}
