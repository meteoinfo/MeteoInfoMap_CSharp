using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace MeteoInfo.Forms
{
    public partial class frmScript : Form
    {
        private string _fileName;

        public frmScript()
        {
            InitializeComponent();
        }

        private void TSB_RunScript_Click(object sender, EventArgs e)
        {
            ScriptEngine scriptEngine = Python.CreateEngine();
            ScriptScope pyScope = scriptEngine.CreateScope();

            //Set path
            string path = Assembly.GetExecutingAssembly().Location;
            string rootDir = Directory.GetParent(path).FullName;
            List<string> paths = new List<string>();
            paths.Add(rootDir);
            paths.Add(Path.Combine(rootDir, "Lib"));
            scriptEngine.SetSearchPaths(paths.ToArray());

            pyScope.SetVariable("mipy", frmMain.CurrentWin);

            string code = RTB_ScriptText.Text;
            if (code.Trim() == "")
                return;

            this.Cursor = Cursors.WaitCursor;
            ScriptSource source = scriptEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            source.Execute(pyScope);
            this.Cursor = Cursors.Default;
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog();
            aDlg.Filter = "Python file (*.py)|*.py";
            if (aDlg.ShowDialog() == DialogResult.OK)
            {
                _fileName = aDlg.FileName;
                RTB_ScriptText.Clear();
                RTB_ScriptText.LoadFile(_fileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void TSMI_SaveFile_Click(object sender, EventArgs e)
        {
            RTB_ScriptText.SaveFile(_fileName, RichTextBoxStreamType.PlainText);
        }

        private void TSMI_SaveAs_Click(object sender, EventArgs e)
        {

        }
    }
}
