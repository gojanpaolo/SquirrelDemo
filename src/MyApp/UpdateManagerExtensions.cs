using Microsoft.Win32;
using Squirrel;
using System.IO;

namespace MyApp
{
    public static class UpdateManagerExtensions
    {
        private static RegistryKey OpenRunAtStartupRegistryKey() =>
            Registry.CurrentUser.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static void CreateRunAtStartupRegistry(this UpdateManager updateManager)
        {
            using (var startupRegistryKey = OpenRunAtStartupRegistryKey())
                startupRegistryKey.SetValue(
                    updateManager.ApplicationName, 
                    Path.Combine(updateManager.RootAppDirectory, $"{updateManager.ApplicationName}.exe"));
        }

        public static void RemoveRunAtStartupRegistry(this UpdateManager updateManager)
        {
            using (var startupRegistryKey = OpenRunAtStartupRegistryKey())
                startupRegistryKey.DeleteValue(updateManager.ApplicationName);
        }
    }
}
