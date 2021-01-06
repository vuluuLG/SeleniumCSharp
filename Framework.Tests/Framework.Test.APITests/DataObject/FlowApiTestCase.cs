using System.Net;

namespace Framework.Test.APITests.DataObject
{
    /// <summary>
    /// Data object for API test case that has multiple steps/requests in order
    /// </summary>
    public class FlowApiTestCase : BaseApiTestCase
    {
        public ApiTestStep[] TestCaseSteps { get; set; }
    }

    public class ApiTestStep
    {
        public object Parameters { get; set; }
        public object Body { get; set; }
        public HttpStatusCode ExpectedStatusCode { get; set; }
        public object ExpectedResult { get; set; }
    }

    public class FlowApiTestCollection
    {
        public FlowApiTestCase[] TestCases { get; set; }
    }
}
