using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfo.Classes;
using MeteoInfoC.Data.MeteoData;

namespace MeteoInfo.Forms
{
    public partial class frmInterpolate : Form
    {
        private InterpolationSetting _GridInter = new InterpolationSetting();

        public frmInterpolate()
        {
            InitializeComponent();
        }
                
        private void frmInterpolate_Load(object sender, EventArgs e)
        {

        }

        public void SetParameters(InterpolationSetting GI)
        {
            _GridInter = GI;

            TB_MinX.Text = GI.GridDataSet.DataExtent.minX.ToString();
            TB_MaxX.Text = GI.GridDataSet.DataExtent.maxX.ToString();
            TB_MinY.Text = GI.GridDataSet.DataExtent.minY.ToString();
            TB_MaxY.Text = GI.GridDataSet.DataExtent.maxY.ToString();
            TB_XNum.Text = GI.GridDataSet.XNum.ToString();
            TB_YNum.Text = GI.GridDataSet.YNum.ToString();
            SetXYSize();

            CB_Method.Items.Add(InterpolationMethods.IDW_Radius.ToString());
            CB_Method.Items.Add(InterpolationMethods.IDW_Neighbors.ToString());
            CB_Method.Items.Add(InterpolationMethods.Cressman.ToString());
            CB_Method.Items.Add(InterpolationMethods.AssignPointToGrid.ToString());
            CB_Method.Text = GI.InterpolationMethod.ToString();
            switch (GI.InterpolationMethod)
            {
                case InterpolationMethods.Cressman:
                    string radStr = "";
                    for (int i = 0; i < GI.RadList.Count; i++)
                        radStr = radStr + GI.RadList[i].ToString() + ";";

                    radStr = radStr.TrimEnd(';');
                    TB_Radius.Text = radStr;
                    TB_MinNum.Text = GI.MinPointNum.ToString();
                    TB_UndefData.Text = GI.UnDefData.ToString();
                    TB_UndefData.ReadOnly = true;
                    break;
                case InterpolationMethods.IDW_Neighbors:
                case InterpolationMethods.IDW_Radius:
                    TB_Radius.Text = GI.Radius.ToString();
                    TB_MinNum.Text = GI.MinPointNum.ToString();
                    TB_UndefData.Text = GI.UnDefData.ToString();
                    TB_UndefData.ReadOnly = true;
                    break;
                case InterpolationMethods.AssignPointToGrid:

                    break;
            }            
        }

        public void GetParameters(ref InterpolationSetting GI)
        {
            Single minX, maxX, minY, maxY;                        
            minX = Convert.ToSingle(TB_MinX.Text);
            maxX = Convert.ToSingle(TB_MaxX.Text);
            minY = Convert.ToSingle(TB_MinY.Text);
            maxY = Convert.ToSingle(TB_MaxY.Text);
            GridDataSetting aGDP = new GridDataSetting();
            aGDP.DataExtent.minX = minX;
            aGDP.DataExtent.maxX = maxX;
            aGDP.DataExtent.minY = minY;
            aGDP.DataExtent.maxY = maxY;
            aGDP.XNum = Convert.ToInt32(TB_XNum.Text);
            aGDP.YNum = Convert.ToInt32(TB_YNum.Text);
            GI.GridDataSet = aGDP;

            GI.InterpolationMethod = (InterpolationMethods)Enum.Parse(typeof(InterpolationMethods),
                CB_Method.Text, true);
            switch (GI.InterpolationMethod)
            {
                case InterpolationMethods.Cressman:
                    if (TB_Radius.Text.Trim() != string.Empty)
                    {
                        string[] radStrs = TB_Radius.Text.Split(';');
                        GI.RadList = new List<double>();
                        for (int i = 0; i < radStrs.Length; i++)
                        {
                            GI.RadList.Add(double.Parse(radStrs[i]));
                        }
                    }
                    else
                        GI.RadList = new List<double>();

                    GI.MinPointNum = Convert.ToInt32(TB_MinNum.Text);
                    break;
                case InterpolationMethods.IDW_Neighbors:
                case InterpolationMethods.IDW_Radius:
                    GI.Radius = double.Parse(TB_Radius.Text);
                    GI.MinPointNum = Convert.ToInt32(TB_MinNum.Text);
                    break;
            }            
        }

        private void SetXYNum()
        {
            Single minX, maxX, minY, maxY;
            Single XSize, YSize;
            int XNum, YNum;
            minX = Convert.ToSingle(TB_MinX.Text);
            maxX = Convert.ToSingle(TB_MaxX.Text);
            minY = Convert.ToSingle(TB_MinY.Text);
            maxY = Convert.ToSingle(TB_MaxY.Text);
            XSize = Convert.ToSingle(TB_XSize.Text);
            YSize = Convert.ToSingle(TB_YSize.Text);

            XNum = Convert.ToInt32((maxX - minX) / XSize);
            YNum = Convert.ToInt32((maxY - minY) / YSize);

            maxX = minX + XNum * XSize;
            maxY = minY + YNum * YSize;

            XNum += 1;
            YNum += 1;
            TB_XNum.Text = XNum.ToString();
            TB_YNum.Text = YNum.ToString();

            TB_MaxX.Text = maxX.ToString();
            TB_MaxY.Text = maxY.ToString();
        }

        private void SetXYSize()
        {
            Single minX, maxX, minY, maxY;
            Single XSize, YSize;
            int XNum, YNum;
            minX = Convert.ToSingle(TB_MinX.Text);
            maxX = Convert.ToSingle(TB_MaxX.Text);
            minY = Convert.ToSingle(TB_MinY.Text);
            maxY = Convert.ToSingle(TB_MaxY.Text);
            XNum = Convert.ToInt32(TB_XNum.Text);
            YNum = Convert.ToInt32(TB_YNum.Text);

            XSize = (maxX - minX) / (XNum - 1);
            YSize = (maxY - minY) / (YNum - 1);
            TB_XSize.Text = XSize.ToString();
            TB_YSize.Text = YSize.ToString();
        }

        private void TB_XSize_Validated(object sender, EventArgs e)
        {
            SetXYNum();
        }

        private void TB_YSize_Validated(object sender, EventArgs e)
        {
            SetXYNum();
        }

        private void TB_MinX_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void TB_MaxX_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void TB_MinY_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void TB_MaxY_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void TB_XNum_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void TB_YNum_Validated(object sender, EventArgs e)
        {
            SetXYSize();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CB_Method_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((InterpolationMethods)Enum.Parse(typeof(InterpolationMethods),
                CB_Method.Text, true))
            {
                case InterpolationMethods.IDW_Radius:
                    TB_Radius.Enabled = true;
                    TB_MinNum.Enabled = true;
                    TB_Radius.Text = _GridInter.Radius.ToString();                    
                    break;
                case InterpolationMethods.IDW_Neighbors:
                    TB_Radius.Enabled = false;
                    TB_MinNum.Enabled = true;
                    TB_Radius.Text = _GridInter.Radius.ToString();
                    break;
                case InterpolationMethods.Cressman:
                    TB_Radius.Enabled = true;
                    TB_MinNum.Enabled = false;
                    string radStr = "";
                    for (int i = 0; i < _GridInter.RadList.Count; i++)
                        radStr = radStr + _GridInter.RadList[i].ToString() + ";";

                    radStr = radStr.TrimEnd(';');
                    TB_Radius.Text = radStr;
                    break;
            }
        }
    }
}
