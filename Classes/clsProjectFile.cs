using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using MeteoInfoC.Map;
using MeteoInfoC.Layer;
using MeteoInfoC.Shape;
using MeteoInfoC.Legend;
using MeteoInfoC.Drawing;
using MeteoInfoC.Data.MapData;
using MeteoInfo.Forms;
using MeteoInfoC.Projections;

namespace MeteoInfo.Classes
{
    /// <summary>
    /// MeteoInfo project file
    /// </summary>
    class clsProjectFile
    {
        #region private variables
        private string m_FileName;

        #endregion

        #region Constructor
        public clsProjectFile()
        {
            m_FileName = "";
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get or set project filename
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        #endregion

        #region Method

        #region Save project
        /// <summary>
        /// Save project file
        /// </summary>
        /// <param name="aFile"></param>
        public void SaveProjFile(string aFile)
        {
            m_FileName = aFile;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<MeteoInfo name='" + Path.GetFileNameWithoutExtension(aFile) + "' type='projectfile'></MeteoInfo>");
            XmlElement root = doc.DocumentElement;

            //Add language element
            AddLanguageElement(ref doc, root, Thread.CurrentThread.CurrentUICulture.Name);

            ////Add MapView content
            //frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.ExportProjectXML(ref doc, root, m_FileName);

            //Add LayersLegend content
            frmMain.CurrentWin.MapDocument.MapLayout.UpdateMapFrameOrder();
            frmMain.CurrentWin.MapDocument.ExportProjectXML(ref doc, root, m_FileName);

            //Add MapLayout content
            frmMain.CurrentWin.MapDocument.MapLayout.ExportProjectXML(ref doc, root);            

            //Save project file            
            doc.Save(aFile);
        }        

        private void AddLanguageElement(ref XmlDocument m_Doc, XmlElement parent, string name)
        {
            XmlElement Language = m_Doc.CreateElement("Language");
            XmlAttribute LanName = m_Doc.CreateAttribute("Name");

            LanName.InnerText = name;
            Language.Attributes.Append(LanName);

            parent.AppendChild(Language);
        }        

        #endregion

        #region Load project
        /// <summary>
        /// Load project file
        /// </summary>
        /// <param name="aFile"></param>
        public void LoadProjFile(string aFile)
        {
            m_FileName = aFile;

            XmlDocument doc = new XmlDocument();
            doc.Load(aFile);
            XmlElement root = doc.DocumentElement;

            Environment.CurrentDirectory = Path.GetDirectoryName(aFile);

            //Load elements
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = true;
            LoadLanguageElement(root);         
            //Load map frames content
            frmMain.CurrentWin.MapDocument.ImportProjectXML(root);
            frmMain.CurrentWin.MapDocument.MapLayout.MapFrames = frmMain.CurrentWin.MapDocument.MapFrames;
            //Load MapLayout content
            frmMain.CurrentWin.MapDocument.MapLayout.ImportProjectXML(root);            
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.LockViewUpdate = false;
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.PaintLayers();            
        }        

        private void LoadLanguageElement(XmlElement parent)
        {
            XmlNode Language = parent.GetElementsByTagName("Language")[0];
            if (Language != null)
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language.Attributes["Name"].InnerText);
            //frmMain.CurrentWin.ApplyResource(false);
        }        

        #endregion
        
        #endregion
    }
}
