using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.API.Helper
{
    public class Utils
    {
        public static void LogRequestBodyInfo(this ExtentTest node, object body)
        {
            if (body != null)
            {
                ParameterValidator.ValidateNotNull(node, "Extent Test Node");
                node.Info("Create request body");
                node.Info(body.ConvertObjectToJson().MarkupJsonString());
            }
        }

        public static void LogRestResponseInfo(this ExtentTest node, IRestResponse response)
        {
            ParameterValidator.ValidateNotNull(node, "Extent Test Node");
            node.Info("Response");
            node.Info(response.ConvertObjectToJson().MarkupJsonString());

            if (response != null)
            {
                node.Info("Status Code");
                node.Info(response.StatusCode.ConvertObjectToJson().MarkupJsonString());
                node.Info("Status Description");
                node.Info(response.StatusDescription.ConvertObjectToJson().MarkupJsonString());
                node.Info("Content");
                node.Info(response.Content.MarkupJsonString());
            }
        }
    }
}
