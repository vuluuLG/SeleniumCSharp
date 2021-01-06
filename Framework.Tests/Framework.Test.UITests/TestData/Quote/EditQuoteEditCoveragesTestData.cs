using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;

namespace Framework.Test.UITests.TestData
{
    public class EditQuoteEditCoveragesTestData
    {
        public EditQuoteEditCoveragesInformation[] testDataList = new EditQuoteEditCoveragesInformation[]
        {
            // Regression - Edit Coverages IL
            new EditQuoteEditCoveragesInformation()
            {
                BusinessClassification = new BusinessClassification
                {
                    BusinessGroup = "Medical",
                    BusinessCategory = "Doctors & Professional Health Businesses",
                    BusinessSubCategory = "Dietician"
                },

                PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
                {
                    Radius = "Over 500 Miles",
                    Interstate = "Yes",
                    GaragingZipCode = null
                },

                CustomerInformation = new CustomerInformation
                {
                    EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                    DotNumber = null,
                    EntityType = "Corporation",
                    BusinessInformation = new BusinessInformation
                    {
                        CustomerName = "Regression - Edit Coverages IL",
                        DBA = "",
                        Address = new Address
                        {
                            Line1 = "1309 Deerpath Dr",
                            Line2 = "",
                            City = "Morris",
                            State = "IL",
                            Zip = "60450"
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
                },

                VehicleInfo1 = new VehicleInformation
                {
                    VIN = null,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2015",
                        Make = "Honda",
                        Model = "Civic"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Sedan",
                    FactorFlow = new string[] { "No" }
                },

                DriverInfo1 = new DriverInformation
                {
                    MVRStatus = OrderMVRStatus.Unknown,
                    IsPO = true,
                    LicenseNumber = "G01234567",
                    LicenseState = "IL",
                    DateOfBirth = "01/01/1980",
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
                },

                DefaultCoverageSections = new string []{ "Liability", "UM", "UIM", "Medical Payments" },

                CoverageLimits = new CoverageLimits
                {
                    Recalculate = true,
                    LiabilityLimitType = "Split",
                    LiabilitySplitLimit = "$1,000,000/$1,000,000",
                    LiabilitySplitBipdLimit = "$500,000",
                    UMLimitType = "Split",
                    UMSplitLimit = "$1,000,000/$1,000,000",
                    UMPD = true,
                    UIMLimitType = "Split",
                    UIMSplitLimit = "$1,000,000/$1,000,000",
                    MedicalPaymentsLimit = "$2,000"
                },

                AdditionalCoverages = new AdditionalCoverages
                {
                    AdditionalInterests = new AdditionalInterests
                    {
                        DesignatedInsuredCount = "0",
                        AdditionalNamedInsuredCount = "0",
                        NamedInsuredWaiverOfSubrogationCount = "0",
                        WantsBlanketAdditionalInsured = "Yes",
                        BlanketWaiverOfSubrogationCount = "4"
                    },

                    Cargo = new Cargo
                    {
                        CargoLimitDeductible = new CargoLimitDeductible
                        {
                            CargoLimit = "$100,000",
                            Deductible = "$10,000"
                        },
                        CargoCategory = "Consumer Goods",
                        CargoCommodity = "Clothing",
                        CargoModifiers = null
                    }
                },

                AdditionalCoveragesChange = new AdditionalCoverages
                {
                    TrailerInterchange = new TrailerInterchange
                    {
                        Limit = "$70,000",
                        Deductible = "$1,000/$1,000"
                    }
                }
            },

            // Regression - Edit Coverages CO
            new EditQuoteEditCoveragesInformation()
            {
                BusinessClassification = new BusinessClassification
                {
                    BusinessGroup = "Contractor",
                    BusinessCategory = "Artisan Contractor",
                    BusinessSubCategory = "Granite/Marble"
                },

                PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
                {
                    Radius = "Over 500 Miles",
                    Interstate = "Yes",
                    GaragingZipCode = null
                },

                CustomerInformation = new CustomerInformation
                {
                    EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                    DotNumber = null,
                    EntityType = "Corporation",
                    BusinessInformation = new BusinessInformation
                    {
                        CustomerName = "Regression - Edit Coverages CO",
                        DBA = "",
                        Address = new Address
                        {
                            Line1 = "4057 Boxelder Dr",
                            Line2 = "",
                            City = "Loveland",
                            State = "CO",
                            Zip = "80538"
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
                },

                VehicleInfo1 = new VehicleInformation
                {
                    VIN = null,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2016",
                        Make = "Ford",
                        Model = "F-150"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Pickup",
                    FactorFlow = new string[] { "Yes" }
                },

                DriverInfo1 = new DriverInformation
                {
                    MVRStatus = OrderMVRStatus.Invalid,
                    IsPO = true,
                    DateOfBirth = "01/01/1980",
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
                },

                DefaultCoverageSections = new string []{ "Liability", "UM", "Medical Payments" },

                CoverageLimits = new CoverageLimits
                {
                    Recalculate = true,
                    LiabilityLimitType = "Split",
                    LiabilitySplitLimit = "$1,000,000/$1,000,000",
                    LiabilitySplitBipdLimit = "$500,000",
                    UMLimitType = "CSL",
                    UMCslLimit = "$750,000",
                    UMPD = true,
                    MedicalPaymentsLimit = "Decline this coverage"
                },

                AdditionalCoverages = new AdditionalCoverages
                {
                    AdditionalInterests = new AdditionalInterests
                    {
                        DesignatedInsuredCount = "1",
                        AdditionalNamedInsuredCount = "1",
                        NamedInsuredWaiverOfSubrogationCount = "1",
                        WantsBlanketAdditionalInsured = "No"
                    },

                    Cargo = new Cargo
                    {
                        CargoLimitDeductible = new CargoLimitDeductible
                        {
                            CargoLimit = "$100,000",
                            Deductible = "$10,000"
                        },
                        CargoCategory = "Consumer Goods",
                        CargoCommodity = "Clothing",
                        CargoModifiers = null
                    }
                },

                AdditionalCoveragesChange = new AdditionalCoverages
                {
                    TrailerInterchange = new TrailerInterchange
                    {
                        Limit = "$70,000",
                        Deductible = "$1,000/$1,000"
                    }
                }
            },

            // Regression - Edit Coverages TX
            new EditQuoteEditCoveragesInformation()
            {
                BusinessClassification = new BusinessClassification
                {
                    BusinessGroup = "Trucks & Truckers",
                    BusinessCategory = "General Commodity Trucking",
                    BusinessSubCategory = "General Commodity Trucker"
                },

                PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
                {
                    Radius = "Over 500 Miles",
                    Interstate = "Yes",
                    GaragingZipCode = null
                },

                CustomerInformation = new CustomerInformation
                {
                    EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                    DotNumber = null,
                    EntityType = "Corporation",
                    BusinessInformation = new BusinessInformation
                    {
                        CustomerName = "Regression - Edit Coverages TX",
                        DBA = "",
                        Address = new Address
                        {
                            Line1 = "503 W Martin Luther King Jr Blvd",
                            Line2 = "",
                            City = "Austin",
                            State = "TX",
                            Zip = "78701"
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
                },

                VehicleInfo1 = new VehicleInformation
                {
                    VIN = null,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2017",
                        Make = "Mack",
                        Model = "Mack"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Tractor",
                    ClassificationFlow = new string[] { "None of the Above", "Other commodities" }
                },

                DriverInfo1 = new DriverInformation
                {
                    MVRStatus = OrderMVRStatus.Invalid,
                    IsPO = true,
                    DateOfBirth = "01/01/1980",
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
                },

                DefaultCoverageSections = new string []{ "Liability", "UM/UIM", "Medical Payments", "Personal Injury Protection" },

                CoverageLimits = new CoverageLimits
                {
                    Recalculate = true,
                    LiabilityLimitType = "CSL",
                    LiabilityCslLimit = "$750,000",
                    UMUIMLimitType = "Split",
                    UMUIMSplitLimit = "$500,000/$500,000",
                    UMUIMSplitBipdLimit = "$300,000",
                    MedicalPaymentsLimit = "$500",
                    PersonalInjuryProtection = "Decline this coverage"
                },

                AdditionalCoverages = new AdditionalCoverages
                {
                    AdditionalInterests = new AdditionalInterests
                    {
                        DesignatedInsuredCount = "1",
                        AdditionalNamedInsuredCount = "0",
                        NamedInsuredWaiverOfSubrogationCount = "0",
                        WantsBlanketAdditionalInsured = "Yes",
                        BlanketWaiverOfSubrogationCount = "0"
                    },

                    Cargo = new Cargo
                    {
                        CargoLimitDeductible = new CargoLimitDeductible
                        {
                            CargoLimit = "$100,000",
                            Deductible = "$10,000"
                        },
                        CargoCategory = "Consumer Goods",
                        CargoCommodity = "Clothing",
                        CargoModifiers = null
                    }
                },

                AdditionalCoveragesChange = new AdditionalCoverages
                {
                    TrailerInterchange = new TrailerInterchange
                    {
                        Limit = "$70,000",
                        Deductible = "$1,000/$1,000"
                    }
                }
            },

            // Regression - Edit Coverages GA
            new EditQuoteEditCoveragesInformation()
            {
                BusinessClassification = new BusinessClassification
                {
                    BusinessGroup = "Public Organizations & Transport",
                    BusinessCategory = "Public Auto All Other",
                    BusinessSubCategory = "Staffing Agency"
                },

                PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
                {
                    Radius = "Over 500 Miles",
                    Interstate = "Yes",
                    GaragingZipCode = null
                },

                CustomerInformation = new CustomerInformation
                {
                    EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                    DotNumber = null,
                    EntityType = "Corporation",
                    BusinessInformation = new BusinessInformation
                    {
                        CustomerName = "Regression - Edit Coverages GA",
                        DBA = "",
                        Address = new Address
                        {
                            Line1 = "1622 Holly Springs Rd NE",
                            Line2 = "",
                            City = "Marietta",
                            State = "GA",
                            Zip = "30062"
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
                },

                VehicleInfo1 = new VehicleInformation
                {
                    VIN = null,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2018",
                        Make = "Acura",
                        Model = "Integra"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Sedan",
                    ClassificationFlow = new string[] { "Transport passengers", "No", "Yes" }
                },

                DriverInfo1 = new DriverInformation
                {
                    MVRStatus = OrderMVRStatus.Invalid,
                    IsPO = true,
                    DateOfBirth = "01/01/1980",
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
                },

                DefaultCoverageSections = new string []{ "Liability", "UM/UIM", "Medical Payments" },

                CoverageLimits = new CoverageLimits
                {
                    Recalculate = true,
                    UMUIMDeductible = "$2,000",
                    AddOnCoverage = true,
                    MedicalPaymentsLimit = "$1,000"
                },

                AdditionalCoverages = new AdditionalCoverages
                {
                    AdditionalInterests = new AdditionalInterests
                    {
                        DesignatedInsuredCount = "5",
                        AdditionalNamedInsuredCount = "5",
                        NamedInsuredWaiverOfSubrogationCount = "5",
                        WantsBlanketAdditionalInsured = "No"
                    },

                    Cargo = new Cargo
                    {
                        CargoLimitDeductible = new CargoLimitDeductible
                        {
                            CargoLimit = "$100,000",
                            Deductible = "$10,000"
                        },
                        CargoCategory = "Consumer Goods",
                        CargoCommodity = "Clothing",
                        CargoModifiers = null
                    }
                },

                AdditionalCoveragesChange = new AdditionalCoverages
                {
                    TrailerInterchange = new TrailerInterchange
                    {
                        Limit = "$70,000",
                        Deductible = "$1,000/$1,000"
                    }
                }
            },

            // Regression - Edit Coverages FL
            new EditQuoteEditCoveragesInformation()
            {
                BusinessClassification = new BusinessClassification
                {
                    BusinessGroup = "All Other",
                    BusinessCategory = "Animal Businesses",
                    BusinessSubCategory = "Chicken Coop Cleaner"
                },

                PolicyLevelVehicleInformation = new PolicyLevelVehicleInformation
                {
                    Radius = "Over 500 Miles",
                    Interstate = "Yes",
                    WorkersCompensation = "Yes",
                    GaragingAddress = new Address
                    {
                        IsSameAddress = true
                    }
                },

                CustomerInformation = new CustomerInformation
                {
                    EffectiveDate = DateHelper.GetNextDateString("MM/dd/yyyy"),
                    DotNumber = null,
                    EntityType = "Corporation",
                    BusinessInformation = new BusinessInformation
                    {
                        CustomerName = "Regression - Edit Coverages FL",
                        DBA = "",
                        Address = new Address
                        {
                            Line1 = "1300 W Main St",
                            Line2 = "",
                            City = "Pensacola",
                            State = "FL",
                            Zip = "32502"
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
                },

                VehicleInfo1 = new VehicleInformation
                {
                    VIN = null,
                    VehicleDescription = new VehicleDescription
                    {
                        Year = "2019",
                        Make = "GMC",
                        Model = "Sierra"
                    },
                    PhysicalDamage = new PhysicalDamage
                    {
                        UsePhysicalDamageCoverage = "No"
                    },
                    BodyType = "Pickup",
                },

                DriverInfo1 = new DriverInformation
                {
                    MVRStatus = OrderMVRStatus.Invalid,
                    IsPO = true,
                    DateOfBirth = "01/01/1980",
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
                },

                DefaultCoverageSections = new string []{ "Liability", "UM", "Medical Payments", "Personal Injury Protection" },

                CoverageLimits = new CoverageLimits
                {
                    Recalculate = true,
                    LiabilityLimitType = "CSL",
                    LiabilityCslLimit = "$30,000",
                    UMLimitType = "CSL",
                    UMCslLimit = "Decline this coverage",
                    MedicalPaymentsLimit = "Decline this coverage"
                },

                AdditionalCoverages = new AdditionalCoverages
                {
                    AdditionalInterests = new AdditionalInterests
                    {
                        DesignatedInsuredCount = "4",
                        AdditionalNamedInsuredCount = "0",
                        NamedInsuredWaiverOfSubrogationCount = "0",
                        WantsBlanketAdditionalInsured = "Yes",
                        BlanketWaiverOfSubrogationCount = "5"
                    },

                    Cargo = new Cargo
                    {
                        CargoLimitDeductible = new CargoLimitDeductible
                        {
                            CargoLimit = "$100,000",
                            Deductible = "$10,000"
                        },
                        CargoCategory = "Consumer Goods",
                        CargoCommodity = "Clothing",
                        CargoModifiers = null
                    }
                },

                AdditionalCoveragesChange = new AdditionalCoverages
                {
                    TrailerInterchange = new TrailerInterchange
                    {
                        Limit = "$70,000",
                        Deductible = "$1,000/$1,000"
                    }
                }
            }
        };
    }

    public class EditQuoteEditCoveragesInformation
    {
        public string DialogContent = "Are you sure you want to delete Cargo?";
        public string[] ExpectedAdditionalCoverages = new string[]
        {
            AdditionalCoveragesType.HiredCarNonOwned,
            AdditionalCoveragesType.Cargo,
            AdditionalCoveragesType.TrailerInterchange
        };
        public string[] UnexpectedAdditionalCoverages = new string[]
        {
            AdditionalCoveragesType.AdditionalInterests
        };
        public string[] DefaultCoverageSections { get; set; }
        public string[] DefaultAdditionalCoveragesList = new string[]
        {
            AdditionalCoveragesType.AdditionalInterests,
            AdditionalCoveragesType.HiredCarNonOwned,
            AdditionalCoveragesType.Cargo,
            AdditionalCoveragesType.TrailerInterchange
        };
        public BusinessClassification BusinessClassification { get; set; }
        public PolicyLevelVehicleInformation PolicyLevelVehicleInformation { get; set; }
        public CustomerInformation CustomerInformation { get; set; }
        public VehicleInformation VehicleInfo1 { get; set; }
        public DriverInformation DriverInfo1 { get; set; }
        public CoverageLimits CoverageLimits { get; set; }
        public AdditionalCoverages DefaultAdditionalCoverages = null;
        public AdditionalCoverages AdditionalCoverages { get; set; }
        public AdditionalCoverages AdditionalCoveragesChange { get; set; }
    }
}
