namespace dire
{
    partial class frmSkillBuild
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picBoxHero = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picBoxAbility1 = new System.Windows.Forms.PictureBox();
            this.picBoxAbility2 = new System.Windows.Forms.PictureBox();
            this.picBoxAbility3 = new System.Windows.Forms.PictureBox();
            this.picBoxAbility4 = new System.Windows.Forms.PictureBox();
            this.picBoxAttrib = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAttrib)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxHero
            // 
            this.picBoxHero.Location = new System.Drawing.Point(12, 12);
            this.picBoxHero.Name = "picBoxHero";
            this.picBoxHero.Size = new System.Drawing.Size(118, 136);
            this.picBoxHero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxHero.TabIndex = 1;
            this.picBoxHero.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // picBoxAbility1
            // 
            this.picBoxAbility1.Location = new System.Drawing.Point(136, 28);
            this.picBoxAbility1.Name = "picBoxAbility1";
            this.picBoxAbility1.Size = new System.Drawing.Size(58, 58);
            this.picBoxAbility1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxAbility1.TabIndex = 3;
            this.picBoxAbility1.TabStop = false;
            // 
            // picBoxAbility2
            // 
            this.picBoxAbility2.Location = new System.Drawing.Point(136, 92);
            this.picBoxAbility2.Name = "picBoxAbility2";
            this.picBoxAbility2.Size = new System.Drawing.Size(58, 58);
            this.picBoxAbility2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxAbility2.TabIndex = 4;
            this.picBoxAbility2.TabStop = false;
            // 
            // picBoxAbility3
            // 
            this.picBoxAbility3.Location = new System.Drawing.Point(136, 155);
            this.picBoxAbility3.Name = "picBoxAbility3";
            this.picBoxAbility3.Size = new System.Drawing.Size(58, 58);
            this.picBoxAbility3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxAbility3.TabIndex = 5;
            this.picBoxAbility3.TabStop = false;
            // 
            // picBoxAbility4
            // 
            this.picBoxAbility4.Location = new System.Drawing.Point(136, 218);
            this.picBoxAbility4.Name = "picBoxAbility4";
            this.picBoxAbility4.Size = new System.Drawing.Size(58, 58);
            this.picBoxAbility4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxAbility4.TabIndex = 6;
            this.picBoxAbility4.TabStop = false;
            // 
            // picBoxAttrib
            // 
            this.picBoxAttrib.Location = new System.Drawing.Point(136, 281);
            this.picBoxAttrib.Name = "picBoxAttrib";
            this.picBoxAttrib.Size = new System.Drawing.Size(58, 58);
            this.picBoxAttrib.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxAttrib.TabIndex = 7;
            this.picBoxAttrib.TabStop = false;
            // 
            // frmSkillBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 368);
            this.Controls.Add(this.picBoxAttrib);
            this.Controls.Add(this.picBoxAbility4);
            this.Controls.Add(this.picBoxAbility3);
            this.Controls.Add(this.picBoxAbility2);
            this.Controls.Add(this.picBoxAbility1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBoxHero);
            this.Name = "frmSkillBuild";
            this.Text = "DIRE - Skill Build";
            this.Load += new System.EventHandler(this.SkillBuild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAbility4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAttrib)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxHero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBoxAbility1;
        private System.Windows.Forms.PictureBox picBoxAbility2;
        private System.Windows.Forms.PictureBox picBoxAbility3;
        private System.Windows.Forms.PictureBox picBoxAbility4;
        private System.Windows.Forms.PictureBox picBoxAttrib;
    }
}