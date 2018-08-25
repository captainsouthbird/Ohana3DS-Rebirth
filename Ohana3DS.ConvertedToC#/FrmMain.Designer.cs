using System;
namespace Ohana3DS
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class FrmMain : System.Windows.Forms.Form
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.BtnClose = new System.Windows.Forms.Label();
			this.BtnMinimize = new System.Windows.Forms.Label();
			this.Splash = new System.Windows.Forms.PictureBox();
			this.ModelNameTip = new System.Windows.Forms.ToolTip(this.components);
			this.MainTabs = new Ohana3DS.MyTabcontrol();
			this.ModelPage = new System.Windows.Forms.TabPage();
			this.GrpOptions = new Ohana3DS.MyGroupbox();
			this.BtnModelMapEditor = new System.Windows.Forms.Button();
			this.BtnModelSave = new System.Windows.Forms.Button();
			this.BtnModelVertexEditor = new System.Windows.Forms.Button();
			this.BtnModelScale = new System.Windows.Forms.Button();
			this.ProgressModels = new Ohana3DS.MyProgressbar();
			this.BtnModelExportAllFF = new System.Windows.Forms.Button();
			this.BtnModelExport = new System.Windows.Forms.Button();
			this.BtnModelOpen = new System.Windows.Forms.Button();
			this.GrpInfo = new Ohana3DS.MyGroupbox();
			this.LblModelName = new System.Windows.Forms.Label();
			this.BtnModelTexturesMore = new System.Windows.Forms.Button();
			this.LblInfoTextures = new System.Windows.Forms.Label();
			this.LblInfoBones = new System.Windows.Forms.Label();
			this.LblInfoTriangles = new System.Windows.Forms.Label();
			this.LblInfoVertices = new System.Windows.Forms.Label();
			this.LblInfoTexturesDummy = new System.Windows.Forms.Label();
			this.LblInfoBonesDummy = new System.Windows.Forms.Label();
			this.LblInfoTrianglesDummy = new System.Windows.Forms.Label();
			this.LblInfoVerticesDummy = new System.Windows.Forms.Label();
			this.Screen = new System.Windows.Forms.PictureBox();
			this.TexturePage = new System.Windows.Forms.TabPage();
			this.GrpTexOptions = new Ohana3DS.MyGroupbox();
			this.BtnTextureInsertAll = new System.Windows.Forms.Button();
			this.BtnTextureSave = new System.Windows.Forms.Button();
			this.BtnTextureInsert = new System.Windows.Forms.Button();
			this.BtnTextureMode = new System.Windows.Forms.Button();
			this.BtnTextureExportAllFF = new System.Windows.Forms.Button();
			this.ProgressTextures = new Ohana3DS.MyProgressbar();
			this.BtnTextureExportAll = new System.Windows.Forms.Button();
			this.BtnTextureExport = new System.Windows.Forms.Button();
			this.BtnTextureOpen = new System.Windows.Forms.Button();
			this.GrpTexInfo = new Ohana3DS.MyGroupbox();
			this.LblInfoTextureCD = new System.Windows.Forms.Label();
			this.LblInfoTextureFormat = new System.Windows.Forms.Label();
			this.LblInfoTextureResolution = new System.Windows.Forms.Label();
			this.LblInfoTextureCDDummy = new System.Windows.Forms.Label();
			this.LblInfoTextureFormatDummy = new System.Windows.Forms.Label();
			this.LblInfoTextureResolutionDummy = new System.Windows.Forms.Label();
			this.LblInfoTextureIndex = new System.Windows.Forms.Label();
			this.LblInfoTextureIndexDummy = new System.Windows.Forms.Label();
			this.GrpTexturePreview = new Ohana3DS.MyGroupbox();
			this.ImgTexture = new Ohana3DS.MyPicturebox();
			this.GrpTextures = new Ohana3DS.MyGroupbox();
			this.LstTextures = new Ohana3DS.MyListview();
			this.TextPage = new System.Windows.Forms.TabPage();
			this.GrpTextOptions = new Ohana3DS.MyGroupbox();
			this.BtnTextSave = new System.Windows.Forms.Button();
			this.BtnTextImport = new System.Windows.Forms.Button();
			this.BtnTextExport = new System.Windows.Forms.Button();
			this.BtnTextOpen = new System.Windows.Forms.Button();
			this.GrpTextStrings = new Ohana3DS.MyGroupbox();
			this.LstStrings = new Ohana3DS.MyListview();
			this.GARCPage = new System.Windows.Forms.TabPage();
			this.GrpGARCOptions = new Ohana3DS.MyGroupbox();
			this.BtnGARCCompression = new System.Windows.Forms.Button();
			this.BtnGARCSave = new System.Windows.Forms.Button();
			this.BtnGARCInsert = new System.Windows.Forms.Button();
			this.ProgressGARC = new Ohana3DS.MyProgressbar();
			this.BtnGARCExtractAll = new System.Windows.Forms.Button();
			this.BtnGARCExtract = new System.Windows.Forms.Button();
			this.BtnGARCOpen = new System.Windows.Forms.Button();
			this.GrpFiles = new Ohana3DS.MyGroupbox();
			this.LstFiles = new Ohana3DS.MyListview();
			this.ROMPage = new System.Windows.Forms.TabPage();
			this.GrpROMLog = new Ohana3DS.MyGroupbox();
			this.LstROMLog = new Ohana3DS.MyListview();
			this.GrpROMOptions = new Ohana3DS.MyGroupbox();
			this.BtnROMDecrypt = new System.Windows.Forms.Button();
			this.BtnROMOpenXorPad = new System.Windows.Forms.Button();
			this.BtnROMOpen = new System.Windows.Forms.Button();
			this.SearchPage = new System.Windows.Forms.TabPage();
			this.GrpMatches = new Ohana3DS.MyGroupbox();
			this.LstMatches = new Ohana3DS.MyListview();
			this.GrpSearchOptions = new Ohana3DS.MyGroupbox();
			this.TxtSearch = new System.Windows.Forms.TextBox();
			this.ProgressSearch = new Ohana3DS.MyProgressbar();
			this.BtnSearch = new System.Windows.Forms.Button();
			this.Title = new Ohana3DS.MyWindowTitle();
			this.colorBG = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)this.Splash).BeginInit();
			this.MainTabs.SuspendLayout();
			this.ModelPage.SuspendLayout();
			this.GrpOptions.SuspendLayout();
			this.GrpInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Screen).BeginInit();
			this.TexturePage.SuspendLayout();
			this.GrpTexOptions.SuspendLayout();
			this.GrpTexInfo.SuspendLayout();
			this.GrpTexturePreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.ImgTexture).BeginInit();
			this.GrpTextures.SuspendLayout();
			this.TextPage.SuspendLayout();
			this.GrpTextOptions.SuspendLayout();
			this.GrpTextStrings.SuspendLayout();
			this.GARCPage.SuspendLayout();
			this.GrpGARCOptions.SuspendLayout();
			this.GrpFiles.SuspendLayout();
			this.ROMPage.SuspendLayout();
			this.GrpROMLog.SuspendLayout();
			this.GrpROMOptions.SuspendLayout();
			this.SearchPage.SuspendLayout();
			this.GrpMatches.SuspendLayout();
			this.GrpSearchOptions.SuspendLayout();
			this.SuspendLayout();
			//
			//BtnClose
			//
			this.BtnClose.BackColor = System.Drawing.Color.Transparent;
			this.BtnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(756, 4);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(32, 24);
			this.BtnClose.TabIndex = 0;
			this.BtnClose.Text = "X";
			this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//BtnMinimize
			//
			this.BtnMinimize.BackColor = System.Drawing.Color.Transparent;
			this.BtnMinimize.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.BtnMinimize.ForeColor = System.Drawing.Color.White;
			this.BtnMinimize.Location = new System.Drawing.Point(724, 4);
			this.BtnMinimize.Name = "BtnMinimize";
			this.BtnMinimize.Size = new System.Drawing.Size(32, 24);
			this.BtnMinimize.TabIndex = 15;
			this.BtnMinimize.Text = "_";
			this.BtnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//Splash
			//
			this.Splash.Image = (System.Drawing.Image)resources.GetObject("Splash.Image");
			this.Splash.Location = new System.Drawing.Point(0, 32);
			this.Splash.Name = "Splash";
			this.Splash.Size = new System.Drawing.Size(800, 568);
			this.Splash.TabIndex = 20;
			this.Splash.TabStop = false;
			//
			//MainTabs
			//
			this.MainTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.MainTabs.BackgroundImage = (System.Drawing.Image)resources.GetObject("MainTabs.BackgroundImage");
			this.MainTabs.Controls.Add(this.ModelPage);
			this.MainTabs.Controls.Add(this.TexturePage);
			this.MainTabs.Controls.Add(this.TextPage);
			this.MainTabs.Controls.Add(this.GARCPage);
			this.MainTabs.Controls.Add(this.ROMPage);
			this.MainTabs.Controls.Add(this.SearchPage);
			this.MainTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.MainTabs.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.MainTabs.ItemSize = new System.Drawing.Size(56, 24);
			this.MainTabs.Location = new System.Drawing.Point(0, 32);
			this.MainTabs.Multiline = true;
			this.MainTabs.Name = "MainTabs";
			this.MainTabs.SelectedIndex = 0;
			this.MainTabs.Size = new System.Drawing.Size(800, 568);
			this.MainTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.MainTabs.TabIndex = 19;
			this.MainTabs.Visible = false;
			//
			//ModelPage
			//
			this.ModelPage.BackColor = System.Drawing.Color.Transparent;
			this.ModelPage.Controls.Add(this.GrpOptions);
			this.ModelPage.Controls.Add(this.GrpInfo);
			this.ModelPage.Controls.Add(this.Screen);
			this.ModelPage.ForeColor = System.Drawing.Color.White;
			this.ModelPage.Location = new System.Drawing.Point(28, 4);
			this.ModelPage.Name = "ModelPage";
			this.ModelPage.Padding = new System.Windows.Forms.Padding(3);
			this.ModelPage.Size = new System.Drawing.Size(768, 560);
			this.ModelPage.TabIndex = 0;
			this.ModelPage.Text = "Model";
			//
			//GrpOptions
			//
			this.GrpOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpOptions.Controls.Add(this.BtnModelMapEditor);
			this.GrpOptions.Controls.Add(this.BtnModelSave);
			this.GrpOptions.Controls.Add(this.BtnModelVertexEditor);
			this.GrpOptions.Controls.Add(this.BtnModelScale);
			this.GrpOptions.Controls.Add(this.ProgressModels);
			this.GrpOptions.Controls.Add(this.BtnModelExportAllFF);
			this.GrpOptions.Controls.Add(this.BtnModelExport);
			this.GrpOptions.Controls.Add(this.BtnModelOpen);
			this.GrpOptions.ForeColor = System.Drawing.Color.White;
			this.GrpOptions.Location = new System.Drawing.Point(206, 480);
			this.GrpOptions.Name = "GrpOptions";
			this.GrpOptions.Size = new System.Drawing.Size(562, 80);
			this.GrpOptions.TabIndex = 23;
			this.GrpOptions.TabStop = false;
			this.GrpOptions.Text = "Options";
			//
			//BtnModelMapEditor
			//
			this.BtnModelMapEditor.Enabled = false;
			this.BtnModelMapEditor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelMapEditor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelMapEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelMapEditor.Location = new System.Drawing.Point(162, 48);
			this.BtnModelMapEditor.Name = "BtnModelMapEditor";
			this.BtnModelMapEditor.Size = new System.Drawing.Size(150, 24);
			this.BtnModelMapEditor.TabIndex = 8;
			this.BtnModelMapEditor.Text = "Edit map data...";
			this.BtnModelMapEditor.UseVisualStyleBackColor = true;
			//
			//BtnModelSave
			//
			this.BtnModelSave.Enabled = false;
			this.BtnModelSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelSave.Location = new System.Drawing.Point(0, 48);
			this.BtnModelSave.Name = "BtnModelSave";
			this.BtnModelSave.Size = new System.Drawing.Size(72, 24);
			this.BtnModelSave.TabIndex = 6;
			this.BtnModelSave.Text = "Save";
			this.BtnModelSave.UseVisualStyleBackColor = true;
			//
			//BtnModelVertexEditor
			//
			this.BtnModelVertexEditor.Enabled = false;
			this.BtnModelVertexEditor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelVertexEditor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelVertexEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelVertexEditor.Location = new System.Drawing.Point(84, 48);
			this.BtnModelVertexEditor.Name = "BtnModelVertexEditor";
			this.BtnModelVertexEditor.Size = new System.Drawing.Size(72, 24);
			this.BtnModelVertexEditor.TabIndex = 7;
			this.BtnModelVertexEditor.Text = "Edit...";
			this.BtnModelVertexEditor.UseVisualStyleBackColor = true;
			//
			//BtnModelScale
			//
			this.BtnModelScale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelScale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelScale.Location = new System.Drawing.Point(412, 18);
			this.BtnModelScale.Name = "BtnModelScale";
			this.BtnModelScale.Size = new System.Drawing.Size(150, 24);
			this.BtnModelScale.TabIndex = 5;
			this.BtnModelScale.Text = "Model scale: 1:32";
			this.BtnModelScale.UseVisualStyleBackColor = true;
			//
			//ProgressModels
			//
			this.ProgressModels.Location = new System.Drawing.Point(412, 48);
			this.ProgressModels.Name = "ProgressModels";
			this.ProgressModels.Percentage = 0f;
			this.ProgressModels.Size = new System.Drawing.Size(150, 24);
			this.ProgressModels.TabIndex = 0;
			//
			//BtnModelExportAllFF
			//
			this.BtnModelExportAllFF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelExportAllFF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelExportAllFF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelExportAllFF.Location = new System.Drawing.Point(162, 18);
			this.BtnModelExportAllFF.Name = "BtnModelExportAllFF";
			this.BtnModelExportAllFF.Size = new System.Drawing.Size(150, 24);
			this.BtnModelExportAllFF.TabIndex = 3;
			this.BtnModelExportAllFF.Text = "Export all from folder";
			this.BtnModelExportAllFF.UseVisualStyleBackColor = true;
			//
			//BtnModelExport
			//
			this.BtnModelExport.Enabled = false;
			this.BtnModelExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelExport.Location = new System.Drawing.Point(84, 18);
			this.BtnModelExport.Name = "BtnModelExport";
			this.BtnModelExport.Size = new System.Drawing.Size(72, 24);
			this.BtnModelExport.TabIndex = 2;
			this.BtnModelExport.Text = "Export";
			this.BtnModelExport.UseVisualStyleBackColor = true;
			//
			//BtnModelOpen
			//
			this.BtnModelOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelOpen.Location = new System.Drawing.Point(0, 18);
			this.BtnModelOpen.Name = "BtnModelOpen";
			this.BtnModelOpen.Size = new System.Drawing.Size(72, 24);
			this.BtnModelOpen.TabIndex = 1;
			this.BtnModelOpen.Text = "Open";
			this.BtnModelOpen.UseVisualStyleBackColor = true;
			//
			//GrpInfo
			//
			this.GrpInfo.BackColor = System.Drawing.Color.Transparent;
			this.GrpInfo.Controls.Add(this.LblModelName);
			this.GrpInfo.Controls.Add(this.BtnModelTexturesMore);
			this.GrpInfo.Controls.Add(this.LblInfoTextures);
			this.GrpInfo.Controls.Add(this.LblInfoBones);
			this.GrpInfo.Controls.Add(this.LblInfoTriangles);
			this.GrpInfo.Controls.Add(this.LblInfoVertices);
			this.GrpInfo.Controls.Add(this.LblInfoTexturesDummy);
			this.GrpInfo.Controls.Add(this.LblInfoBonesDummy);
			this.GrpInfo.Controls.Add(this.LblInfoTrianglesDummy);
			this.GrpInfo.Controls.Add(this.LblInfoVerticesDummy);
			this.GrpInfo.ForeColor = System.Drawing.Color.White;
			this.GrpInfo.Location = new System.Drawing.Point(0, 480);
			this.GrpInfo.Name = "GrpInfo";
			this.GrpInfo.Size = new System.Drawing.Size(200, 80);
			this.GrpInfo.TabIndex = 22;
			this.GrpInfo.TabStop = false;
			this.GrpInfo.Text = "Info";
			//
			//LblModelName
			//
			this.LblModelName.Location = new System.Drawing.Point(112, 18);
			this.LblModelName.Name = "LblModelName";
			this.LblModelName.Size = new System.Drawing.Size(88, 16);
			this.LblModelName.TabIndex = 24;
			//
			//BtnModelTexturesMore
			//
			this.BtnModelTexturesMore.Enabled = false;
			this.BtnModelTexturesMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnModelTexturesMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnModelTexturesMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnModelTexturesMore.Location = new System.Drawing.Point(112, 48);
			this.BtnModelTexturesMore.Name = "BtnModelTexturesMore";
			this.BtnModelTexturesMore.Size = new System.Drawing.Size(88, 24);
			this.BtnModelTexturesMore.TabIndex = 9;
			this.BtnModelTexturesMore.Text = "Texture info...";
			this.BtnModelTexturesMore.UseVisualStyleBackColor = true;
			//
			//LblInfoTextures
			//
			this.LblInfoTextures.AutoSize = true;
			this.LblInfoTextures.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTextures.Location = new System.Drawing.Point(59, 57);
			this.LblInfoTextures.Name = "LblInfoTextures";
			this.LblInfoTextures.Size = new System.Drawing.Size(13, 13);
			this.LblInfoTextures.TabIndex = 7;
			this.LblInfoTextures.Text = "0";
			//
			//LblInfoBones
			//
			this.LblInfoBones.AutoSize = true;
			this.LblInfoBones.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoBones.Location = new System.Drawing.Point(59, 44);
			this.LblInfoBones.Name = "LblInfoBones";
			this.LblInfoBones.Size = new System.Drawing.Size(13, 13);
			this.LblInfoBones.TabIndex = 6;
			this.LblInfoBones.Text = "0";
			//
			//LblInfoTriangles
			//
			this.LblInfoTriangles.AutoSize = true;
			this.LblInfoTriangles.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTriangles.Location = new System.Drawing.Point(59, 31);
			this.LblInfoTriangles.Name = "LblInfoTriangles";
			this.LblInfoTriangles.Size = new System.Drawing.Size(13, 13);
			this.LblInfoTriangles.TabIndex = 5;
			this.LblInfoTriangles.Text = "0";
			//
			//LblInfoVertices
			//
			this.LblInfoVertices.AutoSize = true;
			this.LblInfoVertices.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoVertices.Location = new System.Drawing.Point(59, 18);
			this.LblInfoVertices.Name = "LblInfoVertices";
			this.LblInfoVertices.Size = new System.Drawing.Size(13, 13);
			this.LblInfoVertices.TabIndex = 4;
			this.LblInfoVertices.Text = "0";
			//
			//LblInfoTexturesDummy
			//
			this.LblInfoTexturesDummy.AutoSize = true;
			this.LblInfoTexturesDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTexturesDummy.Location = new System.Drawing.Point(0, 57);
			this.LblInfoTexturesDummy.Name = "LblInfoTexturesDummy";
			this.LblInfoTexturesDummy.Size = new System.Drawing.Size(53, 13);
			this.LblInfoTexturesDummy.TabIndex = 3;
			this.LblInfoTexturesDummy.Text = "Textures:";
			//
			//LblInfoBonesDummy
			//
			this.LblInfoBonesDummy.AutoSize = true;
			this.LblInfoBonesDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoBonesDummy.Location = new System.Drawing.Point(0, 44);
			this.LblInfoBonesDummy.Name = "LblInfoBonesDummy";
			this.LblInfoBonesDummy.Size = new System.Drawing.Size(41, 13);
			this.LblInfoBonesDummy.TabIndex = 2;
			this.LblInfoBonesDummy.Text = "Bones:";
			//
			//LblInfoTrianglesDummy
			//
			this.LblInfoTrianglesDummy.AutoSize = true;
			this.LblInfoTrianglesDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTrianglesDummy.Location = new System.Drawing.Point(0, 31);
			this.LblInfoTrianglesDummy.Name = "LblInfoTrianglesDummy";
			this.LblInfoTrianglesDummy.Size = new System.Drawing.Size(56, 13);
			this.LblInfoTrianglesDummy.TabIndex = 1;
			this.LblInfoTrianglesDummy.Text = "Triangles:";
			//
			//LblInfoVerticesDummy
			//
			this.LblInfoVerticesDummy.AutoSize = true;
			this.LblInfoVerticesDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoVerticesDummy.Location = new System.Drawing.Point(0, 18);
			this.LblInfoVerticesDummy.Name = "LblInfoVerticesDummy";
			this.LblInfoVerticesDummy.Size = new System.Drawing.Size(50, 13);
			this.LblInfoVerticesDummy.TabIndex = 0;
			this.LblInfoVerticesDummy.Text = "Vertices:";
			//
			//Screen
			//
			this.Screen.BackColor = System.Drawing.Color.Black;
			this.Screen.Location = new System.Drawing.Point(0, 0);
			this.Screen.Name = "Screen";
			this.Screen.Size = new System.Drawing.Size(768, 480);
			this.Screen.TabIndex = 21;
			this.Screen.TabStop = false;
			//
			//TexturePage
			//
			this.TexturePage.BackColor = System.Drawing.Color.Transparent;
			this.TexturePage.Controls.Add(this.GrpTexOptions);
			this.TexturePage.Controls.Add(this.GrpTexInfo);
			this.TexturePage.Controls.Add(this.GrpTexturePreview);
			this.TexturePage.Controls.Add(this.GrpTextures);
			this.TexturePage.ForeColor = System.Drawing.Color.White;
			this.TexturePage.Location = new System.Drawing.Point(28, 4);
			this.TexturePage.Name = "TexturePage";
			this.TexturePage.Padding = new System.Windows.Forms.Padding(3);
			this.TexturePage.Size = new System.Drawing.Size(768, 560);
			this.TexturePage.TabIndex = 1;
			this.TexturePage.Text = "Textures";
			//
			//GrpTexOptions
			//
			this.GrpTexOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpTexOptions.Controls.Add(this.BtnTextureInsertAll);
			this.GrpTexOptions.Controls.Add(this.BtnTextureSave);
			this.GrpTexOptions.Controls.Add(this.BtnTextureInsert);
			this.GrpTexOptions.Controls.Add(this.BtnTextureMode);
			this.GrpTexOptions.Controls.Add(this.BtnTextureExportAllFF);
			this.GrpTexOptions.Controls.Add(this.ProgressTextures);
			this.GrpTexOptions.Controls.Add(this.BtnTextureExportAll);
			this.GrpTexOptions.Controls.Add(this.BtnTextureExport);
			this.GrpTexOptions.Controls.Add(this.BtnTextureOpen);
			this.GrpTexOptions.ForeColor = System.Drawing.Color.White;
			this.GrpTexOptions.Location = new System.Drawing.Point(206, 480);
			this.GrpTexOptions.Name = "GrpTexOptions";
			this.GrpTexOptions.Size = new System.Drawing.Size(562, 80);
			this.GrpTexOptions.TabIndex = 26;
			this.GrpTexOptions.TabStop = false;
			this.GrpTexOptions.Text = "Options";
			//
			//BtnTextureInsertAll
			//
			this.BtnTextureInsertAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureInsertAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureInsertAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureInsertAll.Location = new System.Drawing.Point(162, 48);
			this.BtnTextureInsertAll.Name = "BtnTextureInsertAll";
			this.BtnTextureInsertAll.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureInsertAll.TabIndex = 8;
			this.BtnTextureInsertAll.Text = "Import all";
			this.BtnTextureInsertAll.UseVisualStyleBackColor = true;
			//
			//BtnTextureSave
			//
			this.BtnTextureSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureSave.Location = new System.Drawing.Point(0, 48);
			this.BtnTextureSave.Name = "BtnTextureSave";
			this.BtnTextureSave.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureSave.TabIndex = 6;
			this.BtnTextureSave.Text = "Save";
			this.BtnTextureSave.UseVisualStyleBackColor = true;
			//
			//BtnTextureInsert
			//
			this.BtnTextureInsert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureInsert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureInsert.Location = new System.Drawing.Point(84, 48);
			this.BtnTextureInsert.Name = "BtnTextureInsert";
			this.BtnTextureInsert.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureInsert.TabIndex = 7;
			this.BtnTextureInsert.Text = "Import";
			this.BtnTextureInsert.UseVisualStyleBackColor = true;
			//
			//BtnTextureMode
			//
			this.BtnTextureMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureMode.Location = new System.Drawing.Point(490, 18);
			this.BtnTextureMode.Name = "BtnTextureMode";
			this.BtnTextureMode.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureMode.TabIndex = 5;
			this.BtnTextureMode.Text = "Original";
			this.BtnTextureMode.UseVisualStyleBackColor = true;
			//
			//BtnTextureExportAllFF
			//
			this.BtnTextureExportAllFF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureExportAllFF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureExportAllFF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureExportAllFF.Location = new System.Drawing.Point(240, 18);
			this.BtnTextureExportAllFF.Name = "BtnTextureExportAllFF";
			this.BtnTextureExportAllFF.Size = new System.Drawing.Size(150, 24);
			this.BtnTextureExportAllFF.TabIndex = 4;
			this.BtnTextureExportAllFF.Text = "Export all from folder";
			this.BtnTextureExportAllFF.UseVisualStyleBackColor = true;
			//
			//ProgressTextures
			//
			this.ProgressTextures.Location = new System.Drawing.Point(240, 48);
			this.ProgressTextures.Name = "ProgressTextures";
			this.ProgressTextures.Percentage = 0f;
			this.ProgressTextures.Size = new System.Drawing.Size(150, 24);
			this.ProgressTextures.TabIndex = 0;
			//
			//BtnTextureExportAll
			//
			this.BtnTextureExportAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureExportAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureExportAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureExportAll.Location = new System.Drawing.Point(162, 18);
			this.BtnTextureExportAll.Name = "BtnTextureExportAll";
			this.BtnTextureExportAll.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureExportAll.TabIndex = 3;
			this.BtnTextureExportAll.Text = "Export all";
			this.BtnTextureExportAll.UseVisualStyleBackColor = true;
			//
			//BtnTextureExport
			//
			this.BtnTextureExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureExport.Location = new System.Drawing.Point(84, 18);
			this.BtnTextureExport.Name = "BtnTextureExport";
			this.BtnTextureExport.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureExport.TabIndex = 2;
			this.BtnTextureExport.Text = "Export";
			this.BtnTextureExport.UseVisualStyleBackColor = true;
			//
			//BtnTextureOpen
			//
			this.BtnTextureOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextureOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextureOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextureOpen.Location = new System.Drawing.Point(0, 18);
			this.BtnTextureOpen.Name = "BtnTextureOpen";
			this.BtnTextureOpen.Size = new System.Drawing.Size(72, 24);
			this.BtnTextureOpen.TabIndex = 1;
			this.BtnTextureOpen.Text = "Open";
			this.BtnTextureOpen.UseVisualStyleBackColor = true;
			//
			//GrpTexInfo
			//
			this.GrpTexInfo.BackColor = System.Drawing.Color.Transparent;
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureCD);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureFormat);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureResolution);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureCDDummy);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureFormatDummy);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureResolutionDummy);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureIndex);
			this.GrpTexInfo.Controls.Add(this.LblInfoTextureIndexDummy);
			this.GrpTexInfo.ForeColor = System.Drawing.Color.White;
			this.GrpTexInfo.Location = new System.Drawing.Point(0, 480);
			this.GrpTexInfo.Name = "GrpTexInfo";
			this.GrpTexInfo.Size = new System.Drawing.Size(200, 80);
			this.GrpTexInfo.TabIndex = 25;
			this.GrpTexInfo.TabStop = false;
			this.GrpTexInfo.Text = "Info";
			//
			//LblInfoTextureCD
			//
			this.LblInfoTextureCD.AutoSize = true;
			this.LblInfoTextureCD.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTextureCD.Location = new System.Drawing.Point(77, 57);
			this.LblInfoTextureCD.Name = "LblInfoTextureCD";
			this.LblInfoTextureCD.Size = new System.Drawing.Size(19, 13);
			this.LblInfoTextureCD.TabIndex = 10;
			this.LblInfoTextureCD.Text = "---";
			//
			//LblInfoTextureFormat
			//
			this.LblInfoTextureFormat.AutoSize = true;
			this.LblInfoTextureFormat.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTextureFormat.Location = new System.Drawing.Point(77, 44);
			this.LblInfoTextureFormat.Name = "LblInfoTextureFormat";
			this.LblInfoTextureFormat.Size = new System.Drawing.Size(19, 13);
			this.LblInfoTextureFormat.TabIndex = 9;
			this.LblInfoTextureFormat.Text = "---";
			//
			//LblInfoTextureResolution
			//
			this.LblInfoTextureResolution.AutoSize = true;
			this.LblInfoTextureResolution.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTextureResolution.Location = new System.Drawing.Point(77, 31);
			this.LblInfoTextureResolution.Name = "LblInfoTextureResolution";
			this.LblInfoTextureResolution.Size = new System.Drawing.Size(24, 13);
			this.LblInfoTextureResolution.TabIndex = 8;
			this.LblInfoTextureResolution.Text = "0x0";
			//
			//LblInfoTextureCDDummy
			//
			this.LblInfoTextureCDDummy.AutoSize = true;
			this.LblInfoTextureCDDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTextureCDDummy.Location = new System.Drawing.Point(0, 57);
			this.LblInfoTextureCDDummy.Name = "LblInfoTextureCDDummy";
			this.LblInfoTextureCDDummy.Size = new System.Drawing.Size(71, 13);
			this.LblInfoTextureCDDummy.TabIndex = 7;
			this.LblInfoTextureCDDummy.Text = "Color depth:";
			//
			//LblInfoTextureFormatDummy
			//
			this.LblInfoTextureFormatDummy.AutoSize = true;
			this.LblInfoTextureFormatDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTextureFormatDummy.Location = new System.Drawing.Point(0, 44);
			this.LblInfoTextureFormatDummy.Name = "LblInfoTextureFormatDummy";
			this.LblInfoTextureFormatDummy.Size = new System.Drawing.Size(47, 13);
			this.LblInfoTextureFormatDummy.TabIndex = 6;
			this.LblInfoTextureFormatDummy.Text = "Format:";
			//
			//LblInfoTextureResolutionDummy
			//
			this.LblInfoTextureResolutionDummy.AutoSize = true;
			this.LblInfoTextureResolutionDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTextureResolutionDummy.Location = new System.Drawing.Point(0, 31);
			this.LblInfoTextureResolutionDummy.Name = "LblInfoTextureResolutionDummy";
			this.LblInfoTextureResolutionDummy.Size = new System.Drawing.Size(30, 13);
			this.LblInfoTextureResolutionDummy.TabIndex = 5;
			this.LblInfoTextureResolutionDummy.Text = "Size:";
			//
			//LblInfoTextureIndex
			//
			this.LblInfoTextureIndex.AutoSize = true;
			this.LblInfoTextureIndex.Font = new System.Drawing.Font("Segoe UI Light", 8.25f);
			this.LblInfoTextureIndex.Location = new System.Drawing.Point(77, 18);
			this.LblInfoTextureIndex.Name = "LblInfoTextureIndex";
			this.LblInfoTextureIndex.Size = new System.Drawing.Size(23, 13);
			this.LblInfoTextureIndex.TabIndex = 4;
			this.LblInfoTextureIndex.Text = "0/0";
			//
			//LblInfoTextureIndexDummy
			//
			this.LblInfoTextureIndexDummy.AutoSize = true;
			this.LblInfoTextureIndexDummy.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LblInfoTextureIndexDummy.Location = new System.Drawing.Point(0, 18);
			this.LblInfoTextureIndexDummy.Name = "LblInfoTextureIndexDummy";
			this.LblInfoTextureIndexDummy.Size = new System.Drawing.Size(37, 13);
			this.LblInfoTextureIndexDummy.TabIndex = 0;
			this.LblInfoTextureIndexDummy.Text = "Num.:";
			//
			//GrpTexturePreview
			//
			this.GrpTexturePreview.Controls.Add(this.ImgTexture);
			this.GrpTexturePreview.ForeColor = System.Drawing.Color.White;
			this.GrpTexturePreview.Location = new System.Drawing.Point(206, 0);
			this.GrpTexturePreview.Name = "GrpTexturePreview";
			this.GrpTexturePreview.Size = new System.Drawing.Size(562, 480);
			this.GrpTexturePreview.TabIndex = 28;
			this.GrpTexturePreview.TabStop = false;
			this.GrpTexturePreview.Text = "View";
			//
			//ImgTexture
			//
			this.ImgTexture.Image = null;
			this.ImgTexture.Location = new System.Drawing.Point(0, 18);
			this.ImgTexture.Name = "ImgTexture";
			this.ImgTexture.Size = new System.Drawing.Size(562, 462);
			this.ImgTexture.TabIndex = 0;
			this.ImgTexture.TabStop = false;
			//
			//GrpTextures
			//
			this.GrpTextures.Controls.Add(this.LstTextures);
			this.GrpTextures.ForeColor = System.Drawing.Color.White;
			this.GrpTextures.Location = new System.Drawing.Point(0, 0);
			this.GrpTextures.Name = "GrpTextures";
			this.GrpTextures.Size = new System.Drawing.Size(200, 480);
			this.GrpTextures.TabIndex = 27;
			this.GrpTextures.TabStop = false;
			this.GrpTextures.Text = "Textures";
			//
			//LstTextures
			//
			this.LstTextures.Location = new System.Drawing.Point(0, 18);
			this.LstTextures.Name = "LstTextures";
			this.LstTextures.SelectedIndex = -1;
			this.LstTextures.Size = new System.Drawing.Size(200, 460);
			this.LstTextures.TabIndex = 9;
			this.LstTextures.TileHeight = 16;
			//
			//TextPage
			//
			this.TextPage.BackColor = System.Drawing.Color.Transparent;
			this.TextPage.Controls.Add(this.GrpTextOptions);
			this.TextPage.Controls.Add(this.GrpTextStrings);
			this.TextPage.ForeColor = System.Drawing.Color.White;
			this.TextPage.Location = new System.Drawing.Point(28, 4);
			this.TextPage.Name = "TextPage";
			this.TextPage.Size = new System.Drawing.Size(768, 560);
			this.TextPage.TabIndex = 2;
			this.TextPage.Text = "Text";
			//
			//GrpTextOptions
			//
			this.GrpTextOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpTextOptions.Controls.Add(this.BtnTextSave);
			this.GrpTextOptions.Controls.Add(this.BtnTextImport);
			this.GrpTextOptions.Controls.Add(this.BtnTextExport);
			this.GrpTextOptions.Controls.Add(this.BtnTextOpen);
			this.GrpTextOptions.ForeColor = System.Drawing.Color.White;
			this.GrpTextOptions.Location = new System.Drawing.Point(0, 480);
			this.GrpTextOptions.Name = "GrpTextOptions";
			this.GrpTextOptions.Size = new System.Drawing.Size(768, 80);
			this.GrpTextOptions.TabIndex = 27;
			this.GrpTextOptions.TabStop = false;
			this.GrpTextOptions.Text = "Options";
			//
			//BtnTextSave
			//
			this.BtnTextSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextSave.Location = new System.Drawing.Point(0, 48);
			this.BtnTextSave.Name = "BtnTextSave";
			this.BtnTextSave.Size = new System.Drawing.Size(72, 24);
			this.BtnTextSave.TabIndex = 3;
			this.BtnTextSave.Text = "Save";
			this.BtnTextSave.UseVisualStyleBackColor = true;
			//
			//BtnTextImport
			//
			this.BtnTextImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextImport.Location = new System.Drawing.Point(84, 48);
			this.BtnTextImport.Name = "BtnTextImport";
			this.BtnTextImport.Size = new System.Drawing.Size(72, 24);
			this.BtnTextImport.TabIndex = 4;
			this.BtnTextImport.Text = "Import";
			this.BtnTextImport.UseVisualStyleBackColor = true;
			//
			//BtnTextExport
			//
			this.BtnTextExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextExport.Location = new System.Drawing.Point(84, 18);
			this.BtnTextExport.Name = "BtnTextExport";
			this.BtnTextExport.Size = new System.Drawing.Size(72, 24);
			this.BtnTextExport.TabIndex = 2;
			this.BtnTextExport.Text = "Export";
			this.BtnTextExport.UseVisualStyleBackColor = true;
			//
			//BtnTextOpen
			//
			this.BtnTextOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnTextOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnTextOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTextOpen.Location = new System.Drawing.Point(0, 18);
			this.BtnTextOpen.Name = "BtnTextOpen";
			this.BtnTextOpen.Size = new System.Drawing.Size(72, 24);
			this.BtnTextOpen.TabIndex = 1;
			this.BtnTextOpen.Text = "Open";
			this.BtnTextOpen.UseVisualStyleBackColor = true;
			//
			//GrpTextStrings
			//
			this.GrpTextStrings.Controls.Add(this.LstStrings);
			this.GrpTextStrings.ForeColor = System.Drawing.Color.White;
			this.GrpTextStrings.Location = new System.Drawing.Point(0, 0);
			this.GrpTextStrings.Name = "GrpTextStrings";
			this.GrpTextStrings.Size = new System.Drawing.Size(768, 480);
			this.GrpTextStrings.TabIndex = 26;
			this.GrpTextStrings.TabStop = false;
			this.GrpTextStrings.Text = "Texts (Preview only!)";
			//
			//LstStrings
			//
			this.LstStrings.Location = new System.Drawing.Point(0, 18);
			this.LstStrings.Name = "LstStrings";
			this.LstStrings.SelectedIndex = -1;
			this.LstStrings.Size = new System.Drawing.Size(768, 460);
			this.LstStrings.TabIndex = 5;
			this.LstStrings.Text = "MyListview1";
			this.LstStrings.TileHeight = 16;
			//
			//GARCPage
			//
			this.GARCPage.BackColor = System.Drawing.Color.Transparent;
			this.GARCPage.Controls.Add(this.GrpGARCOptions);
			this.GARCPage.Controls.Add(this.GrpFiles);
			this.GARCPage.ForeColor = System.Drawing.Color.White;
			this.GARCPage.Location = new System.Drawing.Point(28, 4);
			this.GARCPage.Name = "GARCPage";
			this.GARCPage.Size = new System.Drawing.Size(768, 560);
			this.GARCPage.TabIndex = 3;
			this.GARCPage.Text = "Container";
			//
			//GrpGARCOptions
			//
			this.GrpGARCOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpGARCOptions.Controls.Add(this.BtnGARCCompression);
			this.GrpGARCOptions.Controls.Add(this.BtnGARCSave);
			this.GrpGARCOptions.Controls.Add(this.BtnGARCInsert);
			this.GrpGARCOptions.Controls.Add(this.ProgressGARC);
			this.GrpGARCOptions.Controls.Add(this.BtnGARCExtractAll);
			this.GrpGARCOptions.Controls.Add(this.BtnGARCExtract);
			this.GrpGARCOptions.Controls.Add(this.BtnGARCOpen);
			this.GrpGARCOptions.ForeColor = System.Drawing.Color.White;
			this.GrpGARCOptions.Location = new System.Drawing.Point(0, 480);
			this.GrpGARCOptions.Name = "GrpGARCOptions";
			this.GrpGARCOptions.Size = new System.Drawing.Size(768, 80);
			this.GrpGARCOptions.TabIndex = 25;
			this.GrpGARCOptions.TabStop = false;
			this.GrpGARCOptions.Text = "Options";
			//
			//BtnGARCCompression
			//
			this.BtnGARCCompression.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCCompression.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCCompression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCCompression.Location = new System.Drawing.Point(615, 18);
			this.BtnGARCCompression.Name = "BtnGARCCompression";
			this.BtnGARCCompression.Size = new System.Drawing.Size(150, 24);
			this.BtnGARCCompression.TabIndex = 4;
			this.BtnGARCCompression.Text = "Optimal compression";
			this.BtnGARCCompression.UseVisualStyleBackColor = true;
			//
			//BtnGARCSave
			//
			this.BtnGARCSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCSave.Location = new System.Drawing.Point(0, 48);
			this.BtnGARCSave.Name = "BtnGARCSave";
			this.BtnGARCSave.Size = new System.Drawing.Size(72, 24);
			this.BtnGARCSave.TabIndex = 5;
			this.BtnGARCSave.Text = "Save";
			this.BtnGARCSave.UseVisualStyleBackColor = true;
			//
			//BtnGARCInsert
			//
			this.BtnGARCInsert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCInsert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCInsert.Location = new System.Drawing.Point(84, 48);
			this.BtnGARCInsert.Name = "BtnGARCInsert";
			this.BtnGARCInsert.Size = new System.Drawing.Size(72, 24);
			this.BtnGARCInsert.TabIndex = 6;
			this.BtnGARCInsert.Text = "Insert";
			this.BtnGARCInsert.UseVisualStyleBackColor = true;
			//
			//ProgressGARC
			//
			this.ProgressGARC.Location = new System.Drawing.Point(615, 48);
			this.ProgressGARC.Name = "ProgressGARC";
			this.ProgressGARC.Percentage = 0f;
			this.ProgressGARC.Size = new System.Drawing.Size(150, 24);
			this.ProgressGARC.TabIndex = 0;
			//
			//BtnGARCExtractAll
			//
			this.BtnGARCExtractAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCExtractAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCExtractAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCExtractAll.Location = new System.Drawing.Point(162, 18);
			this.BtnGARCExtractAll.Name = "BtnGARCExtractAll";
			this.BtnGARCExtractAll.Size = new System.Drawing.Size(72, 24);
			this.BtnGARCExtractAll.TabIndex = 3;
			this.BtnGARCExtractAll.Text = "Extract all";
			this.BtnGARCExtractAll.UseVisualStyleBackColor = true;
			//
			//BtnGARCExtract
			//
			this.BtnGARCExtract.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCExtract.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCExtract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCExtract.Location = new System.Drawing.Point(84, 18);
			this.BtnGARCExtract.Name = "BtnGARCExtract";
			this.BtnGARCExtract.Size = new System.Drawing.Size(72, 24);
			this.BtnGARCExtract.TabIndex = 2;
			this.BtnGARCExtract.Text = "Extract";
			this.BtnGARCExtract.UseVisualStyleBackColor = true;
			//
			//BtnGARCOpen
			//
			this.BtnGARCOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnGARCOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnGARCOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnGARCOpen.Location = new System.Drawing.Point(0, 18);
			this.BtnGARCOpen.Name = "BtnGARCOpen";
			this.BtnGARCOpen.Size = new System.Drawing.Size(72, 24);
			this.BtnGARCOpen.TabIndex = 1;
			this.BtnGARCOpen.Text = "Open";
			this.BtnGARCOpen.UseVisualStyleBackColor = true;
			//
			//GrpFiles
			//
			this.GrpFiles.Controls.Add(this.LstFiles);
			this.GrpFiles.ForeColor = System.Drawing.Color.White;
			this.GrpFiles.Location = new System.Drawing.Point(0, 0);
			this.GrpFiles.Name = "GrpFiles";
			this.GrpFiles.Size = new System.Drawing.Size(768, 480);
			this.GrpFiles.TabIndex = 24;
			this.GrpFiles.TabStop = false;
			this.GrpFiles.Text = "Files";
			//
			//LstFiles
			//
			this.LstFiles.Location = new System.Drawing.Point(0, 18);
			this.LstFiles.Name = "LstFiles";
			this.LstFiles.SelectedIndex = -1;
			this.LstFiles.Size = new System.Drawing.Size(768, 460);
			this.LstFiles.TabIndex = 7;
			this.LstFiles.TileHeight = 16;
			//
			//ROMPage
			//
			this.ROMPage.BackColor = System.Drawing.Color.Transparent;
			this.ROMPage.Controls.Add(this.GrpROMLog);
			this.ROMPage.Controls.Add(this.GrpROMOptions);
			this.ROMPage.ForeColor = System.Drawing.Color.White;
			this.ROMPage.Location = new System.Drawing.Point(28, 4);
			this.ROMPage.Name = "ROMPage";
			this.ROMPage.Size = new System.Drawing.Size(768, 560);
			this.ROMPage.TabIndex = 5;
			this.ROMPage.Text = "ROM";
			//
			//GrpROMLog
			//
			this.GrpROMLog.Controls.Add(this.LstROMLog);
			this.GrpROMLog.ForeColor = System.Drawing.Color.White;
			this.GrpROMLog.Location = new System.Drawing.Point(0, 0);
			this.GrpROMLog.Name = "GrpROMLog";
			this.GrpROMLog.Size = new System.Drawing.Size(768, 480);
			this.GrpROMLog.TabIndex = 30;
			this.GrpROMLog.TabStop = false;
			this.GrpROMLog.Text = "Log";
			//
			//LstROMLog
			//
			this.LstROMLog.Font = new System.Drawing.Font("Lucida Console", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.LstROMLog.Location = new System.Drawing.Point(0, 18);
			this.LstROMLog.Name = "LstROMLog";
			this.LstROMLog.SelectedIndex = -1;
			this.LstROMLog.Size = new System.Drawing.Size(768, 460);
			this.LstROMLog.TabIndex = 3;
			this.LstROMLog.TileHeight = 16;
			//
			//GrpROMOptions
			//
			this.GrpROMOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpROMOptions.Controls.Add(this.BtnROMDecrypt);
			this.GrpROMOptions.Controls.Add(this.BtnROMOpenXorPad);
			this.GrpROMOptions.Controls.Add(this.BtnROMOpen);
			this.GrpROMOptions.ForeColor = System.Drawing.Color.White;
			this.GrpROMOptions.Location = new System.Drawing.Point(0, 480);
			this.GrpROMOptions.Name = "GrpROMOptions";
			this.GrpROMOptions.Size = new System.Drawing.Size(768, 80);
			this.GrpROMOptions.TabIndex = 31;
			this.GrpROMOptions.TabStop = false;
			this.GrpROMOptions.Text = "Options";
			//
			//BtnROMDecrypt
			//
			this.BtnROMDecrypt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnROMDecrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnROMDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnROMDecrypt.Location = new System.Drawing.Point(156, 18);
			this.BtnROMDecrypt.Name = "BtnROMDecrypt";
			this.BtnROMDecrypt.Size = new System.Drawing.Size(72, 24);
			this.BtnROMDecrypt.TabIndex = 4;
			this.BtnROMDecrypt.Text = "Decrypt";
			this.BtnROMDecrypt.UseVisualStyleBackColor = true;
			//
			//BtnROMOpenXorPad
			//
			this.BtnROMOpenXorPad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnROMOpenXorPad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnROMOpenXorPad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnROMOpenXorPad.Location = new System.Drawing.Point(78, 18);
			this.BtnROMOpenXorPad.Name = "BtnROMOpenXorPad";
			this.BtnROMOpenXorPad.Size = new System.Drawing.Size(72, 24);
			this.BtnROMOpenXorPad.TabIndex = 3;
			this.BtnROMOpenXorPad.Text = "Open XOR";
			this.BtnROMOpenXorPad.UseVisualStyleBackColor = true;
			//
			//BtnROMOpen
			//
			this.BtnROMOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnROMOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnROMOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnROMOpen.Location = new System.Drawing.Point(0, 18);
			this.BtnROMOpen.Name = "BtnROMOpen";
			this.BtnROMOpen.Size = new System.Drawing.Size(72, 24);
			this.BtnROMOpen.TabIndex = 2;
			this.BtnROMOpen.Text = "Open";
			this.BtnROMOpen.UseVisualStyleBackColor = true;
			//
			//SearchPage
			//
			this.SearchPage.BackColor = System.Drawing.Color.Transparent;
			this.SearchPage.Controls.Add(this.GrpMatches);
			this.SearchPage.Controls.Add(this.GrpSearchOptions);
			this.SearchPage.ForeColor = System.Drawing.Color.White;
			this.SearchPage.Location = new System.Drawing.Point(28, 4);
			this.SearchPage.Name = "SearchPage";
			this.SearchPage.Size = new System.Drawing.Size(768, 560);
			this.SearchPage.TabIndex = 4;
			this.SearchPage.Text = "Search";
			//
			//GrpMatches
			//
			this.GrpMatches.Controls.Add(this.LstMatches);
			this.GrpMatches.ForeColor = System.Drawing.Color.White;
			this.GrpMatches.Location = new System.Drawing.Point(0, 0);
			this.GrpMatches.Name = "GrpMatches";
			this.GrpMatches.Size = new System.Drawing.Size(768, 480);
			this.GrpMatches.TabIndex = 28;
			this.GrpMatches.TabStop = false;
			this.GrpMatches.Text = "Matches";
			//
			//LstMatches
			//
			this.LstMatches.Location = new System.Drawing.Point(0, 18);
			this.LstMatches.Name = "LstMatches";
			this.LstMatches.SelectedIndex = -1;
			this.LstMatches.Size = new System.Drawing.Size(768, 460);
			this.LstMatches.TabIndex = 3;
			this.LstMatches.TileHeight = 16;
			//
			//GrpSearchOptions
			//
			this.GrpSearchOptions.BackColor = System.Drawing.Color.Transparent;
			this.GrpSearchOptions.Controls.Add(this.TxtSearch);
			this.GrpSearchOptions.Controls.Add(this.ProgressSearch);
			this.GrpSearchOptions.Controls.Add(this.BtnSearch);
			this.GrpSearchOptions.ForeColor = System.Drawing.Color.White;
			this.GrpSearchOptions.Location = new System.Drawing.Point(0, 480);
			this.GrpSearchOptions.Name = "GrpSearchOptions";
			this.GrpSearchOptions.Size = new System.Drawing.Size(768, 80);
			this.GrpSearchOptions.TabIndex = 29;
			this.GrpSearchOptions.TabStop = false;
			this.GrpSearchOptions.Text = "Options";
			//
			//TxtSearch
			//
			this.TxtSearch.BackColor = System.Drawing.Color.Black;
			this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5f);
			this.TxtSearch.ForeColor = System.Drawing.Color.White;
			this.TxtSearch.Location = new System.Drawing.Point(0, 18);
			this.TxtSearch.Name = "TxtSearch";
			this.TxtSearch.Size = new System.Drawing.Size(150, 24);
			this.TxtSearch.TabIndex = 1;
			//
			//ProgressSearch
			//
			this.ProgressSearch.Location = new System.Drawing.Point(0, 48);
			this.ProgressSearch.Name = "ProgressSearch";
			this.ProgressSearch.Percentage = 0f;
			this.ProgressSearch.Size = new System.Drawing.Size(228, 24);
			this.ProgressSearch.TabIndex = 0;
			//
			//BtnSearch
			//
			this.BtnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.BtnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
			this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnSearch.Location = new System.Drawing.Point(156, 18);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(72, 24);
			this.BtnSearch.TabIndex = 2;
			this.BtnSearch.Text = "Search";
			this.BtnSearch.UseVisualStyleBackColor = true;
			//
			//Title
			//
			this.Title.AutoSize = true;
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Title.ForeColor = System.Drawing.Color.White;
			this.Title.Location = new System.Drawing.Point(349, 4);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(103, 25);
			this.Title.TabIndex = 18;
			this.Title.Text = "Ohana3DS";
			//
			//FrmMain
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.MainTabs);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.BtnMinimize);
			this.Controls.Add(this.BtnClose);
			this.Controls.Add(this.Splash);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ohana3DS";
			((System.ComponentModel.ISupportInitialize)this.Splash).EndInit();
			this.MainTabs.ResumeLayout(false);
			this.ModelPage.ResumeLayout(false);
			this.GrpOptions.ResumeLayout(false);
			this.GrpInfo.ResumeLayout(false);
			this.GrpInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.Screen).EndInit();
			this.TexturePage.ResumeLayout(false);
			this.GrpTexOptions.ResumeLayout(false);
			this.GrpTexInfo.ResumeLayout(false);
			this.GrpTexInfo.PerformLayout();
			this.GrpTexturePreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.ImgTexture).EndInit();
			this.GrpTextures.ResumeLayout(false);
			this.TextPage.ResumeLayout(false);
			this.GrpTextOptions.ResumeLayout(false);
			this.GrpTextStrings.ResumeLayout(false);
			this.GARCPage.ResumeLayout(false);
			this.GrpGARCOptions.ResumeLayout(false);
			this.GrpFiles.ResumeLayout(false);
			this.ROMPage.ResumeLayout(false);
			this.GrpROMLog.ResumeLayout(false);
			this.GrpROMOptions.ResumeLayout(false);
			this.SearchPage.ResumeLayout(false);
			this.GrpMatches.ResumeLayout(false);
			this.GrpSearchOptions.ResumeLayout(false);
			this.GrpSearchOptions.PerformLayout();
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
		internal Ohana3DS.MyTabcontrol MainTabs;
		internal System.Windows.Forms.TabPage ModelPage;
		internal System.Windows.Forms.TabPage TexturePage;
		internal System.Windows.Forms.TabPage TextPage;
		internal System.Windows.Forms.TabPage GARCPage;
		internal System.Windows.Forms.TabPage SearchPage;
		internal System.Windows.Forms.TabPage ROMPage;
		internal Ohana3DS.MyGroupbox GrpOptions;
		private System.Windows.Forms.Button withEventsField_BtnModelScale;
		internal System.Windows.Forms.Button BtnModelScale {
			get { return withEventsField_BtnModelScale; }
			set {
				if (withEventsField_BtnModelScale != null) {
					withEventsField_BtnModelScale.Click -= BtnModelScale_Click;
				}
				withEventsField_BtnModelScale = value;
				if (withEventsField_BtnModelScale != null) {
					withEventsField_BtnModelScale.Click += BtnModelScale_Click;
				}
			}
		}
		internal Ohana3DS.MyProgressbar ProgressModels;
		private System.Windows.Forms.Button withEventsField_BtnModelExportAllFF;
		internal System.Windows.Forms.Button BtnModelExportAllFF {
			get { return withEventsField_BtnModelExportAllFF; }
			set {
				if (withEventsField_BtnModelExportAllFF != null) {
					withEventsField_BtnModelExportAllFF.Click -= BtnModelExportAllFF_Click;
				}
				withEventsField_BtnModelExportAllFF = value;
				if (withEventsField_BtnModelExportAllFF != null) {
					withEventsField_BtnModelExportAllFF.Click += BtnModelExportAllFF_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnModelExport;
		internal System.Windows.Forms.Button BtnModelExport {
			get { return withEventsField_BtnModelExport; }
			set {
				if (withEventsField_BtnModelExport != null) {
					withEventsField_BtnModelExport.Click -= BtnModelExport_Click;
				}
				withEventsField_BtnModelExport = value;
				if (withEventsField_BtnModelExport != null) {
					withEventsField_BtnModelExport.Click += BtnModelExport_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnModelOpen;
		internal System.Windows.Forms.Button BtnModelOpen {
			get { return withEventsField_BtnModelOpen; }
			set {
				if (withEventsField_BtnModelOpen != null) {
					withEventsField_BtnModelOpen.Click -= BtnModelOpen_Click;
				}
				withEventsField_BtnModelOpen = value;
				if (withEventsField_BtnModelOpen != null) {
					withEventsField_BtnModelOpen.Click += BtnModelOpen_Click;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpInfo;
		private System.Windows.Forms.Button withEventsField_BtnModelTexturesMore;
		internal System.Windows.Forms.Button BtnModelTexturesMore {
			get { return withEventsField_BtnModelTexturesMore; }
			set {
				if (withEventsField_BtnModelTexturesMore != null) {
					withEventsField_BtnModelTexturesMore.Click -= BtnModelTexturesMore_Click;
				}
				withEventsField_BtnModelTexturesMore = value;
				if (withEventsField_BtnModelTexturesMore != null) {
					withEventsField_BtnModelTexturesMore.Click += BtnModelTexturesMore_Click;
				}
			}
		}
		internal System.Windows.Forms.Label LblInfoTextures;
		internal System.Windows.Forms.Label LblInfoBones;
		internal System.Windows.Forms.Label LblInfoTriangles;
		internal System.Windows.Forms.Label LblInfoVertices;
		internal System.Windows.Forms.Label LblInfoTexturesDummy;
		internal System.Windows.Forms.Label LblInfoBonesDummy;
		internal System.Windows.Forms.Label LblInfoTrianglesDummy;
		internal System.Windows.Forms.Label LblInfoVerticesDummy;
		private System.Windows.Forms.PictureBox withEventsField_Screen;
		internal System.Windows.Forms.PictureBox Screen {
			get { return withEventsField_Screen; }
			set {
				if (withEventsField_Screen != null) {
					withEventsField_Screen.MouseDown -= Screen_MouseDown;
					withEventsField_Screen.MouseUp -= Screen_MouseUp;
					withEventsField_Screen.MouseMove -= Screen_MouseMove;
					withEventsField_Screen.MouseWheel -= Screen_MouseWheel;
				}
				withEventsField_Screen = value;
				if (withEventsField_Screen != null) {
					withEventsField_Screen.MouseDown += Screen_MouseDown;
					withEventsField_Screen.MouseUp += Screen_MouseUp;
					withEventsField_Screen.MouseMove += Screen_MouseMove;
					withEventsField_Screen.MouseWheel += Screen_MouseWheel;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpTexOptions;
		private System.Windows.Forms.Button withEventsField_BtnTextureMode;
		internal System.Windows.Forms.Button BtnTextureMode {
			get { return withEventsField_BtnTextureMode; }
			set {
				if (withEventsField_BtnTextureMode != null) {
					withEventsField_BtnTextureMode.Click -= BtnTextureMode_Click;
				}
				withEventsField_BtnTextureMode = value;
				if (withEventsField_BtnTextureMode != null) {
					withEventsField_BtnTextureMode.Click += BtnTextureMode_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextureExportAllFF;
		internal System.Windows.Forms.Button BtnTextureExportAllFF {
			get { return withEventsField_BtnTextureExportAllFF; }
			set {
				if (withEventsField_BtnTextureExportAllFF != null) {
					withEventsField_BtnTextureExportAllFF.Click -= BtnTextureExportAllFF_Click;
				}
				withEventsField_BtnTextureExportAllFF = value;
				if (withEventsField_BtnTextureExportAllFF != null) {
					withEventsField_BtnTextureExportAllFF.Click += BtnTextureExportAllFF_Click;
				}
			}
		}
		internal Ohana3DS.MyProgressbar ProgressTextures;
		private System.Windows.Forms.Button withEventsField_BtnTextureExportAll;
		internal System.Windows.Forms.Button BtnTextureExportAll {
			get { return withEventsField_BtnTextureExportAll; }
			set {
				if (withEventsField_BtnTextureExportAll != null) {
					withEventsField_BtnTextureExportAll.Click -= BtnTextureExportAll_Click;
				}
				withEventsField_BtnTextureExportAll = value;
				if (withEventsField_BtnTextureExportAll != null) {
					withEventsField_BtnTextureExportAll.Click += BtnTextureExportAll_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextureExport;
		internal System.Windows.Forms.Button BtnTextureExport {
			get { return withEventsField_BtnTextureExport; }
			set {
				if (withEventsField_BtnTextureExport != null) {
					withEventsField_BtnTextureExport.Click -= BtnTextureExport_Click;
				}
				withEventsField_BtnTextureExport = value;
				if (withEventsField_BtnTextureExport != null) {
					withEventsField_BtnTextureExport.Click += BtnTextureExport_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextureOpen;
		internal System.Windows.Forms.Button BtnTextureOpen {
			get { return withEventsField_BtnTextureOpen; }
			set {
				if (withEventsField_BtnTextureOpen != null) {
					withEventsField_BtnTextureOpen.Click -= BtnTextureOpen_Click;
				}
				withEventsField_BtnTextureOpen = value;
				if (withEventsField_BtnTextureOpen != null) {
					withEventsField_BtnTextureOpen.Click += BtnTextureOpen_Click;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpTexInfo;
		internal System.Windows.Forms.Label LblInfoTextureCD;
		internal System.Windows.Forms.Label LblInfoTextureFormat;
		internal System.Windows.Forms.Label LblInfoTextureResolution;
		internal System.Windows.Forms.Label LblInfoTextureCDDummy;
		internal System.Windows.Forms.Label LblInfoTextureFormatDummy;
		internal System.Windows.Forms.Label LblInfoTextureResolutionDummy;
		internal System.Windows.Forms.Label LblInfoTextureIndex;
		internal System.Windows.Forms.Label LblInfoTextureIndexDummy;
		internal Ohana3DS.MyGroupbox GrpTexturePreview;
		internal Ohana3DS.MyGroupbox GrpTextures;
		internal Ohana3DS.MyGroupbox GrpGARCOptions;
		internal Ohana3DS.MyProgressbar ProgressGARC;
		private System.Windows.Forms.Button withEventsField_BtnGARCExtractAll;
		internal System.Windows.Forms.Button BtnGARCExtractAll {
			get { return withEventsField_BtnGARCExtractAll; }
			set {
				if (withEventsField_BtnGARCExtractAll != null) {
					withEventsField_BtnGARCExtractAll.Click -= BtnGARCExtractAll_Click;
				}
				withEventsField_BtnGARCExtractAll = value;
				if (withEventsField_BtnGARCExtractAll != null) {
					withEventsField_BtnGARCExtractAll.Click += BtnGARCExtractAll_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnGARCExtract;
		internal System.Windows.Forms.Button BtnGARCExtract {
			get { return withEventsField_BtnGARCExtract; }
			set {
				if (withEventsField_BtnGARCExtract != null) {
					withEventsField_BtnGARCExtract.Click -= BtnGARCExtract_Click;
				}
				withEventsField_BtnGARCExtract = value;
				if (withEventsField_BtnGARCExtract != null) {
					withEventsField_BtnGARCExtract.Click += BtnGARCExtract_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnGARCOpen;
		internal System.Windows.Forms.Button BtnGARCOpen {
			get { return withEventsField_BtnGARCOpen; }
			set {
				if (withEventsField_BtnGARCOpen != null) {
					withEventsField_BtnGARCOpen.Click -= BtnOpenGARC_Click;
				}
				withEventsField_BtnGARCOpen = value;
				if (withEventsField_BtnGARCOpen != null) {
					withEventsField_BtnGARCOpen.Click += BtnOpenGARC_Click;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpFiles;
		internal Ohana3DS.MyGroupbox GrpMatches;
		internal Ohana3DS.MyGroupbox GrpSearchOptions;
		internal System.Windows.Forms.TextBox TxtSearch;
		internal Ohana3DS.MyProgressbar ProgressSearch;
		private System.Windows.Forms.Button withEventsField_BtnSearch;
		internal System.Windows.Forms.Button BtnSearch {
			get { return withEventsField_BtnSearch; }
			set {
				if (withEventsField_BtnSearch != null) {
					withEventsField_BtnSearch.Click -= BtnSearch_Click;
				}
				withEventsField_BtnSearch = value;
				if (withEventsField_BtnSearch != null) {
					withEventsField_BtnSearch.Click += BtnSearch_Click;
				}
			}
		}
		private System.Windows.Forms.PictureBox withEventsField_Splash;
		internal System.Windows.Forms.PictureBox Splash {
			get { return withEventsField_Splash; }
			set {
				if (withEventsField_Splash != null) {
					withEventsField_Splash.Click -= Splash_Click;
				}
				withEventsField_Splash = value;
				if (withEventsField_Splash != null) {
					withEventsField_Splash.Click += Splash_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextureInsert;
		internal System.Windows.Forms.Button BtnTextureInsert {
			get { return withEventsField_BtnTextureInsert; }
			set {
				if (withEventsField_BtnTextureInsert != null) {
					withEventsField_BtnTextureInsert.Click -= BtnTextureInsert_Click;
				}
				withEventsField_BtnTextureInsert = value;
				if (withEventsField_BtnTextureInsert != null) {
					withEventsField_BtnTextureInsert.Click += BtnTextureInsert_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextureSave;
		internal System.Windows.Forms.Button BtnTextureSave {
			get { return withEventsField_BtnTextureSave; }
			set {
				if (withEventsField_BtnTextureSave != null) {
					withEventsField_BtnTextureSave.Click -= BtnTextureSave_Click;
				}
				withEventsField_BtnTextureSave = value;
				if (withEventsField_BtnTextureSave != null) {
					withEventsField_BtnTextureSave.Click += BtnTextureSave_Click;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpTextOptions;
		private System.Windows.Forms.Button withEventsField_BtnTextExport;
		internal System.Windows.Forms.Button BtnTextExport {
			get { return withEventsField_BtnTextExport; }
			set {
				if (withEventsField_BtnTextExport != null) {
					withEventsField_BtnTextExport.Click -= BtnTextExport_Click;
				}
				withEventsField_BtnTextExport = value;
				if (withEventsField_BtnTextExport != null) {
					withEventsField_BtnTextExport.Click += BtnTextExport_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextOpen;
		internal System.Windows.Forms.Button BtnTextOpen {
			get { return withEventsField_BtnTextOpen; }
			set {
				if (withEventsField_BtnTextOpen != null) {
					withEventsField_BtnTextOpen.Click -= BtnTextOpen_Click;
				}
				withEventsField_BtnTextOpen = value;
				if (withEventsField_BtnTextOpen != null) {
					withEventsField_BtnTextOpen.Click += BtnTextOpen_Click;
				}
			}
		}
		internal Ohana3DS.MyGroupbox GrpTextStrings;
		private System.Windows.Forms.Button withEventsField_BtnTextImport;
		internal System.Windows.Forms.Button BtnTextImport {
			get { return withEventsField_BtnTextImport; }
			set {
				if (withEventsField_BtnTextImport != null) {
					withEventsField_BtnTextImport.Click -= BtnTextImport_Click;
				}
				withEventsField_BtnTextImport = value;
				if (withEventsField_BtnTextImport != null) {
					withEventsField_BtnTextImport.Click += BtnTextImport_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnTextSave;
		internal System.Windows.Forms.Button BtnTextSave {
			get { return withEventsField_BtnTextSave; }
			set {
				if (withEventsField_BtnTextSave != null) {
					withEventsField_BtnTextSave.Click -= BtnTextSave_Click;
				}
				withEventsField_BtnTextSave = value;
				if (withEventsField_BtnTextSave != null) {
					withEventsField_BtnTextSave.Click += BtnTextSave_Click;
				}
			}
		}
		private Ohana3DS.MyListview withEventsField_LstTextures;
		internal Ohana3DS.MyListview LstTextures {
			get { return withEventsField_LstTextures; }
			set {
				if (withEventsField_LstTextures != null) {
					withEventsField_LstTextures.SelectedIndexChanged -= LstTextures_SelectedIndexChanged;
				}
				withEventsField_LstTextures = value;
				if (withEventsField_LstTextures != null) {
					withEventsField_LstTextures.SelectedIndexChanged += LstTextures_SelectedIndexChanged;
				}
			}
		}
		internal Ohana3DS.MyListview LstFiles;
		internal Ohana3DS.MyListview LstMatches;
		private System.Windows.Forms.Button withEventsField_BtnGARCInsert;
		internal System.Windows.Forms.Button BtnGARCInsert {
			get { return withEventsField_BtnGARCInsert; }
			set {
				if (withEventsField_BtnGARCInsert != null) {
					withEventsField_BtnGARCInsert.Click -= BtnGARCInsert_Click;
				}
				withEventsField_BtnGARCInsert = value;
				if (withEventsField_BtnGARCInsert != null) {
					withEventsField_BtnGARCInsert.Click += BtnGARCInsert_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnGARCSave;
		internal System.Windows.Forms.Button BtnGARCSave {
			get { return withEventsField_BtnGARCSave; }
			set {
				if (withEventsField_BtnGARCSave != null) {
					withEventsField_BtnGARCSave.Click -= BtnGARCSave_Click;
				}
				withEventsField_BtnGARCSave = value;
				if (withEventsField_BtnGARCSave != null) {
					withEventsField_BtnGARCSave.Click += BtnGARCSave_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnGARCCompression;
		internal System.Windows.Forms.Button BtnGARCCompression {
			get { return withEventsField_BtnGARCCompression; }
			set {
				if (withEventsField_BtnGARCCompression != null) {
					withEventsField_BtnGARCCompression.Click -= BtnGARCCompression_Click;
				}
				withEventsField_BtnGARCCompression = value;
				if (withEventsField_BtnGARCCompression != null) {
					withEventsField_BtnGARCCompression.Click += BtnGARCCompression_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnModelVertexEditor;
		internal System.Windows.Forms.Button BtnModelVertexEditor {
			get { return withEventsField_BtnModelVertexEditor; }
			set {
				if (withEventsField_BtnModelVertexEditor != null) {
					withEventsField_BtnModelVertexEditor.Click -= BtnModelVertexEditor_Click;
				}
				withEventsField_BtnModelVertexEditor = value;
				if (withEventsField_BtnModelVertexEditor != null) {
					withEventsField_BtnModelVertexEditor.Click += BtnModelVertexEditor_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnModelSave;
		internal System.Windows.Forms.Button BtnModelSave {
			get { return withEventsField_BtnModelSave; }
			set {
				if (withEventsField_BtnModelSave != null) {
					withEventsField_BtnModelSave.Click -= BtnModelSave_Click;
				}
				withEventsField_BtnModelSave = value;
				if (withEventsField_BtnModelSave != null) {
					withEventsField_BtnModelSave.Click += BtnModelSave_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnModelMapEditor;
		internal System.Windows.Forms.Button BtnModelMapEditor {
			get { return withEventsField_BtnModelMapEditor; }
			set {
				if (withEventsField_BtnModelMapEditor != null) {
					withEventsField_BtnModelMapEditor.Click -= BtnModelMapEditor_Click;
				}
				withEventsField_BtnModelMapEditor = value;
				if (withEventsField_BtnModelMapEditor != null) {
					withEventsField_BtnModelMapEditor.Click += BtnModelMapEditor_Click;
				}
			}
		}
		internal Ohana3DS.MyListview LstStrings;
		internal Ohana3DS.MyPicturebox ImgTexture;
		private System.Windows.Forms.Button withEventsField_BtnTextureInsertAll;
		internal System.Windows.Forms.Button BtnTextureInsertAll {
			get { return withEventsField_BtnTextureInsertAll; }
			set {
				if (withEventsField_BtnTextureInsertAll != null) {
					withEventsField_BtnTextureInsertAll.Click -= BtnTextureInsertAll_Click;
				}
				withEventsField_BtnTextureInsertAll = value;
				if (withEventsField_BtnTextureInsertAll != null) {
					withEventsField_BtnTextureInsertAll.Click += BtnTextureInsertAll_Click;
				}
			}
		}
		internal System.Windows.Forms.Label LblModelName;
		internal System.Windows.Forms.ToolTip ModelNameTip;
		internal Ohana3DS.MyGroupbox GrpROMLog;
		internal Ohana3DS.MyListview LstROMLog;
		internal Ohana3DS.MyGroupbox GrpROMOptions;
		private System.Windows.Forms.Button withEventsField_BtnROMDecrypt;
		internal System.Windows.Forms.Button BtnROMDecrypt {
			get { return withEventsField_BtnROMDecrypt; }
			set {
				if (withEventsField_BtnROMDecrypt != null) {
					withEventsField_BtnROMDecrypt.Click -= BtnROMDecrypt_Click;
				}
				withEventsField_BtnROMDecrypt = value;
				if (withEventsField_BtnROMDecrypt != null) {
					withEventsField_BtnROMDecrypt.Click += BtnROMDecrypt_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnROMOpenXorPad;
		internal System.Windows.Forms.Button BtnROMOpenXorPad {
			get { return withEventsField_BtnROMOpenXorPad; }
			set {
				if (withEventsField_BtnROMOpenXorPad != null) {
					withEventsField_BtnROMOpenXorPad.Click -= BtnROMOpenXorPad_Click;
				}
				withEventsField_BtnROMOpenXorPad = value;
				if (withEventsField_BtnROMOpenXorPad != null) {
					withEventsField_BtnROMOpenXorPad.Click += BtnROMOpenXorPad_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_BtnROMOpen;
		internal System.Windows.Forms.Button BtnROMOpen {
			get { return withEventsField_BtnROMOpen; }
			set {
				if (withEventsField_BtnROMOpen != null) {
					withEventsField_BtnROMOpen.Click -= BtnROMOpen_Click;
				}
				withEventsField_BtnROMOpen = value;
				if (withEventsField_BtnROMOpen != null) {
					withEventsField_BtnROMOpen.Click += BtnROMOpen_Click;
				}
			}
		}

		internal System.Windows.Forms.ColorDialog colorBG;
	}
}
