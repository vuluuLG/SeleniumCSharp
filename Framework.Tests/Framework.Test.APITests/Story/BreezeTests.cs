using FluentAssertions;
using Framework.Test.APITests.DataObject;
using Framework.Test.APITests.DataSource;
using Framework.Test.APITests.Helper;
using Framework.Test.Common.Helper;
using Framework.Tests.API.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using static Framework.Test.Common.Helper.EnvironmentHelper;

namespace Framework.Test.APITests.Story
{
    [TestClass]
    public class BreezeTests : ApiTestBase
    {
        private void ExecuteFlowApiTestCase(FlowApiTestCase testItem, string quoteId = null)
        {
            ParameterValidator.ValidateNotNull(testItem, "Test Item");

            AssignTestName(testItem);

            var breezeApi = new BreezeApi(EnvironmentSetting, quoteId);

            foreach (var stepItem in testItem.TestCaseSteps)
            {
                // Given
                var parameters = stepItem.Parameters.ToJObject();
                string screenName = parameters.Item("screenName");
                var header = parameters.Item("header");
                // Generate dynamic data for budy
                var body = breezeApi.ConvertDynamicData(stepItem.Body);

                // When
                IRestResponse result = breezeApi.ExecuteBreezeAPI(screenName, body, header);

                // Then
                result.StatusCode.Should().Be(stepItem.ExpectedStatusCode);
                // Update dynamic Id from response content
                breezeApi.UpdateDynamicIdData(screenName, result.Content);
                // Generate dynamic data for expected result
                var expectedResult = breezeApi.ConvertDynamicData(stepItem.ExpectedResult);

                var expectedResponse = breezeApi.GetExpectedResponseObject(expectedResult);
                var actualResponse = breezeApi.GetActualResponseObject(result.Content);

                actualResponse.ShouldBeEquivalentTo(expectedResponse);
            }
        }

        [BreezeTestMethod]
        [FlowApiDataSource(TestData = "breezeApi.Vehicle.BodyType")]
        [TestCategory("Integration")]
        public void API_Breeze_Vehicle_BodyType(FlowApiTestCase testItem)
        {
            ExecuteFlowApiTestCase(testItem);
        }
    }
}
