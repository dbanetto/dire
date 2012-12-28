using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dire.net;

using dire.gui;
namespace dire
{
    public partial class frmSkillBuild : Form
    {
        private Hero hero = null;
        private SkillGroup[] SkillGroups = new SkillGroup[25];
        public frmSkillBuild()
        {
            InitializeComponent();

            

            HeroFetcher.GenerateObjects();
            AbilitiesFetcher.GenerateObjects();

            hero = HeroFetcher.AllHeros[new Random().Next(0, HeroFetcher.AllHeros.Length)];
            //hero = HeroFetcher.ResloveDotaNameToHero("enchantress");
        }

        public frmSkillBuild(Hero Hero)
        {
            InitializeComponent();
            this.hero = Hero;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void SkillBuild_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < SkillGroups.Length; i++)
            {
                SkillGroups[i] = new SkillGroup(i + 1);
                SkillGroups[i].Location = new Point(200 + (SkillGroup.X_OFF_SET * i), 13);
                SkillGroups[i].Font = new Font("Microsoft Sans Serif", 6.4f, FontStyle.Bold);

                this.Controls.Add(SkillGroups[i]);
            }
            picBoxHero.Image = Image.FromFile(hero.ImagePathVert);

            label1.Text = hero.Name;

            PictureBox[] picBoxs = { picBoxAbility1, picBoxAbility2, picBoxAbility3, picBoxAbility4 };
            int n = 0;
            string lastAbility = "qwerty";
            for (int i = 0; i < hero.Abilities.Length; i++)
            {
                if (!hero.Abilities[i].DotaName.Replace(hero.DotaName + "_", "").Contains(lastAbility))
                {
                    picBoxs[n].Image = Image.FromFile("cache/abilities/" + hero.Abilities[i].DotaName + ".png");
                    lastAbility = hero.Abilities[i].DotaName.Replace(hero.DotaName + "_", "") ;
                    n++;
                }
            }

            
        }
    }
}
