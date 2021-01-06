using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class TertiarySearchPage : LandingPage
    {
        #region Locators
        private By _txtSearch => By.XPath("//input[contains(@placeholder, 'Search')]");
        private By _lblBusinessCategorization => By.XPath("//*[contains(@class,'business-classification')]//h4");
        #endregion

        #region Elements
        public IWebElement SearchTextBox => StableFindElement(_txtSearch);
        public ReadOnlyCollection<IWebElement> BusinessCategorizationLabelList => StableFindElements(_lblBusinessCategorization);
        #endregion

        #region Business Methods
        public TertiarySearchPage() : base()
        {
            Url = "forms/business-tertiary—search";
            RequiredElementLocator = _txtSearch;
        }

        [ExtentStepNode]
        public TertiarySearchPage SearchTertiary(string tertiary, string searchBusiness = null)
        {
            var node = GetLastNode();
            string searchKey = tertiary;
            if (searchBusiness != null) searchKey = searchBusiness;
            node.Info("Search key: " + searchKey);
            SearchTextBox.InputText(searchKey);
            node.Info("Select tertiary: " + tertiary);
            SearchTextBox.SelectByText(tertiary, false);
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public EffectiveDatePage SelectNextButton()
        {
            return SelectNextButton<EffectiveDatePage>();
        }

        public TertiaryPage SelectPreviousButton()
        {
            return SelectPreviousButton<TertiaryPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public TertiarySearchPage ValidateTertiarySearchPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidateTertiarySearchPageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTertiarySearchPageDisplayed);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTertiarySearchPageDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public TertiarySearchPage ValidateTertiarySelectionPathDisplayedCorrectly(string[] expectedPathList)
        {
            var node = GetLastNode();
            try
            {
                string[] actualPathList = BusinessCategorizationLabelList.Select(x => x.Text.Replace("\r\n", "").Replace("subdirectory_arrow_right", "")).ToArray();
                string actualSelectionPath = string.Join("—>", actualPathList);

                string expectedSelectionPath = string.Join("—>", expectedPathList);
                if (actualSelectionPath == expectedSelectionPath)
                {
                    SetPassValidation(node, ValidationMessage.ValidateTertiarySelectionPathDisplayedCorrectly, expectedValue: expectedSelectionPath);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTertiarySelectionPathDisplayedCorrectly, expectedValue: expectedSelectionPath, actualValue: actualSelectionPath);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTertiarySelectionPathDisplayedCorrectly, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidateTertiarySearchPageDisplayed = "Validate Tertiary Search page is displayed";
            public static string ValidateTertiarySelectionPathDisplayedCorrectly = "Validate Tertiary selection path is displayed correctly";
        }
        #endregion
    }
}