using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using static Framework.Test.Common.Helper.ExtentReportsHelper;
using static Framework.Test.UI.Helper.WaitHelper;
using static Framework.Test.UI.Helper.WebElementHelper;

namespace Framework.Test.UI.Pages
{
    public class LandingPage : BasePage
    {
        #region Locators
        private By _eleSubItem(string item) => By.XPath($"//div[@class='mat-menu-content']/a[text()='{item}']");
        private By _lblSubmissionNumber => By.XPath("//*[contains(@class, 'submission-number')]");
        private By _imgLogo => By.XPath("//img[contains(@src, 'icon')]");
        private By _imgHeaderLogo => By.XPath("//breeze-header//img");
        private By _lstBusiness => By.XPath("//mat-panel-title[contains(.,'Business')]//ancestor::mat-expansion-panel//li/a");
        private By _lstCustomer => By.XPath("//mat-panel-title[contains(.,'Customer')]//ancestor::mat-expansion-panel//li/a");
        private By _lstVehicles => By.XPath("//mat-panel-title[contains(.,'Vehicles')]//ancestor::mat-expansion-panel//li/a");
        private By _lstDrivers => By.XPath("//mat-panel-title[contains(.,'Drivers')]//ancestor::mat-expansion-panel//li/a");
        private By _lstCoverages => By.XPath("//mat-panel-title[contains(.,'Coverage')]//ancestor::mat-expansion-panel//li");
        private By _eleVehicle(string item) => By.XPath($"//mat-panel-title[contains(.,'Vehicles')]//ancestor::mat-expansion-panel//li/a[text()=\"{item}\"]");
        private By _eleDriver(string item) => By.XPath($"//mat-panel-title[contains(.,'Drivers')]//ancestor::mat-expansion-panel//li/a[text()=\"{item}\"]");
        private By _lnkMenuHeaders => By.XPath("//mat-panel-title//a");
        private By _lnkMenuHeader(string item) => By.XPath($"//mat-panel-title[contains(.,'{item}')]//a");
        private By _eleMenuHeader(string item) => By.XPath($"//mat-panel-title[contains(.,'{item}')]/*");
        private By _iconMenuHeader(string item) => By.XPath($"//mat-panel-title[contains(.,'{item}')]//following-sibling::mat-icon[1]");
        private By _iconCheckmarkHeader(string item) => By.XPath($"//mat-panel-title[contains(.,'{item}')]//following-sibling::mat-icon[@svgicon='menu-complete-check']");
        private By _dlgConfirmAddress => By.XPath("//breeze-confirm-address");
        private By _btnAddress(int index) => By.XPath($"(//breeze-confirm-address//mat-button-toggle/button)[{index}]");
        private By _btnUseSelectedAddress => By.XPath("//breeze-confirm-address//button[.='USE SELECTED ADDRESS']");
        private By _eleQuoteGroupPanelItem => By.XPath("//mat-expansion-panel-header[@tabindex='0']");
        protected By _lblHeader => By.XPath("//breeze-page-header//h1");
        protected By _dlgConfirm => By.XPath("//breeze-confirm-dialog");
        protected By _lblDialogContent => By.XPath("//mat-dialog-content");
        protected By _btnConfirmDialogOption(string item) => By.XPath($"//breeze-confirm-dialog//button[contains(.,'{item}')]");
        protected By _btnNext => By.XPath("//button[contains(.,'NEXT')]");
        protected By _btnPrevious => By.XPath("//button[contains(.,'PREVIOUS')]");
        protected By _btnClose => By.XPath("//*[contains(@icon, 'close')]");
        protected By _eleWarningBar => By.XPath("//simple-snack-bar");
        protected By _btnDismiss => By.XPath("//simple-snack-bar//div[contains(@class, 'snackbar-action')]//span[text() = 'Dismiss']");
        protected By _lblNavCoverages => By.XPath("//ul[contains(@class,'coverageSubItemListContainer')]//span[contains(@class,'coverageItemText')]");
        protected By _lblNavCoverage(string item) => By.XPath($"//ul[contains(@class,'coverageSubItemListContainer')]//span[contains(@class,'coverageItemText') and text()='{item}']");
        protected By _lblNavPremiums => By.XPath("//ul[contains(@class,'coverageSubItemListContainer')]//span[contains(@class,'coverageItemPremium')]");
        protected By _lblNavPremium(string item) => By.XPath($"//ul[contains(@class,'coverageSubItemListContainer')]//span[text()='{item}']//following-sibling::span[contains(@class,'coverageItemPremium')]");
        protected By _lblNavTotal => By.XPath("//ul[contains(@class, 'coverageSummaryTotalListContainer')]//span[contains(@class,'coverageItemPremium')]");
        protected By _eleDropdownOptions => By.XPath("//mat-option");
        protected By _eleDropdownOption(string item) => By.XPath($"//mat-option[.//*[normalize-space(text())='{item}']]");
        protected By _lblDropdownOptions => By.XPath("//*[@class='mat-option-text']");
        protected By _btnGiveFeedback => By.XPath("//button[@id='QSIFeedbackButton-btn']");
        #endregion

