using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using static Framework.Test.Common.Helper.EnvironmentHelper;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class CoveragesPage : LandingPage
    {
        #region Locators
        // Coverage limit controls
        private By _drpLiabilityLimitType => By.XPath("//mat-select[contains(@data-test-id,'liab-limit-type')]");
        private By _drpLiabilityCslLimit => By.XPath("//mat-select[contains(@data-test-id,'liab-cs-limit')]");
        private By _drpLiabilitySplitLimit => By.XPath("//mat-select[contains(@data-test-id,'liab-split-limit')]");
        private By _drpLiabilitySplitBipdLimit => By.XPath("//mat-select[contains(@data-test-id,'liab-pd-limit')]");
        private By _drpUMLimitType => By.XPath("//mat-select[contains(@data-test-id,'um-limit-type')]");
        private By _drpUMCslLimit => By.XPath("//mat-select[contains(@data-test-id,'um-limit-csl')]");
        private By _drpUMSplitLimit => By.XPath("//mat-select[contains(@data-test-id,'um-limit-split')]");
        private By _drpUMSplitBipdLimit => By.XPath("//mat-select[contains(@data-test-id,'um-limit-split-pd')]");
        private By _drpUMDeductible => By.XPath("//breeze-uninsured-motorist//mat-select[@formcontrolname='deductible']");
        private By _chxUMPD => By.XPath("//mat-checkbox[contains(@data-test-id,'um-limit-split-pd')]");
        private By _chxWaiveCollisionDeductible => By.XPath("//breeze-collision-deductible-waiver//mat-checkbox");
        private By _drpUIMLimitType => By.XPath("//mat-select[contains(@data-test-id,'uim-limit-type')]");
        private By _lblUIMLimitType => By.XPath("//div[text()=' UIM ']/following-sibling::div[1]/div[contains(@class, 'col')][1]/div");
        private By _drpUIMCslLimit => By.XPath("//mat-select[contains(@data-test-id,'uim-limit-csl')]");
        private By _lblUIMCslLimit => By.XPath("//div[text()=' UIM ']/following-sibling::div[1]/div[contains(@class, 'col')][2]/div");
        private By _drpUIMSplitLimit => By.XPath("//mat-select[contains(@data-test-id,'uim-limit-split')]");
        private By _drpUIMSplitBipdLimit => By.XPath("//mat-select[contains(@data-test-id,'uim-limit-split-pd')]");
        private By _drpUMUIMLimitType => By.XPath("//mat-select[contains(@data-test-id,'umUim-limit-type')]");
        private By _drpUMUIMCslLimit => By.XPath("//mat-select[contains(@data-test-id,'umUim-limit-csl')]");
        private By _drpUMUIMSplitLimit => By.XPath("//mat-select[contains(@data-test-id,'umUim-limit-split')]");
        private By _drpUMUIMSplitBipdLimit => By.XPath("//mat-select[contains(@data-test-id,'umUim-limit-split-pd')]");
        private By _drpUMUIMDeductible => By.XPath("//breeze-um-uim//mat-select[@formcontrolname='deductible']");
        private By _chxAddOnCoverage => By.XPath("//mat-checkbox[@formcontrolname='hasCoverage']");
        private By _drpMedicalExpenseBenefitsLimit => By.XPath("//mat-select[contains(@data-test-id,'medical-expense-benefits-limit')]");
        private By _chxIncomeLossBenefits => By.XPath("//mat-checkbox[contains(@data-test-id,'income-loss-benefits-checkbox')]");
        private By _drpMedicalPaymentsLimit => By.XPath("//mat-select[contains(@data-test-id,'med-pay-limit')]");
        private By _drpPersonalInjuryProtection => By.XPath("//breeze-personal-injury-protection//mat-select[contains(@data-test-id,'pip-cov-type')]");
        private By _drpPedestrianPIP => By.XPath("//breeze-pedestrian-pip//mat-select[contains(@data-test-id,'pip-cov-type')]");
        // Premium values
        private By _lblLiabilityPremium => By.XPath("//div[text()=' Liability ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblUMPremium => By.XPath("//div[text()=' UM ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblUMPDPremium => By.XPath("(//mat-checkbox[contains(@data-test-id,'um-limit-split-pd')]/ancestor::div[contains(@class, 'row')]//div[contains(@class, 'col')])[3]//span");
        private By _lblUIMPremium => By.XPath("//div[text()=' UIM ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblUMUIMPremium => By.XPath("//div[text()=' UM/UIM ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblMedicalExpenseBenefitsPremium => By.XPath("//div[text()=' Medical Expense Benefits ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblIncomeLossBenefitsPremium => By.XPath("//div[text()=' Income Loss Benefits ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblMedicalPaymentsPremium => By.XPath("//div[text()=' Medical Payments ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblPersonalInjuryProtectionPremium => By.XPath("//div[text()=' Personal Injury Protection ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        private By _lblRentedVehicleLiabilityPremium => By.XPath("//breeze-premium-only-update[@name='Rented Vehicle Liability']//span[@data-test-id='premium-display']");
        private By _lblRentedVehiclePhysicalDamagePremium => By.XPath("//breeze-premium-only-update[@name='Rented Vehicle Physical Damage']//span[@data-test-id='premium-display']");
        private By _lblPedestrianPIPPremium => By.XPath("//div[text()=' Pedestrian PIP ']/following-sibling::div[1]/div[contains(@class, 'col')][3]//span[1]");
        // Others
        private By _lblPremiums = By.XPath("//div[@class='coverages-wrapper']//div[3]/span[1]");
        private By _lblTotal => By.XPath("//div[@class='row' and .//*[text()='Total Annual Premium']]//span");
        private By _btnCalculate => By.XPath("//button/span[.='Calculate']");
        private By _btnBreezeHelp => By.XPath("//breeze-help-button");
        private By _btnQuoteSummary => By.XPath("//breeze-spin-button");
        private By _iconEditAdditionalCoverages = By.XPath("//*[./span[.='Additional Coverages']]//mat-icon[@svgicon='edit']");
        private By _lblDisclaimer => By.XPath("//div[contains(@class, 'disclaimer')]");
        private By _lblAdditionalCoveragesSection => By.XPath("//span[.='Additional Coverages']");
        private By _lblCoveragesSection(string item) => By.XPath($"//div[@class='coverages-wrapper']//div[contains(text(), ' {item} ')]");
        private By _lblAdditionalCoverages => By.XPath("//breeze-premium-only-coverage//div[contains(@class, 'bar')]");
        private By _lblAdditionalCoverage(string item) => By.XPath($"//breeze-premium-only-coverage//div[contains(@class, 'bar') and contains(text(), '{item}')]");
        private By _lblAdditionalCoveragePremium(string item) => By.XPath($"//breeze-premium-only-coverage//div[contains(@class, 'bar') and contains(text(), '{item}')]/following-sibling::div//span");
        private By _lblErrorMessage(string message) => By.XPath($"//div[contains(@class, 'error')]/div[normalize-space(text())=\"{message}\"]");
        private By _lblErrorMessages => By.XPath("//div[contains(@class, 'error')]/div");
        #endregion

        #region Elements
        // Coverage limit controls
        public IWebElement LiabilityLimitTypeDropdown => StableFindElement(_drpLiabilityLimitType);
        public IWebElement LiabilityCslLimitDropdown => StableFindElement(_drpLiabilityCslLimit);
        public IWebElement LiabilitySplitLimitDropdown => StableFindElement(_drpLiabilitySplitLimit);
        public IWebElement LiabilitySplitBipdLimitDropdown => StableFindElement(_drpLiabilitySplitBipdLimit);
        public IWebElement UMLimitTypeDropdown => StableFindElement(_drpUMLimitType);
        public IWebElement UMCslLimitDropdown => StableFindElement(_drpUMCslLimit);
        public IWebElement UMSplitLimitDropdown => StableFindElement(_drpUMSplitLimit);
        public IWebElement UMSplitBipdLimitDropdown => StableFindElement(_drpUMSplitBipdLimit);
        public IWebElement UMDeductibleDropdown => StableFindElement(_drpUMDeductible);
        public IWebElement UMPDCheckbox => StableFindElement(_chxUMPD);
        public IWebElement WaiveCollisionDeductibleCheckbox => StableFindElement(_chxWaiveCollisionDeductible);
        public IWebElement UIMLimitTypeDropdown => StableFindElement(_drpUIMLimitType);
        public IWebElement UIMCslLimitDropdown => StableFindElement(_drpUIMCslLimit);
        public IWebElement UIMSplitLimitDropdown => StableFindElement(_drpUIMSplitLimit);
        public IWebElement UIMSplitBipdLimitDropdown => StableFindElement(_drpUIMSplitBipdLimit);
        public IWebElement UMUIMLimitTypeDropdown => StableFindElement(_drpUMUIMLimitType);
        public IWebElement UMUIMCslLimitDropdown => StableFindElement(_drpUMUIMCslLimit);
        public IWebElement UMUIMSplitLimitDropdown => StableFindElement(_drpUMUIMSplitLimit);
        public IWebElement UMUIMSplitBipdLimitDropdown => StableFindElement(_drpUMUIMSplitBipdLimit);
        public IWebElement UMUIMDeductibleDropdown => StableFindElement(_drpUMUIMDeductible);
        public IWebElement AddOnCoverageCheckbox => StableFindElement(_chxAddOnCoverage);
        public IWebElement MedicalExpenseBenefitsLimitDropdown => StableFindElement(_drpMedicalExpenseBenefitsLimit);
        public IWebElement IncomeLossBenefitsCheckbox => StableFindElement(_chxIncomeLossBenefits);
        public IWebElement MedicalPaymentsLimitDropdown => StableFindElement(_drpMedicalPaymentsLimit);
        public IWebElement PersonalInjuryProtectionDropdown => StableFindElement(_drpPersonalInjuryProtection);
        public IWebElement PedestrianPIPDropdown => StableFindElement(_drpPedestrianPIP);
        // Premium values
        public IWebElement LiabilityPremiumLabel => StableFindElement(_lblLiabilityPremium);
        public IWebElement UMPremiumLabel => StableFindElement(_lblUMPremium);
        public IWebElement UMPDPremiumLabel => StableFindElement(_lblUMPDPremium);
        public IWebElement UIMPremiumLabel => StableFindElement(_lblUIMPremium);
        public IWebElement UMUIMPremiumLabel => StableFindElement(_lblUMUIMPremium);
        public IWebElement MedicalExpenseBenefitsPremiumLabel => StableFindElement(_lblMedicalExpenseBenefitsPremium);
        public IWebElement IncomeLossBenefitsPremiumLabel => StableFindElement(_lblIncomeLossBenefitsPremium);
        public IWebElement MedicalPaymentPremiumLabel => StableFindElement(_lblMedicalPaymentsPremium);
        public IWebElement PersonalInjuryProtectionPremiumLabel => StableFindElement(_lblPersonalInjuryProtectionPremium);
        public IWebElement RentedVehicleLiabilityPremiumLabel => StableFindElement(_lblRentedVehicleLiabilityPremium);
        public IWebElement RentedVehiclePhysicalDamagePremiumLabel => StableFindElement(_lblRentedVehiclePhysicalDamagePremium);
        public IWebElement PedestrianPIPPremiumLabel => StableFindElement(_lblPedestrianPIPPremium);
        // Others
        public IWebElement TotalLabel => StableFindElement(_lblTotal);
        public IWebElement DisclaimerLabel => StableFindElement(_lblDisclaimer);
        public IWebElement EditAdditionalCoveragesIcon => StableFindElement(_iconEditAdditionalCoverages);
        public IWebElement CalculateButton => StableFindElement(_btnCalculate);
        public IWebElement QuoteSummaryButton => StableFindElement(_btnQuoteSummary);

        public ReadOnlyCollection<IWebElement> PremiumLabels => StableFindElements(_lblPremiums);
        public ReadOnlyCollection<IWebElement> AdditionalCoverageLabels => StableFindElements(_lblAdditionalCoverages);
        public ReadOnlyCollection<IWebElement> ErrorMessageLabels => StableFindElements(_lblErrorMessages);
        #endregion

        #region Business Methods
        public CoveragesPage() : base()
        {
            Url = "forms/coverages";
            RequiredElementLocator = _lblTotal;
        }

        public AdditionalCoveragesPage SelectNextButton()
        {
            return SelectNextButton<AdditionalCoveragesPage>();
        }

        [ExtentStepNode]
        public CoveragesPage SelectCoverageLimits(CoverageLimits coverageLimits)
        {
            var node = GetLastNode();
            node.LogDataInfo(coverageLimits);
            if (coverageLimits != null)
            {
                var coverageList = coverageLimits.GetType().GetProperties().Where(x => x.Name != "Recalculate" && x.GetValue(coverageLimits, null) != null);
                foreach (var coverageInfo in coverageList)
                {
                    if (coverageInfo.PropertyType == typeof(string))
                    {
                        string coverageValue = coverageInfo.GetValue(coverageLimits, null).ToString();
                        node.Info($"Select {coverageInfo.ToMemberDescription()}: {coverageValue}");
                        if (coverageValue != GetText(ControlLocator(WebControl.Dropdown, coverageInfo.ToMemberDescription())))
                        {
                            Control(WebControl.Dropdown, coverageInfo.ToMemberDescription()).SelectByText(coverageValue);
                        }
                    }
                    else if (coverageInfo.PropertyType == typeof(bool?))
                    {
                        bool? coverageValue = (bool?)coverageInfo.GetValue(coverageLimits, null);
                        node.Info($"Set {coverageInfo.ToMemberDescription()}: {coverageValue.Value}");
                        if (coverageValue.Value)
                        {
                            Control(WebControl.Checkbox, coverageInfo.ToMemberDescription()).Check();
                        }
                        else
                        {
                            Control(WebControl.Checkbox, coverageInfo.ToMemberDescription()).UnCheck();
                        }
                    }
                    WaitForElementExists(_lblTotal, throwException: false);
                }
            }

            if (coverageLimits != null && coverageLimits.Recalculate)
            {
                WaitForElementExists(_btnCalculate, throwException: false);
            }
            else
            {
                WaitForElementEnabled(_btnNext);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage SelectCoverageLimit(string controlName, object value)
        {
            if (value is string)
            {
                GetLastNode().Info($"Select {controlName}: {value}");
                if (Control(WebControl.Dropdown, controlName).Text != value.ToString())
                {
                    Control(WebControl.Dropdown, controlName).SelectByText(value.ToString());
                }
            }
            else if (value is bool)
            {
                GetLastNode().Info($"Set {controlName}: {value}");
                if ((bool)value)
                {
                    Control(WebControl.Checkbox, controlName).Check();
                }
                else
                {
                    Control(WebControl.Checkbox, controlName).UnCheck();
                }
            }
            WaitForElementExists(_lblTotal, throwException: false);

            return this;
        }

        [ExtentStepNode]
        public SummaryPage SelectQuoteSummaryButton()
        {
            QuoteSummaryButton.Click();
            var page = new SummaryPage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public CoveragesPage SelectCalculateButton()
        {
            CalculateButton.ClickWithJS();
            WaitForElementExists(_lblTotal, throwException: false);
            // Delay 3s to make sure the premium was re-calculated
            Wait(3);
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage SelectEditAdditionalCoveragesIcon()
        {
            EditAdditionalCoveragesIcon.ClickWithJS();
            var page = new AdditionalCoveragesOverviewPage();
            page.WaitForPageLoad();
            return page;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public CoveragesPage ValidatePreviousCoveragesListed(CoverageLimits coverageLimits)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(coverageLimits, "Coverage Limits");
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();
                var coverageList = coverageLimits.GetType().GetProperties().Where(x => x.Name != "Recalculate" && x.GetValue(coverageLimits, null) != null);
                foreach (var coverageInfo in coverageList)
                {
                    if (coverageInfo.PropertyType == typeof(string))
                    {
                        string coverageValue = coverageInfo.GetValue(coverageLimits, null).ToString();
                        comparisons.Add(new KeyValuePair<string, string[]>(coverageInfo.ToMemberDescription(), new string[] { coverageValue, GetText(ControlLocator(WebControl.Dropdown, coverageInfo.ToMemberDescription())) }));
                    }
                    else if (coverageInfo.PropertyType == typeof(bool?))
                    {
                        bool? coverageValue = (bool?)coverageInfo.GetValue(coverageLimits, null);
                        comparisons.Add(new KeyValuePair<string, string[]>(coverageInfo.ToMemberDescription(), new string[] { coverageValue.Value.ToString(), GetCheckboxStatus(ControlLocator(WebControl.Checkbox, coverageInfo.ToMemberDescription())).ToString() }));
                    }
                }

                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePreviousCoveragesListed, item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePreviousCoveragesListed, item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePreviousCoveragesListed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidatePremiumInLeftNavigationMatchesCoveragesScreen()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();

                // Coverage premiums
                var coveragePremiumList = typeof(CoveragePremiumsType).GetFields()
                                            .Where(x =>
                                            {
                                                return IsElementDisplayed(ControlLocator(WebControl.Label, x.GetValue(null).ToString()));
                                            })
                                            .Select(x => x.GetValue(null).ToString());

                foreach (var premiumName in coveragePremiumList)
                {
                    string coverageType = premiumName.Replace("Premium", "").Trim();
                    // name to find premium control on coverage screen
                    string coverageName = coverageType;
                    // name to find premium control on left nav
                    string navCoverageName = CoveragesType.GetShortName(coverageType);
                    // get selected limit type that was used in full name of Liability - UM - UIM - UM/UIM coverages
                    string dynamicLimitType = CoveragesType.GetDynamicLimitType(coverageType);
                    if (dynamicLimitType != null)
                    {
                        string limitType = GetText(ControlLocator(WebControl.Dropdown, dynamicLimitType));
                        navCoverageName += " " + limitType;
                        coverageName += " " + limitType;
                    }
                    comparisons.Add(new KeyValuePair<string, string[]>(coverageName + " Premium", new string[] { GetTextFromPageSource(_lblNavPremium(navCoverageName)), GetText(ControlLocator(WebControl.Label, premiumName)) }));
                }

                // Additional Coverage premiums
                if (IsElementDisplayed(_lblAdditionalCoverages))
                {
                    foreach (var coverage in AdditionalCoverageLabels)
                    {
                        // name to find premium control on coverage screen
                        string coverageName = coverage.Text.Trim();
                        // name to find premium control on left nav
                        string navCoverageName = AdditionalCoveragesType.GetShortName(coverageName);
                        comparisons.Add(new KeyValuePair<string, string[]>(coverageName + " Premium", new string[] { GetTextFromPageSource(_lblNavPremium(navCoverageName)), GetText(_lblAdditionalCoveragePremium(coverageName)) }));
                    }
                }

                // Total premiums
                if (IsElementDisplayed(_lblTotal))
                {
                    comparisons.Add(new KeyValuePair<string, string[]>("Total Premium", new string[] { GetTextFromPageSource(_lblNavTotal), GetText(_lblTotal) }));
                }

                foreach (var item in comparisons)
                {
                    if (item.Value[1] != "N/A")
                    {
                        if (item.Value[0] == item.Value[1])
                        {
                            SetPassValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesCoveragesScreen, item.Key, item.Value[1]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesCoveragesScreen, item.Key, item.Value[1], item.Value[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesCoveragesScreen, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateTotalPremiumRecalculatedAndDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblTotal))
                {
                    string total = TotalLabel.Text;
                    if (Regex.IsMatch(total, @"(\$)([\d][.\d,]*)"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumRecalculatedAndDisplayed, additionalInfo: total);
                    }
                    else if (EnvironmentSetting.EnvironmentName == "iso" && total.Contains("X"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumRecalculatedAndDisplayed, additionalInfo: total, expectedValue: $"X was accepted in {EnvironmentSetting.EnvironmentName} environment");
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateTotalPremiumRecalculatedAndDisplayed, additionalInfo: total);
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumRecalculatedAndDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumRecalculatedAndDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateTotalPremiumNotContainCharacterX()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblTotal))
                {
                    string total = TotalLabel.Text;
                    if (!total.Contains("X"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumNotContainCharacterX, additionalInfo: total);
                    }
                    else if (EnvironmentSetting.EnvironmentName == "iso")
                    {
                        SetPassValidation(node, ValidationMessage.ValidateTotalPremiumNotContainCharacterX, additionalInfo: total, expectedValue: $"X was accepted in {EnvironmentSetting.EnvironmentName} environment");
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateTotalPremiumNotContainCharacterX, additionalInfo: total);
                    }
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumNotContainCharacterX);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumNotContainCharacterX, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidatePremiumsNoLongerShown()
        {
            var node = GetLastNode();
            try
            {
                string[] premiums = PremiumLabels.Select(x => x.Text).ToArray();
                string actualPremiums = string.Join(" - ", premiums);
                if (premiums.All(x => (x.Contains("X") || x == "N/A")))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePremiumsNoLongerShown, additionalInfo: actualPremiums);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePremiumsNoLongerShown, additionalInfo: actualPremiums);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumsNoLongerShown, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidatePremiumsRecalculatedAndDisplayed(params string[] expectedPremiums)
        {
            var node = GetLastNode();
            try
            {
                foreach (var premium in expectedPremiums)
                {
                    string premiumName = premium.EndsWith("Premium") ? premium : premium + " Premium";
                    string actualPremium = GetText(ControlLocator(WebControl.Label, premiumName));
                    if (Regex.IsMatch(actualPremium, @"(\$)([\d][.\d,]*)") || actualPremium == "N/A")
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: $"{premiumName} - Value: {actualPremium}");
                    }
                    else if (EnvironmentSetting.EnvironmentName == "iso" && actualPremium.Contains("X"))
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: $"{premiumName} - Value: {actualPremium}", expectedValue: $"X was accepted in {EnvironmentSetting.EnvironmentName} environment");
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: $"{premiumName} - Value: {actualPremium}");
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidatePremiumsRecalculatedAndDisplayed()
        {
            var node = GetLastNode();
            try
            {
                string[] premiums = PremiumLabels.Select(x => x.Text).ToArray();
                string actualPremiums = string.Join(" - ", premiums);

                if (premiums.All(x => (Regex.IsMatch(x, @"(\$)([\d][.\d,]*)") || x == "N/A")))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: actualPremiums);
                }
                else if (EnvironmentSetting.EnvironmentName == "iso" && premiums.All(x => (Regex.IsMatch(x, @"(\$)([\d][.\d,]*)") || x == "N/A" || x.Contains("X"))))
                {
                    SetPassValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: actualPremiums, expectedValue: $"X was accepted in {EnvironmentSetting.EnvironmentName} environment");
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, additionalInfo: actualPremiums);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumsRecalculatedAndDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateCoveragesPageDisplayed()
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
        public CoveragesPage ValidateCalculateButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnCalculate))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCalculateButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCalculateButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCalculateButtonDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateAdditionalCoveragesEditButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_iconEditAdditionalCoverages))
                {
                    SetPassValidation(node, ValidationMessage.ValidateAdditionalCoveragesEditButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAdditionalCoveragesEditButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAdditionalCoveragesEditButtonDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateAdditionalCoveragesSectionDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblAdditionalCoveragesSection))
                {
                    SetPassValidation(node, ValidationMessage.ValidateAdditionalCoveragesSectionDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAdditionalCoveragesSectionDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAdditionalCoveragesSectionDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateCoverageSectionsDisplayed(params string[] coverageSections)
        {
            var node = GetLastNode();
            try
            {
                for (int i = 0; i < coverageSections.Length; i++)
                {
                    if (IsElementDisplayed(_lblCoveragesSection(coverageSections[i])))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoverageSectionsDisplayed, coverageSections[i]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoverageSectionsDisplayed, coverageSections[i]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageSectionsDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateCoverageSectionsNotDisplayed(params string[] coverageSections)
        {
            var node = GetLastNode();
            try
            {
                for (int i = 0; i < coverageSections.Length; i++)
                {
                    if (!IsElementDisplayed(_lblCoveragesSection(coverageSections[i])))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoverageSectionsNotDisplayed, coverageSections[i]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoverageSectionsNotDisplayed, coverageSections[i]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageSectionsNotDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateCoveragesHeaderDisplayedCorrectly(string header)
        {
            var node = GetLastNode();
            try
            {
                string actualHeader = GetText(_lblHeader).Trim();
                if (actualHeader == header)
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoveragesHeaderDisplayedCorrectly, expectedValue: header);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoveragesHeaderDisplayedCorrectly, expectedValue: header, actualValue: actualHeader);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragesHeaderDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateDisclaimerDisplayedCorrectly(string expectedDisclaimer)
        {
            var node = GetLastNode();
            try
            {
                string actualDisclaimer = GetText(_lblDisclaimer);
                if (actualDisclaimer.Trim() == expectedDisclaimer)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDisclaimerDisplayedCorrectly, expectedValue: expectedDisclaimer);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDisclaimerDisplayedCorrectly, expectedValue: expectedDisclaimer, actualValue: actualDisclaimer);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDisclaimerDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateErrorMessagesDisplayed(params string[] expectedMessages)
        {
            var node = GetLastNode();
            try
            {
                foreach (var message in expectedMessages)
                {
                    if (IsElementDisplayed(_lblErrorMessage(message), 3))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateErrorMessagesDisplayed, message);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateErrorMessagesDisplayed, message);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateErrorMessagesDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateErrorMessagesNotDisplayed(params string[] expectedMessages)
        {
            var node = GetLastNode();
            try
            {
                foreach (var message in expectedMessages)
                {
                    if (!IsElementDisplayed(_lblErrorMessage(message)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateErrorMessagesNotDisplayed, message);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateErrorMessagesNotDisplayed, message);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateErrorMessagesNotDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateControlDisplayed(string controlName, WebControl controlType)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(ControlLocator(controlType, controlName)))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateControlDisplayed, controlName, controlType.ToEnumName().ToLower()));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateControlDisplayed, controlName, controlType.ToEnumName().ToLower()));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateControlDisplayed, controlName, controlType.ToEnumName().ToLower()), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateControlNotDisplayed(string controlName, WebControl controlType)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(ControlLocator(controlType, controlName)))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateControlNotDisplayed, controlName, controlType.ToEnumName().ToLower()));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateControlNotDisplayed, controlName, controlType.ToEnumName().ToLower()));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateControlNotDisplayed, controlName, controlType.ToEnumName().ToLower()), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateControlDisplayedCorrectly(string controlName, WebControl controlType, string expectedValue)
        {
            var node = GetLastNode();
            try
            {
                string actualValue = GetText(ControlLocator(controlType, controlName));
                if (actualValue == expectedValue)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateControlDisplayedCorrectly, controlName, controlType.ToEnumName().ToLower()), expectedValue: expectedValue);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateControlDisplayedCorrectly, controlName, controlType.ToEnumName().ToLower()), expectedValue: expectedValue, actualValue: actualValue);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateControlDisplayedCorrectly, controlName, controlType.ToEnumName().ToLower()), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateDropdownOptionsDisplayedCorrectly(string controlName, params string[] expectedOptions)
        {
            var node = GetLastNode();
            try
            {
                Control(WebControl.Dropdown, controlName).Click();
                WaitForElementVisible(_eleDropdownOptions);
                string[] actualOptions = DropdownOptionsLabel.Select(x => x.Text).ToArray();
                Array.Sort(expectedOptions);
                Array.Sort(actualOptions);
                string expectedOptionsList = string.Join(" - ", expectedOptions);
                string actualOptionsList = string.Join(" - ", actualOptions);

                if (expectedOptionsList == actualOptionsList)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateDropdownOptionsDisplayedCorrectly, controlName), expectedValue: expectedOptionsList);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateDropdownOptionsDisplayedCorrectly, controlName), expectedValue: expectedOptionsList, actualValue: actualOptionsList);
                }

                Control(WebControl.Dropdown, controlName).SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateDropdownOptionsDisplayedCorrectly, controlName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateOptionsDisplayedInDropdown(string controlName, params string[] expectedOptions)
        {
            var node = GetLastNode();
            try
            {
                Control(WebControl.Dropdown, controlName).Click();
                WaitForElementVisible(_eleDropdownOptions);
                string[] actualOptions = DropdownOptionsLabel.Select(x => x.Text).ToArray();
                string actualOptionsList = string.Join(" - ", actualOptions);

                foreach (var option in expectedOptions)
                {
                    if (actualOptions.Contains(option))
                    {
                        SetPassValidation(node, string.Format(ValidationMessage.ValidateOptionsDisplayedInDropdown, option, controlName), actualOptionsList);
                    }
                    else
                    {
                        SetFailValidation(node, string.Format(ValidationMessage.ValidateOptionsDisplayedInDropdown, option, controlName), actualOptionsList);
                    }
                }

                Control(WebControl.Dropdown, controlName).SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateOptionsDisplayedInDropdown, string.Join(" - ", expectedOptions), controlName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateOptionsNotDisplayedInDropdown(string controlName, params string[] expectedOptions)
        {
            var node = GetLastNode();
            try
            {
                Control(WebControl.Dropdown, controlName).Click();
                WaitForElementVisible(_eleDropdownOptions);
                string[] actualOptions = DropdownOptionsLabel.Select(x => x.Text).ToArray();
                string actualOptionsList = string.Join(" - ", actualOptions);

                foreach (var option in expectedOptions)
                {
                    if (!actualOptions.Contains(option))
                    {
                        SetPassValidation(node, string.Format(ValidationMessage.ValidateOptionsNotDisplayedInDropdown, option, controlName), actualOptionsList);
                    }
                    else
                    {
                        SetFailValidation(node, string.Format(ValidationMessage.ValidateOptionsNotDisplayedInDropdown, option, controlName), actualOptionsList);
                    }
                }

                Control(WebControl.Dropdown, controlName).SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateOptionsNotDisplayedInDropdown, string.Join(" - ", expectedOptions), controlName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateADropdownLimitLessThanOrEqualToAnotherDropdownLimit(string controlName1, string controlName2)
        {
            var node = GetLastNode();
            try
            {
                string limit1 = GetText(ControlLocator(WebControl.Dropdown, controlName1));
                string limit2 = GetText(ControlLocator(WebControl.Dropdown, controlName2));
                if (int.Parse(limit1.Replace("$", "").Replace(",", "")) <=
                     int.Parse(limit2.Replace("$", "").Replace(",", "")))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateADropdownLimitLessThanOrEqualToAnotherDropdownLimit, controlName1, limit1, controlName2, limit2));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateADropdownLimitLessThanOrEqualToAnotherDropdownLimit, controlName1, limit1, controlName2, limit2));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateADropdownLimitLessThanOrEqualToAnotherDropdownLimit, controlName1, "unknown", controlName2, "unknown"), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateMinimumDropdownOptionDisplayedCorrectly(string controlName, string expectedMinimumValue)
        {
            var node = GetLastNode();
            try
            {
                Control(WebControl.Dropdown, controlName).Click();
                WaitForElementVisible(_eleDropdownOptions);

                var minValue = DropdownOptionsLabel.Min(x => int.Parse(Utils.GetNumberStringFromCurrency(x.Text)));
                string actualMinimumValue = DropdownOptionsLabel.Where(x => Utils.GetNumberStringFromCurrency(x.Text) == minValue.ToString()).Select(x => x.Text).First();
                if (actualMinimumValue == expectedMinimumValue)
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateMinimumDropdownOptionDisplayedCorrectly, controlName), expectedValue: expectedMinimumValue);
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateMinimumDropdownOptionDisplayedCorrectly, controlName), expectedValue: expectedMinimumValue, actualValue: actualMinimumValue);
                }

                Control(WebControl.Dropdown, controlName).SendKeys(Keys.Tab);
                WaitForElementInvisible(_eleDropdownOptions);
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateMinimumDropdownOptionDisplayedCorrectly, controlName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public CoveragesPage ValidateTotalPremiumDisplayedCorrectly(string expectedPremium)
        {
            var node = GetLastNode();
            try
            {
                string actualPremium = GetText(_lblTotal).Trim();
                if (actualPremium == expectedPremium)
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

        public CoveragesPage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<CoveragesPage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Coverages page is displayed";
            public static string ValidateCalculateButtonDisplayed = "Validate Calculate button is displayed";
            public static string ValidateTotalPremiumRecalculatedAndDisplayed = "Validate Total Premium is recalculated and displayed (with format $#,### and does not contain character(s) \"X\")";
            public static string ValidateTotalPremiumNotContainCharacterX = "Validate Total Premium does not contain character(s) \"X\"";
            public static string ValidatePremiumsRecalculatedAndDisplayed = "Validate Premiums are recalculated and displayed (with format $#,### or N/A and does not contain character(s) \"X\")";
            public static string ValidatePreviousCoveragesListed = "Validate previous selections coverages are listed";
            public static string ValidatePremiumsNoLongerShown = "Validate Premiums are no longer shown (contain character(s) \"X\" or \"N/A\")";
            public static string ValidatePremiumInLeftNavigationMatchesCoveragesScreen = "Validate premium in left navigation matches coverages screen";
            public static string ValidateAdditionalCoveragesEditButtonDisplayed = "Validate Additional Coverages Edit button is displayed";
            public static string ValidateAdditionalCoveragesSectionDisplayed = "Validate Additional Coverages section is displayed";
            public static string ValidateCoverageSectionsDisplayed = "Validate Coverage Sections are displayed";
            public static string ValidateCoverageSectionsNotDisplayed = "Validate Coverage Sections are not displayed";
            public static string ValidateCoveragesHeaderDisplayedCorrectly = "Validate Coverages header is displayed correctly";
            public static string ValidateDisclaimerDisplayedCorrectly = "Validate Disclaimer is displayed correctly";
            public static string ValidateErrorMessagesDisplayed = "Validate error messages are displayed";
            public static string ValidateErrorMessagesNotDisplayed = "Validate error messages are not displayed";
            public static string ValidateControlDisplayed = "Validate {0} {1} is displayed";
            public static string ValidateControlNotDisplayed = "Validate {0} {1} is not displayed";
            public static string ValidateControlDisplayedCorrectly = "Validate {0} {1} is displayed correctly";
            public static string ValidateDropdownOptionsDisplayedCorrectly = "Validate {0} dropdown options are displayed correctly";
            public static string ValidateOptionsDisplayedInDropdown = "Validate ({0}) option is displayed in {1} dropdown";
            public static string ValidateOptionsNotDisplayedInDropdown = "Validate ({0}) option is not displayed in {1} dropdown";
            public static string ValidateADropdownLimitLessThanOrEqualToAnotherDropdownLimit = "Validate {0} ({1}) is less than or equal to {2} ({3})";
            public static string ValidateMinimumDropdownOptionDisplayedCorrectly = "Validate minimum {0} dropdown option is displayed correctly";
            public static string ValidateTotalPremiumDisplayedCorrectly = "Validate Total Premium is displayed correctly";
        }
        #endregion
    }
}