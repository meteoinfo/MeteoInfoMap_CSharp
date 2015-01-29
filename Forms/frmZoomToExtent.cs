using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfoC.Global;

namespace MeteoInfo.Forms
{
    public partial class frmZoomToExtent : Form
    {
        private bool m_IsLayout;

        public frmZoomToExtent(bool isLayout)
        {
            InitializeComponent();

            m_IsLayout = isLayout;
        }

        private void frmZoomToExtent_Load(object sender, EventArgs e)
        {
            double minLon, maxLon, minLat, maxLat;
            if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
            {
                minLon = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ViewExtent.minX;
                maxLon = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ViewExtent.maxX;
                minLat = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ViewExtent.minY;
                maxLat = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ViewExtent.maxY;
            }
            else
            {
                minLon = 0;
                maxLon = 0;
                minLat = 0;
                maxLat = 0;
            }

            TB_MinLon.Text = minLon.ToString();
            TB_MaxLon.Text = maxLon.ToString();
            TB_MinLat.Text = minLat.ToString();
            TB_MaxLat.Text = maxLat.ToString();
        }

        private void B_Zoom_Click(object sender, EventArgs e)
        {
            //Check lon/lat set
            double minLon, maxLon, minLat, maxLat;
            minLon = double.Parse(TB_MinLon.Text);
            maxLon = double.Parse(TB_MaxLon.Text);
            minLat = double.Parse(TB_MinLat.Text);
            maxLat = double.Parse(TB_MaxLat.Text);

            if (minLon >= maxLon || minLat >= maxLat)
            {
                MessageBox.Show("Lon/Lat set error!", "Error");
                return;
            }

            //Zoom to lon/lat extent
            Extent aExtent = new Extent();
            aExtent.minX = minLon;
            aExtent.maxX = maxLon;
            aExtent.minY = minLat;
            aExtent.maxY = maxLat;

            if (frmMain.CurrentWin.tabControl1.SelectedIndex == 0)
                frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ZoomToExtentLonLatEx(aExtent);
            else if (frmMain.CurrentWin.tabControl1.SelectedIndex == 1)
            {
                frmMain.CurrentWin.MapDocument.MapLayout.ActiveLayoutMap.ZoomToExtentLonLatEx(aExtent);
                frmMain.CurrentWin.MapDocument.MapLayout.PaintGraphics();
            }

        }

        private void B_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
        
    }
}
