using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class TrailerInterchangePage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-limit')]//h1");
        private By _drpLimit => By.XPath("//*[contains(@class, 'automation-id-limit')]//mat-select");
        private By _drpDeductible => By.XPath("//*[contains(@class, 'automation-id-deductible')]//mat-select");
        #endregion

        #region Elements
        public IWebElement LimitDropdown => StableFindElement(_drpLimit);
        public IWebElement DeductibleDropdown => StableFindElement(_drpDeductible);
        #endregion

        #region Business Methods
        public TrailerInterchangePage() : base()
        {
            Url = "forms/trailer-interchange";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public TrailerInterchangePage InputTrailerInterchange(TrailerInterchange trailerlnterchange)
        {
            GetLastNode().LogDataInfo(trailerlnterchange);
            ParameterValidator.ValidateNotNull(trailerlnterchange, "Trailer Interchange");
            StableSelectLimitAndDeductible(_drpLimit, _drpDeductible, trailerlnterchange.Limit, trailerlnterchange.Deductible);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public SummaryPage SelectNextButton()
        {
            return SelectNextButton<SummaryPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public TrailerInterchangePage ValidateTrailerInterchangePageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Trailer Interchange page is displayed";
        }
        #endregion
    }
}