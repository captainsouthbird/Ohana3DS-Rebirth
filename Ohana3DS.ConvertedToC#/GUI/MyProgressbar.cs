using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Ohana3DS
{

    public class MyProgressbar : Control
	{

		private float ProgressVal;
		public MyProgressbar()
		{
			this.DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
		}
		public float Percentage {
			get { return ProgressVal; }
			set {
				ProgressVal = value;
				this.Refresh();
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
			LinearGradientBrush Bg = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height - 1), Color.Transparent, Color.FromArgb(15, 82, 186));
			e.Graphics.FillRectangle(Bg, new Rectangle(0, 0, Convert.ToInt32((ProgressVal / 100) * (this.Width - 1)), this.Height - 1));

			string Text = this.Text;
			bool Ellipsis = e.Graphics.MeasureString(Text, this.Font).Width > this.Width;
			if (Ellipsis) {
				while (e.Graphics.MeasureString(Text, this.Font).Width > this.Width) {
					Text = Text.Substring(0, Text.Length - 1);
				}
				if (Text.Length > 0)
					Text = Text.Substring(0, Text.Length - 1);
				Text += "...";
			}
			int TextW = Convert.ToInt32(e.Graphics.MeasureString(Text, this.Font).Width);
			int TextH = Convert.ToInt32(e.Graphics.MeasureString(Text, this.Font).Height);
			e.Graphics.DrawString(Text, this.Font, new SolidBrush(this.ForeColor), new Point((this.Width / 2) - (TextW / 2), (this.Height / 2) - (TextH / 2)));

			LinearGradientBrush Border = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height - 1), Color.Transparent, this.ForeColor);
			e.Graphics.DrawRectangle(new Pen(Border), new Rectangle(0, 0, this.Width - 1, this.Height - 1));

			base.OnPaint(e);
		}
	}
}
