using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LiabilityQuestions1Page : LandingPage
    {
        #region Locators
        private By _txtGrossReciptsLastYear => By.XPath("//*[contains(@class, 'automation-id-grossReceiptsLastYear')]//input");
        private By _txtGrossReciptsNextYear => By.XPath("//*[contains(@class, 'automation-id-estimatedGrossReceiptsForComingYear')]//input");
        private By _txtCostOfHire => By.XPath("//*[contains(@class, 'automation-id-estimatedCostOfHireForComingYear')]//input");
        private By _btnPrimaryUseHiredAuto(string item) => By.XPath($"//*[contains(@class, 'automation-id-hiredAutosPrimaryUse')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnPlanToUseUnscheduledVehicleNextYear(string item) => By.XPath($"//*[contains(@class, 'automation-id-areNonScheduledVehiclePlannedToBeUsed')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        #endregion

        #region Elements
        public IWebElement GrossReciptsLastYearTextBox => StableFindElement(_txtGrossReciptsLastYear);
        public IWebElement GrossReciptsNextYearTextBox => StableFindElement(_txtGrossReciptsNextYear);
        public IWebElement CostOfHireTextBox => StableFindElement(_txtCostOfHire);
        public IWebElement PrimaryUseHiredAutoButton(string item) => StableFindElement(_btnPrimaryUseHiredAuto(item));
        public IWebElement PlanToUseUnscheduledVehicleNextYearButton(string item) => StableFindElement(_btnPlanToUseUnscheduledVehicleNextYear(item));
        #endregion

        #region Business Methods
        public LiabilityQuestions1Page() : base()
        {
            Url = "forms/hcno-liabllity-questions-1";
            RequiredElementLocator = _txtGrossReciptsLastYear;
        }

        [ExtentStepNode]
        public LiabilityQuestions1Page EnterLiabilityQuestion1(LiabilityQuestion1 liabilityQuestion1)
        {
            GetLastNode().LogDataInfo(liabilityQuestion1);
            ParameterValidator.ValidateNotNull(liabilityQuestion1, "Liability Question 1");
            GrossReciptsLastYearTextBox.InputText(liabilityQuestion1.GrossReciptsLastYear);
            GrossReciptsNextYearTextBox.InputText(liabilityQuestion1.GrossReciptsNextYear);
            CostOfHireTextBox.InputText(liabilityQuestion1.CostOfHire);
            PrimaryUseHiredAutoButton(liabilityQuestion1.PrimaryUseHiredAuto).ScrollIntoViewAndClick();
            PlanToUseUnscheduledVehicleNextYearButton(liabilityQuestion1.PlanToUseUnscheduledVehicleNextYear).ScrollIntoViewAndClick();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        #endregion
        #region Validations
        [ExtentStepNode]
        public LiabilityQuestions1Page ValidateLiabilityQuestions1PageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
                else

                    SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Liability Questions 1 page is displayed";
        }
        #endregion
    }
}