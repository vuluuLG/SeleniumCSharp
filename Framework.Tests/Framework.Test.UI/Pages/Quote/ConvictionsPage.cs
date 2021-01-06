using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class ConvictionsPage : LandingPage
    {
        #region Locators
        private By _txtConvictionCount => By.XPath("//*[contains(@class, 'automation-id-convictionCount')]//input");
        private By _drpConvictionDatel => By.XPath("//*[contains(@class, 'automation-id-convictionDateRangel')]//mat-select");
        private By _drpConvictionDate2 => By.XPath("//*[contains(@class, 'automation-id-convictionDateRange2')]//mat-select");
        private By _drpConvictionDate3 => By.XPath("//*[contains(@class, 'automation-id-convictionDateRange3')]//mat-select");
        #endregion

        #region Elements
        public IWebElement ConvictionCountTextBox => StableFindElement(_txtConvictionCount);
        public IWebElement ConvictionDate1Dropdown => StableFindElement(_drpConvictionDatel);
        public IWebElement ConvictionDate2Dropdown => StableFindElement(_drpConvictionDate2);
        public IWebElement ConvictionDate3Dropdown => StableFindElement(_drpConvictionDate3);
        #endregion

        #region Business Methods
        public ConvictionsPage() : base()
        {
            Url = "forms/convictions";
            RequiredElementLocator = _txtConvictionCount;
        }

        [ExtentStepNode]
        public ConvictionsPage EnterConviction(Conviction conviction)
        {
            GetLastNode().LogDataInfo(conviction);
            ParameterValidator.ValidateNotNull(conviction, "Conviction");
            ConvictionCountTextBox.InputText(conviction.NumberConviction);
            int number = Convert.ToInt32(conviction.NumberConviction);
            if (number > 0)
            {
                if (conviction.ConvictionDate1 != null)
                {
                    ConvictionDate1Dropdown.SelectByText(conviction.ConvictionDate1);
                }
                if (conviction.ConvictionDate2 != null)
                {
                    ConvictionDate2Dropdown.SelectByText(conviction.ConvictionDate2);
                }
                if (conviction.ConvictionDate3 != null)
                {
                    ConvictionDate3Dropdown.SelectByText(conviction.ConvictionDate3);
                }
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public ConvictionsPage ValidateConvictionsPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Convictions page is displayed";
        }
        #endregion
    }
}