using Framework.Test.Common.DriverWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.Common.DataObject
{
    public class EnvironmentSetting
    {
        public string EnvironmentName { get; set; }
        public string BrowserName { get; set; }
        public string WebUrl { get; set; }
        public UserAccount[] UserAccounts { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string ResultLocation { get; set; }
        public Dictionary<string, ServiceSetting> Services { get; set; }
        public DriverProperty DriverProperty { get; set; }
    }

    public class ServiceSetting
    {
        public string Host { get; set; }
        public string ResourceAddress { get; set; }
        public string SubscriptionKey { get; set; }
    }
}
