using Squirrel;
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
                            mgr.CreateRunAtWindowsStartupRegistry();
                        },
                        onAppUninstall: v =>
                        {
                            mgr.RemoveShortcutForThisExe();
                            mgr.RemoveRunAtWindowsStartupRegistry();
                        });
                }
            });
        }
    }
}
