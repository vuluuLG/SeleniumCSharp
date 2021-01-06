using Framework.Test.Common.Helper;
using OpenQA.Selenium;
using System;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class VehicleSeatCapPage : LandingPage
    {
        #region Locators
        private By _txtSeatCap => By.XPath("//*[contains(@class, 'automation-id-seatingCapacity')]//input");
        #endregion

        #region Elements
        public IWebElement SeatCapTextBox => StableFindElement(_txtSeatCap);
        #endregion

        #region Business Methods
        public VehicleSeatCapPage() : base()
        {
            Url = "forms/seating-capacity";
            RequiredElementLocator = _txtSeatCap;
        }

        [ExtentStepNode]
        public VehicleSeatCapPage EnterSeatCap(string seatCap)
        {
            GetLastNode().Info("Input seat cap: " + seatCap);
            SeatCapTextBox.InputText(seatCap);
            WaitForElementEnabled(_btnNext);
            return this;
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public VehicleSeatCapPage ValidateVehicleSeatCapPageDisplayed()
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

        private static class ValidationMessage
        {
            public static string ValidatePageDisplayed = "Validate Vehicle Seat Cap page is displayed";
        }
        #endregion
    }
}