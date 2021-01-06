using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using WindowsInput;
using WindowsInput.Native;
using static Framework.Test.Common.Helper.EnvironmentHelper;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class SummaryPage : LandingPage
    {
        #region Locators
        private By _btnSubmit => By.XPath("//button[.='SUBMIT']");
        private By _btnPrint => By.XPath("//button[.='PRINT INDICATION']");
        private By _btnBindAccount => By.XPath("//button[.='BIND ACCOUNT']");
        private By _btnSubmitted => By.XPath("//span[contains(@class,'submitted')]");
        private By _lblBusinessName => By.XPath("//h3[contains(text(),'Business Name')]/../following-sibling::p");
        private By _lblDBA => By.XPath("//h3[contains(text(),'DBA')]/../following-sibling::p");
        private By _lblBusinessAddress => By.XPath("//h3[contains(text(),'Business Address')]/../following-sibling::p");
        private By _lblPrimaryOfficerName => By.XPath("//h3[contains(text(),'Primary Officer Name')]/../following-sibling::p");
        private By _lblPrimaryOfficerAddress => By.XPath("//h3[contains(text(),'Primary Officer Address')]/../following-sibling::p");
        private By _lblVehicles => By.XPath("//h1[contains(text(),'Vehicles')]/..//p");
        private By _lblVehicle(string item) => By.XPath($"//h1[contains(text(),'Vehicles')]/..//p[contains(text(),\"{item}\")]");
        private By _lblDrivers => By.XPath("//h1[contains(text(),'Drivers')]/..//p");
        private By _lblTotal => By.XPath("//div[./*[text()='Total Premium']]//span[contains(@class, 'premium')]");
        private By _txtComments => By.XPath("//textarea");
        private By _lblInsuranceScoreStatus => By.XPath("//breeze-summary//i");
        private By _lblCoverages => By.XPath("//h1[.='Coverages']/following-sibling::*//h3");
        private By _lblCoverage(string item) => By.XPath($"//h1[.='Coverages']/following-sibling::*//h3[.='{item}']");
        private By _lblCoveragePremium(string item) => By.XPath($"//h1[.='Coverages']/following-sibling::*//h3[.='{item}']/following-sibling::span");
        private By _lblCoverageLimit(string item) => By.XPath($"//h1[.='Coverages']/following-sibling::*//h3[.='{item}']/../following-sibling::div//span");
        private By _lblAttachmentsNote => By.XPath("//div[contains(@class, 'attachments-note-block')]/p");
        private By _btnBrowse => By.XPath("//label[text()='Browse']");
        private By _eleFileSelect => By.Id("fileselect");
        private By _lblFiles => By.XPath("//div[contains(@class, 'files-list')]/p");
        private By _eleProgressBar => By.XPath("//mat-progress-bar");
        #endregion

        #region Elements
        public IWebElement SubmitButton => StableFindElement(_btnSubmit);
        public IWebElement BusinessNameLabel => StableFindElement(_lblBusinessName);
        public IWebElement DBALabel => StableFindElement(_lblDBA);
        public IWebElement BusinessAddressLabel => StableFindElement(_lblBusinessAddress);
        public IWebElement PrimaryOfficerNameLabel => StableFindElement(_lblPrimaryOfficerName);
        public IWebElement PrimaryOfficerAddressLabel => StableFindElement(_lblPrimaryOfficerAddress);
        public List<IWebElement> VehiclesLabel => StableFindElements(_lblVehicles).ToList();
        public List<IWebElement> DriversLabel => StableFindElements(_lblDrivers).ToList();
        public IWebElement TotalLabel => StableFindElement(_lblTotal);
        public IWebElement CommentsTextarea => StableFindElement(_txtComments);
        public List<IWebElement> CoveragesLabel => StableFindElements(_lblCoverages).ToList();
        public IWebElement AttachmentsNoteLabel => StableFindElement(_lblAttachmentsNote);
        public IWebElement BrowseButton => StableFindElement(_btnBrowse);
        public IWebElement BindAccountButton => StableFindElement(_btnBindAccount);
        public IWebElement FileSelectElement => StableFindElement(_eleFileSelect);
        public List<IWebElement> FilesLabel => StableFindElements(_lblFiles).ToList();
        #endregion

        #region Business Methods
        public SummaryPage() : base()
        {
            Url = "forms/summary";
            RequiredElementLocator = _txtComments;
        }

        [ExtentStepNode]
        public SummaryPage EnterComments(string comments)
        {
            GetLastNode().Info("Input comments: " + comments);
            CommentsTextarea.InputText(comments);
            return this;
        }

        [ExtentStepNode]
        public IndicationPage SelectSubmitButton()
        {
            SubmitButton.Click();
            var page = new IndicationPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public SummaryPage AttachFile(string filePath)
        {
            GetLastNode().Info("Attach file: " + filePath);
            FileSelectElement.ScrollIntoView();
            try
            {
                if (WebDriver.Property.DriverType == DriverType.InternetExplorer)
                {
                    try
                    {
                        BrowseButton.Click();
                        Wait(3);
                    }
                    catch (Exception) { }

                    InputSimulator inputSimulator = new InputSimulator();
                    inputSimulator.Keyboard.TextEntry(filePath);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                }
                else
                {
                    FileSelectElement.SendKeys(filePath);
                }
            }
            catch (Exception)
            {
                GetLastNode().Info("Error when attach file");
            }

            if (IsAlertDisplayed(3))
            {
                return this;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (IsElementDisplayed(_eleProgressBar, 3) && stopwatch.ElapsedMilliseconds <= waitForElementTimeout * 1000)
            {
                WaitForElementInvisible(_eleProgressBar, waitForElementTimeout);
            }
            stopwatch.Stop();

            return this;
        }

        [ExtentStepNode]
        public string[] GetCoverageLimitsFormatUsedInIndicationFile()
        {
            string[] coverageLimits = Array.Empty<string>();
            string[] coverageLabels = CoveragesLabel.Where(x => x.Text != "Total Premium").Select(x => x.Text).ToArray();
            foreach (var item in coverageLabels)
            {
                var coverage = typeof(CoveragesType).GetFields()
                                                .Where(x => item.Contains(x.GetValue(null).ToString()))
                                                .Select(x => x.GetValue(null).ToString())
                                                .FirstOrDefault();
                if (coverage != null)
                {
                    string limitType = item.Replace(coverage, "").Trim();
                    string limit = GetText(_lblCoverageLimit(item), 3);
                    string coverageLimit = $"{CoveragesType.GetFullName(coverage)} {limit} {limitType}".Replace("  ", " ").Trim();
                    coverageLimits = coverageLimits.Concat(new string[] { coverageLimit }).ToArray();
                }
                else
                {
                    throw new Exception($"'{item}' does not match any CoveragesType");
                }
            }
            GetLastNode().Info(string.Join(" - ", coverageLimits));
            return coverageLimits;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public SummaryPage ValidateSummaryPageDisplayed()
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
        public SummaryPage ValidateBusinessNameDisplayedCorrectly(string expectedBusinessName)
        {
            var node = GetLastNode();
            try
            {
                if (BusinessNameLabel.Text.Trim() == expectedBusinessName)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessNameDisplayedCorrectly, expectedValue: expectedBusinessName);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessNameDisplayedCorrectly, expectedValue: expectedBusinessName, actualValue: BusinessNameLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessNameDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateDBADisplayedCorrectly(string expectedDBA)
        {
            var node = GetLastNode();
            try
            {
                if (DBALabel.Text.Trim() == expectedDBA)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDBADisplayedCorrectly, expectedValue: expectedDBA);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDBADisplayedCorrectly, expectedValue: expectedDBA, actualValue: DBALabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDBADisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateBusinessAddressDisplayedCorrectly(string expectedBussinessAddress)
        {
            var node = GetLastNode();
            try
            {
                if (BusinessAddressLabel.Text.Trim() == expectedBussinessAddress)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessAddressDisplayedCorrectly, expectedValue: expectedBussinessAddress);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessAddressDisplayedCorrectly, expectedValue: expectedBussinessAddress, actualValue: BusinessAddressLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessAddressDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidatePrimaryOfficerNameDisplayedCorrectly(string expectedPrimaryOfficerName)
        {
            var node = GetLastNode();
            try
            {
                if (PrimaryOfficerNameLabel.Text.Trim() == expectedPrimaryOfficerName)
                {
                    SetPassValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, expectedValue: expectedPrimaryOfficerName);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, expectedValue: expectedPrimaryOfficerName, actualValue: PrimaryOfficerNameLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrimaryOfficerNameDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateVehiclesDisplayedCorrectly(params string[] expectedVehicles)
        {
            var node = GetLastNode();
            try
            {
                int total = GetElementNumber(_lblVehicles);
                if (expectedVehicles.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, "Total Vehicles", expectedVehicles.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < expectedVehicles.Length; i++)
                    {
                        if (VehiclesLabel[i].Text.Trim() == expectedVehicles[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, expectedValue: expectedVehicles[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateVehiclesDisplayedCorrectly, expectedValue: expectedVehicles[i], actualValue: VehiclesLabel[i].Text.Trim());
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
        public SummaryPage ValidateDriversDisplayedCorrectly(params string[] expectedDrivers)
        {
            var node = GetLastNode();
            try
            {
                int total = GetElementNumber(_lblDrivers);
                if (expectedDrivers.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriversDisplayedCorrectly, "Total Drivers", expectedDrivers.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < expectedDrivers.Length; i++)
                    {
                        if (DriversLabel[i].Text.Trim() == expectedDrivers[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateDriversDisplayedCorrectly, expectedValue: expectedDrivers[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateDriversDisplayedCorrectly, expectedValue: expectedDrivers[i], actualValue: DriversLabel[i].Text.Trim());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriversDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateVehicleDeleted(string delectedVehicle)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblVehicle(delectedVehicle)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateVehicleDeleted);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehicleDeleted);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleDeleted, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateCoveragesDisplayed(params string[] expectedCoverages)
        {
            var node = GetLastNode();
            try
            {
                foreach (string coverage in expectedCoverages)
                {
                    if (IsElementDisplayed(_lblCoverage(coverage)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoveragesDisplayed, coverage);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoveragesDisplayed, coverage);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragesDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateCoveragesNotDisplayed(params string[] expectedCoverages)
        {
            var node = GetLastNode();
            try
            {
                foreach (string coverage in expectedCoverages)
                {
                    if (!IsElementDisplayed(_lblCoverage(coverage)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoveragesNotDisplayed, coverage);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoveragesNotDisplayed, coverage);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragesNotDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateTotalPremiumCalculatedAndDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblTotal))
                {
                    string total = TotalLabel.Text;
                    if (Regex.IsMatch(total, @"(\$)([\d][.\d,]*)"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumCalculatedAndDisplayed, additionalInfo: total);
                    }
                    else if (EnvironmentSetting.EnvironmentName == "iso" && total.Contains("X"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumCalculatedAndDisplayed, additionalInfo: total, expectedValue: $"X was accepted in {EnvironmentSetting.EnvironmentName} environment");
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateTotalPremiumCalculatedAndDisplayed, additionalInfo: total);
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumCalculatedAndDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumCalculatedAndDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateTotalPremiumDisplayedCorrectly(string expectedPremium)
        {
            var node = GetLastNode();
            try
            {
                string actualPremium = GetText(_lblTotal).Trim();
                if (expectedPremium == actualPremium)
                {
                    SetPassValidation(node, ValidationMessage.ValidateTotalPremiumDisplayedCorrectly, expectedValue: expectedPremium);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumDisplayedCorrectly, expectedValue: expectedPremium, actualValue: actualPremium);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateInsuranceScoreStatusDisplayedCorrectly(string expectedStatus)
        {
            var node = GetLastNode();
            try
            {
                string actualStatus = GetText(_lblInsuranceScoreStatus);
                if (actualStatus == expectedStatus)
                {
                    SetPassValidation(node, ValidationMessage.ValidateInsuranceScoreStatusDisplayedCorrectly, expectedValue: expectedStatus);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateInsuranceScoreStatusDisplayedCorrectly, expectedValue: expectedStatus, actualValue: actualStatus);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateInsuranceScoreStatusDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidatePremiumInLeftNavigationMatchesSummaryScreen()
        {
            var node = GetLastNode();

            List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();

            try
            {
                foreach (var item in CoveragesLabel)
                {
                    string coverageType = item.Text;
                    if (coverageType == "Total Premium")
                    {
                        comparisons.Add(new KeyValuePair<string, string[]>(coverageType, new string[] { GetTextFromPageSource(_lblNavTotal), GetText(_lblCoveragePremium(coverageType)) }));
                    }
                    else
                    {
                        // Get coverage name that is used in left navigation
                        string navCoverageName = CoveragesType.GetShortName(coverageType);
                        if (navCoverageName == coverageType)
                            navCoverageName = AdditionalCoveragesType.GetShortName(coverageType);
                        comparisons.Add(new KeyValuePair<string, string[]>(coverageType + " Premium", new string[] { GetTextFromPageSource(_lblNavPremium(navCoverageName)), GetText(_lblCoveragePremium(coverageType)) }));
                    }
                }

                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesSummaryScreen, item.Key, item.Value[1]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesSummaryScreen, item.Key, item.Value[1], item.Value[0]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesSummaryScreen, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidatePrintIndicationButtonEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementEnabled(_btnPrint))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePrintIndicationButtonEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePrintIndicationButtonEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrintIndicationButtonEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateSubmitButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnSubmit))
                {
                    SetPassValidation(node, ValidationMessage.ValidateSubmitButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateSubmitButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSubmitButtonDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateBindAccountButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnBindAccount))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBindAccountButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBindAccountButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBindAccountButtonDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateBindAccountButtonEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementEnabled(_btnBindAccount))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBindAccountButtonEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBindAccountButtonEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBindAccountButtonEnabled, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateSubmittedButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnSubmitted))
                {
                    SetPassValidation(node, ValidationMessage.ValidateSubmittedButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateSubmittedButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSubmittedButtonDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateBrowseButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnBrowse))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBrowseButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBrowseButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBrowseButtonDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateAttachmentsNoteDisplayedCorrectly(string expectedNote)
        {
            var node = GetLastNode();
            try
            {
                string actualNote = GetText(_lblAttachmentsNote);
                if (actualNote == expectedNote)
                {
                    SetPassValidation(node, ValidationMessage.ValidateAttachmentsNoteDisplayedCorrectly, expectedValue: expectedNote);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAttachmentsNoteDisplayedCorrectly, expectedValue: expectedNote, actualValue: actualNote);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAttachmentsNoteDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateAttachmentsDisplayedCorrectly(string[] expectedAttachments)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(expectedAttachments, "Attachments");
                int total = GetElementNumber(_lblFiles);
                if (expectedAttachments.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateAttachmentsDisplayedCorrectly, "Total Attachments", expectedAttachments.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    for (int i = 0; i < expectedAttachments.Length; i++)
                    {
                        if (FilesLabel[i].Text.Contains(expectedAttachments[i]))
                        {
                            SetPassValidation(node, ValidationMessage.ValidateAttachmentsDisplayedCorrectly, expectedValue: expectedAttachments[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateAttachmentsDisplayedCorrectly, expectedValue: expectedAttachments[i], actualValue: FilesLabel[i].Text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAttachmentsDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateFileSizeLimitWarningAlertDisplayedCorrectly(string expectedMessage)
        {
            var node = GetLastNode();
            try
            {
                if (IsAlertDisplayed(waitForElementTimeout))
                {
                    string actualMessage = WebDriver.Alert.Text;
                    if (actualMessage == expectedMessage)
                    {
                        SetPassValidation(node, ValidationMessage.ValidateFileSizeLimitWarningAlertDisplayedCorrectly, expectedValue: expectedMessage);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateFileSizeLimitWarningAlertDisplayedCorrectly, expectedValue: expectedMessage, actualValue: actualMessage);
                    }
                    WebDriver.Alert.Accept();
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateFileSizeLimitWarningAlertDisplayedCorrectly, expectedValue: expectedMessage);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateFileSizeLimitWarningAlertDisplayedCorrectly, e);
            }
            return this;
        }

        [ExtentStepNode]
        public SummaryPage ValidateCoveragePremiumsNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                foreach (var item in CoveragesLabel)
                {
                    string coverageType = item.Text;
                    if (coverageType != "Total Premium")
                    {
                        if (string.IsNullOrEmpty(GetText(_lblCoveragePremium(coverageType))))
                        {
                            SetPassValidation(node, ValidationMessage.ValidateCoveragePremiumsNotDisplayed, coverageType);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateCoveragePremiumsNotDisplayed, coverageType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragePremiumsNotDisplayed, e);
            }

            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Summary page is displayed";
            public static string ValidateBusinessNameDisplayedCorrectly = "Validate Business Name is displayed correctly";
            public static string ValidateDBADisplayedCorrectly = "Validate DBA is displayed correctly";
            public static string ValidateBusinessAddressDisplayedCorrectly = "Validate Business Address is displayed correctly";
            public static string ValidatePrimaryOfficerNameDisplayedCorrectly = "Validate Primary Officer Name is displayed correctly";
            public static string ValidateVehiclesDisplayedCorrectly = "Validate Vehicles are displayed correctly";
            public static string ValidateDriversDisplayedCorrectly = "Validate Drivers are displayed correctly";
            public static string ValidateVehicleDeleted = "Validate vehicle is deleted";
            public static string ValidateTotalPremiumCalculatedAndDisplayed = "Validate Total Premium is calculated and displayed (with format $#,### and does not contain character(s) \"X\")";
            public static string ValidateInsuranceScoreStatusDisplayedCorrectly = "Validate Insurance score status is displayed correctly";
            public static string ValidatePremiumInLeftNavigationMatchesSummaryScreen = "Validate premium in left navigation matches summary screen";
            public static string ValidatePrintIndicationButtonEnabled = "Validate Print Indication button is enabled";
            public static string ValidateSubmitButtonDisplayed = "Validate Submit button is displayed";
            public static string ValidateBindAccountButtonDisplayed = "Validate Bind Account button is displayed";
            public static string ValidateBindAccountButtonEnabled = "Validate Bind Account button is enabled";
            public static string ValidateSubmittedButtonDisplayed = "Validate Submitted button is displayed";
            public static string ValidateBrowseButtonDisplayed = "Validate Browse button is displayed";
            public static string ValidateAttachmentsNoteDisplayedCorrectly = "Validate attachments note is displayed correctly";
            public static string ValidateAttachmentsDisplayedCorrectly = "Validate attachments are displayed correctly";
            public static string ValidateFileSizeLimitWarningAlertDisplayedCorrectly = "Validate File size limit warning alert is displayed correctly";
            public static string ValidateTotalPremiumDisplayedCorrectly = "Validate Total Premium is displayed correctly";
            public static string ValidateCoveragesDisplayed = "Validate Coverages are displayed";
            public static string ValidateCoveragesNotDisplayed = "Validate Coverages are not displayed";
            public static string ValidateCoveragePremiumsNotDisplayed = "Validate Coverage premiums are not displayed";
        }
        #endregion
    }
}