using Framework.Test.APITests.DataObject;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Test.APITests.DataSource
{
    /// <summary>
    /// Test data source for flow API data driven tests
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class FlowApiDataSourceAttribute : BaseApiDataSourceAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var testCollection = GetTestCollection<FlowApiTestCollection>();
            foreach (var item in testCollection.TestCases)
            {
                yield return new object[] { item };
            }
        }
    }
}
