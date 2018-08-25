using System;
using System.Drawing;
using System.Windows.Forms;
namespace Ohana3DS
{
    public class MySliderLabel : Control
	{

		private Timer withEventsField_Timer = new Timer();
		private Timer Timer {
			get { return withEventsField_Timer; }
			set {
				if (withEventsField_Timer != null) {
					withEventsField_Timer.Tick -= Timer_Tick;
				}
				withEventsField_Timer = value;
				if (withEventsField_Timer != null) {
					withEventsField_Timer.Tick += Timer_Tick;
				}
			}

		}
		private int Scroll_X;

		private bool Scrolling_Needed;
		public MySliderLabel()
		{
			this.DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

			Timer.Interval = 40;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Transparent, e.ClipRectangle);
			int Text_Width = Convert.ToInt32(e.Graphics.MeasureString(this.Text, this.Font).Width);
			if (Text_Width > this.Width)
				Scrolling_Needed = true;

			if (Scrolling_Needed) {
				if (Scroll_X > Text_Width)
					Scroll_X = 0;
				e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(Scroll_X * -1, -1));
				e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(Text_Width + (Scroll_X * -1), -1));
			} else {
				e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(0, -1));
			}

			base.OnPaint(e);
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			if (Scrolling_Needed)
				Timer.Enabled = true;

			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			Timer.Enabled = false;
			Scroll_X = 0;
			this.Refresh();

			base.OnMouseLeave(e);
		}
		protected override void OnTextChanged(EventArgs e)
		{
			Scrolling_Needed = false;
			this.Refresh();

			base.OnTextChanged(e);
		}

		private void Timer_Tick(object sender, System.EventArgs e)
		{
			Scroll_X += 1;
			this.Refresh();
		}
	}
}
