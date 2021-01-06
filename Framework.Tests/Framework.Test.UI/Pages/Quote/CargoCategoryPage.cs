using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CargoCategoryPage : LandingPage
    {
        #region Locators
        private By _btnCargoCategory(string type) => By.XPath($"//*[contains(@class, 'automation-id-cargoCategoryValue')]//mat-radio-button[.//*[normalize-space(text())='{type}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-cargoCategoryValue')]//h1");
        #endregion

        #region Elements
        public IWebElement CargoCategoryButton(string type) => StableFindElement(_btnCargoCategory(type));
        #endregion

        #region Business Methods
        public CargoCategoryPage() : base()
        {
            Url = "forms/cargo-category";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public CargoCategoryPage SelectCargoCategory(string cargoCategory)
        {
            GetLastNode().Info("Select cargo category: " + cargoCategory);
            CargoCategoryButton(cargoCategory).Click();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public CargoCommodityPage SelectNextButton()
        {
            return SelectNextButton<CargoCommodityPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CargoCategoryPage ValidateCargoCategoryPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Cargo Category page is displayed";
        }
        #endregion
    }
}