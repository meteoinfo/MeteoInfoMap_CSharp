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
    public partial class frmAddField : Form
    {
        public frmAddField()
        {
            InitializeComponent();
        }

        private void frmAddField_Load(object sender, EventArgs e)
        {
            CB_Type.Items.AddRange(new string[]{ "String", "Integer", "Double" });
            CB_Type.SelectedIndex = 0;
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
