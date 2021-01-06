using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;

namespace Framework.Test.UITests.TestData
{
    public class EditQuoteSubmittedQuotesCannotBeEditedTestData
    {
        public Quote Quote = new Quote
        {
            BusinessClassification = new BusinessClassification
            {
                BusinessGroup = "Public Organizations & Transport",
                BusinessCategory = "Daycare",
                BusinessSubCategory = "Headstart/Preschool"
            },

            PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
            {
                Radius = "0 - 25 Miles",
                Interstate = "Yes",
                GaragingZipCode = null,
                Transportation = "No"
            },

            CustomerInformation = new CustomerInformation
            {
                EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                DotNumber = null,
                EntityType = "LLC",
                BusinessInformation = new BusinessInformation
                {
                    CustomerName = "Regression - Submitted Quote cannot be edited",
                    DBA = "",
                    Address = new Address
                    {
                        Line1 = "930 Pennsylvania Ave",
                        Line2 = "",
                        City = "Aurora",
                        State = "IL",
                        Zip = "60506"
                    }
                },
                CustomerAdditionalInformation = new CustomerAdditionalInformation
                {
                    BusinessDateRange = "At least 1 year but less than 2 years",
                    FilingType = "No",
                    LiabilityLosses = "1"
                },
                POName = new FullName
                {
                    FirstName = "Jane",
                    MiddleInitial = "",
                    LastName = "Smith"
                },
                POAddress = new Address
                {
                    IsSameAddress = true
                }
            },

            VehicleList = new VehicleInformation[]
            {
                new VehicleInformation
                {
                    VIN = "4S3BMHB68B3286050",
                    VinStatus = VinStatus.Unknown,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2011",
                        Make = "SUBARU",
                        Model = "LEGACY AWD"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Minivan",
                    ClassificationFlow = new string[] { "No" },
                    FactorFlow = new string[] { "No" }
                }
            },

            DriverList = new DriverInformation[]
            {
                new DriverInformation
                {
                    IsPO = true,
                    MVRStatus = OrderMVRStatus.Invalid,
                    FullName = new FullName
                    {
                        FirstName = "Jane",
                        MiddleInitial = "",
                        LastName = "Smith"
                    },
                    DateOfBirth = "01/01/1960",
                    LicenseNumber = "456123",
                    LicenseState = "IL",
                    DoesNotDrive = false,
                    CDLExperience = new CDLExperience
                    {
                        HasCDL = "No"
                    },
                    AccidentsAndViolations = new AccidentsAndViolations
                    {
                        HasAccidentsOrViolations = "No"
                    },
                    Conviction = new Conviction
                    {
                        NumberConviction = "0"
                    }
                }
            },

            AdditionalCoverages = null
        };

        public string[] MenuHeaderTitles = { "Customer", "Vehicles", "Drivers", "Coverage" };
    }

}
