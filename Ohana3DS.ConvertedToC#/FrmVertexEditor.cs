using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
namespace Ohana3DS
{
    public partial class FrmVertexEditor
	{
		[DllImport("dwmapi")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);
		[DllImport("dwmapi")]
		public static extern int DwmSetWindowAttribute(IntPtr hWnd, int Attr, ref int AttrValue, int AttrSize);
		public struct Margins
		{
			public int TopHeight;
			public int BottomHeight;
			public int LeftWidth;
			public int RightWidth;
		}
		protected override CreateParams CreateParams {
			//Cria sombra (sem Aero)
			get {
				CreateParams Create_Params = base.CreateParams;
				Create_Params.ClassStyle = Create_Params.ClassStyle | 0x20000;
				return Create_Params;
			}
		}
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
				case 0x85:
					//Cria sombra (com Aero)
					var val = 2;
					DwmSetWindowAttribute(Handle, 2, ref val, 4);
					Margins Margins = new Margins();
					var _with1 = Margins;
					_with1.TopHeight = 1;
					_with1.BottomHeight = 1;
					_with1.LeftWidth = 1;
					_with1.RightWidth = 1;
					DwmExtendFrameIntoClientArea(Handle, ref Margins);
					break;
			}
		}

		private void FrmVertexEditor_Load(object sender, EventArgs e)
		{
			LstObjects.Clear();

			int ObjIndex = 0;
			foreach (Ohana.VertexList Obj in Common.MyOhana.Model_Object) {
				var _with2 = Obj;
				LstObjects.AddItem("obj_" + ObjIndex + "_" + Common.MyOhana.Model_Texture_Index[_with2.Texture_ID]);

				ObjIndex += 1;
			}

			LstObjects.Refresh();
		}
		private void FrmMapProp_FormClosing(object sender, EventArgs e)
		{
			Common.MyOhana.Edit_Mode = false;
		}

		private void LstObjects_SelectedIndexChanged(int Index)
		{
			LstFaces.Clear();
			Common.MyOhana.Selected_Object = Index;
			Common.MyOhana.Selected_Face = -1;
			for (int Face = 0; Face <= Common.MyOhana.Model_Object[Index].Per_Face_Index.Count - 1; Face++) {
				LstFaces.AddItem("face_" + Face);
			}
			TxtTexID.Text = Common.MyOhana.Model_Object[Index].Texture_ID.ToString();
			LstFaces.Refresh();
			Common.MyOhana.Edit_Mode = true;
		}
		private void LstFaces_SelectedIndexChanged(int Index)
		{
			Common.MyOhana.Selected_Face = Index;
		}

		private void BtnImportObj_Click(object sender, EventArgs e)
		{
			if (LstObjects.SelectedIndex > -1) {
				OpenFileDialog OpenDlg = new OpenFileDialog();
				OpenDlg.Filter = "Wavefront OBJ|*.obj";
				if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					if (File.Exists(OpenDlg.FileName)) {
						Common.MyOhana.Insert_OBJ(OpenDlg.FileName);
					}
				}
			} else {
				Interaction.MsgBox("You must select an object first!", Constants.vbExclamation);
			}
		}
		private void BtnExportObj_Click(object sender, EventArgs e)
		{
			if (LstObjects.SelectedIndex > -1) {
				SaveFileDialog SaveDlg = new SaveFileDialog();
				SaveDlg.Filter = "Wavefront OBJ|*.obj";
				if (SaveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					StringBuilder Out = new StringBuilder();
					NumberFormatInfo Info = new NumberFormatInfo();
					Info.NumberDecimalSeparator = ".";
					Info.NumberDecimalDigits = 6;

					Out.AppendLine("mtllib " + Path.GetFileName(SaveDlg.FileName) + ".mtl");

					var _with3 = Common.MyOhana.Model_Object[Common.MyOhana.Selected_Object];
					for (int i = 0; i <= _with3.Vertice.Length - 1; i++) {
						Out.AppendLine("v " + (_with3.Vertice[i].X * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with3.Vertice[i].Y * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with3.Vertice[i].Z * Common.MyOhana.Load_Scale).ToString("N", Info));
						Out.AppendLine("vn " + (_with3.Vertice[i].NX * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with3.Vertice[i].NY * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with3.Vertice[i].NZ * Common.MyOhana.Load_Scale).ToString("N", Info));
						Out.AppendLine("vt " + _with3.Vertice[i].U.ToString("N", Info) + " " + _with3.Vertice[i].V.ToString("N", Info));
					}

					Out.AppendLine("usemtl " + Common.MyOhana.Model_Texture_Index[_with3.Texture_ID]);

					for (int i = 0; i <= _with3.Index.Length - 1; i += 3) {
						string a = (_with3.Index[i] + 1).ToString();
						string b = (_with3.Index[i + 1] + 1).ToString();
						string c = (_with3.Index[i + 2] + 1).ToString();

						Out.AppendLine("f " + a + "/" + a + "/" + a + " " + b + "/" + b + "/" + b + " " + c + "/" + c + "/" + c);
					}

					File.WriteAllText(SaveDlg.FileName, Out.ToString());

					StringBuilder OutMtl = new StringBuilder();
					OutMtl.AppendLine("newmtl " + Common.MyOhana.Model_Texture_Index[_with3.Texture_ID]);
					OutMtl.AppendLine("illum 2");
					OutMtl.AppendLine("Kd 0.800000 0.800000 0.800000");
					OutMtl.AppendLine("Ka 0.200000 0.200000 0.200000");
					OutMtl.AppendLine("Ks 0.000000 0.000000 0.000000");
					OutMtl.AppendLine("Ke 0.000000 0.000000 0.000000");
					OutMtl.AppendLine("Ns 0.000000");
					OutMtl.AppendLine("map_Kd " + Common.MyOhana.Model_Texture_Index[_with3.Texture_ID] + ".png");

					File.WriteAllText(SaveDlg.FileName + ".mtl", OutMtl.ToString());
				}
			} else {
				MessageBox.Show("You must select an object first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		private void BtnExportFace_Click(object sender, EventArgs e)
		{
			if (LstFaces.SelectedIndex > -1) {
				SaveFileDialog SaveDlg = new SaveFileDialog();
				SaveDlg.Filter = "Wavefront OBJ|*.obj";
				if (SaveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					StringBuilder Out = new StringBuilder();
					NumberFormatInfo Info = new NumberFormatInfo();
					Info.NumberDecimalSeparator = ".";
					Info.NumberDecimalDigits = 6;

					Out.AppendLine("mtllib " + Path.GetFileName(SaveDlg.FileName) + ".mtl");

					var _with4 = Common.MyOhana.Model_Object[Common.MyOhana.Selected_Object];
					List<int> Temp = _with4.Per_Face_Index[LstFaces.SelectedIndex].ToList();
					int[] Vertex_Remap = new int[_with4.Vertice.Length + 1];
					int Count = 0;
					for (int i = 0; i <= _with4.Vertice.Length - 1; i++) {
						//Verifica se o vértice é de fato usado na face
						if (Temp.IndexOf(i) > -1) {
							Out.AppendLine("v " + (_with4.Vertice[i].X * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with4.Vertice[i].Y * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with4.Vertice[i].Z * Common.MyOhana.Load_Scale).ToString("N", Info));
							Out.AppendLine("vn " + (_with4.Vertice[i].NX * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with4.Vertice[i].NY * Common.MyOhana.Load_Scale).ToString("N", Info) + " " + (_with4.Vertice[i].NZ * Common.MyOhana.Load_Scale).ToString("N", Info));
							Out.AppendLine("vt " + _with4.Vertice[i].U.ToString("N", Info) + " " + _with4.Vertice[i].V.ToString("N", Info));
							Vertex_Remap[i] = Count;
							Count += 1;
						}
					}

					Out.AppendLine("usemtl " + Common.MyOhana.Model_Texture_Index[_with4.Texture_ID]);

					for (int i = 0; i <= _with4.Per_Face_Index[LstFaces.SelectedIndex].Length - 1; i += 3) {
						string a = (Vertex_Remap[_with4.Per_Face_Index[LstFaces.SelectedIndex][i]] + 1).ToString();
						string b = (Vertex_Remap[_with4.Per_Face_Index[LstFaces.SelectedIndex][i + 1]] + 1).ToString();
						string c = (Vertex_Remap[_with4.Per_Face_Index[LstFaces.SelectedIndex][i + 2]] + 1).ToString();

						Out.AppendLine("f " + a + "/" + a + "/" + a + " " + b + "/" + b + "/" + b + " " + c + "/" + c + "/" + c);
					}

					File.WriteAllText(SaveDlg.FileName, Out.ToString());

					StringBuilder OutMtl = new StringBuilder();
					OutMtl.AppendLine("newmtl " + Common.MyOhana.Model_Texture_Index[_with4.Texture_ID]);
					OutMtl.AppendLine("illum 2");
					OutMtl.AppendLine("Kd 0.800000 0.800000 0.800000");
					OutMtl.AppendLine("Ka 0.200000 0.200000 0.200000");
					OutMtl.AppendLine("Ks 0.000000 0.000000 0.000000");
					OutMtl.AppendLine("Ke 0.000000 0.000000 0.000000");
					OutMtl.AppendLine("Ns 0.000000");
					OutMtl.AppendLine("map_Kd " + Common.MyOhana.Model_Texture_Index[_with4.Texture_ID] + ".png");

					File.WriteAllText(SaveDlg.FileName + ".mtl", OutMtl.ToString());
				}
			} else {
				MessageBox.Show("You must select an face first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			if (Common.MyOhana.Selected_Face > -1) {
				byte[] Data = File.ReadAllBytes(Common.MyOhana.Temp_Model_File);
				var _with5 = Common.MyOhana.Model_Object[Common.MyOhana.Selected_Object];
				int Current_Face_Offset = _with5.Per_Face_Entry[Common.MyOhana.Selected_Face].Offset;
				int Face_Length = _with5.Per_Face_Entry[Common.MyOhana.Selected_Face].Length;

				for (int i = Current_Face_Offset; i <= (Current_Face_Offset + Face_Length); i++) {
					Data[i] = 0;
				}

				int j = 0;
				for (int k = 0; k <= Common.MyOhana.Selected_Face - 1; k++) {
					j += _with5.Per_Face_Entry[k].Length / _with5.Per_Face_Entry[k].Format;
				}
				for (int g = 0; g <= (Face_Length / _with5.Per_Face_Entry[Common.MyOhana.Selected_Face].Format) - 1; g++) {
					_with5.Index[j] = 0;
					_with5.Per_Face_Index[Common.MyOhana.Selected_Face][g] = 0;
					j += 1;
				}
				File.WriteAllBytes(Common.MyOhana.Temp_Model_File, Data);
			} else {
				MessageBox.Show("You must select an face first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void TxtTexID_TextChanged(object sender, EventArgs e)
		{
			if (LstObjects.SelectedIndex > -1) {
				int TexID = Int32.Parse(TxtTexID.Text);
				if (TexID < Common.MyOhana.Model_Texture_Index.Length) {
					var _with6 = Common.MyOhana.Model_Object[Common.MyOhana.Selected_Object];
					_with6.Texture_ID = TexID;
					byte[] Data = File.ReadAllBytes(Common.MyOhana.Temp_Model_File);
					Data[_with6.Texture_ID_Offset] = Convert.ToByte(TexID & 0xff);
					File.WriteAllBytes(Common.MyOhana.Temp_Model_File, Data);
				}
			}
		}

		#region "GUI"
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
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
		#endregion

	}
}
