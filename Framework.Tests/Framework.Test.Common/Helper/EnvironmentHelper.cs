using AventStack.ExtentReports.MarkupUtils;
using Flurl;
using Framework.Test.Common.DataObject;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

// XML format
//  <?xml version="1.0" encoding="utf-8"?>
//  <Environments>
//      <iso>
//          <Accounts>
//              <Account>
//                  <Email></Email>
//                  <Password></Password>
//                  <Username></Username>
//                  <Agent></Agent>
//              </Account>
//          </Accounts>
//          <DatabaseConnectionString></DatabaseConnectionString>
//          <BreezeSubscriptionKey></BreezeSubscriptionKey>
//          <NicoSubscriptionKey></NicoSubscriptionKey>
//      </iso>
//  </Environments>

namespace Framework.Test.Common.Helper
{
    public static class EnvironmentHelper
    {
        public static EnvironmentSetting EnvironmentSetting { get; set; }

        public static void LoadEnvironmentSetting(string path, string environment)
        {
            var content = FileHelper.ReadAllTextFromFile(path);
            XmlDocument data = new XmlDocument();
            data.LoadXml(content);
            var root = data.SelectSingleNode("/Environments/" + environment);
            if (root == null) throw new Exception($"There is no setting for enviroment '{environment}' in xml");

            var userAccounts = root.GetUserAccounts();
            var nicoSubscriptionKey = root.SelectSingleNode("NicoSubscriptionKey")?.InnerText;
            var breezeSubscriptionKey = root.SelectSingleNode("BreezeSubscriptionKey")?.InnerText;

            var configJson = FileHelper.ReadAllTextFromFile("testconfig.json");
            var testConfig = JObject.Parse(configJson);
            var webUrl = testConfig.GetBaseUrls(environment, "web").full;

            var servicesConfig = ((JObject)testConfig["services"])
                .Properties()
                .Select(x => x)
                .ToDictionary(x => x.Name,
                    e =>
                    {
                        var baseType = e.Value.SelectToken("baseType").ToString();
                        var subscriptionKey = "";

                        switch (baseType)
                        {
                            case "breeze":
                                subscriptionKey = breezeSubscriptionKey;
                                break;
                            case "nico":
                                subscriptionKey = nicoSubscriptionKey;
                                break;
                            default:
                                throw new Exception("Can't get subscript key for " + e.Name);
                        }

                        var (host, resourceAddress) = testConfig.GetServiceUrls(environment, baseType, e.Name);

                        return new ServiceSetting()
                        {
                            Host = host,
                            ResourceAddress = resourceAddress,
                            SubscriptionKey = subscriptionKey
                        };
                    }
                );

            EnvironmentSetting = new EnvironmentSetting()
            {
                EnvironmentName = environment,
                WebUrl = webUrl,
                UserAccounts = userAccounts,
                DatabaseConnectionString = root.SelectSingleNode("DatabaseConnectionString")?.InnerText,
                Services = servicesConfig
            };
        }

        private static (string host, string path, string full) GetBaseUrls(this JObject config,
            string environment,
            string baseType)
        {
            var urls = config.SelectToken($"environment.{environment}.baseUrls.{baseType}");
            var host = urls["host"].ToString();
            var path = urls["path"].ToString();
            return (host, path, host + path);
        }

        private static (string host, string path) GetServiceUrls(this JObject config,
            string environment,
            string baseType,
            string serviceName)
        {
            var (host, basePath, _) = config.GetBaseUrls(environment, baseType);
            var servicePath = config.SelectToken($"services.{serviceName}.path").ToString();
            var completePath = Url.Combine(basePath, servicePath);

            return (host, completePath);
        }

        private static UserAccount[] GetUserAccounts(this XmlNode node)
        {
            List<UserAccount> accounts = new List<UserAccount>();

            foreach (var item in node.SelectNodes("Accounts/Account").Cast<XmlNode>().ToArray())
            {
                accounts.Add(
                    new UserAccount
                    {
                        Email = item.SelectSingleNode("Email")?.InnerText,
                        Password = item.SelectSingleNode("Password")?.InnerText,
                        Username = item.SelectSingleNode("Username")?.InnerText,
                        Agent = item.SelectSingleNode("Agent")?.InnerText
                    }
                );
            }

            return accounts.ToArray();
        }

        public static IMarkup MarkupJsonString(this EnvironmentSetting setting)
        {
            string sensitiveMask = "********";
            string[] sensitiveList = { "Password", "DatabaseConnectionString", "SubscriptKey" };
            return setting.ConvertObjectToJson().MaskSensitiveJsonData(sensitiveList, sensitiveMask).MarkupJsonString();
        }
    }
}
