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
    public class VehicleClassificationPage : LandingPage
    {
        #region Locators
        private By _btnClassification(string item) => By.XPath($"//*[contains(@class, 'automation-id-selectedAnswers')]//*[(local-name()='button' or local-name()='mat—radio-button')and.//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-selectedAnswers')]//h1");
        #endregion

        #region Elements
        public IWebElement ClassificationButton(string item) => StableFindElement(_btnClassification(item));
        public IWebElement QuestionLable => StableFindElement(_lblQuestion);
        #endregion

        #region Business Methods
        public VehicleClassificationPage() : base()
        {
            Url = "forms/classification";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public T SelectClassificationFlow<T>(params string[] classificationFlow) where T : BasePage
        {
            var node = GetLastNode();
            node.Info("Question: " + QuestionLable.Text);
            string currentUrl = WebDriver.Url;
            foreach (var item in classificationFlow)
            {
                node.Info("Select the answer: " + item);
                ClassificationButton(item).Click();
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
        public VehicleClassificationPage ValidateFirstQuestiontDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblQuestion))
                {
                    SetPassValidation(node, ValidationMessage.ValidateFirstQuestiontDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFirstQuestiontDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFirstQuestiontDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleClassificationPage ValidateVehicleClassificationPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle Classification page is displayed";
            public static string ValidateFirstQuestiontDisplayed = "Validate first is displayed";
        }
        #endregion
    }
}