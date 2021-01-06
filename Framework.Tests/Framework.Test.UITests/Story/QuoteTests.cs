using Framework.Test.Common.Helper;
using Framework.Test.UI.Pages;
using Framework.Test.UITests.DataSource;
using Framework.Test.UITests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Framework.Test.Common.Helper.EnvironmentHelper;
using static Framework.Test.UITests.SharedStep.Common;
using static Framework.Test.UITests.SharedStep.Business;
using static Framework.Test.UITests.SharedStep.Customer;
using static Framework.Test.UITests.SharedStep.Vehicle;
using static Framework.Test.UITests.SharedStep.Driver;
using static Framework.Test.UITests.SharedStep.Coverage;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;

namespace Framework.Test.UITests.Story
{
    public class QuoteTests
    {
        [BreezeTestMethod]
        [TestValidation(Description = "Edit Quote - Edit Coverages, Add Additional Coverages, Edit Additional Coverages")]
        [UIDataSource(DataClassName = nameof(EditQuoteEditCoveragesTestData))]
        [TestCategory("Integration")]
        public void UI_EditQuote_Edit_Coverages_Add_Additional_Coverages_Edit_Additional_Coverages(int number, EditQuoteEditCoveragesInformation testData)
        {
            ParameterValidator.ValidateNotNull(testData, "Test Item");

            //1. Navigate to Home page.
            LoginPage loginPage = OpenBreezeApplication();

            //2. Login to Welcome Page
            WelcomePage welcomePage = loginPage.Login();

            //3. Start New Submision
            BusinessClassificationPage businessClassificationPage = welcomePage.StartNewSubmission();

            //4. Get quoteID to be used in subsequent steps
            string quoteId = businessClassificationPage.GetQuoteId();

            //5. Enter business information
            EffectiveDatePage effectiveDatePage = businessClassificationPage.EnterBusinessInformation(testData.BusinessClassification);

            //6. Enter customer information
            VehicleInfoPage vehicleInfoPage = effectiveDatePage.EnterCustomerInformation(testData.CustomerInformation);

            //7. Enter policy level vehicle info
            VehicleEntryPage vehicleEntryPage = vehicleInfoPage.EnterPolicyLevelVehicleInfo<VehicleEntryPage>(testData.PolicyLevelVehicleInformation);

            //8. Add first vehicle
            AddAnotherVehiclePage addAnotherVehiclePage = vehicleEntryPage.AddVehicle(testData.VehicleInfo1);

            //9. No more vehicles
            DriverEnterPage driverEnterPage = addAnotherVehiclePage.NoMoreVehicle();

            //10. Add first driver (PO) - invalid MVR
            AddAnotherDriverPage addAnotherDriverPage = driverEnterPage.AddDriver(testData.DriverInfo1);

            //11. No more drivers
            CoveragesPage coveragesPage = addAnotherDriverPage.NoMoreDriver();

            //12. Use default coverages
            AdditionalCoveragesPage additionalCoveragesPage = coveragesPage.UseDefaultCoverages();

            //13. Select Additional Coverages
            SummaryPage summaryPage = additionalCoveragesPage.SelectAdditionalCoverages<SummaryPage>(testData.DefaultAdditionalCoverages);

            //14. Select Close
            summaryPage.SelectCloseButton();
            // Verification
            welcomePage.ValidateWelcomePageDisplayed();

            //15. Open quote using the quoteID captured in step 4
            welcomePage.ResumeQuote<SummaryPage>(EnvironmentSetting.WebUrl, quoteId);
            // Verification
            summaryPage.ValidateSummaryPageDisplayed();

            //16. Select coverages in the left navigation
            summaryPage.SelectMenuLink<CoveragesPage>(LeftNavigationHeader.Coverage);
            // Verification
            coveragesPage.ValidateCoveragesPageDisplayed()
                         .ValidateAdditionalCoveragesSectionDisplayed()
                         .ValidateAdditionalCoveragesEditButtonDisplayed();

            //17. Change coverages
            coveragesPage.SelectCoverageLimits(testData.CoverageLimits);
            // Verification
            coveragesPage.ValidatePremiumsNoLongerShown();

            //18. Select Calculate button
            coveragesPage.SelectCalculateButton();
            // Verification
            coveragesPage.ValidateTotalPremiumRecalculatedAndDisplayed();

            //19. Add Additional coverage from Coverage screen with no prior additional coverage
            coveragesPage.AddAdditionalCoverageFromCoverageScreenWithNoPriorAdditionalCoverage(testData.DefaultCoverageSections, testData.DefaultAdditionalCoveragesList);

            //20. Select Additional Interests and Cargo, Select Next
            AdditionalInterestCountsPage additionalInterestCountsPage = additionalCoveragesPage.SelectAdditionalCoverages<AdditionalInterestCountsPage>(testData.AdditionalCoverages, true);

            //21. Enter @AddInterests, Select Next
            CargoLimitDeductiblePage cargoLimitDeductiblePage = additionalInterestCountsPage.EnterAdditionalInterestCounts<CargoLimitDeductiblePage>(testData.AdditionalCoverages, true);

            //22. Make @CargoInfo selections
            AdditionalCoveragesOverviewPage additionalCoveragesOverviewPage = cargoLimitDeductiblePage.EnterCargoInformation<AdditionalCoveragesOverviewPage>(testData.AdditionalCoverages, true);
            // Verification
            additionalCoveragesOverviewPage.ValidateAdditionalCoverageDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateAdditionalCoverageDisplayed(AdditionalCoveragesType.Cargo)
                                           .ValidateAdditionalInterestsCoverageDisplayedCorrectly(testData.AdditionalCoverages.AdditionalInterests)
                                           .ValidateCargoCoverageDisplayedCorrectly(testData.AdditionalCoverages.Cargo)
                                           .ValidateEditButtonDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateDeleteButtonDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateEditButtonDisplayed(AdditionalCoveragesType.Cargo)
                                           .ValidateDeleteButtonDisplayed(AdditionalCoveragesType.Cargo)
                                           .ValidateAdditionalCoveragePremiumDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateAdditionalCoveragePremiumDisplayed(AdditionalCoveragesType.Cargo);

            //23. Select Coverages from the left navigation
            additionalCoveragesOverviewPage.SelectMenuLink<CoveragesPage>(LeftNavigationHeader.Coverage);
            // Verification
            coveragesPage.ValidateCoveragesPageDisplayed()
                         .ValidatePreviousCoveragesListed(testData.CoverageLimits)
                         .ValidatePremiumInLeftNavigationMatchesCoveragesScreen();

            //24. Select Edit button of Additional Coverages section
            coveragesPage.SelectEditAdditionalCoveragesIcon();
            // Verification
            additionalCoveragesOverviewPage.ValidateAdditionalCoveragesOverviewPageDisplayed()
                                           .ValidateAdditionalCoverageDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateAdditionalCoverageDisplayed(AdditionalCoveragesType.Cargo);

            //25. Click Delete button for Cargo
            additionalCoveragesOverviewPage.SelectDeleteIcon(AdditionalCoveragesType.Cargo);
            // Verification
            additionalCoveragesOverviewPage.ValidateDialogContentDisplayedCorrectly(testData.DialogContent);

            //26. Select yes
            additionalCoveragesOverviewPage.ConfirmDeleteAdditionalCoverage(AdditionalCoveragesType.Cargo);
            // Verification
            additionalCoveragesOverviewPage.ValidateAdditionalCoverageDisplayed(AdditionalCoveragesType.AdditionalInterests)
                                           .ValidateAdditionalCoverageNotDisplayed(AdditionalCoveragesType.Cargo)
                                           .ValidateAddAdditionalCoveragesButtonDisplayed()
                                           .ValidateCoverageSummaryButtonDisplayed();

            //27. Select Add Additional Coverages button
            additionalCoveragesOverviewPage.SelectAddAdditionalCoveragesButton();
            // Verification
            additionalCoveragesPage.ValidateAdditionalCoveragesPageDisplayed()
                                   .ValidateAdditionalCoveragesListed(testData.ExpectedAdditionalCoverages)
                                   .ValidateAdditionalCoveragesNotListed(testData.UnexpectedAdditionalCoverages);

            //28.1 Select Trailer Interchange and click Next
            TrailerInterchangePage trailerInterchangePage = additionalCoveragesPage.SelectAdditionalCoverages<TrailerInterchangePage>(testData.AdditionalCoveragesChange);

            //28.2 Finish Trailer Interchange coverage info
            trailerInterchangePage.EnterTrailerInterchange<AdditionalCoveragesOverviewPage>(testData.AdditionalCoveragesChange, true);

            //29. Inspect page contents
            additionalCoveragesOverviewPage.ValidatePremiumInLeftNavigationMatchesAdditionalCoveragesOverviewScreen();
        }

