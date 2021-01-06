using Framework.Test.Common.DriverWrapper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.Common.Helper
{
    public static class ScreenshotHelper
    {
        public const string defaultCaptureLocation = "c:\\temp\\TestResults\\";

        public static string GetCaptureScreenshot(IWebElement highLightElement = null)
        {
            TakeScreenshot(out string screenshotFilePath, highLightElement);
            return screenshotFilePath;
        }

        public static void TakeScreenshot(out string filePath, IWebElement highLightElement = null, string captureLocation = defaultCaptureLocation)
        {
            string timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");

            filePath = captureLocation + "ErrorCapture" + timeStamp + ".png";

            if (highLightElement != null)
            {
                WebDriver.ExecuteScript("argument[0].style.border='3px solid red'", highLightElement);
            }

            try
            {
                Screenshot screenshot = WebDriver.GetScreenshot();
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            }
            catch (UnhandledAlertException)
            {
                IAlert alert = WebDriver.SwitchTo.Alert();
                alert.Dismiss();
                Screenshot screenshot = WebDriver.GetScreenshot();
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine("TakeScreenshot encountered an error. " + e.Message);
            }
        }

        public static string ImageToBase64(string imagePath)
        {
            string base64String;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
