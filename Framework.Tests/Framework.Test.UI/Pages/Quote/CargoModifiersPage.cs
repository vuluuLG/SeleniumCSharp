using OpenQA.Selenium;
using System;
using Framework.Test.Common.Helper;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CargoModifiersPage : LandingPage
    {
        #region Locators
        private By _btnCargoModifier(string type) => By.XPath($"//*[contains(@class, 'automation-id-modifiers')]//mat-button-toggle[.//*[normalize-space(text())='{type}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-modifiers')]//h1");
        #endregion

        #region Elements
        public IWebElement CargoModifierButton(string type) => StableFindElement(_btnCargoModifier(type));
        #endregion

        #region Business Methods
        public CargoModifiersPage() : base()
        {
            Url = "forms/cargo-modifiers";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public CargoModifiersPage SelectCargoModifiers(params string[] cargoModifiers)
        {
            var node = GetLastNode();
            if (cargoModifiers != null)
            {
                foreach (var item in cargoModifiers)
                {
                    node.Info("Select cargo modifier: " + item);
                    CargoModifierButton(item).Click();
                }
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CargoModifiersPage ValidateCargoModifiersPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Cargo Modifiers page is displayed";
        }
        #endregion
    }
}