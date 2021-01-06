using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleEntryPage : LandingPage
    {
        #region Locators
        private By _txtVin => By.XPath("//*[contains(@class, 'automation-id-vin')]//input");
        private By _lnkHasNoVin => By.XPath("//*[contains(@class, 'automation-id-hasNoVin')]//span");
        #endregion

        #region Elements
        public IWebElement VinTextBox => StableFindElement(_txtVin);
        public IWebElement HasNoVinLink => StableFindElement(_lnkHasNoVin);
        #endregion

        #region Business Methods
        public VehicleEntryPage() : base()
        {
            Url = "forms/Vin";
            RequiredElementLocator = _txtVin;
        }

        [ExtentStepNode]
        public VehicleEntryPage EnterVin(string vin)
        {
            GetLastNode().Info("Input vin: " + vin);
            VinTextBox.InputText(vin);
            return this;
        }

        [ExtentStepNode]
        public VehicleDescriptorsPage SelectHasNoVin()
        {
            HasNoVinLink.Click();
            var page = new VehicleDescriptorsPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public dynamic SelectNextButton()
        {
            dynamic page;
            string currentUrl = WebDriver.Url;
            NextButton.ScrollIntoViewBottom();
            NextButton.Click();
            WaitForUrlChanged(currentUrl);
            if (WebDriver.Url.Contains("descriptors"))
            {
                page = new VehicleDescriptorsPage();
            }
            else
            {
                page = new VehicleBodyTypePage();
            }
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleEntryPage ValidateVehicleEntryPageDisplayed()
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
        public VehicleEntryPage ValidateVehicleInfoInTheHeaderDisplayedCorrectly(string expectedVehiclelnfo)
        {
            var node = GetLastNode();
            try
            {
                string actualVehiclelnfo = GetText(_lblHeader);
                if (actualVehiclelnfo.Trim() == expectedVehiclelnfo)
                {
                    SetPassValidation(node, ValidationMessage.ValidateVehicleInfoInTheHeaderDisplayedCorrectly, expectedValue: expectedVehiclelnfo);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehicleInfoInTheHeaderDisplayedCorrectly, expectedValue: expectedVehiclelnfo, actualValue: actualVehiclelnfo);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleInfoInTheHeaderDisplayedCorrectly, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Vehicle Entry page is displayed";
            public static string ValidateVehicleInfoInTheHeaderDisplayedCorrectly = "Validate Vehicle info in the header is displayed correctly";
        }
        #endregion
    }
}