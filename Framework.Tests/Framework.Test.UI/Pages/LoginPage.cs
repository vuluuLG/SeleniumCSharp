using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LoginPage : BasePage
    {
        #region Locators
        private By _txtUsername => By.XPath("//input[@id='Username']");
        private By _txtPassword => By.XPath("//input[@id='Password']");
        private By _chxRemember => By.XPath("//input[@id='RememberLogin']");
        private By _lnkForgotPassword => By.XPath("//a[@id='forgotPassword']");
        private By _btnLogin => By.XPath("//button[@value='login']");
        private By _lblValidation(string reason) => By.XPath($"//div[contains(@class, 'danger')]//li[normalize-space(text())='{reason}']");

        #endregion

        #region Elements
        public IWebElement UsernameTextBox => StableFindElement(_txtUsername);
        public IWebElement PasswordTextBox => StableFindElement(_txtPassword);
        public IWebElement RememberCheckBox => StableFindElement(_chxRemember);
        public IWebElement ForgotPasswordLink => StableFindElement(_lnkForgotPassword);
        public IWebElement LoginButton => StableFindElement(_btnLogin);
        public IWebElement ValidationLabel(string reason) => StableFindElement(_lblValidation(reason));
        #endregion

        #region Business Methods
        public LoginPage()
        {
            RequiredElementLocator = _btnLogin;
        }

        [ExtentStepNode]
        public WelcomePage Login(Account account)
        {
            UsernameTextBox.InputText(account.Email);
            PasswordTextBox.InputText(account.Password);

            if (account.Remember)
                RememberCheckBox.Check();

            LoginButton.Click();

            var page = new WelcomePage();

            if (account.Success)
                page.WaitForPageLoad();

            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public LoginPage ValidateLoginPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Login page is displayed";
        }
        #endregion
    }
}