using Squirrel;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/gojanpaolo/SquirrelDemo"))
                using (var result = await mgr)
                {
                    SquirrelAwareApp.HandleEvents(
                      onInitialInstall: v => RegisterAppToRunOnStartup());
                }
            });

            Task.Run(async () =>
            {
                try
                {
                    using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/gojanpaolo/SquirrelDemo"))
                    using (var result = await mgr)
                    {
                        await result.UpdateApp();
                    }
                }
                catch (Exception e)
                {
                    Dispatcher.Invoke(() =>
                    {
                        exception.Text = e.ToString();
                    });
                }
            });
            var assembly = Assembly.GetExecutingAssembly();
            location.Text = assembly.Location;
            version.Text = assembly.GetName().Version.ToString(3);
        }

        private void RegisterAppToRunOnStartup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var startupRegistryKey = Registry.CurrentUser.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                startupRegistryKey.SetValue(assembly.GetName().Name, assembly.Location);
        }
    }
}
