using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Framework.Test.Common.DriverWrapper
{
    public static class WebDriver
    {
        [ThreadStatic]
        private static DriverProperty defaultDriverProperty;
        [ThreadStatic]
        private static string defaultKey;
        [ThreadStatic]
        private static string currentKey;
        [ThreadStatic]
        private static Dictionary<string, IWebDriver> driverList;
        [ThreadStatic]
        private static Dictionary<string, DriverProperty> propertyList;

        public static IWebDriver Driver
        {
            get
            {
                try
                {
                    return driverList[currentKey];
                }
                catch { return null; }
            }
        }

        public static DriverProperty Property
        {
            get { return propertyList[currentKey]; }
        }

        public static DriverProperty DefaultProperty
        {
            get { return defaultDriverProperty; }
        }

        public static string Title
        {
            get { return Driver.Title; }
        }

        public static string Url
        {
            get { return Driver.Url; }
        }

        public static Actions Actions
        {
            get { return new Actions(Driver); }
        }

        public static IOptions Manage
        {
            get { return Driver.Manage(); }
        }

        public static INavigation Navigate
        {
            get { return Driver.Navigate(); }
        }

        public static ITargetLocator SwitchTo
        {
            get { return Driver.SwitchTo(); }
        }

        public static IAlert Alert
        {
            get { return Driver.SwitchTo().Alert(); }
        }

        public static IJavaScriptExecutor JsExecutor
        {
            get { return (IJavaScriptExecutor)Driver; }
        }

        public static void InitDriverManager(DriverProperty defaultProperty)
        {
            driverList = new Dictionary<string, IWebDriver>();
            propertyList = new Dictionary<string, DriverProperty>();

            if (defaultProperty != null)
            {
                defaultKey = defaultProperty.DriverType.ToDescription() + "-1";
                defaultDriverProperty = defaultProperty;
            }
        }

        public static void AddNewDriver(DriverProperty property)
        {
            ParameterValidator.ValidateNotNull(property, "Driver Property");
            string key;

            // Get target driver from factory
            IWebDriver webDriver = WebDriverFactory.GetWebDriver(property);

            //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            key = property.DriverType.ToDescription() + "-" + GetNextPlatformNumber(property.DriverType.ToDescription());
            driverList.Add(key, webDriver);
            propertyList.Add(key, property);
            currentKey = key;
        }

        private static int GetNextPlatformNumber(string plaform)
        {
            int number = 1;
            foreach (var item in driverList.Keys)
            {
                int lastIndex = item.LastIndexOf("-");
                if (item.Substring(0, lastIndex) == plaform)
                {
                    number++;
                }
            }
            return number;
        }

        public static DriverProperty GetTargetProperty(string platform, int index = 1)
        {
            try
            {
                return propertyList[platform + "-" + index];
            }
            catch
            {
                return null;
            }
        }

        public static void SwitchToDefaultDriver()
        {
            currentKey = defaultKey;
        }

        public static void SwitchToTargetDriver(string platform, int index = 1)
        {
            currentKey = platform + "-" + index;
        }

        public static void Maximize()
        {
            Manage.Window.Maximize();
        }

        public static void Minimize()
        {
            Manage.Window.Minimize();
        }

        public static void GoToUrl(string url)
        {
            Navigate.GoToUrl(new Uri(url));
        }

        public static void SwitchToIframe(IWebElement iframe)
        {
            SwitchTo.Frame(iframe);
        }

        public static void SwitchToPrevious()
        {
            SwitchTo.ParentFrame();
        }

        public static void SwitchToDefaultWindow()
        {
            SwitchTo.Window(Driver.WindowHandles.First());
        }

        public static void SwitchToLastWindow()
        {
            SwitchTo.Window(Driver.WindowHandles.Last());
        }

        public static void Sleep(int second)
        {
            System.Threading.Thread.Sleep(second * 1000);
        }

        public static WebDriverWait Wait(int second = 30)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(second));
        }

        public static object ExecuteScript(string code, params object[] args)
        {
            return JsExecutor.ExecuteScript(code, args);
        }

        public static Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)Driver).GetScreenshot();
        }

        public static void Close()
        {
            Driver.Close();
        }

        public static void Quit()
        {
            Driver.Quit();
            driverList.Remove(currentKey);
            propertyList.Remove(currentKey);
            if (driverList.Count > 0)
            {
                if (!driverList.ContainsKey(defaultKey))
                {
                    defaultKey = driverList.First().Key;
                }

                currentKey = driverList.Last().Key;
            }
        }

        public static void QuitAllDriver()
        {
            foreach (var driver in driverList.Values)
            {
                driver.Quit();
            }
            driverList.Clear();
            propertyList.Clear();
        }

        public static IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }
    }
}