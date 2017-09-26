using System;
using System.IO;
using CefSharp;
using WpfCefSharpSample.Properties;

namespace WpfCefSharpSample.Cef
{
    internal class DemoSchemeHandlerFactory : ISchemeHandlerFactory, IResourceHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            //You can match the scheme/host if required, we don't need that in this example, so keeping it simple.
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath;
            var extension = Path.GetExtension(fileName);

            //Compare scheme and host
            if (String.Equals(uri.Scheme, "local", StringComparison.OrdinalIgnoreCase)
                && String.Equals(uri.Host, "resource", StringComparison.OrdinalIgnoreCase))
            {

                if (fileName == "LocalIndex.html")
                {
                    //For css/js/etc it's important to specify a mime/type, here we use the file extension to perform a lookup
                    //there are overloads where you can specify more options including Encoding, mimeType
                    return ResourceHandler.FromString(Resources.LocalIndex, extension);
                }
            }

            return null;
        }

        public IResourceHandler GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath;
            var extension = Path.GetExtension(fileName);

            //Compare scheme and host
            if (String.Equals(uri.Scheme, "local", StringComparison.OrdinalIgnoreCase)
                && String.Equals(uri.Host, "resource", StringComparison.OrdinalIgnoreCase))
            {
                if (fileName == "LocalIndex.html")
                {
                    //For css/js/etc it's important to specify a mime/type, here we use the file extension to perform a lookup
                    //there are overloads where you can specify more options including Encoding, mimeType
                    return ResourceHandler.FromString(Resources.LocalIndex, extension);
                }
            }

            return null;
        }

        public bool HasHandlers => true;
    }
}