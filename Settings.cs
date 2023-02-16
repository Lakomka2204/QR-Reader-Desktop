using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Documents;

namespace QR_Reader_Desktop
{
    internal static class Settings
    {
        public static int LightColor { get; set; } = Color.White.ToArgb();
        public static int DarkColor { get; set; } = Color.Black.ToArgb();
        public static int IconBorderColor { get; set; } = Color.White.ToArgb();
        public static string IconPath { get; set; } = "";
        public static int IconSize { get; set; } = 15;
        public static int BorderRadius { get; set; } = 0;
        public static int PPU { get; set; } = 20;
        public static bool DrawQuietZones { get; set; } = true;
        static readonly string AppName = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).FileDescription;
        public static void Load()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{AppName}"))
            {
                if (key == null)
                {
                    Save();
                    return;
                }
                foreach (var prop in typeof(Settings).GetProperties())
                        prop.SetValue(null, Convert.ChangeType(key.GetValue(prop.Name), prop.PropertyType));
            }
        }

        public static void Save()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{AppName}", true)
                ?? Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{AppName}", true))
            {
                foreach (var prop in typeof(Settings).GetProperties())
                        key.SetValue(prop.Name, prop.GetValue(null));
            }
        }
    }
}
