using System;
namespace Ohana3DS
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class FrmTextureInfo : System.Windows.Forms.Form
	{

		//Descartar substituições de formulário para limpar a lista de componentes.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

		//Exigido pelo Windows Form Designer

		private System.ComponentModel.IContainer components;
		//OBSERVAÇÃO: O procedimento a seguir é exigido pelo Windows Form Designer
		//Ele pode ser modificado usando o Windows Form Designer.  
		//Não o modifique usando o editor de códigos.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTextureInfo));
			this.Title = new Ohana3DS.MyWindowTitle();
			this.BtnMinimize = new System.Windows.Forms.Label();
			this.BtnClose = new System.Windows.Forms.Label();
			this.LstModelTextures = new Ohana3DS.MyListview();
			this.SuspendLayout();
			//
			//Title
			//
			this.Title.AutoSize = true;
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Title.ForeColor = System.Drawing.Color.White;
			this.Title.Location = new System.Drawing.Point(278, 0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(85, 25);
			this.Title.TabIndex = 21;
			this.Title.Text = "Textures";
			//
			//BtnMinimize
			//
			this.BtnMinimize.BackColor = System.Drawing.Color.Transparent;
			this.BtnMinimize.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnMinimize.ForeColor = System.Drawing.Color.White;
			this.BtnMinimize.Location = new System.Drawing.Point(564, 4);
			this.BtnMinimize.Name = "BtnMinimize";
			this.BtnMinimize.Size = new System.Drawing.Size(32, 24);
			this.BtnMinimize.TabIndex = 20;
			this.BtnMinimize.Text = "_";
			this.BtnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//BtnClose
			//
			this.BtnClose.BackColor = System.Drawing.Color.Transparent;
			this.BtnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(596, 4);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(32, 24);
			this.BtnClose.TabIndex = 19;
			this.BtnClose.Text = "X";
			this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//LstModelTextures
			//
			this.LstModelTextures.BackColor = System.Drawing.Color.Transparent;
			this.LstModelTextures.ForeColor = System.Drawing.Color.White;
			this.LstModelTextures.Location = new System.Drawing.Point(0, 32);
			this.LstModelTextures.Name = "LstModelTextures";
			this.LstModelTextures.SelectedIndex = -1;
			this.LstModelTextures.Size = new System.Drawing.Size(640, 448);
			this.LstModelTextures.TabIndex = 22;
			this.LstModelTextures.TileHeight = 16;
			//
			//FrmTextureInfo
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
			this.ClientSize = new System.Drawing.Size(640, 480);
			this.Controls.Add(this.LstModelTextures);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.BtnMinimize);
			this.Controls.Add(this.BtnClose);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "FrmTextureInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Textures";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal Ohana3DS.MyWindowTitle Title;
		private System.Windows.Forms.Label withEventsField_BtnMinimize;
		internal System.Windows.Forms.Label BtnMinimize {
			get { return withEventsField_BtnMinimize; }
			set {
				if (withEventsField_BtnMinimize != null) {
					withEventsField_BtnMinimize.Click -= BtnMinimize_Click;
					withEventsField_BtnMinimize.MouseEnter -= Button_MouseEnter;
					withEventsField_BtnMinimize.MouseLeave -= Button_MouseLeave;
				}
				withEventsField_BtnMinimize = value;
				if (withEventsField_BtnMinimize != null) {
					withEventsField_BtnMinimize.Click += BtnMinimize_Click;
					withEventsField_BtnMinimize.MouseEnter += Button_MouseEnter;
					withEventsField_BtnMinimize.MouseLeave += Button_MouseLeave;
				}
			}
		}
		private System.Windows.Forms.Label withEventsField_BtnClose;
		internal System.Windows.Forms.Label BtnClose {
			get { return withEventsField_BtnClose; }
			set {
				if (withEventsField_BtnClose != null) {
					withEventsField_BtnClose.Click -= BtnClose_Click;
					withEventsField_BtnClose.MouseEnter -= BtnClose_MouseEnter;
					withEventsField_BtnClose.MouseLeave -= Button_MouseLeave;
				}
				withEventsField_BtnClose = value;
				if (withEventsField_BtnClose != null) {
					withEventsField_BtnClose.Click += BtnClose_Click;
					withEventsField_BtnClose.MouseEnter += BtnClose_MouseEnter;
					withEventsField_BtnClose.MouseLeave += Button_MouseLeave;
				}
			}
		}
		private Ohana3DS.MyListview withEventsField_LstModelTextures;
		internal Ohana3DS.MyListview LstModelTextures {
			get { return withEventsField_LstModelTextures; }
			set {
				if (withEventsField_LstModelTextures != null) {
					withEventsField_LstModelTextures.SelectedIndexChanged -= LstModelTextures_SelectedIndexChanged;
				}
				withEventsField_LstModelTextures = value;
				if (withEventsField_LstModelTextures != null) {
					withEventsField_LstModelTextures.SelectedIndexChanged += LstModelTextures_SelectedIndexChanged;
				}
			}
		}
		public FrmTextureInfo()
		{
			InitializeComponent();
		}
	}
}
