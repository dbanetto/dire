namespace dire
{
    partial class HeroPicker
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
            this.button1 = new System.Windows.Forms.Button();
            this.TxtboxAuthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.glComboBox1 = new dire.gui.GlComboBox();
            this.heroCombobox = new dire.gui.GlComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TxtboxAuthor
            // 
            this.TxtboxAuthor.Location = new System.Drawing.Point(51, 68);
            this.TxtboxAuthor.Name = "TxtboxAuthor";
            this.TxtboxAuthor.Size = new System.Drawing.Size(171, 20);
            this.TxtboxAuthor.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Author";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Title";
            // 
            // txtBoxTitle
            // 
            this.txtBoxTitle.Location = new System.Drawing.Point(51, 42);
            this.txtBoxTitle.Name = "txtBoxTitle";
            this.txtBoxTitle.Size = new System.Drawing.Size(171, 20);
            this.txtBoxTitle.TabIndex = 2;
            this.txtBoxTitle.TextChanged += new System.EventHandler(this.txtBoxTitle_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hero";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(228, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 33);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // glComboBox1
            // 
            this.glComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.glComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.glComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.glComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.glComboBox1.FormattingEnabled = true;
            this.glComboBox1.ImageList = null;
            this.glComboBox1.ItemHeight = 18;
            this.glComboBox1.Location = new System.Drawing.Point(51, 12);
            this.glComboBox1.Name = "glComboBox1";
            this.glComboBox1.Size = new System.Drawing.Size(171, 24);
            this.glComboBox1.TabIndex = 1;
            this.glComboBox1.SelectedIndexChanged += new System.EventHandler(this.glComboBox1_SelectedIndexChanged);
            // 
            // heroCombobox
            // 
            this.heroCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.heroCombobox.ImageList = null;
            this.heroCombobox.Location = new System.Drawing.Point(1, 1);
            this.heroCombobox.Name = "heroCombobox";
            this.heroCombobox.Size = new System.Drawing.Size(171, 21);
            this.heroCombobox.TabIndex = 0;
            // 
            // HeroPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 99);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.glComboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtboxAuthor);
            this.Controls.Add(this.button1);
            this.Name = "HeroPicker";
            this.Text = "HeroPicker";
            this.Load += new System.EventHandler(this.HeroPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtboxAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxTitle;
        private dire.gui.GlComboBox heroCombobox;
        private gui.GlComboBox glComboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}