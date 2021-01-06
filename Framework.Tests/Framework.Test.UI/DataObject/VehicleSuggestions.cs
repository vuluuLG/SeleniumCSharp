namespace Framework.Test.UI.DataObject
{
    public class VehicleSuggestions
    {
        /// <summary>
        /// Suggested vehicle list on Vehicle Suggestion screen
        /// </summary>
        public string[] SuggestedVehicles { get; set; }
        /// <summary>
        /// Vehicle list (name) that you want to select
        /// </summary>
        public string[] SelectedVehicles { get; set; }
        /// <summary>
        /// Vehicle list (index - start from 1) that you want to select
        /// </summary>
        public int[] SelectedVehiclesIndex { get; set; }
    }
}
