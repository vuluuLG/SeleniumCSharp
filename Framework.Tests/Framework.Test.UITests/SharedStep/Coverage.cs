using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using Framework.Test.UI.Pages.Global;
using System;

namespace Framework.Test.UITests.SharedStep
{
    public static class Coverage
    {
        public static AdditionalCoveragesPage UseDefaultCoverages(this CoveragesPage coveragesPage)
        {
            ParameterValidator.ValidateNotNull(coveragesPage, "CoveragesPage");

            // Lave coverages as default and select Next
            AdditionalCoveragesPage additionalCoveragesPage = coveragesPage.SelectNextButton();
            // Verification
            additionalCoveragesPage.ValidateAdditionalCoveragesPageDisplayed();
            return additionalCoveragesPage;
        }

        public static T SelectAdditionalCoverages<T>(this AdditionalCoveragesPage additionalCoveragesPage, AdditionalCoverages additionalCoverages = null, bool edit = false) where T : BasePage
        {
            ParameterValidator.ValidateNotNull(additionalCoveragesPage, "AdditionalCoveragesPage");
            if (additionalCoverages == null)
            {
                if (edit)
                {
                    AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = additionalCoveragesPage.SelectNoAdditionalCoverages<AdditionalCoveragesOverviewPage>();

                    // Verification
                    additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed();
                }
                else
                {
                    SummaryPage summaryPage = additionalCoveragesPage.SelectNoAdditionalCoverages<SummaryPage>();
                    // Verification
                    summaryPage.ValidateSummaryPageDisplayed();
                }
            }
            else
            {
                if (additionalCoverages.AdditionalInterests != null)
                {
                    additionalCoveragesPage.SelectAdditionalCoverages(AdditionalCoveragesType.AdditionalInterests);
                }
                // else
                // {
                //     additionalCoveragesPage.DeselectAdditionalCoverages(AdditionalCoveragesType.AdditionalInterests);
                // }
                if (additionalCoverages.HiredCarNonOwned != null)
                {
                    additionalCoveragesPage.SelectAdditionalCoverages(AdditionalCoveragesType.HiredCarNonOwned);
                }
                else
                // {
                //     additionalCoveragesPage.DeselectAdditionalCoverages(AdditionalCoveragesType.HiredCarNonOwned);
                // }

                if (additionalCoverages.Cargo != null)
                {
                    additionalCoveragesPage.SelectAdditionalCoverages(AdditionalCoveragesType.Cargo);
                }
                // {
                //     additionalCoveragesPage.DeselectAdditionalCoverages(AdditionalCoveragesType.Cargo);
                // }

                if (additionalCoverages.TrailerInterchange != null)
                {
                    additionalCoveragesPage.SelectAdditionalCoverages(AdditionalCoveragesType.TrailerInterchange);
                }
                // {
                //     additionalCoveragesPage.DeselectAdditionalCoverages(AdditionalCoveragesType.TrailerInterchange);
                // }

                // Select NextButton
                //User navigates to the first entry screen of the first selected additional coverage (by oder)
                if (additionalCoverages.AdditionalInterests != null)
                {
                    // Select NextButton
                    AdditionalInterestCountsPage additionalInterestCountsPage = additionalCoveragesPage.SelectNextButton<AdditionalInterestCountsPage>();
                    //verification
                    additionalInterestCountsPage.ValidateAdditionalInterestCountsPageDisplayed();
                }
                else if (additionalCoverages.HiredCarNonOwned != null)
                {
                    // Select next button
                    LiabilityQuestions1Page liabilityQuestions1Page = additionalCoveragesPage.SelectNextButton<LiabilityQuestions1Page>();
                    // verification
                    liabilityQuestions1Page.ValidateLiabilityQuestions1PageDisplayed();
                }
                else if (additionalCoverages.Cargo != null)
                {
                    // Select Next Button
                    CargoLimitDeductiblePage cargoLimitDeductiblePage = additionalCoveragesPage.SelectNextButton<CargoLimitDeductiblePage>();
                    // Verification
                    cargoLimitDeductiblePage.ValidateCargoLimitDeductiblePageDisplayed();
                }
                else if (additionalCoverages.TrailerInterchange != null)
                {
                    // Select Next Button
                    TrailerInterchangePage trailerInterchangePage = additionalCoveragesPage.SelectNextButton<TrailerInterchangePage>();
                    // Verification
                    trailerInterchangePage.ValidateTrailerInterchangePageDisplayed();
                }
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static AdditionalCoveragesPage AddAdditionalCoverageFromCoverageScreenWithNoPriorAdditionalCoverage(this LandingPage landingPage, string[] coverageSections, string[] additionalCoveragesList)
        {
            ParameterValidator.ValidateNotNull(landingPage, "Landing Page");

            //User is navigated to Coverages screen by clicking on Coverages from left nav
            CoveragesPage coveragesPage = landingPage.SelectMenuLink<CoveragesPage>(LeftNavigationHeader.Coverage);

            // Verification
            coveragesPage.ValidateCoveragesPageDisplayed()
                         .ValidateCoverageSectionsDisplayed(coverageSections)
                         .ValidateAdditionalCoveragesSectionDisplayed()
                         .ValidateAdditionalCoveragesEditButtonDisplayed();

            // Select Additional Coverages edit button on the Coverages screen
            AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = coveragesPage.SelectEditAdditionalCoveragesIcon();
            // verification
            additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed()
                                           .ValidateAddAdditionalCoveragesButtonDisplayed()
                                           .ValidateCoverageSummaryButtonDisplayed();
            // Click add Additional Coverage button
            AdditionalCoveragesPage additionalCoveragesPage = additionalCoveragesOverviewPage.SelectAddAdditionalCoveragesButton();
            //Verification
            additionalCoveragesPage.ValidateAdditionalCoveragesPageDisplayed()
                                   .ValidateAdditionalCoveragesListed(additionalCoveragesList);
            return additionalCoveragesPage;
        }

        public static T EnterAdditionalInterestCounts<T>(this AdditionalInterestCountsPage additionalInterestCountsPage, AdditionalCoverages additionalCoverages, bool edit = false)
        {
            ParameterValidator.ValidateNotNull(additionalInterestCountsPage, "AdditionalInterestCountsPage");
            ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
            ParameterValidator.ValidateNotNull(additionalCoverages.AdditionalInterests, "Additional Interest Count");

            // Enter Additional Interest count
            additionalInterestCountsPage.InputAdditionalInterestCounts(additionalCoverages.AdditionalInterests);

            if (additionalCoverages.HiredCarNonOwned != null)
            {
                // Select next button
                LiabilityQuestions1Page liabilityQuestions1Page = additionalInterestCountsPage.SelectNextButton<LiabilityQuestions1Page>();
                //Verification
                liabilityQuestions1Page.ValidateLiabilityQuestions1PageDisplayed();
            }
            else if (additionalCoverages.Cargo != null)
            {
                // Select Next Button
                CargoLimitDeductiblePage cargoLimitDeductiblePage = additionalInterestCountsPage.SelectNextButton<CargoLimitDeductiblePage>();
                // Verification
                cargoLimitDeductiblePage.ValidateCargoLimitDeductiblePageDisplayed();
            }
            else if (additionalCoverages.TrailerInterchange != null)
            {
                // Select Next Button
                TrailerInterchangePage trailerInterchange = additionalInterestCountsPage.SelectNextButton<TrailerInterchangePage>();
                // Verification
                trailerInterchange.ValidateTrailerInterchangePageDisplayed();
            }
            else if (edit)
            {
                // Select next button
                AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = additionalInterestCountsPage.SelectNextButton<AdditionalCoveragesOverviewPage>();
                //Verification
                additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed();
            }
            else
            {
                //Select Next button
                SummaryPage summaryPage = additionalInterestCountsPage.SelectNextButton<SummaryPage>();
                //Verification
                summaryPage.ValidateSummaryPageDisplayed();

            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T EnterHiredCarInformation<T>(this LiabilityQuestions1Page liabilityQuestions1Page, AdditionalCoverages additionalCoverages, bool edit = false)
        {
            ParameterValidator.ValidateNotNull(liabilityQuestions1Page, "LiabilityQuestions1Page");
            ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
            ParameterValidator.ValidateNotNull(additionalCoverages.HiredCarNonOwned, "Hired Car Information");
            LiabilityQuestions3Page liabilityQuestions3Page = new LiabilityQuestions3Page();
            //1. Enter Liability Question 1 page
            liabilityQuestions1Page.EnterLiabilityQuestion1(additionalCoverages.HiredCarNonOwned.LiabilityQuestion1);

            if (additionalCoverages.HiredCarNonOwned.LiabilityQuestion1.PlanToUseUnscheduledVehicleNextYear == AnswerOption.Yes)
            {
                // Select next button
                LiabilityQuestions2Page liabilityQuestions2Page = liabilityQuestions1Page.SelectNextButton<LiabilityQuestions2Page>();
                // Verification
                liabilityQuestions2Page.ValidateLiabilityQuestions2PageDisplayed();

                //2. Enter Liability Question 2 page and select Next
                liabilityQuestions2Page.EnterLiabilityQuestion2(additionalCoverages.HiredCarNonOwned.LiabilityQuestion2).SelectNextButton();
            }
            else
            {
                // Select next button
                liabilityQuestions1Page.SelectNextButton<LiabilityQuestions3Page>();
            }
            // Verification
            liabilityQuestions3Page.ValidateLiabilityQuestions3PageDisplayed();

            //3. enter Liability Questions 3 page and select next
            HiredCarPhysicalDamagePage hiredCarPhysicalDamagePage = liabilityQuestions3Page.EnterLiabilityQuestion3(additionalCoverages.HiredCarNonOwned.LiabilityQuestion3).SelectNextButton();

            //verification
            hiredCarPhysicalDamagePage.ValidateHiredCarPhysicalDamagePageDisplayed();

            //4. Enter hired car physical damage page
            hiredCarPhysicalDamagePage.EnterHiredCarPhysDamage(additionalCoverages.HiredCarNonOwned.HiredCarPhysicalDamage);
            if (additionalCoverages != null)
            {
                // Select next button
                CargoLimitDeductiblePage cargoLimitDeductiblePage = hiredCarPhysicalDamagePage.SelectNextButton<CargoLimitDeductiblePage>();
                //Verification
                cargoLimitDeductiblePage.ValidateCargoLimitDeductiblePageDisplayed();
            }
            else if (additionalCoverages.TrailerInterchange != null)
            {
                //Select next button
                TrailerInterchangePage trailerInterchangePage = hiredCarPhysicalDamagePage.SelectNextButton<TrailerInterchangePage>();
                //Verification
                trailerInterchangePage.ValidateTrailerInterchangePageDisplayed();
            }
            else if (edit)
            {
                // Select Next button
                SummaryPage summaryPage = hiredCarPhysicalDamagePage.SelectNextButton<SummaryPage>();
                //Verification
                summaryPage.ValidateSummaryPageDisplayed();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T EnterCargoInformation<T>(this CargoLimitDeductiblePage cargoLimitDeductiblePage, AdditionalCoverages additionalCoverages, bool edit = false)
        {
            ParameterValidator.ValidateNotNull(cargoLimitDeductiblePage, "CargoLimitDeductiblePage");
            ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
            ParameterValidator.ValidateNotNull(additionalCoverages.Cargo, "Cargo Information");

            //1. Enter the following cargo limit and deductible information and proceed
            CargoCategoryPage cargoCategoryPage = cargoLimitDeductiblePage.SelectCargoLimitAndDeductible(additionalCoverages.Cargo.CargoLimitDeductible).SelectNextButton();

            //Validation
            cargoCategoryPage.ValidateCargoCategoryPageDisplayed();

            //2. Select the following and proceed @CargoCategory
            CargoCommodityPage cargoCommodityPage = cargoCategoryPage.SelectCargoCategory(additionalCoverages.Cargo.CargoCategory).SelectNextButton();
            //Verification
            cargoCommodityPage.ValidateCargoCommodityPageDisplayed();

            //3. Select the following and proceed @CargoCommodity
            CargoModifiersPage cargoModifiersPage = cargoCommodityPage.SelectCargoCommodity(additionalCoverages.Cargo.CargoCommodity).SelectNextButton();
            // Verification
            cargoModifiersPage.ValidateCargoModifiersPageDisplayed();

            //4. Select the following for cargo modifiers and proceed
            cargoModifiersPage.SelectCargoModifiers(additionalCoverages.Cargo.CargoModifiers);

            if (additionalCoverages.TrailerInterchange != null)
            {
                // Select next button
                TrailerInterchangePage trailerInterchange = cargoModifiersPage.SelectNextButton<TrailerInterchangePage>();
                // Verification
                trailerInterchange.ValidateTrailerInterchangePageDisplayed();
            }
            else if (edit)
            {
                // Select next button
                AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = cargoModifiersPage.SelectNextButton<AdditionalCoveragesOverviewPage>();
                // Verification
                additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed();
            }
            else
            {
                // Select next button
                SummaryPage summaryPage = cargoModifiersPage.SelectNextButton<SummaryPage>();
                //Verification
                summaryPage.ValidateSummaryPageDisplayed();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T EnterTrailerInterchange<T>(this TrailerInterchangePage trailerInterchangePage, AdditionalCoverages additionalCoverages, bool edit = false)
        {
            ParameterValidator.ValidateNotNull(trailerInterchangePage, "TrailerInterchangePage");
            ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
            ParameterValidator.ValidateNotNull(additionalCoverages.TrailerInterchange, "Trailer Interchange");

            // Enter Trailer interchange
            trailerInterchangePage.InputTrailerInterchange(additionalCoverages.TrailerInterchange);
            if (edit)
            {
                // Select next button
                AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = trailerInterchangePage.SelectNextButton<AdditionalCoveragesOverviewPage>();
                // Verification
                additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed();
            }
            else
            {
                // Select Next button
                SummaryPage summaryPage = trailerInterchangePage.SelectNextButton();
                // Verification
                summaryPage.ValidateSummaryPageDisplayed();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static SummaryPage EnterAdditionalCoverages(this BasePage page, AdditionalCoverages additionalCoverages)
        {
            ParameterValidator.ValidateNotNull(page, "Page");
            if (additionalCoverages != null)
            {
                if (additionalCoverages.AdditionalInterests != null)
                {
                    AdditionalInterestCountsPage additionalInterestCountsPage = new AdditionalInterestCountsPage();
                    additionalInterestCountsPage.EnterAdditionalInterestCounts<AdditionalInterestCountsPage>(additionalCoverages);
                }
                if (additionalCoverages.HiredCarNonOwned != null)
                {
                    LiabilityQuestions1Page liabilityQuestions1Page = new LiabilityQuestions1Page();
                    liabilityQuestions1Page.EnterHiredCarInformation<LiabilityQuestions1Page>(additionalCoverages);
                }
                if (additionalCoverages.Cargo != null)
                {
                    CargoLimitDeductiblePage cargoLimitDeductiblePage = new CargoLimitDeductiblePage();
                    cargoLimitDeductiblePage.EnterCargoInformation<CargoLimitDeductiblePage>(additionalCoverages);
                }
                if (additionalCoverages.TrailerInterchange != null)
                {
                    TrailerInterchangePage trailerInterchangePage = new TrailerInterchangePage();
                    trailerInterchangePage.EnterTrailerInterchange<TrailerInterchangePage>(additionalCoverages);
                }
            }
            return new SummaryPage();
        }
    }
}