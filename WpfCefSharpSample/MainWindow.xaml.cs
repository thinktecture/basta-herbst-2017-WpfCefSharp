using System.Diagnostics;
using System.Windows;
using WpfCefSharpSample.BrowserInterface;

namespace WpfCefSharpSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var hostInterface = new HostInterface(this);
            Browser.RegisterAsyncJsObject("hostInterface", hostInterface);

            Browser.ConsoleMessage += (s, e) =>
            {
                Debug.WriteLine($"BROWSERCONSOLE: {e.Source}:{e.Line} - {e.Message}");
            };
        }

    }
}
