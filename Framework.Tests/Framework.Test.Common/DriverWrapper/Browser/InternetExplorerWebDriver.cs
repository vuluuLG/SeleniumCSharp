using Framework.Test.Common.Helper;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Net;
using System.Xml;
using WebDriverManager.DriverConfigs;

namespace Framework.Test.Common.DriverWrapper.Browser
{
    public class InternetExplorerWebDriver : WebDriverFactory
    {
        private static readonly object Instancelock = new object();
        private static string DriverVersion = null;

        public override IWebDriver CreateDriver(DriverProperty driverProperty)
        {
            ParameterValidator.ValidateNotNull(driverProperty, "Driver Property");

            InternetExplorerOptions ieOptions = new InternetExplorerOptions
            {
                EnableNativeEvents = true,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                EnablePersistentHover = true,
                RequireWindowFocus = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
                EnsureCleanSession = true
            };

            ieOptions.AddAdditionalCapability("disable-popup-blocking", true);
            ieOptions.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);

            if (driverProperty.DownloadLocation != null)
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true);
                if (myKey != null)
                {
                    if (myKey.GetValue("Default Download Directory") == null || myKey.GetValue("Default Download Directory").ToString() != driverProperty.DownloadLocation)
                    {
                        myKey.SetValue("Default Download Directory", driverProperty.DownloadLocation);
                    }
                    myKey.Close();
                }

                myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\3", true);
                if (myKey != null)
                {
                    if (myKey.GetValue("1803") == null || myKey.GetValue("1803").ToString() != "0")
                    {
                        myKey.SetValue("1803", 0);
                    }
                    myKey.Close();
                }
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

            // Setup driver binary
            try
            {
                new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerExConfig(), DriverVersion);
            }
            catch (Exception)
            {
                throw new Exception($"Cannot get Internet Explorer driver version {DriverVersion}");
            }

            return new InternetExplorerDriver(ieOptions);
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

    public class InternetExplorerExConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "InternetExplorer";
        }

        public virtual string GetUrl32()
        {
            return GetUrl();
        }

        public virtual string GetUrl64()
        {
            //return "http://selenium-release.storage.googleapis.com/<release>/IEDriverServer_x64_<version>.zip";
            return GetUrl();
        }

        private string GetUrl()
        {
            return "http://selenium-release.storage.googleapis.com/<release>/IEDriverServer_Win32_<version>.zip";
        }

        public virtual string GetBinaryName()
        {
            return "IEDriverServer.exe";
        }

        public virtual string GetLatestVersion()
        {
            // default version
            string version = "3.150.1";
            string content = null;
            using (var client = new WebClient())
            {
                content = client.DownloadString("http://selenium-release.storage.googleapis.com");
            }

            if (content != null)
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(content);
                XmlNamespaceManager npm = new XmlNamespaceManager(data.NameTable);
                npm.AddNamespace("x", "http://doc.s3.amazonaws.com/2006-03-01");
                var latestVersionKey = data.SelectSingleNode("//x:Contents[contains(x:Key, 'IEDriverServer') and not(x:Generation < //x:Contents[contains(x:Key, 'IEDriverServer')]/x:Generation)]/x:Key", npm);
                if (latestVersionKey != null)
                {
                    string key = latestVersionKey.InnerText;
                    version = key.Substring(key.LastIndexOf("_") + 1, key.LastIndexOf(".") - key.LastIndexOf("_") - 1);
                }
            }

            return version;
        }
    }
}