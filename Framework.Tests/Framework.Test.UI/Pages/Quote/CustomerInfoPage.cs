using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CustomerInfoPage : LandingPage
    {
        #region Locators
        private By _lbl1stQuestion => By.XPath("//*[contains(@class, 'automation-id-businessDateRange')]//h1");
        private By _lbl2ndQuestion => By.XPath("//*[contains(@class, 'automation-id-filingType')]//h1");
        private By _lbl3rdQuestion => By.XPath("//*[contains(@class, 'automation-id-liabilityLossCount')]//h1");
        private By _drpBusinessDateRange => By.XPath("//*[contains(@class, 'automation-id-businessDateRange')]//mat-select");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-businessDateRange')]//h1");
        private By _btnFilingType(string type) => By.XPath($"//*[contains(@class, 'automation-id-filing')]//mat-radio-button[.//*[contains(normalize-space(text()),'{type}')]]");
        private By _btnFilingTypes => By.XPath("//*[contains(@class, 'automation-id-filing')]//mat-radio-button");
        private By _txtLiabilityLosses => By.XPath("//*[contains(@class, 'automation-id-liabilityLossCount')]//input");
        #endregion

        #region Elements
        public IWebElement BusinessDateRangeDropdown => StableFindElement(_drpBusinessDateRange);
        public IWebElement QuestionLabeI => StableFindElement(_lblQuestion);
        public IWebElement FilingTypeButton(string type) => StableFindElement(_btnFilingType(type));
        public ReadOnlyCollection<IWebElement> FilingTypeButtonList => StableFindElements(_btnFilingTypes);
        public IWebElement LiabilityLossesTextbox => StableFindElement(_txtLiabilityLosses);
        #endregion

        #region Business Methods
        public CustomerInfoPage() : base()
        {
            Url = "forms/customer-info";
            RequiredElementLocator = _drpBusinessDateRange;
        }

        [ExtentStepNode]
        public CustomerInfoPage EnterCustomerInfo(CustomerAdditionalInformation customerAdditionalInformation)
        {
            GetLastNode().LogDataInfo(customerAdditionalInformation);
            ParameterValidator.ValidateNotNull(customerAdditionalInformation, "Customer Additional Information");
            BusinessDateRangeDropdown.SelectByText(customerAdditionalInformation.BusinessDateRange);
            FilingTypeButton(customerAdditionalInformation.FilingType).Click();
            LiabilityLossesTextbox.InputText(customerAdditionalInformation.LiabilityLosses);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public VehicleInfoPage SelectNextButton()
        {
            VehicleInfoPage vehicleInfoPage = SelectNextButton<VehicleInfoPage>();
            Wait(10); // Default coverages are created asynchronously, giving some time for the action to finish.
            return vehicleInfoPage;
        }

        public PrimaryOfficerPage SelectPreviousButton()
        {
            return SelectPreviousButton<PrimaryOfficerPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CustomerInfoPage ValidateBusinessDateRangeDropdownMuItiple()
        {
            var node = GetLastNode();
            try
            {
                BusinessDateRangeDropdown.Click();
                WaitForElementVisible(_eleDropdownOptions);
                int count = 0;
                foreach (var item in DropdownOptionsLabel)
                {
                    if (item.Text != null)
                    {
                        count++;
                    }
                }
                if (count > 1)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownMultiple);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownMultiple);
                }
                BusinessDateRangeDropdown.SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownMultiple, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateBusinessDateRangeDropdownDisplayedAndUnselected()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_drpBusinessDateRange) && BusinessDateRangeDropdown.Text.Trim().Length == 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisplayedAndUnselected);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisplayedAndUnselected);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisplayedAndUnselected, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateBusinessDateRangeDisplayedInTheDatePicker(string businessDateRange)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_drpBusinessDateRange))
                {
                    if (BusinessDateRangeDropdown.Text == businessDateRange)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateBusinessDateRangeDisplayedInTheDatePicker, expectedValue: businessDateRange);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateBusinessDateRangeDisplayedInTheDatePicker, expectedValue: businessDateRange, actualValue: BusinessDateRangeDropdown.Text);
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessDateRangeDisplayedInTheDatePicker, expectedValue: businessDateRange);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessDateRangeDisplayedInTheDatePicker, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateCustomerInfoPageDisplayed()
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
        public CustomerInfoPage ValidateFilingTypesDisplayedAndUnselected(string[] filingTypes)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(filingTypes, "Filing Types");
                foreach (var item in filingTypes)
                {
                    if (IsElementDisplayed(_btnFilingType(item)) && FilingTypeButton(item).GetAttribute("class").Contains("checked"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateFilingTypesDisplayedAndUnselected, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateFilingTypesDisplayedAndUnselected, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFilingTypesDisplayedAndUnselected, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateFiIingTypeIsSelected(string filingType)
        {
            var node = GetLastNode();
            try
            {
                if (FilingTypeButton(filingType).GetAttribute("cIass").Contains("checked"))
                {
                    SetPassValidation(node, ValidationMessage.ValidateFilingTypeIsSelected, filingType);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFilingTypeIsSelected, filingType);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFilingTypeIsSelected, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateLiabilityLossesDisplayedAndEmpty()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtLiabilityLosses) && LiabilityLossesTextbox.GetValue().Trim().Length == 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedAndEmpty);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedAndEmpty);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedAndEmpty, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateLiabilityLossesDisplayedInTheField(string liabilityLosses)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtLiabilityLosses))
                {
                    if (LiabilityLossesTextbox.GetValue() == liabilityLosses)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedInTheField, expectedValue: liabilityLosses);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedInTheField, expectedValue: liabilityLosses, actualValue: LiabilityLossesTextbox.GetValue());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedInTheField, expectedValue: liabilityLosses);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLiabilityLossesDisplayedInTheField, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateFinalQuestionsDisplayed()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, By>> locators = new List<KeyValuePair<string, By>>()
                {
                    new KeyValuePair<string, By>("lst Question", _lbl1stQuestion),
                    new KeyValuePair<string, By>("2nd Question", _lbl2ndQuestion),
                    new KeyValuePair<string, By>("3rd Question", _lbl3rdQuestion)
                };
                foreach (var item in locators)
                {
                    if (IsElementDisplayed(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateFinalQuestionsDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateFinalQuestionsDisplayed, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFinalQuestionsDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateCustomerInfoFieldsEditable()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, bool>> comparisons = new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("Business Date Range", IsDropdownEnabled(_drpBusinessDateRange)),
                    new KeyValuePair<string, bool>("Fi|ing Type Buttons", FilingTypeButtonList.All(x => x.Enabled)),
                    new KeyValuePair<string, bool>("Liability Losses", IsElementEnabled(_txtLiabilityLosses))
                };
                foreach (var item in comparisons)
                {
                    if (item.Value)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCustomerInfoFieldsEditable, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCustomerInfoFieldsEditable, item.Key);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCustomerInfoFieldsEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage ValidateBusinessDateRangeDropdownDisabled()
        {
            var node = GetLastNode();
            try
            {
                if (!IsDropdownEnabled(_drpBusinessDateRange))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessDateRangeDropdownDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage VaIidateFiIingTypesEditabIe()
        {
            var node = GetLastNode();
            try
            {
                if (FilingTypeButtonList.All(x => x.Enabled))
                {
                    SetPassValidation(node, ValidationMessage.ValidateFilingTypesEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFilingTypesEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFilingTypesEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CustomerInfoPage VaIidateLiabilityLossesEditabIe()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementEnabled(_txtLiabilityLosses))
                {
                    SetPassValidation(node, ValidationMessage.ValidateLiabilityLossesEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateLiabilityLossesEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLiabilityLossesEditable, e);
            }
            return this;
        }

        public CustomerInfoPage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<CustomerInfoPage>();
        }

        public CustomerInfoPage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<CustomerInfoPage>();
        }

        public CustomerInfoPage ValidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<CustomerInfoPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Customer Info page is displayed";
            public static string ValidateBusinessDateRangeDropdownDisplayedAndUnselected = "Validate Business Date Range dropdown is displayed and unselected";
            public static string ValidateBusinessDateRangeDropdownMultiple = "Validate Business Date Range dropdown is multiple";
            public static string ValidateBusinessDateRangeDisplayedInTheDatePicker = "Validate Business Date Range is displayed in the date picker";
            public static string ValidateFilingTypesDisplayedAndUnselected = "Validate Filing Type buttons are displayed and unselected";
            public static string ValidateFilingTypeIsSelected = "Validate Filing Type button is selected";
            public static string ValidateLiabilityLossesDisplayedAndEmpty = "Validate Liability Losses is displayed and empty";
            public static string ValidateLiabilityLossesDisplayedInTheField = "Validate Liability Losses is displayed in the field";
            public static string ValidateFinalQuestionsDisplayed = "Validate final questions are displayed";
            public static string ValidateCustomerInfoFieldsEditable = "Validate Customer Info fields can be edited";
            public static string ValidateFilingTypesEditable = "Validate Filing Type buttons can be edited";
            public static string ValidateLiabilityLossesEditable = "Validate Liability Losses can be edited";
            public static string ValidateBusinessDateRangeDropdownDisabled = "Validate Business Date Range dropdown is disabled";
        }
        #endregion
    }
}