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
    public class EntityTypePage : LandingPage
    {
        #region Locators
        private By _btnEntityType(string type) => By.XPath($"//*[contains(@class, 'automation-id-entityType')]//mat-radio-button[.//*[normalize-space(text())='{type}']]");
        private By _btnEntityTypes => By.XPath("//*[contains(@class, 'automation-id-entityType')]//mat-radio-button");
        private By _lblQuestion => By.XPath("//*[contains(@class, 'automation-id-entityType')]//h1");
        #endregion

        #region Elements
        public IWebElement EntityTypeButton(string type) => StableFindElement(_btnEntityType(type));
        public ReadOnlyCollection<IWebElement> EntityTypeButtonList => StableFindElements(_btnEntityTypes);
        #endregion

        #region Business Methods
        public EntityTypePage() : base()
        {
            Url = "forms/business-entity-type";
            RequiredElementLocator = _lblQuestion;
        }

        [ExtentStepNode]
        public EntityTypePage SelectEntityType(string type)
        {
            GetLastNode().Info("Select entity type: " + type);
            EntityTypeButton(type).Click();
            WaitForElementEnabled(_btnNext);
            return this;
        }

        public BusinessInformationPage SelectNextButton()
        {
            return SelectNextButton<BusinessInformationPage>();
        }

        public DotNumberPage SelectPreviousButton()
        {
            return SelectPreviousButton<DotNumberPage>();
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public EntityTypePage ValidateEntityTypesDisplayed(string[] entityTypes)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(entityTypes, "Entity Types");
                foreach (var item in entityTypes)
                {
                    if (IsElementDisplayed(_btnEntityType(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateEntityTypesDisplayed, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateEntityTypesDisplayed, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEntityTypesDisplayed, e);
            }
            return this;
        }

        [ExtentStepNode]
        public EntityTypePage ValidateEntityTypePageDisplayed()
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
        public LandingPage VaIidateEntityTypesEditable()
        {
            var node = GetLastNode();
            try
            {
                if (EntityTypeButtonList.All(x => x.Enabled))
                {
                    SetPassValidation(node, ValidationMessage.ValidateEntityTypesEditable);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateEntityTypesEditable);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateEntityTypesEditable, e);
            }
            return this;
        }

        public EntityTypePage ValidateNextButtonEnabled()
        {
            return ValidateNextButtonEnabled<EntityTypePage>();
        }

        public EntityTypePage VaIidateNextButtonDisplayedAndDisabIed()
        {
            return ValidateNextButtonDisplayedAndDisabled<EntityTypePage>();
        }

        public EntityTypePage VaIidateNextButtonDisplayedAndEnabled()
        {
            return ValidateNextButtonDisplayedAndEnabled<EntityTypePage>();
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Entity Type page is displayed";
            public static string ValidateEntityTypesDisplayed = "Validate Entity Type buttons are displayed";
            public static string ValidateEntityTypeIsSelected = "Validate Entity Type button is selected";
            public static string ValidateEntityTypesEditable = "Validate Entity Type buttons can be edited";
        }
        #endregion
    }
}
