using Framework.Test.Common.DataObject;
using Framework.Test.Common.Helper;
using Framework.Tests.API.DataObject;
using Framework.Tests.API.Helper;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static Framework.Test.Common.Helper.ExtentReportsHelper;

namespace Framework.Tests.API.Service
{
    public class BreezeApi : ApiBase
    {
        #region Properties
        private const string ApiKey = "BreezeApi";
        public string QuoteId { get; set; }
        public string VehicleId { get; set; }
        public string DriverId { get; set; }

        public static Dictionary<string, Type> responseDataTypeList = new Dictionary<string, Type>()
        {
            {"vin", typeof(ResponseData_Vin)},
            {"enter", typeof(ResponseData_Enter)},
            {"add-another-vehicle", typeof(ResponseData_AddAnotherVehicle)},
            {"gross-weight", typeof(ResponseData_GrossWeight)},
            {"seating-capacity", typeof(ResponseData_SeatingCapacity)},
            {"physical-damage", typeof(ResponseData_PhysicalDamage)},
            {"classification", typeof(ResponseData_Classification)},
            {"factor", typeof(ResponseData_Factor)},
            {"body-type", typeof(ResponseData_BodyType)},
            {"descriptors", typeof(ResponseData_Descriptors)},
            {"convictions", typeof(ResponseData_Convictions)},
            {"coverages", typeof(ResponseData_Coverages)},
            {"accidents-and-violations", typeof(ResponseData_AccidentsAndViolations)},
            {"add-another-driver", typeof(ResponseData_AddAnotherDriver)},
            {"cdl-experience", typeof(ResponseData_CdlExperience)},
            {"hcno-liability-questions-1", typeof(ResponseData_HcnoLiabilityQuestions1)},
            {"hcno-liability-questions-2", typeof(ResponseData_HcnoLiabilityQuestions2)},
            {"hcno-liability-questions-3", typeof(ResponseData_HcnoLiabilityQuestions3)},
            {"hired-car-physical-damage", typeof(ResponseData_HiredCarPhsyicalDamage)},
            {"cargo-category", typeof(ResponseData_CargoCategory)},
            {"cargo-commodity", typeof(ResponseData_CargoCommodity)},
            {"additional-interest-counts", typeof(ResponseData_AdditionalInterestCounts)},
            {"summary", typeof(ResponseData_Summary)},
            {"cargo-limit-deductible", typeof(ResponseData_CargoLimitDeductible)},
            {"primary-officer", typeof(ResponseData_PrimaryOfficer)},
            {"business-information", typeof(ResponseData_BusinessInformation)},
            {"trailer-interchange", typeof(ResponseData_TrailerInterchange)},
            {"business-entity-type", typeof(ResponseData_BusinessEntityType)},
            {"dot-number", typeof(ResponseData_DotNumber)},
        };
        #endregion

        public BreezeApi(EnvironmentSetting setting, string quoteId = null) : base(ApiKey, setting)
        {
            QuoteId = quoteId ?? Guid.NewGuid().ToString();
            VehicleId = Guid.NewGuid().ToString();
            DriverId = Guid.NewGuid().ToString();
        }

        [ExtentStepNode]
        public IRestResponse ExecuteBreezeAPI(string screenName, object postBody, object header = null)
        {
            string action = GetActionByScreenName(screenName, postBody);
            return ExecuteAPI(action, postBody, header.ToDictionary());
        }

        [ExtentStepNode]
        public object GetExpectedResponseObject(object value)
        {
            ParameterValidator.ValidateNotNull(value, "Expected Response Object");
            return GetResponseObject(value.ToString());
        }

        [ExtentStepNode]
        public object GetActualResponseObject(string value)
        {
            string rawData = TryDeserializeValueToCorrectString(value);
            return GetResponseObject(rawData);
        }

        public void UpdateDynamicIdData(string screenName, string returnedResponse)
        {
            string rawData = TryDeserializeValueToCorrectString(returnedResponse);
            try
            {
                var data = NewtonsoftJsonSerializer.Default.Deserialize<ResponseData_GeneratedValue>(rawData);

                // In case VehicleId/DriverId was generated on the first time from response (not exist in request body)
                if (data.FormAlias == "vin" ||
                    (screenName == "vehicle-suggestions" && data.FormAlias == "body-type"))
                {
                    VehicleId = data.VehicleId;
                }
                else if (data.FormAlias == "enter")
                {
                    DriverId = data.DriverId;
                }
            }
            catch (Exception) { }
        }

