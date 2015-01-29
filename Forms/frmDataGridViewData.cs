using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MeteoInfoC.Data;
using MeteoInfo.Classes;
using MeteoInfoC.Projections;
using MeteoInfoC.Data.MeteoData;
using ZedGraph;

namespace MeteoInfo.Forms
{
    public partial class frmDataGridViewData : Form
    {
        public double MissingValue;
        public string DataType;
        public object Data;
        public ProjectionInfo ProjInfo;
        private string[] _DataTitleList;

        public frmDataGridViewData()
        {
            InitializeComponent();
        }

        private void frmDataGridView_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            TSB_Stat.Enabled = false;
            TSB_Chart.Enabled = false;
            switch (DataType)
            {
                case "GridData":
                    TSB_ToStation.Enabled = true;
                    break;
                default:
                    TSB_ToStation.Enabled = false;
                    break;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TSB_Chart.Enabled = false;
            TSB_Stat.Enabled = false;                   
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TSB_Chart.Enabled = true;
            TSB_Stat.Enabled = true;               
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string aNewName;
            frmInputBox aInputBox = new frmInputBox("Please input new column name：", "Change colunn name", dataGridView1.Columns[e.ColumnIndex].HeaderText);
            if (aInputBox.ShowDialog() == DialogResult.OK)
            {
                aNewName = aInputBox.Value;
                if (!(aNewName == string.Empty))
                {
                    dataGridView1.Columns[e.ColumnIndex].HeaderText = aNewName;
                    dataGridView1.Refresh();
                }
            }
        }

        private void TSB_Save_Click(object sender, EventArgs e)
        {
            switch (DataType)
            {
                case "GridData":
                    SaveFile_GridData();
                    break;
                case "StationData":
                    SaveFile_StationData();
                    break;
                case "PointData":
                    SaveFile_PointData();
                    break;
            }
        }

        private void SaveFile_StationData()
        {
            string nfileName;
            SaveFileDialog myDlg = new SaveFileDialog();            
            string aStr;
            int i, j;
            
            myDlg.Filter = "CSV File (*.csv)|*.csv|Text File (*.txt)|*.txt";           
            if (myDlg.ShowDialog() == DialogResult.OK)
            {                
                // Write file
                nfileName = myDlg.FileName;
                StreamWriter sw = new StreamWriter(nfileName);

                aStr = "";
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                    aStr = aStr + dataGridView1.Columns[j].Name.Trim() + ",";

                aStr = aStr.TrimEnd(',');
                sw.WriteLine(aStr);
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    //Not save undefine data
                    if (double.Parse(dataGridView1[3, i].Value.ToString()) == MissingValue)
                        continue;

                    aStr = "";
                    for (j = 0; j < dataGridView1.ColumnCount; j++)
                        aStr = aStr + dataGridView1[j, i].Value.ToString().Trim() + ",";

                    aStr = aStr.TrimEnd(',');
                    sw.WriteLine(aStr);                    
                }

                sw.Close();
            }
        }

