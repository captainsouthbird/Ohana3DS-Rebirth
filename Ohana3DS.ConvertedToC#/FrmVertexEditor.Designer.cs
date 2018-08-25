using System;
namespace Ohana3DS
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class FrmVertexEditor : System.Windows.Forms.Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVertexEditor));
			this.LstObjects = new Ohana3DS.MyListview();
			this.LstFaces = new Ohana3DS.MyListview();
			this.BtnImportObj = new System.Windows.Forms.Button();
			this.BtnExportObj = new System.Windows.Forms.Button();
			this.BtnClear = new System.Windows.Forms.Button();
			this.Title = new Ohana3DS.MyWindowTitle();
			this.BtnMinimize = new System.Windows.Forms.Label();
			this.BtnClose = new System.Windows.Forms.Label();
			this.BtnExportFace = new System.Windows.Forms.Button();
			this.LblTexID = new System.Windows.Forms.Label();
			this.TxtTexID = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			//
			//LstObjects
			//
			this.LstObjects.BackColor = System.Drawing.Color.Transparent;
			this.LstObjects.ForeColor = System.Drawing.Color.White;
			this.LstObjects.Location = new System.Drawing.Point(12, 32);
			this.LstObjects.Name = "LstObjects";
			this.LstObjects.SelectedIndex = -1;
			this.LstObjects.Size = new System.Drawing.Size(250, 406);
			this.LstObjects.TabIndex = 0;
			this.LstObjects.Text = "MyListview1";
			this.LstObjects.TileHeight = 16;
			//
			//LstFaces
			//
			this.LstFaces.BackColor = System.Drawing.Color.Transparent;
			this.LstFaces.ForeColor = System.Drawing.Color.White;
			this.LstFaces.Location = new System.Drawing.Point(268, 32);
			this.LstFaces.Name = "LstFaces";
			this.LstFaces.SelectedIndex = -1;
			this.LstFaces.Size = new System.Drawing.Size(360, 406);
			this.LstFaces.TabIndex = 1;
			this.LstFaces.Text = "MyListview2";
			this.LstFaces.TileHeight = 16;
			//
			//BtnImportObj
			//
			this.BtnImportObj.BackColor = System.Drawing.Color.Transparent;
			this.BtnImportObj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnImportObj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnImportObj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnImportObj.ForeColor = System.Drawing.Color.White;
			this.BtnImportObj.Location = new System.Drawing.Point(200, 444);
			this.BtnImportObj.Name = "BtnImportObj";
			this.BtnImportObj.Size = new System.Drawing.Size(88, 24);
			this.BtnImportObj.TabIndex = 7;
			this.BtnImportObj.Text = "Import .obj...";
			this.BtnImportObj.UseVisualStyleBackColor = false;
			//
			//BtnExportObj
			//
			this.BtnExportObj.BackColor = System.Drawing.Color.Transparent;
			this.BtnExportObj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnExportObj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnExportObj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportObj.ForeColor = System.Drawing.Color.White;
			this.BtnExportObj.Location = new System.Drawing.Point(12, 444);
			this.BtnExportObj.Name = "BtnExportObj";
			this.BtnExportObj.Size = new System.Drawing.Size(88, 24);
			this.BtnExportObj.TabIndex = 8;
			this.BtnExportObj.Text = "Export .obj...";
			this.BtnExportObj.UseVisualStyleBackColor = false;
			//
			//BtnClear
			//
			this.BtnClear.BackColor = System.Drawing.Color.Transparent;
			this.BtnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnClear.ForeColor = System.Drawing.Color.White;
			this.BtnClear.Location = new System.Drawing.Point(540, 444);
			this.BtnClear.Name = "BtnClear";
			this.BtnClear.Size = new System.Drawing.Size(88, 24);
			this.BtnClear.TabIndex = 9;
			this.BtnClear.Text = "Clear face";
			this.BtnClear.UseVisualStyleBackColor = false;
			//
			//Title
			//
			this.Title.AutoSize = true;
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Title.ForeColor = System.Drawing.Color.White;
			this.Title.Location = new System.Drawing.Point(258, 0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(124, 25);
			this.Title.TabIndex = 24;
			this.Title.Text = "Vertex Editor";
			//
			//BtnMinimize
			//
			this.BtnMinimize.BackColor = System.Drawing.Color.Transparent;
			this.BtnMinimize.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnMinimize.ForeColor = System.Drawing.Color.White;
			this.BtnMinimize.Location = new System.Drawing.Point(564, 4);
			this.BtnMinimize.Name = "BtnMinimize";
			this.BtnMinimize.Size = new System.Drawing.Size(32, 24);
			this.BtnMinimize.TabIndex = 23;
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
			this.BtnClose.TabIndex = 22;
			this.BtnClose.Text = "X";
			this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//BtnExportFace
			//
			this.BtnExportFace.BackColor = System.Drawing.Color.Transparent;
			this.BtnExportFace.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnExportFace.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnExportFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExportFace.ForeColor = System.Drawing.Color.White;
			this.BtnExportFace.Location = new System.Drawing.Point(106, 444);
			this.BtnExportFace.Name = "BtnExportFace";
			this.BtnExportFace.Size = new System.Drawing.Size(88, 24);
			this.BtnExportFace.TabIndex = 25;
			this.BtnExportFace.Text = "Export face...";
			this.BtnExportFace.UseVisualStyleBackColor = false;
			//
			//LblTexID
			//
			this.LblTexID.AutoSize = true;
			this.LblTexID.BackColor = System.Drawing.Color.Transparent;
			this.LblTexID.ForeColor = System.Drawing.Color.White;
			this.LblTexID.Location = new System.Drawing.Point(428, 450);
			this.LblTexID.Name = "LblTexID";
			this.LblTexID.Size = new System.Drawing.Size(60, 13);
			this.LblTexID.TabIndex = 26;
			this.LblTexID.Text = "Texture ID:";
			//
			//TxtTexID
			//
			this.TxtTexID.BackColor = System.Drawing.Color.Black;
			this.TxtTexID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtTexID.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtTexID.ForeColor = System.Drawing.Color.White;
			this.TxtTexID.Location = new System.Drawing.Point(494, 444);
			this.TxtTexID.Name = "TxtTexID";
			this.TxtTexID.Size = new System.Drawing.Size(40, 24);
			this.TxtTexID.TabIndex = 27;
			//
			//FrmVertexEditor
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
			this.ClientSize = new System.Drawing.Size(640, 480);
			this.Controls.Add(this.TxtTexID);
			this.Controls.Add(this.LblTexID);
			this.Controls.Add(this.BtnExportFace);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.BtnMinimize);
			this.Controls.Add(this.BtnClose);
			this.Controls.Add(this.BtnClear);
			this.Controls.Add(this.BtnExportObj);
			this.Controls.Add(this.BtnImportObj);
			this.Controls.Add(this.LstFaces);
			this.Controls.Add(this.LstObjects);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmVertexEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Vertex Editor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private Ohana3DS.MyListview withEventsField_LstObjects;
		internal Ohana3DS.MyListview LstObjects {
			get { return withEventsField_LstObjects; }
			set {
				if (withEventsField_LstObjects != null) {
					withEventsField_LstObjects.SelectedIndexChanged -= LstObjects_SelectedIndexChanged;
				}
				withEventsField_LstObjects = value;
				if (withEventsField_LstObjects != null) {
					withEventsField_LstObjects.SelectedIndexChanged += LstObjects_SelectedIndexChanged;
				}
			}
		}
		private Ohana3DS.MyListview withEventsField_LstFaces;
		internal Ohana3DS.MyListview LstFaces {
			get { return withEventsField_LstFaces; }
			set {
				if (withEventsField_LstFaces != null) {
					withEventsField_LstFaces.SelectedIndexChanged -= LstFaces_SelectedIndexChanged;
				}
				withEventsField_LstFaces = value;
				if (withEventsField_LstFaces != null) {
					withEventsField_LstFaces.SelectedIndexChanged += LstFaces_SelectedIndexChanged;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnImportObj;
		internal System.Windows.Forms.Button BtnImportObj {
			get { return withEventsField_BtnImportObj; }
			set {
				if (withEventsField_BtnImportObj != null) {
					withEventsField_BtnImportObj.Click -= BtnImportObj_Click;
				}
				withEventsField_BtnImportObj = value;
				if (withEventsField_BtnImportObj != null) {
					withEventsField_BtnImportObj.Click += BtnImportObj_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnExportObj;
		internal System.Windows.Forms.Button BtnExportObj {
			get { return withEventsField_BtnExportObj; }
			set {
				if (withEventsField_BtnExportObj != null) {
					withEventsField_BtnExportObj.Click -= BtnExportObj_Click;
				}
				withEventsField_BtnExportObj = value;
				if (withEventsField_BtnExportObj != null) {
					withEventsField_BtnExportObj.Click += BtnExportObj_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnClear;
		internal System.Windows.Forms.Button BtnClear {
			get { return withEventsField_BtnClear; }
			set {
				if (withEventsField_BtnClear != null) {
					withEventsField_BtnClear.Click -= BtnClear_Click;
				}
				withEventsField_BtnClear = value;
				if (withEventsField_BtnClear != null) {
					withEventsField_BtnClear.Click += BtnClear_Click;
				}
			}
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
		private System.Windows.Forms.Button withEventsField_BtnExportFace;
		internal System.Windows.Forms.Button BtnExportFace {
			get { return withEventsField_BtnExportFace; }
			set {
				if (withEventsField_BtnExportFace != null) {
					withEventsField_BtnExportFace.Click -= BtnExportFace_Click;
				}
				withEventsField_BtnExportFace = value;
				if (withEventsField_BtnExportFace != null) {
					withEventsField_BtnExportFace.Click += BtnExportFace_Click;
				}
			}
		}
		internal System.Windows.Forms.Label LblTexID;
		private System.Windows.Forms.TextBox withEventsField_TxtTexID;
		internal System.Windows.Forms.TextBox TxtTexID {
			get { return withEventsField_TxtTexID; }
			set {
				if (withEventsField_TxtTexID != null) {
					withEventsField_TxtTexID.TextChanged -= TxtTexID_TextChanged;
				}
				withEventsField_TxtTexID = value;
				if (withEventsField_TxtTexID != null) {
					withEventsField_TxtTexID.TextChanged += TxtTexID_TextChanged;
				}
			}
		}
		public FrmVertexEditor()
		{
			FormClosing += FrmMapProp_FormClosing;
			Load += FrmVertexEditor_Load;
			InitializeComponent();
		}
	}
}
