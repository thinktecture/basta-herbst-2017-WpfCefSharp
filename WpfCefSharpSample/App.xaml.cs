using System.Windows;
using WpfCefSharpSample.Cef;
using WpfCefSharpSample.Model;

namespace WpfCefSharpSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs args)
        {
            CefManager.Initialize();

            base.OnStartup(args);

            var wnd = new MainWindow();

            Current.MainWindow = wnd;

            wnd.DataContext = new MainWindowViewModel()
            {
                WebBrowser = wnd.Browser,
                BrowserAddress = "",
            };

            Current.MainWindow.Show();
            Current.MainWindow.Closed += (s, e) => Current.Shutdown(0);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            CefManager.Shutdown();

            base.OnExit(e);
        }
    }
}
