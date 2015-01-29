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
    public partial class frmUVSet : Form
    {
        public frmUVSet()
        {
            InitializeComponent();
        }

        public void SetUVItems(List<string> vList)
        {
            int i;
            for (i = 0; i < vList.Count; i++)
            {
                CB_U.Items.Add(vList[i]);
                CB_V.Items.Add(vList[i]);
            }            
        }

        public void GetUVItems(ref string UStr, ref string VStr)
        {
            UStr = CB_U.Text;
            VStr = CB_V.Text;
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            if (CB_U.SelectedIndex < 0 || CB_V.SelectedIndex < 0)
            {
                MessageBox.Show("Please select U/V (Direction/Speed) variable!", "Error");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void B_NoUV_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }       
    }
}
