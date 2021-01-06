using Framework.Test.UI.Pages;
using Framework.Test.UI.DataObject;
using Framework.Test.Common.Helper;

namespace Framework.Test.UITests.SharedStep
{
    public static class Business
    {
        public static EffectiveDatePage EnterBusinessInformation(this BusinessClassificationPage businessClassificationPage, BusinessClassification businessClassification)
        {
            ParameterValidator.ValidateNotNull(businessClassificationPage, "BusinessClassificationPage");
            ParameterValidator.ValidateNotNull(businessClassification, "BusinessClassification");

            // Select business group
            TertiaryPage tertiaryPage = businessClassificationPage.SelectBusinessClassification(businessClassification.BusinessGroup).SelectNextButton();

            // Select business category and subcategory
            EffectiveDatePage effectiveDatePage;
            if (businessClassification.BusinessCategory != null)
            {
                effectiveDatePage = tertiaryPage.SelectBusinessCategory(businessClassification.BusinessCategory, businessClassification.BusinessSubCategory).SelectNextButton();
            }
            else
            {
                TertiarySearchPage tertiarySearchPage = tertiaryPage.SelectSearchAllLink();
                effectiveDatePage = tertiarySearchPage.SearchTertiary(businessClassification.BusinessSubCategory).SelectNextButton();
            }
            // Verification
            effectiveDatePage.ValidateEffectiveDatePageDisplayed();
            return effectiveDatePage;
        }
    }
}