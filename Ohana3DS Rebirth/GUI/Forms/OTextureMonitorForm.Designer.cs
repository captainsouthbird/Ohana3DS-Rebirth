namespace Ohana3DS_Rebirth.GUI.Forms
{
    partial class OTextureMonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OTextureMonitorForm));
            this.BtnOk = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnBrowseFolder = new Ohana3DS_Rebirth.GUI.OButton();
            this.TxtOutFolder = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyOutFolder = new Ohana3DS_Rebirth.GUI.OLabel();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.BtnBrowseFolder);
            this.ContentContainer.Controls.Add(this.TxtOutFolder);
            this.ContentContainer.Controls.Add(this.LblDummyOutFolder);
            this.ContentContainer.Location = new System.Drawing.Point(1, 1);
            this.ContentContainer.Size = new System.Drawing.Size(382, 382);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnBrowseFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_tick;
            this.BtnOk.Location = new System.Drawing.Point(237, 347);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 32;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Centered = true;
            this.BtnCancel.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_block;
            this.BtnCancel.Location = new System.Drawing.Point(307, 347);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(64, 24);
            this.BtnCancel.TabIndex = 31;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Centered = true;
            this.BtnBrowseFolder.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.BtnBrowseFolder.Location = new System.Drawing.Point(339, 49);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.BtnBrowseFolder.TabIndex = 27;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // TxtOutFolder
            // 
            this.TxtOutFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtOutFolder.CharacterWhiteList = null;
            this.TxtOutFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOutFolder.Location = new System.Drawing.Point(11, 49);
            this.TxtOutFolder.Name = "TxtOutFolder";
            this.TxtOutFolder.Size = new System.Drawing.Size(322, 20);
            this.TxtOutFolder.TabIndex = 26;
            this.TxtOutFolder.Text = "C:\\";
            // 
            // LblDummyOutFolder
            // 
            this.LblDummyOutFolder.AutomaticSize = true;
            this.LblDummyOutFolder.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyOutFolder.Centered = false;
            this.LblDummyOutFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyOutFolder.Location = new System.Drawing.Point(11, 26);
            this.LblDummyOutFolder.Name = "LblDummyOutFolder";
            this.LblDummyOutFolder.Size = new System.Drawing.Size(84, 17);
            this.LblDummyOutFolder.TabIndex = 25;
            this.LblDummyOutFolder.Text = "Textures folder:";
            // 
            // OTextureMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.KeyPreview = true;
            this.Name = "OTextureMonitorForm";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "Set Texture Monitor Folder";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
            this.Load += new System.EventHandler(this.OTextureMonitorForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OTextureMonitorForm_KeyDown);
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OButton BtnOk;
        private OButton BtnCancel;
        private OButton BtnBrowseFolder;
        private OTextBox TxtOutFolder;
        private OLabel LblDummyOutFolder;
    }
}