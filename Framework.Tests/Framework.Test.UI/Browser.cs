using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;

namespace Framework.Test.UI
{
    public static class Browser
    {
        public static string CurrentTitle
        {
            get { return WebDriver.Title; }
        }

        public static string CurrentUrl
        {
            get { return WebDriver.Url; }
        }

        public static void Open(DriverType browser, string url, bool headless, string fileDownloadLocation, string argument = null)
        {
            DriverProperty prop = new DriverProperty
            {
                DriverType = browser,
                Headless = headless,
                DownloadLocation = fileDownloadLocation,
                Arguments = argument
            };

            WebDriver.AddNewDriver(prop);
            WebDriver.GoToUrl(url);
            MaximizeWindow();
        }

        public static void Open(DriverType browser, string url)
        {
            DriverProperty prop = WebDriver.DefaultProperty;
            if (prop.DriverType != browser)
            {
                prop = new DriverProperty
                {
                    DriverType = browser
                };
            }
            WebDriver.AddNewDriver(prop);
            WebDriver.GoToUrl(url);
            MaximizeWindow();
        }

        public static void Open(string browser,string url, bool headless, string fileDownloadLocation = null, string argument = null)
        {
            DriverType browserType = browser.ToEnumValue<DriverType>();
            Open(browserType, url, headless, fileDownloadLocation, argument);
        }

        public static void Open(string browser, string url)
        {
            DriverType browserType = browser.ToEnumValue<DriverType>();
            Open(browserType, url);
        }

        public static void Wait(int second = 30)
        {
            WebDriver.Sleep(second);
        }

        public static void Close()
        {
            WebDriver.Close();
        }

        public static void Quit()
        {
            WebDriver.Quit();
        }

        public static void QuitAll()
        {
            WebDriver.QuitAllDriver();
        }

        public static void Navigate(string url)
        {
            WebDriver.Driver.Url = url;
        }

        internal static void MinimizeWindow()
        {
            WebDriver.Minimize();
        }

        internal static void MaximizeWindow()
        {
            WebDriver.Maximize();
        }

        public static void UseDefaultBrowser()
        {
            WebDriver.SwitchToDefaultDriver();
        }

        public static void UseBrowser(string browserName, int browserIndex = 1)
        {
            WebDriver.SwitchToTargetDriver(browserName, browserIndex);
        }
    }
}
