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
    public partial class frmComboBox : Form
    {
        public frmComboBox()
        {
            InitializeComponent();
        }

        public void SetComboBox(string[] items)
        {
            CB_Item.Items.Clear();
            foreach (string item in items)
            {
                CB_Item.Items.Add(item);
            }
            CB_Item.SelectedIndex = 0;
        }

        public string GetSelectedItem()
        {
            return CB_Item.Text;
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

        private void frmComboBox_Load(object sender, EventArgs e)
        {

        }
    }
}
