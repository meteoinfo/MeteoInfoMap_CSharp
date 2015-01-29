using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MeteoInfo.Classes;
using MeteoInfoC.Data.MapData;
using MeteoInfoC.Layer;
using MeteoInfoC.Shape;
using MeteoInfoC.Projections;
//using DotSpatial.Projections;

namespace MeteoInfo.Forms
{
    public partial class frmOpenMap : Form
    {
        
        public frmOpenMap()
        {
            InitializeComponent();
        }

        private void B_OpenMapFile_Click(object sender, EventArgs e)
        {
            string aFile;            

            if (RB_DefaultFolder.Checked)
            {
                if (LB_MapFiles.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select one map file!", "Error");
                    return;
                }
                else
                {                                        
                    aFile = Application.StartupPath + "\\Map\\" + LB_MapFiles.SelectedItem.ToString();
                    if (! File.Exists(aFile))
                    {
                        aFile = Application.StartupPath + "/Map/" + LB_MapFiles.SelectedItem.ToString();
                    }
                                      
                }
            }
            else
            {
                OpenFileDialog aDlg = new OpenFileDialog();
                aDlg.Filter = "Supported Formats|*.shp;*.wmp;*.bln;*.bmp;*.gif;*.jpg;*.tif;*.png|Shape File (*.shp)|*.shp|WMP File (*.wmp)|*.wmp|BLN File (*.bln)|*.bln|" + 
                    "Bitmap Image (*.bmp)|*.bmp|Gif Image (*.gif)|*.gif|Jpeg Image (*.jpg)|*.jpg|Tif Image (*.tif)|*.tif|Png Iamge (*.png)|*.png|All Files (*.*)|*.*";

                if (aDlg.ShowDialog() == DialogResult.OK)
                    aFile = aDlg.FileName;
                else
                    return;
            }

            MapLayer aLayer = MapDataManage.OpenLayer(aFile);

            if (aLayer != null)
            {
                ////Set projection
                //if (Path.GetExtension(aLayer.LayerName).ToLower() != ".shp")
                //    aLayer.ProjInfo = frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Projection.ProjInfo;

                //Remove map layers
                if (CB_RemoveMaps.Checked)
                {
                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.RemoveMapLayers();
                }

                //Add layer                
                if (aLayer.LayerType == LayerTypes.VectorLayer)
                {                    
                    if (aLayer.ShapeType == ShapeTypes.Polygon)
                    {
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolygonLayer((VectorLayer)aLayer);
                    }
                    else if (aLayer.ShapeType == ShapeTypes.Polyline)
                    {
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertPolylineLayer((VectorLayer)aLayer);
                    }
                    else
                    {
                        frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(aLayer);
                    }
                }
                else
                    frmMain.CurrentWin.MapDocument.ActiveMapFrame.InsertImageLayer((ImageLayer)aLayer);
            } 
        }
                
        private void frmOpenMap_Load(object sender, EventArgs e)
        {
            RB_DefaultFolder.Checked = true;
            CB_RemoveMaps.Checked = false;

            string aFolder = Application.StartupPath + "\\Map";
            if (! Directory.Exists(aFolder))
            {
                aFolder = Application.StartupPath + "/Map";
            }
            if (Directory.Exists(aFolder))
            {
                string aExtension;
                foreach (string aFile in Directory.GetFiles(aFolder))
                {
                    aExtension = Path.GetExtension(aFile).ToLower();
                    if (aExtension == "" || aExtension == ".shp" || aExtension == ".dat" ||
                        aExtension == ".wmp" || aExtension == ".bmp" || aExtension == ".gif" ||
                        aExtension == ".jpg" || aExtension == ".tif" || aExtension == ".png")
                    {
                        LB_MapFiles.Items.Add(Path.GetFileName(aFile));
                    }
                }
            }
        }

        private void RB_DefaultFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_DefaultFolder.Checked)
            {
                LB_MapFiles.Enabled = true;
            }
            else
            {
                LB_MapFiles.Enabled = false;
            }
        }
    }
}
