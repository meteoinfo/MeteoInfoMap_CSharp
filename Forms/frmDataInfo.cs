using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeteoInfo
{
    public partial class frmDataInfo : Form
    {
        public frmDataInfo()
        {
            InitializeComponent();
        }

        public void SetTextBox(string aStr)
        {
            textBox1.Text = aStr;
            textBox1.Select(0, 0);
        }

        private void frmDataInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
