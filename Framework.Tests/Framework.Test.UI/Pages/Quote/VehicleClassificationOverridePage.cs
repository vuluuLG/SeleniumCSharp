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
    public class VehicleClassificationOverridePage : LandingPage
    {
        #region Locators
        private By _btnClassificationOverride(string item) => By.XPath($"//*[contains(@class, 'automation-id-selectedAnswers')]//*[(local-name()='button' or local-name()='mat—radio-button') and.//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-selectedAnswers')]//h1");
        #endregion

        #region Elements
        public IWebElement ClassOverrideButton(string item) => StableFindElement(_btnClassificationOverride(item));
        public IWebElement QuestionLable => StableFindElement(_lblQuestion);
        #endregion

        #region Business Methods
        public VehicleClassificationOverridePage() : base()
        {
            Url = "forms/classification-override";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public T SelectClassificationOverride<T>(params string[] classificationOverride) where T : BasePage
        {
            var node = GetLastNode();
            node.Info("Question: " + QuestionLable.Text);
            string currentUrl = WebDriver.Url;
            foreach (var item in classificationOverride)
            {
                node.Info("Select the answer: " + item);
                ClassOverrideButton(item).Click();
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
        public VehicleClassificationOverridePage ValidateVehicleClassificationOverridePageDisplayed()
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
        public VehicleClassificationOverridePage ValidateClassificationOverrideQuesDisplayedCorrectly(string expectedQuestion)
        {
            var node = GetLastNode();
            try
            {
                string actualQuestion = GetText(_lblQuestion);
                if (actualQuestion == expectedQuestion)
                {
                    SetPassValidation(node, ValidationMessage.ValidateClassificationOverrideQuesDisplayedCorrectly, expectedValue: expectedQuestion);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateClassificationOverrideQuesDisplayedCorrectly, expectedValue: expectedQuestion, actualValue: actualQuestion);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateClassificationOverrideQuesDisplayedCorrectly, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate ClassificationOverride page is displayed";
            public static string ValidateClassificationOverrideQuesDisplayedCorrectly = "Validate ClassificationOverride question is displayed correctly";
        }
        #endregion
    }
}