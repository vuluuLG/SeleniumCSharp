using Framework.Test.Common.DataObject;
using Framework.Test.Common.Helper;
using Framework.Tests.API.DataObject;
using Framework.Tests.API.Helper;
using RestSharp;
using System.Collections.Generic;
using static Framework.Test.Common.Helper.ExtentReportsHelper;

namespace Framework.Tests.API.Service
{
    public class ZipTerritoryApi : ApiBase
    {
        #region Properties
        private const string ApiKey = "ZipTerritoryApi";
        #endregion

        #region Actions
        public ZipTerritoryApi(EnvironmentSetting setting) : base(ApiKey, setting)
        {
        }

        [ExtentStepNode]
        public IRestResponse GetTerritory(string zipCode, string companyNumber, string effectiveDate)
        {
            string action = "/v2/territories";

            // Prepare queqy parameters
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            if (zipCode != null)
                queryParameters.Add("zipcode", zipCode);
            if (companyNumber != null)
                queryParameters.Add("companyNumber", companyNumber);
            if (effectiveDate != null)
                queryParameters.Add("effectiveDate", effectiveDate);

            return ExecuteAPI(action, queryParameters);
        }

        [ExtentStepNode]
        public IRestResponse HealthCheck()
        {
            string action = "/health-check?verbosity=detailed";
            return ExecuteAPI(action);
        }

        private IRestResponse ExecuteAPI(string action, Dictionary<string, string> queryParameters = null)
        {
            RequestParameter requestParameter = new RequestParameter
            {
                Method = Method.GET,
                OperationPath = action,
                QueryParameters = queryParameters
            };

            IRestResponse response = SendHttpCall(requestParameter);
            GetLastNode().LogRestResponseInfo(response);
            return response;
        }
        #endregion
    }
}
