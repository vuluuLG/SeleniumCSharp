namespace Framework.Test.UI.DataObject
{
    public class LiabilityQuestion2
    {
        /// <summary>
        /// Type(s) of vehicles will be used in the business but not scheduled on the policy
        /// </summary>
        public string[] TypeOfUnscheduledVehicleUsed { get; set; }
        /// <summary>
        /// How are these additional units used in the course of your customer's business?
        /// </summary>
        public string[] HowVehicleUsed { get; set; }
        /// <summary>
        /// Will any of these vehicles be operated by an individual that is not an employee of your customer?
        /// <para/>Yes/No
        /// </summary>
        public string HasVehicleOperatedIndividual { get; set; }
    }
}
