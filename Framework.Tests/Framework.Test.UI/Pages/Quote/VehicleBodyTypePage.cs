using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleBodyTypePage : LandingPage
    {
        #region Locators
        private By _eleBodyTypeGroupTab(string item) => By.XPath($"//div[@role='tab' and .='{item}']");
        private By _btnBodyType(string item) => By.XPath($"//*[contains(@class, 'automation-id-bodyType')]//mat-radio-button[.//*[normalize-space(text())='{item}']]");
        #endregion

        #region Elements
        public IWebElement BodyTypeGroupTab(string item) => StableFindElement(_eleBodyTypeGroupTab(item));
        public IWebElement BodyTypeButton(string item) => StableFindElement(_btnBodyType(item));
        #endregion

        #region Business Methods
        public VehicleBodyTypePage() : base()
        {
            Url = "forms/body-type";
            RequiredElementLocator = _eleBodyTypeGroupTab("All");
        }

        [ExtentStepNode]
        public VehicleBodyTypePage SelectBodyType(string bodyType)
        {
            GetLastNode().Info("Select body type: " + bodyType);
            if (!IsElementDisplayed(_btnBodyType(bodyType)))
            {
                BodyTypeGroupTab("All").Click();
                // Try to scroll to target body type
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                do
                {
                    try
                    {
                        BodyTypeButton(bodyType).ScrollIntoView();
                        // Wait 15 to make sure scolling complete
                        Wait(1);
                        BodyTypeButton(bodyType).ScrollIntoViewAndClick();
                        break;
                    }
                    catch (WebDriverException ex)
                    {
                        // Sometimes the connection was closed by the server when finding body type button after scrolling
                        // Ignore the exception and try again
                        if (!ex.Message.Contains("The underlying connection was closed"))
                        {
                            throw;
                        }
                    }
                } while (stopwatch.ElapsedMilliseconds <= longTimeout * 1000);
                stopwatch.Stop();
            }
            else
            {
                BodyTypeButton(bodyType).ScrollIntoViewAndClick();
            }
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleBodyTypePage ValidateVehicleBodyTypePageDisplayed()
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
        public VehicleBodyTypePage ValidateSuggestedBodyTypesDisplayed(string[] suggestedBodyTypes)
        {
            var node = GetLastNode();
            try
            {
                ParameterValidator.ValidateNotNull(suggestedBodyTypes, "Suggested Body Types");
                foreach (var item in suggestedBodyTypes)
                {
                    if (IsElementDisplayed(_btnBodyType(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateSuggestedBodyTypesDisplayed, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateSuggestedBodyTypesDisplayed, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSuggestedBodyTypesDisplayed, e);
            }
            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Vehicle Body Type page is displayed";
            public static string ValidateSuggestedBodyTypesDisplayed = "Validate suggested body types are displayed";
        }
        #endregion
    }
}


