namespace Framework.Test.UI.DataObject
{
    public class CustomerInformation
    {
        public string EffectiveDate { get; set; }
        public string DotNumber { get; set; }
        public string EntityType { get; set; }
        /// <summary>
        /// Use this property when you need to enter/update customer info
        /// </summary>
        public BusinessInformation BusinessInformation { get; set; }
        /// <summary>
        /// Use this property when you need to verify the filled out customer info
        /// </summary>
        public BusinessInformation FilledOutBusinessInformation { get; set; }
        public CustomerAdditionalInformation CustomerAdditionalInformation { get; set; }
        public FullName POName { get; set; }
        public Address POAddress { get; set; }
    }
}
