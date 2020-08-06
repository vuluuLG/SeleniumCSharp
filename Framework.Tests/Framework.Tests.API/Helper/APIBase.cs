using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Tests.API.DataObject;

namespace Framework.Tests.API.Helper
{
    public class APIBase
    {
        public string AccessToken { get; set; }
        public string SubscriptionKey { get; set; }
        public string Code { get; set; }
        public string Environment { get; set; }
        public string Host { get; set; }
        public string BaseUrl { get; set; }
        public string ResourceAddress { get; set; }

        public APIBase(string apiKey, EnvironmentSetting setting)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("API key must be not null or empty");
            }

            ParameterValidator.ValidateNotNull(setting, "Environment Setting");

            Environment = setting.EnvironmentName;

            if (setting.Services.ContainsKey(apiKey))
            {
                var serviceSetting = setting.Services[apiKey];
                Host = serviceSetting.Host;
                ResourceAddress = serviceSetting.ResourceAddress;
                SubscriptionKey = serviceSetting.SubscriptionKey;
            }

            BaseUrl = Host;

            AccessToken = AccessTokenHelper.GetAccesstoken(setting.BrowserName, setting.WebUrl, setting.Username, setting.Password);
        }

        protected IRestResponse SendHttpCall(RequestParameter requestParameter)
        {
            ParameterValidator.ValidateNotNull(requestParameter, "Request parameters");

            var client = new RestClient(BaseUrl);

            string uri = BaseUrl;

            if (!string.IsNullOrEmpty(ResourceAddress))
            {
                uri = Url.Combine(uri, ResourceAddress);
            }

            if (!string.IsNullOrEmpty(requestParameter.OperationPath))
            {
                uri = Url.Combine(uri, requestParameter.OperationPath);
            }

            var request = new RestRequest(new Uri(uri), requestParameter.Method);

            // Header
            request.AddHeader("content-Type", "application/json");

            if (requestParameter.AddAccessToken)
            {
                request.AddHeader("Authorization", "Bearer " + AccessToken);
            }

            if (requestParameter.AddSubscriptionKey)
            {
                request.AddHeader("Ocp-Apim-Subscription-Key", SubscriptionKey);
            }

            if (requestParameter.HeaderParameters != null && requestParameter.HeaderParameters.Count > 0)
            {
                foreach (var item in requestParameter.HeaderParameters)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }

            // Parameters
            request.AddParameter("cache-control", "no-cache");

            if (requestParameter.QueryParameters != null && requestParameter.QueryParameters.Count > 0)
            {
                foreach (var item in requestParameter.QueryParameters)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
            }

            if (!string.IsNullOrEmpty(Code))
            {
                request.AddQueryParameter("code", Code);
            }

            // Body
            if (requestParameter.PostBody != null)
            {
                request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                if (requestParameter.PostBody is string)
                {
                    request.AddParameter(request.JsonSerializer.ContentType, requestParameter.PostBody, ParameterType.RequestBody);
                }
                else
                {
                    request.AddJsonBody(requestParameter.PostBody);
                }
            }

            var response = client.Execute(request);

            return response;
        }
    }
}
