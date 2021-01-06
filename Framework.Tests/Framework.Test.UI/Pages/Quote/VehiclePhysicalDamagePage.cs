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
    public class VehiclePhysicalDamagePage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-hasCoverage‘)]//h1");
        private By _btnPhysicalDamage(string item) => By.XPath($"//*[contains(@class, 'automation-id-hasCoverage')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _txtVehicleValue => By.XPath($"//*[contains(@class, 'automation-id-vehicleValue')]//input");
        private By _drpDeductible => By.XPath("//*[contains(@class, 'automation-id-deductible')]//mat-select");
        private By _lblFullGlassCoverage => By.XPath("//*[contains(@class, 'automation-id-fullGlassCoverageValue')]//h1");
        private By _btnCesForRental(string item) => By.XPath($"//*[contains(@class, 'automation-id-hasCesForRental')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnFullGlassCoverage(string item) => By.XPath($"//*[contains(@class, 'automation-id-fullGlassCoverageValue')]/lmat-radio-button[.//*[normalize-space(text())='{item}']]");
        #endregion

        #region Elements
        public IWebElement PhysicalDamageButton(string item) => StableFindElement(_btnPhysicalDamage(item));
        public IWebElement VehicleValueTextBox => StableFindElement(_txtVehicleValue);
        public IWebElement DeductibleDropdown => StableFindElement(_drpDeductible);
        public IWebElement CesForRentalButton(string item) => StableFindElement(_btnCesForRental(item));
        public IWebElement FullGlassCoverageButton(string item) => StableFindElement(_btnFullGlassCoverage(item));
        #endregion

        #region Business Methods
        public VehiclePhysicalDamagePage() : base()
        {
            Url = "forms/physical-damage";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage EnterPhysicalDamage(PhysicalDamage physicalDamage)
        {
            GetLastNode().LogDataInfo(physicalDamage);
            ParameterValidator.ValidateNotNull(physicalDamage, "Physical Damage");
            PhysicalDamageButton(physicalDamage.UsePhysicalDamageCoverage).Click();
            if (physicalDamage.UsePhysicalDamageCoverage == AnswerOption.Yes)
            {
                StableSelectLimitAndDeductible(_txtVehicleValue, _drpDeductible, physicalDamage.StatedValue, physicalDamage.Deductible);
                if (physicalDamage.CesForRental != null)
                {
                    CesForRentalButton(physicalDamage.CesForRental).Click();
                }
                if (physicalDamage.FullGlassCoverage != null)
                {
                    FullGlassCoverageButton(physicalDamage.FullGlassCoverage).Click();
                }
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage SelectPhysicalDamage(string physicalDamage)
        {
            var node = GetLastNode();
            node.Info("Select Physical Damage: " + physicalDamage);
            PhysicalDamageButton(physicalDamage).Click();
            if (physicalDamage == AnswerOption.Yes)
                WaitForElementVisible(_txtVehicleValue);
            return this;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage EnterPhysicalDamageValue(PhysicalDamage physicalDamage)
        {
            GetLastNode().LogDataInfo(physicalDamage);
            ParameterValidator.ValidateNotNull(physicalDamage, "Physical Damage");
            StableSelectLimitAndDeductible(_txtVehicleValue, _drpDeductible, physicalDamage.StatedValue, physicalDamage.Deductible);
            if (physicalDamage.CesForRental != null)
            {
                CesForRentalButton(physicalDamage.CesForRental).Click();
            }
            if (physicalDamage.FullGlassCoverage != null)
            {
                FullGlassCoverageButton(physicalDamage.FullGlassCoverage).Click();
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehiclePhysicalDamagePage ValidatePhysicalDamageQuestiontDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblQuestion))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePhysicalDamageQuestiontDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePhysicalDamageQuestiontDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePhysicalDamageQuestiontDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage ValidateFullGlassCoverageLabelDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblFullGlassCoverage))
                {
                    SetPassValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage ValidateFullGlassCoverageLabelNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblFullGlassCoverage))
                {
                    SetPassValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelNotDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelNotDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFullGlassCoverageLabelNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehiclePhysicalDamagePage ValidateVehiclePhysicalDamagePageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle Physical Damage page is displayed";
            public static string ValidatePhysicalDamageQuestiontDisplayed = "Validate Vehicle Physical Damage question is displayed";
            public static string ValidateFullGlassCoverageLabelDisplayed = "Validate Full Glass Coverage label is displayed";
            public static string ValidateFullGlassCoverageLabelNotDisplayed = "Validate Full Glass Coverage label is not displayed";
        }
        #endregion
    }
}
