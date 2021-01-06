using Framework.Test.Common.DriverWrapper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Framework.Test.UI.Helper
{
    public static class WaitHelper
    {
        // Timeout constants in seconds 
        public const int longTimeout = 30;
        public const int mediumTimeout = 15;
        public const int shortTimeout = 5;
        public const int waitForElementTimeout = 60;
        public const int waitForPageTimeout = 120;
        public const int waitForDownloadTimeout = 180;

        public static void Wait(int seconds)
        {
            WebDriver.Sleep(seconds);
        }

        public static T WaitUntil<T>(Func<IWebDriver, T> condition, int timeout = waitForElementTimeout)
        {
            var wait = WebDriver.Wait(timeout);
            var ignoredExceptions = new List<Type>() { typeof(StaleElementReferenceException),
                                                       typeof(WebDriverTimeoutException),
                                                       typeof(NoSuchElementException) };

            wait.IgnoreExceptionTypes(ignoredExceptions.ToArray());
            return wait.Until(condition);
        }

        public static void WaitForElementExists(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} does not exist within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementNotExists(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} still exists within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementVisible(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} is not visible within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementInvisible(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} is not invisible within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForUrlChanged(string currentUrl, int timeout = waitForPageTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.Url != currentUrl);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Url {" + currentUrl + "} does not change within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementClickable(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} is not clickable within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementEnabled(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).Enabled);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} is not enabled within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementDisabled(By elementLocator, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).Enabled == false);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} is not disabled within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementTextChanged(By elementLocator, string text, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementWithText(elementLocator, text));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Text of element {" + elementLocator + "} does not change within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementValueChanged(By elementLocator, string currentValue, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).GetAttribute("value") != currentValue);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Value of element {" + elementLocator + "} does not change within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementValueToBe(By elementLocator, string expectedValue, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).GetAttribute("value") == expectedValue);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Value of element {" + elementLocator + "} is not equal to {" + expectedValue + "} within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementCSSAttribute(By elementLocator, string cssAttribute, string cssAttributeValue, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).GetCssValue(cssAttribute).Contains(cssAttributeValue));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Css attribute {" + cssAttribute + "} of element {" + elementLocator + "} is not equal to {" + cssAttributeValue + "} within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForElementAttribute(By elementLocator, string attribute, string attributeValue, int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(driver => driver.FindElement(elementLocator).GetAttribute(attribute).Contains(attributeValue));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Element {" + elementLocator + "} does not contain attribute {\"" + attribute + "\"=\"" + attributeValue + "\"} within " + timeout + " seconds");
                    throw;
                }
            }
        }

        public static void WaitForAlert(int timeout = waitForElementTimeout, bool throwException = true)
        {
            var wait = WebDriver.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    if (ex is WebDriverTimeoutException)
                        throw new WebDriverTimeoutException("Alert does not exist within " + timeout + " seconds");
                    throw;
                }
            }
        }
    }
}
