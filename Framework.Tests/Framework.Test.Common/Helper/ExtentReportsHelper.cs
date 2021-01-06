using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using MethodBoundaryAspect.Fody.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Framework.Test.Common.Helper
{
    public static class ExtentReportsHelper
    {
        [ThreadStatic]
        public static ExtentReports extent;
        [ThreadStatic]
        public static ExtentTest test;
        [ThreadStatic]
        public static List<ExtentTest> extentTestList;
        [ThreadStatic]
        public static List<ExtentTest> nodeList;
        [ThreadStatic]
        public static string lastReportPath;

        public static ExtentReports CreateReport(string reportPath, string reportName, List<KeyValuePair<string, string>> systemInfo = null)
        {
            var isNew = false;

            if (extent == null)
            {
                System.IO.File.Create(reportPath).Dispose();
                isNew = true;
            }
            else
            {
                var reporterList = extent.StartedReporterList;
                if (Utils.GetPropertyValue(reporterList.Last(), "Config.DocumentTitle").ToString() != reportName)
                {
                    isNew = true;
                }
            }

            if (isNew)
            {
                extent = new ExtentReports();
                lastReportPath = reportPath;
                if (systemInfo != null)
                {
                    foreach (var item in systemInfo)
                    {
                        extent.AddSystemInfo(item.Key, item.Value);
                    }
                }
                var htmlReporter = new ExtentV3HtmlReporter(lastReportPath);
                //htmlReporter.LoadConfig(Utils.GetProjectPath() + "extent-config.xml");
                htmlReporter.Config.ReportName = reportName;
                htmlReporter.Config.DocumentTitle = reportName;
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                extent.AttachReporter(htmlReporter);
            }

            extentTestList = new List<ExtentTest>();
            nodeList = new List<ExtentTest>();
            return extent;
        }

        public static ExtentTest LogTest(string testDetail, string testDescription = null)
        {
            test = extent.CreateTest(testDetail, testDescription);
            nodeList = new List<ExtentTest>();
            if (!extentTestList.Contains(test))
            {
                extentTestList.Add(test);
            }
            return test;
        }

        public static ExtentTest CreateStepNode([CallerMemberName]string memberName = "")
        {
            ExtentTest node;
            if (nodeList.Count ==  0)
            {
                node = extentTestList.LastOrDefault().CreateNode(memberName);
                nodeList.Add(node);
            }
            else
            {
                node = extentTestList.LastOrDefault().CreateNode(memberName);
                if (!nodeList.Contains(node))
                    nodeList.Add(node);
            }
            return node;
        }

        public static void EndStepNode(ExtentTest node)
        {
            if (nodeList.ElementAt(0) == node)
            {
                nodeList = new List<ExtentTest>();
            }
            else
            {
                nodeList.RemoveAt(nodeList.Count - 1);
            }
        }

        public static ExtentTest GetLastNode()
        {
            if (nodeList.Count != 0)
            {
                return nodeList[nodeList.Count - 1];
            }
            else return null;
        }

        public static MediaEntityModelProvider AttachScreenshot(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return null;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(ScreenshotHelper.ImageToBase64(imagePath)).Build();
        }

        public static void LogDataInfo(this ExtentTest node, object data)
        {
            if (node != null)
            {
                if (data != null)
                {
                    node.Info("Data: " + data.GetType().Name);
                    node.Info(data.ConvertObjectToJson().MarkupJsonString());
                }
                else
                {
                    node.Info("Data: null");
                }
            }
        }

        public static IMarkup MarkupJsonString(this string data)
        {
            return MarkupHelper.CreateCodeBlock(data, CodeLanguage.Json);
        }

        public static string MarkupTestCategory(string category)
        {
            if (string.IsNullOrEmpty(category)) return category;
            return category.Replace(" ", "_");
        }
    }

    /// <summary>
    /// The ExtentTest step node attribute.
    ///     Automaticaly handle creating and releasing step node
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public sealed class ExtentStepNodeAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args != null)
            {
                string nodeName = args.Method.Name;
                if (args.Method.GetParameters().Length > 0
                    && (args.Method.GetParameters()[0].Name == "controlName"
                        || args.Method.GetParameters()[0].Name == "itemName"))
                {
                    if (args.Method.GetParameters().Length > 1 && args.Method.GetParameters()[1].Name == "controlType")
                    {
                        Type eNumType = args.Method.GetParameters()[1].ParameterType;
                        nodeName += $" ({args.Arguments[0]} {Enum.GetName(eNumType, args.Arguments[1]).ToLower()})";
                    }
                    else
                    {
                        nodeName += $" ({args.Arguments[0]})";
                    }
                }

                if (nodeName.StartsWith("Validate"))
                {
                    nodeName = $"<span style='color:#00CCFF'>{nodeName}</span>";
                }

                ExtentReportsHelper.CreateStepNode(nodeName);
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args != null && ExtentReportsHelper.GetLastNode() != null)
            {
                ExtentReportsHelper.EndStepNode(ExtentReportsHelper.GetLastNode());
            }
        }
    }
}
