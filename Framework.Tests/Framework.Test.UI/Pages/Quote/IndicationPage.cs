using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class IndicationPage : LandingPage
    {
        #region Variables
        private readonly string indicationPDFFilePath = System.IO.Path.Combine(WebDriver.Property.DownloadLocation, "Indication_{0}.pdf");
        #endregion

        #region Locators
        private By _btnPrint => By.XPath("//button[.='PRINT lNDlCATlON']");
        private By _lblCongratulations => By.XPath("//breeze-page-header//h1[contains(text(),'Congratulations!')]");
        private By _lblPremiumValue => By.XPath("//breeze-submitted//h2");
        #endregion

        #region Elements
        public IWebElement PrintButton => StableFindElement(_btnPrint);
        public IWebElement PremiumValuelabel => StableFindElement(_lblPremiumValue);
        #endregion

        #region Business Methods
        public IndicationPage() : base()
        {
            Url = "forms/submitted";
            RequiredElementLocator = _btnPrint;
        }

        public SummaryPage SelectPreviousButton()
        {
            return SelectPreviousButton<SummaryPage>();
        }

        [ExtentStepNode]
        public IndicationPage SelectPrintButton()
        {
            string submissionNumber = GetSubmissionNumber();
            if (File.Exists(string.Format(indicationPDFFilePath, submissionNumber))) ;
            {
                File.Delete(string.Format(indicationPDFFilePath, submissionNumber));
            }
            PrintButton.Click();
            //Handle download notification bar on IE browser
            if (WebDriver.Property.DriverType == DriverType.InternetExplorer)
            {
                string filePath = string.Format(indicationPDFFilePath, GetSubmissionNumber());
                int tabCount = IsElementPresent(_btnGiveFeedback) ? 4 : 2;
                Wait(10);
                var action = WebDriver.Actions;
                action.SendKeys(string.Concat(Enumerable.Repeat(Keys.Tab, tabCount)) + Keys.Enter);
                action.Perform();
                // Try to download again
                if (!FileHelper.DoesFileExist(filePath, waitForDownloadTimeout))
                {
                    GetLastNode().Info("Try to download file again using Alt+S keys");
                    try
                    {
                        action.KeyDown(Keys.Alt);
                        action.SendKeys("s");
                        action.KeyUp(Keys.Alt);
                        action.Perform();
                    }
                    catch (Exception)
                    {
                        // Skip exception
                    }
                }
            }
            return this;
        }

        public List<KeyValuePair<string, string>> GetPremiumsOfCoverageLimitsFromIndicationFile(params string[] coverageLimits)
        {
            string submissionNumber = GetSubmissionNumber();
            string beginCoverageLabel = "Coverage Limit Annual Premium**";
            string totalLabel = "Total Indicated Annual Premium";
            string endCoverageLabel = "* Numbers displayed have been provided by the agent.";
            string text = string.Empty;
            // Read pdf content
            using (PdfReader reader = new PdfReader(string.Format(indicationPDFFilePath, submissionNumber)))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page) + "\n";
                }
            }
            // Get all coverage premium infos
            int startIndex = text.IndexOf(beginCoverageLabel) + beginCoverageLabel.Length;
            int endIndex = text.IndexOf(totalLabel);
            string[] premiumlnfos = text.Substring(startIndex, endIndex - startIndex)
            .Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim())
            .ToArray();
            List<KeyValuePair<string, string>> premiumList = new List<KeyValuePair<string, string>>();
            foreach (var coverage in coverageLimits)
            {
                string premium = "";
                if (coverage != "Total")
                {
                    foreach (var item in premiumlnfos)
                    {
                        if (item.StartsWith(coverage))
                        {
                            // Get premium
                            premium = item.Replace(coverage, "").Trim();
                            break;
                        }
                    }
                }
                else
                {
                    // Get total premium
                    startIndex = text.IndexOf(totalLabel) + totalLabel.Length;
                    endIndex = text.IndexOf(endCoverageLabel);
                    premium = text.Substring(startIndex, endIndex - startIndex).Replace("\n", "").Trim();
                }
                premiumList.Add(new KeyValuePair<string, string>(coverage, premium));
            }
            return premiumList;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public IndicationPage ValidatePrintIndicationButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnPrint))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePrintIndicationButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePrintIndicationButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePrintIndicationButtonDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public IndicationPage ValidatePremiumValueDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblPremiumValue))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePremiumValueDispIayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePremiumValueDispIayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumValueDispIayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public IndicationPage ValidateCongratulationsMessageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblCongratulations))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCongratulationsMessageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCongratulationsMessageDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCongratulationsMessageDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public IndicationPage ValidateIndicationPageDisplayed()
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
        public IndicationPage ValidatePDFFileDownloadedSuccessfully()
        {
            var node = GetLastNode();
            string filePath = string.Format(indicationPDFFilePath, GetSubmissionNumber());
            try
            {
                if (FileHelper.DoesFileExist(filePath, waitForDownloadTimeout))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePDFFileDownloadedSuccessfully, filePath);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePDFFileDownloadedSuccessfully, filePath);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePDFFileDownloadedSuccessfully, e, filePath);
            }
            return this;
        }

        [ExtentStepNode]
        public IndicationPage ValidatePremiumsOfCoverageLimitsIndicationFileNotDisplayed(params string[] coverageLimits)
        {
            var node = GetLastNode();
            var actualPremiums = GetPremiumsOfCoverageLimitsFromIndicationFile(coverageLimits);
            try
            {
                foreach (var item in actualPremiums)
                {
                    if (string.IsNullOrEmpty(item.Value))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePremiumsOfCoverageLimitslnlndicationFileNotDisplayed, item.Key);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePremiumsOfCoverageLimitslnlndicationFileNotDisplayed, item.Key, item.Value);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumsOfCoverageLimitslnlndicationFileNotDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public IndicationPage ValidateTotalPremiumInIndicationFileDisplayedCorrectly(string expectedPremium)
        {
            var node = GetLastNode();
            var actualPremiums = GetPremiumsOfCoverageLimitsFromIndicationFile("Total");
            try
            {
                string actualPremium = actualPremiums[0].Value;
                if (actualPremium == expectedPremium)
                {
                    SetPassValidation(node, ValidationMessage.ValidateTotalPremiumInIndicationFileDisplayedCorrectly, expectedValue: expectedPremium);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumInIndicationFileDisplayedCorrectly, expectedValue: expectedPremium, actualValue: actualPremium);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumInIndicationFileDisplayedCorrectly, e);
            }
            return this;
        }
        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Indication page is displayed";
            public static string ValidatePrintIndicationButtonDisplayed = "Validate Print Indication button is displayed";
            public static string ValidatePremiumValueDispIayed = "Validate Premium Value is displayed";
            public static string ValidateCongratulationsMessageDisplayed = "Validate 'Congratulations!' message is displayed";
            public static string ValidatePDFFileDownloadedSuccessfully = "Validate PDF file is downloaded successfully";
            public static string ValidatePremiumsOfCoverageLimitslnlndicationFileNotDisplayed = "Validate premiums of coverage limits in Indication file are not displayed";
            public static string ValidateTotalPremiumInIndicationFileDisplayedCorrectly = "Validate Total Premium in Indication file is displayed correctly";
        }
        #endregion
    }
}
