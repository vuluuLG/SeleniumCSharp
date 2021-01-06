using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleSuggestionsPage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-vehicles')]//h1");
        private By _btnVehicleSuggestions => By.XPath($"//*[contains(@class, 'automation-id-vehicles')]//button");
        private By _btnVehicleSuggestion(string item) => By.XPath($"//*[contains(@class, 'automation-id-vehicles')]//button[.//*[normalize-space(text())=\"{item}\"]]");
        private By _btnVehicleSuggestionlndex(int index) => By.XPath($"(//*[contains(@class, 'automation-id-vehicles')]//button)[{index}]");
        private By _lnkIDontSeeMyVehicles => By.XPath("//*[contains(@class, 'automation-id-dontSeeMyVehicles')]//button");
        #endregion

        #region Elements
        public List<IWebElement> VehicleSuggestions => StableFindElements(_btnVehicleSuggestions).ToList();
        public IWebElement VehicleSuggestionButton(string item) => StableFindElement(_btnVehicleSuggestion(item));
        public IWebElement VehicleSuggestionlndexButton(int index) => StableFindElement(_btnVehicleSuggestionlndex(index));
        public IWebElement IDontSeeMyVehiclesLinkButton => StableFindElement(_lnkIDontSeeMyVehicles);
        #endregion

        #region Business Methods
        public VehicleSuggestionsPage() : base()
        {
            Url = "forms/vehicle-suggestions";
            RequiredElementLocator = _lnkIDontSeeMyVehicles;
        }

        [ExtentStepNode]
        public VehicleSuggestionsPage SelectSuggestedVehicles(string[] vehicles)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(vehicles, "Suggested Vehicles");
            foreach (var item in vehicles)
            {
                node.Info("Select vehicle: " + item);
                VehicleSuggestionButton(item).Click();
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public VehicleSuggestionsPage SelectSuggestedVehiclesBylndex(int[] vehicleslndex)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(vehicleslndex, "Suggested Vehicles Index");
            foreach (var item in vehicleslndex)
            {
                node.Info("Select vehicle at index: " + item);
                VehicleSuggestionlndexButton(item).Click();
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public VehicleEntryPage SelectIDontSeeMyVehicles()
        {
            IDontSeeMyVehiclesLinkButton.Click();
            var page = new VehicleEntryPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public string GetVehicleNameBylndex(int vehiclelndex)
        {
            string vehicleName = VehicleSuggestionlndexButton(vehiclelndex).Text;
            GetLastNode().Info($"Vehicle name at index {vehiclelndex}: {vehicleName}");
            return vehicleName;
        }

        public VehicleBodyTypePage SelectNextButton()
        {
            return SelectNextButton<VehicleBodyTypePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleSuggestionsPage ValidateSuggestedVehiclesDisplayedCorrectly(string[] suggestedVehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(suggestedVehicles, "Suggested Vehicles");
                int total = GetElementNumber(_btnVehicleSuggestions);
                if (suggestedVehicles.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayedCorrectly, "Total Vehicles", suggestedVehicles.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < suggestedVehicles.Length; i++)
                    {
                        if (IsElementDisplayed(_btnVehicleSuggestion(suggestedVehicles[i])))
                        {
                            SetPassValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayedCorrectly, suggestedVehicles[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayedCorrectly, suggestedVehicles[i]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleSuggestionsPage ValidateSuggestedVehiclesDisplayed()
        {
            var node = GetLastNode();
            try
            {
                int total = GetElementNumber(_btnVehicleSuggestions);
                if (total > 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayed, $"Total vehicles:{total}");
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayed, $"Total vehicles:{total}");
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSuggestedVehiclesDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleSuggestionsPage ValidateVehicleSuggestionsPageDisplayed()
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
            public static string ValidatePageDisplayed = "Validate Vehicle Suggestions page is displayed";
            public static string ValidateSuggestedVehiclesDisplayedCorrectly = "Validate Vehicle Suggestions are displayed correctly";
            public static string ValidateSuggestedVehiclesDisplayed = "Validate there are 1 or more suggested vehicles";
        }
        #endregion
    }
}
