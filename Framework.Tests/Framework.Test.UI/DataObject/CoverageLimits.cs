using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Framework.Test.UI.DataObject
{
    /// <summary> 
    /// Data object for Coverage Limits
    /// </summary>
    public class CoverageLimits
    {
        public bool Recalculate { get; set; }
        [Description(CoverageLimitsType.LiabilityLimitType)]
        public string LiabilityLimitType { get; set; }
        [Description(CoverageLimitsType.LiabilityCslLimit)]
        public string LiabilityCslLimit { get; set; }
        [Description(CoverageLimitsType.LiabilitySplitLimit)]
        public string LiabilitySplitLimit { get; set; }
        [Description(CoverageLimitsType.LiabilitySplitBipdLimit)]
        public string LiabilitySplitBipdLimit { get; set; }
        [Description(CoverageLimitsType.UMLimitType)]
        public string UMLimitType { get; set; }
        [Description(CoverageLimitsType.UMCslLimit)]
        public string UMCslLimit { get; set; }
        [Description(CoverageLimitsType.UMSplitLimit)]
        public string UMSplitLimit { get; set; }
        [Description(CoverageLimitsType.UMSplitBipdLimit)]
        public string UMSplitBipdLimit { get; set; }
        [Description(CoverageLimitsType.UMDeductible)]
        public string UMDeductible { get; set; }
        [Description(CoverageLimitsType.UMPD)]
        public bool? UMPD { get; set; }
        [Description(CoverageLimitsType.WaiveCollisionDeductible)]
        public bool? WaiveCollisionDeductible { get; set; }
        [Description(CoverageLimitsType.UIMLimitType)]
        public string UIMLimitType { get; set; }
        [Description(CoverageLimitsType.UIMCslLimit)]
        public string UIMCslLimit { get; set; }
        [Description(CoverageLimitsType.UIMSplitLimit)]
        public string UIMSplitLimit { get; set; }
        [Description(CoverageLimitsType.UIMSplitBipdLimit)]
        public string UIMSplitBipdLimit { get; set; }
        [Description(CoverageLimitsType.UMUIMLimitType)]
        public string UMUIMLimitType { get; set; }
        [Description(CoverageLimitsType.UMUIMCslLimit)]
        public string UMUIMCslLimit { get; set; }
        [Description(CoverageLimitsType.UMUIMSplitLimit)]
        public string UMUIMSplitLimit { get; set; }
        [Description(CoverageLimitsType.UMUIMSplitBipdLimit)]
        public string UMUIMSplitBipdLimit { get; set; }
        [Description(CoverageLimitsType.UMUIMDeductible)]
        public string UMUIMDeductible { get; set; }
        [Description(CoverageLimitsType.AddOnCoverage)]
        public bool? AddOnCoverage { get; set; }
        [Description(CoverageLimitsType.MedicalExpenseBenefitsLimit)]
        public string MedicalExpenseBenefitsLimit { get; set; }
        [Description(CoverageLimitsType.IncomeLossBenefits)]
        public bool? IncomeLossBenefits { get; set; }
        [Description(CoverageLimitsType.MedicalPaymentsLimit)]
        public string MedicalPaymentsLimit { get; set; }
        [Description(CoverageLimitsType.PersonalInjuryProtection)]
        public string PersonalInjuryProtection { get; set; }
        [Description(CoverageLimitsType.PedestrianPIP)]
        public string PedestrianPIP { get; set; }
    }

    /// <summary>
    /// List of coverage limit controls
    /// </summary>
    public static class CoverageLimitsType
    {
        public const string LiabilityLimitType = "Liability Limit Type";
        public const string LiabilityCslLimit = "Liability Csl Limit";
        public const string LiabilitySplitLimit = "Liability Split Limit";
        public const string LiabilitySplitBipdLimit = "Liability Split Bipd Limit";
        public const string UMLimitType = "UM Limit Type";
        public const string UMCslLimit = "UM Csl Limit";
        public const string UMSplitLimit = "UM Split Limit";
        public const string UMSplitBipdLimit = "UM Split Bipd Limit";
        public const string UMDeductible = "UM Deductible";
        public const string UMPD = "UMPD";
        public const string WaiveCollisionDeductible = "Waive Collision Deductible";
        public const string UIMLimitType = "UIM Limit Type";
        public const string UIMCslLimit = "UIM Csl Limit";
        public const string UIMSplitLimit = "UIM Split Limit";
        public const string UIMSplitBipdLimit = "UIM Split Bipd Limit";
        public const string UMUIMLimitType = "UM/UIM Limit Type";
        public const string UMUIMCslLimit = "UM/UIM Csl Limit";
        public const string UMUIMSplitLimit = "UM/UIM Split Limit";
        public const string UMUIMSplitBipdLimit = "UM/UIM Split Bipd Limit";
        public const string UMUIMDeductible = "UM/UIM Deductible";
        public const string AddOnCoverage = "Add On Coverage";
        public const string MedicalExpenseBenefitsLimit = "Medical Expense Benefits Limit";
        public const string IncomeLossBenefits = "Income Loss Benefits";
        public const string MedicalPaymentsLimit = "Medical Payments Limit";
        public const string PersonalInjuryProtection = "Personal Injury Protection";
        public const string PedestrianPIP = "Pedestrian PIP";
    }

    /// <summary>
    /// List of coverage premium controls
    /// </summary>
    public static class CoveragePremiumsType
    {
        public const string LiabilityPremium = "Liability Premium";
        public const string UMPremium = "UM Premium";
        public const string UMPDPremium = "UMPD Premium";
        public const string UIMPremium = "UIM Premium";
        public const string UMUIMPremium = "UM/UIM Premium";
        public const string MedicalExpenseBenefitsPremium = "Medical Expense Benefits Premium";
        public const string IncomeLossBenefitsPremium = "Income Loss Benefits Premium";
        public const string MedicalPaymentsPremium = "Medical Payments Premium";
        public const string PersonalInjuryProtectionPremium = "Personal Injury Protection Premium";
        public const string PedestrianPIP = "Pedestrian PIP Premium";
        public const string RentedVehicleLiabilityPremium = "Rented Vehicle Liability Premium";
        public const string RentedVehiclePhysicalDamagePremium = "Rented Vehicle Physical Damage Premium";
    }

    /// <summary>
    /// Contains information for all Coverage types
    /// </summary>
    public static class CoveragesType
    {
        [CoveragesDescription(DynamicLimitType = CoverageLimitsType.LiabilityLimitType)]
        public const string Liability = "Liability";
        [CoveragesDescription(FullName = "Uninsured Motorist", DynamicLimitType = CoverageLimitsType.UMLimitType)]
        public const string UM = "UM";
        public const string UMPD = "UMPD";
        [CoveragesDescription(FullName = "Underinsured Motorists", DynamicLimitType = CoverageLimitsType.UIMLimitType)]
        public const string UIM = "UIM";
        [CoveragesDescription(FullName = "Uninsured Motorist/Underinsured Motorists", DynamicLimitType = CoverageLimitsType.UMUIMLimitType)]
        public const string UMUIM = "UM/UIM";
        [CoveragesDescription(ShortName = "Medical Exp. Benefits")]
        public const string MedicalExpenseBenefits = "Medical Expense Benefits";
        public const string IncomeLossBenefits = "Income Loss Benefits";
        public const string MedicalPayments = "Medical Payments";
        [CoveragesDescription(ShortName = "PIP")]
        public const string PersonalInjuryProtection = "Personal Injury Protection";
        [CoveragesDescription(ShortName = "Ped. PIP")]
        public const string PedestrianPIP = "Pedestrian PIP";
        public const string RentedVehicleLiability = "Rented Vehicle Liability";
        public const string RentedVehiclePhysicalDamage = "Rented Vehicle Physical Damage";

        public static string GetShortName(string coverageName)
        {
            var attribute = coverageName.ToCoverageDescription();
            if (attribute != null && attribute.ShortName != null)
                return attribute.ShortName;
            return coverageName;
        }

        public static string GetFullName(string coverageName)
        {
            var attribute = coverageName.ToCoverageDescription();
            if (attribute != null && attribute.FullName != null)
                return attribute.FullName;
            return coverageName;
        }

        public static string GetDynamicLimitType(string coverageName)
        {
            var attribute = coverageName.ToCoverageDescription();
            if (attribute != null)
                return attribute.DynamicLimitType;
            return null;
        }

        private static CoveragesDescriptionAttribute ToCoverageDescription(this string coverageName)
        {
            var coverages = typeof(CoveragesType).GetFields().Where(x => x.GetValue(null).ToString() == coverageName);
            foreach (var item in coverages)
            {
                if (item.GetCustomAttribute<CoveragesDescriptionAttribute>() != null)
                {
                    return item.GetCustomAttribute<CoveragesDescriptionAttribute>();
                }
            }
            return null;
        }
    }

    public class CoveragesDescriptionAttribute : DescriptionAttribute
    {
        // Assign this when there is a short name version (used in left navigation menu)
        public string ShortName { get; set; }
        // Assign this when there is a more details name version (used in report content. Ex: Indication.pdf)
        public string FullName { get; set; }
        // Assign this when the displayed coverage name is base on the selected limit type 
        // Ex: Liability Csl = Liability (base coverage name) + Csl (limit type)
        public string DynamicLimitType { get; set; }
    }
}
