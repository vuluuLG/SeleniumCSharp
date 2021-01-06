using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LiabilityQuestions2Page : LandingPage
    {
        #region Locators
        private By _btnTypeOfUnscheduledVehicleUsed(string item) => By.XPath($"//*[contains(@class, 'automation-id-notScheduledButPlannedToBeUsedVehiclesTypes')]//button[.//*[normalize-space(text())='{item}']]");
        private By _btnHowVehicleUsed(string item) => By.XPath($"//*[contains(@class, 'automation-id-additionalUnitsPlannedToBeUsedAs')]//button[.//*[normalize-space(text())='{item}']]");
        private By _btnHasVehicleOperatedlndividual(string item) => By.XPath($"//*[contains(@class, 'automation-id-areVehiclesPlannedToBeOperatedByNonEmployees')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-notScheduledButPlannedToBeUsedVehiclesTypes')]//h1");
        #endregion

        #region Elements
        public IWebElement TypeOfUnscheduledVehicleUsedButton(string item) => StableFindElement(_btnTypeOfUnscheduledVehicleUsed(item));
        public IWebElement HowVehicleUsedButton(string item) => StableFindElement(_btnHowVehicleUsed(item));
        public IWebElement HasVehicleOperatedlndividualButton(string item) => StableFindElement(_btnHasVehicleOperatedlndividual(item));
        #endregion

        #region Business Methods
        public LiabilityQuestions2Page() : base()
        {
            Url = "forms/hcno-liability-questions-2";
            RequiredElementLocator = _lblQuestion;
        }

        public LiabilityQuestions3Page SelectNextButton()
        {
            return SelectNextButton<LiabilityQuestions3Page>();
        }

        [ExtentStepNode]
        public LiabilityQuestions2Page EnterLiabilityQuestion2(LiabilityQuestion2 liabilityQuestion2)
        {
            var node = GetLastNode();
            node.LogDataInfo(liabilityQuestion2);
            ParameterValidator.ValidateNotNull(liabilityQuestion2, "Liability Question 2");
            foreach (var item in liabilityQuestion2.TypeOfUnscheduledVehicleUsed)
            {
                node.Info("Select type(s) of vehicles: " + item);
                TypeOfUnscheduledVehicleUsedButton(item).ScrollIntoViewAndClick();
            }
            foreach (var item in liabilityQuestion2.HowVehicleUsed)
            {
                node.Info("Select how vehicle used: " + item);
                HowVehicleUsedButton(item).ScrollIntoViewAndClick();
            }
            HasVehicleOperatedlndividualButton(liabilityQuestion2.HasVehicleOperatedIndividual).ScrollIntoViewAndClick();
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public LiabilityQuestions2Page ValidateLiabilityQuestions2PageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Liability Questions 2 page is displayed";
        }
        #endregion
    }
}