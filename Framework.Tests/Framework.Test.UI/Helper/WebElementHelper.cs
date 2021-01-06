using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using static Framework.Test.UI.Helper.WaitHelper;

namespace Framework.Test.UI.Helper
{
    public static class WebElementHelper
    {
        internal const int maxStaleElementRetries = 3;

        #region Common Locators
        // Drop-down
        internal static By optionsLocator = By.XPath("//mat-option");
        internal static By targetOptionLocator(string text) => By.XPath($"//mat-option[.//*[normalize-space(text())='{text}']]");
        internal static By targetExceptedOptionLocator(string exceptedText) => By.XPath($"//mat-option[not(.//*[normalize-space(text())='{exceptedText}'])]");
        // Date-picker
        internal static By calendarLocator = By.XPath("//mat-calendar");
        internal static By calendarPeriodItemLocator = By.XPath("//button[contains(@class,'at-calendar-period-button')]/span");
        internal static By previousCalendarLocator = By.XPath("//button[contains(@class,'mat-calendar-previous-button')]");
        internal static By nextCalendarLocator = By.XPath("//button[contains(@class,'mat-calendar-next-button')]");
        internal static By targetCalendarItemLocator(string item) => By.XPath($"//div[contains(@class,'mat-calendar-body-cell-content') and normalize-space(text())='{item}']");
        #endregion

        #region Common Elements
        // Drop-down
        internal static IWebElement TargetOption(string text) => StableFindElement(targetOptionLocator(text));
        internal static IWebElement TargetExceptedOption(string exceptedText) => StableFindElement(targetExceptedOptionLocator(exceptedText));
        // Date-picker
        internal static IWebElement TargetCalendarItem(string item) => StableFindElement(targetCalendarItemLocator(item));
        internal static IWebElement CalendarPeriodItem => StableFindElement(calendarPeriodItemLocator);
        #endregion

        #region Finder
        public static IWebElement StableFindElement(By elementLocator, int timeout = waitForElementTimeout)
        {
            IWebElement element = null;
            var wait = WebDriver.Wait(timeout);
            element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
            try
            {
                // trigger stale element checking
                var checkFlag = element.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                element = WebDriver.FindElement(elementLocator);
            }

            return element;
        }

        public static ReadOnlyCollection<IWebElement> StableFindElements(By elementLocator, int timeout = waitForElementTimeout)
        {
            var wait = WebDriver.Wait(timeout);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(elementLocator));
        }

        public static HtmlNode StableFindElementFromPageSource(By elementLocator, int timeout = waitForElementTimeout)
        {
            ParameterValidator.ValidateNotNull(elementLocator, "Element Locator");
            HtmlNode element = null;
            string locator = elementLocator.ToString();
            if (!locator.Contains("By.XPath"))
            {
                throw new Exception("Only support XPath locator");
            }
            locator = locator.Replace("By.XPath: ", "");
            HtmlDocument document = new HtmlDocument();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    document.LoadHtml(WebDriver.Driver.PageSource);
                    element = document.DocumentNode.SelectSingleNode(locator);
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch { }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);
            stopwatch.Stop();

