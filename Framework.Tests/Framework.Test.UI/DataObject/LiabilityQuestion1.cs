namespace Framework.Test.UI.DataObject
{
    public class LiabilityQuestion1
    {
        /// <summary>
        /// Customer's Gross Receipts last year
        /// </summary>
        public string GrossReciptsLastYear { get; set; }
        /// <summary>
        /// Customer's anticipated Gross Receipts for the coming year
        /// </summary>
        public string GrossReciptsNextYear { get; set; }
        /// <summary>
        /// Cost of hire
        /// </summary>
        public string CostOfHire { get; set; }
        /// <summary>
        /// The primary use of hired car autos
        /// </summary>
        public string PrimaryUseHiredAuto { get; set; }
        /// <summary>
        /// Does your customer plan to use any vehicle in their business that will not be scheduled in the next year?
        /// <para/>Yes/No
        /// </summary>
        public string PlanToUseUnscheduledVehicleNextYear { get; set; }
    }
}
