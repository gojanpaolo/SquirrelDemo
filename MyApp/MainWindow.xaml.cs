using Squirrel;
using System.Reflection;
using System.Windows;

namespace MyApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var mgr = new UpdateManager(@"C:\git\SquirrelDemo\Releases"))
            {
                mgr.UpdateApp();
            }

            var assembly = Assembly.GetExecutingAssembly();
            location.Text = assembly.Location;
            version.Text = assembly.GetName().Version.ToString(3);
        }
    }
}
