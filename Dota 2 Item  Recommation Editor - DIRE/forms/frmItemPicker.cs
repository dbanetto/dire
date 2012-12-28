using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;


using dire.editor;
using dire.net;
using dire.gui;
using System.Threading;
namespace dire
{
    public partial class frmItemPicker : Form
    {
        public static frmItemPicker Build;

        private Hero hero;
        private string author = string.Empty;
        private string title = string.Empty;
        frmHeroPicker Caller;
        public frmItemPicker (string Author, string Title , Hero hero , frmHeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.author = Author;
            this.title = Title;
            this.hero = hero;
             this.Caller = caller;
        }

        public frmItemPicker(build build, frmHeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.LoadBuild(build);
            this.Caller = caller;
        }

        public frmItemPicker(frmHeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.Caller = caller;
        }

        ListBox.ObjectCollection backup = new ListBox.ObjectCollection(null);
        TabDragger tabDragger;
        private void main_Load(object sender, EventArgs e)
        {
           //Generate objects
            ItemFetcher.GenerateObjects();

            //Custom items (soul ring recipe etc)
            if (File.Exists("cache/items.custom.json"))
            {
                ItemFetcher.GenerateObjects("cache/items.custom.json", true);
            }

            //Hide while the startup happens
            this.Visible = false;
            backup = new ListBox.ObjectCollection(new ListBox());

            GListBox1.ImageList = new ImageList();
            GListBox1.ImageList.ImageSize = new System.Drawing.Size(32, 24);
            
            //Imageindex
            int n = 0;
             try
                {
            foreach (Item it in ItemFetcher.AllItems)
            {
                try { GListBox1.ImageList.Images.Add(it.Name, Image.FromFile("cache\\items\\" + it.DotaName + ".png")); }
                catch
                {
                    GListBox1.ImageList.Images.Add(it.Name, Image.FromFile("cache\\items\\" + it.img ));
                }
                    GListBox1.Items.Add(new GListBoxItem(it.Name, n));
                    it.ImageListIndex = n;
                    n++;
               
            }
            }
                catch (Exception ex)
             {
                 MessageBox.Show("Error while loading files. Please verify cache and try again.","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            //Backup for searching
            backup.AddRange(GListBox1.Items);

            if (tabControl1.TabCount == 0)
            {
                //Creating Default tabs
                AddNewBuildtab("Starting Items");
                AddNewBuildtab("Early Game");
                AddNewBuildtab("Core Items");
                AddNewBuildtab("Situational");
                AddNewBuildtab("Luxury");
            }
            else
            {
                foreach (BuildTab b in tabControl1.TabPages)
                {
                    b.ItemList.LargeImageList = GListBox1.ImageList;
                    b.ItemList.SmallImageList = GListBox1.ImageList;
                }
            }
            //Change the Picture and text to match hero and build
            Image pic = Image.FromFile(hero.ImagePath);
            this.HeroNameLabel.Text = hero.Name + " - " + title;
            pictureBox1.Image = pic;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //Enable Tad dragger
            tabDragger = new TabDragger(tabControl1, TabDragBehavior.TabDragArrange);

            this.Visible = true;
            
            this.Text = "DIRE - Build";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                if (textBox1.Text.TrimStart() == "")
                {
                    GListBox1.Items.AddRange(backup);
                }

                GListBox1.Items.Clear();

                foreach (GListBoxItem i in backup)
                {
                    if (i.Text.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        GListBox1.Items.Add(i);
                    }
                }
            }

        private void GListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GListBoxItem i = (GListBoxItem)(GListBox1.SelectedItem);

            SetInfoBox(ItemFetcher.AllItems[i.ImageIndex]);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text.ToLower().Replace("<br />", "\n");
        }

        private void GListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                GListBoxItem b = (GListBoxItem)GListBox1.SelectedItem;

                ListViewItem item = new ListViewItem(b.Text, b.ImageIndex);
                BuildTab i = (BuildTab)this.tabControl1.SelectedTab;
                i.AddItem(item);
            }
            catch { }
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuildTab b = new BuildTab(Microsoft.VisualBasic.Interaction.InputBox( "Enter Group Name" , "DIRE -Build"));

