using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfoC.Layer;
using MeteoInfoC.Geoprocess;
using MeteoInfoC.Shape;

namespace MeteoInfo.Forms
{
    public partial class frmClipping : Form
    {
        List<VectorLayer> _vLayers = new List<VectorLayer>();
        List<VectorLayer> _polygonLayers = new List<VectorLayer>();

        public frmClipping()
        {
            InitializeComponent();
        }

        private void frmClipping_Load(object sender, EventArgs e)
        {
            foreach (MapLayer aLayer in frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers)
            {
                if (aLayer.LayerType == LayerTypes.VectorLayer)
                {
                    _vLayers.Add((VectorLayer)aLayer);
                    if (aLayer.ShapeType == ShapeTypes.Polygon)
                        _polygonLayers.Add((VectorLayer)aLayer);
                }
            }

            CB_FromLayer.Items.Clear();
            foreach (VectorLayer aLayer in _vLayers)
                CB_FromLayer.Items.Add(aLayer.LayerName);
            CB_FromLayer.SelectedIndex = 0;

            CB_ClippingLayer.Items.Clear();
            foreach (VectorLayer aLayer in _polygonLayers)
                CB_ClippingLayer.Items.Add(aLayer.LayerName);
            CB_ClippingLayer.SelectedIndex = 0;
        }

        private void CB_ClippingLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            VectorLayer aLayer = _polygonLayers[CB_ClippingLayer.SelectedIndex];
            List<int> selIndexes = aLayer.GetSelectedShapeIndexes();
            if (selIndexes.Count > 0)
            {
                ChB_SelFeaturesOnly.Enabled = true;
                ChB_SelFeaturesOnly.Text = "Selected features only (" + selIndexes.Count.ToString() + " features selected)";
            }
            else
            {
                ChB_SelFeaturesOnly.Enabled = false;
                ChB_SelFeaturesOnly.Checked = false;
                ChB_SelFeaturesOnly.Text = "Selected features only";
            }
        }

        private void B_Apply_Click(object sender, EventArgs e)
        {
            if (CB_FromLayer.Text == CB_ClippingLayer.Text)
            {
                MessageBox.Show("The two layers are same!", "Alarm");
                return;
            }

            //---- Show progressbar                      
            this.Cursor = Cursors.WaitCursor;

            VectorLayer fromLayer = _vLayers[CB_FromLayer.SelectedIndex];
            VectorLayer clipLayer = _vLayers[CB_ClippingLayer.SelectedIndex];
            bool onlySel = ChB_SelFeaturesOnly.Checked;
            VectorLayer newLayer = fromLayer.Clip(clipLayer, onlySel);
            newLayer.LayerName = "Clip_" + newLayer.LayerName;
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.AddLayer(newLayer);
            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();

            //---- Hide progressbar                      
            this.Cursor = Cursors.Default;
        }
    }
}
