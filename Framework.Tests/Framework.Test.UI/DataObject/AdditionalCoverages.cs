using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Framework.Test.UI.DataObject
{
    /// <summary> 
    /// Data object for Additional Coverages
    /// </summary>
    public class AdditionalCoverages
    {
        public AdditionalInterests AdditionalInterests { get; set; }
        public HiredCarNonOwned HiredCarNonOwned { get; set; }
        public Cargo Cargo { get; set; }
        public TrailerInterchange TrailerInterchange { get; set; }
    }

    /// <summary>
    /// List of Additional Coverages types
    /// </summary>
    public static class AdditionalCoveragesType
    {
        public const string AdditionalInterests = "Additional Interests";
        [AdditionalCoveragesDescription(ShortName = "HC/NO")]
        public const string HiredCarNonOwned = "Hired Car/Non-Owned";
        public const string Cargo = "Cargo";
        public const string TrailerInterchange = "Trailer Interchange";

        public static string GetShortName(string coverageName)
        {
            var coverages = typeof(AdditionalCoveragesType).GetFields().Where(x => x.GetValue(null).ToString() == coverageName);
            foreach (var item in coverages)
            {
                if (item.GetCustomAttribute<AdditionalCoveragesDescriptionAttribute>() != null)
                {
                    return item.GetCustomAttribute<AdditionalCoveragesDescriptionAttribute>().ShortName;
                }
            }
            return coverageName;
        }
    }

    public class AdditionalCoveragesDescriptionAttribute : DescriptionAttribute
    {
        // Assign this when there is a short name version (used in left navigation menu)
        public string ShortName { get; set; }
    }

}