        #region Elements
        public IWebElement HeaderLogoImage => StableFindElement(_imgHeaderLogo);
        public IWebElement HeaderLabel => StableFindElement(_lblHeader);
        public IWebElement NextButton => StableFindElement(_btnNext);
        public IWebElement PreviousButton => StableFindElement(_btnPrevious);
        public IWebElement CloseButton => StableFindElement(_btnClose);
        public IWebElement DismissButton => StableFindElement(_btnDismiss);
        public IWebElement DialogContentLabel => StableFindElement(_lblDialogContent);
        public IWebElement AddressButton(int index) => StableFindElement(_btnAddress(index));
        public IWebElement UseSelectedAddressButton => StableFindElement(_btnUseSelectedAddress);
        public IWebElement SubmissionNumberLabel => StableFindElement(_lblSubmissionNumber);
        public IWebElement SubItem(string item) => StableFindElement(_eleSubItem(item));
        public IWebElement MenuHeaderLink(string item) => StableFindElement(_lnkMenuHeader(item));
        public IWebElement MenuHeaderTitle(string item) => StableFindElement(_eleMenuHeader(item));
        public IWebElement MenuHeaderIcon(string item) => StableFindElement(_iconMenuHeader(item));
        public IWebElement ConfirmDialogOptionButton(string item) => StableFindElement(_btnConfirmDialogOption(item));
        public IWebElement QuoteGroupPanelItem => StableFindElement(_eleQuoteGroupPanelItem);
        public IWebElement DropdownOptions => StableFindElement(_eleDropdownOptions);
        public IWebElement DropdownOption(string item) => StableFindElement(_eleDropdownOption(item));
        public ReadOnlyCollection<IWebElement> DropdownOptionsLabel => StableFindElements(_lblDropdownOptions);
        #endregion

        #region Business Methods
        public LandingPage()
        {
            RequiredElementLocator = _btnNext;
        }

        [ExtentStepNode]
        public string GetQuoteId()
        {
            string[] urlPart = WebDriver.Url.Split('/');
            GetLastNode().Info("QuoteID: " + urlPart[4]);
            return urlPart[4];
        }

