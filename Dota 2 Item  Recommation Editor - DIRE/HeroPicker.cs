﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dire.net;

namespace dire
{
    public partial class HeroPicker : Form
    {
        public HeroPicker()
        {
            InitializeComponent();
            //ImageList heros = new ImageList();

            //ImageList i = new ImageList();
            //i.ImageSize = new System.Drawing.Size(32, 18);
            ////glComboBox1.ItemHeight = 16;

            //int n = 0;
            //try
            //{
            //    foreach (Hero it in HeroFetcher.AllHeros)
            //    {
            //        i.Images.Add(it.Name, Image.FromFile("cache\\heros\\" + it.DotaName + "_sm.png"));
            //        glComboBox1.Items.Add(new dire.gui.GlComboBoxItem(it.Name, n));
            //        it.ImageListIndex = n;
            //        n++;

            //    }
            //}
            //catch { }
            //glComboBox1.ImageList = i;

            TxtboxAuthor.Text = Environment.UserName;
        }


        private bool autoGenTitle = true;
        private void HeroPicker_Load(object sender, EventArgs e)
        {

        }

        private void glComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Size = new System.Drawing.Size(59, 33);
                pictureBox1.Image = Image.FromFile("cache\\heros\\" + HeroFetcher.AllHeros[glComboBox1.SelectedIndex].DotaName + "_sm.png");

                if (autoGenTitle)
                {
                    txtBoxTitle.Text = Environment.UserName + "'s " + HeroFetcher.AllHeros[glComboBox1.SelectedIndex].Name + " Build";
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                main m = new main(TxtboxAuthor.Text, txtBoxTitle.Text, HeroFetcher.AllHeros[glComboBox1.SelectedIndex], this);
                m.Show();
                this.Visible = false;
            }
            catch { }
        }

        private void txtBoxTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxTitle.Text != Environment.UserName + "'s " + HeroFetcher.AllHeros[glComboBox1.SelectedIndex].Name + " Build")
            {
                autoGenTitle = false;
            }
            else if (txtBoxTitle.Text == "" || txtBoxTitle.Text == string.Empty || txtBoxTitle.Text == Environment.UserName + "'s " + HeroFetcher.AllHeros[glComboBox1.SelectedIndex].Name + " Build")
            {
                autoGenTitle = true;
            }
        }

        private void HeroPicker_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HeroFetcher.GenerateObjects();

                ImageList heros = new ImageList();

                ImageList i = new ImageList();
                i.ImageSize = new System.Drawing.Size(32, 18);
                //glComboBox1.ItemHeight = 16;

                int n = 0;
                try
                {
                    foreach (Hero it in HeroFetcher.AllHeros)
                    {
                        i.Images.Add(it.Name, Image.FromFile("cache\\heros\\" + it.DotaName + "_sm.png"));
                        glComboBox1.Items.Add(new dire.gui.GlComboBoxItem(it.Name, n));
                        it.ImageListIndex = n;
                        n++;

                    }
                }
                catch { }
                glComboBox1.ImageList = i;
            }
            else
            {
                this.glComboBox1.ImageList.Dispose();
                this.glComboBox1.Items.Clear();
            }
        }

        private void loadBuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    new main(editor.build.LoadBuild(openFileDialog.FileName), this).Show();
                    this.Visible = false;
                }
                catch
                {

                }
            }
        }
    }
}
