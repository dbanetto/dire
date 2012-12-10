using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using dire.net;

namespace dire
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            
        }  

        private void Splash_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        new public void Update()
        {
            this.label1.Text = "Cache Updating - " + cache.cache.status;
            this.label1.Update();
            if (this.progressBar1.Value < this.progressBar1.Maximum)
            {
                this.progressBar1.Value++;
            }
            else
            {
                this.progressBar1.Value = this.progressBar1.Minimum;
            }this.progressBar1.Update();
            
            base.Update();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.Visible = true;
        }
    }
}