        [BreezeTestMethod]
        [TestValidation(Description = "Automate Create Quote Happy Path IN - UM Deductible")]
        [TestCategory("Integration")]
        public void UI_CreateQuote_Happy_Path_IN()
        {
            //Given
            //Prepare test data
            CreateQuoteHappyPathINTestData quoteTestData = new CreateQuoteHappyPathINTestData();

            //1. Navigate to Home page.
            LoginPage loginPage = OpenBreezeApplication();

            //2. Login to Welcome Page
            WelcomePage welcomePage = loginPage.Login();

            //3. Start New Submision
            BusinessClassificationPage businessClassificationPage = welcomePage.StartNewSubmission();

            //4. Enter business information
            EffectiveDatePage effectiveDatePage = businessClassificationPage.EnterBusinessInformation(quoteTestData.BusinessClassification);

            //5. Enter customer information
            VehicleInfoPage vehicleInfoPage = effectiveDatePage.EnterCustomerInformation(quoteTestData.CustomerInformation);

            //6. Enter policy level vehicle info
            VehicleEntryPage vehicleEntryPage = vehicleInfoPage.EnterPolicyLevelVehicleInfo<VehicleEntryPage>(quoteTestData.PolicyLevelVehicleInformation);

            //7. Add first vehicle
            AddAnotherVehiclePage addAnotherVehiclePage = vehicleEntryPage.AddVehicle(quoteTestData.VehicleInfo);

            //8. No more vehicle
            DriverEnterPage driverEnterPage = addAnotherVehiclePage.NoMoreVehicle();

            //9. Add first driver (PO) - Invalid MVR
            AddAnotherDriverPage addAnotherDriverPage = driverEnterPage.AddDriver<AddAnotherDriverPage>(quoteTestData.DriverInfo);

            //10. No more driver
            CoveragesPage coveragesPage = addAnotherDriverPage.NoMoreDriver();
            // Verification
            coveragesPage.ValidateControlDisplayed(CoverageLimitsType.UMDeductible, WebControl.Dropdown)
                         .ValidateControlDisplayedCorrectly(CoverageLimitsType.UMLimitType, WebControl.Dropdown, quoteTestData.DefaultUMLimitType);

            //11. Select None for UM deductible then click Calculate
            coveragesPage.SelectCoverageLimit(CoverageLimitsType.UMDeductible, quoteTestData.CoverageLimits1.UMDeductible)
                         .SelectCalculateButton();
            // Verification
            coveragesPage.ValidateTotalPremiumRecalculatedAndDisplayed();

            //12. Select UM Deductible $300 then click Calculate
            coveragesPage.SelectCoverageLimit(CoverageLimitsType.UMDeductible, quoteTestData.CoverageLimits2.UMDeductible)
                         .SelectCalculateButton();
            // Verification
            coveragesPage.ValidateTotalPremiumRecalculatedAndDisplayed();

            //13. Select UM CSL BI Only
            coveragesPage.SelectCoverageLimit(CoverageLimitsType.UMLimitType, quoteTestData.CoverageLimits1.UMLimitType);
            // Verification
            coveragesPage.ValidateControlNotDisplayed(CoverageLimitsType.UMDeductible, WebControl.Dropdown);

            //14. Select Caculate button
            coveragesPage.SelectCalculateButton();
            // Verification
            coveragesPage.ValidateTotalPremiumRecalculatedAndDisplayed();

            //15. Select UM Split BIPD
            coveragesPage.SelectCoverageLimit(CoverageLimitsType.UMLimitType, quoteTestData.CoverageLimits2.UMLimitType);
            // Verification
            coveragesPage.ValidateControlDisplayed(CoverageLimitsType.UMDeductible, WebControl.Dropdown);

            //16. Select UM Limit, Deductible
            coveragesPage.SelectCoverageLimits(quoteTestData.CoverageLimits2);
            // Verification
            coveragesPage.ValidateCalculateButtonDisplayed();
        }

