using RestSharp;
using System.Collections.Generic;

namespace Framework.Tests.API.DataObject
{
    public class RequestParameter
    {
        public Method Method { get; set; }
        public string OperationPath { get; set; }
        public Dictionary<string, string> HeaderParameters { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
        public object PostBody { get; set; }
        public bool AddAccessToken = true;
        public bool AddSubscriptionKey = true;
    }
}
