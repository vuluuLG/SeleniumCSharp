using System.Collections.Generic;

namespace Framework.Test.Common.DriverWrapper
{
    public class DriverProperty
    {
        #region Properties
        public DriverType DriverType { get; set; }
        // Local settings
        public string DriverVersion { get; set; }
        public bool Headless { get; set; }
        public string DownloadLocation { get; set; }
        public string Arguments { get; set; }
        // Remote settings
        public string RemoteUrl { get; set; }
        public Dictionary<string, object> Capabilities { get; set; }
        // Mobile emuation
        public string DeviceName { get; set; }
        public string CustomUserAgent { get; set; }
        public long CustomWidth { get; set; }
        public long CustomHeight { get; set; }
        public double CustomPixelRatio { get; set; }

        public string[] GetArgumentsAsArray()
        {
            if (Arguments != null)
            {
                return Arguments.Split(';');
            }
            else return null;
        }

        public string GetArgumentsAsString()
        {
            if (Arguments != null)
            {
                return Arguments.Replace(';', ' ');
            }
            else return "";
        }
        #endregion
    }
}