namespace Framework.Test.UI.DataObject
{
    public class AdditionalInterests
    {
        /// <summary>
        /// Number of Designated Insureds
        /// </summary>
        public string DesignatedInsuredCount { get; set; }
        /// <summary>
        /// Number of Additional Insureds (Named)
        /// </summary>
        public string AdditionalNamedInsuredCount { get; set; }
        /// <summary>
        /// Number of Additional Insureds (Named) with Waiver of Subrogation
        /// </summary>
        public string NamedInsuredWaiverOfSubrogationCount { get; set; }
        /// <summary>
        /// Do you want a Blanket Additional Insured?
        /// <para/>Yes/No
        /// </summary>
        public string WantsBlanketAdditionalInsured { get; set; }
        /// <summary>
        /// For Blanket Additional Insured, number of Waivers of Subrogation
        /// </summary>
        public string BlanketWaiverOfSubrogationCount { get; set; }
    }
}
