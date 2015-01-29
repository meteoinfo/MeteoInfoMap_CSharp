using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfo.Classes;
using System.Collections;
using System.Drawing.Drawing2D;
using MeteoInfoC.Map;
using MeteoInfoC.Global;
using MeteoInfoC.Layer;
using MeteoInfoC.Projections;
//using DotSpatial.Projections;

namespace MeteoInfo.Forms
{
    public partial class frmProjection : Form
    {
        public frmProjection()
        {
            InitializeComponent();
        }

        private void frmProjection_Load(object sender, EventArgs e)
        {
            CB_Projection.Items.Clear();            
            CB_Projection.Items.Add(ProjectionNames.Lon_Lat.ToString());
            CB_Projection.Items.Add(ProjectionNames.Lambert_Conformal.ToString());
            CB_Projection.Items.Add(ProjectionNames.Albers_Conic_Equal_Area.ToString());
            CB_Projection.Items.Add(ProjectionNames.North_Polar_Stereographic.ToString());
            CB_Projection.Items.Add(ProjectionNames.South_Polar_Stereographic.ToString());
            CB_Projection.Items.Add(ProjectionNames.Mercator.ToString());
            CB_Projection.Items.Add(ProjectionNames.Robinson.ToString());
            CB_Projection.Items.Add(ProjectionNames.Mollweide.ToString());
            CB_Projection.Items.Add(ProjectionNames.Orthographic.ToString());
            CB_Projection.Items.Add(ProjectionNames.Geostationary.ToString());
            CB_Projection.Items.Add(ProjectionNames.Oblique_Stereographic.ToString());
            CB_Projection.Items.Add(ProjectionNames.Transverse_Mercator.ToString());
            CB_Projection.Items.Add(ProjectionNames.Sinusoidal.ToString());
            CB_Projection.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName.ToString();

            //if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
            //{
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.GeoLayers = (ArrayList)frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.Layers.Clone();
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.GeoLayers = new List<MapLayer>(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers);
            //}
        }

        private void B_Apply_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ProjectionNames aPrjName = (ProjectionNames)Enum.Parse(typeof(ProjectionNames), 
                CB_Projection.Text, true);            
            //double refLon;

            //if (!frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.IsLonLatMap)
            //{
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.Layers = (ArrayList)frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.GeoLayers.Clone();
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.Layers = new List<MapLayer>(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.GeoLayers);
            //}
            //ProjectionInfo fromProj = KnownCoordinateSystems.Geographic.World.WGS1984;            
            //ProjectionInfo toProj = new ProjectionInfo();
            string toProjStr;

            switch (aPrjName)
            {
                case ProjectionNames.Lambert_Conformal:
                    toProjStr = "+proj=lcc" +
                        "+lat_1=" + TB_StandPara1.Text +
                        "+lat_2=" + TB_StandPara2.Text +
                        "+lat_0=" + TB_RefLat.Text +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+x_0=" + TB_FalseEasting.Text +
                        "+y_0=" + TB_FalseNorthing.Text;
                    break;
                case ProjectionNames.Albers_Conic_Equal_Area:
                    toProjStr = "+proj=aea" +
                        "+lat_1=" + TB_StandPara1.Text +
                        "+lat_2=" + TB_StandPara2.Text +
                        "+lat_0=" + TB_RefLat.Text +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+x_0=" + TB_FalseEasting.Text +
                        "+y_0=" + TB_FalseNorthing.Text;
                    break;
                case ProjectionNames.North_Polar_Stereographic:
                    toProjStr = "+proj=stere" +
                        "+lat_0=90" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                    break;
                case ProjectionNames.South_Polar_Stereographic:
                    toProjStr = "+proj=stere" +
                        "+lat_0=-90" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                    break;
                case ProjectionNames.Mercator:
                    toProjStr = "+proj=merc" +
                        "+lat_ts=" + TB_StandPara1.Text +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+x_0=" + TB_FalseEasting.Text +
                        "+y_0=" + TB_FalseNorthing.Text;
                    break;
                //case ProjectionNames.Mercator:
                //    toProjStr = "+proj=merc" +
                //        "+lat_0=" + TB_StandPara1.Text +
                //        "+lon_0=" + TB_CentralMeridian.Text +
                //        "+x_0=" + TB_FalseEasting.Text +
                //        "+y_0=" + TB_FalseNorthing.Text;
                //    break;
                case ProjectionNames.Robinson:
                    toProjStr = "+proj=robin" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                    break;
                case ProjectionNames.Mollweide:
                    toProjStr = "+proj=moll" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                    break;
                case ProjectionNames.Orthographic:
                    toProjStr = "+proj=ortho" +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+lat_0=" + TB_RefLat.Text;
                    break;
                case ProjectionNames.Geostationary:
                    toProjStr = "+proj=geos" +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+h=" + TB_RefLat.Text;
                    break;
                case ProjectionNames.Oblique_Stereographic:
                    toProjStr = "+proj=stere" +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+lat_0=" + TB_RefLat.Text;
                    break;
                case ProjectionNames.Transverse_Mercator:
                    toProjStr = "+proj=tmerc" +
                        "+lon_0=" + TB_CentralMeridian.Text +
                        "+lat_0=" + TB_RefLat.Text;
                    break;
                case ProjectionNames.Lon_Lat:
                    toProjStr = KnownCoordinateSystems.Geographic.World.WGS1984.ToProj4String();
                    break;
                case ProjectionNames.Sinusoidal:
                    toProjStr = "+proj=sinu" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                        //"+x_0=" + TB_FalseEasting.Text +
                        //"+y_0=" + TB_FalseNorthing.Text;
                    break;
                default:
                    toProjStr = "+proj=robin" +
                        "+lon_0=" + TB_CentralMeridian.Text;
                    break;
            }

