using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class EffectiveDatePage : LandingPage
    {
        #region Locators
        private By _lblEffectiveDateTitle => By.XPath("//*[contains(@class, 'automation-id-value')]//h1");
        private By _txtDatePicker => By.XPath("//*[contains(@class, 'automation-id-value')]//input");
        private By _datePicker => By.XPath("//*[contains(@class, 'automation-id-value')]//mat-datepicker-toggle");
        #endregion

        #region Elements
        public IWebElement EffectiveDateTitleLabel => StableFindElement(_lblEffectiveDateTitle);
        public IWebElement EffectiveDateTextBox => StableFindElement(_txtDatePicker);
        public IWebElement EffectiveDatePicker => StableFindElement(_datePicker);
        #endregion

        #region Business Methods
        public EffectiveDatePage() : base()
        {
            Url = "forms/effective-date";
            RequiredElementLocator = _txtDatePicker;
        }

        [ExtentStepNode]
        public EffectiveDatePage SelectEffectiveDate(string date)
        {
            GetLastNode().Info("Select effective date: " + date);
            EffectiveDateTextBox.InputDate(date);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public DotNumberPage SelectNextButton()
        {
            return SelectNextButton<DotNumberPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public EffectiveDatePage ValidateEffectiveDateTitleDisplayed(string title)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblEffectiveDateTitle))
                {
                    if (EffectiveDateTitleLabel.Text.Trim() == title)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEffectiveDateTitleDisplayed, expectedValue: title);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEffectiveDateTitleDisplayed, expectedValue: title, actualValue: EffectiveDateTitleLabel.Text.Trim());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateEffectiveDateTitleDisplayed, expectedValue: title);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEffectiveDateTitleDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public EffectiveDatePage ValidateDatePickerDisplayedAndEmpty()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDatePicker) && EffectiveDateTextBox.GetValue().Trim().Length == 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDatePickerDisplayedAndEmpty);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDatePickerDisplayedAndEmpty);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDatePickerDisplayedAndEmpty, e);
            }
            return this;
        }

        [ExtentStepNode]
        public EffectiveDatePage ValidateEffectiveDatePickerEditable()
        {
            var node = GetLastNode();
            try
            {
                if (EffectiveDateTextBox.Enabled)
                {
                    SetPassValidation(node, ValidationMessage.ValidateEffectiveDatePickerEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateEffectiveDatePickerEditable);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEffectiveDatePickerEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public EffectiveDatePage ValidateEffectiveDatePageDisplayed()
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
        public EffectiveDatePage ValidateEffectiveDateDisplayedInDatePicker(string effectiveDate)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDatePicker))
                {
                    if (EffectiveDateTextBox.GetValue() == effectiveDate)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEffectiveDateDisplayedInDatePicker, expectedValue: effectiveDate);

                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEffectiveDateDisplayedInDatePicker, expectedValue: effectiveDate, actualValue: EffectiveDateTextBox.GetValue());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateEffectiveDateDisplayedInDatePicker, expectedValue: effectiveDate);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEffectiveDateDisplayedInDatePicker, e);
            }
            return this;
        }

        public EffectiveDatePage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<EffectiveDatePage>();
        }

        public EffectiveDatePage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<EffectiveDatePage>();
        }

        public EffectiveDatePage ValidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<EffectiveDatePage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Effective Date page is displayed";
            public static string ValidateEffectiveDateTitleDisplayed = "Validate Effective Date title is displayed correctly";
            public static string ValidateDatePickerDisplayedAndEmpty = "Validate Date Picker is displayed and empty";
            public static string ValidateEffectiveDateDisplayedInDatePicker = "Validate Effective Date is display in the date picker";
            public static string ValidateEffectiveDatePickerEditable = "Validate Effective Date picker can be edited";
        }
        #endregion
    }
}


