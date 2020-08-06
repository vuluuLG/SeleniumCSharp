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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseConnectionString { get; set; }
        public Dictionary<string, ServiceSetting> Services { get; set; }
    }

    public class ServiceSetting
    {
        public string Host { get; set; }
        public string ResoureAddress { get; set; }
        public string SubcriptionKey { get; set; }
    }
}
