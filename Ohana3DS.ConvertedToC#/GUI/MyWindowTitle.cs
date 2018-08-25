using System;
using System.Windows.Forms;
namespace Ohana3DS
{
    public class MyWindowTitle : Label
	{
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			base.WndProc(ref m);
			switch (m.Msg) {
				case 0x84:
					m.Result = new IntPtr(-1);
					break;
			}
		}
	}
}