            b.ItemList.LargeImageList = GListBox1.ImageList;
            b.ItemList.SmallImageList = GListBox1.ImageList;
            tabControl1.Controls.Add(b);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.Controls.Remove(tabControl1.SelectedTab);
            }
            catch { }
        }

        private void createBuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create The Build
            List<group> groups = new List<group>();

            foreach (TabPage i in this.tabControl1.TabPages)
            {
                BuildTab j = (BuildTab)i;
                groups.Add(j.Group);
            }

            dire.editor.build UserBuild = new build(hero, author, title, groups);
            if (frmSettings.Settings.SaveInDota)
            {
                UserBuild.WriteBuild(frmSettings.Settings.DotaPath + "\\dota\\itembuilds\\default_" + hero.DotaName + ".txt", !frmSettings.Settings.DotaPathOverride);
            }
            else
            {
                UserBuild.WriteBuild("default_" + hero.DotaName + ".txt");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void SetInfoBox(Item i)
        {
            richTextBox1.Text = string.Empty;
            
            richTextBox1.Text += "Cost: " + i.Cost;

            if (i.Attributes != string.Empty)
            {
                string editted = i.Attributes.Replace("</span>", "").Replace("+ <span class=\"attribVal\">", "").Replace("<span class=\"attribVal\">", "").Replace("<span class=\"attribValText\">", "").Replace("<br />", "\n");

                richTextBox1.Text += "\n" + "Attributes: " + editted + "\n";
            }
            if (i.Components != null)
            {
                richTextBox1.Text += "\n" + "Components: ";
                richTextBox1.Text += ItemFetcher.ResloveDotaNameToName( i.Components[0] );
                for (int n = 1; n < i.Components.Length; n++)
                {
                    richTextBox1.Text += ", " + ItemFetcher.ResloveDotaNameToName( i.Components[n] );
                }
                richTextBox1.Text += "\n";
            }
            if (i.Discription != string.Empty)
            {
                richTextBox1.Text += "\n" + "Discription: " + i.Discription.Replace("<br />", "\n");
            }

#if DEBUG
             richTextBox1.Text += "\n" + "Dota Name: " + i.DotaName;
#endif

        }

        public void UpdateCurrentCost()
        {
            try
            {
                BuildTab b = (BuildTab)tabControl1.SelectedTab;
                this.CostLabel.Text = "";
                if (b.Cost != 0)
                {
                    this.CostLabel.Text = b.Group.GroupTitle + "'s Total Cost : " + b.Cost;
                }
                
                int cost = 0;
                foreach (TabPage i in this.tabControl1.TabPages)
                {
                    BuildTab j = (BuildTab)i;
                    cost += j.Cost;
                }
                if (cost != 0)
                {
                    this.CostLabel.Text += " Total Build Cost : " + cost;
                }
            }
            catch {
                
            }

        }

        public void UpdateAllCost()
        {
            int cost = 0;
            foreach (TabPage i in this.tabControl1.TabPages)
            {
                BuildTab j = (BuildTab)i;
                cost += j.Cost;
            }
            this.CostLabel.Text += " Total Cost : " + cost;

        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Caller.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GListBoxItem b = (GListBoxItem)GListBox1.SelectedItem;

                ListViewItem item = new ListViewItem(b.Text, b.ImageIndex);
                BuildTab i = (BuildTab)this.tabControl1.SelectedTab;
                i.AddItem(item);
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                BuildTab b = (BuildTab)tabControl1.SelectedTab;
                foreach (ListViewItem i in b.ItemList.SelectedItems)
                {
                    b.Group.items.Remove( ItemFetcher.AllItems[i.ImageIndex]);
                    b.ItemList.Items.Remove(i);
                    b.Cost -= ItemFetcher.AllItems[i.ImageIndex].Cost;
                    frmItemPicker.Build.UpdateCurrentCost();
                }
            }
            catch
            {
                
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentCost();
        }

        public void LoadBuild(build Build)
        {
            this.title = Build.Title;
            this.hero = Build.Hero;
            this.author = Build.Author;

            //Imageindex
            int n = 0;
            try
            {
                foreach (Item it in ItemFetcher.AllItems)
                {
                    it.ImageListIndex = n;
                    n++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading files. Please verify cache and try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Image pic = Image.FromFile(hero.ImagePath);
            this.HeroNameLabel.Text = hero.Name + " - " + title;
            pictureBox1.Image = pic;

            foreach (group g in Build.Items)
            {
                //Add item Groups
                BuildTab b = new BuildTab(g);

                b.ItemList.LargeImageList = this.GListBox1.ImageList;
                b.ItemList.SmallImageList = this.GListBox1.ImageList;
                tabControl1.Controls.Add(b);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            UpdateCurrentCost();
        }

        private void AddNewBuildtab(string name)
        {
            BuildTab b = new BuildTab(name);

            //Add ImageList to the tab
            b.ItemList.LargeImageList = this.GListBox1.ImageList;
            b.ItemList.SmallImageList = this.GListBox1.ImageList;
            tabControl1.Controls.Add(b);
        }

        //Drag N Drop functionality

        private void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (frmSettings.Settings.DragAndDrop)
            {
                //If the dropping type isn't correct, don't show a droppable icon
                e.Effect =
                    e.Data.GetDataPresent(typeof(GListBoxItem)) ?
                    DragDropEffects.Copy : DragDropEffects.Move;
            }
        }

        private void tabControl1_DragDrop(object sender, DragEventArgs e)
        {
            if (frmSettings.Settings.DragAndDrop)
            {
                if (!e.Data.GetDataPresent(typeof(GListBoxItem)))
                    return;

                //A Method like AddItem() would be better
                GListBoxItem g_item = (GListBoxItem)e.Data.GetData(typeof(GListBoxItem));
                ListViewItem item = new ListViewItem(g_item.Text, g_item.ImageIndex);
                BuildTab tab = (BuildTab)this.tabControl1.SelectedTab;
                tab.AddItem(item);
            }
        }


        private void GListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (frmSettings.Settings.DragAndDrop)
            {
                GListBoxItem item = (GListBoxItem)GListBox1.SelectedItem;

                GListBox1.DoDragDrop(item, DragDropEffects.Copy);
            }
            
        }

        private void GListBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (frmSettings.Settings.DragAndDrop)
            {
                //If the dropping type isn't correct, don't show a droppable icon
                e.Effect =
                    e.Data.GetDataPresent(typeof(ListView.SelectedIndexCollection)) ?
                    DragDropEffects.Copy : DragDropEffects.Move;
            }
        }

        private void GListBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (frmSettings.Settings.DragAndDrop)
            {
                if (!e.Data.GetDataPresent(typeof(ListViewItem)))
                    return;

                // This should be maybe be an RemoveItem() method
                BuildTab b = (BuildTab)tabControl1.SelectedTab;
                foreach (ListViewItem i in b.ItemList.SelectedItems)
                {
                    b.Group.items.Remove(ItemFetcher.AllItems[i.ImageIndex]);
                    b.ItemList.Items.Remove(i);
                    b.Cost -= ItemFetcher.AllItems[i.ImageIndex].Cost;
                    frmItemPicker.Build.UpdateCurrentCost();
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSettings().Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().Show();
        }


    }
}
