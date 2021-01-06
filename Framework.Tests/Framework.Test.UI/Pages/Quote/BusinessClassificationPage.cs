using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class BusinessClassificationPage : LandingPage
    {
        #region Locators
        private By _btnBusinessAnswer(string item) => By.XPath($"//*[contains(@class, 'automation-id-classification')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-classification')]//h1");
        #endregion

        #region Elements
        public IWebElement BusinessAnswerButton(string item) => StableFindElement(_btnBusinessAnswer(item));
        public IWebElement QuestionLable => StableFindElement(_lblQuestion);
        #endregion

        #region Business Methods
        public BusinessClassificationPage() : base()
        {
            Url = "forms/business-group";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public BusinessClassificationPage SelectBusinessClassification(string item)
        {
            var node = GetLastNode();
            node.Info("Question: " + QuestionLable.Text);
            node.Info("SeIect the answer: " + item);
            BusinessAnswerButton(item).ScrollIntoViewAndClick();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public TertiaryPage SelectNextButton()
        {
            return SelectNextButton<TertiaryPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public BusinessClassificationPage ValidateBusinessClassificationPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessClassificationPageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessClassificationPageDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessClassificationPageDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessClassificationPage VaIidateBusinessGrouplsSelected(string businessGroup)
        {
            var node = GetLastNode();
            try
            {
                if (BusinessAnswerButton(businessGroup).IsButtonSelected())
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessGroupIsSelected, businessGroup);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessGroupIsSelected, businessGroup);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessGroupIsSelected, e);
            }
            return this;
        }

        [ExtentStepNode]
        public BusinessClassificationPage ValidateBusinessClassificationQuestionDisplayed(string question)
        {
            var node = GetLastNode();
            try
            {
                if (QuestionLable.Text == question)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessClassificationQuestionDisplayed, expectedValue: question);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessClassificationQuestionDisplayed, expectedValue: question, actualValue: QuestionLable.Text);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessClassificationQuestionDisplayed, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidateBusinessClassificationPageDisplayed = "Validate Business Classification page is displayed";
            public static string ValidateBusinessClassificationQuestionDisplayed = "Validate Business question is displayed correctly";
            public static string ValidateBusinessGroupIsSelected = "Validate business group is selected";
        }
        #endregion
    }
}