            throw new NoSuchElementException("Unable to locate element: {\"method\":\"xpath\",\"selector\":\"" + locator + "\"} after " + timeout + " seconds");
        }

        public static HtmlNodeCollection StableFindElementsFromPageSource(By elementLocator, int timeout = waitForElementTimeout)
        {
            ParameterValidator.ValidateNotNull(elementLocator, "Element Locator");
            HtmlNodeCollection elements = null;
            string locator = elementLocator.ToString();
            if (!locator.Contains("By.XPath"))
            {
                throw new Exception("Only support XPath locator");
            }
            locator = locator.Replace("By.XPath: ", "");
            HtmlDocument document = new HtmlDocument();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    document.LoadHtml(WebDriver.Driver.PageSource);
                    elements = document.DocumentNode.SelectNodes(locator);
                    if (elements != null)
                    {
                        return elements;
                    }
                }
                catch { }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);
            stopwatch.Stop();

            throw new NoSuchElementException("Unable to locate elements: {\"method\":\"xpath\",\"selector\":\"" + locator + "\"} after " + timeout + " seconds");
        }

        public static IWebElement StableFindChildElement(this IWebElement element, By by, int timeout = waitForElementTimeout)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            IWebElement ele = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = WebDriver.Wait(shortTimeout);
                    wait.Until(d => element.FindElement(by).Displayed);
                    ele = element.FindElement(by);
                    break;
                }
                catch (Exception)
                {
                    //skip remain exceptions
                }

            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return ele;
        }

        public static ReadOnlyCollection<IWebElement> StableFindChildElements(this IWebElement element, By by, int timeout = waitForElementTimeout)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            ReadOnlyCollection<IWebElement> eles = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = WebDriver.Wait(shortTimeout);
                    wait.Until(d => element.FindElements(by).Count > 0);
                    eles = element.FindElements(by);
                    break;
                }
                catch
                {
                    //skip remain exceptions
                }

            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return eles;
        }
        #endregion

        #region Retrieve information
        public static int GetElementNumber(By elementLocator, int timeout = waitForElementTimeout)
        {
            try
            {
                var wait = WebDriver.Wait(timeout);
                ReadOnlyCollection<IWebElement> elements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(elementLocator));
                return elements.Count;
            }
            catch (WebDriverTimeoutException)
            {
                return 0;
            }
        }

        public static string GetText(By elementLocator, int timeout = waitForElementTimeout)
        {
            try
            {
                var wait = WebDriver.Wait(timeout);
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
                return element.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return "";
            }
        }

        public static string GetValue(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            return element.GetAttribute("value");
        }

        public static bool GetCheckboxStatus(By elementLocator, int timeout = waitForElementTimeout)
        {
            try
            {
                var wait = WebDriver.Wait(timeout);
                // mat-checkbox element
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
                IWebElement childElement = element.StableFindChildElement(By.XPath(".//input"));
                return bool.Parse(childElement.GetAttribute("aria-checked"));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static string GetTextFromPageSource(By elementLocator, int timeout = waitForElementTimeout)
        {
            try
            {
                var element = StableFindElementFromPageSource(elementLocator, timeout);
                return element.InnerText;
            }
            catch (NoSuchElementException)
            {
                return "";
            }
        }

        public static int GetElementNumberFromPageSource(By elementLocator, int timeout = waitForElementTimeout)
        {
            try
            {
                var elements = StableFindElementsFromPageSource(elementLocator, timeout);
                return elements.Count;
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
        }

        public static bool IsElementPresent(By elementLocator)
        {
            try
            {
                WebDriver.FindElement(elementLocator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsDropdownEnabled(By elementLocator)
        {
            int retries = 0;
            while (true)
            {
                try
                {
                    if (WebDriver.FindElement(elementLocator).GetAttribute("aria-disabled") == "false")
                    {
                        return true;
                    }
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    if (retries < maxStaleElementRetries)
                    {
                        retries++;
                        continue;
                    }
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static bool IsElementEnabled(By elementLocator)
        {
            int retries = 0;
            while (true)
            {
                try
                {
                    if (WebDriver.FindElement(elementLocator).Enabled)
                    {
                        return true;
                    }
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    if (retries < maxStaleElementRetries)
                    {
                        retries++;
                        continue;
                    }
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static bool IsElementDisplayed(By elementLocator, int timeout = 0)
        {
            int retries = 0;

            if (timeout > 0)
            {
                WaitForElementVisible(elementLocator, timeout, false);
            }

            while (true)
            {
                try
                {
                    if (WebDriver.FindElement(elementLocator).Displayed)
                    {
                        return true;
                    }
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    if (retries < maxStaleElementRetries)
                    {
                        retries++;
                        continue;
                    }
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static bool IsElementDisplayedFromPageSource(By elementLocator, int timeout = 0)
        {
            try
            {
                var element = StableFindElementFromPageSource(elementLocator, timeout);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsAlertDisplayed(int timeout = 0)
        {
            try
            {
                WaitForAlert(timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static bool IsButtonSelected(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");

            if (element.TagName == "button" && element.GetAttribute("aria-pressed") == "true")
            {
                return true;
            }
            else if (element.TagName == "mat-radio-button" && element.GetAttribute("class").Contains("checked"))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Interaction
        public static void InputText(this IWebElement element, string text, bool byJS = false)
        {
            if (text != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");
                if (byJS)
                {
                    try
                    {
                        WebDriver.ExecuteScript("arguments[0].value = arguments[1];", element, text);
                    }
                    catch (TimeoutException e)
                    {
                        throw new Exception($"{element.TagName} - Element not visible within timeout period - Message: {e.Message}");
                    }
                }
                else
                {
                    element.SendKeys("");
                    element.Clear();
                    if (text.Length > 0)
                    {
                        element.SendKeys(text);
                    }
                    else
                    {
                        // In case user want to remove the old value and leave the field empty
                        // Trigger changing data event on field
                        element.SendKeys(" " + Keys.Backspace);
                    }

                    // Try to sendKeys again if the first try was fail
                    if (text.Length > 0 && string.IsNullOrEmpty(element.GetValue()))
                    {
                        Wait(3);
                        if (WebDriver.Property.DriverType == DriverType.InternetExplorer)
                        {
                            // split to groups with maximum 30 chars for more stable
                            foreach (Match item in Regex.Matches(text, @".{1,30}"))
                            {
                                element.SendKeys(item.Value);
                            }
                        }
                        else
                        {
                            element.SendKeys(text);
                        }
                    }
                }
            }
        }

        public static void ClearByBackSpace(this IWebElement element)
        {
            string backspaceSeries = string.Concat(Enumerable.Repeat(Keys.Backspace, element.GetValue().Length));
            element.SendKeys(backspaceSeries);
        }

        public static void InputDate(this IWebElement element, string date)
        {
            if (date != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");
                string backspaceSeries = string.Concat(Enumerable.Repeat(Keys.Backspace, element.GetValue().Length));
                element.SendKeys(backspaceSeries + date);
                // Try to sendKeys again if the first try was fail
                if (string.IsNullOrEmpty(element.GetValue()))
                {
                    Wait(3);
                    element.SendKeys(date);
                }
                else if (element.GetValue().Contains("_"))
                {
                    Wait(3);
                    backspaceSeries = string.Concat(Enumerable.Repeat(Keys.Backspace, element.GetValue().Length));
                    element.SendKeys(backspaceSeries + date);
                }
            }
        }

        public static void SelectDatePicker(this IWebElement element, string date, string dateFormat = "MM/dd/yyyy")
        {
            string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

            if (date != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");

                string currentValue;
                string[] currentMonthYear;
                string[] currentYearRange;
                int monthIndex = 0;
                int yearIndex = 1;
                char dateSeparator = ' ';
                char yearRangeSeparator = '–';
                var targetDate = DateHelper.GetDateFromString(date, dateFormat);
                Stopwatch stopwatch = new Stopwatch();

                // Prepare arrow button direction
                var arrowButton = nextCalendarLocator;
                if (DateTime.Now.Date > targetDate)
                    arrowButton = previousCalendarLocator;

                // Open date picker
                element.Click();
                WaitForElementVisible(calendarLocator);

                // Select month year if needed
                currentValue = GetText(calendarPeriodItemLocator);
                if (currentValue.Trim().Contains("/"))
                {
                    yearIndex = 2;
                    dateSeparator = '/';
                }

                currentMonthYear = currentValue.Trim().Split(dateSeparator);
                if (targetDate.Year != Convert.ToInt32(currentMonthYear[yearIndex]))
                {
                    // Open Month Year calendar
                    CalendarPeriodItem.Click();

                    // Loop to open suitable year ranges
                    stopwatch.Start();
                    do
                    {
                        currentValue = GetText(calendarPeriodItemLocator);
                        currentYearRange = currentValue.Trim().Split(yearRangeSeparator);
                        // Stop looping if target year match year ranges
                        if (targetDate.Year >= Convert.ToInt32(currentYearRange[0]) && targetDate.Year <= Convert.ToInt32(currentYearRange[1]))
                            break;

                        // Open other year ranges
                        StableFindElement(arrowButton, mediumTimeout).Click();
                        WaitForElementTextChanged(calendarPeriodItemLocator, currentValue, mediumTimeout);

                    } while (stopwatch.ElapsedMilliseconds <= longTimeout * 1000);
                    stopwatch.Reset();

                    // Select Year
                    currentValue = GetText(calendarPeriodItemLocator);
                    TargetCalendarItem(targetDate.Year.ToString()).Click();
                    WaitForElementTextChanged(calendarPeriodItemLocator, currentValue, mediumTimeout);

                    // Select Month
                    currentValue = GetText(calendarPeriodItemLocator);
                    TargetCalendarItem(months[targetDate.Month - 1]).Click();
                    WaitForElementTextChanged(calendarPeriodItemLocator, currentValue, mediumTimeout);
                }

                // Loop to open suitable month
                stopwatch.Start();
                do
                {
                    currentValue = GetText(calendarPeriodItemLocator);
                    currentMonthYear = currentValue.Trim().Split(dateSeparator);
                    // Stop looping if target month match current month
                    if ((Utils.IsNumber(currentMonthYear[monthIndex]) && Convert.ToInt32(currentMonthYear[monthIndex]) == targetDate.Month)
                        || currentMonthYear[monthIndex] == months[targetDate.Month - 1])
                        break;

                    // Open other month
                    StableFindElement(arrowButton, mediumTimeout).Click();
                    WaitForElementTextChanged(calendarPeriodItemLocator, currentValue, mediumTimeout);

                } while (stopwatch.ElapsedMilliseconds <= longTimeout * 1000);
                stopwatch.Stop();

                // Select Day
                currentValue = GetText(calendarPeriodItemLocator);
                TargetCalendarItem(targetDate.Day.ToString()).Click();
                WaitForElementInvisible(calendarLocator);
            }
        }

        public static void SelectItem(this IWebElement element, string item, string selectBy = "Text")
        {
            if (item != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");
                SelectElement selector = new SelectElement(element);
                if (selectBy == "Value")
                    selector.SelectByValue(item);
                else if (selectBy == "Index")
                    selector.SelectByIndex(int.Parse(item) - 1);
                else
                    selector.SelectByText(item);
            }
        }

        public static void SelectByText(this IWebElement element, string text, bool doesOpenOptions = true)
        {
            if (text != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");

                if (doesOpenOptions)
                {
                    element.Click();
                    WaitForElementExists(optionsLocator, longTimeout);
                }

                TargetOption(text).ClickWithJS();
                WaitForElementInvisible(optionsLocator, longTimeout, false);
            }
        }

        public static void SelectAnyExceptText(this IWebElement element, string exceptedText, bool doesOpenOptions = true)
        {
            if (exceptedText != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");

                if (doesOpenOptions)
                {
                    element.Click();
                    WaitForElementExists(optionsLocator, longTimeout);
                }

                TargetExceptedOption(exceptedText).ClickWithJS();
                WaitForElementInvisible(optionsLocator, longTimeout, false);
            }
        }

        public static void TrySelectByText(this IWebElement element, string text, int timeout = waitForElementTimeout)
        {
            if (text != null)
            {
                ParameterValidator.ValidateNotNull(element, "Element");

                // try to populate drop-down items
                bool isLoaded = element.TryOpenDropDownList(timeout);
                // the last chance to populate drop-down items
                if (!isLoaded)
                {
                    element.Click();
                }

                WaitForElementVisible(optionsLocator, longTimeout);
                TargetOption(text).ClickWithJS();
                WaitForElementInvisible(optionsLocator, longTimeout, false);
            }
        }

        public static bool TryOpenDropDownList(this IWebElement element, int timeout = waitForElementTimeout)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            bool isLoaded = false;
            // loop until the drop-down items are populated
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    element.Click();
                    if (IsElementDisplayed(optionsLocator, 3))
                    {
                        isLoaded = true;
                        break;
                    }
                }
                catch { }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);
            stopwatch.Stop();

            return isLoaded;
        }

        public static void Check(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            string isChecked = element.FindElement(By.TagName("input")).GetAttribute("aria-checked");
            if (isChecked == "false")
            {
                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    element.FindElement(By.TagName("input")).Click();
                }
            }
        }

        public static void UnCheck(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            string isChecked = element.FindElement(By.TagName("input")).GetAttribute("aria-checked");
            if (isChecked == "true")
            {
                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    element.FindElement(By.TagName("input")).Click();
                }
            }
        }

        public static void HoverElement(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            Actions action = WebDriver.Actions;
            action.MoveToElement(element);
            action.Perform();
        }

        public static void ScrollIntoViewAndClick(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            element.ScrollIntoView();
            element.Click();
        }

        public static void ClickWithJS(this IWebElement element, bool scrollIntoView = true)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            if (scrollIntoView)
            {
                element.ScrollIntoViewBottom();
            }
            WebDriver.ExecuteScript("arguments[0].click();", element);
        }

        public static void ScrollIntoView(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            WebDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ScrollIntoViewBottom(this IWebElement element)
        {
            ParameterValidator.ValidateNotNull(element, "Element");
            WebDriver.ExecuteScript("arguments[0].scrollIntoView(false);", element);
        }
        #endregion 
    }
}
