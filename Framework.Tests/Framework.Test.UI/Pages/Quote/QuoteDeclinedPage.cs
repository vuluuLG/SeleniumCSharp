using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class QuoteDeclinedPage : LandingPage
    {
        #region Locators
        private By _lblMessage => By.XPath("//breeze-page—header//h1");
        private By _btnGoToDashboard => By.XPath("//button[contains(.,'GO TO DASHBOARD')]");
        #endregion

        #region Elements
        public IWebElement MessageLabel => StableFindElement(_lblMessage);
        public IWebElement GoToDashboardButton => StableFindElement(_btnGoToDashboard);
        #endregion

        #region Business Methods
        public QuoteDeclinedPage() : base()
        {
            Url = "forms/quotevdeclined";
            RequiredElementLocator = _btnGoToDashboard;
        }

        [ExtentStepNode]
        public WelcomePage SelectGoToDashboardButton()
        {
            GoToDashboardButton.Click();
            var page = new WelcomePage();
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public QuoteDeclinedPage ValidateQuoteDeclinedPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
                return this;
            }
            return this;
        }

        [ExtentStepNode]
        public QuoteDeclinedPage ValidateMessageDisplayedCorrectly(string expectedMessage)
        {
            var node = GetLastNode();
            try
            {
                if (MessageLabel.Text.Trim() == expectedMessage)
                {
                    SetPassValidation(node, ValidationMessage.ValidateMessageDisplayedCorrectly, expectedValue: expectedMessage);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateMessageDisplayedCorrectly, expectedValue: expectedMessage, actualValue: MessageLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMessageDisplayedCorrectly, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Quote Declined page is displayed";
            public static string ValidateMessageDisplayedCorrectly = "Validate message is displayed correctly";
        }
        #endregion
    }
}
