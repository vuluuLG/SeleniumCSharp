using Framework.Test.Common.Helper;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using Framework.Test.UI.Pages.Global;
using System;

namespace Framework.Test.UITests.SharedStep
{
    public static class Driver
    {
        public static AddAnotherDriverPage AddDriver(this DriverEnterPage driverEnterPage, DriverInformation driverInformation)
        {
            return AddDriver<AddAnotherDriverPage>(driverEnterPage, driverInformation);
        }

        public static DriverOverviewPage EditDriver(this DriverEnterPage driverFormPage, DriverInformation driverInformation)
        {
            if (driverInformation != null)
                driverInformation.IsEditFlow = true;
            return AddDriver<DriverOverviewPage>(driverFormPage, driverInformation);
        }

        public static T AddDriver<T>(this DriverEnterPage driverEnterPage, DriverInformation driverInformation)
        {
            ParameterValidator.ValidateNotNull(driverEnterPage, "Driver enter page");
            ParameterValidator.ValidateNotNull(driverInformation, "Driver information");

            AddAnotherDriverPage addAnotherDriverPage = new AddAnotherDriverPage();
            CDLExperiencePage cDLExperiencePage = new CDLExperiencePage();
            DriverOverviewPage driverOverviewPage = new DriverOverviewPage();

            if (driverInformation.IsPO)
            {
                //1. Complete driver info and select next
                driverEnterPage.EnterDriverInfo(driverInformation).SelectNextButton();
            }
            else
            {
                //1.1 Complete Name and Date of Birth
                driverEnterPage.EnterNameAndDOB(driverInformation);
                //Verification
                driverEnterPage.ValidateNextButtonDisplayedAndEnabled();

                //1.2 Complete license number and state and select next
                driverEnterPage.EnterLicenseAndState(driverInformation).SelectNextButton();
            }
            //Verification
            cDLExperiencePage.ValidateCDLExperiencePageDisplayed();

            //2. Complete CDL Experience info and select next
            dynamic currentPage = cDLExperiencePage.EnterCDLExperience(driverInformation.CDLExperience).SelectNextButton();
            if (driverInformation.MVRStatus == OrderMVRStatus.Invalid 
                || (driverInformation.MVRStatus == OrderMVRStatus.Unknown 
                    && currentPage is AccidentsAndViolationsPage))
            {
                AccidentsAndViolationsPage accidentsAndViolationsPage = new AccidentsAndViolationsPage();
                //verification
                accidentsAndViolationsPage.ValidateAccidentsAndViolationsPageDisplayed();

                //3. Complete Accident and Violation info and select next
                ConvictionsPage convictionsPage = accidentsAndViolationsPage.EnterAccidentAndViolation(driverInformation.AccidentsAndViolations).SelectNextButton();

                //Verification
                convictionsPage.ValidateConvictionsPageDisplayed();

                //4. Complete Convictions info
                convictionsPage.EnterConviction(driverInformation.Conviction);

                // Navigate to Driver Overview page if process is edit Flow
                if (driverInformation.IsEditFlow)
                {
                    //Select next button
                    convictionsPage.SelectNextButton<DriverOverviewPage>();
                }
                // Require the second driver if DoseNotDrive was checked
                else if (driverInformation.DoesNotDrive)
                {
                    // Select Next Button
                    convictionsPage.SelectNextButton<DriverEnterPage>();
                }
                else
                {
                    // Select Next button
                    convictionsPage.SelectNextButton<AddAnotherDriverPage>();
                }
            }
            if (driverInformation.IsEditFlow)
            {
                // verification
                driverOverviewPage.ValidateDriverOverviewPageDisplayed();
            }
            else if (driverInformation.DoesNotDrive)
            {
                //Verification
                driverEnterPage.ValidateDriverEnterPageDisplayed();
            }
            else
            {
                // Verification
                addAnotherDriverPage.ValidateAddAnotherDriverPageDisplayed();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static CoveragesPage AddDriver(this DriverEnterPage driverEnterPage, DriverInformation[] driverList)
        {
            ParameterValidator.ValidateNotNull(driverList, "Driver List");

            AddAnotherDriverPage addAnotherDriverPage = new AddAnotherDriverPage();
            for (int i = 0; i < driverList.Length; i++)
            {
                //9. Add vehicle
                if (driverList[i].DoesNotDrive)
                {
                    driverEnterPage.AddDriver<DriverEnterPage>(driverList[i]);
                }
                else
                {
                    driverEnterPage.AddDriver(driverList[i]);
                    if (i != driverList.Length - 1)
                    {
                        //10. Yes more driver
                        addAnotherDriverPage.YesMoreDriver();
                    }
                    else
                    {
                        //10. No more driver
                        addAnotherDriverPage.NoMoreDriver();
                    }
                }
            }
            return new CoveragesPage();
        }

        public static DriverEnterPage YesMoreDriver(this AddAnotherDriverPage addAnotherDriverPage)
        {
            ParameterValidator.ValidateNotNull(addAnotherDriverPage, "AddAnotherDriverPage");

            // Select decision when promted to add more driver
            addAnotherDriverPage.SelectAddMoreDriverButton(AnswerOption.Yes);
            //Select Next Button
            DriverEnterPage driverEnterPage = addAnotherDriverPage.SelectNextButton<DriverEnterPage>();
            //verification
            driverEnterPage.ValidateDriverEnterPageDisplayed();
            return driverEnterPage;
        }

        public static CoveragesPage NoMoreDriver(this AddAnotherDriverPage addAnotherDriverPage)
        {
            ParameterValidator.ValidateNotNull(addAnotherDriverPage, "AddAnotherDriverPage");

            // Select decision when promted to add more driver
            addAnotherDriverPage.SelectAddMoreDriverButton(AnswerOption.No);
            //Select Next Button
            CoveragesPage coveragesPage = addAnotherDriverPage.SelectNextButton<CoveragesPage>();
            //verification
            coveragesPage.ValidateCoveragesPageDisplayed();
            return coveragesPage;
        }

        public static T NoMoreDriver<T>(this AddAnotherDriverPage addAnotherDriverPage) where T : BasePage
        {
            ParameterValidator.ValidateNotNull(addAnotherDriverPage, "AddAnotherDriverPage");
            //Select next button
            dynamic currentPage = addAnotherDriverPage.SelectNextButton<T>();
            if (typeof(T) == typeof(DriverOverviewPage))
            {
                // Verification
                currentPage.ValidateValidateDriverOverViewPageDisplayed();
            }
            else
            {
                // Verification
                currentPage.ValidateCoveragesPageDisplayed();
            }
            return currentPage;
        }
    }
}