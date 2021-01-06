using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleGVWPage : LandingPage
    {
        #region Locators
        private By _drpGrossWeight => By.XPath("//*[contains(@c|ass, 'automation-id-grossWeight')]//mat-select");
        #endregion

        #region Elements
        public IWebElement GrossWeightDropdown => StableFindElement(_drpGrossWeight);
        #endregion

        #region Business Methods
        public VehicleGVWPage() : base()
        {
            Url = "forms/gross-weight";
            RequiredElementLocator = _drpGrossWeight;
        }

        [ExtentStepNode]
        public VehicleGVWPage SelectGrossWeight(string grossWeight)
        {
            GetLastNode().Info("Select Gross Weight: " + grossWeight);
            GrossWeightDropdown.SelectByText(grossWeight);
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleGVWPage ValidateVehicleGVWPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle GWV page is displayed";
        }
        #endregion
    }
}