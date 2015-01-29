using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeteoInfo.Classes;
using MeteoInfoC.Global;

namespace MeteoInfo.Forms
{
    public partial class frmPluginManager : Form
    {
        private frmMain _parent;
        List<Plugin> _plugins = new List<Plugin>();

        public frmPluginManager(Form parent)
        {
            InitializeComponent();

            _parent = (frmMain)parent;
            Initialize();
        }

        private void Initialize()
        {
            this._plugins = _parent.Plugins;
            this.UpdatePluginCheckList();
        }

        private void UpdatePluginCheckList()
        {
            this.CLB_Plugins.Items.Clear();
            foreach (Plugin plugin in _plugins)
            {
                this.CLB_Plugins.Items.Add(plugin, plugin.Loaded);
            }
        }

        private void CLB_Plugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Plugin plugin = (Plugin)CLB_Plugins.SelectedItem;
            String detailStr = "Name: " + plugin.Name
                + Environment.NewLine + "Author: " + plugin.Author
                + Environment.NewLine + "Version: " + plugin.Version
                + Environment.NewLine + "Description: " + plugin.Description
                + Environment.NewLine + "DLL Path: " + plugin.GetDllPath()
                + Environment.NewLine + "Class Name: " + plugin.ClassName;
            this.TB_PluginDetail.Text = detailStr;
        }

        private void CLB_Plugins_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (CLB_Plugins.SelectedItem == null)
                return;

            Plugin plugin = (Plugin)CLB_Plugins.SelectedItem;
            if (CLB_Plugins.GetItemChecked(CLB_Plugins.SelectedIndex))
            {
                if (plugin.Loaded)
                {
                    _parent.UnloadPlugin(plugin);
                }
            }
            else
            {
                if (!plugin.Loaded)
                {
                    _parent.LoadPlugin(plugin);
                }
            }
        }

        private void B_UpdateList_Click(object sender, EventArgs e)
        {
            List<Plugin> plugins = new List<Plugin>();
            
            String pluginPath = Application.StartupPath + "\\Plugin";
            if (System.IO.Directory.Exists(pluginPath))
            {
                List<String> fileNames = GlobalUtil.GetAllFilesByFolder(pluginPath, "*.dll", true);
                foreach (String fn in fileNames)
                {
                    Plugin plugin = _parent.ReadPlugin(fn);
                    if (plugin != null)
                    {
                        plugins.Add(plugin);
                    }
                }

                List<string> pluginNames = new List<string>();
                foreach (Plugin plugin in _plugins)
                    pluginNames.Add(plugin.Name);

                List<string> newPluginNames = new List<string>();
                foreach (Plugin plugin in plugins)                
                    newPluginNames.Add(plugin.Name);

                for (int i = 0; i < _plugins.Count; i++)
                {
                    if (!newPluginNames.Contains(_plugins[i].Name))
                    {
                        _parent.RemovePlugin(_plugins[i]);
                        _plugins.RemoveAt(i);
                        i -= 1;
                    }
                }

                foreach (Plugin plugin in plugins)
                {
                    if (!pluginNames.Contains(plugin.Name))
                    {
                        _plugins.Add(plugin);
                        _parent.AddPlugin(plugin);
                    }
                }
                
                this.UpdatePluginCheckList();
            }
        }
    }
}
