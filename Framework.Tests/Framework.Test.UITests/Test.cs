using Applitools;
using Applitools.Selenium;
using Framework.Test.Common.DriverWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.UITests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Demo_Applitool()
        {
            EyesRunner runner = new ClassicRunner();
            Eyes eye = new Eyes(runner);

            var suiteConfig = new Applitools.Selenium.Configuration();
            suiteConfig
               // Test suite configurations
               .SetApiKey("KgPPOMrJBHbsgzaZ1oJ5A18sNMPiZEXpiRSGqOv6qmk110")
               .SetBatch(new BatchInfo("Demo Regression 001"));

            eye.SetConfiguration(suiteConfig);

            DriverProperty property = new DriverProperty();
            property.DriverType = DriverType.Chrome;
            WebDriver.InitDriverManager(property);
            WebDriver.AddNewDriver(property);
            eye.Open(WebDriver.Driver, "DEMO", "Demo Test 001");
            WebDriver.GoToUrl("http://google.com.vn");

            WebDriver.Sleep(5);

            eye.CheckWindow("Google home page 1");
            eye.Check(Target.Window().Layout(By.XPath("")).Region(By.XPath("")));

            WebDriver.Sleep(5);

            eye.CheckWindow("Google home page 2");

            eye.CloseAsync();

            WebDriver.QuitAllDriver();

            runner.GetAllTestResults(true);
        }

        [TestMethod]
        public void Demo_Emulator()
        {
            DriverProperty property = new DriverProperty();
            property.DriverType = DriverType.Chrome;

            WebDriver.InitDriverManager(property);
            WebDriver.AddNewDriver(property);
            WebDriver.GoToUrl("http://google.com.vn");

            WebDriver.QuitAllDriver();
        }
    }
}
