using Framework.Test.Common.Helper;
using Framework.Test.UI;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using Framework.Test.UITests.TestData;

namespace Framework.Test.UITests.SharedStep
{
    public static class Common
    {
        public static WelcomePage Login(this LoginPage loginPage, Account account = null)
        {
            ParameterValidator.ValidateNotNull(loginPage, "LoginPage");
            if (account == null)
            {
                account = SharedTestData.DefaultAccount;
            }
            WelcomePage wellcomePage = loginPage.Login(account);
            return wellcomePage;
        }

        [ExtentStepNode]
        public static LoginPage OpenBreezeApplication(string browserName = null, string baseUrl = null)
        {
            browserName = browserName ?? EnvironmentHelper.EnvironmentSetting.BrowserName;
            baseUrl = baseUrl ?? EnvironmentHelper.EnvironmentSetting.WebUrl;
            // Open Breeze app
            Browser.Open(browserName, baseUrl);
            LoginPage loginPage = new LoginPage();
            // Wait for login page dislayed
            loginPage.WaitForPageLoad();

            // Verification
            loginPage.ValidateLoginPageDisplayed();
            return loginPage;
        }

        public static BusinessClassificationPage StartNewSubmission(this WelcomePage wellcomePage, string agency = null)
        {
            ParameterValidator.ValidateNotNull(wellcomePage, "WellcomePage");

            // Select start new Breeze submission button and select an agency (if needed)
            BusinessClassificationPage businessClassificationPage;
            if (agency != null)
            {
                businessClassificationPage = wellcomePage.SelectStartNewBreezeSubmissionButton<WelcomePage>().SelectAgency(agency);
            }
            else
            {
                businessClassificationPage = wellcomePage.SelectStartNewBreezeSubmissionButton<BusinessClassificationPage>();
            }

            // Verification
            businessClassificationPage.ValidateSubmissionNumberDisplayed();
            businessClassificationPage.ValidateBusinessClassificationPageDisplayed();
            return businessClassificationPage;
        }

        public static SummaryPage CreateQuote(this WelcomePage wellcomePage, Quote quote)
        {
            ParameterValidator.ValidateNotNull(wellcomePage, "WellcomePage");
            ParameterValidator.ValidateNotNull(quote, "Quote");

            //1. Start new submission
            BusinessClassificationPage businessClassificationPage = wellcomePage.StartNewSubmission();
            //2. Enter business infomation
            EffectiveDatePage effectiveDatePage = businessClassificationPage.EnterBusinessInformation(quote.BusinessClassification);

            //3. Enter policy level vehicle info
            VehicleInfoPage vehicleInfoPage = effectiveDatePage.EnterCustomerInformation(quote.CustomerInformation);

            //4. Enter policy level vehicle info
            VehicleEntryPage vehicleEntryPage = vehicleInfoPage.EnterPolicyLevelVehicleInfo<VehicleEntryPage>(quote.PolicyLevelVehicleInformation);

            //5. Add vehicles
            AddAnotherVehiclePage addAnotherVehiclePage = new AddAnotherVehiclePage();
            for (int i = 0; i < quote.VehicleList.Length; i++)
            {
                vehicleEntryPage.AddVehicle(quote.VehicleList[i]);
                if (i == quote.VehicleList.Length - 1)
                {
                    addAnotherVehiclePage.NoMoreVehicle();
                }
                else
                {
                    addAnotherVehiclePage.YesMoreVehicle();
                }
            }

            //6. Add driver
            DriverEnterPage driverEnterPage = new DriverEnterPage();
            AddAnotherDriverPage addAnotherDriverPage = new AddAnotherDriverPage();
            for (int i = 0; i < quote.DriverList.Length; i++)
            {
                if (quote.DriverList[i].DoesNotDrive)
                {
                    driverEnterPage.AddDriver<DriverEnterPage>(quote.DriverList[i]);
                }
                else
                {
                    driverEnterPage.AddDriver(quote.DriverList[i]);
                    if (i == quote.DriverList.Length - 1)
                    {
                        addAnotherDriverPage.NoMoreDriver();
                    }
                    else
                    {
                        addAnotherDriverPage.YesMoreDriver();
                    }
                }
            }

            //7. Select coverage limits
            CoveragesPage coveragesPage = new CoveragesPage();
            if (quote.CoverageLimits != null)
            {
                coveragesPage.SelectCoverageLimits(quote.CoverageLimits).SelectCalculateButton().SelectNextButton();

            }
            else
            {
                coveragesPage.UseDefaultCoverages();
            }

            //8. Select Additional Coverages
            AdditionalCoveragesPage additionalCoveragesPage = new AdditionalCoveragesPage();
            additionalCoveragesPage.SelectAdditionalCoverages<SummaryPage>(quote.AdditionalCoverages);

            //9. Enter Additional Coverages(if needed)
            SummaryPage summaryPage = additionalCoveragesPage.EnterAdditionalCoverages(quote.AdditionalCoverages);
            return summaryPage;
        }
    }
}