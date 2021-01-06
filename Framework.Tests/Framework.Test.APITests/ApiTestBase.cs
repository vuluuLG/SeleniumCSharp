using AventStack.ExtentReports;
using Framework.Test.APITests.DataObject;
using Framework.Test.Common.Helper;
using Framework.Tests.API.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using static Framework.Test.Common.Helper.EnvironmentHelper;
using static Framework.Test.Common.Helper.ExtentReportsHelper;

namespace Framework.Test.APITests
{
    public class ApiTestBase
    {
        private const string defaultResultLocation = "C:\\temp\\TestResults\\";
        private readonly string ignoreFile = "ignore.testcases.json";
        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            if (context != null)
            {
                var environment = context.Properties["environment"]?.ToString();
                var resultLocation = context.Properties["resultLocation"]?.ToString() ?? defaultResultLocation;

                // Create result folder
                if (!Directory.Exists(resultLocation))
                    Directory.CreateDirectory(resultLocation);

                if (environment != null)
                {
                    string environmentPath = context.Properties["environmentSettingsPath"]?.ToString();
                    string tempEnvironmentPath = string.Format("{0}environment_settings_{1}.xml",
                                                            resultLocation, DateTime.Now.ToDateTimeString("yyyyMMddHHmmssffff"));

                    if (File.Exists(environmentPath))
                    {
                        FileHelper.CopyFile(environmentPath, tempEnvironmentPath);
                    }

                    LoadEnvironmentSetting(tempEnvironmentPath, environment);

                    // Load other settings from test.runsettings
                    EnvironmentSetting.BrowserName = context.Properties["browser"]?.ToString();
                    EnvironmentSetting.ResultLocation = resultLocation;
                    // Set database connection string from current environment setting
                    AzureStorageHelper.ConnectionString = EnvironmentSetting.DatabaseConnectionString;
                }
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Ignore all test cases in ignore list
            var ignoreList = JsonHelper.Default.GetPropertyObjectFromFile<string[]>(Path.Combine(Directory.GetCurrentDirectory(), ignoreFile), TestContext.FullyQualifiedTestClassName);
            if (ignoreList != null && ignoreList.Contains(TestContext.TestName))
                Assert.Inconclusive("Ignored test case");

            // Prepare system info
            List<KeyValuePair<string, string>> systemInfo = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Environment", EnvironmentSetting.EnvironmentName)
            };

            // Create report
            string reportPath = EnvironmentSetting.ResultLocation + Utils.GetRandomValue(TestContext.TestName) + ".html";
            CreateReport(reportPath, TestContext.TestName, systemInfo);
            // Create test
            test = LogTest(TestContext.TestName);

            // Log environment settings
            var node = CreateStepNode("Environment Settings");
            node.Info(EnvironmentSetting.MarkupJsonString());
            EndStepNode(node);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Inconclusive && lastReportPath != null)
            {
                TestContext.AddResultFile(lastReportPath);
            }
        }

        public static void LogException(Exception exception, string testName)
        {
            ParameterValidator.ValidateNotNull(exception, "Exception");

            // If AssertFailedException
            if (exception.ToString().Contains("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException"))
            {
                test.Fail(WebUtility.HtmlEncode(testName + "Failed - " + exception.Message));
            }
            else // If other exception
            {
                string errorMessage = testName + " Got Exception During Execution - " + exception.ToString();

                if (nodeList.LastOrDefault() != null)
                {
                    nodeList.LastOrDefault().Error(WebUtility.HtmlEncode(errorMessage));
                }
                test.Error(WebUtility.HtmlEncode(errorMessage));
            }
        }

        public void AssignTestName(BaseApiTestCase testItem)
        {
            ParameterValidator.ValidateNotNull(testItem, "Test Item");

            string name = TestContext.TestName;
            if (testItem.TestCaseNumber != null)
            {
                name += $" (Test Case {testItem.TestCaseNumber})";
            }
            if (testItem.TestCaseDescription != null)
            {
                name += $" (Description {testItem.TestCaseDescription})";
            }
            test.Model.Name = name;
        }
    }

    /// <summary>
    /// Attribute for custom Test Method that handled customized test report for each execution
    /// </summary>
    public class BreezeTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            bool success = true;

            TestResult[] testResults = base.Execute(testMethod);

            try
            {
                foreach (TestResult item in testResults.Where(x => x.Outcome == UnitTestOutcome.Failed))
                {
                    try
                    {
                        var exception = item.TestFailureException.InnerException ?? item.TestFailureException;
                        ApiTestBase.LogException(exception, test.Model.Name);
                    }
                    catch (Exception) { }

                    success = false;
                    break;
                }

                if (success)
                {
                    test.Pass(test.Model.Name + " Passed");
                }

                extent.AnalysisStrategy = AnalysisStrategy.Test;
                extent.Flush();
            }
            catch (Exception) { }

            return testResults;
        }
    }
}
