using Microsoft.Win32;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

            Task.Run(async () =>
            {
                using (var taskMgr = AppUpdateManager.GitHubUpdateManager())
                using (var mgr = await taskMgr)
                {
                    SquirrelAwareApp.HandleEvents(
                        onInitialInstall: v => 
                        {
                            mgr.CreateShortcutForThisExe();
                            mgr.CreateRunAtStartupRegistry();
                        },
                        onAppUninstall: v =>
                        {
                            mgr.RemoveShortcutForThisExe();
                            mgr.RemoveRunAtStartupRegistry();
                        });
                }
            }).Wait();
        }
    }
}
