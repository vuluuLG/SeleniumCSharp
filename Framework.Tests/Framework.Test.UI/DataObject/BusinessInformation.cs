namespace Framework.Test.UI.DataObject
{
    public class BusinessInformation
    {
        /// <summary>
        /// Assign when business entoty type is nit Individual
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Assign only when bisuness entity type is Individual
        /// </summary>
        public FullName CustomerFullName { get; set; }
        public string DBA { get; set; }
        public Address Address { get; set; }
    }
}
