using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfoC.Layer;
using MeteoInfoC.Shape;

namespace MeteoInfo.Forms
{
    public partial class frmSelectByAttributes : Form
    {
        private string _queryStr;
        //private string _ShowOption;
        private List<VectorLayer> _mapLayers;
        private VectorLayer _selectLayer;
        private string _selectField;
        private int _selectMethodIndex;

        public frmSelectByAttributes()
        {
            InitializeComponent();
        }

        private void frmSelTraj_Load(object sender, EventArgs e)
        {
            //---- Add selectable layers
            _mapLayers = new List<VectorLayer>();
            int i;
            for (i = 0; i < frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers.Count; i++)
            {
                if (frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i].LayerType == LayerTypes.VectorLayer)
                {
                    VectorLayer aLayer = (VectorLayer)frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LayerSet.Layers[i];
                    _mapLayers.Add(aLayer);                    
                }
            }
            
            CB_MapLayers.Items.Clear();
            if (_mapLayers.Count > 0)
            {
                for (i = 0; i < _mapLayers.Count; i++)
                {
                    CB_MapLayers.Items.Add(_mapLayers[i].LayerName);
                }
                CB_MapLayers.SelectedIndex = 0;

                Get_Fields();
            }

            //Add select methods
            CB_Method.Items.Clear();
            CB_Method.Items.Add("Create a new selection");
            CB_Method.Items.Add("Add to current selection");
            CB_Method.Items.Add("Remove from current selection");
            CB_Method.Items.Add("Select from current selection");
            CB_Method.SelectedIndex = 0;
        }

        private void Get_Fields()
        {
            this.LB_Fields.Items.Clear();
            int i = 0;                                              
            for (i = 0; i < _selectLayer.NumFields; i++)
            {
                this.LB_Fields.Items.Add(_selectLayer.Fields[i].ColumnName);
            }
            this.LB_Fields.SelectedIndex = 0;
        }        

