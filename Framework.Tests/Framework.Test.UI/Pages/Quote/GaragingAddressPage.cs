using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class GaragingAddressPage : LandingPage
    {
        #region Locators
        private By _txtLine1 => By.XPath("//*[contains(@class, 'automation-id-address.line1')]//input");
        private By _txtLine2 => By.XPath("//*[contains(@class, 'automation-id-address.line2')]//input");
        private By _txtCity => By.XPath("//*[contains(@class, 'automation-id-address.city')]//input");
        private By _txtState => By.XPath("//*[contains(@class, 'automation-id-address.state')]//input");
        private By _txtZip => By.XPath("//*[contains(@class, 'automation-id-address.zip')]//input");
        private By _chkSameAsAddress => By.XPath("//*[contains(@class, 'automation-id-sameAsBusinessLocation')]//mat-checkbox");
        #endregion

        #region Elements
        public IWebElement Line1TextBox => StableFindElement(_txtLine1);
        public IWebElement Line2TextBox => StableFindElement(_txtLine2);
        public IWebElement CityTextBox => StableFindElement(_txtCity);
        public IWebElement StateTextBox => StableFindElement(_txtState);
        public IWebElement ZipTextBox => StableFindElement(_txtZip);
        public IWebElement SameAsAddressCheckBox => StableFindElement(_chkSameAsAddress);
        #endregion

        #region Business Methods
        public GaragingAddressPage() : base()
        {
            Url = "forms/garaging-address";
            RequiredElementLocator = _txtLine1;
        }

        [ExtentStepNode]
        public GaragingAddressPage InputGaragingAddress(Address address)
        {
            GetLastNode().LogDataInfo(address);
            ParameterValidator.ValidateNotNull(address, "Address");
            if (address.IsSameAddress)
            {
                SameAsAddressCheckBox.Check();
            }
            else if (address.DoesHandleConfirmAddressDialog)
            {
                Line1TextBox.InputText(address.Line1 + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                // Line2TextBox.lnputText(address.Line2 + Keys.Tab);
                CityTextBox.InputText(address.City + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                StateTextBox.InputText(address.State + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                ZipTextBox.InputText(address.Zip + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
            }
            else
            {
                Line1TextBox.InputText(address.Line1 + Keys.Tab);
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                // Line2TextBox.lnputText(address.Line2 + Keys.Tab);
                CityTextBox.InputText(address.City + Keys.Tab);
                StateTextBox.InputText(address.State + Keys.Tab);
                ZipTextBox.InputText(address.Zip + Keys.Tab);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public dynamic SelectNextButton()
        {
            dynamic page;
            string currentUrl = WebDriver.Url;
            NextButton.ScrollIntoViewBottom();
            WaitForElementClickable(_btnNext);
            NextButton.ClickWithJS();
            if (IsElementDisplayed(_eleWarningBar, 3))
            {
                GetLastNode().Info("Closing warning bar");
                DismissButton.ClickWithJS();
                WaitForElementInvisible(_eleWarningBar);
                NextButton.ClickWithJS();
            }
            WaitForUrlChanged(currentUrl);
            if (WebDriver.Url.Contains("vehicle-suggestions"))
            {
                page = new VehicleSuggestionsPage();
            }
            else
            {
                page = new VehicleEntryPage();
            }
            page.WaitForPageLoad();
            return page;
        }

        public VehicleInfoPage SelectPreviousButton()
        {
            return SelectPreviousButton<VehicleInfoPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public GaragingAddressPage ValidateGaragingAddressPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
                else
                { }
                SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
            }
            return this;
        }

        public GaragingAddressPage VaIidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<GaragingAddressPage>();
        }

        public GaragingAddressPage VaIidateNextButtonDispIayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<GaragingAddressPage>();
        }

        public GaragingAddressPage VaIidateNextButtonDisplayedAndEnabIed()
        {
            return ValidateNextButtonDisplayedAndEnabled<GaragingAddressPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Garaging Address page is displayed";
        }
        #endregion
    }
}


