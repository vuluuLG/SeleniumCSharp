namespace Framework.Test.UI.DataObject
{
    public class Quote
    {
        public BusinessClassification BusinessClassification { get; set; }
        public CustomerInformation CustomerInformation { get; set; }
        public PolicyLevelVehicleInformation PolicyLevelVehicleInformation { get; set; }
        public VehicleInformation[] VehicleList { get; set; }
        public DriverInformation[] DriverList { get; set; }
        public CoverageLimits CoverageLimits { get; set; }
        public AdditionalCoverages AdditionalCoverages { get; set; }
    }
}
