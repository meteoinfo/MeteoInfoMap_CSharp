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
    public partial class frmInputBox : Form
    {
        public string Value;

        public frmInputBox(string info, string title, string value)
        {
            InitializeComponent();

            Lab_Info.Text = info;
            this.Text = title;
            TB_Value.Text = value;
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            Value = TB_Value.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