            ProjectionInfo toProj = new ProjectionInfo(toProjStr);
            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo = toProj;
            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjStr = toProj.ToProj4String();
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ProjectLayers(toProj);

            //if (aPrjName == ProjectionNames.Lon_Lat)
            //{
            //    ProjectionSet aProjSet = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection;                
            //    aProjSet.ProjInfo = fromProj;
            //    aProjSet.IsLonLatMap = true;
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection = aProjSet;
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.Layers = (ArrayList)frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.GeoLayers.Clone();
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.Layers = new List<MapLayer>(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Layers.GeoLayers);
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExtentV = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.GetLayersWholeExtent();
            //    Extent aExtent = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExtentV;
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ZoomToExtent(aExtent);
            //    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.RaiseViewExtentChangedEvent();
            //}
            //else
            //{
                

            //    //ProjectionSet aProjSet = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection;                
            //    //aProjSet.ProjInfo = toProj;
            //    //aProjSet.ProjStr = toProjStr;
            //    //aProjSet.IsLonLatMap = false;                              
            //    //refLon = Convert.ToDouble(TB_CentralMeridian.Text);
            //    //aProjSet.RefLon = refLon;
            //    //refLon += 180;
            //    //if (refLon > 180)
            //    //{
            //    //    refLon = refLon - 360;
            //    //}
            //    //else if (refLon < -180)
            //    //{
            //    //    refLon = refLon + 360;
            //    //}
            //    //aProjSet.RefCutLon = refLon;
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection = aProjSet;
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjectLayers_Proj4(frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView, fromProj, toProj);
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LonLatLayer = aProjSet.GenerateLonLatLayer();
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
            //    //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.RaiseViewExtentChangedEvent();
            //}

            this.Cursor = Cursors.Default;
        }

        private string GetParameterStr(string sWKT, string pStr)
        {
            string aStr;
            int idx;
            idx = sWKT.IndexOf(pStr);
            aStr = sWKT.Substring(idx);
            idx = aStr.IndexOf(",");
            aStr = aStr.Substring(idx + 1);
            idx = aStr.IndexOf("]");
            aStr = aStr.Substring(0, idx);

            return aStr;
        }

        private void CB_Projection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectionNames aPrjName = (ProjectionNames)Enum.Parse(typeof(ProjectionNames),
                CB_Projection.Text, true);

