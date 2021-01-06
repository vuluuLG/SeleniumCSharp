using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleDescriptorsPage : LandingPage
    {
        #region Locators
        private By _txtYear => By.XPath("//*[contains(@class, 'automation-id-year')]//input");
        private By _txtMake => By.XPath("//*[contains(@class, 'automation-id-make')]//input");
        private By _txtModel => By.XPath("//*[contains(@class, 'automation-id-model')]//input");
        #endregion

        #region Elements
        public IWebElement YearTextBox => StableFindElement(_txtYear);
        public IWebElement MakeTextBox => StableFindElement(_txtMake);
        public IWebElement ModelTextBox => StableFindElement(_txtModel);
        #endregion

        #region Business Methods
        public VehicleDescriptorsPage() : base()
        {
            Url = "forms/descriptors";
            RequiredElementLocator = _txtYear;
        }

        [ExtentStepNode]
        public VehicleDescriptorsPage EnterVehicleDescription(VehicleDescription vehicleDescription)
        {
            GetLastNode().LogDataInfo(vehicleDescription);
            ParameterValidator.ValidateNotNull(vehicleDescription, "Vehicle Description");
            YearTextBox.InputText(vehicleDescription.Year);
            MakeTextBox.InputText(vehicleDescription.Make + Keys.Tab);
            ModelTextBox.InputText(vehicleDescription.Model + Keys.Tab);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public VehicleBodyTypePage SelectNextButton()
        {
            return SelectNextButton<VehicleBodyTypePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleDescriptorsPage ValidateVehicleDescriptorsPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle Descriptors page is displayed";
        }
        #endregion
    }
}