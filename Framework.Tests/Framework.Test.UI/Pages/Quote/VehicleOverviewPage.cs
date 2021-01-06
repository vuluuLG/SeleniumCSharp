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
    public class VehicleOverviewPage : LandingPage
    {
        #region Locators
        private By _lblVehicles => By.XPath("//label[contains(text(),'Vehicle')]//following-sibling::div");
        private By _lblVehicleNumber(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles‘)]//div[.=\"{item}\"]//preceding-sibling::label");
        private By _lblVehicle(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles')]//div[.=\"{item}\"]");
        private By _lblValue(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles') and .//div[.=\"{item}\"]]//label[contains(text(),'Value')]//following-sibling::div");
        private By _lblDeductible(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles') and .//div[.=\"{item}\"]]//label[contains(text(),'Deductible')]//following-sibling::div");
        private By _iconDelete(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles‘) and .//div[.=\"{item}\"]]//button[2]");
        private By _iconEdit(string item) => By.XPath($"//breeze-tile[contains(@class, 'vehicles') and .//div[.=\"{item}\"]]//button[1]");
        private By _btnAddVehicle => By.XPath("//breeze-spin-button[@text = 'ADD VEHICLE']");
        private By _btnCoverageSummary => By.XPath("//breeze-spin-button[@text = 'COVERAGE SUMMARY']");
        #endregion

        #region Elements
        public List<IWebElement> VehiclesLabel => StableFindElements(_lblVehicles).ToList();
        public IWebElement DeleteIcon(string item) => StableFindElement(_iconDelete(item));
        public IWebElement Editlcon(string item) => StableFindElement(_iconEdit(item));
        public IWebElement VehicleLabelNumber(string item) => StableFindElement(_lblVehicleNumber(item));
        public IWebElement AddVehicleButton => StableFindElement(_btnAddVehicle);
        public IWebElement CoverageSummaryButton => StableFindElement(_btnCoverageSummary);
        #endregion

        #region Business Methods
        public VehicleOverviewPage() : base()
        {
            Url = "forms/vehicles";
            RequiredElementLocator = _btnAddVehicle;
        }

        [ExtentStepNode]
        public VehicleOverviewPage SelectDeletelcon(string vehicle)
        {
            GetLastNode().Info("Select delete icon of vehicle: " + vehicle);
            DeleteIcon(vehicle).ClickWithJS();
            WaitForElementVisible(_dlgConfirm);
            return this;
        }

        [ExtentStepNode]
        public VehicleEntryPage SelectEditlcon(string vehicle)
        {
            GetLastNode().Info("Select edit icon of vehicle: " + vehicle);
            Editlcon(vehicle).ClickWithJS();
            var page = new VehicleEntryPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public VehicleEntryPage SelectAddVehicleButton()
        {
            AddVehicleButton.Click();
            var page = new VehicleEntryPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public CoveragesPage SelectCoverageSummaryButton()
        {
            CoverageSummaryButton.Click();
            var page = new CoveragesPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public VehicleOverviewPage SelectOptionOnConfirmDialog(string option, string vehicle = null)
        {
            SelectOptionOnConfirmDialog<VehicleOverviewPage>(option);
            if (vehicle != null)
            {
                WaitForElementNotExists(_lblVehicle(vehicle), throwException: false);
            }
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleOverviewPage ValidateVehicleOverviewPageDisplayed()
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
        public VehicleOverviewPage ValidateVehiclesDisplayedCorrectly(string[] expectedVehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(expectedVehicles, "Vehicles");
                int total = GetElementNumber(_lblVehicles);
                if (expectedVehicles.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, "Total Vehicles", expectedVehicles.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < expectedVehicles.Length; i++)
                    {
                        if (VehiclesLabel[i].Text == expectedVehicles[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, expectedValue: expectedVehicles[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, expectedValue: expectedVehicles[i], actualValue: VehiclesLabel[i].Text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateVehiclesInfoDisplayedCorrectly(dynamic[] expectedVehiclesInfo)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(expectedVehiclesInfo, "Vehicles Info");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();
                foreach (var info in expectedVehiclesInfo)
                {
                    string vehicle = info[0];
                    comparisons.Add(new KeyValuePair<string, string[]>($"[Vehicle: {vehicle}] - Value", new string[] { info[1], GetText(_lblValue(vehicle)) }));
                    comparisons.Add(new KeyValuePair<string, string[]>($"[Vehicle: {vehicle}] — Deductible", new string[] { info[2], GetText(_lblDeductible(vehicle)) }));
                }
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidateVehiclesInfoDisplayedCorrectly, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateVehiclesInfoDisplayedCorrectly, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehiclesInfoDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateEditAndDeleteIconsDisplayed(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                foreach (var item in vehicles)
                {
                    if (IsElementDisplayed(_iconEdit(item)) && IsElementDisplayed(_iconDelete(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateVehicleNumbersDisplayedCorrectly(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                for (int i = 0; i < vehicles.Length; i++)
                {
                    string expectedNumber = $"Vehicle {i + 1}";
                    string actualNumber = GetText(_lblVehicleNumber(vehicles[i]));
                    if (actualNumber == expectedNumber)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateVehicleNumbersDisplayedCorrectly, vehicles[i], expectedNumber);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateVehicleNumbersDisplayedCorrectly, vehicles[i], expectedNumber, actualNumber);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleNumbersDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateEditIconsEnabled(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                foreach (var item in vehicles)
                {
                    if (IsElementEnabled(_iconEdit(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEditIconsEnabled, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEditIconsEnabled, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEditIconsEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateEditIconsDisabled(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                foreach (var item in vehicles)
                {
                    if (IsElementEnabled(_iconEdit(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEditIconsDisabled, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEditIconsDisabled, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEditIconsDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateDeleteIconsEnabled(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                foreach (var item in vehicles)
                {
                    if (IsElementEnabled(_iconDelete(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateDeleteIconsDisabled(string[] vehicles)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(vehicles, "Vehicles");
                foreach (var item in vehicles)
                {
                    if (!IsElementEnabled(_iconDelete(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDeleteIconsDisabled, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateDeleteIconsDisabled, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDeleteIconsDisabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateAddVehicleButtonDisplayedAndEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnAddVehicle) && IsElementEnabled(_btnAddVehicle))
                {
                    SetPassValidation(node, ValidationMessage.ValidateAddVehicleButtonDisplayedAndEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAddVehicleButtonDisplayedAndEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddVehicleButtonDisplayedAndEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateCoverageSummaryButtonDisplayedAndEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnCoverageSummary) && IsElementEnabled(_btnCoverageSummary))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateVehicleNotDelected(string vehicle)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblVehicle(vehicle)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateVehicleNotDeleted, vehicle);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehicleNotDeleted, vehicle);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleNotDeleted, e);
            }
            return this;
        }

        [ExtentStepNode]
        public VehicleOverviewPage ValidateVehicleDeleted(string deletedVehicle)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblVehicle(deletedVehicle)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateVehicleDeleted, deletedVehicle);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehicleDeleted, deletedVehicle);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleDeleted, e);
            }
            return this;
        }

        public VehicleOverviewPage ValidateConfirmDialogDisplayed()
        {
            return ValidateConfirmDialogDisplayed<VehicleOverviewPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Vehicle Overview page is displayed";
            public static string ValidateVehiclesDisplayedCorrectly = "Validate vehicles are displayed correctly";
            public static string ValidateVehiclesInfoDisplayedCorrectly = "Validate vehicles info are displayed correctly";
            public static string ValidateVehicleNumbersDisplayedCorrectly = "Validate vehicle numbers are displayed correctly";
            public static string ValidateEditAndDeleteIconsDisplayed = "Validate Edit and Delete icons are displayed";
            public static string ValidateAddVehicleButtonDisplayedAndEnabled = "Validate Add Vehicle button is displayed and enabled";
            public static string ValidateCoverageSummaryButtonDisplayedAndEnabled = "Validate coverage summary button is displayed and enabled";
            public static string ValidateEditIconsEnabled = "Validate Edit icons are enabled";
            public static string ValidateEditIconsDisabled = "Validate Edit icons are disabled";
            public static string ValidateDeleteIconsEnabled = "Validate Delete icons are enabled";
            public static string ValidateDeleteIconsDisabled = "Validate Delete icons are disabled";
            public static string ValidateVehicleNotDeleted = "Validate vehicle is not deleted";
            public static string ValidateVehicleDeleted = "Validate vehicle is deleted";
        }
        #endregion
    }
}