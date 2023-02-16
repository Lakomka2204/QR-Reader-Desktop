using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace QR_Reader_Desktop
{
    public partial class About : Form
    {
        private string appMenuPath;
        private readonly string shortcutPath;
        bool exists;
        public About()
        {
            InitializeComponent();
            label1.Text = Settings.AppName;
            string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            appMenuPath = Path.Combine(startMenuPath, Settings.AppName);
            shortcutPath = Path.Combine(appMenuPath, Settings.AppName + ".lnk");
            exists = System.IO.File.Exists(shortcutPath);
            ToggleButton();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Lakomka2204/QR-Reader-Desktop");
        }
        void ToggleButton()
            => button1.Text = (exists ? "Delete" : "Create") + " start menu shortcut";

        private void button1_Click(object sender, EventArgs e)
        {
            if (exists)
            {
                System.IO.File.Delete(shortcutPath);
                exists = false;
                ToggleButton();
                MessageBox.Show("Shortcut deleted",Settings.AppName);
            }
            else
            {
                string path = Assembly.GetExecutingAssembly().Location;
                if (!Directory.Exists(appMenuPath))
                    Directory.CreateDirectory(appMenuPath);
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.Description = Settings.AppName;
                shortcut.TargetPath = path;
                shortcut.Save();
                exists = true;
                ToggleButton();
                MessageBox.Show("Shortcut created", Settings.AppName);
            }
        }
    }
}
