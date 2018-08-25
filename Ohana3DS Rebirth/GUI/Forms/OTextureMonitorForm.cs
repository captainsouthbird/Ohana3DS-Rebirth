using System;
using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI.Forms
{
    public partial class OTextureMonitorForm : OForm
    {
        public class TextureMonitorFolderSetEventArgs : EventArgs
        {
            public string TextureMonitorFolder { get; private set; }

            public TextureMonitorFolderSetEventArgs(string folder)
            {
                TextureMonitorFolder = folder;
            }
        }

        public delegate void TextureMonitorFolderSetHandler(object sender, TextureMonitorFolderSetEventArgs e);
        public event TextureMonitorFolderSetHandler TextureMonitorFolderSet;

        public OTextureMonitorForm()
        {
            InitializeComponent();
        }

        private void OTextureMonitorForm_Load(object sender, EventArgs e)
        {
            TxtOutFolder.Text = Settings.Default.teOutFolder;
        }

        private void OTextureMonitorForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: ok(); break;
                case Keys.Escape: Close(); break;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            TextureMonitorFolderSet?.Invoke(this, new TextureMonitorFolderSetEventArgs(TxtOutFolder.Text));

            ok();
        }

        private void ok()
        {
            if (!Directory.Exists(TxtOutFolder.Text))
            {
                MessageBox.Show("Invalid texture directory!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Settings.Default.teOutFolder = TxtOutFolder.Text;
            Settings.Default.Save();

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK) TxtOutFolder.Text = dlg.SelectedPath;
            }
        }
    }
}
