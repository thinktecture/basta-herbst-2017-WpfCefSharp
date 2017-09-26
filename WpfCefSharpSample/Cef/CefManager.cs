using System;
using CefSharp;

namespace WpfCefSharpSample.Cef
{
    public class CefManager
    {
        public static void Initialize()
        {
            var settings = new CefSettings()
            {
                PackLoadingDisabled = false,
            };

            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            // settings.SetOffScreenRenderingBestPerformanceArgs();

            if (!CefSharp.Cef.Initialize(settings, shutdownOnProcessExit: false, performDependencyCheck: false))
            {
                throw new Exception("Unable to initialize Chromium Embedded Framework (Cef)");
            }
        }

        public static void Shutdown()
        {
            CefSharp.Cef.Shutdown();
        }
    }
}
