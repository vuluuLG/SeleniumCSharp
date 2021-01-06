namespace Framework.Test.UI.DataObject
{
    public class PolicyLevelVehicleInformation
    {
        /// <summary>
        /// The maximum radius travaled for your customer's business
        /// </summary>
        public string Radius { get; set; }
        /// <summary>
        /// Do any of your customer's vehicles travel across state lines?
        /// <para/>Yes/No
        /// </summary>
        public string Interstate { get; set; }
        /// <summary>
        /// The garaging zip code of your customer's vehicles
        /// </summary>
        public string GaragingZipCode { get; set; }
        /// <summary>
        /// Are your customer's employees covered by workers' compensation insurance?
        /// <para/>Yes/No
        /// </summary>
        public string WorkersCompensation { get; set; }
        /// <summary>
        /// Is the business contracted with any transportation network companies (Uber, Lyft, etc)?
        /// <para/>Yes/No
        /// </summary>
        public string Transportation { get; set; }
        /// <summary>
        /// Does the bisuness conduct repossessions more than 5% of the time?
        /// <para/>Yes/No
        /// </summary>
        public string Repossession { get; set; }
        /// <summary>
        /// Are the vehicles primarily oerated by employees?
        /// <para/>Yes/No
        /// </summary>
        public string PrimarilyOperate { get; set; }
        /// <summary>
        /// Where are the vehicles garaged?
        /// </summary>
        public Address GaragingAddress { get; set; }
        /// <summary>
        /// Is customer Owner/Operator?
        /// <para/>Yes/No
        /// </summary>
        public string OwnerOperator { get; set; }
        /// <summary>
        /// Add Carrier AI for Owner/Operator
        /// <para/>Yes/No
        /// </summary>
        public string CarrierAI { get; set; }
        /// <summary>
        /// Enable this flag if you make sure the Vehicle Suggestion screen will be displayed
        /// </summary>
        public bool IsValidVehicleData { get; set; }
    }
}
