using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfoC.Projections;

namespace MeteoInfo.Forms
{
    public partial class frmProjPoint : Form
    {
        ProjectionInfo _fromProj;
        ProjectionInfo _toProj;

        public frmProjPoint()
        {
            InitializeComponent();
        }

        private void frmProjPoint_Load(object sender, EventArgs e)
        {
            _fromProj = KnownCoordinateSystems.Geographic.World.WGS1984;
            _toProj = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
            TB_FromProj.Text = _fromProj.ToProj4String();
            TB_ToProj.Text = _toProj.ToProj4String();
        }

        private void B_Reserve_Click(object sender, EventArgs e)
        {
            string aStr = TB_FromProj.Text;
            TB_FromProj.Text = TB_ToProj.Text;
            TB_ToProj.Text = aStr;            
        }

        private void B_Projection_Click(object sender, EventArgs e)
        {
            double fromX = double.Parse(TB_FromX.Text);
            double fromY = double.Parse(TB_FromY.Text);
            double toX, toY;

            _fromProj = new ProjectionInfo(TB_FromProj.Text);
            _toProj = new ProjectionInfo(TB_ToProj.Text);

            double[][] points = new double[1][];
            points[0] = new double[] { fromX, fromY };
            //double[] Z = new double[1];
            try
            {
                Reproject.ReprojectPoints(points, _fromProj, _toProj, 0, 1);
                toX = points[0][0];
                toY = points[0][1];
                TB_ToX.Text = toX.ToString();
                TB_ToY.Text = toY.ToString();
            }
            catch
            {
               
            }
        }
    }
}
