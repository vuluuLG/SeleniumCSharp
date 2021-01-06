using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class DotNumberPage : LandingPage
    {
        #region Locators
        private By _lblDotNumberTitle => By.XPath("//*[contains(@class, 'automation-id-dotNumber')]//h1");
        private By _lnkDotNumberUnknown => By.XPath("//*[contains(@class, 'automation-id-isUnknownDotNumber')]//button");
        private By _txtDotNumber => By.XPath("//*[contains(@class, 'automation-id-dotNumber')]//input");
        #endregion

        #region Elements
        public IWebElement DotNumberTitleLabel => StableFindElement(_lblDotNumberTitle);
        public IWebElement DotNumberTextBox => StableFindElement(_txtDotNumber);
        public IWebElement DotNumberUnknownLinkButton => StableFindElement(_lnkDotNumberUnknown);
        #endregion

        #region Business Methods
        public DotNumberPage() : base()
        {
            Url = "forms/dot-number";
            RequiredElementLocator = _txtDotNumber;
        }

        [ExtentStepNode]
        public DotNumberPage InputDotNumber(string dotNumber)
        {
            GetLastNode().Info("Input dot number: " + dotNumber);
            DotNumberTextBox.InputText(dotNumber);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public EntityTypePage SelectUnknownDotNumber()
        {
            DotNumberUnknownLinkButton.Click();
            var page = new EntityTypePage();
            page.WaitForPageLoad();
            return page;
        }

        public EntityTypePage SelectNextButton()
        {
            return SelectNextButton<EntityTypePage>();
        }

        public EffectiveDatePage SelectPreviousButton()
        {
            return SelectPreviousButton<EffectiveDatePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberTitleDisplayedCorrectly(string title)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblDotNumberTitle))
                {
                    if (DotNumberTitleLabel.Text.Trim().Contains(title))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDotNumberTitleDisplayedCorrectly, expectedValue: title);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDotNumberTitleDisplayedCorrectly, expectedValue: title, actualValue: DotNumberTitleLabel.Text.Trim());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberTitleDisplayedCorrectly, expectedValue: title);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberTitleDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberUnknownDisplayedCorrectly(string value)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lnkDotNumberUnknown))
                {
                    if (DotNumberUnknownLinkButton.Text.Trim() == value)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDotNumberUnknownDisplayedCorrectly, expectedValue: value);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDotNumberUnknownDisplayedCorrectly, expectedValue: value, actualValue: DotNumberUnknownLinkButton.Text.Trim());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberUnknownDisplayedCorrectly, expectedValue: value);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberUnknownDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberDisplayedAndEmpty()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDotNumber) && DotNumberTextBox.GetValue().Trim().Length == 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDotNumberDisplayedAndEmpty);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberDisplayedAndEmpty);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberDisplayedAndEmpty, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberEditable()
        {
            var node = GetLastNode();
            try
            {
                if (DotNumberTextBox.Enabled)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDotNumberEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberNotEditable()
        {
            var node = GetLastNode();
            try
            {
                if (!DotNumberTextBox.Enabled)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDotNumberNotEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberNotEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberNotEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DotNumberPage ValidateDotNumberPageDisplayed()
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
        public DotNumberPage ValidateDotNumberDisplayedInDotNumberField(string dotNumber)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_txtDotNumber))
                {
                    if (DotNumberTextBox.GetValue() == dotNumber)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDotNumberDisplayedInDotNumberField, expectedValue: dotNumber);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDotNumberDisplayedInDotNumberField, expectedValue: dotNumber, actualValue: DotNumberTextBox.GetValue());
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDotNumberDisplayedInDotNumberField, expectedValue: dotNumber);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDotNumberDisplayedInDotNumberField, e);
            }
            return this;
        }

        public DotNumberPage ValidateNextButtonEnabIed()
        {
            return ValidateNextButtonEnabled<DotNumberPage>();
        }

        public DotNumberPage ValidateNextButtonDisplayedAndDisabled()
        {
            return ValidateNextButtonDisplayedAndDisabled<DotNumberPage>();
        }

        public DotNumberPage ValidateNextButtonDisplayedAndEnabIed()
        {
            return ValidateNextButtonDisplayedAndEnabled<DotNumberPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Dot Number page is displayed";
            public static string ValidateDotNumberDisplayedAndEmpty = "Validate Dot Number is displayed and empty";
            public static string ValidateDotNumberTitleDisplayedCorrectly = "Validate Dot Number Title is displayed correctly";
            public static string ValidateDotNumberUnknownDisplayedCorrectly = "Validate Dot Number Unknown link button is displayed correctly";
            public static string ValidateDotNumberDisplayedInDotNumberField = "Validate Dot Number is displayed in dot number field";
            public static string ValidateDotNumberEditable = "Validate Dot Number can be edited";
            public static string ValidateDotNumberNotEditable = "Validate Dot Number can not be edited";
        }
        #endregion
    }
}