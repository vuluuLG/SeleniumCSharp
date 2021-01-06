using Framework.Test.Common.Helper;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LogoutPage : BasePage
    {
        #region Locators
        private By _btnSignMeOut => By.XPath("//button[contains(text(), 'Sign Me Out')]");

        #endregion

        #region Elements
        public IWebElement SignMeOutButton => StableFindElement(_btnSignMeOut);
        #endregion

        #region Business Methods
        public LogoutPage()
        {
            RequiredElementLocator = _btnSignMeOut;
        }

        [ExtentStepNode]
        public LoginPage SelectSignMeOutButton()
        {
            SignMeOutButton.Click();
            var page = new LoginPage();
            page.WaitForPageLoad();
            return page;
        }

        #endregion

        #region Validations
        [ExtentStepNode]
        public LogoutPage ValidateLogoutPageDisplayed()
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
            }

            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Logout page is displayed";
        }
        #endregion
    }
}