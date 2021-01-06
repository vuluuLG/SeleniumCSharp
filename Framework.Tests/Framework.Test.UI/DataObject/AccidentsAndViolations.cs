namespace Framework.Test.UI.DataObject
{
    public class AccidentsAndViolations
    {
        /// <summary>
        /// Has the driver had any at fault accidents or moving violations in the last 3 years?
        /// <para/>Yes/No
        /// </summary>
        public string HasAccidentsOrViolations { get; set; }
        /// <summary>
        /// Number of fault accidents
        /// </summary>
        public string NumberAccidents { get; set; }
        /// <summary>
        /// Number of moving violations
        /// </summary>
        public string NumberViolations { get; set; }
    }
}
