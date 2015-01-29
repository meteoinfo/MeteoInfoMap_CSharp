using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeteoInfo.Forms
{
    public partial class frmGridViewSet : Form
    {
        public frmGridViewSet()
        {
            InitializeComponent();
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmGridViewSet_Load(object sender, EventArgs e)
        {

        }

        public void SetParameters(bool interpolateGrid)
        {
            CHB_InterpolateGrid.Checked = interpolateGrid;
        }

        public bool GetParameters()
        {
            return CHB_InterpolateGrid.Checked;
        }
    }
}
