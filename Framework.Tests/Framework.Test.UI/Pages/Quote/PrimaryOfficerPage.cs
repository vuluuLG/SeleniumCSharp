using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class PrimaryOfficerPage : LandingPage
    {
        #region Locators
        private By _txtFirstName => By.XPath("//*[contains(@class, 'automation-id-firstName')]//input");
        private By _txtMiddleInitial => By.XPath("//*[contains(@class, 'automation-id-middleInitial')]//input");
        private By _txtLastName => By.XPath("//*[contains(@class, 'automation-id-lastName')]//input");
        private By _txtLine1 => By.XPath("//*[contains(@class, 'automation-id-address.line1')]//input");
        private By _txtLine2 => By.XPath("//*[contains(@class, 'automation-id-address.line2')]//input");
        private By _txtCity => By.XPath("//*[contains(@class, 'automation-id-address.city')]//input");
        private By _txtState => By.XPath("//*[contains(@class, 'automation-id-address.state')]//input");
        private By _txtZip => By.XPath("//*[contains(@class, 'automation-id-address.zip')]//input");
        private By _chkSameAsAddress => By.XPath("//*[contains(@class, 'automation-id-sameAsBusinessLocation')]//mat-checkbox");
        private By _pnlError => By.XPath("//simple-snack—bar/span");
        #endregion

        #region Elements
        public IWebElement FirstNameTextBox => StableFindElement(_txtFirstName);
        public IWebElement MiddlelnitialTextBox => StableFindElement(_txtMiddleInitial);
        public IWebElement LastNameTextBox => StableFindElement(_txtLastName);
        public IWebElement Line1TextBox => StableFindElement(_txtLine1);
        public IWebElement Line2TextBox => StableFindElement(_txtLine2);
        public IWebElement CityTextBox => StableFindElement(_txtCity);
        public IWebElement StateTextBox => StableFindElement(_txtState);
        public IWebElement ZipTextBox => StableFindElement(_txtZip);
        public IWebElement SameAsAddressCheckBox => StableFindElement(_chkSameAsAddress);
        public IWebElement ErrorPanel => StableFindElement(_pnlError);
        #endregion

        #region Business Methods
        public PrimaryOfficerPage() : base()
        {
            Url = "forms/primary-officer";
            RequiredElementLocator = _txtFirstName;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage InputPrimaryOfficer(FullName fullName, Address address)
        {
            var node = GetLastNode();
            node.LogDataInfo(fullName);
            node.LogDataInfo(address);
            ParameterValidator.ValidateNotNull(fullName, "Full Name");
            ParameterValidator.ValidateNotNull(address, "Address");
            FirstNameTextBox.InputText(fullName.FirstName);
            MiddlelnitialTextBox.InputText(fullName.MiddleInitial);
            LastNameTextBox.InputText(fullName.LastName);
            if (address.IsSameAddress)
            {
                SameAsAddressCheckBox.Check();
            }
            else if (address.DoesHandleConfirmAddressDialog)
            {
                Line1TextBox.InputText(address.Line1 + Keys.Tab);
                //Line2TextBox.InputText(address.Line2 + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
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
                //LineZTextBox.|nputText(address.Line2 + Keys.Tab);
                CityTextBox.InputText(address.City + Keys.Tab);
                StateTextBox.InputText(address.State + Keys.Tab);
                ZipTextBox.InputText(address.Zip + Keys.Tab);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage InputPrimaryOfficerAddress(Address address)
        {
            var node = GetLastNode();
            node.LogDataInfo(address);
            ParameterValidator.ValidateNotNull(address, "Address");
            if (address.IsSameAddress)
            {
                SameAsAddressCheckBox.Check();
            }
            else if (address.DoesHandleConfirmAddressDialog)
            {
                Line1TextBox.InputText(address.Line1 + Keys.Tab);
                //Line2TextBox.InputText(address.Line2 + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
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
                //Line2TextBox.InputText(address.Line2 + Keys.Tab);
                CityTextBox.InputText(address.City + Keys.Tab);
                StateTextBox.InputText(address.State + Keys.Tab);
                ZipTextBox.InputText(address.Zip + Keys.Tab);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage CheckTheSameAsBusinessAddress()
        {
            SameAsAddressCheckBox.Check();
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage UnCheckTheSameAsBusinessAddress()
        {
            SameAsAddressCheckBox.UnCheck();
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage InputPOName(FullName fullName)
        {
            GetLastNode().LogDataInfo(fullName);
            ParameterValidator.ValidateNotNull(fullName, "Full Name");
            FirstNameTextBox.InputText(fullName.FirstName);
            MiddlelnitialTextBox.InputText(fullName.MiddleInitial);
            LastNameTextBox.InputText(fullName.LastName);
            return this;
        }

        public CustomerInfoPage SelectNextButton()
        {
            return SelectNextButton<CustomerInfoPage>();
        }

        public BusinessInformationPage SelectPreviousButton()
        {
            return SelectPreviousButton<BusinessInformationPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerNameDisplayed()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("First Name", _txtFirstName),
                    new KeyValuePair<string, By>("Middle Initial", _txtMiddleInitial),
                    new KeyValuePair<string, By>("Last Name", _txtLastName)
                };
                foreach (var item in locators)
                {
                    if (IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayed, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerNameDisplayedCorrectly(FullName fullName)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(fullName, "FullName");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("First Name", new string[] {fullName.FirstName, FirstNameTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Middle Initial", new string[]{fullName.MiddleInitial, MiddlelnitialTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Last Name", new string[] {fullName.LastName, LastNameTextBox.GetValue()})
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfiicerAddressDisplayed()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("Line 1", _txtLine1),
                    //new KeyValuePair<string, By>("Line 2", _txtLine2),
                    new KeyValuePair<string, By>("City", _txtCity),
                    new KeyValuePair<string, By>("State", _txtState),
                    new KeyValuePair<string, By>("Zip", _txtZip)
                };
                foreach (var item in locators)
                {
                    if (IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisplayed, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerAddressDisplayedCorrectly(Address address)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(address, "Address");
                List<KeyValuePair<String, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("Line 1", new string[] { address.Line1, Line1TextBox.GetValue()}),
                    //new KeyValuePair<string, string[]>("Line 2", new string[] { address.Line2, Line2TextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("City", new string[] { address.City, CityTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("State", new string[] { address.State, StateTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Zip", new string[] { address.Zip, ZipTextBox.GetValue()})
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisplayedCorrectly, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerAddressCleared()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("Line 1", _txtLine1),
                    new KeyValuePair<string, By>("Line 2", _txtLine2),
                    new KeyValuePair<string, By>("City", _txtCity),
                    new KeyValuePair<string, By>("State", _txtState),
                    new KeyValuePair<string, By>("Zip", _txtZip)
                };
                foreach (var item in locators)
                {
                    string actual = GetText(item.Value);
                    if (actual.Length == 0)
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressCleared, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressCleared, item.Key, expectedValue: "", actualValue: actual);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressCleared, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerPageDisplayed()
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
        public PrimaryOfficerPage ValidatePrimaryOfficerNameDisabled()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("First Name", _txtFirstName),
                    new KeyValuePair<string, By>("Middle Initial", _txtMiddleInitial),
                    new KeyValuePair<string, By>("Last Name", _txtLastName)
                };
                foreach (var item in locators)
                {
                    if (!IsElementEnabled(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisabled, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisabled, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerAddressDisabled()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("Line 1", _txtLine1),
                    new KeyValuePair<string, By>("Line 2", _txtLine2),
                    new KeyValuePair<string, By>("City", _txtCity),
                    new KeyValuePair<string, By>("State", _txtState),
                    new KeyValuePair<string, By>("Zip", _txtZip)
                };
                foreach (var item in locators)
                {
                    if (!IsElementEnabled(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisabled, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisabled, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerAddressNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("Line 1", _txtLine1),
                    new KeyValuePair<string, By>("Line 2", _txtLine2),
                    new KeyValuePair<string, By>("City", _txtCity),
                    new KeyValuePair<string, By>("State", _txtState),
                    new KeyValuePair<string, By>("Zip", _txtZip)
                };
                foreach (var item in locators)
                {
                    if (!IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressNotDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressNotDisplayed, item.Key);
                    }
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerAddressNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidateErrorPanelDisplayedWhenStatesAreDifferent(string expectedError)
        {
            var node = GetLastNode();
            try
            {
                NextButton.Click();
                WaitForElementVisible(_pnlError);
                if (ErrorPanel.Text.Trim() == expectedError)
                {
                    SetPassValidation(node, ValidationMessage.ValidateErrorPanelDisplayedWhenStatesAreDifferent, expectedValue: expectedError);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateErrorPanelDisplayedWhenStatesAreDifferent, expectedValue: expectedError, actualValue: ErrorPanel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateErrorPanelDisplayedWhenStatesAreDifferent, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfiicerFieldsNotEditable()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, bool>> comparisons = new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("First Name", !IsElementEnabled(_txtFirstName)),
                    new KeyValuePair<string, bool>("Middle Initial", !IsElementEnabled(_txtMiddleInitial)),
                    new KeyValuePair<string, bool>("Last Name", !IsElementEnabled(_txtLastName)),
                    new KeyValuePair<string, bool>("Line 1", !IsElementEnabled(_txtLine1)),
                    new KeyValuePair<string, bool>("City", !IsElementEnabled(_txtCity)),
                    new KeyValuePair<string, bool>("State", !IsElementEnabled(_txtState)),
                    new KeyValuePair<string, bool>("Zip", !IsElementEnabled(_txtZip)),
                };
                foreach (var item in comparisons)
                {
                    if (item.Value)
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerFieldsNotEditable, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerFieldsNotEditable, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerFieldsNotEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidateTheSameAsBusinessAddressCheckBoxChecked()
        {
            var node = GetLastNode();
            try
            {
                if (GetCheckboxStatus(_chkSameAsAddress))
                {
                    SetPassValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxChecked);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxChecked);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxChecked, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidateTheSameAsBusinessAddressCheckBoxUnChecked()
        {
            var node = GetLastNode();
            try
            {
                if (!GetCheckboxStatus(_chkSameAsAddress))
                {
                    SetPassValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxUnChecked);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxChecked);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxUnChecked, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidateTheSameAsBusinessAddressCheckBoxNotDispIayed()
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_chkSameAsAddress))
                {

                    SetPassValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxNotDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxNotDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTheSameAsBusinessAddressCheckBoxNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public PrimaryOfficerPage ValidatePrimaryOfficerNameAreFilledOut(FullName poName)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(poName, "Primary Officer Name");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("First Name", new string[] { poName.FirstName, FirstNameTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Middle Initial", new string[] { poName.MiddleInitial, MiddlelnitialTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Last Name", new string[] { poName.LastName, LastNameTextBox.GetValue()}),
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerNameAreFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameAreFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerNameAreFilledOut, e);
            }
            return this;
        }
        public PrimaryOfficerPage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<PrimaryOfficerPage>();
        }

        public PrimaryOfficerPage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<PrimaryOfficerPage>();
        }

        public PrimaryOfficerPage ValidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<PrimaryOfficerPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Primary Officer page is displayed";
            public static string ValidatePrimaryOfficerNameDisplayed = "Validate Primary Officer Name are displayed";
            public static string ValidatePrimaryOfficerNameDisplayedCorrectly = "Validate Primary Officer Name are displayed correctly";
            public static string ValidatePrimaryOfficerAddressDisplayed = "Validate Primary OfficerAddress fields are displayed";
            public static string ValidatePrimaryOfficerAddressDisplayedCorrectly = "Validate Primary OfficerAddress fields are displayed correctly";
            public static string ValidatePrimaryOfficerAddressNotDisplayed = "Validate Primary OfficerAddress fields are not displayed";
            public static string ValidatePrimaryOfficerNameDisabled = "Validate Primary Officer Name are disabled";
            public static string ValidatePrimaryOfficerAddressDisabled = "Validate Primary OfficerAddress fields are disabled";
            public static string ValidatePrimaryOfficerAddressCleared = "Validate Primary OfficerAddress fields are cleared";
            public static string ValidateErrorPanelDisplayedWhenStatesAreDifferent = "Validate Error panel is displayed when states are different";
            public static string ValidatePrimaryOfficerFieldsNotEditable = "Validate Primary Officer fields can not be edited";
            public static string ValidateTheSameAsBusinessAddressCheckBoxChecked = "Validate the Same As Business Location checkbox is checked";
            public static string ValidateTheSameAsBusinessAddressCheckBoxUnChecked = "Validate the Same As Business Location checkbox is unchecked";
            public static string ValidateTheSameAsBusinessAddressCheckBoxNotDisplayed = "Validate the Same As Business Location checkbox is not displayed";
            public static string ValidatePrimaryOfficerNameAreFilledOut = "Validate Primary Officer Name are filled out";
        }
        #endregion
    }
}
