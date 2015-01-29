using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeteoInfoC.Plugin;

namespace MeteoInfo.Classes
{
    /// <summary>
    /// Plugin
    /// </summary>
    public class Plugin : PluginBase
    {
        #region Variables
        private string _dllFileName;
        private string _className;
        private IPlugin _pluginObject = null;
        private bool _isLoad = false;
        #endregion

        #region Constructor

        #endregion

        #region Properties
        /// <summary>
        /// Get or set dll file name
        /// </summary>
        public string DllFileName
        {
            get { return _dllFileName; }
            set { _dllFileName = value; }
        }

        /// <summary>
        /// Get or set class name
        /// </summary>
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        /// <summary>
        /// Get or set plugin object
        /// </summary>
        public IPlugin PluginObject
        {
            get { return _pluginObject; }
            set 
            { 
                _pluginObject = value;
                if (value != null)
                {
                    this.Name = value.Name;
                    this.Author = value.Author;
                    this.Version = value.Version;
                    this.Description = value.Description;
                }
            }
        }

        /// <summary>
        /// Get or set is loaded
        /// </summary>
        public bool Loaded
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>The name</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get dll path
        /// </summary>
        /// <returns>Dll path</returns>
        public string GetDllPath()
        {
            string path = this._dllFileName;
            int idx = path.IndexOf("Plugin");
            if (idx >= 0)
            {
                path = path.Substring(idx + 7);
            }

            return path;
        }

        /// <summary>
        /// Load the plugin
        /// </summary>
        public override void Load()
        {  
            this._pluginObject.Load();
        }

        /// <summary>
        /// Unload the plugin
        /// </summary>
        public void Unload()
        {
            this._pluginObject.UnLoad();
        }
        #endregion
    }
}
