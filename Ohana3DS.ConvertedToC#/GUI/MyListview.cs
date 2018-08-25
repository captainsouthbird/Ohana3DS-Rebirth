using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
namespace Ohana3DS
{

    public class MyListview : Control
	{

		private Color Bar_Fore_Color = Color.White;
		private Color Selected = Color.FromArgb(15, 82, 186);

		private Color Ash_Gray = Color.FromArgb(178, 190, 181);
		public struct ListText
		{
			public string Text;
			public int Left;
			public Color ForeColor;
			public bool Vertical_Line;
		}
		public struct ListItem
		{
			public ListText[] Text;
			public bool Header;
		}
		private List<ListItem> LstItems = new List<ListItem>();
		private int Tile_Height = 32;
		private int Selected_Index = -1;

		private int Selected_Index_Total = -1;
		private int Scroll_Y;
		private int Scroll_Bar_Y;

		private int Scroll_Bar_Height = 64;
		private Point Mouse_Position;
		private int Scroll_Mouse_Y;
		private bool Mouse_Drag;
		private bool Clicked;
		public MyListview()
		{
			this.DoubleBuffered = true;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
		}
		public int TileHeight {
			get { return Tile_Height; }
			set { Tile_Height = value; }
		}
		public int SelectedIndex {
			get { return Selected_Index; }
			set { Selected_Index = value; }
		}
		public void AddItem(string Text, int Left = 0, bool Header = false)
		{
			ListItem Item = default(ListItem);
			Item.Text = new ListText[1];
			Item.Text[0].Text = Text;
			Item.Text[0].Left = Left;
			Item.Header = Header;
			LstItems.Add(Item);
		}
		public void AddItem(ListItem Item)
		{
			LstItems.Add(Item);
		}
		public void ChangeItem(int Index, ListItem Item)
		{
			ListItem[] Temp = LstItems.ToArray();
			int j = 0;
			for (int i = 0; i <= Temp.Length - 1; i++) {
				if (!Temp[i].Header) {
					if (j == Index) {
						Temp[i] = Item;
						break; // TODO: might not be correct. Was : Exit For
					}
					j += 1;
				}
			}
			LstItems.Clear();
			LstItems.AddRange(Temp);
			this.Refresh();
		}
		public void Clear()
		{
			LstItems.Clear();
			Selected_Index = -1;
			Scroll_Y = 0;
			Scroll_Bar_Y = 0;
			this.Refresh();
		}
		public void Scroll_To_End()
		{
			if (LstItems != null) {
				if (LstItems.Count == 0)
					return;
				int Total_Size = LstItems.Count * Tile_Height;
				while (((LstItems.Count - 1) * Tile_Height) - Scroll_Y > (this.Height - Tile_Height)) {
					int Y = Scroll_Bar_Y + 1;
					if (Y > this.Height - Scroll_Bar_Height)
						Y = this.Height - Scroll_Bar_Height;
					Scroll_Bar_Y = Y;
					Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Height)) * (Total_Size - this.Height));
					if (Y == this.Height - Scroll_Bar_Height)
						break; // TODO: might not be correct. Was : Exit While
				}
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (LstItems.Count == 0)
				return;
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);

			if (LstItems != null) {
				int Total_Size = LstItems.Count * Tile_Height;
				int Start_Y = 0;
				if (Total_Size > this.Height)
					Start_Y = Scroll_Y * -1;
				int Total_Index = 0;
				int Index = 0;

				foreach (ListItem Item in LstItems) {
					//Item selecionado (e detecção de click no Item)
					if (Start_Y >= -Tile_Height) {
						if (Start_Y > this.Height)
							break; // TODO: might not be correct. Was : Exit For
						if (!Item.Header) {
							if (Clicked) {
								Rectangle Item_Rect = new Rectangle(0, Start_Y, this.Width, TileHeight);
								Rectangle Mouse_Rect = new Rectangle(Mouse_Position, new Size(1, 1));
								//Selecionado
								if (Item_Rect.IntersectsWith(Mouse_Rect)) {
									Clicked = false;
									e.Graphics.FillRectangle(new SolidBrush(Selected), new Rectangle(0, Start_Y, this.Width, TileHeight));
									Selected_Index = Index;
									Selected_Index_Total = Total_Index;
									if (SelectedIndexChanged != null) {
										SelectedIndexChanged(Index);
									}
								}
							} else {
								if (Index == Selected_Index)
									e.Graphics.FillRectangle(new SolidBrush(Selected), new Rectangle(0, Start_Y, this.Width, TileHeight));
							}
						}

						//Textos e afins
						int Temp = 0;
                        for(var i = 0; i < Item.Text.Length;i++ ){
							int TxtHeight = Convert.ToInt32(e.Graphics.MeasureString(Item.Text[i].Text, this.Font).Height);
							if (Item.Text[i].ForeColor == null)
                                Item.Text[i].ForeColor = this.ForeColor;
							if (Item.Text[i].Text != null) {
								e.Graphics.DrawString(Item.Text[i].Text, this.Font, new SolidBrush(Item.Text[i].ForeColor), new Point(Item.Text[i].Left, (Start_Y + (Tile_Height / 2) - (TxtHeight / 2))));
							}

							if (Item.Header) {
								int ItemW = 0;
								if (Temp == Item.Text.Count() - 1)
									ItemW = this.Width - Item.Text[i].Left;
								else
									ItemW = Item.Text[Temp + 1].Left - Item.Text[i].Left;
								int X = Item.Text[i].Left + ItemW - 1;
								int Y = Start_Y + Tile_Height - 1;
								e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(Item.Text[i].Left, Y), new Point(X, Y));
								e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(X, Start_Y), new Point(X, Y));
							}

							if (Item.Text[i].Vertical_Line) {
								e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(Item.Text[i].Left - 1, Start_Y), new Point(Item.Text[i].Left - 1, this.Height));
							}

							Temp += 1;
						}
					}

					Start_Y += Tile_Height;
					if (!Item.Header)
						Index += 1;
					Total_Index += 1;
				}

				//Barra de rolagem
				if (Total_Size > this.Height) {
					e.Graphics.FillRectangle(new SolidBrush(Bar_Fore_Color), new Rectangle(this.Width - 10, Scroll_Bar_Y + 1, 9, Scroll_Bar_Height - 2));
					Draw_Rounded_Rectangle(e.Graphics, new Rectangle(this.Width - 11, Scroll_Bar_Y, 10, Scroll_Bar_Height - 1), 4, Bar_Fore_Color);
				}
			}

			base.OnPaint(e);
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			this.Focus();

			base.OnMouseEnter(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				Rectangle Scroll_Rect = new Rectangle(this.Width - 10, Scroll_Bar_Y, 10, Scroll_Bar_Height);
				if (Scroll_Rect.IntersectsWith(new Rectangle(e.X, e.Y, 1, 1))) {
					Scroll_Mouse_Y = e.Y - Scroll_Bar_Y;
					Mouse_Drag = true;
				} else {
					int Index = e.Y / Tile_Height;
					if (Index > -1 & Index < LstItems.Count) {
						if (!LstItems[Index].Header) {
							Clicked = true;
							Mouse_Position = e.Location;
							this.Refresh();
						}
					}
				}
			}

			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.Focus();
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
				Mouse_Drag = false;

			base.OnMouseUp(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Rectangle Scroll_Rect = new Rectangle(this.Width - 10, Scroll_Bar_Y, 10, Scroll_Bar_Height);
			if (Scroll_Rect.IntersectsWith(new Rectangle(e.X, e.Y, 1, 1))) {
				if (Bar_Fore_Color != Ash_Gray) {
					Bar_Fore_Color = Ash_Gray;
					this.Refresh();
				}
			} else if (!Mouse_Drag) {
				if (Bar_Fore_Color != Color.White) {
					Bar_Fore_Color = Color.White;
					this.Refresh();
				}
			}

			if (e.Button == MouseButtons.Left & Mouse_Drag) {
				int Y = e.Y - Scroll_Mouse_Y;
				if (Y < 0) {
					Y = 0;
				} else if (Y > this.Height - Scroll_Bar_Height) {
					Y = this.Height - Scroll_Bar_Height;
				}
				Scroll_Bar_Y = Y;

				int Total_Size = LstItems.Count * Tile_Height;
				Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Height)) * (Total_Size - this.Height));
				this.Refresh();
			}

			base.OnMouseMove(e);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int Total_Size = LstItems.Count * Tile_Height;
			if (e.Delta != 0 & Total_Size > this.Height) {
				int Y = 0;

				if (e.Delta > 0) {
					Y = Scroll_Bar_Y - 16;
				} else if (e.Delta < 0) {
					Y = Scroll_Bar_Y + 16;
				}

				if (Y < 0) {
					Y = 0;
				} else if (Y > this.Height - Scroll_Bar_Height) {
					Y = this.Height - Scroll_Bar_Height;
				}
				Scroll_Bar_Y = Y;

				Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Height)) * (Total_Size - this.Height));
				this.Refresh();
			}

			base.OnMouseWheel(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!Mouse_Drag) {
				Bar_Fore_Color = Color.White;
				this.Refresh();
			}

			base.OnMouseLeave(e);
		}
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData) {
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					return true;
			}
			return base.IsInputKey(keyData);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			int Total_Size = LstItems.Count * Tile_Height;
			switch (e.KeyCode) {
				case Keys.Up:
					if (Selected_Index > 0) {
						Selected_Index -= 1;
						Selected_Index_Total -= 1;
						if (SelectedIndexChanged != null) {
							SelectedIndexChanged(Selected_Index);
						}
					}

					while ((Selected_Index_Total * Tile_Height) - Scroll_Y < 0) {
						int Y = Scroll_Bar_Y - 1;
						if (Y < 0)
							Y = 0;
						Scroll_Bar_Y = Y;
						Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Height)) * (Total_Size - this.Height));
						if (Y == 0)
							break; // TODO: might not be correct. Was : Exit While
					}
					break;
				case Keys.Down:
					if (Selected_Index < Get_Headerless_Length() - 1) {
						Selected_Index += 1;
						Selected_Index_Total += 1;
						if (SelectedIndexChanged != null) {
							SelectedIndexChanged(Selected_Index);
						}
					}

					while ((Selected_Index_Total * Tile_Height) - Scroll_Y > (this.Height - Tile_Height)) {
						int Y = Scroll_Bar_Y + 1;
						if (Y > this.Height - Scroll_Bar_Height)
							Y = this.Height - Scroll_Bar_Height;
						Scroll_Bar_Y = Y;
						Scroll_Y = Convert.ToInt32((Y / (this.Height - Scroll_Bar_Height)) * (Total_Size - this.Height));
						if (Y == this.Height - Scroll_Bar_Height)
							break; // TODO: might not be correct. Was : Exit While
					}
					break;
			}

			this.Refresh();

			base.OnKeyDown(e);
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

		private int Get_Headerless_Length()
		{
			int j = 0;
			for (int i = 0; i <= LstItems.Count - 1; i++) {
				if (!LstItems[i].Header)
					j += 1;
			}
			return j;
		}

		public event SelectedIndexChangedEventHandler SelectedIndexChanged;
		public delegate void SelectedIndexChangedEventHandler(int New_Index);
	}
}
