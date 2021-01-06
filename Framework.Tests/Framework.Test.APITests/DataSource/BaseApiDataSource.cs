using Framework.Test.APITest.Helper;
using Framework.Test.APITests.DataObject;
using Framework.Test.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Framework.Test.APITests.DataSource
{
    /// <summary>
    /// Test data source for base API data driven tests.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class BaseApiDataSourceAttribute : Attribute, ITestDataSource
    {
        public string TestData { get; set; }

        public virtual IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            ParameterValidator.ValidateNotNull(methodInfo, "Method Info");

            if (data != null)
            {
                string name = methodInfo.Name;
                var baseApiTestCaseData = (BaseApiTestCase)data[0];
                if (baseApiTestCaseData.TestCaseNumber != null)
                {
                    name += $" (Test Case {baseApiTestCaseData.TestCaseNumber})";
                }
                if (baseApiTestCaseData.TestCaseDescription != null)
                {
                    name += $" (Description: {baseApiTestCaseData.TestCaseDescription})";
                }
                return string.Format(CultureInfo.CurrentCulture, name);
            }

            return null;
        }

        public T GetTestCollection<T>()
        {
            // Read test collection
            if (string.IsNullOrEmpty(TestData))
                throw new Exception("Please set the 'TestData' property for data source");
            return TestDataHelper.GetTestData<T>(TestData);
        }
    }
}
