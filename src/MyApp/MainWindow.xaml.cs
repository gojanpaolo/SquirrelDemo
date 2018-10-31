using Squirrel;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

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
                try
                {
                    using (var taskMgr = AppUpdateManager.GitHubUpdateManager())
                    using (var mgr = await taskMgr)
                    {
                        await mgr.UpdateApp();
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
    }
}
