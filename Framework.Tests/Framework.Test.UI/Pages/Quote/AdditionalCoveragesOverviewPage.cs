using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class AdditionalCoveragesOverviewPage : LandingPage
    {
        #region Locators
        private By _iconEdit(string item) => By.XPath($"//breeze-tile[contains(@class, 'additional-coverages') and .//h4[contains(.,\"{item}\")]]//mat-icon[@svgicon='edit']");
        private By _iconDelete(string item) => By.XPath($"//breeze-tile[contains(@class, 'additional-coverages') and .//h4[contains(.,\"{item}\")]]//mat-icon[@svgicon='delete']");
        private By _lblCoverageSection(string item) => By.XPath($"//breeze-tile[contains(@class, 'additional-coverages')]//h4[contains(.,\"{item}\")]");
        private By _lblCoverageSections => By.XPath("//breeze-tile[contains(@class, 'additional-coverages')]//h4");
        private By _btnAddAdditionalCoverages => By.XPath("//button[.='ADD ADDITIONAL COVERAGES']");
        private By _btnCoverageSummary => By.XPath("//button[.='COVERAGE SUMMARY']");
        private By _lblTotalPremium(string item) => By.XPath($"(//breeze-tile[.//h4[contains(.,'{item}')]]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[1])[last()]");
        // Additional Interests
        private By _lblDesignatedInsured => By.XPath("//div[.=' Designated Insured ']/following-sibling::div/span");
        private By _lblAdditionalNamedInsured => By.XPath("//div[.=' Additional Insureds (Named) ']/following-sibling::div/span");
        private By _lblNamedInsuredWaiverOfSubrogation => By.XPath("//div[.=' Additional Insureds (Named) with Waiver of Subrogation ']/following-sibling::div/span");
        private By _lblWantsBlanketAdditionalInsured => By.XPath("//div[.=' Blanket Additional Insured ']/following-sibling::div/span");
        private By _lblBlanketWaiverOfSubrogation => By.XPath("//div[.=' Blanket Additional Insured with Waiver of Subrogation ']/following-sibling::div/span");
        private By _lblAdditionalInterestsPremium => By.XPath($"//breeze-tile[.//h4[.='{AdditionalCoveragesType.AdditionalInterests}']]//div[.='Total Premium']/following-sibling::div/span");
        // Hired Car/Non-Owned 
        private By _lblHiredCarNonOwnedLiabilityValue => By.XPath($"//div[.=' {AdditionalCoveragesType.HiredCarNonOwned} Liability ']/following-sibling::div/span[last()]");
        private By _lblHiredCarNonOwnedLiabilityDeductible => By.XPath($"//div[.=' {AdditionalCoveragesType.HiredCarNonOwned} Liability ']/following-sibling::div/span[last()-1]");
        private By _lblHiredCarPhysicalDamageValue => By.XPath("//div[.=' Hired Car Physical Damage ']/following-sibling::div/span[last()]");
        private By _lblHiredCarPhysicalDamageDeductible => By.XPath("//div[.=' Hired Car Physical Damage ']/following-sibling::div/span[last()-1]");
        private By _lblHiredCarNonOwnedPremium => By.XPath($"//breeze-tile[.//h4[.='{AdditionalCoveragesType.HiredCarNonOwned}']]//div[.='Total Premium']/following-sibling::div/span");
        // Cargo
        private By _lblCargoName => By.XPath($"//breeze-tile//h4[contains(.,'{AdditionalCoveragesType.Cargo}')]");
        private By _lblCargoCategory => By.XPath($"//breeze-tile[.//h4[contains(.,'{AdditionalCoveragesType.Cargo}')]]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//div[1]");
        private By _lblCargoPremium => By.XPath($"//breeze-tile[.//h4[contains(.,'{AdditionalCoveragesType.Cargo}')]]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()-2]");
        private By _lblCargoDeductible => By.XPath($"//breeze-tile[.//h4[contains(.,'{AdditionalCoveragesType.Cargo}')]]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()-1]");
        private By _lblCargoLimit => By.XPath($"//breeze-tile[.//h4[contains(.,'{AdditionalCoveragesType.Cargo}')]]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()]");
        // Trailer Interchange
        private By _lblTrailerInterchangePremium => By.XPath($"//breeze-tile[.//h4[.='{AdditionalCoveragesType.TrailerInterchange}']]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()-2]");
        private By _lblTrailerInterchangeDeductible => By.XPath($"//breeze-tile[.//h4[.='{AdditionalCoveragesType.TrailerInterchange}']]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()-1]");
        private By _lblTrailerInterchangeLimit => By.XPath($"//breeze-tile[.//h4[.='{AdditionalCoveragesType.TrailerInterchange}']]//div[contains(@class, 'row') and not(contains(@class, 'row-header'))]//span[last()]");
        #endregion

        #region Elements
        public IWebElement DeleteIcon(string item) => StableFindElement(_iconDelete(item));
        public IWebElement EditIcon(string item) => StableFindElement(_iconEdit(item));
        public IWebElement AddAdditionalCoveragesButton => StableFindElement(_btnAddAdditionalCoverages);
        public IWebElement CoverageSummaryButton => StableFindElement(_btnCoverageSummary);
        public ReadOnlyCollection<IWebElement> CoverageSectionLabels => StableFindElements(_lblCoverageSections);
        // Additional Interests
        public IWebElement DesignatedInsuredLabel => StableFindElement(_lblDesignatedInsured);
        public IWebElement AdditionalNamedInsuredLabel => StableFindElement(_lblAdditionalNamedInsured);
        public IWebElement NamedInsuredWaiverOfSubrogationLabel => StableFindElement(_lblNamedInsuredWaiverOfSubrogation);
        public IWebElement WantsBlanketAdditionalInsuredLabel => StableFindElement(_lblWantsBlanketAdditionalInsured);
        public IWebElement BlanketWaiverOfSubrogationLabel => StableFindElement(_lblBlanketWaiverOfSubrogation);
        public IWebElement AdditionalInterestsPremiumLabel => StableFindElement(_lblAdditionalInterestsPremium);
        // Hired Car/Non-Owned
        public IWebElement HiredCarCarNonOwnedLiabilityValueLabel => StableFindElement(_lblHiredCarNonOwnedLiabilityValue);
        public IWebElement HiredCarCarNonOwnedLiabilityDeductibleLabel => StableFindElement(_lblHiredCarNonOwnedLiabilityDeductible);
        public IWebElement HiredCarCarPhysicalDamageValueLabel => StableFindElement(_lblHiredCarPhysicalDamageValue);
        public IWebElement HiredCarCarPhysicalDamageDeductibleLabel => StableFindElement(_lblHiredCarPhysicalDamageDeductible);
        public IWebElement HiredCarNonOwnedPremiumLabel => StableFindElement(_lblHiredCarNonOwnedPremium);
        // Cargo
        public IWebElement CargoCategoryLabel => StableFindElement(_lblCargoCategory);
        public IWebElement CargoLimitLabel => StableFindElement(_lblCargoLimit);
        public IWebElement CargoDeductibleLabel => StableFindElement(_lblCargoDeductible);
        public IWebElement CargoPremiumLabel => StableFindElement(_lblCargoPremium);
        // Trailer Interchange
        public IWebElement TrailerInterchangeLimitLabel => StableFindElement(_lblTrailerInterchangeLimit);
        public IWebElement TrailerInterchangeDeductibleLabel => StableFindElement(_lblTrailerInterchangeDeductible);
        public IWebElement TrailerInterchangePremiumLabel => StableFindElement(_lblTrailerInterchangePremium);
        #endregion

        #region Business Methods
        public AdditionalCoveragesOverviewPage() : base()
        {
            Url = "forms/additional-coverages-overview";
            RequiredElementLocator = _btnCoverageSummary;
        }

        [ExtentStepNode]
        public AdditionalCoveragesPage SelectAddAdditionalCoveragesButton()
        {
            AddAdditionalCoveragesButton.Click();
            var page = new AdditionalCoveragesPage();
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
        public AdditionalInterestCountsPage SelectEditAdditionalInterestsIcon()
        {
            return SelectEditIcon<AdditionalInterestCountsPage>(AdditionalCoveragesType.AdditionalInterests);
        }

        [ExtentStepNode]
        public LiabilityQuestions1Page SelectEditHiredCarNonOwnedIcon()
        {
            return SelectEditIcon<LiabilityQuestions1Page>(AdditionalCoveragesType.HiredCarNonOwned);
        }

        [ExtentStepNode]
        public CargoLimitDeductiblePage SelectEditCargoIcon()
        {
            return SelectEditIcon<CargoLimitDeductiblePage>(AdditionalCoveragesType.Cargo);
        }

        [ExtentStepNode]
        public TrailerInterchangePage SelectEditTrailerInterchangeIcon()
        {
            return SelectEditIcon<TrailerInterchangePage>(AdditionalCoveragesType.TrailerInterchange);
        }

        [ExtentStepNode]
        public T SelectEditIcon<T>(string itemName) where T : BasePage
        {
            EditIcon(itemName).Click();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage SelectDeleteIcon(string itemName)
        {
            DeleteIcon(itemName).Click();
            WaitForElementVisible(_dlgConfirm);
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage DeleteAdditionalCoverage(string itemName)
        {
            SelectDeleteIcon(itemName);
            SelectOptionOnConfirmDialog<AdditionalCoveragesOverviewPage>(AnswerOption.Yes);
            WaitForElementNotExists(_lblCoverageSection(itemName), throwException: false);
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ConfirmDeleteAdditionalCoverage(string itemName)
        {
            SelectOptionOnConfirmDialog<AdditionalCoveragesOverviewPage>(AnswerOption.Yes);
            WaitForElementNotExists(_lblCoverageSection(itemName), throwException: false);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateAdditionalCoveragesOverviewPageDisplayed()
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
        public AdditionalCoveragesOverviewPage ValidateAddAdditionalCoveragesButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnAddAdditionalCoverages))
                {
                    SetPassValidation(node, ValidationMessage.ValidateAddAdditionalCoveragesButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateAddAdditionalCoveragesButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAddAdditionalCoveragesButtonDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateCoverageSummaryButtonDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnCoverageSummary))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageSummaryButtonDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateAdditionalInterestsCoverageDisplayedCorrectly(AdditionalInterests additionalInterestCounts)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(additionalInterestCounts, AdditionalCoveragesType.AdditionalInterests);

            List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();

            if (additionalInterestCounts.DesignatedInsuredCount != null)
            {
                comparisons.Add(new KeyValuePair<string, string[]>("Designated Insured", new string[] { additionalInterestCounts.DesignatedInsuredCount, GetText(_lblDesignatedInsured) }));
            }

            if (additionalInterestCounts.AdditionalNamedInsuredCount != null)
            {
                comparisons.Add(new KeyValuePair<string, string[]>("Additional Insureds (Named)", new string[] { additionalInterestCounts.AdditionalNamedInsuredCount, GetText(_lblAdditionalNamedInsured) }));
            }

            if (additionalInterestCounts.NamedInsuredWaiverOfSubrogationCount != null)
            {
                comparisons.Add(new KeyValuePair<string, string[]>("Additional Insureds (Named) with Waiver of Subrogation", new string[] { additionalInterestCounts.NamedInsuredWaiverOfSubrogationCount, GetText(_lblNamedInsuredWaiverOfSubrogation) }));
            }

            if (additionalInterestCounts.WantsBlanketAdditionalInsured != null)
            {
                comparisons.Add(new KeyValuePair<string, string[]>("Blanket Additional Insured", new string[] { additionalInterestCounts.WantsBlanketAdditionalInsured, GetText(_lblWantsBlanketAdditionalInsured) }));
            }

            if (additionalInterestCounts.BlanketWaiverOfSubrogationCount != null)
            {
                comparisons.Add(new KeyValuePair<string, string[]>("Blanket Additional Insured with Waiver of Subrogation", new string[] { additionalInterestCounts.BlanketWaiverOfSubrogationCount, GetText(_lblBlanketWaiverOfSubrogation) }));
            }

            try
            {
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.AdditionalInterests), item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.AdditionalInterests), item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.AdditionalInterests), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateHiredCarNonOwnedCoverageDisplayedCorrectly(HiredCarNonOwned hiredCarInformation)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(hiredCarInformation, AdditionalCoveragesType.HiredCarNonOwned);

            // Skip verifing Hired Car Libility because it's data was auto calculated
            if (hiredCarInformation.HiredCarPhysicalDamage.UseHiredCarPhysicalDamageCoverage == AnswerOption.Yes)
            {
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
                {
                    new KeyValuePair<string, string[]>("Hired Car Physical Damage Value", new string[]{ hiredCarInformation.HiredCarPhysicalDamage.MaximumValue, GetText(_lblHiredCarPhysicalDamageValue) }),
                    new KeyValuePair<string, string[]>("Hired Car Physical Damage Deductible", new string[]{ Utils.GetNumberStringFromCurrency(hiredCarInformation.HiredCarPhysicalDamage.Deductible), GetText(_lblHiredCarPhysicalDamageDeductible) })
                };

                try
                {
                    foreach (var item in comparisons)
                    {
                        if (item.Value[0] == item.Value[1])
                        {
                            SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.HiredCarNonOwned), item.Key, item.Value[0]);
                        }
                        else
                        {
                            SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.HiredCarNonOwned), item.Key, item.Value[0], item.Value[1]);
                        }
                    }
                }
                catch (Exception e)
                {
                    SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.HiredCarNonOwned), e);
                }
            }
            else
            {
                SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.HiredCarNonOwned));
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateCargoCoverageDisplayedCorrectly(Cargo cargoInformation)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(cargoInformation, AdditionalCoveragesType.Cargo);

            string cargoName = GetText(_lblCargoName);
            List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
            {
                new KeyValuePair<string, string[]>($"{cargoName} Category", new string[]{ cargoInformation.CargoCategory, GetText(_lblCargoCategory) }),
                new KeyValuePair<string, string[]>($"{cargoName} Limit", new string[]{ cargoInformation.CargoLimitDeductible.CargoLimit, GetText(_lblCargoLimit) }),
                new KeyValuePair<string, string[]>($"{cargoName} Deductible", new string[]{ cargoInformation.CargoLimitDeductible.Deductible, GetText(_lblCargoDeductible) })
            };

            try
            {
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.Cargo), item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.Cargo), item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.Cargo), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateTrailerInterchangeCoverageDisplayedCorrectly(TrailerInterchange trailerInterchange)
        {
            var node = GetLastNode();
            ParameterValidator.ValidateNotNull(trailerInterchange, AdditionalCoveragesType.TrailerInterchange);

            List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>()
            {
                new KeyValuePair<string, string[]>($"{AdditionalCoveragesType.TrailerInterchange} Litmit", new string[]{trailerInterchange.Limit, GetText(_lblTrailerInterchangeLimit) }),
                new KeyValuePair<string, string[]>($"{AdditionalCoveragesType.TrailerInterchange} Deductible", new string[]{ Utils.GetNumberStringFromCurrency(trailerInterchange.Deductible), GetText(_lblTrailerInterchangeDeductible) })
            };

            try
            {
                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.TrailerInterchange), item.Key, item.Value[0]);
                    }
                    else
                    {
                        SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.TrailerInterchange), item.Key, item.Value[0], item.Value[1]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayedCorrectly, AdditionalCoveragesType.TrailerInterchange), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateAdditionalCoverageDisplayed(string itemName)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblCoverageSection(itemName)))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayed, itemName));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayed, itemName));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageDisplayed, itemName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateAdditionalCoverageNotDisplayed(string itemName)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lblCoverageSection(itemName)))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageNotDisplayed, itemName));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageNotDisplayed, itemName));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoverageNotDisplayed, itemName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateEditButtonDisplayed(string itemName)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_iconEdit(itemName)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateEditButtonDisplayed, itemName);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateEditButtonDisplayed, itemName);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEditButtonDisplayed, e, itemName);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateDeleteButtonDisplayed(string itemName)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_iconDelete(itemName)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateDeleteButtonDisplayed, itemName);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDeleteButtonDisplayed, itemName);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDeleteButtonDisplayed, e, itemName);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidateAdditionalCoveragePremiumDisplayed(string itemName)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(ControlLocator(WebControl.Label, itemName + "Premium")))
                {
                    SetPassValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoveragePremiumDisplayed, itemName));
                }
                else
                {
                    SetFailValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoveragePremiumDisplayed, itemName));
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, string.Format(ValidationMessage.ValidateAdditionalCoveragePremiumDisplayed, itemName), e);
            }

            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesOverviewPage ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen()
        {
            var node = GetLastNode();
            try
            {
                List<KeyValuePair<string, string[]>> comparisons = new List<KeyValuePair<string, string[]>>();
                foreach (var coverage in CoverageSectionLabels)
                {
                    string coverageName = coverage.Text.Trim();
                    string navCoverageName = AdditionalCoveragesType.GetShortName(coverageName);
                    comparisons.Add(new KeyValuePair<string, string[]>(coverageName + " Premium", new string[] { GetTextFromPageSource(_lblNavPremium(navCoverageName)), GetText(_lblTotalPremium(coverageName)) }));
                }

                foreach (var item in comparisons)
                {
                    if (item.Value[0] == item.Value[1])
                    {
                        SetPassValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen, item.Key, item.Value[1]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen, item.Key, item.Value[1], item.Value[0]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen, e);
            }

            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Additional Coverages Overview page is displayed";
            public static string ValidateAdditionalCoverageDisplayed = "Validate {0} coverage is displayed";
            public static string ValidateAdditionalCoverageNotDisplayed = "Validate {0} coverage is not displayed";
            public static string ValidateAdditionalCoverageDisplayedCorrectly = "Validate {0} coverage is displayed correctly";
            public static string ValidateAdditionalCoveragePremiumDisplayed = "Validate {0} premium is displayed";
            public static string ValidateCoverageSummaryButtonDisplayed = "Validate Coverage Summary button is displayed";
            public static string ValidateAddAdditionalCoveragesButtonDisplayed = "Validate Add Additional Coverages button is displayed";
            public static string ValidateEditButtonDisplayed = "Validate Edit button is displayed";
            public static string ValidateDeleteButtonDisplayed = "Validate Delete button is displayed";
            public static string ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen = "Validate premium in left navigation matches additional coverages overview screen";
        }
        #endregion
    }
}