        private void SaveFile_PointData()
        {
            string nfileName;
            SaveFileDialog myDlg = new SaveFileDialog();
            string aStr;
            int i, j;

            myDlg.Filter = "CSV File (*.csv)|*.csv|Text File (*.txt)|*.txt";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                // Write file
                nfileName = myDlg.FileName;
                StreamWriter sw = new StreamWriter(nfileName);

                aStr = "";
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                    aStr = aStr + dataGridView1.Columns[j].Name.Trim() + ",";

                aStr = aStr.TrimEnd(',');
                sw.WriteLine(aStr);
                for (i = 0; i < dataGridView1.RowCount; i++)
                {                    
                    aStr = "";
                    for (j = 0; j < dataGridView1.ColumnCount; j++)
                        aStr = aStr + dataGridView1[j, i].Value.ToString().Trim() + ",";

                    aStr = aStr.TrimEnd(',');
                    sw.WriteLine(aStr);
                }

                sw.Close();
            }
        }

        private void SaveFile_GridData()
        {
            SaveFileDialog myDlg = new SaveFileDialog();
            myDlg.Filter = "Surfer ASCII File (*.grd)|*.grd";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                ((GridData)Data).SaveAsSurferASCIIFile(myDlg.FileName);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView1.RowHeadersWidth - 4,
                e.RowBounds.Height);

            switch (DataType)
            {
                case "GridData":
                    TextRenderer.DrawText(e.Graphics, (dataGridView1.RowCount - e.RowIndex - 1).ToString(),
                        dataGridView1.RowHeadersDefaultCellStyle.Font,
                        rectangle,
                        dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                    break;
                default:
                    TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                        dataGridView1.RowHeadersDefaultCellStyle.Font,
                        rectangle,
                        dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                    break;
            }            
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataGridView.HitTestInfo hti = dataGridView1.HitTest(e.X, e.Y);

            if (hti.ColumnIndex == -1 && hti.RowIndex >= 0)
            {
                // row header click
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
            }

            else if (hti.RowIndex == -1 && hti.ColumnIndex >= 0)
            {
                // column header click
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                }
            }
        }

        private void TSB_Stat_Click(object sender, EventArgs e)
        {
            Statistic();
        }

        private void Statistic()
        {
            int aColumnIndex;
            //frmDataGridView aDGVForm = new frmDataGridView();
            int i, c;
            string aDateStr;
            List<double> aArrayList = new List<double>();
            List<string> aNameList = new List<string>();
            double[] aMax, aMin, aMean, aMedian, Q1, Q3, aStdDev;
            int[] aCount;
            int colNum, dataNum;

            // Get statistics data
            //aDGVForm = (frmDataGridView)this.ActiveMdiChild;
            colNum = dataGridView1.SelectedColumns.Count;
            aMax = new double[colNum];
            aMin = new double[colNum];
            aMean = new double[colNum];
            aMedian = new double[colNum];
            Q1 = new double[colNum];
            Q3 = new double[colNum];
            aStdDev = new double[colNum];
            aCount = new int[colNum];

            dataNum = 0;
            for (c = 0; c < colNum; c++)
            {
                aArrayList.Clear();
                aColumnIndex = dataGridView1.SelectedColumns[c].Index;
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1[aColumnIndex, i].Value == null)
                        continue;

                    if (dataGridView1[aColumnIndex, i].Value.ToString() != string.Empty)
                    {
                        if (double.Parse(dataGridView1[aColumnIndex, i].Value.ToString()) > 0)
                            aArrayList.Add(double.Parse(dataGridView1[aColumnIndex, i].Value.ToString()));

                    }
                }
                if (aArrayList.Count < 4)
                    continue;

                aDateStr = dataGridView1[0, 0].Value.ToString();
                aNameList.Add(dataGridView1.Columns[aColumnIndex].HeaderText);
                aMax[dataNum] = Statistics.Maximum(aArrayList);
                aMin[dataNum] = Statistics.Minimum(aArrayList);
                aMean[dataNum] = Statistics.Mean(aArrayList);
                aMedian[dataNum] = Statistics.Median(aArrayList);
                Q1[dataNum] = Statistics.Quantile(aArrayList, 1);
                Q3[dataNum] = Statistics.Quantile(aArrayList, 3);
                aStdDev[dataNum] = Statistics.StandardDeviation(aArrayList);
                aCount[dataNum] = aArrayList.Count;
                dataNum += 1;
            }

            // Set data array      
            string aStr = "Type,Mean,Minimum,Q1,Median,Q3,Maximum,StdDev,Count";
            _DataTitleList = aStr.Split(',');
            List<List<string>> aDataList = new List<List<string>>();
            for (i = dataNum - 1; i >= 0; i--)
            {
                List<string> dList = new List<string>();
                dList.Add(aNameList[i]);
                dList.Add(aMean[i].ToString("0.00"));
                dList.Add(aMin[i].ToString());
                dList.Add(Q1[i].ToString());
                dList.Add(aMedian[i].ToString());
                dList.Add(Q3[i].ToString());
                dList.Add(aMax[i].ToString());
                dList.Add(aStdDev[i].ToString("0.00"));
                dList.Add(aCount[i].ToString());

                aDataList.Add(dList);
            }

            // Open new form
            Open_DataGridViewForm(aDataList, "Statistic Result");
        }

        private void Open_DataGridViewForm(List<List<string>> tDataList, string aText)
        {
            frmDataGridView aForm = new frmDataGridView();
            int i, j, colNum, rowNum;

            colNum = tDataList[0].Count;
            rowNum = tDataList.Count;
            aForm.dataGridView1.ColumnCount = colNum;
            aForm.dataGridView1.RowCount = rowNum;
            for (i = 0; i < colNum; i++)
            {
                aForm.dataGridView1.Columns[i].Name = _DataTitleList[i];
                aForm.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (j = 0; j < rowNum; j++)
                    aForm.dataGridView1[i, j].Value = tDataList[j][i];

                aForm.dataGridView1.Columns[i].Frozen = false;
            }
            aForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            aForm.dataGridView1.MultiSelect = true;
            aForm.dataGridView1.AllowUserToOrderColumns = true;

            if (aText == "合并数据")
            {
                for (i = 0; i < aForm.dataGridView1.ColumnCount; i++)
                {
                    if (aForm.dataGridView1.Columns[i].Name.Substring(aForm.dataGridView1.Columns[i].Name.Length - 4, 4) == "Time")
                        aForm.dataGridView1.Columns[i].DisplayIndex = 0;
                }
            }

            aForm.Text = aText;
            aForm.Show(this);
        }

        private void TSB_Chart_Click(object sender, EventArgs e)
        {
            int aColumnIndex;
            aColumnIndex = dataGridView1.SelectedColumns[0].Index;            
            frmChart aFrmChart = new frmChart();

            //Set chart
            GraphPane myPane = aFrmChart.zedGraphControl1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = dataGridView1.SelectedColumns[0].HeaderText;
            myPane.XAxis.Title.Text = "";
            myPane.XAxis.Scale.FontSpec.Angle = 90;
            myPane.YAxis.Title.Text = "";
           
            List<PointF> obsPList = new List<PointF>();
            PointF aPoint = new PointF();            
            string[] labels = new string[dataGridView1.RowCount];
            int sNum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string vStr = dataGridView1[aColumnIndex, i].Value.ToString();
                if (MeteoInfoC.Global.MIMath.IsNumeric(vStr))
                {
                    Single value = Single.Parse(vStr);
                    if (!(Math.Abs(value / MissingValue - 1) < 0.01))
                    {
                        aPoint.X = sNum;
                        aPoint.Y = value;
                        obsPList.Add(aPoint);
                        labels[sNum] = dataGridView1[0, i].Value.ToString();
                        sNum += 1;
                    }
                }
            }
            Array.Resize(ref labels, sNum);
            //CreateZedGraph(aFrmChart.zedGraphControl1, obsPList, "Bar", labels, "观测");
            PointPairList list = new PointPairList();
            for (int i = 0; i < obsPList.Count; i++)
            {
                list.Add(obsPList[i].X, obsPList[i].Y);
            }
            aFrmChart._DataList.Add(list);
            aFrmChart._Labels = labels;

            //Show chart
            aFrmChart.Show(this);
        }

        public void CreateZedGraph(ZedGraphControl zgc, List<PointF> pointList, string aType,
            string[] labels, string sName)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Make up some data points
            PointPairList list = new PointPairList();
            int i;
            for (i = 0; i < pointList.Count; i++)
            {
                list.Add(pointList[i].X, pointList[i].Y);
            }


            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend            
            Color aColor = GetGraphColor(myPane);
            switch (aType)
            {
                case "Line":
                    LineItem myCurve = myPane.AddCurve(sName, list, aColor, SymbolType.Triangle);
                    break;
                case "Bar":
                    BarItem myBar = myPane.AddBar(sName, list, aColor);
                    break;
            }

            // Set the XAxis to date type   
            myPane.XAxis.Type = AxisType.Text;
            myPane.YAxis.Scale.IsReverse = false;
            myPane.XAxis.Scale.TextLabels = labels;

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private Color GetGraphColor(GraphPane myPane)
        {
            Color aColor;
            List<Color> colorList = new List<Color>();

            colorList.Add(Color.Blue);
            colorList.Add(Color.Red);
            colorList.Add(Color.Green);
            colorList.Add(Color.Black);
            colorList.Add(Color.Purple);
            colorList.Add(Color.GreenYellow);
            colorList.Add(Color.Violet);

            int idx = myPane.CurveList.Count;
            while (idx >= colorList.Count)
            {
                idx = idx - colorList.Count;
            }
            aColor = colorList[idx];

            return aColor;
        }

        private void TSB_ToStation_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.Title = "Open Station Coordinate File";
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                string inFile = ofDlg.FileName;
                SaveFileDialog sfDlg = new SaveFileDialog();
                sfDlg.Title = "Save Station Data File";
                if (sfDlg.ShowDialog() == DialogResult.OK)
                {
                    string outFile = sfDlg.FileName;
                    if (ProjInfo.Transform.ProjectionName == ProjectionNames.Lon_Lat)
                        ((GridData)Data).ToStation(inFile, outFile);
                    else
                    {
                        if (MessageBox.Show("If project stations?", "Alarm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            LonLatStationDataInfo aDataInfo = new LonLatStationDataInfo();
                            aDataInfo.ReadDataInfo(inFile);
                            StationData inStData = aDataInfo.GetNullStationData();
                            ProjectionInfo fromProj = KnownCoordinateSystems.Geographic.World.WGS1984;
                            StationData midStData = inStData.Projection(fromProj, ProjInfo);
                            StationData outStData = ((GridData)Data).ToStation(midStData);
                            outStData.SaveAsCSVFile(outFile, "data");
                        }
                        else
                            ((GridData)Data).ToStation(inFile, outFile);
                    }
                }
            }
        }
    }
}
