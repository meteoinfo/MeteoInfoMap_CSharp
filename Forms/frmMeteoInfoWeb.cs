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
    public partial class frmMeteoInfoWeb : Form
    {
        public frmMeteoInfoWeb()
        {
            InitializeComponent();
        }

        private void frmMeteoInfoWeb_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("http://www.meteothinker.com");
        }
    }
}
