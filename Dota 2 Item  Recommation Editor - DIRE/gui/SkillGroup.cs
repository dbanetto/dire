using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dire.gui
{
    class SkillGroup : GroupBox
    {
        private RadioButton[] radioButtons = new RadioButton[5];
        public static int X_OFF_SET = 32;
        public SkillGroup(int Level)
        {

            this.Size = new Size(X_OFF_SET, 326);
            this.Text = "" + Level;
            if (Level < 10)
            {
                this.Text = "0" + Level;
            }
            else
            {
                this.Text = Level.ToString();
            }
            if (!(Level % 2 == 0)) //Not even
            {
                //this.BackColor = Color.DarkGray;
            }
            else
            {
                this.BackColor = Color.LightGray;
            }

            for (int i = 0; i < radioButtons.Length; i++ )
            {
                this.radioButtons[i] = new RadioButton();
                
                this.Controls.Add(radioButtons[i]);
                
                this.radioButtons[i].AutoSize = true;
                this.radioButtons[i].Size = new System.Drawing.Size(14, 13);
                int offset = ((this.Size.Width - this.radioButtons[i].Size.Width) / 2);
                this.radioButtons[i].Location = new System.Drawing.Point(this.Location.X + offset , this.Location.Y + (i * 64) + 44);
                this.radioButtons[i].TabIndex = i * Level;
                this.radioButtons[i].TabStop = true;
                this.radioButtons[i].UseVisualStyleBackColor = true;
            }


        }

    }
}
