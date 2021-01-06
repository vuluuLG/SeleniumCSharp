using System.Net;

namespace Framework.Test.APITests.DataObject
{
    /// <summary>
    /// Data object for API test case that has single step/request
    /// </summary>
    public class ApiTestCase : BaseApiTestCase
    {
        public object Parameters { get; set; }
        public object Body { get; set; }
        public HttpStatusCode ExpectedStatusCode { get; set; }
        public object ExpectedResult { get; set; }
    }

    public class ApiTestCollection
    {
        public ApiTestCase[] TestCases { get; set; }
    }
}
