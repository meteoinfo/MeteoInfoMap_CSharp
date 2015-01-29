using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfo.Classes;
using MeteoInfo.Forms;
using MeteoInfoC.Shape;

namespace MeteoInfo
{
    public partial class frmProperty : Form
    {
        ShapeTypes m_ShapeType = new ShapeTypes();
        
        public frmProperty()
        {
            InitializeComponent();
        }

        private void frmSymbolSet_Load(object sender, EventArgs e)
        {
            //this.Left = frmLegendSet.pCurrenWin.Left + 300;
        }

        public void SetShapeSet(ShapeTypes aST)
        {
            m_ShapeType = aST;
        }

        public void SetObject(object aObj)
        {
            propertyGrid1.SelectedObject = aObj;
        }
        
    }
}
