using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static Ohana3DS.MyListview;

namespace Ohana3DS
{
    public partial class FrmMain
	{

		#region "Declares"
		//Classes de textos, Compressão/Extração

		Minko MyMinko = new Minko();
		//Movimentação do modelo
		int Rot_InitX;
		int Rot_InitY;
		int Rot_FinalX;
		int Rot_FinalY;
		int Mov_InitX;
		int Mov_InitY;
		int Mov_FinalX;

		int Mov_FinalY;
		string Current_Model;
		string Current_Opened_Text;

		string Current_Text_Temp;
		private enum TextureMode
		{
			Original,
			FlipY,
			Mirror,
			FlipY_Mirror
		}

		TextureMode Texture_Mode = TextureMode.Original;
		Thread Model_Export_Thread;
		Thread Texture_Thread;
		Thread GARC_Thread;

		Thread Search_Thread;
		bool Lighting = true;

		bool First_Click;
		private struct NCCH
		{
			public int Offset;
			public int Length;
			public string Product_Code;
			public byte[] Partition_ID;
			public long ExHeader_Size;
			public long ExeFS_Offset;
			public long ExeFS_Size;
			public long RomFS_Offset;
			public long RomFS_Size;
		}
		NCCH[] NCCH_Container = new NCCH[8];
		private struct Info_Header
		{
			public int Offset;
			public int Length;
		}
		private struct Rom_Directory
		{
			public int Parent_Offset;
			public int Sibling_Offset;
			public int Children_Offset;
			public int File_Offset;
			public int Unknow;
			public string Name;
		}
		private struct Rom_File
		{
			public int Parent_Offset;
			public int Sibling_Offset;
			public UInt64 Data_Offset;
			public UInt64 Data_Length;
			public int Unknow;
			public string Name;
		}
		string Current_ROM;
			#endregion
		string Current_XORPad;

		#region "GUI"

