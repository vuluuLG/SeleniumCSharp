using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class AddAnotherVehiclePage : LandingPage
    {
        #region Locators
        private By _btnAddMoreVehicle(string item) => By.XPath($"//*[contains(@class, 'automation-id—addsAnother')]//mat—radio-button[.//*[normalize~space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-addsAnother')]//h1");
        #endregion

        #region Elements
        public IWebElement AddMoreVehicleButton(string item) => StableFindElement(_btnAddMoreVehicle(item));
        #endregion

        #region Business Methods
        public AddAnotherVehiclePage() : base()
        {
            Url = "forms/add-another-vehicle";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public AddAnotherVehiclePage SelectAddMoreVehicleButton(string addMore)
        {
            GetLastNode().Info("Select the answer: " + addMore);
            AddMoreVehicleButton(addMore).Click();
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AddAnotherVehiclePage ValidateAddAnotherVehiclePageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Add Another Vehicle page is displayed";
        }
        #endregion
    }
}