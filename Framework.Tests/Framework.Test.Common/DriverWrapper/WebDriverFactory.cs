using OpenQA.Selenium;
using System;
using System.Reflection;

namespace Framework.Test.Common.DriverWrapper
{
    public abstract class WebDriverFactory
    {
        public static IWebDriver GetWebDriver(DriverProperty driverProperty)
        {
            if (driverProperty != null)
            {
                string methodName = "CreateDriver";
                if (!string.IsNullOrEmpty(driverProperty.RemoteUrl))
                {
                    methodName = "CreateRemoteDriver";
                }

                Type browserType = Type.GetType($"Framework.Test.Common.DriverWrapper.Browser.{driverProperty.DriverType.ToString()}WebDriver");
                object obj = Activator.CreateInstance(browserType);

                return (IWebDriver)browserType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, new object[] { driverProperty });
            }
            return null;
        }

        public abstract IWebDriver CreateDriver(DriverProperty driverProperty);

        public abstract IWebDriver CreateRemoteDriver(DriverProperty driverProperty);
    }
}