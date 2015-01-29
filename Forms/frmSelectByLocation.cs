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
using MeteoInfoC;

namespace MeteoInfo.Forms
{
    public partial class frmSelectByLocation : Form
    {
        List<VectorLayer> _vLayers = new List<VectorLayer>();

        public frmSelectByLocation()
        {
            InitializeComponent();
        }

        private void frmSelectByLocation_Load(object sender, EventArgs e)
        {
            foreach (MapLayer aLayer in frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers)
            {
                if (aLayer.LayerType == LayerTypes.VectorLayer)
                    _vLayers.Add((VectorLayer)aLayer);
            }

            CB_FromLayer.Items.Clear();
            foreach (VectorLayer aLayer in _vLayers)
                CB_FromLayer.Items.Add(aLayer.LayerName);
            CB_FromLayer.SelectedIndex = 0;

            CB_SelType.Items.Clear();
            CB_SelType.Items.AddRange(Enum.GetNames(typeof(SpatialQueryTypes)));
            CB_SelType.SelectedIndex = 0;

            CB_RelatedLayer.Items.Clear();
            foreach (VectorLayer aLayer in _vLayers)
                CB_RelatedLayer.Items.Add(aLayer.LayerName);
            CB_RelatedLayer.SelectedIndex = 0;
        }

        private void CB_RelatedLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            VectorLayer aLayer = _vLayers[CB_RelatedLayer.SelectedIndex];
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

        private void B_Select_Click(object sender, EventArgs e)
        {
            if (CB_FromLayer.Text == CB_RelatedLayer.Text)
            {
                MessageBox.Show("The two layers are same!", "Alarm");
                return;
            }

            //---- Show progressbar                      
            this.Cursor = Cursors.WaitCursor;

            VectorLayer fromLayer = _vLayers[CB_FromLayer.SelectedIndex];
            VectorLayer relatedLayer = _vLayers[CB_RelatedLayer.SelectedIndex];
            SpatialQueryTypes selType = (SpatialQueryTypes)Enum.Parse(typeof(SpatialQueryTypes), CB_SelType.Text);
            bool onlySel = ChB_SelFeaturesOnly.Checked;
            switch (selType)
            {
                case SpatialQueryTypes.Within:
                    if (relatedLayer.ShapeType != ShapeTypes.Polygon)
                    {
                        MessageBox.Show("The second layer must be polygon layer for 'Within' case!", "Alarm");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        foreach (Shape aShape in fromLayer.ShapeList)
                        {
                            bool isIn = true;
                            List<PointD> points = aShape.GetPoints();
                            foreach (PointD aPoint in points)
                            {
                                if (!GeoComputation.PointInPolygonLayer(relatedLayer, aPoint, onlySel))
                                {
                                    isIn = false;
                                    break;
                                }
                            }
                            if (isIn)
                                aShape.Selected = true;
                            else
                                aShape.Selected = false;
                        }
                    }
                    break;
                case SpatialQueryTypes.Contain:
                    if (fromLayer.ShapeType != ShapeTypes.Polygon)
                    {
                        MessageBox.Show("The first layer must be polygon layer for 'Within' case!", "Alarm");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        List<Shape> shapes = new List<Shape>();
                        if (onlySel)
                        {
                            foreach (Shape aShape in relatedLayer.ShapeList)
                            {
                                if (aShape.Selected)
                                    shapes.Add(aShape);
                            }
                        }
                        else
                            shapes = relatedLayer.ShapeList;

                        foreach (Shape aShape in fromLayer.ShapeList)
                        {
                            aShape.Selected = false;
                            PolygonShape aPolygon = (PolygonShape)aShape;                            
                            foreach (Shape bShape in shapes)
                            {
                                bool isIn = true;
                                List<PointD> points = bShape.GetPoints();
                                foreach (PointD aPoint in points)
                                {
                                    if (!GeoComputation.PointInPolygon(aPolygon, aPoint))
                                    {
                                        isIn = false;
                                        break;
                                    }
                                }
                                if (isIn)
                                {
                                    aShape.Selected = true;
                                    break;
                                }
                            }                            
                        }
                    }
                    break;
            }

            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();

            //---- Hide progressbar                      
            this.Cursor = Cursors.Default;
        }

        private void B_Clear_Click(object sender, EventArgs e)
        {
            VectorLayer fromLayer = _vLayers[CB_FromLayer.SelectedIndex];
            fromLayer.ClearSelectedShapes();
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();
        }
    }
}
