using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class WelcomePage : BasePage
    {
        #region Locators
        private By _drpAgency => By.XPath("//breeze-agency-select");
        private By _eleAgency(string item) => By.XPath($"//breeze-agency-select//mat-list-item[.//span[normalize-space(text())='{item}']]");
        private By _btnStartNewQuote => By.XPath("//button[.='Start New Breeze Submission']");
        private By _lblUsername => By.XPath("//*[contains(@class='user-button')]//span");
        private By _iconOpenUserMenu => By.XPath("//*[contains(@class='user-button')]//mat-icon[1]");
        private By _menuUser => By.XPath("//*[@role='menu']");
        private By _btnLogout => By.XPath("//*[@role='menuitem' and .='Sign Out']");

        #endregion

        #region Elements
        public IWebElement AgencyItem(string item) => StableFindElement(_eleAgency(item));
        public IWebElement StartNewBreezeSubmissionButton => StableFindElement(_btnStartNewQuote);
        public IWebElement UsernameLabel => StableFindElement(_lblUsername);
        public IWebElement OpenUserMenuIcon => StableFindElement(_iconOpenUserMenu);
        public IWebElement LogoutButton => StableFindElement(_btnLogout);
        #endregion

        #region Business Methods
        public WelcomePage()
        {
            RequiredElementLocator = _btnStartNewQuote;
        }

        [ExtentStepNode]
        public T SelectStartNewBreezeSubmissionButton<T>() where T : BasePage
        {
            StartNewBreezeSubmissionButton.ClickWithJS();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public BusinessClassificationPage SelectAgency(string agency)
        {
            GetLastNode().Info("Select agency: " + agency);
            WaitForElementVisible(_drpAgency);
            WaitForElementClickable(_eleAgency(agency));
            AgencyItem(agency).ClickWithJS();
            var page = new BusinessClassificationPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public T ResumeQuote<T>(string baseUrl, string quoteld) where T : BasePage
        {
            string resumeUrl = !string.IsNullOrEmpty(baseUrl) && baseUrl.EndsWith("/")
                ? $"{baseUrl}quotes/{quoteld}/resume"
                : $"{baseUrl}/quotes/{quoteld}/resume";
            GetLastNode().Info("Resume URL' " + resumeUrl);
            WebDriver.Navigate.GoToUrl(new Uri(resumeUrl));
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public LogoutPage SelectSignOutButton()
        {
            OpenUserMenuIcon.Click();
            WaitForElementVisible(_menuUser);
            LogoutButton.ClickWithJS();
            var page = new LogoutPage();
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public WelcomePage ValidateWelcomePageDisplayed()
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

        [ExtentStepNode]
        public WelcomePage ValidateUsernameDisplayedCorrectly(string username)
        {
            var node = GetLastNode();
            try
            {
                string actual = GetText(_lblUsername);
                if (actual.Contains(username))
                {
                    SetPassValidation(node, ValidationMessage.ValidateUsernameDisplayedCorrectly, expectedValue: username);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateUsernameDisplayedCorrectly, expectedValue: username, actualValue: actual);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateUsernameDisplayedCorrectly, e);
            }

            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Welcome page is displayed";
            public static string ValidateUsernameDisplayedCorrectly = "Validate Username is displayed correctly";
        }
        #endregion
    }
}