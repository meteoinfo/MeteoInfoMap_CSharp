using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;

namespace MeteoInfo.Classes
{
    /// <summary>
    /// Options
    /// </summary>
    public class Options
    {
        #region Variables
        private string _fileName;
        private bool _showStartMeteoDataDlg = true;
        private Point _mainFormLocation = new Point(0, 0);
        private Size _mainFormSize = new Size(1000, 650);
        #endregion

        #region Constructor

        #endregion

        #region Properties
        /// <summary>
        /// Get or set option file name
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// Get or set is show startup meteo data dialog
        /// </summary>
        public bool ShowStartMeteoDataDlg
        {
            get { return _showStartMeteoDataDlg; }
            set { _showStartMeteoDataDlg = value; }
        }

        /// <summary>
        /// Get or set main form location
        /// </summary>
        public Point MainFormLocation
        {
            get { return _mainFormLocation; }
            set { _mainFormLocation = value; }
        }

        /// <summary>
        /// Get or set main form size
        /// </summary>
        public Size MainFormSize
        {
            get { return _mainFormSize; }
            set { _mainFormSize = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Save options file
        /// </summary>
        public void SaveFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "gb2312", null);
            doc.AppendChild(xmldecl);
            
            XmlElement optionsElem = doc.CreateElement("Options");

            //Start up
            XmlElement startupElem = doc.CreateElement("Startup");
            XmlAttribute meteoDlgAttr = doc.CreateAttribute("ShowMeteoDataDlg");
            XmlAttribute locationAttr = doc.CreateAttribute("MainFormLocation");
            XmlAttribute sizeAttr = doc.CreateAttribute("MainFormSize");

            meteoDlgAttr.InnerText = _showStartMeteoDataDlg.ToString();
            locationAttr.InnerText = _mainFormLocation.X.ToString() + "," + _mainFormLocation.Y.ToString();
            sizeAttr.InnerText = _mainFormSize.Width.ToString() + "," + _mainFormSize.Height.ToString();

            startupElem.Attributes.Append(meteoDlgAttr);
            startupElem.Attributes.Append(locationAttr);
            startupElem.Attributes.Append(sizeAttr);

            optionsElem.AppendChild(startupElem);
            
            doc.AppendChild(optionsElem);

            doc.Save(_fileName);
        }

        /// <summary>
        /// Load options from file
        /// </summary>
        /// <param name="fileName">The file name</param>
        public void LoadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileName = fileName;
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);
                    XmlElement optionsElem = (XmlElement)doc.SelectSingleNode("Options");
                    XmlNode startupNode = optionsElem.GetElementsByTagName("Startup")[0];
                    this._showStartMeteoDataDlg = bool.Parse(startupNode.Attributes["ShowMeteoDataDlg"].InnerText);
                    string loc = startupNode.Attributes["MainFormLocation"].InnerText;
                    _mainFormLocation.X = int.Parse(loc.Split(',')[0]);
                    _mainFormLocation.Y = int.Parse(loc.Split(',')[1]);
                    string size = startupNode.Attributes["MainFormSize"].InnerText;
                    _mainFormSize.Width = int.Parse(size.Split(',')[0]);
                    _mainFormSize.Height = int.Parse(size.Split(',')[1]);
                }
                catch(Exception e)
                {

                }
            }
        }

        #endregion
    }
}
