using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CargoLimitDeductiblePage : LandingPage
    {
        #region Locators
        private By _drpCargoLimit => By.XPath("//*[contains(@class, 'automation-id-limit')]//mat-select | //mat-select[contains(@formcontrolname, 'limit')]");
        private By _drpDeductible => By.XPath("//*[contains(@class, 'automation-id-deductible')]//mat-select | //mat-seIect[contains(@formcontrolname, 'deductible')]");
        #endregion

        #region Elements
        public IWebElement CargoLimitDropdown => StableFindElement(_drpCargoLimit);
        public IWebElement DeductibleDropdown => StableFindElement(_drpDeductible);
        #endregion

        #region Business Methods
        public CargoLimitDeductiblePage() : base()
        {
            Urls = new string[] 
            {
                "forms/cargo-limit-deductible",
                "forms/cargo-incl-in-tow"
            };
            RequiredElementLocator = _drpCargoLimit;
        }
        [ExtentStepNode]
        public CargoLimitDeductiblePage SelectCargoLimitAndDeductible(CargoLimitDeductible cargoLimitDeductible)
        {
            GetLastNode().LogDataInfo(cargoLimitDeductible);
            ParameterValidator.ValidateNotNull(cargoLimitDeductible, "Cargo Limit Deductible");
            StableSelectLimitAndDeductible(_drpCargoLimit, _drpDeductible, cargoLimitDeductible.CargoLimit, cargoLimitDeductible.Deductible);
            WaitForElementEnabled(_btnNext);
            return this;
        }
        public CargoCategoryPage SelectNextButton()
        {
            return SelectNextButton<CargoCategoryPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CargoLimitDeductiblePage ValidateCargoLimitDeductiblePageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Cargo Limit Deductible page is displayed";
        }
        #endregion
    }
}