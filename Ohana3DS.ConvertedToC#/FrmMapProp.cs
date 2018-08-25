using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Ohana3DS
{
    public partial class FrmMapProp
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

		uint[] mapVals;

		string[] mapProps;
		int mouseX = 0;

		int mouseY = 0;
		short mapWidth = 0;

		short mapHeight = 0;

		bool mode = false;
		private void FrmMapProp_Load(object sender, EventArgs e)
		{
			Common.MyOhana.Map_Properties_Mode = true;
			mapProps = Ohana3DS.My.Resources.Resources.MapProperties.Split(new char[] {
				Environment.NewLine[0],
				','
			}, StringSplitOptions.None);
			mapPropCom.DataSource = Common.MyOhana.getProps();
		}
		private void FrmMapProp_FormClosing(object sender, EventArgs e)
		{
			Common.MyOhana.Map_Properties_Mode = false;
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

		public void makeMapIMG(byte[] byteArray)
		{
			List<uint> proplist = new List<uint>();
			using (System.IO.Stream dataStream = new System.IO.MemoryStream(byteArray)) {
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(dataStream)) {
					try {
						mapWidth = (short)br.ReadUInt16();
						mapHeight = (short)br.ReadUInt16();
						for (int i = 0; i <= mapWidth * mapHeight - 1; i++) {
							proplist.Add(br.ReadUInt32());
						}
					} catch (Exception ex) {
						Console.WriteLine(ex.StackTrace);
					}
					br.Close();
					mapVals = proplist.ToArray();
					updateImg();
				}
			}
		}

		private void updateImg()
		{
			Bitmap img = new Bitmap(mapWidth * 8, mapHeight * 8);
			Color c = new Color();
			int i = 0;
			uint col = 0;
			for (int v = 0; v <= mapVals.Length - 1; v++) {
				col = mapVals[v];
				if (col == 0x1000021) {
					c = Color.Black;
				} else {
					col = LCG(col, 4);
					c = Color.FromArgb(0xff, 0xff - Convert.ToByte(col & 0xff), 0xff - Convert.ToByte((col >> 8) & 0xff), 0xff - Convert.ToByte(col >> 24 & 0xff));
				}
				for (int x = 0; x <= 7; x++) {
					for (int y = 0; y <= 7; y++) {
						img.SetPixel((x + (i * 8) % (img.Width)), y + ((i / mapWidth) * 8), x == 7 | y == 7 ? Color.Black : c);
					}
				}
				i = i + 1;
			}
			mapPicBox.Image = img;
		}

		public uint LCG(long seed, int ctr)
		{
			for (int i = 0; i <= ctr - 1; i++) {
				seed *= 0x41c64e6d;
				seed += 0x6073;
			}
			return (uint)seed;
		}

		private void mapPicBox_Click(object sender, EventArgs e)
		{
			var mouseEventArgs = e as MouseEventArgs;
			if (mouseEventArgs != null) {
				mouseX = (int)Math.Floor((double)(mouseEventArgs.X / 8));
				mouseY = (int)Math.Floor((double)(mouseEventArgs.Y / 8));
				mapCoords.Text = "X= " + Convert.ToString(mouseX) + " Y= " + Convert.ToString(mouseY);
				//Edit mode
				if (mode == true) {
					mapVals[(mouseY * 40) + mouseX] = uint.Parse(mapProps[Array.FindIndex(mapProps, s => s == mapPropCom.Text) - 1]);
					updateImg();
				//View mode
				} else {
					for (uint i = 0; i <= mapProps.Length - 1; i += 2) {
						uint p1 = uint.Parse(mapProps[i]);
						string p2 = mapProps[i + 1];
						if (p1 == mapVals[((mouseY * 40) + mouseX)]) {
							mapPropCom.Text = p2;
						}
					}
				}
			}
		}

		public uint[] getMapVals()
		{
			return mapVals;
		}

		private void mapPropSet_Click(object sender, EventArgs e)
		{
			if (mode == true) {
				mode = false;
				mapPropSet.Text = "Edit";
			} else {
				mode = true;
				mapPropSet.Text = "View";
			}
		}

		private void mapPropSave_Click(object sender, EventArgs e)
		{
			My.MyProject.Forms.FrmMain.saveMapProps(mapWidth, mapHeight, mapVals);
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
