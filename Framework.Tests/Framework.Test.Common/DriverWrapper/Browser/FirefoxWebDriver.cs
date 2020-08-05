using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace Framework.Test.Common.DriverWrapper.Browser
{
    public class FirefoxWebDriver : WebDriverFactory
    {
        private static readonly object Instancelock = new object();
        private static string DriverVersion = null;

        public override IWebDriver CreateDriver(DriverProperty driverProperty)
        {
            ParameterValidator.ValidateNotNull(driverProperty, "Driver Property");

            FirefoxOptions options = new FirefoxOptions();
            if (driverProperty.Headless)
            {
                options.AddArgument("--headless");
                options.SetPreference("intl.accept_languages", "en,en_US");
            }

            if (driverProperty.DownloadLocation != null)
            {
                options.SetPreference("browser.download.folderList", 2);
                options.SetPreference("browser.download.dir", driverProperty.DownloadLocation);
            }

            if (driverProperty.Arguments != null)
            {
                options.AddArguments(driverProperty.GetArgumentsAsArray());
            }

            if (DriverVersion == null)
            {
                lock (Instancelock)
                {
                    if (DriverVersion == null)
                    {
                        if (string.IsNullOrEmpty(driverProperty.DriverVersion))
                        {
                            DriverVersion = GetStableVersion();
                        }
                        else
                        {
                            DriverVersion = driverProperty.DriverVersion;
                        }
                    }
                }
            }

            // run in private mode
            options.AddArgument("--private");

            // Setup driver binary
            try
            {
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), DriverVersion);
            }
            catch (Exception)
            {
                throw new Exception($"Cannot get Firefox driver version {DriverVersion}");
            }

            return new FirefoxDriver(options);
        }

        public override IWebDriver CreateRemoteDriver(DriverProperty driverProperty)
        {
            ParameterValidator.ValidateNotNull(driverProperty, "Driver Property");
            #pragma warning disable CS0618 // Type or member is obsolete
            DesiredCapabilities capabilities = new DesiredCapabilities(driverProperty.Capabilities);
            #pragma warning restore CS0618 // Type or member is obsolete
            return new RemoteWebDriver(new Uri(driverProperty.RemoteUrl), capabilities);
        }

        private string GetStableVersion()
        {
            return "Latest";
        }
    }
}