		#region "General"
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg != 0xa3)
				base.WndProc(ref m);
			switch (m.Msg) {
				case 0x84:
					Point Mouse_Position = PointToClient(Cursor.Position);
					if (Mouse_Position.Y < 32)
						if (m.Result == new IntPtr(1))
							m.Result = new IntPtr(2);
					break;
			}
		}
		private delegate void FileDrop(string File_Name);
		private FileDrop MyFileDrop;
		private void FrmMain_Load(object sender, EventArgs e)
		{
			for (int i = 0; i <= 7; i++) {
				Common.Power_Of_Two[i] = Convert.ToByte(Math.Pow(2, i));
			}

			this.AllowDrop = true;
			MyFileDrop = new FileDrop(File_Dropped);

			Common.MyOhana.Scale = Ohana3DS.My.MySettings.Default.ModelScale;
			BtnModelScale.Text = "Model scale: 1:" + Ohana3DS.My.MySettings.Default.ModelScale;
			switch (Ohana3DS.My.MySettings.Default.TextureFlipMirror) {
				case 0:
					BtnTextureMode.Text = "Original";
					Texture_Mode = TextureMode.Original;
					break;
				case 1:
					BtnTextureMode.Text = "Flip-Y";
					Texture_Mode = TextureMode.FlipY;
					break;
				case 2:
					BtnTextureMode.Text = "Mirror-X";
					Texture_Mode = TextureMode.Mirror;
					break;
				case 3:
					BtnTextureMode.Text = "Flip/Mirror";
					Texture_Mode = TextureMode.FlipY_Mirror;
					break;
			}
			Common.MyNako.Fast_Compression = Ohana3DS.My.MySettings.Default.FastCompression;
			if (Common.MyNako.Fast_Compression)
				BtnGARCCompression.Text = "Fast compression";

			Disable_Model_Buttons();
			Disable_Texture_Buttons();
			Disable_Text_Buttons();
			Disable_GARC_Buttons();
			BtnROMDecrypt.Enabled = false;

			Common.MyOhana.Initialize(Screen);
			Show();
			Common.MyOhana.Render();
		}
		private void FrmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.C) {
				if (colorBG.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					Screen.BackColor = colorBG.Color;
					Common.MyOhana.bgCol = colorBG.Color;
				}
			}
			if (File.Exists(Current_Model)) {
				if (e.KeyCode == Keys.Left | e.KeyCode == Keys.Right) {
					string Model_Name = Path.GetFileName(Current_Model);
					FileInfo[] Input_Files = new DirectoryInfo(Path.GetDirectoryName(Current_Model)).GetFiles();
					switch (e.KeyCode) {
						case Keys.Left:
							for (int Index = 1; Index <= Input_Files.Count() - 1; Index++) {
								if (Input_Files[Index].Name == Model_Name) {
									int i = 1;
									do {
										string CurrFile = Input_Files[Index - i].FullName;
										if (IsModel(CurrFile)) {
											if (Open_Model(CurrFile, false))
												return;
										}
										i += 1;
										if (Index - i < 1)
											break; // TODO: might not be correct. Was : Exit For
									} while (true);
									break; // TODO: might not be correct. Was : Exit For
								}
							}

							break;
						case Keys.Right:
							for (int Index = 0; Index <= Input_Files.Count() - 2; Index++) {
								if (Input_Files[Index].Name == Model_Name) {
									int i = 1;
									do {
										string CurrFile = Input_Files[Index + i].FullName;
										if (IsModel(CurrFile)) {
											if (Open_Model(CurrFile, false))
												return;
										}
										i += 1;
										if (Index + i > Input_Files.Count() - 2)
											return;
									} while (true);
									break; // TODO: might not be correct. Was : Exit For
								}
							}

							break;
					}
				}

				switch (e.KeyCode) {
					case Keys.F9:
						Common.MyOhana.Lighting = !Common.MyOhana.Lighting;
						Common.MyOhana.Switch_Lighting(Common.MyOhana.Lighting);
						break;
				}
			}
		}
		private void FrmMain_DragDrop(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);
			this.BeginInvoke(MyFileDrop, Files[0]);
			this.Activate();
		}
		private void FrmMain_DragEnter(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void Splash_Click(object sender, EventArgs e)
		{
			MainTabs.Visible = true;
			Splash.Visible = false;
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}
		private void BtnMinimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		private void Button_MouseEnter(object sender, EventArgs e)
		{
			Label Lbl = (Label)sender;
			Lbl.BackColor = Color.FromArgb(15, 82, 186);
			Lbl.ForeColor = Color.White;
		}
		private void BtnClose_MouseEnter(object sender, EventArgs e)
		{
			Label Lbl = (Label)sender;
			Lbl.BackColor = Color.Crimson;
			Lbl.ForeColor = Color.WhiteSmoke;
		}
		private void Button_MouseLeave(object sender, EventArgs e)
		{
			Label Lbl = (Label)sender;
			Lbl.BackColor = Color.Transparent;
			Lbl.ForeColor = Color.White;
		}

		private void File_Dropped(string File_Name)
		{
			FileStream Temp = new FileStream(File_Name, FileMode.Open);

			string Magic_2_Bytes = Common.ReadMagic(Temp, 0, 2);
			string Magic_3_Bytes = Common.ReadMagic(Temp, 0, 3);
			string Magic_4_Bytes = Common.ReadMagic(Temp, 0, 4);
			string CLIM_Magic = Common.ReadMagic(Temp, Convert.ToInt32(Temp.Length - 40), 4);
			bool Text_Check = Common.Read32(Temp, 4) == Temp.Length - 0x10;
			Temp.Close();

			if (Magic_2_Bytes == "PC" | Magic_2_Bytes == "MM" | Magic_2_Bytes == "GR" | Magic_3_Bytes == "BCH") {
				MainTabs.SelectTab(0);
				Open_Model(File_Name);
			} else if (Magic_2_Bytes == "PT" | CLIM_Magic == "CLIM" | Magic_4_Bytes == "CGFX") {
				MainTabs.SelectTab(1);
				Open_Texture(File_Name);
			} else if (Magic_4_Bytes == "CRAG") {
				MainTabs.SelectTab(3);
				Open_GARC(File_Name);
			} else if (Text_Check) {
				MainTabs.SelectTab(2);
				Open_Text(File_Name);
			}
		}
		private bool IsModel(string File_Name)
		{
			FileStream Input = new FileStream(File_Name, FileMode.Open);
			string Magic = Common.ReadMagic(Input, 0, 3);
			string Magic_2_Bytes = Magic.Substring(0, 2);
			long Length = Input.Length;
			Input.Close();
			//Verifica se é um modelo válido
			if ((Magic_2_Bytes != "MM" & Magic_2_Bytes != "TM" & Magic_2_Bytes != "PC" & Magic_2_Bytes != "GR" & Magic != "BCH") | Length < 0x80) {
				return false;
			}
			return true;
		}
		#endregion

		#region "Common"
		private delegate void Up_Progress(MyProgressbar ProgressBar, float Percentage, string Msg);
		private delegate void Add_Item(MyListview List, MyListview.ListItem Item, bool Scroll_To_End);
		private delegate void Update_Button(Button Button, string Text);
		private delegate void Change_Picture(MyPicturebox Ctrl, Image Img);
		private delegate void Change_Enabled(Control Ctrl, bool Enabled);
		private void Update_Progress(MyProgressbar ProgressBar, float Percentage, string Msg)
		{
			if (ProgressBar.InvokeRequired) {
				this.Invoke(new Up_Progress(Update_Progress), ProgressBar, Percentage, Msg);
			} else {
				ProgressBar.Text = Msg;
				ProgressBar.Percentage = Percentage;
				ProgressBar.Refresh();
			}
		}
		private void Add_List_Item(MyListview List, MyListview.ListItem Item, bool Scroll_To_End)
		{
			if (List.InvokeRequired) {
				this.Invoke(new Add_Item(Add_List_Item), List, Item, Scroll_To_End);
			} else {
				List.AddItem(Item);
				if (Scroll_To_End) {
					List.Scroll_To_End();
					List.Refresh();
				}
			}
		}
		private void Update_Button_Text(Button Button, string Text)
		{
			if (Button.InvokeRequired) {
				this.Invoke(new Update_Button(Update_Button_Text), Button, Text);
			} else {
				Button.Text = Text;
				Button.Refresh();
			}
		}
		private void Set_Image(MyPicturebox Picture, Image Img)
		{
			if (Picture.InvokeRequired) {
				this.Invoke(new Change_Picture(Set_Image), Picture, Img);
			} else {
				Picture.Image = Img;
				Picture.Refresh();
			}
		}
		private void Set_Enabled(Control Ctrl, bool Enabled)
		{
			if (Ctrl.InvokeRequired) {
				this.Invoke(new Change_Enabled(Set_Enabled), Ctrl, Enabled);
			} else {
				Ctrl.Enabled = Enabled;
			}
		}

		private string Format_Size(int Bytes)
		{
			if (Bytes >= 1073741824) {
				return Strings.Format(Bytes / 1024 / 1024 / 1024, "#0.00") + " GB";
			} else if (Bytes >= 1048576) {
				return Strings.Format(Bytes / 1024 / 1024, "#0.00") + " MB";
			} else if (Bytes >= 1024) {
				return Strings.Format(Bytes / 1024, "#0.00") + " KB";
			} else {
				return Bytes.ToString() + " B";
			}
		}
		#endregion

		#region "Model"
		private void BtnModelOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open Pokémon BCH model";
			OpenDlg.Filter = "BCH Model|*.*";
			if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				First_Click = true;
				if (File.Exists(OpenDlg.FileName))
					Open_Model(OpenDlg.FileName);
			}
		}
		private bool Open_Model(string File_Name, bool Show_Warning = true)
		{
			bool Response = false;

			try {
				Current_Model = File_Name;
				Common.MyOhana.Rendering = false;
				Response = Common.MyOhana.Load_Model(File_Name);
				if (Response) {
					Common.MyOhana.Rendering = true;
					LblModelName.Text = Path.GetFileName(File_Name);
					ModelNameTip.SetToolTip(LblModelName, LblModelName.Text);

					if (Common.MyOhana.BCH_Have_Textures) {
						Enable_Texture_Buttons();
						LstTextures.Clear();
						ImgTexture.Image = null;

						for (int Index = 0; Index <= Common.MyOhana.Model_Texture.Count - 1; Index++) {
							LstTextures.AddItem(Common.MyOhana.Model_Texture[Index].Name);
						}
					}

					Rot_InitX = 0;
					Rot_InitY = 0;
					Rot_FinalX = 0;
					Rot_FinalY = 0;
					Mov_InitX = 0;
					Mov_InitY = 0;
					Mov_FinalX = 0;
					Mov_FinalY = 0;

					Common.MyOhana.Rotation.X = 0;
					Common.MyOhana.Rotation.Y = 0;
					Common.MyOhana.Translation.X = 0;
					Common.MyOhana.Translation.Y = 0;

					LblInfoVertices.Text = Common.MyOhana.Info.Vertex_Count.ToString();
					LblInfoTriangles.Text = Common.MyOhana.Info.Triangles_Count.ToString();
					LblInfoBones.Text = Common.MyOhana.Info.Bones_Count.ToString();
					LblInfoTextures.Text = Common.MyOhana.Info.Textures_Count.ToString();

					Enable_Model_Buttons();
					if (Common.MyOhana.Magic.Substring(0, 2) == "GR") {
						BtnModelMapEditor.Enabled = true;
						if (My.MyProject.Forms.FrmMapProp.IsHandleCreated)
							My.MyProject.Forms.FrmMapProp.makeMapIMG(MapProps());
					} else {
						BtnModelMapEditor.Enabled = false;
					}
				} else {
					if (Show_Warning)
						MessageBox.Show("This file is not a model file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} catch {
				Response = false;
				Common.MyOhana.Model_Object = null;
				if (Show_Warning)
					MessageBox.Show("Sorry, something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if (!Response) {
				LblModelName.Text = null;
				ModelNameTip.SetToolTip(LblModelName, null);
				LblInfoVertices.Text = "0";
				LblInfoTriangles.Text = "0";
				LblInfoBones.Text = "0";
				LblInfoTextures.Text = "0";
				Disable_Model_Buttons();
			}

			Application.DoEvents();
			//Processa o click que foi para o PictureBox, porém será ignorado devido ao First_Click
			First_Click = false;
			Screen.Refresh();

			return Response;
		}
		private void Enable_Model_Buttons()
		{
			BtnModelExport.Enabled = true;
			BtnModelSave.Enabled = true;
			BtnModelVertexEditor.Enabled = true;
			BtnModelTexturesMore.Enabled = true;
		}
		private void Disable_Model_Buttons()
		{
			BtnModelExport.Enabled = false;
			BtnModelSave.Enabled = false;
			BtnModelVertexEditor.Enabled = false;
			BtnModelMapEditor.Enabled = false;
			BtnModelTexturesMore.Enabled = false;
		}
		private void BtnModelExport_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Model_Object != null) {
				SaveFileDialog SaveDlg = new SaveFileDialog();
				SaveDlg.Title = "Save model";
				SaveDlg.Filter = "Valve SMD|*.smd";
				SaveDlg.FileName = Path.GetFileNameWithoutExtension(Common.MyOhana.Current_Model) + ".smd";
				if (SaveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					Common.MyOhana.Export_SMD(SaveDlg.FileName);
				}
			}
		}
		private void BtnModelExportAllFF_Click(object sender, EventArgs e)
		{
			if (Model_Export_Thread != null) {
				if (Model_Export_Thread.IsAlive) {
					Model_Export_Thread.Abort();
					BtnModelExportAllFF.Text = "Export all from folder";
					ProgressModels.Text = null;
					ProgressModels.Percentage = 0;
					ProgressModels.Refresh();

					return;
				}
			}

			FolderBrowserDialog InputDlg = new FolderBrowserDialog();
			if (InputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				FolderBrowserDialog OutputDlg = new FolderBrowserDialog();
				if (OutputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					Model_Export_Thread = new Thread(() => Model_Exporter(InputDlg.SelectedPath, OutputDlg.SelectedPath));
					Model_Export_Thread.Start();

					BtnModelExportAllFF.Text = "Cancel";
				}
			}
		}
		private void Model_Exporter(string InFolder, string OutFolder)
		{
			Ohana Exporter = new Ohana();
			FileInfo[] Input_Files = new DirectoryInfo(InFolder).GetFiles();
			int Total_Index = 0;
			int Index = 0;
			foreach (FileInfo File in Input_Files) {
				try {
					if (Exporter.Load_Model(File.FullName, false)) {
						if (Exporter.Model_Object.Length > 0) {
							string File_Name = Path.Combine(OutFolder, File.Name + ".smd");
							Exporter.Export_SMD(File_Name);
							Index += 1;
						}
					}
				} catch (Exception ex) {
					Debug.WriteLine("Model Exporter -> Erro ao exportar modelo: " + Path.GetFileName(File.FullName));
				}
				Update_Progress(ProgressModels, Convert.ToSingle((Total_Index / Input_Files.Count()) * 100), "Exporting " + Path.GetFileName(File.FullName) + "...");
				Total_Index += 1;
			}

			Update_Progress(ProgressModels, 0, null);
			Update_Button_Text(BtnModelExportAllFF, "Export all from folder");
		}
		private void BtnModelScale_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Scale == 1) {
				Common.MyOhana.Scale = 32;
				BtnModelScale.Text = "Model scale: 1:32";
			} else if (Common.MyOhana.Scale == 32) {
				Common.MyOhana.Scale = 64;
				BtnModelScale.Text = "Model scale: 1:64";
			} else if (Common.MyOhana.Scale == 64) {
				Common.MyOhana.Scale = 1;
				BtnModelScale.Text = "Model scale: 1:1";
			}
			Ohana3DS.My.MySettings.Default.ModelScale = Convert.ToInt32(Common.MyOhana.Scale);
			Ohana3DS.My.MySettings.Default.Save();
			BtnModelScale.Refresh();
		}
		private void BtnModelTexturesMore_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Model_Object != null) {
				My.MyProject.Forms.FrmTextureInfo.LstModelTextures.Clear();

				MyListview.ListItem Header_1 = default(MyListview.ListItem);
				Header_1.Text = new ListText[3];
				Header_1.Text[0].Text = "#";
				Header_1.Text[1].Left = 40;
				Header_1.Text[1].Text = "Texture";
				Header_1.Text[2].Left = 280;
				Header_1.Text[2].Text = "Normal(Bump) map";
				Header_1.Header = true;
				My.MyProject.Forms.FrmTextureInfo.LstModelTextures.AddItem(Header_1);

				for (int Index = 0; Index <= Common.MyOhana.Model_Texture_Index.Length - 1; Index++) {
					MyListview.ListItem Item = default(MyListview.ListItem);
					Item.Text = new ListText[3];

					Item.Text[0].Text = Index.ToString();
					Item.Text[1].Left = 40;
					Item.Text[1].Text = Common.MyOhana.Model_Texture_Index[Index];
					if (Common.MyOhana.Model_Texture != null) {
						bool Found = false;
						for (int Texture_Index = 0; Texture_Index <= Common.MyOhana.Model_Texture.Count - 1; Texture_Index++) {
							if (Common.MyOhana.Model_Texture[Texture_Index].Name == Common.MyOhana.Model_Texture_Index[Index]) {
								Found = true;
								break; // TODO: might not be correct. Was : Exit For
							}
						}
						if (!Found)
							Item.Text[1].ForeColor = Color.Crimson;
					} else {
						Item.Text[1].ForeColor = Color.Crimson;
					}

					Item.Text[2].Left = 280;
					Item.Text[2].Text = Common.MyOhana.Model_Bump_Map_Index[Index];
					My.MyProject.Forms.FrmTextureInfo.LstModelTextures.AddItem(Item);
				}

				My.MyProject.Forms.FrmTextureInfo.Show();
				My.MyProject.Forms.FrmTextureInfo.LstModelTextures.Refresh();
			}
		}
		private void BtnModelVertexEditor_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Model_Object != null)
				My.MyProject.Forms.FrmVertexEditor.Show();
		}
		private void BtnModelSave_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Current_Model != null) {
				File.Delete(Common.MyOhana.Current_Model);
				File.Copy(Common.MyOhana.Temp_Model_File, Common.MyOhana.Current_Model);
			}
		}

		private void Screen_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!First_Click) {
				if (e.Button == MouseButtons.Left) {
					Rot_InitX = MousePosition.X;
					Rot_InitY = MousePosition.Y;
				} else if (e.Button == MouseButtons.Right) {
					Mov_InitX = MousePosition.X;
					Mov_InitY = MousePosition.Y;
				}
			}
		}
		private void Screen_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!First_Click) {
				if (e.Button == MouseButtons.Left) {
					Rot_FinalX += (Rot_InitX - MousePosition.X);
					Rot_FinalY += (Rot_InitY - MousePosition.Y);
				} else if (e.Button == MouseButtons.Right) {
					Mov_FinalX += (Mov_InitX - MousePosition.X);
					Mov_FinalY += (Mov_InitY - MousePosition.Y);
				}
			}
		}
		private void Screen_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!Screen.Focused)
				Screen.Select();
			if (!First_Click) {
				if (e.Button == MouseButtons.Left) {
					Common.MyOhana.Rotation.X = (Rot_InitX - MousePosition.X) + Rot_FinalX;
					Common.MyOhana.Rotation.Y = (Rot_InitY - MousePosition.Y) + Rot_FinalY;
				} else if (e.Button == MouseButtons.Right) {
					Common.MyOhana.Translation.X = (Mov_InitX - MousePosition.X) + Mov_FinalX;
					Common.MyOhana.Translation.Y = (Mov_InitY - MousePosition.Y) + Mov_FinalY;
				}
			}
		}
		private void Screen_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			float z = (Control.ModifierKeys & Keys.Control) == Keys.Control ? 0.05f : 1f;
			if (e.Delta > 0) {
				Common.MyOhana.Zoom += z;
			} else {
				Common.MyOhana.Zoom -= z;
			}
		}
		#endregion

		#region "Textures"
		private void BtnTextureOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open Pokémon BCH Texture";
			OpenDlg.Filter = "BCH Texture|*.*";
			if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				if (File.Exists(OpenDlg.FileName))
					Open_Texture(OpenDlg.FileName);
			}
		}
		private void Open_Texture(string File_Name, bool Show_Warning = true)
		{
			try {
				LstTextures.Clear();
				ImgTexture.Image = null;

				Common.MyOhana.Load_Textures(File_Name);
				if (Common.MyOhana.Model_Texture.Count > 0) {
					Enable_Texture_Buttons();
					foreach (Ohana.OhanaTexture Texture in Common.MyOhana.Model_Texture) {
						LstTextures.AddItem(Texture.Name);
					}
					LstTextures.SelectedIndex = 0;
					LstTextures.Refresh();
					Select_Texture(0);
				} else {
					Disable_Texture_Buttons();
				}
			} catch {
				Disable_Texture_Buttons();
				if (Show_Warning)
					MessageBox.Show("Sorry, something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void Enable_Texture_Buttons()
		{
			BtnTextureExport.Enabled = true;
			BtnTextureExportAll.Enabled = true;
			BtnTextureInsert.Enabled = true;
			BtnTextureInsertAll.Enabled = true;
			BtnTextureSave.Enabled = true;
		}
		private void Disable_Texture_Buttons()
		{
			BtnTextureExport.Enabled = false;
			BtnTextureExportAll.Enabled = false;
			BtnTextureInsert.Enabled = false;
			BtnTextureInsertAll.Enabled = false;
			BtnTextureSave.Enabled = false;
		}
		private void LstTextures_SelectedIndexChanged(int Index)
		{
			if (Index > -1)
				Select_Texture(Index);
		}
		private void Select_Texture(int Index)
		{
			var _with1 = Common.MyOhana.Model_Texture[Index];
			ImgTexture.Image = _with1.Image;
			ImgTexture.Refresh();

			LblInfoTextureIndex.Text = Index + 1 + "/" + Common.MyOhana.Model_Texture.Count;
			LblInfoTextureResolution.Text = _with1.Image.Width + "x" + _with1.Image.Height;
			switch (_with1.Format) {
				case 0:
					LblInfoTextureFormat.Text = "32BPP";
					LblInfoTextureCD.Text = "32BPP";
					break;
				case 1:
					LblInfoTextureFormat.Text = "24BPP";
					LblInfoTextureCD.Text = "24BPP";
					break;
				case 2:
					LblInfoTextureFormat.Text = "RGBA5551";
					LblInfoTextureCD.Text = "16BPP";
					break;
				case 3:
					LblInfoTextureFormat.Text = "RGB565";
					LblInfoTextureCD.Text = "16BPP";
					break;
				case 4:
					LblInfoTextureFormat.Text = "RGBA4444";
					LblInfoTextureCD.Text = "16BPP";
					break;
				case 5:
					LblInfoTextureFormat.Text = "L8A8 (Grayscale)";
					LblInfoTextureCD.Text = "16BPP";
					break;
				case 6:
					LblInfoTextureFormat.Text = "HILO8";
					LblInfoTextureCD.Text = "8BPP";
					break;
				case 7:
					LblInfoTextureFormat.Text = "L8 (Grayscale)";
					LblInfoTextureCD.Text = "8BPP";
					break;
				case 8:
					LblInfoTextureFormat.Text = "A8 (Alpha only)";
					LblInfoTextureCD.Text = "8BPP";
					break;
				case 9:
					LblInfoTextureFormat.Text = "L4A4 (Grayscale)";
					LblInfoTextureCD.Text = "8BPP";
					break;
				case 10:
					LblInfoTextureFormat.Text = "L4 (Grayscale)";
					LblInfoTextureCD.Text = "4BPP";
					break;
				case 12:
					LblInfoTextureFormat.Text = "ETC1 (iPACKMAN)";
					LblInfoTextureCD.Text = "24BPP";
					break;
				case 13:
					LblInfoTextureFormat.Text = "ETC1 + Alpha";
					LblInfoTextureCD.Text = "32BPP";
					break;
			}
		}
		private void BtnTextureExport_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Model_Texture != null) {
				if (LstTextures.SelectedIndex > -1) {
					SaveFileDialog SaveDlg = new SaveFileDialog();
					SaveDlg.Title = "Save image";
					SaveDlg.Filter = "Image|*.png";
					SaveDlg.FileName = Common.MyOhana.Model_Texture[LstTextures.SelectedIndex].Name + ".png";
					if (SaveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
						Bitmap Img = new Bitmap(Common.MyOhana.Model_Texture[LstTextures.SelectedIndex].Image);
						if (Texture_Mode == TextureMode.FlipY | Texture_Mode == TextureMode.FlipY_Mirror)
							Img.RotateFlip(RotateFlipType.RotateNoneFlipY);
						if (Texture_Mode == TextureMode.Mirror | Texture_Mode == TextureMode.FlipY_Mirror)
							Img = Mirror_Image(Img);
						Img.Save(SaveDlg.FileName);
					}
				}
			}
		}
		private void BtnTextureExportAll_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Model_Texture != null) {
				FolderBrowserDialog OutputDlg = new FolderBrowserDialog();
				if (OutputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					foreach (Ohana.OhanaTexture Texture in Common.MyOhana.Model_Texture) {
						string File_Name = Path.Combine(OutputDlg.SelectedPath, Texture.Name + ".png");
						Bitmap Img = new Bitmap(Texture.Image);
						if (Texture_Mode == TextureMode.FlipY | Texture_Mode == TextureMode.FlipY_Mirror)
							Img.RotateFlip(RotateFlipType.RotateNoneFlipY);
						if (Texture_Mode == TextureMode.Mirror | Texture_Mode == TextureMode.FlipY_Mirror)
							Img = Mirror_Image(Img);
						Img.Save(File_Name);
					}
				}
			}
		}
		private void BtnTextureExportAllFF_Click(object sender, EventArgs e)
		{
			if (Texture_Thread != null) {
				if (Texture_Thread.IsAlive) {
					Texture_Thread.Abort();
					BtnTextureExportAllFF.Text = "Export all from folder";
					ProgressTextures.Text = null;
					ProgressTextures.Percentage = 0;
					ProgressTextures.Refresh();

					BtnTextureOpen.Enabled = true;
					BtnTextureSave.Enabled = true;
					BtnTextureInsert.Enabled = true;
					BtnTextureInsertAll.Enabled = true;

					return;
				}
			}

			FolderBrowserDialog InputDlg = new FolderBrowserDialog();
			if (InputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				FolderBrowserDialog OutputDlg = new FolderBrowserDialog();
				if (OutputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					Texture_Thread = new Thread(() => Texture_Exporter(InputDlg.SelectedPath, OutputDlg.SelectedPath));
					Texture_Thread.Start();

					BtnTextureExportAllFF.Text = "Cancel";
					BtnTextureOpen.Enabled = false;
					BtnTextureSave.Enabled = false;
					BtnTextureInsert.Enabled = false;
					BtnTextureInsertAll.Enabled = false;
				}
			}
		}
		private void Texture_Exporter(string InFolder, string OutFolder)
		{
			Ohana Exporter = new Ohana();
			FileInfo[] Input_Files = new DirectoryInfo(InFolder).GetFiles();
			int Total_Index = 0;
			int Index = 0;
			foreach (FileInfo File in Input_Files) {
				try {
					Exporter.Load_Textures(File.FullName, false);
					if (Exporter.Model_Texture.Count > 0) {
						string Output_Folder = Path.Combine(OutFolder, Path.GetFileNameWithoutExtension(File.Name));
						Directory.CreateDirectory(Output_Folder);
						foreach (Ohana.OhanaTexture Texture in Exporter.Model_Texture) {
							string File_Name = Path.Combine(Output_Folder, Texture.Name + ".png");
							if (Texture_Mode == TextureMode.Original) {
								Texture.Image.Save(File_Name);
							} else {
								Bitmap Img = new Bitmap(Texture.Image);
								if (Texture_Mode == TextureMode.FlipY | Texture_Mode == TextureMode.FlipY_Mirror)
									Img.RotateFlip(RotateFlipType.RotateNoneFlipY);
								if (Texture_Mode == TextureMode.Mirror | Texture_Mode == TextureMode.FlipY_Mirror)
									Img = Mirror_Image(Img);
								Img.Save(File_Name);
							}
						}
						Index += 1;
					}
				} catch (Exception ex) {
					Debug.WriteLine("Texture Exporter -> Erro ao exportar textura: " + Path.GetFileName(File.FullName));
				}
				Update_Progress(ProgressTextures, Convert.ToSingle((Total_Index / Input_Files.Count()) * 100), "Exporting " + Path.GetFileName(File.FullName) + "...");
				Total_Index += 1;
			}

			Update_Progress(ProgressTextures, 0, null);
			Update_Button_Text(BtnTextureExportAllFF, "Export all from folder");

			Set_Enabled(BtnTextureOpen, true);
			Set_Enabled(BtnTextureSave, true);
			Set_Enabled(BtnTextureInsert, true);
			Set_Enabled(BtnTextureInsertAll, true);
		}
		private void BtnTextureMode_Click(object sender, EventArgs e)
		{
			switch (Texture_Mode) {
				case TextureMode.Original:
					Texture_Mode = TextureMode.FlipY;
					BtnTextureMode.Text = "Flip-Y";
					Ohana3DS.My.MySettings.Default.TextureFlipMirror = 1;
					break;
				case TextureMode.FlipY:
					Texture_Mode = TextureMode.Mirror;
					BtnTextureMode.Text = "Mirror-X";
					Ohana3DS.My.MySettings.Default.TextureFlipMirror = 2;
					break;
				case TextureMode.Mirror:
					Texture_Mode = TextureMode.FlipY_Mirror;
					BtnTextureMode.Text = "Flip/Mirror";
					Ohana3DS.My.MySettings.Default.TextureFlipMirror = 3;
					break;
				case TextureMode.FlipY_Mirror:
					Texture_Mode = TextureMode.Original;
					BtnTextureMode.Text = "Original";
					Ohana3DS.My.MySettings.Default.TextureFlipMirror = 0;
					break;
			}
			Ohana3DS.My.MySettings.Default.Save();
		}
		private void BtnTextureInsert_Click(object sender, EventArgs e)
		{
			if (Texture_Thread != null) {
				if (Texture_Thread.IsAlive) {
					Texture_Thread.Abort();
					BtnTextureInsert.Text = "Import";
					ProgressTextures.Text = null;
					ProgressTextures.Percentage = 0;
					ProgressTextures.Refresh();

					BtnTextureOpen.Enabled = true;
					BtnTextureSave.Enabled = true;
					BtnTextureExportAllFF.Enabled = true;
					BtnTextureInsertAll.Enabled = true;

					return;
				}
			}

			if ((Common.MyOhana.Current_Texture != null | Common.MyOhana.BCH_Have_Textures) & LstTextures.SelectedIndex > -1) {
				OpenFileDialog OpenDlg = new OpenFileDialog();
				OpenDlg.Title = "Select the Texture to insert";
				OpenDlg.Filter = "PNG|*.png";
				if (OpenDlg.ShowDialog() == DialogResult.OK) {
					if (File.Exists(OpenDlg.FileName)) {
						Thread Trd = new Thread(() => Insert_Texture(OpenDlg.FileName, LstTextures.SelectedIndex));
						Trd.Start();

						BtnTextureInsert.Text = "Cancel";
						BtnTextureOpen.Enabled = false;
						BtnTextureSave.Enabled = false;
						BtnTextureExportAllFF.Enabled = false;
						BtnTextureInsertAll.Enabled = false;
					}
				}
			}
		}
		private void Insert_Texture(string File_Name, int Index)
		{
			Update_Progress(ProgressTextures, 0, "Inserting data...");

			Texture_Thread = new Thread(() => Common.MyOhana.Insert_Texture(File_Name, Index));
			Texture_Thread.Start();

			float Old_Percentage = 0;
			while (Texture_Thread.IsAlive) {
				if (Common.MyOhana.Texture_Insertion_Percentage != Old_Percentage) {
					Update_Progress(ProgressTextures, Common.MyOhana.Texture_Insertion_Percentage, "Inserting data...");
					Old_Percentage = Common.MyOhana.Texture_Insertion_Percentage;
				}
			}

			Update_Progress(ProgressTextures, 0, null);
			Update_Button_Text(BtnTextureInsert, "Import");

			if (LstTextures.SelectedIndex == Index)
				Set_Image(ImgTexture, Common.MyOhana.Model_Texture[Index].Image);
			Set_Enabled(BtnTextureOpen, true);
			Set_Enabled(BtnTextureSave, true);
			Set_Enabled(BtnTextureExportAllFF, true);
			Set_Enabled(BtnTextureInsertAll, true);
		}
		private void BtnTextureInsertAll_Click(object sender, EventArgs e)
		{
			if (Texture_Thread != null) {
				if (Texture_Thread.IsAlive) {
					Texture_Thread.Abort();
					BtnTextureInsertAll.Text = "Import all";
					ProgressTextures.Text = null;
					ProgressTextures.Percentage = 0;
					ProgressTextures.Refresh();

					BtnTextureOpen.Enabled = true;
					BtnTextureSave.Enabled = true;
					BtnTextureExportAllFF.Enabled = true;
					BtnTextureInsert.Enabled = true;

					return;
				}
			}

			if (Common.MyOhana.Current_Texture != null | Common.MyOhana.BCH_Have_Textures) {
				FolderBrowserDialog FolderDlg = new FolderBrowserDialog();
				if (FolderDlg.ShowDialog() == DialogResult.OK) {
					if (Directory.Exists(FolderDlg.SelectedPath)) {
						Thread Trd = new Thread(() => Insert_Textures_From_Folder(FolderDlg.SelectedPath));
						Trd.Start();

						BtnTextureInsertAll.Text = "Cancel";
						BtnTextureOpen.Enabled = false;
						BtnTextureSave.Enabled = false;
						BtnTextureExportAllFF.Enabled = false;
						BtnTextureInsert.Enabled = false;
					}
				}
			}
		}
		private void Insert_Textures_From_Folder(string Folder)
		{
			bool Not_Found = false;
			string Not_Found_Files = null;
			int Index = 0;
			foreach (Ohana.OhanaTexture Texture in Common.MyOhana.Model_Texture) {
				string File_Name = Path.Combine(Folder, Texture.Name + ".png");
				if (File.Exists(File_Name)) {
					Update_Progress(ProgressTextures, Convert.ToSingle((Index / Common.MyOhana.Model_Texture.Count) * 100), "Inserting " + Texture.Name + "...");
					Common.MyOhana.Insert_Texture(File_Name, Index, false);
					if (LstTextures.SelectedIndex == Index)
						Set_Image(ImgTexture, Common.MyOhana.Model_Texture[Index].Image);
				} else {
					Not_Found = true;
					if (Index < 15)
						Not_Found_Files += Texture.Name + ".png" + Environment.NewLine;
				}
				Index += 1;
			}
			if (Common.MyOhana.Model_Texture.Count > 15)
				Not_Found_Files += "[Truncated]";

			Update_Progress(ProgressTextures, 0, null);
			Update_Button_Text(BtnTextureInsertAll, "Import all");

			Set_Enabled(BtnTextureOpen, true);
			Set_Enabled(BtnTextureSave, true);
			Set_Enabled(BtnTextureExportAllFF, true);
			Set_Enabled(BtnTextureInsert, true);

			if (Not_Found)
				MessageBox.Show("The following files couldn't be found:" + Environment.NewLine + Not_Found_Files, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		private void BtnTextureSave_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Current_Texture != null) {
				File.Delete(Common.MyOhana.Current_Texture);
				File.Copy(Common.MyOhana.Temp_Texture_File, Common.MyOhana.Current_Texture);
			} else if (Common.MyOhana.BCH_Have_Textures) {
				File.Delete(Common.MyOhana.Current_Model);
				File.Copy(Common.MyOhana.Temp_Model_File, Common.MyOhana.Current_Model);
			}
		}

		private Bitmap Mirror_Image(Bitmap Img)
		{
			BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			byte[] Data = new byte[(ImgData.Height * ImgData.Stride)];
			Marshal.Copy(ImgData.Scan0, Data, 0, Data.Length);

			byte[] Out = new byte[((Img.Width * 2) * Img.Height * 4)];
			for (int Y = 0; Y <= Img.Height - 1; Y++) {
				for (int X = 0; X <= Img.Width - 1; X++) {
					int Offset = (X + (Y * Img.Width)) * 4;
					int Offset_2 = (X + (Y * (Img.Width * 2))) * 4;
					int Offset_3 = ((Img.Width + (Img.Width - X - 1)) + (Y * (Img.Width * 2))) * 4;
					Buffer.BlockCopy(Data, Offset, Out, Offset_2, 4);
					Buffer.BlockCopy(Data, Offset, Out, Offset_3, 4);
				}
			}

			Bitmap Output = new Bitmap(Img.Width * 2, Img.Height, PixelFormat.Format32bppArgb);
			BitmapData OutData = Output.LockBits(new Rectangle(0, 0, Output.Width, Output.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(Out, 0, OutData.Scan0, Out.Length);
			Output.UnlockBits(OutData);

			return Output;
		}
		#endregion

		#region "Text"
		private void BtnTextOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open Text file";
			if (OpenDlg.ShowDialog() == DialogResult.OK) {
				if (File.Exists(OpenDlg.FileName)) {
					Open_Text(OpenDlg.FileName);
				}
			}
		}
		private void Open_Text(string File_Name)
		{
			Current_Opened_Text = File_Name;
			try {
				MyMinko.Extract_Strings(File_Name);
				Update_Texts();
				Enable_Text_Buttons();
			} catch {
				Disable_Text_Buttons();
				MessageBox.Show("Sorry, something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void Enable_Text_Buttons()
		{
			BtnTextExport.Enabled = true;
			BtnTextImport.Enabled = true;
			BtnTextSave.Enabled = true;
		}
		private void Disable_Text_Buttons()
		{
			BtnTextExport.Enabled = false;
			BtnTextImport.Enabled = false;
			BtnTextSave.Enabled = false;
		}
		private void BtnTextExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog SaveDlg = new SaveFileDialog();
			SaveDlg.Title = "Save texts";
			SaveDlg.Filter = "XML|*.xml";
			if (SaveDlg.ShowDialog() == DialogResult.OK) {
				StringBuilder Out = new StringBuilder();
				Out.AppendLine("<!--OhanaXY Pokémon Text Rip :P-->");
				Out.AppendLine("<textfile>");
				foreach (string Line in MyMinko.Strings) {
					Out.AppendLine("    <text>");
					string[] Temp = Line.Split(Convert.ToChar(0xa));
					foreach (string Temp_Line in Temp) {
						Out.AppendLine("        " + Temp_Line);
					}
					Out.AppendLine("    </text>");
				}
				Out.AppendLine("</textfile>");
				File.WriteAllText(SaveDlg.FileName, Out.ToString());
			}
		}
		private void BtnTextImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open texts";
			OpenDlg.Filter = "XML|*.xml";
			if (OpenDlg.ShowDialog() == DialogResult.OK) {
				MatchCollection TextData = Regex.Matches(File.ReadAllText(OpenDlg.FileName), "<text>(.+?)</text>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				string[] Strings = new string[TextData.Count];
				int Index = 0;
				foreach (Match Text in TextData) {
					string Value = Text.Groups[1].Value.Trim();

					string[] Temp = Value.Split(Environment.NewLine[0]);
					Strings[Index] = null;
					int i = 1;
					foreach (string Temp_Line in Temp) {
						Strings[Index] += Temp_Line.Trim();
						if (i < Temp.Count())
							Strings[Index] += Convert.ToChar(0xa);
						i += 1;
					}
					Index += 1;
				}

				Current_Text_Temp = Path.GetTempFileName();
				MyMinko.Insert_Strings(Strings, Current_Text_Temp);

				MyMinko.Extract_Strings(Current_Text_Temp);
				Update_Texts();
			}
		}
		private void BtnTextSave_Click(object sender, EventArgs e)
		{
			if (Current_Text_Temp != null & Current_Opened_Text != null) {
				File.Delete(Current_Opened_Text);
				File.Copy(Current_Text_Temp, Current_Opened_Text);
			}
		}

		private void Update_Texts()
		{
			LstStrings.Clear();
			foreach (string Line in MyMinko.Strings) {
				string[] Temp = Line.Split(Convert.ToChar(0xa));
				if (Temp.Count() > 1)
					Temp[0] += " [...]";
				LstStrings.AddItem(Temp[0]);
			}
			LstStrings.Refresh();
		}
		#endregion

		#region "GARC"
		private void BtnOpenGARC_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open container";
			OpenDlg.Filter = "GARC/Container file|*.*";
			if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				Open_GARC(OpenDlg.FileName);
			}
		}
		private void Open_GARC(string File_Name)
		{
			if (Common.MyNako.Load(File_Name)) {
				Update_GARC_List();
				Enable_GARC_Buttons();
			} else {
				Disable_GARC_Buttons();
				LstFiles.Clear();
				MessageBox.Show("This is not a container from Pokémon!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void Enable_GARC_Buttons()
		{
			BtnGARCExtract.Enabled = true;
			BtnGARCExtractAll.Enabled = true;
			BtnGARCInsert.Enabled = true;
			BtnGARCSave.Enabled = true;
		}
		private void Disable_GARC_Buttons()
		{
			BtnGARCExtract.Enabled = false;
			BtnGARCExtractAll.Enabled = false;
			BtnGARCInsert.Enabled = false;
			BtnGARCSave.Enabled = false;
		}
		private void Update_GARC_List()
		{
			LstFiles.Clear();
			MyListview.ListItem Header = default(MyListview.ListItem);
			Header.Text = new ListText[3];
			Header.Text[0].Text = "File";
			Header.Text[1].Left = 400;
			Header.Text[1].Text = "Compressed size";
			Header.Text[2].Left = 500;
			Header.Text[2].Text = "Uncompressed size";
			Header.Header = true;
			LstFiles.AddItem(Header);

			foreach (Nako.GARC_File File in Common.MyNako.Files) {
				MyListview.ListItem Item = default(MyListview.ListItem);
				Item.Text = new ListText[3];
				var _with2 = File;
				Item.Text[0].Text = _with2.Name;
				Item.Text[1].Left = 400;
				if (_with2.Compressed) {
					Item.Text[1].Text = Format_Size(_with2.Length);
				} else {
					Item.Text[1].Text = "---";
				}
				Item.Text[2].Left = 500;
				Item.Text[2].Text = Format_Size(_with2.Uncompressed_Length);
				LstFiles.AddItem(Item);
			}
			LstFiles.Refresh();
		}
		private void BtnGARCExtract_Click(object sender, EventArgs e)
		{
			if (Common.MyNako.Files != null) {
				if (LstFiles.SelectedIndex > -1) {
					SaveFileDialog SaveDlg = new SaveFileDialog();
					SaveDlg.Title = "Extract file";
					SaveDlg.FileName = Common.MyNako.Files[LstFiles.SelectedIndex].Name;
					if (SaveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
						FileStream InFile = new FileStream(Common.MyNako.Current_File, FileMode.Open);
						Common.MyNako.Extract(InFile, SaveDlg.FileName, LstFiles.SelectedIndex);
						InFile.Close();
					}
				}
			}
		}
		private void BtnGARCExtractAll_Click(object sender, EventArgs e)
		{
			if (Common.MyNako.Files != null) {
				if (GARC_Thread != null) {
					if (GARC_Thread.IsAlive) {
						GARC_Thread.Abort();
						BtnGARCExtractAll.Text = "Extract all";
						ProgressGARC.Text = null;
						ProgressGARC.Percentage = 0;
						ProgressGARC.Refresh();

						BtnGARCOpen.Enabled = true;
						BtnGARCInsert.Enabled = true;
						BtnGARCExtract.Enabled = true;
						BtnGARCSave.Enabled = true;

						return;
					}
				}

				FolderBrowserDialog OutputDlg = new FolderBrowserDialog();
				if (OutputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					GARC_Thread = new Thread(() => GARC_ExtractAll(OutputDlg.SelectedPath));
					GARC_Thread.Start();

					BtnGARCExtractAll.Text = "Cancel";
					BtnGARCOpen.Enabled = false;
					BtnGARCInsert.Enabled = false;
					BtnGARCExtract.Enabled = false;
					BtnGARCSave.Enabled = false;
				}
			}
		}
		private void GARC_ExtractAll(string OutFolder)
		{
			FileStream InFile = new FileStream(Common.MyNako.Current_File, FileMode.Open);
			for (int Index = 0; Index <= Common.MyNako.Files.Length - 1; Index++) {
				Common.MyNako.Extract(InFile, Path.Combine(OutFolder, Common.MyNako.Files[Index].Name), Index);
				if (Common.MyNako.Files.Length > 1)
					Update_Progress(ProgressGARC, Convert.ToSingle((Index / (Common.MyNako.Files.Length - 1)) * 100), "Extracting " + Common.MyNako.Files[Index].Name + "...");
			}
			InFile.Close();

			Update_Progress(ProgressGARC, 0, null);
			Update_Button_Text(BtnGARCExtractAll, "Extract all");

			Set_Enabled(BtnGARCOpen, true);
			Set_Enabled(BtnGARCInsert, true);
			Set_Enabled(BtnGARCExtract, true);
			Set_Enabled(BtnGARCSave, true);
		}
		private void BtnGARCInsert_Click(object sender, EventArgs e)
		{
			if (LstFiles.SelectedIndex > -1) {
				OpenFileDialog OpenDlg = new OpenFileDialog();
				if (OpenDlg.ShowDialog() == DialogResult.OK) {
					if (File.Exists(OpenDlg.FileName)) {
						Nako.Inserted_File Item = new Nako.Inserted_File();
						Item.Index = LstFiles.SelectedIndex;
						Item.File_Name = OpenDlg.FileName;
						Common.MyNako.Inserted_Files.Add(Item);

						FileStream Temp = new FileStream(OpenDlg.FileName, FileMode.Open);
						string Magic = Encoding.ASCII.GetString(BitConverter.GetBytes(Common.Read32(Temp, 0)));

						MyListview.ListItem Header = default(MyListview.ListItem);
						Header.Text = new ListText[3];
						Header.Text[0].Text = "file_" + LstFiles.SelectedIndex + Common.MyNako.Guess_Format(Magic);
						Header.Text[1].Left = 400;
						Header.Text[1].Text = "???";
						Header.Text[2].Left = 500;
						Header.Text[2].Text = Format_Size(Convert.ToInt32(Temp.Length));
						LstFiles.ChangeItem(LstFiles.SelectedIndex, Header);

						Temp.Close();
					}
				}
			}
		}
		private void BtnGARCSave_Click(object sender, EventArgs e)
		{
			if (GARC_Thread != null) {
				if (GARC_Thread.IsAlive) {
					GARC_Thread.Abort();
					BtnGARCSave.Text = "Save";
					ProgressGARC.Text = null;
					ProgressGARC.Percentage = 0;
					ProgressGARC.Refresh();

					BtnGARCOpen.Enabled = true;
					BtnGARCInsert.Enabled = true;
					BtnGARCExtract.Enabled = true;
					BtnGARCExtractAll.Enabled = true;

					return;
				}
			}

			if (Common.MyNako.Files.Count() > 0) {
				Thread Trd = new Thread(GARC_Save);
				Trd.Start();

				BtnGARCSave.Text = "Cancel";
				BtnGARCOpen.Enabled = false;
				BtnGARCInsert.Enabled = false;
				BtnGARCExtract.Enabled = false;
				BtnGARCExtractAll.Enabled = false;
			}
		}
		private void GARC_Save()
		{
			Update_Progress(ProgressGARC, 0, "Please wait, rebuilding GARC...");
			Common.MyNako.Compression_Percentage = 0;

			GARC_Thread = new Thread(Common.MyNako.Insert);
			GARC_Thread.Start();

			float Old_Percentage = 0;
			while (GARC_Thread.IsAlive) {
				if (Common.MyNako.Compression_Percentage != Old_Percentage) {
					Update_Progress(ProgressGARC, Common.MyNako.Compression_Percentage, "Compressing data...");
					Old_Percentage = Common.MyNako.Compression_Percentage;
				}
			}
			Update_Progress(ProgressGARC, 0, null);
			Update_Button_Text(BtnGARCSave, "Save");

			Set_Enabled(BtnGARCOpen, true);
			Set_Enabled(BtnGARCInsert, true);
			Set_Enabled(BtnGARCExtract, true);
			Set_Enabled(BtnGARCExtractAll, true);
		}
		private void BtnGARCCompression_Click(object sender, EventArgs e)
		{
			Common.MyNako.Fast_Compression = !Common.MyNako.Fast_Compression;
			if (Common.MyNako.Fast_Compression) {
				BtnGARCCompression.Text = "Fast compression";
			} else {
				BtnGARCCompression.Text = "Optimal compression";
			}

			Ohana3DS.My.MySettings.Default.FastCompression = Common.MyNako.Fast_Compression;
			Ohana3DS.My.MySettings.Default.Save();
		}
		#endregion

		#region "ROM"
		private void BtnROMOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open ROM";
			OpenDlg.Filter = "3DS Roms|*.3ds;*.3dz";
			if (OpenDlg.ShowDialog() == DialogResult.OK) {
				if (File.Exists(OpenDlg.FileName)) {
					Current_ROM = OpenDlg.FileName;
					FileStream ROM = new FileStream(OpenDlg.FileName, FileMode.Open);
					Parse_Header(ROM);
					ROM.Close();
					if (Current_XORPad != null)
						BtnROMDecrypt.Enabled = true;
				}
			}
		}
		private void BtnROMOpenXorPad_Click(object sender, EventArgs e)
		{
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Title = "Open XORPad";
			OpenDlg.Filter = "XORPad|*.xorpad";
			if (OpenDlg.ShowDialog() == DialogResult.OK) {
				if (File.Exists(OpenDlg.FileName)) {
					Current_XORPad = OpenDlg.FileName;
					if (Current_ROM != null)
						BtnROMDecrypt.Enabled = true;
				}
			}
		}
		private void BtnROMDecrypt_Click(object sender, EventArgs e)
		{
			if (Current_ROM != null & Current_XORPad != null) {
				FolderBrowserDialog FolderDlg = new FolderBrowserDialog();
				if (FolderDlg.ShowDialog() == DialogResult.OK) {
					Thread Trd = new Thread(() => Decrypt_Data(Current_ROM, Current_XORPad, FolderDlg.SelectedPath + "\\out\\", Convert.ToInt32(NCCH_Container[0].RomFS_Offset), Convert.ToInt32(NCCH_Container[0].RomFS_Size)));
					Trd.Start();
				}
			}
		}

		private void Parse_Header(FileStream InData)
		{
			LstROMLog.Clear();

			string Magic = Get_Data(InData, 0x100, 4, false);
			if (Magic != "NCSD") {
				Add_Log("[!] NCSD container not found!", Color.Red);
				return;
			}
			Add_Log("Parsing NCSD header...", Color.White);

			int Offset = 0x120;
			for (int Container = 0; Container <= 7; Container++) {
				var _with3 = NCCH_Container[Container];
				_with3.Offset = Common.Read32(InData, Offset) * 0x200;
				_with3.Length = Common.Read32(InData, Offset + 4) * 0x200;

				if (_with3.Length > 0) {
					int Base_Offset = _with3.Offset + 0x100;

					if (Get_Data(InData, Base_Offset, 4, false) != "NCCH") {
						Add_Log("[!] Invalid NCCH header! The ROM is corrupted!", Color.Red);
						return;
					}
					Add_Log("NCCH #" + Container + " (" + Get_Data(InData, Base_Offset + 0x50, 0x10, false) + ")", Color.Cyan);

					Add_Log("-- Signature (256 bytes in Hexadecimal/SHA-256):", Color.Gainsboro);
					for (int i = 0; i <= 7; i++) {
						Add_Log(Get_Data(InData, _with3.Offset + i * 32, 32), Color.Gainsboro);
					}

					Add_Log("-- Content Size: " + Format_Size(Common.Read32(InData, Base_Offset + 0x4) * 0x200), Color.White);
					InData.Seek(Base_Offset + 0x8, SeekOrigin.Begin);
					 // ERROR: Not supported in C#: ReDimStatement

					InData.Read(_with3.Partition_ID, 0, 8);
					Add_Log("-- Partition ID: " + "0x" + Get_Data(InData, Base_Offset + 0x8, 8), Color.White);
					Add_Log("-- Maker Code: " + "0x" + Get_Data(InData, Base_Offset + 0x10, 2), Color.White);
					Add_Log("-- Version: " + "0x" + Get_Data(InData, Base_Offset + 0x12, 2), Color.White);
					Add_Log("-- Program ID: " + "0x" + Get_Data(InData, Base_Offset + 0x18, 8), Color.White);
					_with3.Product_Code = Get_Data(InData, Base_Offset + 0x50, 0x10, false);
					Add_Log("-- ExHeader Hash (32 bytes in Hexadecimal/SHA-256):", Color.Gainsboro);
					Add_Log(Get_Data(InData, Base_Offset + 0x60, 0x20), Color.Gainsboro);
					_with3.ExHeader_Size = Common.Read32(InData, Base_Offset + 0x80) * 0x200;
					Add_Log("-- ExHeader Size: " + Format_Size(Convert.ToInt32(_with3.ExHeader_Size)), Color.White);

					string Content_Type = null;
					InData.Seek(Base_Offset + 0x88 + 0x5, SeekOrigin.Begin);
					byte Temp = (byte)InData.ReadByte();
					if ((Temp & 0x1) > 0x1)
						Content_Type += "Data";
					if ((Temp & 0x2) == 0x2)
						if (Content_Type != null)
							Content_Type += "/Executable";
						else
							Content_Type += "Executable";
					if ((Temp & 0x4) == 0x4)
						if (Content_Type != null)
							Content_Type += "/System Update";
						else
							Content_Type += "System Update";
					if ((Temp & 0x8) == 0x8)
						if (Content_Type != null)
							Content_Type += "/Manual";
						else
							Content_Type += "Manual";
					if ((Temp & 0x10) == 0x10)
						if (Content_Type != null)
							Content_Type += "/Trial";
						else
							Content_Type += "Trial";
					Add_Log("-- Flags: " + "0x" + Get_Data(InData, Base_Offset + 0x88, 8) + Content_Type != null ? " (" + Content_Type + ")" : null, Color.White);

					long Plain_Region_Offset = Common.Read32(InData, Base_Offset + 0x90) * 0x200;
					if (Plain_Region_Offset > 0)
						Plain_Region_Offset += _with3.Offset;
					Add_Log("-- Plain Region Offset: " + "0x" + Conversion.Hex(Plain_Region_Offset).PadLeft(8, '0'), Color.White);
					Add_Log("-- Plain Region Size: " + Format_Size(Common.Read32(InData, Base_Offset + 0x94) * 0x200), Color.White);
					long Logo_Region_Offset = Common.Read32(InData, Base_Offset + 0x98) * 0x200;
					if (Logo_Region_Offset > 0)
						Logo_Region_Offset += _with3.Offset;
					Add_Log("-- Logo Region Offset: " + "0x" + Conversion.Hex(Logo_Region_Offset).PadLeft(8, '0'), Color.White);
					Add_Log("-- Logo Region Size: " + Format_Size(Common.Read32(InData, Base_Offset + 0x9c) * 0x200), Color.White);

					_with3.ExeFS_Offset = Common.Read32(InData, Base_Offset + 0xa0) * 0x200;
					if (_with3.ExeFS_Offset > 0)
						_with3.ExeFS_Offset += _with3.Offset;
					_with3.ExeFS_Size = Common.Read32(InData, Base_Offset + 0xa4) * 0x200;
					_with3.RomFS_Offset = Common.Read32(InData, Base_Offset + 0xb0) * 0x200;
					if (_with3.RomFS_Offset > 0)
						_with3.RomFS_Offset += _with3.Offset;
					_with3.RomFS_Size = Common.Read32(InData, Base_Offset + 0xb4) * 0x200;

					Add_Log("-- Executable File System Offset: " + "0x" + Conversion.Hex(_with3.ExeFS_Offset).PadLeft(8, '0'), Color.White);
					Add_Log("-- Executable File System Size: " + Format_Size(Convert.ToInt32(_with3.ExeFS_Size)), Color.White);
					Add_Log("-- Executable File System Hash Region Size: " + Format_Size(Common.Read32(InData, Base_Offset + 0xa8) * 0x200), Color.White);
					Add_Log("-- ROM File System Offset: " + "0x" + Conversion.Hex(_with3.RomFS_Offset).PadLeft(8, '0'), Color.White);
					Add_Log("-- ROM File System Size: " + Format_Size(Convert.ToInt32(_with3.RomFS_Size)), Color.White);
					Add_Log("-- ROM File System Hash Region Size: " + Format_Size(Common.Read32(InData, Base_Offset + 0xb8) * 0x200), Color.White);

					Add_Log("-- Executable File System Hash (32 bytes in Hexadecimal/SHA-256):", Color.Gainsboro);
					Add_Log(Get_Data(InData, Base_Offset + 0xc0, 0x20), Color.Gainsboro);
					Add_Log("-- ROM File System Hash (32 bytes in Hexadecimal/SHA-256):", Color.Gainsboro);
					Add_Log(Get_Data(InData, Base_Offset + 0xe0, 0x20), Color.Gainsboro);
				}

				Offset += 8;
			}

			Add_Log("Done! The ROM is ready to be decrypted!", Color.Green, true);
		}
		private void Add_Log(string Text, Color Color, bool Refresh = false)
		{
			MyListview.ListItem Item = default(MyListview.ListItem);
			Item.Text = new ListText[1];
			Item.Text[0].Text = Text;
			Item.Text[0].ForeColor = Color;
			Add_List_Item(LstROMLog, Item, Refresh);
		}
		public string Get_Data(FileStream InData, int Start_Offset, int Count, bool HexFmt = true)
		{
			byte[] Data = new byte[Count];
			InData.Position = Start_Offset;
			InData.Read(Data, 0, Count);
			string Out = null;
			for (int i = 0; i <= Count - 1; i++) {
				if (HexFmt) {
					Out += Conversion.Hex(Data[i]).PadLeft(2, '0');
				} else {
					if (Data[i] > 0)
						Out += Strings.Chr(Data[i]);
				}
			}
			return Out;
		}

		private void Decrypt_Data(string In_File, string In_XOR, string Out_Path, int InOffset, int InSize)
		{
			Add_Log("Decrypting Rom File System (it may take some time)...", Color.White, true);

			FileStream InFile = new FileStream(In_File, FileMode.Open);
			FileStream InXOR = new FileStream(In_XOR, FileMode.Open);
			string Output_File = Path.Combine(Path.GetDirectoryName(In_File), Path.GetFileNameWithoutExtension(In_File) + ".dec");
			FileStream OutFile = new FileStream(Output_File, FileMode.Create);
			InFile.Seek(InOffset, SeekOrigin.Begin);
			for (int Offset = 0; Offset <= InSize - 1; Offset += 16384) {
				int BuffLen = Convert.ToInt32(InSize - Offset >= 16384 ? 16384 : InSize - Offset);
				byte[] Buffer = new byte[BuffLen];
				byte[] XorBuff = new byte[BuffLen];
				InFile.Read(Buffer, 0, BuffLen);
				InXOR.Read(XorBuff, 0, BuffLen);
				for (int i = 0; i <= Buffer.Length - 1; i++) {
					Buffer[i] = (byte)(Buffer[i] ^ XorBuff[i]);
				}
				OutFile.Write(Buffer, 0, BuffLen);
			}
			OutFile.Close();
			InFile.Close();
			InXOR.Close();

			Extract_RomFS(Output_File, Out_Path);
		}

		private void Extract_RomFS(string InFile, string OutFolder)
		{
			FileStream RomFS = new FileStream(InFile, FileMode.Open);
			BinaryReader Reader = new BinaryReader(RomFS);
			int Info_Offset = 0x1000;
			RomFS.Seek(0x1000, SeekOrigin.Begin);
			int Header_Size = Reader.ReadInt32();
			Info_Header[] Sections = new Info_Header[4];
			for (int Section = 0; Section <= 3; Section++) {
				Sections[Section].Offset = Reader.ReadInt32();
				Sections[Section].Length = Reader.ReadInt32();
			}
			int Data_Offset = 0x1000 + Reader.ReadInt32();

			int Directory_Info_Offset = 0x1000 + Sections[1].Offset;
			int Directory_Info_Length = Sections[1].Length;
			int File_Info_Offset = 0x1000 + Sections[3].Offset;
			int File_Info_Length = Sections[3].Length;

			List<Rom_Directory> Directories = new List<Rom_Directory>();
			Parse_Directory(RomFS, Directory_Info_Offset, Directory_Info_Offset, File_Info_Offset, Data_Offset, null, OutFolder);
			RomFS.Close();
		}
		private void Parse_Directory(FileStream RomFS, int Offset, int Directory_Info_Offset, int File_Info_Offset, int Data_Offset, string Path, string Out_Path)
		{
			Rom_Directory Dir = default(Rom_Directory);

			var _with4 = Dir;
			_with4.Parent_Offset = Common.Read32(RomFS, Offset);
			_with4.Sibling_Offset = Common.Read32(RomFS, Offset + 4);
			_with4.Children_Offset = Common.Read32(RomFS, Offset + 8);
			_with4.File_Offset = Common.Read32(RomFS, Offset + 12);
			_with4.Unknow = Common.Read32(RomFS, Offset + 16);
			_with4.Name = null;
			int Temp = Offset + 24;
			int Name_Length = Common.Read32(RomFS, Offset + 20);
			for (int Name_Offset = Temp; Name_Offset <= Temp + Name_Length - 1; Name_Offset += 2) {
				_with4.Name += Strings.ChrW(Common.Read16(RomFS, Name_Offset));
			}
			string Current_Path = Path + Path != null ? "\\" : null + _with4.Name;

			if ((uint)_with4.File_Offset != 0xffffffff) {
				Parse_File(RomFS, File_Info_Offset + _with4.File_Offset, File_Info_Offset, Data_Offset, Current_Path, Out_Path);
			}
			if ((uint)_with4.Children_Offset != 0xffffffff) {
				Parse_Directory(RomFS, Directory_Info_Offset + _with4.Children_Offset, Directory_Info_Offset, File_Info_Offset, Data_Offset, Current_Path, Out_Path);
			}
			if ((uint)_with4.Sibling_Offset != 0xffffffff) {
				Parse_Directory(RomFS, Directory_Info_Offset + _with4.Sibling_Offset, Directory_Info_Offset, File_Info_Offset, Data_Offset, Path, Out_Path);
			}
		}
		private void Parse_File(FileStream RomFS, int Offset, int File_Info_Offset, int Data_Offset, string Path, string Out_Path)
		{
			Rom_File File = default(Rom_File);

			var _with5 = File;
			_with5.Parent_Offset = Common.Read32(RomFS, Offset);
			_with5.Sibling_Offset = Common.Read32(RomFS, Offset + 4);
			_with5.Data_Offset = Common.Read64(RomFS, Offset + 8);
			_with5.Data_Length = Common.Read64(RomFS, Offset + 16);
			_with5.Unknow = Common.Read32(RomFS, Offset + 24);
			_with5.Name = null;
			int Temp = Offset + 32;
			int Name_Length = Common.Read32(RomFS, Offset + 28);
			for (int Name_Offset = Temp; Name_Offset <= Temp + Name_Length - 1; Name_Offset += 2) {
				_with5.Name += Strings.ChrW(Common.Read16(RomFS, Name_Offset));
			}
			string File_Name = Path + "\\" + _with5.Name;
			Add_Log("Extracting " + Path + "\\" + _with5.Name + "...", Color.White, true);

			if (!Directory.Exists(Out_Path + "\\" + Path))
				Directory.CreateDirectory(Out_Path + "\\" + Path);
			FileStream Out_File = new FileStream(Out_Path + "\\" + File_Name, FileMode.Create);
			int File_Offset = Convert.ToInt32((ulong)Data_Offset + _with5.Data_Offset);
			RomFS.Seek(File_Offset, SeekOrigin.Begin);
			for (int Write_Offset = File_Offset; Write_Offset <= Convert.ToInt32((ulong)File_Offset + _with5.Data_Length - 1); Write_Offset += 16) {
				int BuffLen = Convert.ToInt32(_with5.Data_Length - (ulong)(Write_Offset - File_Offset) >= 16 ? 16 : _with5.Data_Length - (ulong)(Write_Offset - File_Offset));
				byte[] Buffer = new byte[BuffLen + 1];
				RomFS.Read(Buffer, 0, BuffLen);
				Out_File.Write(Buffer, 0, BuffLen);
			}
			Out_File.Close();

			if ((uint)_with5.Sibling_Offset != 0xffffffff) {
				Parse_File(RomFS, File_Info_Offset + _with5.Sibling_Offset, File_Info_Offset, Data_Offset, Path, Out_Path);
			}
		}
		#endregion

		#region "Search"
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			if (TxtSearch.Text != null) {
				if (Search_Thread != null) {
					if (Search_Thread.IsAlive) {
						Search_Thread.Abort();
						BtnSearch.Text = "Search";
						ProgressSearch.Text = null;
						ProgressSearch.Percentage = 0;
						ProgressSearch.Refresh();

						return;
					}
				}

				FolderBrowserDialog InputDlg = new FolderBrowserDialog();
				if (InputDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					LstMatches.Clear();
					MyListview.ListItem Header = default(MyListview.ListItem);
					Header.Text = new ListText[2];
					Header.Text[0].Text = "Name";
					Header.Text[1].Left = 400;
					Header.Text[1].Text = "Offset";
					Header.Header = true;
					LstMatches.AddItem(Header);
					LstMatches.Refresh();

					Search_Thread = new Thread(() => File_String_Search(InputDlg.SelectedPath, TxtSearch.Text));
					Search_Thread.Start();

					BtnSearch.Text = "Cancel";
				}
			}
		}
		private void File_String_Search(string InFolder, string Text)
		{
			int Index = 0;
			FileInfo[] Input_Files = new DirectoryInfo(InFolder).GetFiles();
			byte[] Search_Term = Encoding.UTF8.GetBytes(Text);
			foreach (FileInfo CurrFile in Input_Files) {
				Update_Progress(ProgressSearch, Convert.ToSingle((Index / Input_Files.Count()) * 100), "Searching on file " + CurrFile.Name + "...");
				Index += 1;

				byte[] Data = File.ReadAllBytes(CurrFile.FullName);
				bool Found = false;
				for (int Offset = 0; Offset <= Data.Length - Search_Term.Length; Offset++) {
					for (int Offset_2 = 0; Offset_2 <= Search_Term.Length - 1; Offset_2++) {
						if (Data[Offset + Offset_2] != Search_Term[Offset_2]) {
							break; // TODO: might not be correct. Was : Exit For
						} else if (Offset_2 == Search_Term.Length - 1) {
							MyListview.ListItem Item = default(MyListview.ListItem);
							Item.Text = new ListText[2];
							Item.Text[0].Text = CurrFile.Name;
							Item.Text[1].Left = 400;
							Item.Text[1].Text = "0x" + Conversion.Hex(Offset);
							Add_List_Item(LstMatches, Item, true);
							Found = true;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					if (Found)
						break; // TODO: might not be correct. Was : Exit For
				}
			}

			Update_Progress(ProgressSearch, 0, null);
			Update_Button_Text(BtnSearch, "Search");
		}
		#endregion

		#endregion

		private void BtnModelMapEditor_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Magic.Substring(0, 2) == "GR") {
				My.MyProject.Forms.FrmMapProp.Show();
				My.MyProject.Forms.FrmMapProp.makeMapIMG(MapProps());
			}
		}

		private byte[] MapProps()
		{
			BinaryReader br = new BinaryReader(System.IO.File.OpenRead(Common.MyOhana.Current_Model));
			byte[] buff = br.ReadBytes(0x10);
			br.BaseStream.Position = 0x80;
			buff = br.ReadBytes(Common.Read32(buff, 8) - Common.Read32(buff, 4));
			br.Close();
			return buff;
		}

		public void saveMapProps(short w, short h, uint[] mapVals)
		{
			using (FileStream dataStream = new FileStream(Common.MyOhana.Current_Model, FileMode.Open)) {
				using (BinaryWriter bw = new BinaryWriter(dataStream)) {
					try {
						bw.BaseStream.Position = 0x80;
						bw.Write(w);
						bw.Write(h);
						for (int i = 0; i <= mapVals.Length - 1; i++) {
							bw.Write(mapVals[i]);
						}
						bw.Close();
					} catch (Exception ex) {
						Console.WriteLine(ex.StackTrace);
					}
				}
				MessageBox.Show("Saved map!");
			}
		}
		public FrmMain()
		{
			DragEnter += FrmMain_DragEnter;
			DragDrop += FrmMain_DragDrop;
			KeyDown += FrmMain_KeyDown;
			Load += FrmMain_Load;
			InitializeComponent();
		}

	}
}
