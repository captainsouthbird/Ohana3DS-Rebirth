using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
namespace Ohana3DS
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class FrmMapProp : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
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

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMapProp));
			this.BtnClose = new System.Windows.Forms.Label();
			this.BtnMinimize = new System.Windows.Forms.Label();
			this.mapCoords = new System.Windows.Forms.Label();
			this.Title = new Ohana3DS.MyWindowTitle();
			this.MapTabs = new Ohana3DS.MyTabcontrol();
			this.TabProperties = new System.Windows.Forms.TabPage();
			this.mapPropSet = new System.Windows.Forms.Button();
			this.mapPropCom = new System.Windows.Forms.ComboBox();
			this.LblMapProp = new System.Windows.Forms.Label();
			this.mapPropSave = new System.Windows.Forms.Button();
			this.mapPicBox = new System.Windows.Forms.PictureBox();
			this.TabObjects = new System.Windows.Forms.TabPage();
			this.GrpTranslation = new Ohana3DS.MyGroupbox();
			this.TxtTZ = new System.Windows.Forms.TextBox();
			this.LblTZDummy = new System.Windows.Forms.Label();
			this.TxtTY = new System.Windows.Forms.TextBox();
			this.TxtTX = new System.Windows.Forms.TextBox();
			this.LblTYDummy = new System.Windows.Forms.Label();
			this.LblTXDummy = new System.Windows.Forms.Label();
			this.GrpRotation = new Ohana3DS.MyGroupbox();
			this.TxtRZ = new System.Windows.Forms.TextBox();
			this.LblRZDummy = new System.Windows.Forms.Label();
			this.TxtRY = new System.Windows.Forms.TextBox();
			this.TxtRX = new System.Windows.Forms.TextBox();
			this.LblRYDummy = new System.Windows.Forms.Label();
			this.LblRXDummy = new System.Windows.Forms.Label();
			this.GrpScale = new Ohana3DS.MyGroupbox();
			this.TxtSZ = new System.Windows.Forms.TextBox();
			this.LblSZ = new System.Windows.Forms.Label();
			this.TxtSY = new System.Windows.Forms.TextBox();
			this.TxtSX = new System.Windows.Forms.TextBox();
			this.LblSY = new System.Windows.Forms.Label();
			this.LblSXDummy = new System.Windows.Forms.Label();
			this.LstObjects = new Ohana3DS.MyListview();
			this.MapTabs.SuspendLayout();
			this.TabProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.mapPicBox).BeginInit();
			this.TabObjects.SuspendLayout();
			this.GrpTranslation.SuspendLayout();
			this.GrpRotation.SuspendLayout();
			this.GrpScale.SuspendLayout();
			this.SuspendLayout();
			//
			//BtnClose
			//
			this.BtnClose.BackColor = System.Drawing.Color.Transparent;
			this.BtnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(320, 4);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(32, 24);
			this.BtnClose.TabIndex = 20;
			this.BtnClose.Text = "X";
			this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//BtnMinimize
			//
			this.BtnMinimize.BackColor = System.Drawing.Color.Transparent;
			this.BtnMinimize.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnMinimize.ForeColor = System.Drawing.Color.White;
			this.BtnMinimize.Location = new System.Drawing.Point(288, 4);
			this.BtnMinimize.Name = "BtnMinimize";
			this.BtnMinimize.Size = new System.Drawing.Size(32, 24);
			this.BtnMinimize.TabIndex = 21;
			this.BtnMinimize.Text = "_";
			this.BtnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//mapCoords
			//
			this.mapCoords.AutoSize = true;
			this.mapCoords.BackColor = System.Drawing.Color.Transparent;
			this.mapCoords.ForeColor = System.Drawing.Color.White;
			this.mapCoords.Location = new System.Drawing.Point(9, 41);
			this.mapCoords.Name = "mapCoords";
			this.mapCoords.Size = new System.Drawing.Size(0, 13);
			this.mapCoords.TabIndex = 27;
			//
			//Title
			//
			this.Title.AutoSize = true;
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Title.ForeColor = System.Drawing.Color.White;
			this.Title.Location = new System.Drawing.Point(110, 4);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(143, 25);
			this.Title.TabIndex = 22;
			this.Title.Text = "Map Properties";
			//
			//MapTabs
			//
			this.MapTabs.BackgroundImage = (System.Drawing.Image)resources.GetObject("MapTabs.BackgroundImage");
			this.MapTabs.Controls.Add(this.TabProperties);
			this.MapTabs.Controls.Add(this.TabObjects);
			this.MapTabs.Location = new System.Drawing.Point(12, 32);
			this.MapTabs.Name = "MapTabs";
			this.MapTabs.SelectedIndex = 0;
			this.MapTabs.Size = new System.Drawing.Size(340, 398);
			this.MapTabs.TabIndex = 30;
			//
			//TabProperties
			//
			this.TabProperties.BackColor = System.Drawing.Color.Transparent;
			this.TabProperties.Controls.Add(this.mapPropSet);
			this.TabProperties.Controls.Add(this.mapPropCom);
			this.TabProperties.Controls.Add(this.LblMapProp);
			this.TabProperties.Controls.Add(this.mapPropSave);
			this.TabProperties.Controls.Add(this.mapPicBox);
			this.TabProperties.ForeColor = System.Drawing.Color.White;
			this.TabProperties.Location = new System.Drawing.Point(4, 28);
			this.TabProperties.Name = "TabProperties";
			this.TabProperties.Padding = new System.Windows.Forms.Padding(3);
			this.TabProperties.Size = new System.Drawing.Size(332, 366);
			this.TabProperties.TabIndex = 0;
			this.TabProperties.Text = "Properties";
			//
			//mapPropSet
			//
			this.mapPropSet.BackColor = System.Drawing.Color.Transparent;
			this.mapPropSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.mapPropSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.mapPropSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.mapPropSet.ForeColor = System.Drawing.Color.White;
			this.mapPropSet.Location = new System.Drawing.Point(204, 334);
			this.mapPropSet.Name = "mapPropSet";
			this.mapPropSet.Size = new System.Drawing.Size(56, 23);
			this.mapPropSet.TabIndex = 34;
			this.mapPropSet.Text = "Edit";
			this.mapPropSet.UseVisualStyleBackColor = false;
			//
			//mapPropCom
			//
			this.mapPropCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mapPropCom.FormattingEnabled = true;
			this.mapPropCom.Location = new System.Drawing.Point(66, 336);
			this.mapPropCom.Name = "mapPropCom";
			this.mapPropCom.Size = new System.Drawing.Size(132, 21);
			this.mapPropCom.TabIndex = 33;
			//
			//LblMapProp
			//
			this.LblMapProp.AutoSize = true;
			this.LblMapProp.BackColor = System.Drawing.Color.Transparent;
			this.LblMapProp.ForeColor = System.Drawing.Color.White;
			this.LblMapProp.Location = new System.Drawing.Point(3, 339);
			this.LblMapProp.Name = "LblMapProp";
			this.LblMapProp.Size = new System.Drawing.Size(57, 13);
			this.LblMapProp.TabIndex = 32;
			this.LblMapProp.Text = "Properties:";
			//
			//mapPropSave
			//
			this.mapPropSave.BackColor = System.Drawing.Color.Transparent;
			this.mapPropSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.mapPropSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.mapPropSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.mapPropSave.ForeColor = System.Drawing.Color.White;
			this.mapPropSave.Location = new System.Drawing.Point(270, 334);
			this.mapPropSave.Name = "mapPropSave";
			this.mapPropSave.Size = new System.Drawing.Size(56, 23);
			this.mapPropSave.TabIndex = 31;
			this.mapPropSave.Text = "Save";
			this.mapPropSave.UseVisualStyleBackColor = false;
			//
			//mapPicBox
			//
			this.mapPicBox.BackColor = System.Drawing.Color.Transparent;
			this.mapPicBox.Location = new System.Drawing.Point(6, 6);
			this.mapPicBox.Name = "mapPicBox";
			this.mapPicBox.Size = new System.Drawing.Size(320, 320);
			this.mapPicBox.TabIndex = 30;
			this.mapPicBox.TabStop = false;
			//
			//TabObjects
			//
			this.TabObjects.BackColor = System.Drawing.Color.Transparent;
			this.TabObjects.Controls.Add(this.GrpTranslation);
			this.TabObjects.Controls.Add(this.GrpRotation);
			this.TabObjects.Controls.Add(this.GrpScale);
			this.TabObjects.Controls.Add(this.LstObjects);
			this.TabObjects.ForeColor = System.Drawing.Color.White;
			this.TabObjects.Location = new System.Drawing.Point(4, 28);
			this.TabObjects.Name = "TabObjects";
			this.TabObjects.Padding = new System.Windows.Forms.Padding(3);
			this.TabObjects.Size = new System.Drawing.Size(332, 366);
			this.TabObjects.TabIndex = 1;
			this.TabObjects.Text = "Objects";
			//
			//GrpTranslation
			//
			this.GrpTranslation.Controls.Add(this.TxtTZ);
			this.GrpTranslation.Controls.Add(this.LblTZDummy);
			this.GrpTranslation.Controls.Add(this.TxtTY);
			this.GrpTranslation.Controls.Add(this.TxtTX);
			this.GrpTranslation.Controls.Add(this.LblTYDummy);
			this.GrpTranslation.Controls.Add(this.LblTXDummy);
			this.GrpTranslation.Location = new System.Drawing.Point(209, 232);
			this.GrpTranslation.Name = "GrpTranslation";
			this.GrpTranslation.Size = new System.Drawing.Size(120, 110);
			this.GrpTranslation.TabIndex = 6;
			this.GrpTranslation.TabStop = false;
			this.GrpTranslation.Text = "Translation";
			//
			//TxtTZ
			//
			this.TxtTZ.BackColor = System.Drawing.Color.Black;
			this.TxtTZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtTZ.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtTZ.ForeColor = System.Drawing.Color.White;
			this.TxtTZ.Location = new System.Drawing.Point(28, 79);
			this.TxtTZ.Name = "TxtTZ";
			this.TxtTZ.Size = new System.Drawing.Size(89, 24);
			this.TxtTZ.TabIndex = 5;
			//
			//LblTZDummy
			//
			this.LblTZDummy.AutoSize = true;
			this.LblTZDummy.Location = new System.Drawing.Point(-3, 84);
			this.LblTZDummy.Name = "LblTZDummy";
			this.LblTZDummy.Size = new System.Drawing.Size(17, 13);
			this.LblTZDummy.TabIndex = 4;
			this.LblTZDummy.Text = "Z:";
			//
			//TxtTY
			//
			this.TxtTY.BackColor = System.Drawing.Color.Black;
			this.TxtTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtTY.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtTY.ForeColor = System.Drawing.Color.White;
			this.TxtTY.Location = new System.Drawing.Point(28, 49);
			this.TxtTY.Name = "TxtTY";
			this.TxtTY.Size = new System.Drawing.Size(89, 24);
			this.TxtTY.TabIndex = 3;
			//
			//TxtTX
			//
			this.TxtTX.BackColor = System.Drawing.Color.Black;
			this.TxtTX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtTX.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtTX.ForeColor = System.Drawing.Color.White;
			this.TxtTX.Location = new System.Drawing.Point(28, 19);
			this.TxtTX.Name = "TxtTX";
			this.TxtTX.Size = new System.Drawing.Size(89, 24);
			this.TxtTX.TabIndex = 2;
			//
			//LblTYDummy
			//
			this.LblTYDummy.AutoSize = true;
			this.LblTYDummy.Location = new System.Drawing.Point(-3, 54);
			this.LblTYDummy.Name = "LblTYDummy";
			this.LblTYDummy.Size = new System.Drawing.Size(17, 13);
			this.LblTYDummy.TabIndex = 1;
			this.LblTYDummy.Text = "Y:";
			//
			//LblTXDummy
			//
			this.LblTXDummy.AutoSize = true;
			this.LblTXDummy.Location = new System.Drawing.Point(-3, 24);
			this.LblTXDummy.Name = "LblTXDummy";
			this.LblTXDummy.Size = new System.Drawing.Size(17, 13);
			this.LblTXDummy.TabIndex = 0;
			this.LblTXDummy.Text = "X:";
			//
			//GrpRotation
			//
			this.GrpRotation.Controls.Add(this.TxtRZ);
			this.GrpRotation.Controls.Add(this.LblRZDummy);
			this.GrpRotation.Controls.Add(this.TxtRY);
			this.GrpRotation.Controls.Add(this.TxtRX);
			this.GrpRotation.Controls.Add(this.LblRYDummy);
			this.GrpRotation.Controls.Add(this.LblRXDummy);
			this.GrpRotation.Location = new System.Drawing.Point(206, 116);
			this.GrpRotation.Name = "GrpRotation";
			this.GrpRotation.Size = new System.Drawing.Size(120, 110);
			this.GrpRotation.TabIndex = 6;
			this.GrpRotation.TabStop = false;
			this.GrpRotation.Text = "Rotation";
			//
			//TxtRZ
			//
			this.TxtRZ.BackColor = System.Drawing.Color.Black;
			this.TxtRZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtRZ.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtRZ.ForeColor = System.Drawing.Color.White;
			this.TxtRZ.Location = new System.Drawing.Point(31, 78);
			this.TxtRZ.Name = "TxtRZ";
			this.TxtRZ.Size = new System.Drawing.Size(89, 24);
			this.TxtRZ.TabIndex = 5;
			//
			//LblRZDummy
			//
			this.LblRZDummy.AutoSize = true;
			this.LblRZDummy.Location = new System.Drawing.Point(0, 83);
			this.LblRZDummy.Name = "LblRZDummy";
			this.LblRZDummy.Size = new System.Drawing.Size(17, 13);
			this.LblRZDummy.TabIndex = 4;
			this.LblRZDummy.Text = "Z:";
			//
			//TxtRY
			//
			this.TxtRY.BackColor = System.Drawing.Color.Black;
			this.TxtRY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtRY.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtRY.ForeColor = System.Drawing.Color.White;
			this.TxtRY.Location = new System.Drawing.Point(31, 49);
			this.TxtRY.Name = "TxtRY";
			this.TxtRY.Size = new System.Drawing.Size(89, 24);
			this.TxtRY.TabIndex = 3;
			//
			//TxtRX
			//
			this.TxtRX.BackColor = System.Drawing.Color.Black;
			this.TxtRX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtRX.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtRX.ForeColor = System.Drawing.Color.White;
			this.TxtRX.Location = new System.Drawing.Point(31, 19);
			this.TxtRX.Name = "TxtRX";
			this.TxtRX.Size = new System.Drawing.Size(89, 24);
			this.TxtRX.TabIndex = 2;
			//
			//LblRYDummy
			//
			this.LblRYDummy.AutoSize = true;
			this.LblRYDummy.Location = new System.Drawing.Point(0, 54);
			this.LblRYDummy.Name = "LblRYDummy";
			this.LblRYDummy.Size = new System.Drawing.Size(17, 13);
			this.LblRYDummy.TabIndex = 1;
			this.LblRYDummy.Text = "Y:";
			//
			//LblRXDummy
			//
			this.LblRXDummy.AutoSize = true;
			this.LblRXDummy.Location = new System.Drawing.Point(0, 24);
			this.LblRXDummy.Name = "LblRXDummy";
			this.LblRXDummy.Size = new System.Drawing.Size(17, 13);
			this.LblRXDummy.TabIndex = 0;
			this.LblRXDummy.Text = "X:";
			//
			//GrpScale
			//
			this.GrpScale.Controls.Add(this.TxtSZ);
			this.GrpScale.Controls.Add(this.LblSZ);
			this.GrpScale.Controls.Add(this.TxtSY);
			this.GrpScale.Controls.Add(this.TxtSX);
			this.GrpScale.Controls.Add(this.LblSY);
			this.GrpScale.Controls.Add(this.LblSXDummy);
			this.GrpScale.Location = new System.Drawing.Point(206, 0);
			this.GrpScale.Name = "GrpScale";
			this.GrpScale.Size = new System.Drawing.Size(120, 110);
			this.GrpScale.TabIndex = 1;
			this.GrpScale.TabStop = false;
			this.GrpScale.Text = "Scale";
			//
			//TxtSZ
			//
			this.TxtSZ.BackColor = System.Drawing.Color.Black;
			this.TxtSZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtSZ.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtSZ.ForeColor = System.Drawing.Color.White;
			this.TxtSZ.Location = new System.Drawing.Point(31, 79);
			this.TxtSZ.Name = "TxtSZ";
			this.TxtSZ.Size = new System.Drawing.Size(89, 24);
			this.TxtSZ.TabIndex = 5;
			//
			//LblSZ
			//
			this.LblSZ.AutoSize = true;
			this.LblSZ.Location = new System.Drawing.Point(0, 84);
			this.LblSZ.Name = "LblSZ";
			this.LblSZ.Size = new System.Drawing.Size(17, 13);
			this.LblSZ.TabIndex = 4;
			this.LblSZ.Text = "Z:";
			//
			//TxtSY
			//
			this.TxtSY.BackColor = System.Drawing.Color.Black;
			this.TxtSY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtSY.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtSY.ForeColor = System.Drawing.Color.White;
			this.TxtSY.Location = new System.Drawing.Point(31, 49);
			this.TxtSY.Name = "TxtSY";
			this.TxtSY.Size = new System.Drawing.Size(89, 24);
			this.TxtSY.TabIndex = 3;
			//
			//TxtSX
			//
			this.TxtSX.BackColor = System.Drawing.Color.Black;
			this.TxtSX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtSX.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtSX.ForeColor = System.Drawing.Color.White;
			this.TxtSX.Location = new System.Drawing.Point(31, 19);
			this.TxtSX.Name = "TxtSX";
			this.TxtSX.Size = new System.Drawing.Size(89, 24);
			this.TxtSX.TabIndex = 2;
			//
			//LblSY
			//
			this.LblSY.AutoSize = true;
			this.LblSY.Location = new System.Drawing.Point(0, 54);
			this.LblSY.Name = "LblSY";
			this.LblSY.Size = new System.Drawing.Size(17, 13);
			this.LblSY.TabIndex = 1;
			this.LblSY.Text = "Y:";
			//
			//LblSXDummy
			//
			this.LblSXDummy.AutoSize = true;
			this.LblSXDummy.Location = new System.Drawing.Point(0, 24);
			this.LblSXDummy.Name = "LblSXDummy";
			this.LblSXDummy.Size = new System.Drawing.Size(17, 13);
			this.LblSXDummy.TabIndex = 0;
			this.LblSXDummy.Text = "X:";
			//
			//LstObjects
			//
			this.LstObjects.Location = new System.Drawing.Point(6, 6);
			this.LstObjects.Name = "LstObjects";
			this.LstObjects.SelectedIndex = -1;
			this.LstObjects.Size = new System.Drawing.Size(194, 354);
			this.LstObjects.TabIndex = 0;
			this.LstObjects.TileHeight = 32;
			//
			//FrmMapProp
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
			this.ClientSize = new System.Drawing.Size(364, 442);
			this.Controls.Add(this.MapTabs);
			this.Controls.Add(this.mapCoords);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.BtnMinimize);
			this.Controls.Add(this.BtnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmMapProp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Map Properties";
			this.TopMost = true;
			this.MapTabs.ResumeLayout(false);
			this.TabProperties.ResumeLayout(false);
			this.TabProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.mapPicBox).EndInit();
			this.TabObjects.ResumeLayout(false);
			this.GrpTranslation.ResumeLayout(false);
			this.GrpTranslation.PerformLayout();
			this.GrpRotation.ResumeLayout(false);
			this.GrpRotation.PerformLayout();
			this.GrpScale.ResumeLayout(false);
			this.GrpScale.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		internal Ohana3DS.MyWindowTitle Title;
		internal System.Windows.Forms.Label mapCoords;
		internal Ohana3DS.MyTabcontrol MapTabs;
		internal System.Windows.Forms.TabPage TabProperties;
		private System.Windows.Forms.Button withEventsField_mapPropSet;
		internal System.Windows.Forms.Button mapPropSet {
			get { return withEventsField_mapPropSet; }
			set {
				if (withEventsField_mapPropSet != null) {
					withEventsField_mapPropSet.Click -= mapPropSet_Click;
				}
				withEventsField_mapPropSet = value;
				if (withEventsField_mapPropSet != null) {
					withEventsField_mapPropSet.Click += mapPropSet_Click;
				}
			}
		}
		internal System.Windows.Forms.ComboBox mapPropCom;
		internal System.Windows.Forms.Label LblMapProp;
		private System.Windows.Forms.Button withEventsField_mapPropSave;
		internal System.Windows.Forms.Button mapPropSave {
			get { return withEventsField_mapPropSave; }
			set {
				if (withEventsField_mapPropSave != null) {
					withEventsField_mapPropSave.Click -= mapPropSave_Click;
				}
				withEventsField_mapPropSave = value;
				if (withEventsField_mapPropSave != null) {
					withEventsField_mapPropSave.Click += mapPropSave_Click;
				}
			}
		}
		private System.Windows.Forms.PictureBox withEventsField_mapPicBox;
		internal System.Windows.Forms.PictureBox mapPicBox {
			get { return withEventsField_mapPicBox; }
			set {
				if (withEventsField_mapPicBox != null) {
					withEventsField_mapPicBox.Click -= mapPicBox_Click;
				}
				withEventsField_mapPicBox = value;
				if (withEventsField_mapPicBox != null) {
					withEventsField_mapPicBox.Click += mapPicBox_Click;
				}
			}
		}
		internal System.Windows.Forms.TabPage TabObjects;
		internal Ohana3DS.MyGroupbox GrpScale;
		internal System.Windows.Forms.Label LblSXDummy;
		internal Ohana3DS.MyListview LstObjects;
		internal System.Windows.Forms.TextBox TxtSX;
		internal System.Windows.Forms.TextBox TxtSZ;
		internal System.Windows.Forms.Label LblSZ;
		internal System.Windows.Forms.TextBox TxtSY;
		internal System.Windows.Forms.Label LblSY;
		internal Ohana3DS.MyGroupbox GrpTranslation;
		internal System.Windows.Forms.TextBox TxtTZ;
		internal System.Windows.Forms.Label LblTZDummy;
		internal System.Windows.Forms.TextBox TxtTY;
		internal System.Windows.Forms.TextBox TxtTX;
		internal System.Windows.Forms.Label LblTXDummy;
		internal Ohana3DS.MyGroupbox GrpRotation;
		internal System.Windows.Forms.TextBox TxtRZ;
		internal System.Windows.Forms.Label LblRZDummy;
		internal System.Windows.Forms.TextBox TxtRY;
		internal System.Windows.Forms.TextBox TxtRX;
		internal System.Windows.Forms.Label LblRYDummy;
		internal System.Windows.Forms.Label LblRXDummy;
		internal System.Windows.Forms.Label LblTYDummy;
		public FrmMapProp()
		{
			FormClosing += FrmMapProp_FormClosing;
			Load += FrmMapProp_Load;
			InitializeComponent();
		}
	}
}
