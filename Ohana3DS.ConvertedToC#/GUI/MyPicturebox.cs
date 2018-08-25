using System;
using System.Drawing;
using System.Windows.Forms;
namespace Ohana3DS
{
    public class MyPicturebox : PictureBox
	{


		private Color Ash_Gray = Color.FromArgb(178, 190, 181);
		private int Scroll_X;
		private int Scroll_Y;
		private int Scroll_Bar_X;
		private int Scroll_Bar_Y;

		private int Scroll_Bar_Size = 64;
		private Point Mouse_Position;
		private int Scroll_Mouse_X;
		private int Scroll_Mouse_Y;
		private bool Mouse_Drag_H;
		private bool Mouse_Drag_V;
		private Color Bar_Fore_Color_H;

		private Color Bar_Fore_Color_V;
		private bool Mouse_Inside;

		private bool Show_Scrollbars;

		private Image Img;
		public Image Image {
			get { return Img; }
			set {
				Img = value;
				Scroll_X = 0;
				Scroll_Y = 0;
				Scroll_Bar_X = 0;
				Scroll_Bar_Y = 0;
				Show_Scrollbars = false;
				this.Refresh();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (Img != null) {
				pe.Graphics.DrawImage(Img, new Point(Img.Width > this.Width ? Scroll_X * -1 : 0, Img.Height > this.Height ? Scroll_Y * -1 : 0));

				//Barra de rolagem
				if (Show_Scrollbars) {
					//Horizontal
					if (Img.Width > this.Width) {
						pe.Graphics.FillRectangle(new SolidBrush(Bar_Fore_Color_H), new Rectangle(Scroll_Bar_X + 1, this.Height - 10, Scroll_Bar_Size - 2, 9));
						Draw_Rounded_Rectangle(pe.Graphics, new Rectangle(Scroll_Bar_X, this.Height - 11, Scroll_Bar_Size - 1, 10), 4, Bar_Fore_Color_H);
					}
					//Vertical
					if (Img.Height > this.Height) {
						pe.Graphics.FillRectangle(new SolidBrush(Bar_Fore_Color_V), new Rectangle(this.Width - 10, Scroll_Bar_Y + 1, 9, Scroll_Bar_Size - 2));
						Draw_Rounded_Rectangle(pe.Graphics, new Rectangle(this.Width - 11, Scroll_Bar_Y, 10, Scroll_Bar_Size - 1), 4, Bar_Fore_Color_V);
					}
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				Rectangle Scroll_Rect_H = new Rectangle(Scroll_Bar_X, this.Height - 10, Scroll_Bar_Size, 10);
				Rectangle Scroll_Rect_V = new Rectangle(this.Width - 10, Scroll_Bar_Y, 10, Scroll_Bar_Size);
				Rectangle Mouse_Rect = new Rectangle(e.X, e.Y, 1, 1);
				if (Scroll_Rect_H.IntersectsWith(Mouse_Rect)) {
					Scroll_Mouse_X = e.X - Scroll_Bar_X;
					Mouse_Drag_H = true;
				} else if (Scroll_Rect_V.IntersectsWith(Mouse_Rect)) {
					Scroll_Mouse_Y = e.Y - Scroll_Bar_Y;
					Mouse_Drag_V = true;
				}
			}

			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.Focus();
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				Mouse_Drag_H = false;
				Mouse_Drag_V = false;
				if (!Mouse_Inside) {
					Show_Scrollbars = false;
					this.Refresh();
				}
			}

			base.OnMouseUp(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Rectangle Scroll_Rect_H = new Rectangle(Scroll_Bar_X, this.Height - 10, Scroll_Bar_Size, 10);
			Rectangle Scroll_Rect_V = new Rectangle(this.Width - 10, Scroll_Bar_Y, 10, Scroll_Bar_Size);
			Rectangle Mouse_Rect = new Rectangle(e.X, e.Y, 1, 1);
			if (Scroll_Rect_H.IntersectsWith(Mouse_Rect)) {
				if (Bar_Fore_Color_H != Ash_Gray) {
					Bar_Fore_Color_H = Ash_Gray;
					this.Refresh();
				}
			} else if (!Mouse_Drag_H) {
				if (Bar_Fore_Color_H != Color.White) {
					Bar_Fore_Color_H = Color.White;
					this.Refresh();
				}
			}
			if (Scroll_Rect_V.IntersectsWith(Mouse_Rect)) {
				if (Bar_Fore_Color_V != Ash_Gray) {
					Bar_Fore_Color_V = Ash_Gray;
					this.Refresh();
				}
			} else if (!Mouse_Drag_V) {
				if (Bar_Fore_Color_V != Color.White) {
					Bar_Fore_Color_V = Color.White;
					this.Refresh();
				}
			}

			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				if (Mouse_Drag_H) {
					int X = e.X - Scroll_Mouse_X;
					if (X < 0) {
						X = 0;
					} else if (X > this.Width - Scroll_Bar_Size) {
						X = this.Width - Scroll_Bar_Size;
					}
					Scroll_Bar_X = X;

					Scroll_X = Convert.ToInt32((X / (this.Width - Scroll_Bar_Size)) * (Img.Width - this.Width));
					this.Refresh();
				} else if (Mouse_Drag_V) {
					int Y = e.Y - Scroll_Mouse_Y;
					if (Y < 0) {
						Y = 0;
					} else if (Y > this.Height - Scroll_Bar_Size) {
						Y = this.Height - Scroll_Bar_Size;
					}
					Scroll_Bar_Y = Y;

					Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Size)) * (Img.Height - this.Height));
					this.Refresh();
				}
			}