        private void LB_Field_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TB_SQL.Text += LB_Fields.SelectedItem.ToString();
            TB_SQL.Focus();
        }

        private void B_EQ_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " = ";
            TB_SQL.Focus();
        }

        private void B_GT_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " > ";
            TB_SQL.Focus();
        }

        private void B_LT_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " < ";
            TB_SQL.Focus();
        }

        private void B_NE_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " <> ";
            TB_SQL.Focus();
        }

        private void B_GE_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " >= ";
            TB_SQL.Focus();
        }

        private void B_LE_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " <= ";
            TB_SQL.Focus();
        }

        private void B_And_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " And ";
            TB_SQL.Focus();
        }

        private void B_All_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " Or ";
            TB_SQL.Focus();
        }

        private void B_Not_Click(object sender, EventArgs e)
        {
            TB_SQL.Text += " Not ";
            TB_SQL.Focus();
        }

        private void B_Select_Click(object sender, EventArgs e)
        {
            //---- Judge the SQL syntax
            _queryStr = this.TB_SQL.Text;            

            //---- Show progressbar                      
            this.Cursor = Cursors.AppStarting;

            //---- Selection loop                        
            Application.DoEvents();            
            DataTable aTable = new DataTable();
            foreach (DataColumn aDC in _selectLayer.AttributeTable.Table.Columns)
            {
                DataColumn bDC = new DataColumn();
                bDC.ColumnName = aDC.ColumnName;
                bDC.DataType = aDC.DataType;
                aTable.Columns.Add(bDC);
            }
            foreach (DataRow aRow in _selectLayer.AttributeTable.Table.Rows)
            {
                aTable.ImportRow(aRow);
            }
            aTable.Columns.Add(new DataColumn("Shape_ID", typeof(int)));
            aTable.Columns["Shape_ID"].SetOrdinal(0);
            for (int j = 0; j < aTable.Rows.Count; j++)
            {
                aTable.Rows[j][0] = j;
            }
            Query(aTable, _queryStr, _selectLayer);
            
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();

            //---- Hide progressbar                      
            this.Cursor = Cursors.Default;
        }

        public void Query(DataTable querytable, string querystring, VectorLayer aLayer)
        {

            try
            {
                DataRow[] foundRows = null;

                try
                {
                    //Fix double-quote string enclosures
                    if (querystring.Contains("\"") & !querystring.Contains("'"))
                        querystring = querystring.Replace("\"", "'");

                    foundRows = querytable.Select(querystring);                    

                }
                catch (System.Data.EvaluateException e)
                {
                    MessageBox.Show("The query you have entered is not valid. Please adjust your query syntax" + 
                        Environment.NewLine + e.Message, "Syntax Error");
                    return;
                }

                Hashtable queryhash = new Hashtable();
                int i = 0;
                int j = 0;                
                if (foundRows.Length > 0)
                {
                    //g_MW.View.ClearSelectedShapes()
                    if (!string.IsNullOrEmpty(querystring))
                    {

                        foreach (DataRow singlerow in foundRows)
                        {                            
                            queryhash.Add(singlerow[0], i);
                            //g_MW.View.SelectedShapes.AddByIndex(singlerow(0), g_MW.View.SelectColor)
                            j = j + 1;
                        }
                    }
                }
                SelectShapes(queryhash, aLayer);

            }
            catch (System.Exception e)
            {
                MessageBox.Show("Syntax Error"+ Environment.NewLine + e.Message, "Syntax Error");
                return;
            }
        }

        private void SelectShapes(Hashtable ShapeHandles, VectorLayer aLayer)
        {
            int i = 0;
            switch (_selectMethodIndex)
            {
                case 0:    //Create a new selection
                    for (i = 0; i < aLayer.ShapeNum; i++)
                    {
                        if (ShapeHandles.ContainsKey(i))
                            ((Shape)aLayer.ShapeList[i]).Selected = true;
                        else
                            ((Shape)aLayer.ShapeList[i]).Selected = false;
                    }
                    break;
                case 1:    //Add to current selection
                    for (i = 0; i < aLayer.ShapeNum; i++)
                    {
                        if (ShapeHandles.ContainsKey(i))
                            ((Shape)aLayer.ShapeList[i]).Selected = true;
                    }
                    break;
                case 2:    //Remove from current selection
                    for (i = 0; i < aLayer.ShapeNum; i++)
                    {
                        if (ShapeHandles.ContainsKey(i))
                            ((Shape)aLayer.ShapeList[i]).Selected = false;
                    }
                    break;
                case 3:    //Select from current selection
                    for (i = 0; i < aLayer.ShapeNum; i++)
                    {
                        if (((Shape)aLayer.ShapeList[i]).Selected)
                        {
                            if (ShapeHandles.ContainsKey(i))
                                ((Shape)aLayer.ShapeList[i]).Selected = true;
                            else
                                ((Shape)aLayer.ShapeList[i]).Selected = false;
                        }
                    }
                    break;
            }
        }

        private void B_Clear_Click(object sender, EventArgs e)
        {
            TB_SQL.Text = string.Empty;

            //B_Select.PerformClick();
        }

        private void CB_MapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectLayer = _mapLayers[CB_MapLayers.SelectedIndex];
            Get_Fields();
        }

        private void CB_Method_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectMethodIndex = CB_Method.SelectedIndex;
        }

        private void LB_Fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selField = LB_Fields.SelectedItem.ToString();
            if (selField != _selectField)
            {
                LB_Values.Items.Clear();
                LB_Values.Enabled = false;
                B_GetValues.Enabled = true;
            }
            _selectField = selField;
        }

        private void B_GetValues_Click(object sender, EventArgs e)
        {
            List<string> valueList = new List<string>();
            
            for (int i = 0; i < _selectLayer.AttributeTable.Table.Rows.Count; i++)
            {
                if (!valueList.Contains(_selectLayer.AttributeTable.Table.Rows[i][_selectField].ToString()))
                {
                    valueList.Add(_selectLayer.AttributeTable.Table.Rows[i][_selectField].ToString());
                }
            }
            valueList.Sort();

            LB_Values.Enabled = true;
            LB_Values.Items.Clear();
            foreach (string vStr in valueList)
                LB_Values.Items.Add("'" + vStr + "'");

            B_GetValues.Enabled = false;
        }

        private void LB_Values_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            TB_SQL.Text += LB_Values.SelectedItem.ToString();
            TB_SQL.Focus();
        }
    }
}