        [BreezeTestMethod]
        [TestValidation(Description = "Automate Edit Quote Submitted Quotes cannot be edited")]
        [TestCategory("Integration")]
        public void UI_EditQuote_Submitted_Quotes_Cannot_Be_Edited()
        {
            //Given
            //Prepare test data
            EditQuoteSubmittedQuotesCannotBeEditedTestData testData = new EditQuoteSubmittedQuotesCannotBeEditedTestData();

            //1. Navigate to Home page.
            LoginPage loginPage = OpenBreezeApplication();

            //2. Login to Welcome Page
            WelcomePage welcomePage = loginPage.Login();

            //3. Create Quote - Single Vehicle/Single Driver No Additional Coverages
            SummaryPage summaryPage = welcomePage.CreateQuote(testData.Quote);
            string quoteId = summaryPage.GetQuoteId();

            //4. Submit Quote
            IndicationPage indicationPage = summaryPage.SelectSubmitButton();
            // Verification
            indicationPage.ValidateIndicationPageDisplayed()
                          .ValidateCongratulationsMessageDisplayed();

            //5. Select Close
            indicationPage.SelectCloseButton();
            // Verification
            welcomePage.ValidateWelcomePageDisplayed();

            //6. Open the quote that was just submitted.
            welcomePage.ResumeQuote<SummaryPage>(EnvironmentSetting.WebUrl, quoteId);
            // Verification
            summaryPage.ValidateSummaryPageDisplayed()
                       .ValidateSubmittedButtonDisplayed()
                       .ValidatePrintIndicationButtonEnabled();

            //7. Select Menu Headers
            foreach (var item in testData.MenuHeaderTitles)
            {
                summaryPage.SelectMenuTitle<SummaryPage>(item);
                // Verification
                summaryPage.ValidateMenuHeaderLinkNotDisplayed(item);
            }

            //8. Select Close
            summaryPage.SelectCloseButton();
            // Verification
            welcomePage.ValidateWelcomePageDisplayed();
        }
    }
}
