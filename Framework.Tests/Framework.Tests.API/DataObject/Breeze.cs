namespace Framework.Tests.API.DataObject
{
    public class ResponseData
    {
        public string FormAlias { get; set; }
        public string QuoteId { get; set; }
    }

    public class ResponseData_GeneratedValue
    {
        public string FormAlias { get; set; }
        public string VehicleId { get; set; }
        public string DriverId { get; set; }
    }

    public class ResponseData_AddAnotherVehicle : ResponseData
    {
    }

    public class ResponseData_AddAnotherDriver : ResponseData
    {
    }

    public class ResponseData_Coverages : ResponseData
    {
    }

    public class ResponseData_Vin : ResponseData
    {
        public string VehicleId { get; set; }
    }

    public class ResponseData_Enter : ResponseData
    {
        public string DriverId { get; set; }
    }

    public class ResponseData_BodyType : ResponseData_Vin
    {
    }

    public class ResponseData_GrossWeight : ResponseData_Vin
    {
    }

    public class ResponseData_SeatingCapacity : ResponseData_Vin
    {
    }

    public class ResponseData_PhysicalDamage : ResponseData_Vin
    {
    }

    public class ResponseData_Descriptors : ResponseData_Vin
    {
    }

    public class ResponseData_Convictions : ResponseData_Enter
    {
    }

    public class ResponseData_AccidentsAndViolations : ResponseData_Enter
    {
    }

    public class ResponseData_CdlExperience : ResponseData_Enter
    {
    }

    public class ResponseData_Classification : ResponseData_Vin
    {
        public string FlowQuestionId { get; set; }
    }

    public class ResponseData_Factor : ResponseData_Vin
    {
        public string FlowQuestionId { get; set; }
    }

    public class ResponseData_HcnoLiabilityQuestions1 : ResponseData
    {
    }

    public class ResponseData_HcnoLiabilityQuestions2 : ResponseData
    {
    }

    public class ResponseData_HcnoLiabilityQuestions3 : ResponseData
    {
    }

    public class ResponseData_HiredCarPhsyicalDamage : ResponseData
    {
    }

    public class ResponseData_CargoCategory : ResponseData
    {
    }

    public class ResponseData_CargoCommodity : ResponseData
    {
    }

    public class ResponseData_AdditionalInterestCounts : ResponseData
    {
    }

    public class ResponseData_Summary : ResponseData
    {
    }

    public class ResponseData_CargoLimitDeductible : ResponseData
    {
    }

    public class ResponseData_PrimaryOfficer : ResponseData
    {
    }

    public class ResponseData_BusinessInformation : ResponseData
    {
    }

    public class ResponseData_TrailerInterchange : ResponseData
    {
    }

    public class ResponseData_BusinessEntityType : ResponseData
    {
    }

    public class ResponseData_DotNumber : ResponseData
    {
    }
}
