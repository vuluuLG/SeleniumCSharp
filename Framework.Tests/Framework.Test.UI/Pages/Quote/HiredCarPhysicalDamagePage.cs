using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class HiredCarPhysicalDamagePage : LandingPage
    {
        #region Locators
        private By _btnHiredCarPhysicalDamage(string item) => By.XPath($"//*[contains(@class, 'automation-id-hasCoverage ')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _txtMaximumValue => By.XPath("//*[contains(@class, 'automation-id-maximumValue')]//input");
        private By _drpMaximumValue => By.XPath("//*[contains(@class, 'automation-id-maximumValue')]//mat-select");
        private By _drpDeductible => By.XPath("//*[contains(@class, 'automation-id-deductible')]//mat-select");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation—id-hasCoverage')]//h1");
        #endregion

        #region Elements
        public IWebElement HiredCarPhysicalDamageButton(string item) => StableFindElement(_btnHiredCarPhysicalDamage(item));
        public IWebElement MaximumValueTextBox => StableFindElement(_txtMaximumValue);
        public IWebElement MaximumValueDropdown => StableFindElement(_drpMaximumValue);
        public IWebElement DeductibleDropdown => StableFindElement(_drpDeductible);
        #endregion

        #region Business Methods
        public HiredCarPhysicalDamagePage() : base()
        {
            Url = "forms/hired-car-physical-damage";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public HiredCarPhysicalDamagePage EnterHiredCarPhysDamage(HiredCarPhysicalDamage hiredCarPhysicalDamage)
        {
            GetLastNode().LogDataInfo(hiredCarPhysicalDamage);
            ParameterValidator.ValidateNotNull(hiredCarPhysicalDamage, "Hired Car Physical Damage");
            HiredCarPhysicalDamageButton(hiredCarPhysicalDamage.UseHiredCarPhysicalDamageCoverage).Click();
            if (hiredCarPhysicalDamage.UseHiredCarPhysicalDamageCoverage == AnswerOption.Yes)
            {
                if (IsElementDisplayed(_txtMaximumValue, 3))
                {
                    StableSelectLimitAndDeductible(_txtMaximumValue, _drpDeductible, hiredCarPhysicalDamage.MaximumValue, hiredCarPhysicalDamage.Deductible);
                }
                else
                {
                    StableSelectLimitAndDeductible(_drpMaximumValue, _drpDeductible, hiredCarPhysicalDamage.MaximumValue, hiredCarPhysicalDamage.Deductible);
                }

                WaitForElementEnabled(_btnNext);
            }
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public HiredCarPhysicalDamagePage ValidateHiredCarPhysicalDamagePageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Hired Car Physical Damage page is displayed";
        }
        #endregion
    }
}

