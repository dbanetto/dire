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
    public partial class main : Form
    {
        public static main Build;

        private Hero hero;
        private string author = string.Empty;
        private string title = string.Empty;
        HeroPicker Caller;
        public main (string Author, string Title , Hero hero , HeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.author = Author;
            this.title = Title;
            this.hero = hero;
             this.Caller = caller;
        }

        public main(build build, HeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.LoadBuild(build);
            this.Caller = caller;
        }

        public main(HeroPicker caller)
        {
            InitializeComponent();
            Build = this;
            this.Caller = caller;
        }

        ListBox.ObjectCollection backup = new ListBox.ObjectCollection(null);
        TabDragger tabDragger;
        private void main_Load(object sender, EventArgs e)
        {
           
            //Hide while the startup happens
            this.Visible = false;
            backup = new ListBox.ObjectCollection(new ListBox());

            //splashscreen.ChangeStatusText("Loading Items icon list");
            GListBox1.ImageList = new ImageList();
            GListBox1.ImageList.ImageSize = new System.Drawing.Size(32, 24);
            
            //Imageindex
            int n = 0;
             try
                {
            foreach (Item it in ItemFetcher.AllItems)
            {
                    GListBox1.ImageList.Images.Add(it.Name, Image.FromFile("cache\\items\\" + it.DotaName + ".png"));
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
                AddNewBuildtab("Core");
                AddNewBuildtab("Situational");
                AddNewBuildtab("Luxary");
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
            Image pic = Image.FromFile(hero.ImagePathSmall);
            this.HeroNameLabel.Text = hero.Name + " - " + title;
            pictureBox1.Image = pic;

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
            UserBuild.WriteBuild("default_" + hero.DotaName + ".txt");
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
                    main.Build.UpdateCurrentCost();
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


            Image pic = Image.FromFile(hero.ImagePathSmall);
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
            //If the dropping type isn't correct, don't show a droppable icon
            e.Effect =
                e.Data.GetDataPresent(typeof(GListBoxItem)) ?
                DragDropEffects.Copy : DragDropEffects.Move;
        }

        private void tabControl1_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(GListBoxItem)))
                return;

            //A Method like AddItem() would be better
            GListBoxItem g_item = (GListBoxItem)e.Data.GetData(typeof(GListBoxItem));
            ListViewItem item = new ListViewItem(g_item.Text, g_item.ImageIndex);
            BuildTab tab = (BuildTab)this.tabControl1.SelectedTab;
            tab.AddItem(item);
        }


        private void GListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            GListBoxItem item = (GListBoxItem)GListBox1.SelectedItem;
            GListBox1.DoDragDrop(item, DragDropEffects.Copy);
        }

        private void GListBox1_DragEnter(object sender, DragEventArgs e)
        {
            //If the dropping type isn't correct, don't show a droppable icon
            e.Effect =
                e.Data.GetDataPresent(typeof(ListView.SelectedIndexCollection)) ?
                DragDropEffects.Copy : DragDropEffects.Move;
        }

        private void GListBox1_DragDrop(object sender, DragEventArgs e)
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
                main.Build.UpdateCurrentCost();
            }
        }


    }
}
