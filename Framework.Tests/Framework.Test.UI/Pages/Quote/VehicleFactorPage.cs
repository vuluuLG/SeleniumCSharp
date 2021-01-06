using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleFactorPage : LandingPage
    {
        #region Locators
        private By _btnFactor(string item) => By.XPath($"//*[contains(@class, 'automation-id-selectedAnswers')]//*[(Iocal-name()='button' or local-name()='mat-radio-button') and .//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-selectedAnswers')]//h1");
        private By _txtFactor => By.XPath("//*[contains(@class, 'automation-id-selectedAnswers')]//input");
        #endregion

        #region Elements
        public IWebElement FactorButton(string item) => StableFindElement(_btnFactor(item));
        public IWebElement FactorTextbox => StableFindElement(_txtFactor);
        public IWebElement QuestionLable => StableFindElement(_lblQuestion);
        #endregion

        #region Business Methods
        public VehicleFactorPage() : base()
        {
            Url = "forms/factor";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public T SelectFactorFlow<T>(params string[] factorFlow) where T : BasePage
        {
            var node = GetLastNode();
            node.Info("Question: " + QuestionLable.Text);
            string currentUrl = WebDriver.Url;
            foreach (var item in factorFlow)
            {
                if (!IsElementDisplayed(_txtFactor))
                {
                    FactorButton(item).Click();
                    node.Info("Select the answer: " + item);
                }
                else
                {
                    FactorTextbox.InputText(item);
                    node.Info("lnput the answer: " + item);
                }
            }
            WaitForElementEnabled(_btnNext);
            NextButton.Click();
            WaitForUrlChanged(currentUrl);
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleFactorPage ValidateVehicleFactorPageDisplayed()
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
        public VehicleFactorPage ValidateFactorQuestionDisplayedCorrectly(string expectedQuestion)
        {
            var node = GetLastNode();
            try
            {
                string actualQuestion = GetText(_lblQuestion);
                if (actualQuestion == expectedQuestion)
                {
                    SetPassValidation(node, ValidationMessage.ValidateFactorQuestionDisplayedCorrectly, expectedValue: expectedQuestion);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFactorQuestionDisplayedCorrectly, expectedValue: expectedQuestion, actualValue: actualQuestion);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFactorQuestionDisplayedCorrectly, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Vehicle Factor page is displayed";
            public static string ValidateFactorQuestionDisplayedCorrectly = "Validate factor question is displayed correctly";
        }
        #endregion
    }
}

