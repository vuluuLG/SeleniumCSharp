using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class AddAnotherDriverPage : LandingPage
    {
        #region Locators
        private By _btnAddMoreDriver(string item) => By.XPath($"//*[contains(@class, 'automation-id-addsAnother')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-addsAnother')]//h1");
        #endregion

        #region Elements
        public IWebElement AddMoreDriverButton(string item) => StableFindElement(_btnAddMoreDriver(item));
        #endregion

        #region Business Methods
        public AddAnotherDriverPage() : base()
        {
            Url = "forms/add-another-driver";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public AddAnotherDriverPage SelectAddMoreDriverButton(string addMore)
        {
            GetLastNode().Info("Se|ect the answer: " + addMore);
            AddMoreDriverButton(addMore).Click();
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AddAnotherDriverPage ValidateAddAnotherDriverPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Add Another Driver page is displayed";
        }
        #endregion
    }
}