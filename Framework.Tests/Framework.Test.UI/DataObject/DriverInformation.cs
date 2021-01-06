namespace Framework.Test.UI.DataObject
{
    public class DriverInformation
    {
        /// <summary>
        /// Enable this flag if driver is Primary Officer
        /// </summary>
        public bool IsPO { get; set; }
        public OrderMVRStatus MVRStatus { get; set; }
        public FullName FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseState { get; set; }
        public bool DoesNotDrive { get; set; }
        /// <summary>
        /// Commercial Driver's License
        /// </summary>
        public CDLExperience CDLExperience { get; set; }
        public AccidentsAndViolations AccidentsAndViolations { get; set; }
        public Conviction Conviction { get; set; }
        public bool IsEditFlow { get; set; }
    }
}
