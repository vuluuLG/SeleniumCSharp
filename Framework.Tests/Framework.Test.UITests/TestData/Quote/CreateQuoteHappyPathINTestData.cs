using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;

namespace Framework.Test.UITests.TestData
{
    public class CreateQuoteHappyPathINTestData
    {
        public string DefaultUMLimitType = "CSL - BIPD";
        public string ExpectedErrorMessage = "Uninsured Motorist Property Damage coverage is required in IN when an Uninsured Motorist deductible is present.";

        public BusinessClassification BusinessClassification = new BusinessClassification
        {
            BusinessGroup = "All Other",
            BusinessCategory = "Professional Services",
            BusinessSubCategory = "Photography & Videography"
        };

        public PolicyLevelVehicleInformation PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
        {
            Radius = "Over 500 Miles",
            Interstate = "No",
            GaragingZipCode = null
        };

        public CustomerInformation CustomerInformation = new CustomerInformation
        {
            EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
            DotNumber = null,
            EntityType = "Corporation",
            BusinessInformation = new BusinessInformation
            {
                CustomerName = "Happy Path - IN - UM Deductible",
                DBA = "",
                Address = new Address
                {
                    Line1 = "302 W Washington St",
                    Line2 = "",
                    City = "Indianapolis",
                    State = "IN",
                    Zip = "46204"
                }
            },
            CustomerAdditionalInformation = new CustomerAdditionalInformation
            {
                BusinessDateRange = "5 or more years",
                FilingType = "No",
                LiabilityLosses = "0"
            },
            POName = new FullName
            {
                FirstName = "Primary",
                MiddleInitial = "",
                LastName = "Officer"
            },
            POAddress = new Address
            {
                IsSameAddress = true
            }
        };

        public VehicleInformation VehicleInfo = new VehicleInformation
        {
            VIN = null,
            VinStatus = VinStatus.Invalid,
            VehicleDescription = new VehicleDescription
            {
                Year = "2020",
                Make = "Acura",
                Model = "TL"
            },
            PhysicalDamage = new PhysicalDamage
            {
                UsePhysicalDamageCoverage = "No"
            },
            BodyType = "Sedan"
        };

        public DriverInformation DriverInfo = new DriverInformation
        {
            MVRStatus = OrderMVRStatus.Invalid,
            IsPO = true,
            FullName = new FullName
            {
                FirstName = "Primary",
                MiddleInitial = "",
                LastName = "Officer"
            },
            DateOfBirth = "01/01/1980",
            LicenseNumber = null,
            LicenseState = null,
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
        };

        public CoverageLimits CoverageLimits1 = new CoverageLimits
        {
            UMLimitType = "CSL - BI only",
            UMDeductible = "None"
        };

        public CoverageLimits CoverageLimits2 = new CoverageLimits
        {
            Recalculate = true,
            UMLimitType = "Split - BIPD",
            UMSplitLimit = "$1,000,000/$1,000,000",
            UMSplitBipdLimit = "$1,000,000",
            UMDeductible = "$300"
        };
    }
}
