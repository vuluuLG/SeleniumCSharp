namespace Framework.Test.UI.DataObject
{
    public class LiabilityQuestion3
    {
        /// <summary>
        /// Does your customer own or operate any additional businesses or have any subsidiaries?
        /// <para/>Yes/No
        /// </summary>
        public string HasAdditionalBusinessesOrSubsidiaries { get; set; }
        /// <summary>
        /// Additional businesses or subsidiaries
        /// </summary>
        public string AdditionalBusinessesOrSubsidiaries { get; set; }
        /// <summary>
        /// Number of Scheduled Drivers are involved in your customer's operation
        /// </summary>
        public string NumberScheduledDriversInvolved { get; set; }
        /// <summary>
        /// Number of Non-Driving Employees are involved in your customer's operation
        /// </summary>
        public string NumberNonDrivingEmployeesInvolved { get; set; }
        /// <summary>
        /// Number of Independent Contractors are involved in your customer's operation
        /// </summary>
        public string NumberIndependentContractorsInvolved { get; set; }
        /// <summary>
        /// Number of Volunteers are involved in your customer's operation
        /// </summary>
        public string NumberVolunteersInvolved { get; set; }
        /// <summary>
        /// The anticipated annual mileage for vehicles not scheduled on the application
        /// </summary>
        public string AnticipatedAnnualMileage { get; set; }
    }
}
