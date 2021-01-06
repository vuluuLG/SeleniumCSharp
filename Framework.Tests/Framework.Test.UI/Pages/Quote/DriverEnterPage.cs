using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class DriverEnterPage : LandingPage
    {
        #region Locators
        private By _txtFirstName => By.XPath("//*[contains(@class, 'automation-id-firstName')]//input");
        private By _txtMiddleInitial => By.XPath("//*[contains(@class, 'automation-id-middlelnitial')]//input");
        private By _txtLastName => By.XPath("//*[contains(@class, 'automation-id-lastName')]//input");
        private By _txtDateOfBirth => By.XPath("//*[contains(@class, 'automation-id-dateOfBirth')]//input");
        private By _txtLicenseNumber => By.XPath("//*[contains(@class, 'automation-id-licenseNumber')]//input");
        private By _drpLicenseState => By.XPath("//*[contains(@class, 'automation-id-state')]//mat-select");
        private By _chkDoesNotDrive => By.XPath("//*[contains(@class, 'automation-id-doesNotDrive')]//mat-checkbox");
        #endregion

        #region Elements
        public IWebElement FirstNameTextBox => StableFindElement(_txtFirstName);
        public IWebElement MiddleInitialTextBox => StableFindElement(_txtMiddleInitial);
        public IWebElement LastNameTextBox => StableFindElement(_txtLastName);
        public IWebElement DateOfBirthTextBox => StableFindElement(_txtDateOfBirth);
        public IWebElement LicenseNumberTextBox => StableFindElement(_txtLicenseNumber);
        public IWebElement LicenseStateDropdown => StableFindElement(_drpLicenseState);
        public IWebElement DoesNotDriveCheckBox => StableFindElement(_chkDoesNotDrive);
        #endregion

        #region Business Methods
        public DriverEnterPage() : base()
        {
            Url = "forms/enter";
            RequiredElementLocator = _txtFirstName;
        }

        [ExtentStepNode]
        public DriverEnterPage EnterDriverInfo(DriverInformation driverInformation, bool wasAbleToProceed = true)
        {
            GetLastNode().LogDataInfo(driverInformation);
            ParameterValidator.ValidateNotNull(driverInformation, "Driver Information");
            if (!driverInformation.IsPO)
            {
                FirstNameTextBox.InputText(driverInformation.FullName.FirstName);
                MiddleInitialTextBox.InputText(driverInformation.FullName.MiddleInitial);
                LastNameTextBox.InputText(driverInformation.FullName.LastName);
            }
            DateOfBirthTextBox.InputText(driverInformation.DateOfBirth);
            LicenseNumberTextBox.InputText(driverInformation.LicenseNumber);
            LicenseStateDropdown.SelectByText(driverInformation.LicenseState);
            if (driverInformation.DoesNotDrive)
            {
                DoesNotDriveCheckBox.Check();
            }
            if (wasAbleToProceed)
            {
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage EnterNameAndDOB(DriverInformation driverInformation, bool wasAbleToProceed = true)
        {
            GetLastNode().LogDataInfo(driverInformation);
            ParameterValidator.ValidateNotNull(driverInformation, "Driver Information");
            FirstNameTextBox.InputText(driverInformation.FullName.FirstName);
            MiddleInitialTextBox.InputText(driverInformation.FullName.MiddleInitial);
            LastNameTextBox.InputText(driverInformation.FullName.LastName);
            DateOfBirthTextBox.InputText(driverInformation.DateOfBirth);
            if (wasAbleToProceed)
            {
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage EnterLicenseAndState(DriverInformation driverInformation, bool wasAbleToProceed = true)
        {
            GetLastNode().LogDataInfo(driverInformation);
            ParameterValidator.ValidateNotNull(driverInformation, "Driver Information");
            LicenseNumberTextBox.InputText(driverInformation.LicenseNumber);
            LicenseStateDropdown.SelectByText(driverInformation.LicenseState);
            if (driverInformation.DoesNotDrive)
            {
                DoesNotDriveCheckBox.Check();
            }
            if (wasAbleToProceed)
            {
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        public CDLExperiencePage SelectNextButton()
        {
            return SelectNextButton<CDLExperiencePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public DriverEnterPage ValidateDriverEnterPageDisplayed()
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
        public DriverEnterPage ValidateDriverNameIsFilledOut(FullName driverName)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(driverName, "Driver Name");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("First Name", new string[] { driverName.FirstName, FirstNameTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Middle Initial", new string[] { driverName.MiddleInitial, MiddleInitialTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Last Name", new string[] { driverName.LastName, LastNameTextBox.GetValue()}),
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDriverNameIsFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDriverNameIsFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverNameIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateDriverlnfolsFilledOut(DriverInformation driverInformation)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(driverInformation, "Driver Information");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("First Name", new string[] { driverInformation.FullName.FirstName, FirstNameTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("MiddIe Initial", new string[] { driverInformation.FullName.MiddleInitial, MiddleInitialTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("Last Name", new string[] { driverInformation.FullName.LastName, LastNameTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("Date Of Birth", new string[] { driverInformation.DateOfBirth, DateOfBirthTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("License Number", new string[] { driverInformation.LicenseNumber, LicenseNumberTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("License State", new string[] { driverInformation.LicenseState, LicenseStateDropdown.Text }),
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDriverInfoIsFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDriverInfoIsFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverInfoIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateLicenseStateDisplayedInAlphabeticalWithSpecifiedStateAtBottom(string specifiedState)
        {
            var node = GetLastNode();
            bool result = true;
            LicenseStateDropdown.Click();
            WaitForElementVisible(_eleDropdownOptions);
            string[] licenseStateList = DropdownOptionsLabel.Select(x => x.Text).ToArray();
            string actualStateList = string.Join(" - ", licenseStateList);
            try
            {
                if (licenseStateList[licenseStateList.Length - 1] != specifiedState)
                {
                    result = false;
                }
                else
                {
                    for (int i = 0; i < licenseStateList.Length - 2; i++)
                    {
                        if (licenseStateList[i].Length != 2
                        || StringComparer.Ordinal.Compare(licenseStateList[i][0], licenseStateList[i + 1][0]) > 0)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                if (result)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithSpecifiedStateAtBottom, specifiedState), actualStateList);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithSpecifiedStateAtBottom, specifiedState), actualStateList);
                }
                LicenseStateDropdown.SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithSpecifiedStateAtBottom, specifiedState), e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateDriverNameFieldsDisplayedAndDisabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtFirstName) && !IsElementEnabled(_txtFirstName))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "First Name");
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "First Name");
                }

                if (IsElementDisplayed(_txtMiddleInitial) && !IsElementEnabled(_txtMiddleInitial))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "Middle Initial");
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "Middle Initial");
                }

                if (IsElementDisplayed(_txtLastName) && !IsElementEnabled(_txtLastName))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "Last Name");
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, "Last Name");
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverNameFieldsDisplayedAndDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateLicenseStateDisplayedInAlphabeticalWithoutSpecifiedStateAtBottom(string specifiedState)
        {
            var node = GetLastNode();
            bool result = true;
            LicenseStateDropdown.Click();
            WaitForElementVisible(_eleDropdownOptions);
            string[] licenseStateList = DropdownOptionsLabel.Select(x => x.Text).ToArray();
            string actualStateList = string.Join(" - ", licenseStateList);
            try
            {
                if (licenseStateList[licenseStateList.Length - 1] == specifiedState)
                {
                    result = false;
                }
                else
                {
                    for (int i = 0; i < licenseStateList.Length - 1; i++)
                    {
                        if (licenseStateList[i].Length != 2
                        || StringComparer.Ordinal.Compare(licenseStateList[i][0], licenseStateList[i + 1][0]) > 0)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                if (result)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithoutSpecifiedStateAtBottom, specifiedState), actualStateList);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithoutSpecifiedStateAtBottom, specifiedState), actualStateList);
                }
                LicenseStateDropdown.SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);

            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateLicenseStateDisplayedInAlphabeticalWithoutSpecifiedStateAtBottom, specifiedState), e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateLicenseStateIsFilledOut(string expectedState)
        {
            var node = GetLastNode();
            try
            {
                string actualState = GetText(_drpLicenseState);
                if (expectedState == actualState)
                {
                    SetPassValidation(node, ValidationMessage.ValidateLicenseStateIsFilledOut, expectedValue: expectedState);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateLicenseStateIsFilledOut, expectedValue: expectedState, actualValue: actualState);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLicenseStateIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateDriverFieldsAreEmpty()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("First Name", new string[] {string.Empty, FirstNameTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Midd|e Initial", new string[] {string.Empty, MiddleInitialTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Last Name", new string[] {string.Empty, LastNameTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("Date Of Birth", new string[] {string.Empty, DateOfBirthTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("License Number", new string[] {string.Empty, LicenseNumberTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("License State", new string[] { "Select State", LicenseStateDropdown.Text}),
                };

                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDriverFieldsAreEmpty, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDriverFieldsAreEmpty, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverFieldsAreEmpty, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage ValidateOptionDisplayedInLicenseStateDropdown(string option)
        {
            var node = GetLastNode();
            try
            {
                LicenseStateDropdown.Click();
                WaitForElementVisible(_eleDropdownOptions);
                string[] optionList = DropdownOptionsLabel.Select(x => x.Text).ToArray();
                string actualOptionsList = string.Join(" - ", optionList);
                if (optionList.Contains(option))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateOptionDisplayedInLicenseStateDropdown, option), actualOptionsList);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateOptionDisplayedInLicenseStateDropdown, option), actualOptionsList);
                }
                LicenseStateDropdown.SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateOptionDisplayedInLicenseStateDropdown, option), e);
            }
            return this;
        }

        public DriverEnterPage ValidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<DriverEnterPage>();
        }

        public DriverEnterPage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<DriverEnterPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Driver Enter page is displayed";
            public static string ValidateDriverNameIsFilledOut = "Validate Driver name is filled out";
            public static string ValidateDriverInfoIsFilledOut = "Validate Driver Information is filled out";
            public static string ValidateLicenseStateDisplayedInAlphabeticalWithSpecifiedStateAtBottom = "Validate license state is displayed in alphabetical order with {0} at the bottom";
            public static string ValidateLicenseStateDisplayedInAlphabeticalWithoutSpecifiedStateAtBottom = "Validate license state is displayed in alphabetical order with {0} not at the bottom";
            public static string ValidateLicenseStateIsFilledOut = "Validate license state is filled out";
            public static string ValidateDriverNameFieldsDisplayedAndDisabled = "Validate Driver name fields are displayed and disabled";
            public static string ValidateDriverFieldsAreEmpty = "Validate Driver fields are empty";
            public static string ValidateOptionDisplayedInLicenseStateDropdown = "Validate ({0}) option displayed in license state dropdown";
        }
        #endregion
    }
}