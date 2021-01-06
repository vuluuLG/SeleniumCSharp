using Framework.Test.APITests.DataObject;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Test.APITests.DataSource
{
    /// <summary>
    /// Test data source for single API data driven tests
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ApiDataSourceAttribute : BaseApiDataSourceAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var testCollection = GetTestCollection<ApiTestCollection>();
            foreach (var item in testCollection.TestCases)
            {
                yield return new object[] { item };
            }
        }
    }
}
