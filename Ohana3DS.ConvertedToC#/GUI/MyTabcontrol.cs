using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace Ohana3DS
{
    public class MyTabcontrol : TabControl
	{
		public MyTabcontrol()
		{
			InitializeComponent();

			this.DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		//Descartar substituições de formulário para limpar a lista de componentes.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if ((components != null)) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Exigido pelo Windows Form Designer

		private System.ComponentModel.IContainer components;
		//OBSERVAÇÃO: O procedimento a seguir é exigido pelo Windows Form Designer
		//Ele pode ser modificado usando o Windows Form Designer.  
		//Não o modifique usando o editor de códigos.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		private Image BgImage;
		[Browsable(true)]
		public override Image BackgroundImage {
			get { return BgImage; }
			set {
				BgImage = value;
				this.Refresh();
			}
		}
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			if (BgImage != null)
				e.Graphics.FillRectangle(new TextureBrush(BgImage), this.ClientRectangle);

			if (TabCount <= 0)
				return;

			for (int Index = 0; Index <= TabCount - 1; Index++) {
				TabPage Current_Tab = this.TabPages[Index];
				Rectangle Tab_Rect = this.GetTabRect(Index);
				e.Graphics.FillRectangle(new SolidBrush(Current_Tab.BackColor), Tab_Rect);
				if (Index == SelectedIndex)
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(15, 82, 186)), Tab_Rect);

				//Abast com rotação de texto
				if (Alignment == TabAlignment.Left | Alignment == TabAlignment.Right) {
					float Rotation_Angle = 90;
					if (Alignment == TabAlignment.Left)
						Rotation_Angle = 270;
					PointF Point = new PointF(Tab_Rect.Left + (Tab_Rect.Width / 2), Tab_Rect.Top + (Tab_Rect.Height / 2));
					e.Graphics.TranslateTransform(Point.X, Point.Y);
					e.Graphics.RotateTransform(Rotation_Angle);
					Tab_Rect = new Rectangle(-(Tab_Rect.Height / 2), -(Tab_Rect.Width / 2), Tab_Rect.Height, Tab_Rect.Width);
				}

				//Desenha nome da aba
				StringFormat Format = new StringFormat();
				Format.Alignment = StringAlignment.Center;
				Format.LineAlignment = StringAlignment.Center;
				e.Graphics.DrawString(Current_Tab.Text, this.Font, new SolidBrush(Current_Tab.ForeColor), (RectangleF)(Tab_Rect), Format);
				e.Graphics.ResetTransform();
			}
		}
	}
}
