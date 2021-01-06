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
    public class BusinessInformationPage : LandingPage
    {
        #region Locators
        private By _txtDotNumber => By.XPath("//*[contains(@class, 'automation-id-dotNumber')]//input");
        private By _txtBusinessName => By.XPath("//*[contains(@class, 'automation-id-nameOfBusiness')]//input");
        private By _txtDoingBusinessAs => By.XPath("//*[contains(@class, 'automation-id-doingBusinessAs')]//input");
        private By _txtLine1 => By.XPath("//*[contains(@c|ass, 'automation-id-line1')]//input");
        private By _txtLine2 => By.XPath("//*[contains(@c|ass, 'automation-id-line2')]//input");
        private By _txtCity => By.XPath("//*[contains(@class, 'automation-id-city')]//input");
        private By _drpState => By.XPath("//*[contains(@class, 'automation-id-state')]//mat-select");
        private By _txtZip => By.XPath("//*[contains(@class, 'automation-id-zip')]//input");
        private By _txtFirstName => By.XPath("//*[contains(@class, 'automation-id-firstName')]//input");
        private By _txtMiddleInitial => By.XPath("//*[contains(@class, 'automation-id-middleInitial')]//input");
        private By _txtLastName => By.XPath("//*[contains(@c|ass, 'automation-id-lastName')]//input");
        private By _lbl1stQuestion => By.XPath("(//formly-field)[3]//h1");
        #endregion

        #region Elements
        public IWebElement DotNumberTextBox => StableFindElement(_txtDotNumber);
        public IWebElement BusinessNameTextBox => StableFindElement(_txtBusinessName);
        public IWebElement FirstNameTextBox => StableFindElement(_txtFirstName);
        public IWebElement MiddleInitialTextBox => StableFindElement(_txtMiddleInitial);
        public IWebElement LastNameTextBox => StableFindElement(_txtLastName);
        public IWebElement DoingBusinessAsTextBox => StableFindElement(_txtDoingBusinessAs);
        public IWebElement Line1TextBox => StableFindElement(_txtLine1);
        public IWebElement Line2TextBox => StableFindElement(_txtLine2);
        public IWebElement CityTextBox => StableFindElement(_txtCity);
        public IWebElement StateDropdown => StableFindElement(_drpState);
        public IWebElement ZipTextBox => StableFindElement(_txtZip);
        public IWebElement QuestionLabel => StableFindElement(_lbl1stQuestion);
        #endregion

        #region Business Methods
        public BusinessInformationPage() : base()
        {
            Url = "forms/business—information";
            RequiredElementLocator = _txtLine1;
        }

        [ExtentStepNode]
        public BusinessInformationPage InputBusinessInformation(BusinessInformation businessInformation)
        {
            GetLastNode().LogDataInfo(businessInformation);
            ParameterValidator.ValidateNotNull(businessInformation, "Business Information");
            if (businessInformation.CustomerFullName != null)
            {
                FirstNameTextBox.InputText(businessInformation.CustomerFullName.FirstName);
                MiddleInitialTextBox.InputText(businessInformation.CustomerFullName.MiddleInitial);
                LastNameTextBox.InputText(businessInformation.CustomerFullName.LastName);
            }
            else
            {
                BusinessNameTextBox.InputText(businessInformation.CustomerName);
            }
            DoingBusinessAsTextBox.InputText(businessInformation.DBA);
            if (businessInformation.Address.DoesHandleConfirmAddressDialog)
            {
                Line1TextBox.InputText(businessInformation.Address.Line1 + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                //Line2TextBox.InputText(businessInformation.Address.Line2 + Keys.Tab);
                CityTextBox.InputText(businessInformation.Address.City + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                StateDropdown.SelectByText(businessInformation.Address.State);
                CloseConfirmAddressDialogIfExist();
                ZipTextBox.InputText(businessInformation.Address.Zip + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
            }
            else
            {
                Line1TextBox.InputText(businessInformation.Address.Line1 + Keys.Tab);
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                //Line2TextBox.InputText(businessInformationAddress.Line2 + KeysIab);
                CityTextBox.InputText(businessInformation.Address.City + Keys.Tab);
                StateDropdown.SelectByText(businessInformation.Address.State);
                ZipTextBox.InputText(businessInformation.Address.Zip + Keys.Tab);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage InputCustomerNameAndDBA(BusinessInformation businessInformation)
        {
            GetLastNode().LogDataInfo(businessInformation);
            ParameterValidator.ValidateNotNull(businessInformation, "Business Information");
            if (businessInformation.CustomerFullName != null)
            {
                FirstNameTextBox.InputText(businessInformation.CustomerFullName.FirstName);
                MiddleInitialTextBox.InputText(businessInformation.CustomerFullName.MiddleInitial);
                LastNameTextBox.InputText(businessInformation.CustomerFullName.LastName);
            }
            else
            {
                BusinessNameTextBox.InputText(businessInformation.CustomerName);
            }

            DoingBusinessAsTextBox.InputText(businessInformation.DBA);

            return this;

        }

        [ExtentStepNode]

        public BusinessInformationPage InputBusinesslnformationWithoutState(BusinessInformation businessInformation)
        {
            GetLastNode().LogDataInfo(businessInformation);
            ParameterValidator.ValidateNotNull(businessInformation, "Business Information");
            if (businessInformation.CustomerFullName != null)
            {
                FirstNameTextBox.InputText(businessInformation.CustomerFullName.FirstName);
                MiddleInitialTextBox.InputText(businessInformation.CustomerFullName.MiddleInitial);
                LastNameTextBox.InputText(businessInformation.CustomerFullName.LastName);
            }
            else
            {
                BusinessNameTextBox.InputText(businessInformation.CustomerName);
            }

            DoingBusinessAsTextBox.InputText(businessInformation.DBA);

            if (businessInformation.Address.DoesHandleConfirmAddressDialog)
            {
                Line1TextBox.InputText(businessInformation.Address.Line1 + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                //Line2TextBox.InputText(businessInformation.Address.Line2 + Keys.Tab);
                CityTextBox.InputText(businessInformation.Address.City + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
                ZipTextBox.InputText(businessInformation.Address.Zip + Keys.Tab);
                CloseConfirmAddressDialogIfExist();
            }
            else
            {
                Line1TextBox.InputText(businessInformation.Address.Line1 + Keys.Tab);
                // Close SmartyStreet dropdown if exists
                CloseDropdownIfOpened();
                //Line2TextBox.InputText(businessInformation.Address.Line2 + Keys.Tab);
                CityTextBox.InputText(businessInformation.Address.City + Keys.Tab);
                ZipTextBox.InputText(businessInformation.Address.Zip + Keys.Tab);
            }

            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage SelectState(string state, bool isAddressCompleted = false)
        {
            GetLastNode().Info("Se|ect state: " + state);
            if (IsElementDisplayed(_eleDropdownOptions))
            {
                StateDropdown.SelectByText(state, false);
            }
            else
            {
                StateDropdown.SelectByText(state);
            }
            if (isAddressCompleted)
            {
                //CloseConfirmAddressDialogIfExist();
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage OpenStateDropdown()
        {
            StateDropdown.Click();
            WaitForElementVisible(_eleDropdownOptions);
            return this;
        }

        public PrimaryOfficerPage SelectNextButton()
        {
            return SelectNextButton<PrimaryOfficerPage>();
        }

        public EntityTypePage SelectPreviousButton()
        {
            return SelectPreviousButton<EntityTypePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public BusinessInformationPage ValidateBussinesNameDispIayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtBusinessName))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBussinesNameDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBussinesNameDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBussinesNameDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateFMLNameNotDisplayed()
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
                    if (!IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateFMLNameNotDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateFMLNameNotDisplayed, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFMLNameNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateDBADisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDoingBusinessAs))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDBADisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDBADisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDBADisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateAddressFieldsDisplayed()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("Line 1", _txtLine1),
                    //new KeyValuePair<string, By>("Line 2", _txtLine2),
                    new KeyValuePair<string, By>("City", _txtCity),
                    new KeyValuePair<string, By>("State", _drpState),
                    new KeyValuePair<string, By>("Zip", _txtZip)
                };
                foreach (var item in locators)
                {
                    if (IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAddressFieldsDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAddressFieldsDisplayed, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddressFieldsDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage Validate1stQuestionCorrect(string question)
        {
            var node = GetLastNode();
            try
            {
                if (QuestionLabel.Text.Trim() == question)
                {
                    SetPassValidation(node, ValidationMessage.Validate1stQuestionCorrect, expectedValue: question);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.Validate1stQuestionCorrect, expectedValue: question, actualValue: QuestionLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.Validate1stQuestionCorrect, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateBusinessInformationPageDisplayed()
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
        public BusinessInformationPage ValidateBussinesNameDisplayedInRespectiveField(string businessName)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtBusinessName))
                {
                    if (BusinessNameTextBox.GetValue() == businessName)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateBussinesNameDisplayedInRespectiveField, expectedValue: businessName);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateBussinesNameDisplayedInRespectiveField, expectedValue: businessName, actualValue: BusinessNameTextBox.GetValue());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBussinesNameDisplayedInRespectiveField, expectedValue: businessName);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBussinesNameDisplayedInRespectiveField, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateBusinessNameIsFilledOut(string customerName)
        {
            var node = GetLastNode();
            try
            {
                if (BusinessNameTextBox.GetValue() == customerName)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessNameIsFilledOut, expectedValue: customerName);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessNameIsFilledOut, expectedValue: customerName, actualValue: BusinessNameTextBox.GetValue());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessNameIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateDBAIsFilledOut(string dBA)
        {
            var node = GetLastNode();
            try
            {
                if (DoingBusinessAsTextBox.GetValue() == dBA)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDBAIsFilledOut, expectedValue: dBA);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDBAIsFilledOut, expectedValue: dBA, actualValue: DoingBusinessAsTextBox.GetValue());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDBAIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateDBADisplayedInRespectiveField(string dBA)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDoingBusinessAs))
                {
                    if (DoingBusinessAsTextBox.GetValue() == dBA)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDBADisplayedInRespectiveField, expectedValue: dBA);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDBADisplayedInRespectiveField, expectedValue: dBA, actualValue: DoingBusinessAsTextBox.GetValue());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDBADisplayedInRespectiveField, expectedValue: dBA);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDBADisplayedInRespectiveField, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateAddressFieldsDisplayedInRespectiveFields(Address address)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(address, "Address");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("Line 1", new string[] { address.Line1, Line1TextBox.GetValue() }),
                    //new KeyValuePair<string, string[]>("Line 2", new string[] { address.Line2, Line2TextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("City", new string[] { address.City, CityTextBox.GetValue() }),
                    new KeyValuePair<string, string[]>("5tate", new string[] { address.State, StateDropdown.Text }),
                    new KeyValuePair<string, string[]>("Zip", new string[] { address.Zip, ZipTextBox.GetValue() })
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAddressFieldsDisplayedInRespectiveFields, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAddressFieldsDisplayedInRespectiveFields, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddressFieldsDisplayedInRespectiveFields, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateAddressFieldsAreFilledOut(Address address)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(address, "Address");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("Line 1", new string[] { address.Line1, Line1TextBox.GetValue()}),
                    //new KeyValuePair<string, string[]>("Line 2", new string[] { address.Line2, Line2TextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("City", new string[] { address.City, CityTextBox.GetValue()}),
                    new KeyValuePair<string, string[]>("State", new string[] { address.State, StateDropdown.Text }),
                    new KeyValuePair<string, string[]>("Zip", new string[] { address.Zip, ZipTextBox.GetValue()})
                };
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAddressFieldsAreFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAddressFieldsAreFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddressFieldsAreFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateStateNotDisplayedInDropdown(string state)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_eleDropdownOption(state)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateStateNotDisplayedInDropdown, state);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateStateNotDisplayedInDropdown, state);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateStateNotDisplayedInDropdown, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateStateDisplayedInDropdown(string state)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_eleDropdownOption(state)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateStateDisplayedInDropdown, state);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateStateDisplayedInDropdown, state);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateStateDisplayedInDropdown, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateBusinessInfoFieldsEditable()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, bool>> comparisons = new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("Business Name", IsElementEnabled(_txtBusinessName)),
                    new KeyValuePair<string, bool>("Doing Business As", IsElementEnabled(_txtDoingBusinessAs))
                };
                foreach (var item in comparisons)
                {
                    if (item.Value)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateBusinessInfoFieldsEditable, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateBusinessInfoFieldsEditable, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessInfoFieldsEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessInformationPage ValidateAddressFieldsNotEditable()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, bool>> comparisons = new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("Line 1", !IsElementEnabled(_txtLine1)),
                    new KeyValuePair<string, bool>("City", !IsElementEnabled(_txtCity)),
                    new KeyValuePair<string, bool>("State", !IsDropdownEnabled(_drpState)),
                    new KeyValuePair<string, bool>("Zip", !IsElementEnabled(_txtZip))
                };
                foreach (var item in comparisons)
                {
                    if (item.Value)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAddressFieldsNotEditable, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAddressFieldsNotEditable, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddressFieldsNotEditable, e);
            }
            return this;
        }

        public BusinessInformationPage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<BusinessInformationPage>();
        }

        public BusinessInformationPage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<BusinessInformationPage>();
        }

        public BusinessInformationPage ValidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<BusinessInformationPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Business Information page is displayed";
            public static string ValidateBussinesNameDisplayed = "Validate Business name is displayed";
            public static string Validate1stQuestionCorrect = "Validate 1st question text is correct";
            public static string ValidateFMLNameNotDisplayed = "Validate F/M/Lname are not displayed";
            public static string ValidateAddressFieldsDisplayed = "Validate Address fields are displayed";
            public static string ValidateDBADisplayed = "Validate DBA is displayed";
            public static string ValidateBussinesNameDisplayedInRespectiveField = "Validate Business name is displayed in respective field";
            public static string ValidateBusinessNameIsFilledOut = "Validate Business Name is filled out";
            public static string ValidateAddressFieldsDisplayedInRespectiveFields = "Validate Address fields are displayed in respective fields";
            public static string ValidateAddressFieldsAreFilledOut = "Validate Address fields are filled out";
            public static string ValidateDBADisplayedInRespectiveField = "Validate DBA is displayed in respective field";
            public static string ValidateDBAIsFilledOut = "Validate DBA is filled out";
            public static string ValidateStateNotDisplayedInDropdown = "Validate State is not displayed in drop down";
            public static string ValidateStateDisplayedInDropdown = "Validate State is displayed in drop down";
            public static string ValidateBusinessInfoFieldsEditable = "Validate Business info fields can be edited";
            public static string ValidateAddressFieldsNotEditable = "Validate Address fields can not be edited";

        }
        #endregion
    }
}