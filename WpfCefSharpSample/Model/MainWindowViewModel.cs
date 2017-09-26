using System;
using System.ComponentModel;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;

namespace WpfCefSharpSample.Model
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ShowDevToolsCommand { get; private set; }
        public ICommand CloseDevToolsCommand { get; private set; }
        public ICommand LoadLocalFileCommand { get; private set; }
        public ICommand LoadExternalWebsiteCommand { get; private set; }
        public ICommand LoadPokedexCommand { get; private set; }
        public ICommand InjectSomeJavaScriptCommand { get; private set; }

        public MainWindowViewModel()
        {
            ShowDevToolsCommand = new CommandHandler(() => _webBrowser?.ShowDevTools(), true);
            CloseDevToolsCommand = new CommandHandler(() => _webBrowser?.CloseDevTools(), true);
            LoadPokedexCommand = new CommandHandler(() => BrowserAddress = "http://localhost:8000", true);
            LoadExternalWebsiteCommand = new CommandHandler(() => BrowserAddress = "http://thinktecture.com", true);
            InjectSomeJavaScriptCommand = new CommandHandler(() => WebBrowser?.ExecuteScriptAsync($"alert('Es ist {DateTime.Now.ToShortTimeString()} Uhr.');"), true);

            var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "index.html").AbsoluteUri;
            LoadLocalFileCommand = new CommandHandler(() => BrowserAddress = uri, true);
        }

        private string _browserAddress;
        public string BrowserAddress
        {
            get { return _browserAddress; }
            set { _browserAddress = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BrowserAddress))); }
        }

        private IWpfWebBrowser _webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return _webBrowser; }
            set { _webBrowser = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WebBrowser))); }
        }
    }
}