using System;
using System.IO;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmMain form = new FrmMain();
            if (args.Length > 0 && File.Exists(args[0]))
            {
                string textureDir = null;
                if(args.Length >= 2 && Directory.Exists(args[1]))
                {
                    textureDir = args[1];
                }

                form.setFileToOpen(args[0], textureDir);
            }
            Application.Run(form);
        }
    }
}
