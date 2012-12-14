using System;
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
    public partial class SkillBuild : Form
    {
        private Hero hero = null;

        public SkillBuild()
        {
            InitializeComponent();

            HeroFetcher.GenerateObjects();
            AbilitiesFetcher.GenerateObjects();

            hero = HeroFetcher.AllHeros[new Random().Next(0, HeroFetcher.AllHeros.Length)];
            //hero = HeroFetcher.ResloveDotaNameToHero("furion");
        }

        public SkillBuild(Hero Hero)
        {
            InitializeComponent();
            this.hero = Hero;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SkillBuild_Load(object sender, EventArgs e)
        {

            picBoxHero.Image = Image.FromFile(hero.ImagePathLarge);

            label1.Text = hero.Name;

            picBoxAbility1.Image = Image.FromFile( "cache/abilities/" + hero.Abilities[0].DotaName + ".png");
            picBoxAbility2.Image = Image.FromFile("cache/abilities/" + hero.Abilities[1].DotaName + ".png");
            picBoxAbility3.Image = Image.FromFile("cache/abilities/" + hero.Abilities[2].DotaName + ".png");
            picBoxAbility4.Image = Image.FromFile("cache/abilities/" + hero.Abilities[3].DotaName + ".png");

            
        }
    }
}
