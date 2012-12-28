namespace dire
{
    partial class frmSettings
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
            this.checkDragDrop = new System.Windows.Forms.CheckBox();
            this.checkDotaFolder = new System.Windows.Forms.CheckBox();
            this.txtBoxDota2Folder = new System.Windows.Forms.TextBox();
            this.groupFolderSettings = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupFolderSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkDragDrop
            // 
            this.checkDragDrop.AutoSize = true;
            this.checkDragDrop.Location = new System.Drawing.Point(13, 13);
            this.checkDragDrop.Name = "checkDragDrop";
            this.checkDragDrop.Size = new System.Drawing.Size(132, 17);
            this.checkDragDrop.TabIndex = 0;
            this.checkDragDrop.Text = "Enable Drag and Drop";
            this.checkDragDrop.UseVisualStyleBackColor = true;
            this.checkDragDrop.CheckedChanged += new System.EventHandler(this.checkDragDrop_CheckedChanged);
            // 
            // checkDotaFolder
            // 
            this.checkDotaFolder.AutoSize = true;
            this.checkDotaFolder.Location = new System.Drawing.Point(13, 37);
            this.checkDotaFolder.Name = "checkDotaFolder";
            this.checkDotaFolder.Size = new System.Drawing.Size(126, 17);
            this.checkDotaFolder.TabIndex = 1;
            this.checkDotaFolder.Text = "Save in Dota 2 folder";
            this.checkDotaFolder.UseVisualStyleBackColor = true;
            this.checkDotaFolder.CheckedChanged += new System.EventHandler(this.checkDotaFolder_CheckedChanged);
            // 
            // txtBoxDota2Folder
            // 
            this.txtBoxDota2Folder.Location = new System.Drawing.Point(79, 19);
            this.txtBoxDota2Folder.Name = "txtBoxDota2Folder";
            this.txtBoxDota2Folder.Size = new System.Drawing.Size(113, 20);
            this.txtBoxDota2Folder.TabIndex = 2;
            // 
            // groupFolderSettings
            // 
            this.groupFolderSettings.Controls.Add(this.checkBox1);
            this.groupFolderSettings.Controls.Add(this.btnBrowseFolder);
            this.groupFolderSettings.Controls.Add(this.label1);
            this.groupFolderSettings.Controls.Add(this.txtBoxDota2Folder);
            this.groupFolderSettings.Enabled = false;
            this.groupFolderSettings.Location = new System.Drawing.Point(13, 60);
            this.groupFolderSettings.Name = "groupFolderSettings";
            this.groupFolderSettings.Size = new System.Drawing.Size(259, 66);
            this.groupFolderSettings.TabIndex = 3;
            this.groupFolderSettings.TabStop = false;
            this.groupFolderSettings.Text = "Dota 2 Folder Settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Override existing file";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(198, 17);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(55, 23);
            this.btnBrowseFolder.TabIndex = 4;
            this.btnBrowseFolder.Text = "Browse";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dota 2 Path:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupFolderSettings);
            this.Controls.Add(this.checkDotaFolder);
            this.Controls.Add(this.checkDragDrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "DIRE - Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupFolderSettings.ResumeLayout(false);
            this.groupFolderSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkDragDrop;
        private System.Windows.Forms.CheckBox checkDotaFolder;
        private System.Windows.Forms.TextBox txtBoxDota2Folder;
        private System.Windows.Forms.GroupBox groupFolderSettings;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}