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
    public class AccidentsAndViolationsPage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-hasAccidentsOrViolations')]//h1");
        private By _btnAccidentsOrViolations(string item) => By.XPath($"//*[contains(@class, 'automation-id-hasAccidentsOrViolations')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _txtViolationCount => By.XPath("//*[contains(@class, 'automation-id-violationCount')]//input");
        private By _txtAccidentCount => By.XPath("//*[contains(@class, 'automation-id-accidentCount')]//input");
        #endregion

        #region Elements
        public IWebElement AccidentsOrViolationsButton(string item) => StableFindElement(_btnAccidentsOrViolations(item));
        public IWebElement ViolationCountTextBox => StableFindElement(_txtViolationCount);
        public IWebElement AccidentCountTextBox => StableFindElement(_txtAccidentCount);
        #endregion

        #region Business Methods
        public AccidentsAndViolationsPage() : base()
        {
            Url = "forms/accidents-and-violations";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public AccidentsAndViolationsPage EnterAccidentAndViolation(AccidentsAndViolations accidentsAndViolations)
        {
            GetLastNode().LogDataInfo(accidentsAndViolations);
            ParameterValidator.ValidateNotNull(accidentsAndViolations, "Accidents And Violations");
            AccidentsOrViolationsButton(accidentsAndViolations.HasAccidentsOrViolations).Click();
            if (accidentsAndViolations.HasAccidentsOrViolations == AnswerOption.Yes)
            {
                ViolationCountTextBox.InputText(accidentsAndViolations.NumberViolations);
                AccidentCountTextBox.InputText(accidentsAndViolations.NumberAccidents);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public ConvictionsPage SelectNextButton()
        {
            return SelectNextButton<ConvictionsPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AccidentsAndViolationsPage ValidateAccidentsAndViolationsPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Accidents And Violations page is displayed";
        }
        #endregion
    }
}