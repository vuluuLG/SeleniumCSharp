using Framework.Test.Common.Helper;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class AdditionalCoveragesPage : LandingPage
    {
        #region Locators
        private By _lnkNoAdditionalCoverages => By.XPath("//*[contains(@class, 'automation-id-wantsNoAdditionalCoverages')]//span");
        private By _btnCoverages(string item) => By.XPath($"//*[contains(@class, 'automation-id-coverages')]//button[.//*[contains(text(),'{item}')]]");
        #endregion

        #region Elements
        public IWebElement NoAdditionalCoveragesLink => StableFindElement(_lnkNoAdditionalCoverages);
        public IWebElement CoveragesButton(string item) => StableFindElement(_btnCoverages(item));
        #endregion

        #region Business Methods
        public AdditionalCoveragesPage() : base()
        {
            Url = "forms/additional-coverages";
            RequiredElementLocator = _lnkNoAdditionalCoverages;
        }

        [ExtentStepNode]
        public AdditionalCoveragesPage SelectAdditionalCoverages(string itemName)
        {
            GetLastNode().Info("Select additional coverages: " + itemName);
            if (!CoveragesButton(itemName).IsButtonSelected())
            {
                CoveragesButton(itemName).Click();
                WaitForElementEnabled(_btnNext);
            }
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesPage DeselectAdditionaICoverages(string itemName)
        {
            GetLastNode().Info("Deselect additional coverages: " + itemName);
            if (CoveragesButton(itemName).IsButtonSelected())
            {
                CoveragesButton(itemName).Click();
                Wait(1);
            }
            return this;
        }

        [ExtentStepNode]
        public T SelectNoAdditionalCoverages<T>() where T : BasePage
        {
            NoAdditionalCoveragesLink.Click();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        public LiabilityQuestions1Page SelectNextButton()
        {
            return SelectNextButton<LiabilityQuestions1Page>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public AdditionalCoveragesPage ValidateAdditionalCoveragesPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsPageDisplayed())
                {
                    SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesPage ValidateAdditionalCoveragesListed(string[] additionalCoverages)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
                for (int i = 0; i < additionalCoverages.Length; i++)
                {
                    if (IsElementDisplayed(_btnCoverages(additionalCoverages[i])))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAdditionalCoveragesListed, additionalCoverages[i]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAdditionalCoveragesListed, additionalCoverages[i]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAdditionalCoveragesListed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public AdditionalCoveragesPage ValidateAdditionalCoveragesNotListed(string[] additionalCoverages)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(additionalCoverages, "Additional Coverages");
                for (int i = 0; i < additionalCoverages.Length; i++)
                {
                    if (IsElementDisplayed(_btnCoverages(additionalCoverages[i])))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateAdditionalCoveragesNotListed, additionalCoverages[i]);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateAdditionalCoveragesNotListed, additionalCoverages[i]);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateAdditionalCoveragesNotListed, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Additional Coverages page is displayed";
            public static string ValidateAdditionalCoveragesListed = "Validate Additional Coverages buttons are listed";
            public static string ValidateAdditionalCoveragesNotListed = "Validate Additional Coverages buttons are not listed";
        }
        #endregion
    }
}