using AventStack.ExtentReports;
using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI;
using Framework.Test.UI.DataObject;
using Framework.Test.UITests.TestData;
using MethodBoundaryAspect.Fody.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using static Framework.Test.Common.Helper.EnvironmentHelper;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.Common.Helper.ValidationHelper;

namespace Framework.Test.UITests
{
    public class UITestBase
    {
        private const string defaultResultLocation = "C:\\temp\\TestResults\\";
        private readonly string ignoreFile = "ignore.testcases.json";
        public TestContext TestContext { get; set; }

        [ThreadStatic]
        public static string errorImagePath;

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
                    EnvironmentSetting.DriverProperty = new DriverProperty
                    {
                        DriverType = EnvironmentSetting.BrowserName.ToEnumValue<DriverType>(),
                        DriverVersion = context.Properties["driverVersion"]?.ToString(),
                        Arguments = context.Properties["arguments"]?.ToString(),
                        Headless = context.Properties["headless"]?.ToType<bool>("headless") ?? false,
                        DownloadLocation = context.Properties["downloadLocation"]?.ToString(),
                        DeviceName = context.Properties["deviceName"]?.ToString(),
                        CustomUserAgent = context.Properties["customUserAgent"]?.ToString(),
                        CustomWidth = context.Properties["customWidth"]?.ToType<long>("customWidth") ?? 0,
                        CustomHeight = context.Properties["customHeight"]?.ToType<long>("customHeight") ?? 0,
                        CustomPixelRatio = context.Properties["customPixelRatio"]?.ToType<double>("customPixelRatio") ?? 0
                    };

                    // Get login credentials from environment settings
                    SharedTestData.Accounts = EnvironmentSetting.UserAccounts.Select(x => new Account(x)).ToArray();
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
                new KeyValuePair<string, string>("Environment", EnvironmentSetting.EnvironmentName),
                new KeyValuePair<string, string>("Browser", EnvironmentSetting.BrowserName)
            };

            // Create report
            string reportPath = EnvironmentSetting.ResultLocation + Utils.GetRandomValue(TestContext.TestName) + ".html";
            CreateReport(reportPath, TestContext.TestName, systemInfo);

            // Create pre-condition step
            test = LogTest("Pre-condition");

            // Log environment settings
            var node = CreateStepNode("Envinronment Settings");
            node.Info(EnvironmentSetting.MarkupJsonString());
            EndStepNode(node);

            // Init web driver
            WebDriver.InitDriverManager(EnvironmentSetting.DriverProperty);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Inconclusive)
            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                    ScreenshotHelper.TakeScreenshot(out errorImagePath, null, EnvironmentSetting.ResultLocation);

                if (lastReportPath != null)
                    TestContext.AddResultFile(lastReportPath);

                Browser.QuitAll();
            }
        }

        public static void LogSummaryException(Exception exception, string testName)
        {
            ParameterValidator.ValidateNotNull(exception, "Exception");

            // If AssertFailedException
            if (exception.ToString().Contains("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException"))
            {
                test.Fail(WebUtility.HtmlEncode(testName + "Failed - " + exception.Message));
            }
            else // If other exception
            {
                var attactment = errorImagePath != null ? AttachScreenshot(errorImagePath) : null;
                test.Error(WebUtility.HtmlEncode(testName + " Got Exception During Execution - " + exception.ToString()), attactment);
            }
        }

        public static void LogException(Exception exception, string testName)
        {
            ParameterValidator.ValidateNotNull(exception, "Exception");

            // Only handle for unexpected exception
            if (!exception.ToString().Contains("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException"))
            {
                string errorMessage = testName + " Got Exception During Execution - " + exception.ToString();
                var attactment = errorImagePath != null ? AttachScreenshot(errorImagePath) : null;

                if (nodeList.LastOrDefault() != null)
                {
                    nodeList.LastOrDefault().Error(WebUtility.HtmlEncode(errorMessage), attactment);
                }
                else
                {
                    test.Error(WebUtility.HtmlEncode(errorMessage), attactment);
                }
            }
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
                if (testMethod != null)
                {
                    Exception exception = null;
                    foreach (TestResult item in testResults.Where(x => x.Outcome == UnitTestOutcome.Failed))
                    {
                        try
                        {
                            exception = item.TestFailureException.InnerException ?? item.TestFailureException;
                            // Handle unexpected exception for the last test
                            UITestBase.LogException(exception, testMethod.TestMethodName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Get TestFailureException encountered an error. " + e.Message);
                        }

                        success = false;
                        break;
                    }

                    // Create summary test
                    test = extent.CreateTest("Test Summary");

                    if (success)
                    {
                        test.Pass(testMethod.TestMethodName + " Passed");
                    }
                    else
                    {
                        UITestBase.LogSummaryException(exception, testMethod.TestMethodName);
                    }

                    // Log all verifications
                    if (validations != null)
                    {
                        foreach (var item in validations)
                        {
                            var status = Status.Error;
                            if (item.Value == true)
                            {
                                status = Status.Pass;
                            }
                            else if (item.Value == false)
                            {
                                status = Status.Fail;
                            }

                            test.Log(status, item.Key);
                        }
                    }

                    extent.AnalysisStrategy = AnalysisStrategy.Test;
                    extent.Flush();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Analyze Test result encountered an error. " + e.Message);
            }

            return testResults;
        }
    }

    /// <summary>
    /// The Test Validation attribute.
    ///     Automatically handle creating test node for test case and calling assertion at the end
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public sealed class TestValidationAttribute : OnMethodBoundaryAspect
    {
        public string Description { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            validations = new List<KeyValuePair<string, bool?>>();

            string description = Description;

            if (args != null)
            {
                if (args.Arguments.Length > 0)
                {
                    foreach (var item in args.Arguments)
                    {
                        if (item is int)
                        {
                            description += $" (Test iteration {item})";
                            break;
                        }
                    }
                }
            }

            LogTest(description);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            try
            {
                AssertAll();
            }
            catch (AssertFailedException)
            {
                throw new AssertFailedException("Expected all checkpoints passed but there are following failed checkpoints: " + string.Join(", ", GetFailedValidations()));
            }
        }
    }
}
