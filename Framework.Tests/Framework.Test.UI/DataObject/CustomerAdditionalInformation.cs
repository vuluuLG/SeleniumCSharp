namespace Framework.Test.UI.DataObject
{
    public class CustomerAdditionalInformation
    {
        /// <summary>
        /// How long has your customer had their own continues insurance coverage in the current business name?
        /// </summary>
        public string BusinessDateRange { get; set; }
        /// <summary>
        /// The filings required for your customer
        /// </summary>
        public string FilingType { get; set; }
        /// <summary>
        /// The liability claims that your customer has had in the past 3 years
        /// </summary>
        public string LiabilityLosses { get; set; }
    }
}
