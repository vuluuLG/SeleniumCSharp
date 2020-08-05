using System.ComponentModel;

namespace Framework.Test.Common.DriverWrapper
{
    public enum DriverType
    {
        [Description("chrome")]
        Chrome,
        [Description("firefox")]
        Firefox,
        [Description("internetexplorer")]
        InternetExplorer
    }
}