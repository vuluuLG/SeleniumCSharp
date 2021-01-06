using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CargoCommodityPage : LandingPage
    {
        #region Locators
        private By _btnCargoCommodity(string type) => By.XPath($"//*[contains(@class, 'automation-id-cargoCommodityValue')]//mat-radio-button[.//*[normalize-space(text())='{type}']]");
        private By _lblQuestion => By.XPath("//*[contains(@ class, 'automation-id-cargoCommodityVaIue')]//h1");
        #endregion

        #region Elements
        public IWebElement CargoCommodityButton(string type) => StableFindElement(_btnCargoCommodity(type));
        #endregion

        #region Business Methods
        public CargoCommodityPage() : base()
        {
            Url = "forms/cargo-commodity";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public CargoCommodityPage SelectCargoCommodity(string cargoCommodity)
        {
            GetLastNode().Info("Select cargo commodity: " + cargoCommodity);
            CargoCommodityButton(cargoCommodity).Click();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public CargoModifiersPage SelectNextButton()
        {
            return SelectNextButton<CargoModifiersPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CargoCommodityPage ValidateCargoCommodityPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Cargo Commodity page is displayed";
        }
        #endregion
    }
}