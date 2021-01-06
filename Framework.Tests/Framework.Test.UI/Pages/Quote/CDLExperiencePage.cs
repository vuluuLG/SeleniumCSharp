using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CDLExperiencePage : LandingPage
    {
        #region Locators
        private By _btnCDL(string item) => By.XPath($"//*[contains(@class, 'automation-id-')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnSelectedCDL => By.XPath("//*[contains(@class, 'automation-id-')]//mat-radio-button[contains(@class,'checked')]");
        private By _drpCDLExperienceAnswer => By.XPath("//*[contains(@class, 'automation-id-dateIssued')]//mat-select");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-')]//h1");
        #endregion

        #region Elements
        public IWebElement CDLButton(string item) => StableFindElement(_btnCDL(item));
        public IWebElement CDLExperienceAnswerDropdown => StableFindElement(_drpCDLExperienceAnswer);
        public IWebElement QuestionLabel => StableFindElement(_lblQuestion);
        #endregion

        #region Business Methods
        public CDLExperiencePage() : base()
        {
            Url = "forms/cdl-experience";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public CDLExperiencePage EnterCDLExperience(CDLExperience cDLExperience)
        {
            GetLastNode().LogDataInfo(cDLExperience);
            ParameterValidator.ValidateNotNull(cDLExperience, "CDL Experience");
            CDLButton(cDLExperience.HasCDL).Click();
            if (cDLExperience.HasCDL != AnswerOption.No)
            {
                CDLExperienceAnswerDropdown.SelectByText(cDLExperience.CDLExperienceAnswer);
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public dynamic SelectNextButton()
        {
            dynamic page;
            string currentUrl = WebDriver.Url;
            NextButton.ScrollIntoViewBottom();
            NextButton.Click();
            WaitForUrlChanged(currentUrl);
            if (WebDriver.Url.Contains("accidents-and-violations"))
            {
                page = new AccidentsAndViolationsPage();
            }
            else if (WebDriver.Url.Contains("enter"))
            {
                page = new DriverEnterPage();
            }
            else
            {
                page = new AddAnotherDriverPage();
            }
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CDLExperiencePage ValidateCDLExperiencePageDisplayed()
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
        public CDLExperiencePage ValidateCDLExperiencelsFilledOut(CDLExperience cdlExperience)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(cdlExperience, "CDL Experience");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("CDL Answer", new string[] { cdlExperience.HasCDL, GetText(_btnSelectedCDL)})
                };

                if (cdlExperience.HasCDL != AnswerOption.No)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("CDL Issued", new string[] { cdlExperience.CDLExperienceAnswer, CDLExperienceAnswerDropdown.Text }));
                }
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCDLExperienceIsFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCDLExperienceIsFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCDLExperienceIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public CDLExperiencePage ValidateCDLQuestionDisplayedCorrectly(string question)
        {
            var node = GetLastNode();
            try
            {
                if (QuestionLabel.Text.Trim() == question)
                {
                    SetPassValidation(node, ValidationMessage.ValidateCDLQuestionDisplayedCorrectly, expectedValue: question);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCDLQuestionDisplayedCorrectly, expectedValue: question, actualValue: QuestionLabel.Text);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCDLQuestionDisplayedCorrectly, e);
            }
            return this;
        }

        public DriverEnterPage SelectPreviousButton()
        {
            return SelectPreviousButton<DriverEnterPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate CDL Experience page is displayed";
            public static string ValidateCDLExperienceIsFilledOut = "Validate CDL Experience is filled out";
            public static string ValidateCDLQuestionDisplayedCorrectly = "Validate CDL question is displayed correctly";
        }
        #endregion
    }
}