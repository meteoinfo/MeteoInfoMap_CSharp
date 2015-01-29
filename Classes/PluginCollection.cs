using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MeteoInfo.Classes
{
    /// <summary>
    /// Plugin collection
    /// </summary>
    public class PluginCollection:List<Plugin>
    {
        #region Variables
        private string _pluginPath;
        private string _configFile;

        #endregion

        #region Properties
        /// <summary>
        /// Get or set configure file
        /// </summary>
        public string ConfigFile
        {
            get { return _configFile; }
            set { _configFile = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save Plugin collection configure file
        /// </summary>
        /// <param name="fileName">The configure file name</param>
        public void SaveConfigFile(string fileName)
        {            
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "gb2312", null);
            doc.AppendChild(xmldecl);
            //XmlElement root = doc.DocumentElement;
            XmlElement pluginsElem = doc.CreateElement("Plugins");
            foreach (Plugin plugin in this)
            {
                XmlElement pluginElem = doc.CreateElement("Plugin");

                XmlAttribute nameAttr = doc.CreateAttribute("Name");
                XmlAttribute authorAttr = doc.CreateAttribute("Author");
                XmlAttribute versionAttr = doc.CreateAttribute("Version");
                XmlAttribute descriptionAttr = doc.CreateAttribute("Description");
                XmlAttribute dllPathAttr = doc.CreateAttribute("DllPath");
                XmlAttribute classNameAttr = doc.CreateAttribute("ClassName");
                XmlAttribute isLoadAttr = doc.CreateAttribute("IsLoad");

                nameAttr.InnerText = plugin.Name;
                authorAttr.InnerText = plugin.Author;
                versionAttr.InnerText = plugin.Version;
                descriptionAttr.InnerText = plugin.Description;
                dllPathAttr.InnerText = plugin.GetDllPath();
                classNameAttr.InnerText = plugin.ClassName;
                isLoadAttr.InnerText = plugin.Loaded.ToString();

                pluginElem.Attributes.Append(nameAttr);
                pluginElem.Attributes.Append(authorAttr);
                pluginElem.Attributes.Append(versionAttr);
                pluginElem.Attributes.Append(descriptionAttr);
                pluginElem.Attributes.Append(dllPathAttr);
                pluginElem.Attributes.Append(classNameAttr);
                pluginElem.Attributes.Append(isLoadAttr);

                pluginsElem.AppendChild(pluginElem);
            }
            doc.AppendChild(pluginsElem);

            doc.Save(fileName);
        }

        /// <summary>
        /// Load plugin collection from the configure file
        /// </summary>
        /// <param name="fileName">The configure file name</param>
        public void LoadConfigFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                _configFile = fileName;
                _pluginPath = Path.GetDirectoryName(fileName);
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                //XmlElement root = doc.DocumentElement;
                XmlNode pluginsNode = doc.SelectSingleNode("Plugins");
                this.Clear();
                foreach (XmlNode pluginNode in pluginsNode)
                {
                    Plugin plugin = new Plugin();
                    plugin.Name = pluginNode.Attributes["Name"].InnerText;
                    plugin.Author = pluginNode.Attributes["Author"].InnerText;
                    plugin.Version = pluginNode.Attributes["Version"].InnerText;
                    plugin.Description = pluginNode.Attributes["Description"].InnerText;
                    string dllPath = pluginNode.Attributes["DllPath"].InnerText;
                    plugin.DllFileName = Path.Combine(_pluginPath, dllPath);
                    plugin.ClassName = pluginNode.Attributes["ClassName"].InnerText;
                    plugin.Loaded = bool.Parse(pluginNode.Attributes["IsLoad"].InnerText);
                    if (File.Exists(plugin.DllFileName))
                        this.Add(plugin);
                }
            }
        }
        #endregion
    }
}
