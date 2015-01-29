using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfoC.Data.MeteoData;

namespace MeteoInfo.Forms
{
    public partial class frmGridSet : Form
    {
        public frmGridSet()
        {
            InitializeComponent();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmGridSet_Load(object sender, EventArgs e)
        {

        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void SetParameters(GridDataSetting aGDP)
        {
            TB_MinX.Text = aGDP.DataExtent.minX.ToString();
            TB_MaxX.Text = aGDP.DataExtent.maxX.ToString();
            TB_MinY.Text = aGDP.DataExtent.minY.ToString();
            TB_MaxY.Text = aGDP.DataExtent.maxY.ToString();
            TB_XNum.Text = aGDP.XNum.ToString();
            TB_YNum.Text = aGDP.YNum.ToString();
            SetXYSize();            
        }

        public void GetParameters(ref GridDataSetting aGDP)
        {
            Single minX, maxX, minY, maxY;
            minX = Convert.ToSingle(TB_MinX.Text);
            maxX = Convert.ToSingle(TB_MaxX.Text);
            minY = Convert.ToSingle(TB_MinY.Text);
            maxY = Convert.ToSingle(TB_MaxY.Text);  
          
            aGDP.DataExtent.minX = minX;
            aGDP.DataExtent.maxX = maxX;
            aGDP.DataExtent.minY = minY;
            aGDP.DataExtent.maxY = maxY;
            aGDP.XNum = Convert.ToInt32(TB_XNum.Text);
            aGDP.YNum = Convert.ToInt32(TB_YNum.Text);                        
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

        
    }
}
