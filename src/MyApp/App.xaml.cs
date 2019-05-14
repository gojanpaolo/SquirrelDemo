using Squirrel;
using System;
using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //using (var mgr = new UpdateManager(default))
            //{
            //    SquirrelAwareApp.HandleEvents(
            //        onInitialInstall: v =>
            //        {
            //            mgr.CreateShortcutForThisExe();
            //            mgr.CreateRunAtWindowsStartupRegistry();
            //        },
            //        onAppUninstall: v =>
            //        {
            //            mgr.RemoveShortcutForThisExe();
            //            mgr.RemoveRunAtWindowsStartupRegistry();
            //        });
            //}

            var rootDirectory =
                $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\";
            var updateManager = new UpdateManager(@"C:\git\SquirrelDemo\src\Releases", "MyApp", rootDirectory);
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: v =>
                {
                    updateManager.CreateShortcutForThisExe();
                    updateManager.CreateUninstallerRegistryEntry();
                },
                onAppUpdate: v =>
                {
                    //updateManager.RemoveShortcutForThisExe();
                    updateManager.CreateShortcutForThisExe();
                    //updateManager.RemoveUninstallerRegistryEntry();
                    updateManager.CreateUninstallerRegistryEntry();
                    //UpdateManager.RestartApp();
                },
                onAppUninstall: v =>
                {
                    MessageBox.Show("Uninstall started");
                    updateManager.RemoveShortcutForThisExe();
                    updateManager.RemoveUninstallerRegistryEntry();
                });

        }
    }
}
