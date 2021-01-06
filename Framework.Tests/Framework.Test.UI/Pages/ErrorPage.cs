using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class ErrorPage : BasePage
    {
        #region Locators
        private By _btnGotoDashboard => By.XPath("//button[normalize-space(.)='Go to Dashboard']");
        #endregion

        #region Elements
        public IWebElement GoToDashboardButton => StableFindElement(_btnGotoDashboard);
        #endregion

        #region Business Methods
        public ErrorPage()
        {
            Url = "/error";
            RequiredElementLocator = _btnGotoDashboard;
        }
        #endregion
    }
}
