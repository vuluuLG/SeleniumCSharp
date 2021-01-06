using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LiabilityQuestions3Page : LandingPage
    {
        #region Locators
        private By _txtAdditionalBusinessesOrSubsidiaries => By.XPath("//*[contains(@class, 'automation-id-additionalBusinessesOrSubsidiaries')]//textarea");
        private By _txtNumberScheduledDriversInvolved => By.XPath("//*[contains(@class, 'automation-id-scheduledDriversInvolvedlnOperation')]//input");
        private By _txtNumberNonDrivingEmployeesInvolved => By.XPath("//*[contains(@class, 'automation-id-nonDrivingEmployeesInvolvedlnOperation')]//input");
        private By _txtNumberlndependentContractorsInvolved => By.XPath("//*[contains(@class, 'automation-id-independentContractorsInvolvedlnOperation')]//input");
        private By _txtNumberVolunteersInvolved => By.XPath("//*[contains(@class, 'automation-id-volunteersInvolvedanperation')]//input");
        private By _txtAnticipatedAnnualMileage => By.XPath("//*[contains(@class, 'automation-id-anticipatedAnnualMileageForNotScheduledVehicles')]//input");
        private By _btnHasAdditionalBusinessesOrSubsidiaries(string item) => By.XPath($"//*[contains(@class, 'automation-id-hasAdditionalBusinessesOrSubsidiaries')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        #endregion
        #region Elements
        public IWebElement AdditionalBusinessesOrSubsidiariesTextBox => StableFindElement(_txtAdditionalBusinessesOrSubsidiaries);
        public IWebElement NumberScheduledDriverslnvolvedTextBox => StableFindElement(_txtNumberScheduledDriversInvolved);
        public IWebElement NumberNonDrivingEmployeeslnvolvedTextBox => StableFindElement(_txtNumberNonDrivingEmployeesInvolved);
        public IWebElement NumberlndependentContractorslnvolvedTextBox => StableFindElement(_txtNumberlndependentContractorsInvolved);
        public IWebElement NumberVolunteerslnvolvedTextBox => StableFindElement(_txtNumberVolunteersInvolved);
        public IWebElement AnticipatedAnnualMileageTextBox => StableFindElement(_txtAnticipatedAnnualMileage);
        public IWebElement HasAdditionalBusinessesOrSubsidiariesButton(string item) => StableFindElement(_btnHasAdditionalBusinessesOrSubsidiaries(item));
        #endregion

        #region Business Methods
        public LiabilityQuestions3Page() : base()
        {
            Url = "forms/hcno-liability-questions-3";
            RequiredElementLocator = _txtNumberScheduledDriversInvolved;
        }

        public HiredCarPhysicalDamagePage SelectNextButton()
        {
            return SelectNextButton<HiredCarPhysicalDamagePage>();
        }

        [ExtentStepNode]
        public LiabilityQuestions3Page EnterLiabilityQuestion3(LiabilityQuestion3 liabilityQuestion3)
        {
            GetLastNode().LogDataInfo(liabilityQuestion3);
            ParameterValidator.ValidateNotNull(liabilityQuestion3, "Liability Question 3");
            HasAdditionalBusinessesOrSubsidiariesButton(liabilityQuestion3.HasAdditionalBusinessesOrSubsidiaries).ScrollIntoViewAndClick();
            AdditionalBusinessesOrSubsidiariesTextBox.InputText(liabilityQuestion3.AdditionalBusinessesOrSubsidiaries);
            NumberScheduledDriverslnvolvedTextBox.InputText(liabilityQuestion3.NumberScheduledDriversInvolved);
            NumberNonDrivingEmployeeslnvolvedTextBox.InputText(liabilityQuestion3.NumberNonDrivingEmployeesInvolved);
            NumberlndependentContractorslnvolvedTextBox.InputText(liabilityQuestion3.NumberIndependentContractorsInvolved);
            NumberVolunteerslnvolvedTextBox.InputText(liabilityQuestion3.NumberVolunteersInvolved);
            AnticipatedAnnualMileageTextBox.InputText(liabilityQuestion3.AnticipatedAnnualMileage);
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public LiabilityQuestions3Page ValidateLiabilityQuestions3PageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Liability Questions 3 page is displayed";
        }
        #endregion
    }
}