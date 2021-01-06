using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class TertiaryPage : LandingPage
    {
        #region Locators
        private By _lblQuestion => By.XPath("//forml/h1");
        private By _eleTertiaryGroup => By.XPath("//div[@class='tertiary-form']");
        private By _btnCategory(string item) => By.XPath($"(//div[@class='tertiary-formj/lmat-radio-group)[1]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        private By _btnCategories => By.XPath("(//div[@class='tertiary-formj/lmat-radio-group[contains(@class,'vertical')])[1]//mat-radio-button");
        private By _btnSubCategory(string item) => By.XPath($"(//div[@class='tertiary-form']//mat-radio-group)[2]//mat-radio-buttori[.//*[normalize-space(text())='{item}']]");
        private By _lnkSubCategory => By.XPath("//button[contains(@class, 'link-button')]//span[text()='Search all business categories']");
        #endregion

        #region Elements
        public IWebElement QuestionLable => StableFindElement(_lblQuestion);
        public IWebElement CategoryButton(string item) => StableFindElement(_btnCategory(item));
        public IWebElement SubCategoryButton(string item) => StableFindElement(_btnSubCategory(item));
        public IWebElement SubCategoryLink => StableFindElement(_lnkSubCategory);
        #endregion

        #region Business Methods
        public TertiaryPage() : base()
        {
            Url = "forms/business-tertiary";
            RequiredElementLocator = _eleTertiaryGroup;
        }

        [ExtentStepNode]
        public TertiaryPage SelectBusinessCategory(string category, string subCategory)
        {
            var node = GetLastNode();
            node.Info("Question: " + QuestionLable.Text);
            node.Info("Select category: " + category);
            node.Info("Select subcategory: " + subCategory);
            CategoryButton(category).ScrollIntoViewAndClick();
            WaitForElementExists(_btnSubCategory(subCategory), throwException: false);
            SubCategoryButton(subCategory).ScrollIntoViewAndClick();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        [ExtentStepNode]
        public TertiarySearchPage SelectSearchAllLink()
        {
            SubCategoryLink.Click();
            var page = new TertiarySearchPage();
            page.WaitForPageLoad();
            return page;
        }

        public EffectiveDatePage SelectNextButton()
        {
            return SelectNextButton<EffectiveDatePage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public TertiaryPage ValidateTertiaryPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidateTertiaryPageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTertiaryPageDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTertiaryPageDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public TertiaryPage ValidateBusinessCategoryButtonsDisplayedVertically()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_btnCategories))
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessCategoryButtonsDisplayedVertically);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessCategoryButtonsDisplayedVertically);
                }
            }

            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessCategoryButtonsDisplayedVertically, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidateTertiaryPageDisplayed = "Validate Tertiary page is displayed";
            public static string ValidateBusinessCategoryButtonsDisplayedVertically = "Validate Business categories are displayed as buttons vertically";
        }
        #endregion
    }
}