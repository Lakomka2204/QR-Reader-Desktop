using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QR_Reader_Desktop
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            List<string> RequiredFiles = new List<string>()
                {
                "zxing.dll",
                "zxing.presentation.dll",
                "AForge.dll",
                "AForge.Video.DirectShow.dll",
                "AForge.Video.dll",
                "QRCoder.dll"
            };
            RequiredFiles.RemoveAll(File.Exists);
            if (RequiredFiles.Any())
            {
                MessageBox.Show($"Missing files\r\n{string.Join("\r\n",RequiredFiles)}",Settings.AppName,MessageBoxButtons.OK,MessageBoxIcon.Error);
                Environment.Exit(-1);
                return;
            }
            RequiredFiles = null;
            GC.Collect();
            Application.Run(new Main());
        }
    }
}