        [ExtentStepNode]
        public T SelectNextButton<T>() where T : BasePage
        {
            NextButton.ScrollIntoViewBottom();
            WaitForElementClickable(_btnNext);
            NextButton.ClickWithJS();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public T SelectPreviousButton<T>() where T : BasePage
        {
            PreviousButton.Click();
            // Close confirm dialog if exists with option = 'Yes'
            CloseConfirmDialogIfExist();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public T SelectMenuLink<T>(string itemName) where T : BasePage
        {
            GetLastNode().Info("Select menu link: " + itemName);
            MenuHeaderLink(itemName).Click();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();

            return page;
        }

        [ExtentStepNode]
        public T SelectMenuTitle<T>(string itemName) where T : BasePage
        {
            GetLastNode().Info("Select menu title: " + itemName);
            MenuHeaderTitle(itemName).Click();
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public T ExpandQuoteGroupMenu<T>() where T : BasePage
        {
            if (IsElementDisplayed(_eleQuoteGroupPanelItem, 3) && QuoteGroupPanelItem.GetAttribute("aria-expanded") == "false")
            {
                QuoteGroupPanelItem.ScrollIntoViewAndClick();
                WaitForElementAttribute(_eleQuoteGroupPanelItem, "aria-expanded", "true");
            }
            var page = (T)Activator.CreateInstance(typeof(T));
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public void CloseConfirmDialogIfExist(string option = AnswerOption.Yes)
        {
            if (IsElementDisplayed(_dlgConfirm, 3))
            {
                GetLastNode().Info("The confirm dialog exists, closing with option: " + option);
                WaitForElementClickable(_btnConfirmDialogOption(option));
                ConfirmDialogOptionButton(option).ClickWithJS();
                WaitForElementInvisible(_dlgConfirm);
            }
        }

        [ExtentStepNode]
        public WelcomePage SelectCloseButton(bool doesHandleConfirmDialog = true)
        {
            CloseButton.Click();
            if (doesHandleConfirmDialog)
            {
                CloseConfirmDialogIfExist();
            }
            var page = new WelcomePage();
            page.WaitForPageLoad();
            return page;
        }

        [ExtentStepNode]
        public T SelectOptionOnConfirmDialog<T>(string option) where T : BasePage
        {
            GetLastNode().Info("Select option: " + option);
            WaitForElementClickable(_btnConfirmDialogOption(option));
            ConfirmDialogOptionButton(option).ClickWithJS();
            WaitForElementInvisible(_dlgConfirm);
            return (T)Activator.CreateInstance(typeof(T));
        }

        [ExtentStepNode]
        public void CloseConfirmAddressDialogIfExist(int addressIndex = 0)
        {
            var node = GetLastNode();
            if (IsElementDisplayed(_dlgConfirmAddress, 2))
            {
                if (addressIndex <= 0)
                {
                    node.Info("The confirm address dialog exists, closing without selecting address");
                    try
                    {
                        WebDriver.Actions.Click(HeaderLogoImage).Build().Perform();
                    }
                    catch { }
                }
                else
                {
                    string address = GetText(_btnAddress(addressIndex)).Replace("\r\n", ", ");
                    node.Info("The confirm address dialog exists, select address: " + address);
                    WaitForElementClickable(_btnAddress(addressIndex));
                    AddressButton(addressIndex).ClickWithJS();
                    WaitForElementEnabled(_btnUseSelectedAddress);
                    UseSelectedAddressButton.ClickWithJS();
                }
                WaitForElementInvisible(_dlgConfirmAddress);
            }
        }

        [ExtentStepNode]
        public void CloseDropdownIfOpened()
        {
            if (IsElementDisplayed(_eleDropdownOptions, 2))
            {
                GetLastNode().Info("The dropdown is opened, closing");
                try
                {
                    HeaderLogoImage.ClickWithJS(false);
                }
                catch { }
                WaitForElementNotExists(_eleDropdownOptions, 15, false);
            }
        }

        public string GetSubmissionNumber()
        {
            return GetText(_lblSubmissionNumber).Replace("Submission ", "");
        }

        public void WaitForLogoLoad()
        {
            WaitForElementVisible(_imgLogo);
        }
        #endregion

        #region Validations
        [ExtentStepNode]
        public LandingPage ValidateSubmissionNumberDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblSubmissionNumber, waitForElementTimeout))
                {
                    SetPassValidation(node, ValidationMessage.ValidateSubmissionNumber);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateSubmissionNumber);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateSubmissionNumber, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateLandingPageDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_imgLogo))
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
        public T ValidateNextButtonEnabled<T>()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementEnabled(_btnNext))
                {
                    SetPassValidation(node, ValidationMessage.ValidateNextButtonEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateNextButtonEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateNextButtonEnabled, e);
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        [ExtentStepNode]
        public T ValidateNextButtonDisplayedAndDisabled<T>()
        {
            var node = GetLastNode();
            try
            {
                if ((IsElementDisplayed(_btnNext)) && (!IsElementEnabled(_btnNext)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndDisabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndDisabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndDisabled, e);
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        [ExtentStepNode]
        public T ValidateNextButtonDisplayedAndEnabled<T>()
        {
            var node = GetLastNode();
            try
            {
                if ((IsElementDisplayed(_btnNext)) && (IsElementEnabled(_btnNext)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateNextButtonDisplayedAndEnabled, e);
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        [ExtentStepNode]
        public T ValidateConfirmDialogDisplayed<T>()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_dlgConfirm))
                {
                    SetPassValidation(node, ValidationMessage.ValidateConfirmDialogDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateConfirmDialogDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateConfirmDialogDisplayed, e);
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        [ExtentStepNode]
        public LandingPage ValidateVehiclesOnLeftNavigationDisplayedCorrectly(params string[] expectedVehicles)
        {
            var node = GetLastNode();
            try
            {
                int total = GetElementNumberFromPageSource(_lstVehicles);
                if (expectedVehicles.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehiclesOnLeftNavigationDisplayedCorrectly, "Total Vehicles", expectedVehicles.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    var vehiclesList = StableFindElementsFromPageSource(_lstVehicles);
                    for (int i = 0; i < expectedVehicles.Length; i++)
                    {
                        if (vehiclesList[i].InnerText.Trim() == expectedVehicles[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateVehiclesOnLeftNavigationDisplayedCorrectly, expectedValue: expectedVehicles[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateVehiclesOnLeftNavigationDisplayedCorrectly, expectedValue: expectedVehicles[i], actualValue: vehiclesList[i].InnerText.Trim());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehiclesOnLeftNavigationDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateBusinessOnLeftNavigationDisplayedCorrectly(string expectedBusiness)
        {
            var node = GetLastNode();
            try
            {
                var business = StableFindElementFromPageSource(_lstBusiness);
                if (business.InnerText.Trim() == expectedBusiness)
                {
                    SetPassValidation(node, ValidationMessage.ValidateBusinessOnLeftNavigationDisplayedCorrectly, expectedValue: expectedBusiness);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateBusinessOnLeftNavigationDisplayedCorrectly, expectedValue: expectedBusiness, actualValue: business.InnerText.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessOnLeftNavigationDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCustomersOnLeftNavigationDisplayedCorrectly(string expectedCustomer)
        {
            var node = GetLastNode();
            try
            {
                var customer = StableFindElementFromPageSource(_lstCustomer);
                if (customer.InnerText.Trim() == expectedCustomer)
                {
                    SetPassValidation(node, ValidationMessage.ValidatCustomersOnLeftNavigationDisplayedCorrectly, expectedValue: expectedCustomer);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatCustomersOnLeftNavigationDisplayedCorrectly, expectedValue: expectedCustomer, actualValue: customer.InnerText.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateBusinessOnLeftNavigationDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateVehicleOnLeftNavigationDeleted(string delectedVehicle)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayedFromPageSource(_eleVehicle(delectedVehicle)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateVehicleOnLeftNavigationDeleted, delectedVehicle);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateVehicleOnLeftNavigationDeleted, delectedVehicle);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateVehicleOnLeftNavigationDeleted, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateDriversOnLeftNavigationDisplayedCorrectly(params string[] expectedDrivers)
        {
            var node = GetLastNode();
            try
            {
                int total = GetElementNumberFromPageSource(_lstDrivers);
                if (expectedDrivers.Length != total)
                {
                    SetFailValidation(node, ValidationMessage.ValidateDriversOnLeftNavigationDisplayedCorrectly, "Total Drivers", expectedDrivers.Length.ToString(), actualValue: total.ToString());
                }
                else
                {
                    var driversList = StableFindElementsFromPageSource(_lstDrivers);
                    for (int i = 0; i < expectedDrivers.Length; i++)
                    {
                        if (driversList[i].InnerText.Trim() == expectedDrivers[i])
                        {
                            SetPassValidation(node, ValidationMessage.ValidateDriversOnLeftNavigationDisplayedCorrectly, expectedValue: expectedDrivers[i]);
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateDriversOnLeftNavigationDisplayedCorrectly, expectedValue: expectedDrivers[i], actualValue: driversList[i].InnerText.Trim());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDriversOnLeftNavigationDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCoveragesOnLeftNavigationDisplayed(params string[] expectedCoverages)
        {
            var node = GetLastNode();
            try
            {
                foreach (var coverage in expectedCoverages)
                {
                    if (IsElementDisplayedFromPageSource(_lblNavCoverage(coverage)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationDisplayed, coverage);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationDisplayed, coverage);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCoveragesOnLeftNavigationNotDisplayed(params string[] expectedCoverages)
        {
            var node = GetLastNode();
            try
            {
                foreach (var coverage in expectedCoverages)
                {
                    if (!IsElementDisplayedFromPageSource(_lblNavCoverage(coverage)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationNotDisplayed, coverage);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationNotDisplayed, coverage);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragesOnLeftNavigationNotDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCoveragePremiumOnLeftNavigationDisplayedCorrectly(string expectedCoverage, string expectedPremium)
        {
            var node = GetLastNode();
            try
            {
                string actualPremium = GetTextFromPageSource(_lblNavPremium(expectedCoverage)).Trim();
                if (actualPremium == expectedPremium)
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoveragePremiumOnLeftNavigationDisplayedCorrectly, expectedCoverage, expectedPremium);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoveragePremiumOnLeftNavigationDisplayedCorrectly, expectedCoverage, expectedPremium, actualPremium);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragePremiumOnLeftNavigationDisplayedCorrectly, e, expectedCoverage);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCoveragePremiumsOnLeftNavigationNotDisplayed()
        {
            var node = GetLastNode();
            try
            {
                var coverageLabels = StableFindElementsFromPageSource(_lblNavCoverages);
                foreach (var item in coverageLabels)
                {
                    string actualPremium = GetTextFromPageSource(_lblNavPremium(item.InnerText)).Trim();
                    if (string.IsNullOrEmpty(actualPremium))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateCoveragePremiumsOnLeftNavigationNotDisplayed, item.InnerText);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateCoveragePremiumsOnLeftNavigationNotDisplayed, item.InnerText);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoveragePremiumsOnLeftNavigationNotDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateLeftNavigationUpdatedWithLinks(params string[] linkedItems)
        {
            var node = GetLastNode();
            try
            {
                foreach (var item in linkedItems)
                {
                    if (IsElementDisplayed(_lnkMenuHeader(item)))
                    {
                        SetPassValidation(node, ValidationMessage.ValidateLeftNavigationUpdatedWithLinks, item);
                    }
                    else
                    {
                        SetFailValidation(node, ValidationMessage.ValidateLeftNavigationUpdatedWithLinks, item);
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLeftNavigationUpdatedWithLinks, e);
            }
            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateHeaderDisplayedCorrectly(string header)
        {
            var node = GetLastNode();
            try
            {
                if (HeaderLabel.Text.Trim() == header)
                {
                    SetPassValidation(node, ValidationMessage.ValidateHeaderDisplayedCorrectly, expectedValue: header);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateHeaderDisplayedCorrectly, expectedValue: header, actualValue: HeaderLabel.Text.Trim());
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateHeaderDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateLeftNavigationHasNoLinkEnabled()
        {
            var node = GetLastNode();
            try
            {
                if (GetElementNumber(_lnkMenuHeaders, 1) == 0)
                {
                    SetPassValidation(node, ValidationMessage.ValidateLeftNavigationHasNoLinkEnabled);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateLeftNavigationHasNoLinkEnabled);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateLeftNavigationHasNoLinkEnabled, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateDialogContentDisplayedCorrectly(string content)
        {
            var node = GetLastNode();
            try
            {
                string actual = GetText(_lblDialogContent);
                if (content == actual)
                {
                    SetPassValidation(node, ValidationMessage.ValidateDialogContentDisplayedCorrectly, content);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateDialogContentDisplayedCorrectly, expectedValue: content, actualValue: actual);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateDialogContentDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateMenuHeaderIsBold(string menu)
        {
            var node = GetLastNode();
            try
            {
                if (MenuHeaderTitle(menu).GetCssValue("font-weight") == "600")
                {
                    SetPassValidation(node, ValidationMessage.ValidateMenuHeaderIsBold, menu);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateMenuHeaderIsBold, menu);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMenuHeaderIsBold, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateMenuHeaderIsNotBold(string menu)
        {
            var node = GetLastNode();
            try
            {
                if (MenuHeaderTitle(menu).GetCssValue("font-weight") != "600")
                {
                    SetPassValidation(node, ValidationMessage.ValidateMenuHeaderIsNotBold, menu);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateMenuHeaderIsNotBold, menu);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMenuHeaderIsNotBold, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateMenuIconIsOnlyOneHighLighted(string[] iconlist, string highlightedIcon)
        {
            var node = GetLastNode();
            try
            {
                WaitForElementVisible(_imgLogo, throwException: false);
                WaitForElementCSSAttribute(_iconMenuHeader(highlightedIcon), "opacity", "1", throwException: false);
                ParameterValidator.ValidateNotNull(iconlist, "Icon list");
                foreach (var item in iconlist)
                {
                    if (item == highlightedIcon)
                    {
                        if (MenuHeaderIcon(item).GetCssValue("opacity") == "1")
                        {
                            SetPassValidation(node, ValidationMessage.ValidateMenuIconIsOnlyOneHighLighted, $"The {item} icon is highlighted");
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateMenuIconIsOnlyOneHighLighted, $"The {item} icon is not highlighted");
                        }
                    }
                    else
                    {
                        if (MenuHeaderIcon(item).GetCssValue("opacity") != "1")
                        {
                            SetPassValidation(node, ValidationMessage.ValidateMenuIconIsOnlyOneHighLighted, $"The {item} icon is not highlighted");
                        }
                        else
                        {
                            SetFailValidation(node, ValidationMessage.ValidateMenuIconIsOnlyOneHighLighted, $"The {item} icon is highlighted");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMenuIconIsOnlyOneHighLighted, e);
            }
            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateMenuIconIsNotHighLighted(string icon)
        {
            var node = GetLastNode();
            try
            {
                WaitForElementVisible(_imgLogo, throwException: false);
                if (MenuHeaderIcon(icon).GetCssValue("opacity") != "1")
                {
                    SetPassValidation(node, ValidationMessage.ValidateMenuIconIsNotHighLighted, icon);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateMenuIconIsNotHighLighted, icon);
                }

            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMenuIconIsNotHighLighted, e);
            }
            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidatePageTitleDisplayedCorrectly(string title)
        {
            var node = GetLastNode();
            try
            {
                string actualTitle = WebDriver.Title;
                if (actualTitle == title)
                {
                    SetPassValidation(node, ValidationMessage.ValidatePageTitleDisplayedCorrectly, title);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidatePageTitleDisplayedCorrectly, expectedValue: title, actualValue: actualTitle);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidatePageTitleDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCheckmarkIconDisplayed(string menu)
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_iconCheckmarkHeader(menu), 3))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCheckmarkIconDisplayed, menu);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCheckmarkIconDisplayed, menu);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCheckmarkIconDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateCoverageTotalLabelOnLeftNavigationDisplayed()
        {
            var node = GetLastNode();
            try
            {
                if (IsElementDisplayed(_lblNavTotal))
                {
                    SetPassValidation(node, ValidationMessage.ValidateCoverageTotalLabelOnLeftNavigationDisplayed);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateCoverageTotalLabelOnLeftNavigationDisplayed);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateCoverageTotalLabelOnLeftNavigationDisplayed, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateTotalPremiumOnLeftNavigationDisplayedCorrectly(string expectedPremium)
        {
            var node = GetLastNode();
            try
            {
                string actualPremium = GetText(_lblNavTotal).Trim();
                if (expectedPremium == actualPremium)
                {
                    SetPassValidation(node, ValidationMessage.ValidateTotalPremiumOnLeftNavigationDisplayedCorrectly, expectedValue: expectedPremium);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateTotalPremiumOnLeftNavigationDisplayedCorrectly, expectedValue: expectedPremium, actualValue: actualPremium);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateTotalPremiumOnLeftNavigationDisplayedCorrectly, e);
            }

            return this;
        }

        [ExtentStepNode]
        public LandingPage ValidateMenuHeaderLinkNotDisplayed(string menu)
        {
            var node = GetLastNode();
            try
            {
                if (!IsElementDisplayed(_lnkMenuHeader(menu)))
                {
                    SetPassValidation(node, ValidationMessage.ValidateMenuHeaderLinkNotDisplayed, menu);
                }
                else
                {
                    SetFailValidation(node, ValidationMessage.ValidateMenuHeaderLinkNotDisplayed, menu);
                }
            }
            catch (Exception e)
            {
                SetErrorValidation(node, ValidationMessage.ValidateMenuHeaderLinkNotDisplayed, e);
            }

            return this;
        }

        private static class ValidationMessage
        {
            public static string ValidateSubmissionNumber = "Validate Submission number is displayed";
            public static string ValidatePageDisplayed = "Validate Landing page is displayed";
            public static string ValidateNextButtonDisplayedAndDisabled = "Validate Next Button is displayed and disabled";
            public static string ValidateNextButtonEnabled = "Validate Next Button is enabled";
            public static string ValidateNextButtonDisplayedAndEnabled = "Validate Next Button is displayed and enabled";
            public static string ValidateVehiclesOnLeftNavigationDisplayedCorrectly = "Validate vehicles on left navigation are displayed correctly";
            public static string ValidateBusinessOnLeftNavigationDisplayedCorrectly = "Validate business on left navigation are displayed correctly";
            public static string ValidateVehicleOnLeftNavigationDeleted = "Validate vehicle is deleted on left navigation";
            public static string ValidateDriversOnLeftNavigationDisplayedCorrectly = "Validate drivers on left navigation are displayed correctly";
            public static string ValidatCustomersOnLeftNavigationDisplayedCorrectly = "Validate customers on left navigation are displayed correctly";
            public static string ValidateHeaderDisplayedCorrectly = "Validate the header is displayed correctly";
            public static string ValidateLeftNavigationHasNoLinkEnabled = "Validate the Left navigation has no link enabled";
            public static string ValidateLeftNavigationUpdatedWithLinks = "Validate the Left navigation is updated to have links";
            public static string ValidateDialogContentDisplayedCorrectly = "Validate dialog content is displayed correctly";
            public static string ValidateMenuHeaderIsBold = "Validate menu header is bold";
            public static string ValidateMenuIconIsOnlyOneHighLighted = "Validate menu icon is only one highlighted";
            public static string ValidatePageTitleDisplayedCorrectly = "Validate page title displayed correctly";
            public static string ValidateCheckmarkIconDisplayed = "Validate checkmark icon is displayed";
            public static string ValidateMenuIconIsNotHighLighted = "Validate menu icon is not highlighted";
            public static string ValidateMenuHeaderIsNotBold = "Validate menu header is bold";
            public static string ValidateCoverageTotalLabelOnLeftNavigationDisplayed = "Validate Coverage Total label on left navigation is displayed";
            public static string ValidateTotalPremiumOnLeftNavigationDisplayedCorrectly = "Validate Total premium on left navigation is displayed correctly";
            public static string ValidateConfirmDialogDisplayed = "Validate Confirm dialog is displayed";
            public static string ValidateMenuHeaderLinkNotDisplayed = "Validate menu header link is not displayed";
            public static string ValidateCoveragesOnLeftNavigationDisplayed = "Validate coverages on left navigation are displayed";
            public static string ValidateCoveragesOnLeftNavigationNotDisplayed = "Validate coverages on left navigation are not displayed";
            public static string ValidateCoveragePremiumOnLeftNavigationDisplayedCorrectly = "Validate coverage premium on left navigation is displayed correctly";
            public static string ValidateCoveragePremiumsOnLeftNavigationNotDisplayed = "Validate coverage premiums on left navigation are not displayed";
        }
        #endregion
    }
}