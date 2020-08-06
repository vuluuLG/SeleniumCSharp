using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.API.Service
{
    public class CommandData
    {
        public string DotNumber { get; set; }
        public string FilingType { get; set; }
        public string UserEmail { get; set; }
    }

    public class CommandData_FilingType
    {
        public string DotNumber { get; set; }
        public string FilingType { get; set; }
        public string IsUnkownUsDotNumber { get; set; }
        public string UnknownUsDotNumberReason { get; set; }
    }

    public class CommandData_Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class CommandData_Applicant
    {
        public CommandData_Address Address { get; set; }
        public string BusinessDescription { get; set; }
        public string DoingBusinessAs { get; set; }
        public string EntityType { get; set; }
        public string OtherEntityType { get; set; }
        public string NameOfBusiness { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
    }


    public class CommandData_PrimaryOfficerAddress
    {
        public CommandData_Address Address { get; set; }
    }

    public class CommandProcessorCommand
    {
        public string AggregateName { get; set; }
        public string AggregateId { get; set; }
        public string CommandName { get; set; }
        public object CommandData { get; set; }
        public string QuoteId { get; set; }
    }

    public class CommandData_GeneralRisk
    {
        public string EffectiveDate { get; set; }
        public string BusinessStarted { get; set; }
        public string RiskState { get; set; }
        public string PowerUnitCount { get; set; }
    }

    public class CommandData_SpecifyInsuredPriorCarrier
    {
        public string InsuredPriorCarrierName { get; set; }
    }

    public class CommandData_SpecifyVehicleBodyType
    {
        public string VehicleId { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class CommandData_IndicateVehicleDescriptors
    {
        public string VehicleId { get; set; }
        public string Vin { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class CommandData_SpecifyDriverInfo
    {
        public string DriverId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public string State { get; set; }
    }

    public class CommandData_SpecifyDriverName
    {
        public string DriverId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
    }

    public class CommandData_SpecifyDriverDateOfBirth
    {
        public string DriverId { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class CommandData_SpecifyDriverAccidentsAndViolations
    {
        public string DriverId { get; set; }
        public string AtFaultAccidentCount { get; set; }
        public string ViolationCount { get; set; }
    }

    public class CommandData_IndicateDesiredConversionEmbezzlementSecretion
    {
        public string VehicleId { get; set; }
        public string Desired { get; set; }
    }

    public class CommandData_SpecifyApplicantEntityType
    {
        public string EntityType { get; set; }
        public string OtherEntityType { get; set; }
    }

    public class CommandData_IndicatePhysicalDamage
    {
        public string VehicleValue { get; set; }
        public string Deductible { get; set; }
    }

    public class CommandData_EnterDriverLicenseInfo
    {
        public string LicenseNumber { get; set; }
        public string State { get; set; }
    }

    public class CommandData_SpecifyCargoLimitDeductible
    {
        public string Limit { get; set; }
        public string Deductible { get; set; }
    }

    public class CommandData_SpecifyPriorLosses
    {
        public string Value { get; set; }
    }

    public class CommandData_BodilyInjuryLimit
    {
        public string BippLimit { get; set; }
        public string BipaLimit { get; set; }
    }

    public class CommandData_SpecifyUninsuredMotoristCoverageSplitLimit
    {
        public CommandData_BodilyInjuryLimit BodilyInjuryLimit { get; set; }
    }

    public class CommandData_SpecifyLiabilitySplitCoverage
    {
        public CommandData_BodilyInjuryLimit BodilyInjuryLimit { get; set; }
        public string BipdLimit { get; set; }
    }

    public class CommandData_SpecifyLiabilityCombinedSingleLimit
    {
        public string Limit { get; set; }
    }

    public class CommandData_SpecifyUnderinsuredMotoristCombinedSingleLimit
    {
        public string Limit { get; set; }
    }

    public class CommandData_IndicateDriverHasCommercialDriversLicense
    {
        public string DriverId { get; set; }
        public string HasCommercialDriversLicense { get; set; }
        public string DateIssued { get; set; }
    }

    public class CommandData_SpecifyMedicalPaymentLimit
    {
        public string Limit { get; set; }
    }

    public class CommandData_AnswerBusinessClassificationQuestion
    {
        public string FlowQuestionId { get; set; }
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
    }

    public class CommandData_SpecifyDotNumberInfo
    {
        public string SubmissionNumber { get; set; }
        public string DotNumber { get; set; }
        public string IsUnknownDotNumber { get; set; }
    }

    public class CommandData_IndicateAlteredSeatingCapacity
    {
        public string VehicleId { get; set; }
        public string Altered { get; set; }
    }

    public class CommandData_SpecifyBusinessClassificationQuestionResult
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
    }

    public class CommandData_SpecifyUninsuredMotoristPhysicalDamage
    {
        public string HasUninsuredMotoristPhysicalDamageCoverage { get; set; }
    }

    public class CommandData_SpecifyAdditionalInterestCounts
    {
        public string DesignatedInsuredCount { get; set; }
        public string AdditionalNamedInsuredCount { get; set; }
        public string NamedInsuredWaiverOfSubrogationCount { get; set; }
        public string WantsBlanketAdditionalInsured { get; set; }
        public string BlanketWaiverOfSubrogationCount { get; set; }
    }

    public class CommandData_SpecifyDriverConvictions
    {
        public string DriverId { get; set; }
        public string ConvictionCount { get; set; }
        public string[] YearsAgoFrom { get; set; }
        public string[] YearsAgoTo { get; set; }
    }

    public class CommandData_SpecifyGeneralRiskDateRange
    {
        public string BusinessDateRange { get; set; }
    }

    public class CommandData_SpecifyUninsuredMotoristCoverageCombinedSingleLimit
    {
        public string CombinedSingleLimit { get; set; }
    }

    public class CommandData_AnswerVehicleClassificationQuestion
    {
        public string FlowQuestionId { get; set; }
        public string Question { get; set; }
        public string[] Answers { get; set; }
    }

    public class CommandData_ReportDriverIncident
    {
        public string DriverId { get; set; }
        public string Type { get; set; }
        public string YearsAgoFrom { get; set; }
        public string YearsAgoTo { get; set; }
    }

    public class CommandData_SpecifyVehicleClassificationQuestionResult
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
    }

    public class CommandData_SpecifyEffectiveDate
    {
        public string Value { get; set; }
    }

    public class CommandData_SpecifyVehicleDetails
    {
        public string VehicleId { get; set; }
        public string Airbags { get; set; }
        public string AntiLockBrakes { get; set; }
        public string SeatingCapacity { get; set; }
        public string Radius { get; set; }
        public string CommerceClassification { get; set; }
        public string Territory { get; set; }
        public string AiLessor { get; set; }
        public string LossPayee { get; set; }
        public string GrossVehicleWeight { get; set; }
    }

    public class CommandData_SpecifyUnderinsuredMotoristSplitLimit
    {
        public CommandData_BodilyInjuryLimit BodilyInjuryLimit { get; set; }
    }

    public class CommandProcessorApi : ApiBase
    {
        #region Properties
        private const string ApiKey = "CommandProcessorApi";
        #endregion

        #region Entities
        private string _userEmail = string.Empty;
        private string _id = string.Empty;

        public string QuoteID
        {
            get { return _id; }
        }

        #endregion

        #region Actions
        public CommandProcessorApi(EnvironmentSetting setting, string id = "") : base(ApiKey, setting)
        {
            if (string.IsNullOrEmpty(id))
            {
                Guid g = Guid.NewGuid();
                _id = g.ToString();
            }
            else
            {
                _id = id;
            }

            _userEmail = setting.Username;
        }

        [ExtentStepNode]
        public IRestResponse ExecuteCommand(string commandName, object cmdPayload)
        {
            var postBody = new CommandProcessorCommand()
            {
                AggregateName = "Quote",
                AggregateId = _id,
                CommandName = commandName,
                CommandData = cmdPayload
            };

            return ExecuteAPI("", postBody);
        }

        [ExtentStepNode]
        public string CreateNewQuote()
        {
            string quoteID = string.Empty;

            var response = ExecuteAPI("CreateNewQuote",
                new CommandData()
                {
                    UserEmail = _userEmail
                }
            );

            if (response.StatusCode == HttpStatusCode.OK)
            {
                quoteID = _id;
            }

            GetLastNode().Info("Quote ID: " + quoteID);

            return quoteID;
        }

        [ExtentStepNode]
        public string ApproveQuote(string quoteID = null)
        {
            if (!string.IsNullOrEmpty(quoteID))
            {
                _id = quoteID;
            }

            GetLastNode().Info("Approve Quote ID: " + _id);
            var response = ExecuteCommand("ApproveQuote", new CommandData());
            return (response.StatusCode == HttpStatusCode.OK);
        }

        [ExtentStepNode]
        public string BindQuote(string quoteID = null)
        {
            if (!string.IsNullOrEmpty(quoteID))
            {
                _id = quoteID;
            }

            GetLastNode().Info("Bind Quote ID: " + _id);
            var response = ExecuteCommand("BindQuote", new CommandData());
            return (response.StatusCode == HttpStatusCode.OK);
        }

        private IRestResponse ExecuteAPI(string action, object postBody)
        {
            var node = GetLastNode();
            IRestResponse response = null;

            node.LogRequestBodyInfo(postBody);

            RequestParameter requestParameter = new RequestParameter
            {
                Method = Method.POST,
                OperationPath = action,
                PostBody = postBody
            };

            response = SendHttpCall(requestParameter);

            node.LogRestResponseInfo(response);

            return response;
        }
        #endregion
    }
}