            Lab_RefLat.Text = Resources.GlobalResource.ResourceManager.GetString("RefLat_Text");
            switch (aPrjName)
            {
                case ProjectionNames.Lon_Lat:
                    GB_Parameters.Enabled = false;
                    break;
                case ProjectionNames.Lambert_Conformal:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = true;
                    }
                    Lab_ScaleFactor.Visible = false;
                    TB_ScaleFactor.Visible = false;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Lambert_Conformal)
                    {
                        ProjectionInfo aProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
                        TB_CentralMeridian.Text = aProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = aProjInfo.LatitudeOfOrigin.ToString();
                        TB_StandPara1.Text = aProjInfo.StandardParallel1.ToString();
                        TB_StandPara2.Text = aProjInfo.StandardParallel2.ToString();
                        TB_FalseEasting.Text = aProjInfo.FalseEasting.ToString();
                        TB_FalseNorthing.Text = aProjInfo.FalseNorthing.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "0";
                        TB_StandPara1.Text = "25";
                        TB_StandPara2.Text = "47";
                        TB_FalseEasting.Text = "0";
                        TB_FalseNorthing.Text = "0";
                    }
                    break;
                case ProjectionNames.Albers_Conic_Equal_Area:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = true;
                    }
                    Lab_ScaleFactor.Visible = false;
                    TB_ScaleFactor.Visible = false;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Albers_Conic_Equal_Area)
                    {
                        ProjectionInfo aProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
                        TB_CentralMeridian.Text = aProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = aProjInfo.LatitudeOfOrigin.ToString();
                        TB_StandPara1.Text = aProjInfo.StandardParallel1.ToString();
                        TB_StandPara2.Text = aProjInfo.StandardParallel2.ToString();
                        TB_FalseEasting.Text = aProjInfo.FalseEasting.ToString();
                        TB_FalseNorthing.Text = aProjInfo.FalseNorthing.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "0";
                        TB_StandPara1.Text = "25";
                        TB_StandPara2.Text = "47";
                        TB_FalseEasting.Text = "0";
                        TB_FalseNorthing.Text = "0";
                    }
                    break;
                case ProjectionNames.North_Polar_Stereographic:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_ScaleFactor.Visible = true;
                    TB_ScaleFactor.Visible = true;
                    Lab_FalseEasting.Visible = true;
                    TB_FalseEasting.Visible = true;
                    Lab_FalseNorthing.Visible = true;
                    TB_FalseNorthing.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.North_Polar_Stereographic)
                    {                       
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_ScaleFactor.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ScaleFactor.ToString();
                        TB_FalseEasting.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.FalseEasting.ToString();
                        TB_FalseNorthing.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.FalseNorthing.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_ScaleFactor.Text = "1.0";
                        TB_FalseEasting.Text = "0";
                        TB_FalseNorthing.Text = "0";
                    }
                    break;
                case ProjectionNames.South_Polar_Stereographic:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_ScaleFactor.Visible = true;
                    TB_ScaleFactor.Visible = true;
                    Lab_FalseEasting.Visible = true;
                    TB_FalseEasting.Visible = true;
                    Lab_FalseNorthing.Visible = true;
                    TB_FalseNorthing.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.South_Polar_Stereographic)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_ScaleFactor.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ScaleFactor.ToString();
                        TB_FalseEasting.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.FalseEasting.ToString();
                        TB_FalseNorthing.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.FalseNorthing.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_ScaleFactor.Text = "1.0";
                        TB_FalseEasting.Text = "0";
                        TB_FalseNorthing.Text = "0";
                    }
                    break;
                case ProjectionNames.Mercator:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_SP1.Visible = true;
                    TB_StandPara1.Visible = true;
                    Lab_FalseEasting.Visible = true;
                    TB_FalseEasting.Visible = true;
                    Lab_FalseNorthing.Visible = true;
                    TB_FalseNorthing.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Mercator)
                    {
                        ProjectionInfo aProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;
                        TB_CentralMeridian.Text = aProjInfo.CentralMeridian.ToString();                        
                        TB_StandPara1.Text = aProjInfo.StandardParallel1.ToString();                        
                        TB_FalseEasting.Text = aProjInfo.FalseEasting.ToString();
                        TB_FalseNorthing.Text = aProjInfo.FalseNorthing.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_StandPara1.Text = "0";
                        TB_FalseEasting.Text = "0";
                        TB_FalseNorthing.Text = "0";
                    }
                    break;
                case ProjectionNames.Robinson:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Robinson)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                    }
                    break;
                case ProjectionNames.Mollweide:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Mollweide)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                    }
                    break;
                case ProjectionNames.Orthographic:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_RefLat.Visible = true;
                    TB_RefLat.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Orthographic)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.LatitudeOfOrigin.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "40";
                    }
                    break;
                case ProjectionNames.Geostationary:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_RefLat.Text = Resources.GlobalResource.ResourceManager.GetString("Height_Orbit");
                    Lab_RefLat.Visible = true;
                    TB_RefLat.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Geostationary)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.ParamD("h").ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "35785831";
                    }
                    break;
                case ProjectionNames.Oblique_Stereographic:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_RefLat.Visible = true;
                    TB_RefLat.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Oblique_Stereographic)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.LatitudeOfOrigin.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "45";
                    }
                    break;
                case ProjectionNames.Transverse_Mercator:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    Lab_RefLat.Visible = true;
                    TB_RefLat.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Transverse_Mercator)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                        TB_RefLat.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.LatitudeOfOrigin.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                        TB_RefLat.Text = "45";
                    }
                    break;
                case ProjectionNames.Sinusoidal:
                    GB_Parameters.Enabled = true;
                    foreach (Control aControl in GB_Parameters.Controls)
                    {
                        aControl.Visible = false;
                    }
                    Lab_CentralMeridian.Visible = true;
                    TB_CentralMeridian.Visible = true;
                    if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.Transform.ProjectionName == ProjectionNames.Sinusoidal)
                    {
                        TB_CentralMeridian.Text = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo.CentralMeridian.ToString();
                    }
                    else
                    {
                        TB_CentralMeridian.Text = "105";
                    }
                    break;
            }

            if (TB_FalseEasting.Text == "")
                TB_FalseEasting.Text = "0";
            if (TB_FalseNorthing.Text == "")
                TB_FalseNorthing.Text = "0";
        }               

        private void B_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
