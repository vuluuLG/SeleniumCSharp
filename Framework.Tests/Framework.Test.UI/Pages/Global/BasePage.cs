using AventStack.ExtentReports;
using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.Common.Helper.ValidationHelper;
using static Framework.Test.Common.Helper.ScreenshotHelper;
using static Framework.Test.UI.Helper.WebElementHelper;
using static Framework.Test.UI.Helper.WaitHelper;

namespace Framework.Test.UI.Pages.Global
{
    public abstract class BasePage
    {
        private const BindingFlags methodScope = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

        #region Properties
        protected string Title { get; set; }
        protected string Url { get; set; }
        protected string[] Urls { get; set; }
        protected By RequiredElementLocator { get; set; }
        #endregion

        #region Actions
        private string ConvertControlName(string controlName)
        {
            return controlName.Replace(" ", "").Replace("/", "").Replace("-", "");
        }

        public By ControlLocator(WebControl controlType, string controlName)
        {
            ParameterValidator.ValidateNotNull(controlName, "Control Name");
            string name = ConvertControlName(controlName);
            MethodInfo method = GetType().GetMethod($"get__{controlType.ToDescription()}{name}", methodScope);
            if (method == null)
                throw new Exception($"Cannot find control locator with name = _{controlType.ToDescription()}{name}");
            return (By)method.Invoke(this, Array.Empty<object>());
        }

        public IWebElement Control(WebControl controlType, string controlName)
        {
            ParameterValidator.ValidateNotNull(controlName, "Control Name");
            string name = ConvertControlName(controlName);
            MethodInfo method = GetType().GetMethod($"get_{name}{controlType.ToString()}", methodScope);
            if (method == null)
                throw new Exception($"Cannot find control with name = {name}{controlType.ToString()}");
            return (IWebElement)method.Invoke(this, Array.Empty<object>());
        }

        public void WaitForPageLoad(int timeout = waitForPageTimeout)
        {
            try
            {
                var wait = WebDriver.Wait(timeout);
                if (Urls != null)
                {
                    wait.Until(driver => (Urls.Any(x => driver.Url.Contains(x))));
                }
                else if (!string.IsNullOrEmpty(Url))
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(Url));
                }
                else if (!string.IsNullOrEmpty(Title))
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(Title));
                }

                // Wait for required element
                if (RequiredElementLocator != null)
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(RequiredElementLocator));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Handle notification bar on IE browser
            if (WebDriver.Property.DriverType == DriverType.InternetExplorer)
            {
                try
                {
                    var action = WebDriver.Actions;
                    action.KeyDown(Keys.Alt);
                    action.SendKeys("q");
                    action.KeyUp(Keys.Alt);
                    action.Perform();
                }
                catch (Exception)
                {
                    // Skip exception
                }
            }

            if ((new ErrorPage()).IsPageDisplayed())
            {
                throw new Exception("A system error occurred");
            }
        }

        public bool IsPageDisplayed()
        {
            if (Urls != null)
            {
                return Urls.Any(x => WebDriver.Url.Contains(x));
            }
            else if (!string.IsNullOrEmpty(Url))
            {
                return WebDriver.Url.Contains(Url);
            }
            else if (!string.IsNullOrEmpty(Title))
            {
                return WebDriver.Title == Title;
            }
            else if (RequiredElementLocator != null)
            {
                return IsElementDisplayed(RequiredElementLocator);
            }
            else return false;
        }

        internal void SetPassValidation(ExtentTest test, string testInfo, string additionalInfo = null, string expectedValue = null)
        {
            if (additionalInfo != null)
            {
                testInfo += $" ({additionalInfo}) ";
            }

            if (expectedValue != null)
            {
                testInfo += ": " + expectedValue;
            }

            test.Pass(testInfo);
            AddValidation(new KeyValuePair<string, bool?>(testInfo, true));
        }

        internal void SetFailValidation(ExtentTest test, string testInfo, string additionalInfo = null, string expectedValue = null, string actualValue = null)
        {
            if (additionalInfo != null)
            {
                testInfo += $" ({additionalInfo}) ";
            }

            if (expectedValue == null)
            {
                test.Fail(testInfo, AttachScreenshot(GetCaptureScreenshot()));
                AddValidation(new KeyValuePair<string, bool?>(testInfo, false));
            }
            else
            {
                test.Fail(ReportFailureOfValidationPoints(testInfo, expectedValue, actualValue), AttachScreenshot(GetCaptureScreenshot()));
                AddValidation(new KeyValuePair<string, bool?>(ReportFailureOfValidationPoints(testInfo, expectedValue, actualValue), false));
            }
        }

        internal void SetErrorValidation(ExtentTest test, string testInfo, Exception exception, string additionalInfo = null)
        {
            if (additionalInfo != null)
            {
                testInfo += $" ({additionalInfo}) ";
            }

            test.Error(ReportExceptionInValidation(testInfo, exception), AttachScreenshot(GetCaptureScreenshot()));
            AddValidation(new KeyValuePair<string, bool?>(ReportExceptionInValidation(testInfo, exception), null));
        }

        internal void StableSelectLimitAndDeductible(By limitLocator, By deductibleLocator, string limit, string deductible)
        {
            int retries = 3;
            if (limitLocator.ToString().Contains("mat-select"))
            {
                for (int i = 0; i < retries; i++)
                {
                    StableFindElement(limitLocator).TrySelectByText(limit);
                    // Try to select drop-down item (loop until the drop-down items are populated)
                    Wait(shortTimeout);
                    if (StableFindElement(deductibleLocator).TryOpenDropDownList(mediumTimeout))
                    {
                        StableFindElement(deductibleLocator).SelectByText(deductible, false);
                        break;
                    }

                    if (i < retries - 1)
                    {
                        // Select other limit to refresh Deductible data
                        StableFindElement(limitLocator).SelectAnyExceptText(limit);
                        Wait(1);
                    }
                }
            }
            else if (limitLocator.ToString().Contains("input"))
            {
                for (int i = 0; i < retries; i++)
                {
                    StableFindElement(limitLocator).InputText(limit + Keys.Tab);
                    // Try to select drop-down item (loop until the drop-down items are populated)
                    Wait(5);
                    if (StableFindElement(deductibleLocator).TryOpenDropDownList(mediumTimeout))
                    {
                        StableFindElement(deductibleLocator).SelectByText(deductible, false);
                        break;
                    }
                    
                    if (i < retries - 1)
                    {
                        // Clear inputted limit to refresh Deductible data
                        StableFindElement(limitLocator).ClearByBackSpace();
                        Wait(1);
                    }
                }
            }
        }
        #endregion
    }
}