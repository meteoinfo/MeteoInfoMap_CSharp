using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace MeteoInfo.Forms
{
    public partial class frmChart : Form
    {
        public List<PointPairList> _DataList = new List<PointPairList>();
        public string[] _Labels;

        public frmChart()
        {
            InitializeComponent();
        }

        private void frmChart_Load(object sender, EventArgs e)
        {
            SetItem_ChartType();
        }

        private void SetItem_ChartType()
        {
            CB_ChartType.Items.Clear();
            CB_ChartType.Items.AddRange(new string[] { "Line", "Point", "PointPlusLine", "Bar" });
            CB_ChartType.Text = "Bar";
        }

        private void CB_ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateZedGraph(this.zedGraphControl1, "");
        }

        public void CreateZedGraph(ZedGraphControl zgc, string sName)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
            //string layerName = GetLayerName();
            Color aColor = GetGraphColor(myPane);
            //SymbolType aST = GetGraphSymbolType();
            switch (CB_ChartType.Text)
            {
                case "Line":
                    foreach (PointPairList list in _DataList)
                    {
                        LineItem myCurve = myPane.AddCurve(sName, list, aColor, SymbolType.Triangle);
                        myCurve.Symbol.IsVisible = false;
                    }

                    // Fill the area under the curve with a white-red gradient at 45 degrees
                    //myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);

                    // Make the symbols opaque by filling them with white
                    //myCurve.Symbol.Fill = new Fill(Color.White);
                    break;
                case "PointPlusLine":
                    foreach (PointPairList list in _DataList)
                    {
                        LineItem myCurve = myPane.AddCurve(sName, list, aColor, SymbolType.Triangle);
                    }
                    break;
                case "Point":
                    foreach (PointPairList list in _DataList)
                    {
                        LineItem myCurve = myPane.AddCurve(sName, list, aColor, SymbolType.Triangle);
                        myCurve.Line.IsVisible = false;
                    }
                    break;
                case "Bar":
                    foreach (PointPairList list in _DataList)
                    {
                        BarItem myBar = myPane.AddBar(sName, list, aColor);
                    }
                    break;
            }

            // Set the XAxis to date type                           
            myPane.YAxis.Scale.IsReverse = false;
            myPane.XAxis.Scale.TextLabels = _Labels;

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
            zgc.Invalidate();
        }

        private Color GetGraphColor(GraphPane myPane)
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

            int idx = myPane.CurveList.Count;
            while (idx >= colorList.Count)
            {
                idx = idx - colorList.Count;
            }
            aColor = colorList[idx];

            return aColor;
        }
    }
}
