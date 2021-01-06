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
    public class DriverOverviewPage : LandingPage
    {
        #region Locators
        private By _lblDriverNames => By.XPath("//label[contains(text(),'Name')]//following-sibling::div");
        private By _lblDriverName(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers')]//div[.=\"{item}\"]");
        private By _lblBirthDate(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//label[contains(text(),'Birth Date')]//following-sibling::div");
        private By _lblLicenseState(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//label[contains(text(),'License State')]//following-sibling::div");
        private By _lblLicenseNumber(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//label[contains(text(),'License Number')]//following::div");
        private By _lblMVRStatus(string item) => By.XPath($"//breeze.tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//label[contains(text(),'MVR Status')]//following-sibling::div");
        private By _iconDelete(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//button[2]");
        private By _iconEdit(string item) => By.XPath($"//breeze-tile[contains(@class, 'drivers') and .//div[.=\"{item}\"]]//button[1]");
        private By _btnAddDriver => By.XPath("//breeze-spin-button[@text = 'ADD DRIVER']");
        private By _btnCoverageSummary => By.XPath("//breeze-spin-button[@text = 'COVERAGE SUMMARY']");
        #endregion

        #region Elements
        public List<IWebElement> DriverNamesLabel => StableFindElements(_lblDriverNames).ToList();
        public IWebElement Deletelcon(string item) => StableFindElement(_iconDelete(item));
        public IWebElement Editlcon(string item) => StableFindElement(_iconEdit(item));
        public IWebElement DriverNameLabel(string item) => StableFindElement(_lblDriverName(item));
        public IWebElement AddDriverButton => StableFindElement(_btnAddDriver);
        public IWebElement CoverageSummaryButton => StableFindElement(_btnCoverageSummary);
        #endregion

        #region Business Methods
        public DriverOverviewPage() : base()
        {
            Url = "forms/drivers";
            RequiredElementLocator = _btnAddDriver;
        }

        [ExtentStepNode]
        public DriverOverviewPage SelectDeleteIcon(string driver)
        {
            GetLastNode().Info("Select delete icon of driver: " + driver);
            Deletelcon(driver).ClickWithJS();
            WaitForElementVisible(_dlgConfirm);
            return this;
        }

        [ExtentStepNode]
        public DriverEnterPage SelectEditlcon(string driver)
        {
            GetLastNode().Info("Select edit icon of driver: " + driver);
            Editlcon(driver).ClickWithJS();
            var page = new DriverEnterPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public DriverEnterPage SelectAddDriverButton()
        {
            AddDriverButton.Click();
            var page = new DriverEnterPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public CoveragesPage SeIectCoverageSummaryButton()
        {
            CoverageSummaryButton.Click();
            var page = new CoveragesPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public DriverOverviewPage SelectOptionOnConfirmDialog(string option, string driver = null)
        {
            SelectOptionOnConfirmDialog<DriverOverviewPage>(option);
            if (driver != null)
            {
                WaitForElementNotExists(_lblDriverName(driver), throwException: false);
            }
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public DriverOverviewPage ValidateDriverOverviewPageDisplayed()
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
        public DriverOverviewPage ValidateDriversDisplayedCorrectly(string[] expectedDrivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(expectedDrivers, "Drivers");
                int total = GetElementNumber(_lblDriverNames);
                if (expectedDrivers.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriversInfoDisplayedCorrectly, "Total Drivers", expectedDrivers.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < expectedDrivers.Length; i++)
                    {
                        if (DriverNamesLabel[i].Text == expectedDrivers[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateDriversInfoDisplayedCorrectly, expectedValue: expectedDrivers[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateDriversInfoDisplayedCorrectly, expectedValue: expectedDrivers[i], actualValue: DriverNamesLabel[i].Text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriversInfoDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateEditAndDeleteIconsDisplayed(string[] drivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(drivers, "Drivers");
                foreach (var item in drivers)
                {
                    if (IsElementDisplayed(_iconEdit(item)) && IsElementDisplayed(_iconDelete(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, item);
                    }
                    else
                        SetFailValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, item);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEditAndDeleteIconsDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateEditIconsEnabled(string[] drivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(drivers, "Drivers");
                foreach (var item in drivers)
                {
                    if (IsElementEnabled(_iconEdit(item)))
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
        public DriverOverviewPage ValidateEditIconsDisabled(string[] drivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(drivers, "Drivers");
                foreach (var item in drivers)
                {
                    if (!IsElementEnabled(_iconEdit(item)))
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
        public DriverOverviewPage ValidateDeleteIconsEnabled(string[] drivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(drivers, "Drivers");
                foreach (var item in drivers)
                {
                    if (IsElementEnabled(_iconDelete(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, item);
                    }
                    else
                        SetFailValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, item);
                }
            }


            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDeleteIconsEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateDeleteIconsDisabled(string[] drivers)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(drivers, "Drivers");
                foreach (var item in drivers)
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
        public DriverOverviewPage ValidateAddDriverButtonDisplayedAndEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnAddDriver) && IsElementEnabled(_btnAddDriver))
                {
                    SetPassValidation(node, ValidationMessage.ValidateAddDriverButtonDisplayedAndEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAddDriverButtonDisplayedAndEnabled);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddDriverButtonDisplayedAndEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateCoverageSummaryButtonDisplayedAndEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnCoverageSummary) && IsElementEnabled(_btnCoverageSummary))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabIed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabIed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayedAndEnabIed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateDriverNotDeleted(string driver)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblDriverName(driver)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDriverNotDeleted, driver);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriverNotDeleted, driver);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverNotDeleted, e);
            }
            return this;
        }

        [ExtentStepNode]
        public DriverOverviewPage ValidateDriverDeleted(string deletedDriver)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblDriverName(deletedDriver)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDriverDeleted, deletedDriver);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriverDeleted, deletedDriver);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriverDeleted, e);
            }
            return this;
        }

        public DriverOverviewPage ValidateConfirmDiangDisplayed()
        {
            return ValidateConfirmDialogDisplayed<DriverOverviewPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Driver Overview page is displayed";
            public static string ValidateDriversDisplayedCorrectly = "Validate drivers are displayed correctly";
            public static string ValidateDriversInfoDisplayedCorrectly = "Validate drivers info are displayed correctly";
            public static string ValidateEditAndDeleteIconsDisplayed = "Validate Edit and Delete icons are displayed";
            public static string ValidateAddDriverButtonDisplayedAndEnabled = "Validate Add Driver button is displayed and enabled";
            public static string ValidateCoverageSummaryButtonDisplayedAndEnabIed = "Validate Coverage summary button is displayed and enabled";
            public static string ValidateEditIconsEnabled = "Validate Edit icons are enabled";
            public static string ValidateEditIconsDisabled = "Validate Edit icons are disabled";
            public static string ValidateDeleteIconsEnabled = "Validate Delete icons are enabled";
            public static string ValidateDeleteIconsDisabled = "Validate Delete icons are disabled";
            public static string ValidateDriverNotDeleted = "Validate driver is not deleted";
            public static string ValidateDriverDeleted = "Validate driver is deleted";
        }
        #endregion
    }
}
