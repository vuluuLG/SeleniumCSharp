using FluentAssertions;
using Framework.Test.APITests.DataObject;
using Framework.Test.APITests.DataSource;
using Framework.Test.APITests.Helper;
using Framework.Test.Common.Helper;
using Framework.Tests.API.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Framework.Test.Common.Helper.EnvironmentHelper;

namespace Framework.Test.APITests.Story
{
    [TestClass]
    public class ZipTerritoryTests : ApiTestBase
    {
        private void ExecuteZipTerritoryAPITestCase(ApiTestCase testItem)
        {
            ParameterValidator.ValidateNotNull(testItem, "Test Item");

            AssignTestName(testItem);

            // Given
            var zipTerritoryApi = new ZipTerritoryApi(EnvironmentSetting);
            var parameters = Utils.ConvertDynamicData(testItem.Parameters).ToJObject();

            string zipCode = parameters.Item("zipCode");
            string companyNumber = parameters.Item("companyNumber");
            string effectiveDate = parameters.Item("effectiveDate");

            // When
            var result = zipTerritoryApi.GetTerritory(zipCode, companyNumber, effectiveDate);

            // Then
            result.StatusCode.Should().Be(testItem.ExpectedStatusCode);
            result.Content.ShouldBeEquivalentTo(testItem.ExpectedResult.ToString());
        }

        [BreezeTestMethod]
        [ApiDataSource(TestData = "zipTerritoryApi.GetTerritoryWithValidZipCode")]
        [TestCategory("Integration")]
        public void API_ZipTerritory_Get_Valid_ZipCode(ApiTestCase testItem)
        {
            ExecuteZipTerritoryAPITestCase(testItem);
        }

        [BreezeTestMethod]
        [ApiDataSource(TestData = "zipTerritoryApi.GetTerritoryWithInvalidZipCode")]
        [TestCategory("Integration")]
        public void API_ZipTerritory_Get_Invalid_ZipCode(ApiTestCase testItem)
        {
            ExecuteZipTerritoryAPITestCase(testItem);
        }
    }
}
