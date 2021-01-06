using Framework.Test.Common.Helper;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Net;
using WebDriverManager.DriverConfigs.Impl;

namespace Framework.Test.Common.DriverWrapper.Browser
{
    public class ChromeWebDriver : WebDriverFactory
    {
        private static readonly object Instancelock = new object();
        private static string DriverVersion = null;

        public override IWebDriver CreateDriver(DriverProperty driverProperty)
        {
            ParameterValidator.ValidateNotNull(driverProperty, "Driver Property");

            ChromeOptions options = new ChromeOptions();
            if (driverProperty.Headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddUserProfilePreference("disable-popup-blocking", "true");
                options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
            }

            if (driverProperty.DownloadLocation != null)
            {
                options.AddUserProfilePreference("download.default_directory", driverProperty.DownloadLocation);
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

            // Use custom Chrome binary (81)
            // https://chromium.cypress.io/win64/stable/81.0.4044.92
            // options.BinaryLocation = "C:\\Users\\sbaltuonis\\Downloads\\chrome-win\\chrome.exe";

            // Run in private mode
            options.AddArgument("--incognito");

            // Enable mobile emulation
            if (!string.IsNullOrEmpty(driverProperty.DeviceName))
            {
                options.EnableMobileEmulation(driverProperty.DeviceName);
            }
            else if (!string.IsNullOrEmpty(driverProperty.CustomUserAgent))
            {
                if (driverProperty.CustomWidth <= 0
                    || driverProperty.CustomHeight <= 0
                    || driverProperty.CustomPixelRatio <= 0)
                    throw new Exception("Custom device width, height and pixel ratio must be greater than 0");

                ChromeMobileEmulationDeviceSettings emulationSettings = new ChromeMobileEmulationDeviceSettings
                {
                    UserAgent = driverProperty.CustomUserAgent,
                    Width = driverProperty.CustomWidth,
                    Height = driverProperty.CustomHeight,
                    PixelRatio = driverProperty.CustomPixelRatio
                };

                options.EnableMobileEmulation(emulationSettings);
            }

            // Setup driver binary
            try
            {
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), DriverVersion);
            }
            catch (Exception)
            {
                throw new Exception($"Cannot get Chrome driver version {DriverVersion}");
            }

            #pragma warning disable CA2000 // Dispose objects before losing scope
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(180));
            #pragma warning restore CA2000 // Dispose objects before losing scope
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
            // Access Registry Editor
            RegistryKey keys = null;
            string chromePath = null;
            string[] browserRegistries =
            {
                @"SOFTWARE\WOW6432Node\Clients\StartMenuInternet" ,
                @"SOFTWARE\Clients\StartMenuInternet"
            };
            // Get chrom path from current user
            foreach (var registry in browserRegistries)
            {
                keys = Registry.CurrentUser.OpenSubKey(registry);
                // Get path of chrome browser
                if (keys != null)
                {
                    chromePath = GetBrowserPath(keys);
                    keys.Close();
                    if (chromePath != null)
                        break;
                }
            }
            // Get chrom path from local machine if needed
            if (chromePath == null)
            {
                foreach (var registry in browserRegistries)
                {
                    keys = Registry.LocalMachine.OpenSubKey(registry);
                    // Get path of chrome browser
                    if (keys != null)
                    {
                        chromePath = GetBrowserPath(keys);
                        keys.Close();
                        if (chromePath != null)
                            break;
                    }
                }
            }

            if (chromePath != null)
            {
                // Get main version of chrome
                string version = FileVersionInfo.GetVersionInfo(chromePath).FileVersion;
                string mainVersion = version.Substring(0, version.IndexOf("."));

                //Get latest release version
                using (var client = new WebClient())
                {
                    return client.DownloadString($"https://chromedriver.storage.googleapis.com/LATEST_RELEASE_{mainVersion}");
                }
            }
            else return "Latest";
        }

        private string GetBrowserPath(RegistryKey browserKeys)
        {
            ParameterValidator.ValidateNotNull(browserKeys, "Browser Keys");
            string path = null;
            string[] browserNames = browserKeys.GetSubKeyNames();
            foreach (var item in browserNames)
            {
                if (item.Contains("Chrome"))
                {
                    path = browserKeys.OpenSubKey($"{item}\\shell\\open\\command").GetValue(null).ToString().Replace("\"", "");
                    break;
                }
            }
            return path;
        }
    }
}