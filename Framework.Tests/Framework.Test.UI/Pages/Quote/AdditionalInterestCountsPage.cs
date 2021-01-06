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
    public class AdditionalInterestCountsPage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-designatedInsuredCount')]//h1");
        private By _txtDesignatedInsuredCount => By.XPath("//*[contains(@class, 'automation-id-designatedInsuredCount')]//input");
        private By _txtAdditionalNamedInsuredCount => By.XPath("//*[contains(@class, 'automation-id-additionalNamedInsuredCount')]//input");
        private By _txtNamedInsuredWaiverOfSubrogationCount => By.XPath("//*[contains(@class, 'automation-id-namedInsuredWaiverOfSubrogationCount')]//input");
        private By _lblWantsBlanketAdditionalInsured => By.XPath("//*[contains(@class, 'automation-id-wantsBlanketAdditionalInsured')]//h1");
        private By _btnWantsBlanketAdditionalInsured(string item) => By.XPath($"//*[contains(@class, 'automation-id-wantsBlanketAdditionalInsured')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _txtBlanketWaiverOfSubrogationCount => By.XPath("//*[contains(@class, 'automation-id-blanketWaiverOfSubrogationCount')]//input");
        #endregion

        #region Elements
        public IWebElement DesignatedInsuredCountTextBox => StableFindElement(_txtDesignatedInsuredCount);
        public IWebElement AdditionalNamedInsuredCountTextBox => StableFindElement(_txtAdditionalNamedInsuredCount);
        public IWebElement NamedInsuredWaiverOfSubrogationCountTextBox => StableFindElement(_txtNamedInsuredWaiverOfSubrogationCount);
        public IWebElement WantsBlanketAdditionalInsuredButton(string item) => StableFindElement(_btnWantsBlanketAdditionalInsured(item));
        public IWebElement BlanketWaiverOfSubrogationCountTextBox => StableFindElement(_txtBlanketWaiverOfSubrogationCount);
        #endregion

        #region Business Methods
        public AdditionalInterestCountsPage() : base()
        {
            Url = "forms/additional-interest-counts";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public AdditionalInterestCountsPage InputAdditionalInterestCounts(AdditionalInterests additionalInterests)
        {
            GetLastNode().LogDataInfo(additionalInterests);
            ParameterValidator.ValidateNotNull(additionalInterests, "Additional Interests");
            DesignatedInsuredCountTextBox.InputText(additionalInterests.DesignatedInsuredCount);
            AdditionalNamedInsuredCountTextBox.InputText(additionalInterests.AdditionalNamedInsuredCount);
            NamedInsuredWaiverOfSubrogationCountTextBox.InputText(additionalInterests.NamedInsuredWaiverOfSubrogationCount);
            if (additionalInterests.WantsBlanketAdditionalInsured != null)
            {
                WantsBlanketAdditionalInsuredButton(additionalInterests.WantsBlanketAdditionalInsured).ScrollIntoViewAndClick();
                if (additionalInterests.WantsBlanketAdditionalInsured == AnswerOption.Yes)
                {
                    BlanketWaiverOfSubrogationCountTextBox.InputText(additionalInterests.BlanketWaiverOfSubrogationCount);
                }
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AdditionalInterestCountsPage ValidateAdditionalInterestCountsPageDisplayed()
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
        public AdditionalInterestCountsPage ValidateWantsBlanketAdditionalInsuredCoverageNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblWantsBlanketAdditionalInsured))
                {
                    SetPassValidation(node, ValidationMessage.ValidateWantsBlanketAdditionalInsuredCoverageNotDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateWantsBlanketAdditionalInsuredCoverageNotDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateWantsBlanketAdditionalInsuredCoverageNotDisplayed, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Additional Interest Counts page is displayed";
            public static string ValidateWantsBlanketAdditionalInsuredCoverageNotDisplayed = "Validate the Wants Blanket Additional Insured coverage is not displayed";
        }
        #endregion
    }
}