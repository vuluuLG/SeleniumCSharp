namespace Framework.Test.UI.DataObject
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsSameAddress { get; set; }
        /// <summary>
        /// Enable this flag to handle Confirm Address dialog when the inputted address data is nit standard
        /// </summary>
        public bool DoesHandleConfirmAddressDialog { get; set; }
    }
}
