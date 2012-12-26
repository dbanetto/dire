using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace dire
{
    public partial class frmSettings : Form
    {
        public static Setting Settings;


        public frmSettings()
        {
            InitializeComponent();

            try
            {
                this.LoadSettings("cache/settings.json");
            }
            catch
            {
                this.WriteSettings("cache/settings.json");
                this.LoadSettings("cache/settings.json");
            }
        }

        private void checkDotaFolder_CheckedChanged(object sender, EventArgs e)
        {
            groupFolderSettings.Enabled = checkDotaFolder.Checked;
        }

        private void checkDragDrop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDragDrop.Checked && this.Visible)
            {
                if (MessageBox.Show( "Warning: Enabling Drag and Drop will disable the item list's ability to update the infomation box below it,\nand the ability to double click to add.\nDo you still wish to enable Drag and Drop?" , "DIRE - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                {
                    checkDragDrop.Checked = false;
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "dota.exe|dota.exe";
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = openFileDialog1.FileName.Remove(openFileDialog1.FileName.Length - 9);
                txtBoxDota2Folder.Text = path;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            groupFolderSettings.Enabled = checkDotaFolder.Checked;
        }

        public void LoadSettings(string path)
        {
            string jsondata = string.Empty;
            jsondata = File.ReadAllText(path);
            Settings = JsonConvert.DeserializeObject<Setting>(jsondata);

            this.txtBoxDota2Folder.Text = Settings.DotaPath;
            this.checkDragDrop.Checked = Settings.DragAndDrop;
            this.checkDotaFolder.Checked = Settings.SaveInDota;
            this.checkBox1.Checked = Settings.DotaPathOverride;
        }


        //public static void LoadSettings (string path) {
        //    string jsondata = string.Empty;
        //    jsondata = File.ReadAllText(path);
        //    Settings = JsonConvert.DeserializeObject<Setting>(jsondata);
        //}

        public void WriteSettings(string path)
        {
            Settings.DotaPathOverride = this.checkBox1.Checked;
            Settings.SaveInDota = this.checkDotaFolder.Checked;
            Settings.DragAndDrop = this.checkDragDrop.Checked;
            Settings.DotaPath = this.txtBoxDota2Folder.Text;

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonWriter.Formatting = Formatting.Indented;
            //Start Setting object
            jsonWriter.WriteStartObject();
            
            //Drag and Drop
            
            jsonWriter.WritePropertyName("draganddrop");
            jsonWriter.WriteValue(Settings.DragAndDrop);
            //jsonWriter.WriteEndObject();

            //Save Dota
            jsonWriter.WritePropertyName("savedota");
            jsonWriter.WriteValue(Settings.SaveInDota);

            //Override
            jsonWriter.WritePropertyName("override");
            jsonWriter.WriteValue(Settings.DotaPathOverride);


            //Dota path
            jsonWriter.WritePropertyName("dotapath");
            jsonWriter.WriteValue(Settings.DotaPath);
            
            //End of Setting object
            jsonWriter.WriteEndObject();

            string output = sw.ToString();
            jsonWriter.Close();
            sw.Close();

            File.WriteAllText(path, output);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteSettings("cache/settings.json");
        }
    }


    public class Setting {

        //Drag and Drop
        [JsonProperty(PropertyName = "draganddrop")]
        public bool DragAndDrop { get; set; }
        
        [JsonProperty(PropertyName = "savedota")]
        public bool SaveInDota { get; set; }
        
        [JsonProperty(PropertyName = "override")]
        public bool DotaPathOverride { get; set; }
        
        [JsonProperty(PropertyName = "dotapath")]
        public string DotaPath { get; set; }

    }

}
