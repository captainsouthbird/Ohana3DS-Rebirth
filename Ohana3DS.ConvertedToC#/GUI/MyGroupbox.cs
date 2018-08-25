using System;
using System.Drawing;
using System.Windows.Forms;
namespace Ohana3DS
{
    public class MyGroupbox : GroupBox
	{
		public MyGroupbox()
		{
			this.DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			SizeF Size = e.Graphics.MeasureString(this.Text, this.Font);
			Size Text_Size = new Size(Convert.ToInt32(Size.Width) + 8, Convert.ToInt32(Size.Height));

			Rectangle Border_Rectangle = new Rectangle(0, 0, this.Width, this.Height);
			Border_Rectangle.Y = (Border_Rectangle.Y + (Text_Size.Height / 2));
			Border_Rectangle.Width -= 1;
			Border_Rectangle.Height = (Border_Rectangle.Height - (Text_Size.Height / 2));

			e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(0, 8), new Point(6, 8));
			e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(Text_Size.Width, 8), new Point(Border_Rectangle.Width, 8));

			Rectangle Text_Rectangle = new Rectangle(8, 0, Text_Size.Width, Text_Size.Height);
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), Text_Rectangle);
			e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), Text_Rectangle);
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			this.Refresh();

			base.OnLocationChanged(e);
		}
	}
}
