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
    public partial class frmIdentifer : Form
    {
        private Form m_RefForm = null;

        public Form RefForm
        {
            get { return m_RefForm; }
            set { m_RefForm = value; }
        }

        public frmIdentifer()
        {
            InitializeComponent();
        }

        private void frmIdentifer_Load(object sender, EventArgs e)
        {
            ListView1.View = View.Details;
            ListView1.Columns.Add(new ColumnHeader());
            ListView1.Columns[0].Width = 100;
            ListView1.Columns[0].Text = "Field";
            ListView1.Columns.Add(new ColumnHeader());
            ListView1.Columns[1].Width = ListView1.Width - ListView1.Columns[0].Width - 5;
            ListView1.Columns[1].Text = "Value";
        }

        private void frmIdentifer_FormClosed(object sender, FormClosedEventArgs e)
        {
            ////frmMain.G_FrmIdentifer = null;
            //if (RefForm == null)
            //    frmMain.G_FrmIdentifer = null;
            //else
            //    frmSectionPlot.G_FrmIdentifer = null;

        }
    }
}