			base.OnMouseMove(e);
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			Mouse_Inside = true;
			Show_Scrollbars = true;
			this.Refresh();

			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!Mouse_Drag_H)
				Bar_Fore_Color_H = Color.White;
			if (!Mouse_Drag_V)
				Bar_Fore_Color_V = Color.White;
			Mouse_Inside = false;
			if (!Mouse_Drag_H & !Mouse_Drag_V)
				Show_Scrollbars = false;
			this.Refresh();

			base.OnMouseLeave(e);
		}

		private void Draw_Rounded_Rectangle(Graphics Gfx, Rectangle Rect, int Width, Color Color)
		{
			Pen Pen = new Pen(Color);

			RectangleF Arc_Rect = new RectangleF(Rect.Location, new SizeF(Width, Width));

			//Top Left Arc
			Gfx.DrawArc(Pen, Arc_Rect, 180, 90);
			Gfx.DrawLine(Pen, Rect.X + Convert.ToInt32(Width / 2), Rect.Y, Rect.X + Rect.Width - Convert.ToInt32(Width / 2), Rect.Y);

			//Top Right Arc
			Arc_Rect.X = Rect.Right - Width;
			Gfx.DrawArc(Pen, Arc_Rect, 270, 90);
			Gfx.DrawLine(Pen, Rect.X + Rect.Width, Rect.Y + Convert.ToInt32(Width / 2), Rect.X + Rect.Width, Rect.Y + Rect.Height - Convert.ToInt32(Width / 2));

			//Bottom Right Arc
			Arc_Rect.Y = Rect.Bottom - Width;
			Gfx.DrawArc(Pen, Arc_Rect, 0, 90);
			Gfx.DrawLine(Pen, Rect.X + Convert.ToInt32(Width / 2), Rect.Y + Rect.Height, Rect.X + Rect.Width - Convert.ToInt32(Width / 2), Rect.Y + Rect.Height);

			//Bottom Left Arc
			Arc_Rect.X = Rect.Left;
			Gfx.DrawArc(Pen, Arc_Rect, 90, 90);
			Gfx.DrawLine(Pen, Rect.X, Rect.Y + Convert.ToInt32(Width / 2), Rect.X, Rect.Y + Rect.Height - Convert.ToInt32(Width / 2));
		}
	}
}
