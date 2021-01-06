namespace Framework.Test.UI.DataObject
{
    public class VehicleInformation
    {
        public string VIN { get; set; }
        public VinStatus VinStatus { get; set; }
        public string BodyType { get; set; }
        public string SeatCap { get; set; }
        public string GVW { get; set; }
        public dynamic[] ClassificationFlow { get; set; }
        public dynamic[] FactorFlow { get; set; }
        public dynamic[] ClassificationOverride { get; set; }
        public VehicleDescription VehicleDescription { get; set; }
        public PhysicalDamage PhysicalDamage { get; set; }
    }
}
