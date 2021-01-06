using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleInfoPage : LandingPage
    {
        #region Locators
        private By _lblQuestion(string content) => By.XPath($"//formly-field//h1[normalize—space(text())=\"{content}\"]");
        private By _drpRadius => By.XPath("//*[contains(@c|ass, 'automation-id-radius')]//mat-select");
        private By _lblRadiusHint => By.XPath("//*[contains(@class, 'automation-id-radius')]//mat—hint");
        private By _btnlnterstate(string item) => By.XPath($"//*[contains(@class; 'automation-id-interstateTravel')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnSelectedlnterstate => By.XPath("//*[contains(@class, 'automation-id-interstateTravel')]//mat-radio-button[contains(@class,'checked')]");
        private By _txtGaragingzip => By.XPath("//*[contains(@class, 'automation-id-garagingZipCode')]//input");
        private By _bthorkersCompensation(string item) => By.XPath($"//*[contains(@class, 'automation-id-workersCompensation‘)]//mat-radio-button[.//*[normalize-space(text())=‘{item}']]");
        private By _btnSelectedWorkersCompensation => By.XPath("//*[contains(@class; 'automation-id-workersCompensation')]//mat-radio-button[contains(@class,'checked')]");
        private By _btnTransportation(string item) => By.XPath($"//*[contains(@class, 'automation-id-transportationNetworkCompany‘)]//mat-radio-button[.//*[normalize-space(text())=‘{item}']]");
        private By _btnSelectedTransportation => By.XPath("//*[contains(@class; 'automation-id-transportationNetworkCompany')]//mat-radio-button[contains(@class,'checked')]");
        private By _btnRepossession(string item) => By.XPath($"//*[contains(@class, ‘automation-id-repossessionPercent‘)]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnSelectedRepossession => By.XPath("//*[contains(@c|ass, 'automation-id-repossessionPercent‘)]//mat-radio-button[contains(@class,'checked')]");
        private By _btnPrimarilyOperate(string item) => By.XPath($"//*[contains(@class, ‘automation-id-primarilyEmployeeOperate')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnSelectedPrimarilyOperate => By.XPath("//*[contains(@c|ass, 'automation-id-primarilyEmployeeOperate')]//mat-radio-button[contains(@class,'checked')]");
        private By _btnOwnerOperator(string item) => By.XPath($"//*[contains(@class, ‘automation-id-ownerOperator‘)]//mat-radio-button[.//*[normalize-space(text())=‘{item}']]");
        private By _btnSelectedOwnerOperator => By.XPath("//*[contains(@c|ass, 'automation-id-ownerOperator‘)]//mat-radio-button[contains(@class, 'checked')]");
        private By _btnCarrierAI(string item) => By.XPath($"//*[contains(@class, 'automation-id-ownerOperatorCertAi')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnSelectedCarrierAI => By.XPath("//*[contains(@class; 'automation-id-ownerOperatorCertAi')]//mat-radio-button[contains(@class, 'checked')]");
        #endregion

        #region Elements
        public IWebElement RadiusDropdown => StableFindElement(_drpRadius);
        public IWebElement RadiusHintLabel => StableFindElement(_lblRadiusHint);
        public IWebElement InterstateButton(string item) => StableFindElement(_btnlnterstate(item));
        public IWebElement GaragingZipCodeTextBox => StableFindElement(_txtGaragingzip);
        public IWebElement WorkersCompensationButton(string item) => StableFindElement(_bthorkersCompensation(item));
        public IWebElement TransportationCompButton(string item) => StableFindElement(_btnTransportation(item));
        public IWebElement RepossessionButton(string item) => StableFindElement(_btnRepossession(item));
        public IWebElement PrimarilyOperateButton(string item) => StableFindElement(_btnPrimarilyOperate(item));
        public IWebElement OwnerOperatorButton(string item) => StableFindElement(_btnOwnerOperator(item));
        public IWebElement CarrierAIButton(string item) => StableFindElement(_btnCarrierAI(item));
        #endregion

        #region Business Methods
        public VehicleInfoPage() : base()
        {
            Url = "forms/vehicles-info";
            RequiredElementLocator = _drpRadius;
        }

        [ExtentStepNode]
        public VehicleInfoPage InputVehicleInformation(PolicyLevelVehicleInformation policyLevelVehiclelnformation)
        {
            GetLastNode().LogDataInfo(policyLevelVehiclelnformation);
            ParameterValidator.ValidateNotNull(policyLevelVehiclelnformation, "Policy Level Vehicle Information");
            if (IsDropdownEnabled(_drpRadius))
            {
                RadiusDropdown.SelectByText(policyLevelVehiclelnformation.Radius);
            }
            else
            {
                GetLastNode().Info("Radius was set to default value: " + RadiusDropdown.Text);
            }
            if (policyLevelVehiclelnformation.Interstate != null)
            {
                InterstateButton(policyLevelVehiclelnformation.Interstate).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.GaragingZipCode != null)
            {
                GaragingZipCodeTextBox.InputText(policyLevelVehiclelnformation.GaragingZipCode);
            }
            if (policyLevelVehiclelnformation.Transportation != null)
            {
                TransportationCompButton(policyLevelVehiclelnformation.Transportation).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.WorkersCompensation != null)
            {
                WorkersCompensationButton(policyLevelVehiclelnformation.WorkersCompensation).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.Repossession != null)
            {
                RepossessionButton(policyLevelVehiclelnformation.Repossession).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.PrimarilyOperate != null)
            {
                PrimarilyOperateButton(policyLevelVehiclelnformation.PrimarilyOperate).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.OwnerOperator != null)
            {
                OwnerOperatorButton(policyLevelVehiclelnformation.OwnerOperator).ScrollIntoViewAndClick();
            }
            if (policyLevelVehiclelnformation.CarrierAI != null)
            {
                CarrierAIButton(policyLevelVehiclelnformation.CarrierAI).ScrollIntoViewAndClick();
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        [ExtentStepNode]
        public dynamic SelectNextButton()
        {
            dynamic page;
            string currentUrl = WebDriver.Url;
            NextButton.ScrollIntoViewBottom();
            NextButton.ClickWithJS();
            WaitForUrlChanged(currentUrl);
            if (WebDriver.Url.Contains("garaging-address"))
            {
                page = new GaragingAddressPage();
            }
            else if (WebDriver.Url.Contains("vehicle-suggestions"))
            {
                page = new VehicleSuggestionsPage();
            }
            else
            {
                page = new VehicleEntryPage();
            }
            page.WaitForPageLoad();
            return page;
        }

        public CustomerInfoPage SelectPreviousButton()
        {
            return SelectPreviousButton<CustomerInfoPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleInfoPage ValidateVehicleLevelTravelQuestionsDisplayed(string[] questions)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(questions, "Questions");
                foreach (var item in questions)
                {
                    if (IsElementDisplayed(_lblQuestion(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateVehicleLevelTravelQuestions, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateVehicleLevelTravelQuestions, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleLevelTravelQuestions, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateVehicleInfoIsFilledOut(PolicyLevelVehicleInformation policyLevelVehiclelnformation)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(policyLevelVehiclelnformation, "Policy Information");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                new KeyValuePair<string, string[]>("Radius", new string[] { policyLevelVehiclelnformation.Radius, GetText(_drpRadius)})
                };
                if (policyLevelVehiclelnformation.Interstate != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("lnterstate", new string[] { policyLevelVehiclelnformation.Interstate, GetText(_btnSelectedlnterstate) }));
                }
                if (policyLevelVehiclelnformation.WorkersCompensation != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Workers Compensation", new string[] { policyLevelVehiclelnformation.WorkersCompensation, GetText(_btnSelectedWorkersCompensation) }));
                }
                if (policyLevelVehiclelnformation.GaragingZipCode != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Garaging Zip", new string[] { policyLevelVehiclelnformation.GaragingZipCode, GaragingZipCodeTextBox.GetValue() }));
                }
                if (policyLevelVehiclelnformation.Transportation != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Transportation", new string[] { policyLevelVehiclelnformation.Transportation, GetText(_btnSelectedTransportation) }));
                }
                if (policyLevelVehiclelnformation.Repossession != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Repossession", new string[] { policyLevelVehiclelnformation.Repossession, GetText(_btnSelectedRepossession) }));
                }
                if (policyLevelVehiclelnformation.PrimarilyOperate != null)
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Primarily Operate", new string[] { policyLevelVehiclelnformation.PrimarilyOperate, GetText(_btnSelectedPrimarilyOperate) }));
                }
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateVehicleInfoIsFilledOut, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateVehicleInfoIsFilledOut, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleInfoIsFilledOut, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusDropDownIsDefaulted(string defaultRadius)
        {
            var node = GetLastNode();
            try
            {
                string actualRadius = GetText(_drpRadius);
                if (actualRadius == defaultRadius)
                {
                    SetPassValidation(node, ValidationMessage.ValidateRadiusDropDownlsDefaulted, expectedValue: defaultRadius);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateRadiusDropDownlsDefaulted, expectedValue: defaultRadius, actualValue: actualRadius);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateRadiusDropDownlsDefaulted, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusDropDownlsNotDefaulted(string defaultRadius)
        {
            var node = GetLastNode();
            try
            {
                string actualRadius = GetText(_drpRadius);
                if (actualRadius != defaultRadius)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateRadiusDropDownlsNotDefaulted, defaultRadius), actualRadius);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateRadiusDropDownlsNotDefaulted, defaultRadius), actualRadius);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateRadiusDropDownlsNotDefaulted, defaultRadius), e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusDropDownEditable()
        {
            var node = GetLastNode();
            try
            {
                if (IsDropdownEnabled(_drpRadius))
                {
                    SetPassValidation(node, ValidationMessage.ValidateRadiusDropDownEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateRadiusDropDownEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateRadiusDropDownEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusDropDownNotEditable()
        {
            var node = GetLastNode();
            try
            {
                if (!IsDropdownEnabled(_drpRadius))
                {
                    SetPassValidation(node, ValidationMessage.ValidateRadiusDropDownNotEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateRadiusDropDownNotEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateRadiusDropDownNotEditable, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusHintDisplayedCorrectly(string expectedHint)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblRadiusHint))
                {
                    string actualHint = GetText(_lblRadiusHint);
                    if (actualHint == expectedHint)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateRadiusHintDisplayedCorrectly, expectedValue: expectedHint);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateRadiusHintDisplayedCorrectly, expectedValue: expectedHint, actualValue: actualHint);
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateRadiusHintDisplayedCorrectly, expectedValue: expectedHint);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateRadiusHintDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateRadiusHintNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblRadiusHint))
                {
                    SetPassValidation(node, ValidationMessage.ValidateRadiusHintNotDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateRadiusHintNotDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateRadiusHintNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleInfoPage ValidateVehicleInfoPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle Info page is displayed";
            public static string ValidateVehicleLevelTravelQuestions = "Validate Vehicle Level Travel questions are displayed";
            public static string ValidateVehicleInfoIsFilledOut = "Validate Vehicle Info is filled out";
            public static string ValidateRadiusDropDownlsDefaulted = "Validate Radius dropdown is defaulted";
            public static string ValidateRadiusDropDownlsNotDefaulted = "Validate Radius dropdown is not defaulted (default = {0})";
            public static string ValidateRadiusDropDownEditable = "Validate Radius dropdown can be edited";
            public static string ValidateRadiusDropDownNotEditable = "Validate Radius dropdown can not be edited";
            public static string ValidateRadiusHintDisplayedCorrectly = "Validate Radius hint is displayed correctly";
            public static string ValidateRadiusHintNotDisplayed = "Validate Radius hint is not displayed";
        }
        #endregion
    }
}