        public string ConvertDynamicData(object data)
        {
            ParameterValidator.ValidateNotNull(data, "Data will be converted");

            string convertedData = data.ToString();
            if (Regex.IsMatch(convertedData, @"@{\w+}"))
            {
                if (QuoteId != null)
                    convertedData = convertedData.Replace("@{quoteId}", QuoteId);
                if (VehicleId != null)
                    convertedData = convertedData.Replace("@{vehicleId}", VehicleId);
                if (DriverId != null)
                    convertedData = convertedData.Replace("@{driverId}", DriverId);
            }
            
            return Framework.Test.Common.Helper.Utils.ConvertDynamicData(convertedData);
        }

        [ExtentStepNode]
        public IRestResponse HealthCheck()
        {
            string action = "/health-check?verbosity=detailed";
            return ExecuteAPI(action, null, null, Method.GET);
        }

        private IRestResponse ExecuteAPI(string action, object postBody, Dictionary<string, string> headerParameters = null, Method method = Method.PUT)
        {
            var node = GetLastNode();
            IRestResponse response = null;

            if (postBody != null)
            {
                node.Info("Create request body");
                node.Info(postBody.ToString().MarkupJsonString());
            }

            RequestParameter requestParameter = new RequestParameter
            {
                Method = method,
                OperationPath = action,
                HeaderParameters = headerParameters,
                PostBody = postBody
            };

            response = SendHttpCall(requestParameter);

            node.LogRestResponseInfo(response);

            return response;
        }

        private string GetActionByScreenName(string screenName, object postBody = null)
        {
            string action = string.Empty;

            switch (screenName)
            {
                case "vin":
                case "body-type":
                case "classification":
                case "factor":
                case "descriptors":
                case "gross-weight":
                case "seating-capacity":
                case "physical-damage":
                    action = string.Format("/quotes/{0}/vehicles/{1}/forms/{2}", QuoteId, VehicleId, screenName);
                    break;
                case "enter":
                case "accidents-and-violations":
                case "cdl-experience":
                case "convictions":
                    action = string.Format("/quotes/{0}/drivers/{1}/forms/{2}", QuoteId, DriverId, screenName);
                    break;
                default:
                    action = string.Format("/quotes/{0}/forms/{1}", QuoteId, screenName);
                    break;
            }

            if (screenName == "classification" || screenName == "factor")
            {
                if (postBody != null)
                {
                    JObject jsonData = postBody.ToJObject();
                    if (jsonData["questionId"] != null)
                    {
                        action += $"/{jsonData["questionId"]}";
                    }
                }
            }

            return action;
        }

        private object GetResponseObject(string value)
        {
            var node = GetLastNode();

            if (value is null) return null;

            object returnObject;
            try
            {
                var data = NewtonsoftJsonSerializer.Default.Deserialize<ResponseData>(value);
                if (data.FormAlias == null)
                {
                    JObject jsonData = value.ToJObject();
                    if (jsonData["result"] != null)
                    {
                        returnObject = NewtonsoftJsonSerializer.Default.Deserialize<object>(value.ToString());
                    }
                    else
                    {
                        returnObject = value;
                    }
                }
                else if (responseDataTypeList.ContainsKey(data.FormAlias))
                {
                    var mappedType = responseDataTypeList[data.FormAlias];
                    MethodInfo method = typeof(NewtonsoftJsonSerializer).GetMethod("Deserialize", new[] { typeof(string) });
                    MethodInfo genericMethod = method.MakeGenericMethod(mappedType);
                    returnObject = genericMethod.Invoke(NewtonsoftJsonSerializer.Default, new[] { value.ToString() });
                }
                else
                {
                    returnObject = value;
                }
            }
            catch(Exception)
            {
                returnObject = value;
            }

            node.Info("Object");
            node.Info(returnObject.ConvertObjectToJson().MarkupJsonString());
            return returnObject;
        }

        private string TryDeserializeValueToCorrectString(string value)
        {
            string rawData = string.Empty;
            try
            {
                rawData = NewtonsoftJsonSerializer.Default.Deserialize<string>(value);
            }
            catch (Exception)
            {
                rawData = value;
            }

            return rawData;
        }
    }
}
