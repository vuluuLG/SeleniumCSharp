using Framework.Test.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Framework.Test.UITests.DataSource
{
    /// <summary>
    /// Test data source for basic UI data driven tests.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class UIDataSourceAttribute : Attribute, ITestDataSource
    {
        public string DataClassName { get; set; }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            if (string.IsNullOrEmpty(DataClassName))
                throw new Exception("Please set the 'DataClassName' property for data source");

            dynamic data = Activator.CreateInstance(Type.GetType("Breeze.Test.UITests.TestData." + DataClassName));

            for (int i = 0; i < data.testDataList.Length; i++)
            {
                yield return new object[] { i + 1, data.testDataList[i] };
            }
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            ParameterValidator.ValidateNotNull(methodInfo, "method Info");

            if (data != null)
                return string.Format(CultureInfo.CurrentCulture, "{0} (Test iteration {1})", methodInfo.Name, data[0]);

            return null;
        }
    }
}
