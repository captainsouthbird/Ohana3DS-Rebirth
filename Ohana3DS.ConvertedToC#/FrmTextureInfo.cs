using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Ohana3DS
{
    public partial class FrmTextureInfo
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

		private void LstModelTextures_SelectedIndexChanged(int Index)
		{
			if (Index > -1) {
				My.MyProject.Forms.FrmMain.TxtSearch.Text = Common.MyOhana.Model_Texture_Index[Index];
			}
		}
		#endregion

	}
}
