using Framework.Test.Common.Helper;
using Framework.Test.UI;
using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using Framework.Test.UI.Pages.Global;
using System;
using System.Linq;

namespace Framework.Test.UITests.SharedStep
{
    public static class Vehicle
    {
        public static T EnterPolicyLevelVehicleInfo<T>(this VehicleInfoPage vehicleInfoPage, PolicyLevelVehicleInformation policyLevelVehicleInformation)
        {
            ParameterValidator.ValidateNotNull(vehicleInfoPage, "VehicleInfoPage");
            ParameterValidator.ValidateNotNull(policyLevelVehicleInformation, "Policy Level Vehicle Information");
            //1. Enter policy level vehicle info
            vehicleInfoPage.InputVehicleInformation(policyLevelVehicleInformation);

            // wait 5s for allow async call to complete
            Browser.Wait(5);

            // Wait an additional 5s if territory can be pulled
            if (!string.IsNullOrWhiteSpace(policyLevelVehicleInformation.GaragingZipCode))
                Browser.Wait(5);

            if (policyLevelVehicleInformation.GaragingAddress != null)
            {
                // Wait for asynchronous process of default coverages complete
                Browser.Wait(10);

                //2. Select next button
                GaragingAddressPage garagingAddressPage = vehicleInfoPage.SelectNextButton<GaragingAddressPage>();
                // verification
                garagingAddressPage.ValidateGaragingAddressPageDisplayed();

                //3. Input Garaging address
                garagingAddressPage.InputGaragingAddress(policyLevelVehicleInformation.GaragingAddress);
            }

            //4. Select Next button
            VehicleEntryPage vehicleEntryPage = new VehicleEntryPage();
            if (!policyLevelVehicleInformation.IsValidVehicleData)
            {
                dynamic unknownPage = vehicleInfoPage.SelectNextButton();
                if (unknownPage is VehicleSuggestionsPage)
                {
                    // Select 'I don't see my vehicle' link if the vehicle Suggestion screen displayed
                    VehicleSuggestionsPage vehicleSuggestionsPage = unknownPage;
                    vehicleSuggestionsPage.SelectIDontSeeMyVehicles();
                }
                //Verification
                vehicleEntryPage.ValidateVehicleEntryPageDisplayed();
            }
            else
            {
                VehicleSuggestionsPage vehicleSuggestionsPage = vehicleInfoPage.SelectNextButton<VehicleSuggestionsPage>();

                //Refresh Vehicle suggestions if vehicle list not returned yet
                //by going to previous page and select Next button again
                if (vehicleEntryPage.IsPageDisplayed())
                {
                    dynamic currentPage;
                    if (policyLevelVehicleInformation.GaragingAddress != null)
                    {
                        currentPage = vehicleEntryPage.SelectPreviousButton<GaragingAddressPage>();
                    }
                    else
                    {
                        currentPage = vehicleEntryPage.SelectPreviousButton<VehicleInfoPage>();
                    }
                    // Wait 5s to allow async call to complete
                    Browser.Wait(5);
                    currentPage.SelectNextButton<VehicleSuggestionsPage>();
                }
                //Verification
                vehicleSuggestionsPage.ValidateVehicleSuggestionsPageDisplayed();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static AddAnotherVehiclePage AddVehicle(this VehicleEntryPage vinEntryPage, VehicleInformation vehicleInformation)
        {
            ParameterValidator.ValidateNotNull(vinEntryPage, "Vehicle Entry Page");
            ParameterValidator.ValidateNotNull(vehicleInformation, "Vehicle Information");

            //1. Enter VIN or select I don't have CIN
            VehicleDescriptorsPage vehicleInfoDescriptorsPage = null;
            VehicleBodyTypePage vehicleBodyTypePage = null;
            if (vehicleInformation.VIN != null)
            {
                // Enter VIN
                vinEntryPage.EnterVin(vehicleInformation.VIN);

                // Check VIN status
                if (vehicleInformation.VinStatus == VinStatus.Unknown)
                {
                    var unknownPage = vinEntryPage.SelectNextButton();
                    if (unknownPage is VehicleDescriptorsPage)
                    {
                        vehicleInfoDescriptorsPage = unknownPage;
                    }
                    else
                    {
                        vehicleBodyTypePage = unknownPage;
                    }
                }
                else if (vehicleInformation.VinStatus == VinStatus.Invalid)
                {
                    vehicleInfoDescriptorsPage = vinEntryPage.SelectNextButton<VehicleDescriptorsPage>();
                }
                else
                {
                    vehicleBodyTypePage = vinEntryPage.SelectNextButton<VehicleBodyTypePage>();
                }
            }
            else
            {
                vehicleInfoDescriptorsPage = vinEntryPage.SelectHasNoVin();
            }

            // If VIN returned NO-HIT
            if (vehicleInfoDescriptorsPage != null)
            {
                // Verification
                vehicleInfoDescriptorsPage.ValidateVehicleDescriptorsPageDisplayed();

                // Complete year, make, model, brakes and airbags and select next
                vehicleBodyTypePage = vehicleInfoDescriptorsPage.EnterVehicleDescription(vehicleInformation.VehicleDescription).SelectNextButton();
            }

            //Verification
            vehicleBodyTypePage.ValidateVehicleBodyTypePageDisplayed();

            //2. Enter vehicle details
            return vehicleBodyTypePage.EnterVehicleDetails(vehicleInformation);
        }

        public static AddAnotherVehiclePage EditVehicle(this VehicleEntryPage vehicleEntryPage, VehicleInformation vehicleInformation)
        {
            return vehicleEntryPage.AddVehicle(vehicleInformation);
        }

        public static DriverEnterPage AddVehicles(this VehicleEntryPage vinEntryPage, VehicleInformation[] vehicleList)
        {
            ParameterValidator.ValidateNotNull(vehicleList, "Vehicle List");

            AddAnotherVehiclePage addAnotherVehiclePage = new AddAnotherVehiclePage();
            for (int i = 0; i < vehicleList.Length; i++)
            {
                //7. Add vehicle
                vinEntryPage.AddVehicle(vehicleList[i]);
                if (i != vehicleList.Length - 1)
                {
                    //8. Yes more vehicle
                    addAnotherVehiclePage.YesMoreVehicle();
                }
                else
                {
                    //8. No more vehicle
                    addAnotherVehiclePage.NoMoreVehicle();
                }
            }
            return new DriverEnterPage();
        }

        public static AddAnotherVehiclePage EnterVehicleDetails(this VehicleBodyTypePage vehicleBodyTypePage, VehicleInformation vehicleInformation)
        {
            ParameterValidator.ValidateNotNull(vehicleBodyTypePage, "vehicleBodyTypePage");
            ParameterValidator.ValidateNotNull(vehicleInformation, "vehicleInformation");

            //1. Select Body type @BodyType
            vehicleBodyTypePage.SelectBodyType(vehicleInformation.BodyType);

            dynamic currentPage = vehicleBodyTypePage;
            //2. Set GVW or seat cap
            if (vehicleInformation.GVW != null)
            {
                //Select Next Button
                VehicleGVWPage vehicleInfoGVWPage = vehicleBodyTypePage.SelectNextButton<VehicleGVWPage>();
                // Verification
                vehicleInfoGVWPage.ValidateVehicleGVWPageDisplayed();

                //Set GVW
                vehicleInfoGVWPage.SelectGrossWeight(vehicleInformation.GVW);
                currentPage = vehicleInfoGVWPage;
            }
            else if (vehicleInformation.SeatCap != null)
            {
                // Select Next button
                VehicleSeatCapPage vehicleInfoSeatCap = vehicleBodyTypePage.SelectNextButton<VehicleSeatCapPage>();
                // Verification
                vehicleInfoSeatCap.ValidateVehicleSeatCapPageDisplayed();

                // Set Seat Cap
                vehicleInfoSeatCap.EnterSeatCap(vehicleInformation.SeatCap);
                currentPage = vehicleInfoSeatCap;
            }

            //Select Next
            if (vehicleInformation.ClassificationFlow != null)
            {
                currentPage.SelectNextButton<VehicleClassificationPage>();
            }
            else if (vehicleInformation.FactorFlow != null)
            {
                currentPage.SelectNextButton<VehicleFactorPage>();
            }
            else if (vehicleInformation.ClassificationOverride != null)
            {
                currentPage.SelectNextButton<VehicleClassificationOverridePage>();
            }
            else
            {
                currentPage.SelectNextButton<VehiclePhysicalDamagePage>();
            }

            //3.1 Select vehicle classification and select next
            if (vehicleInformation.ClassificationFlow != null)
            {
                VehicleClassificationPage vehicleClassificationPage = new VehicleClassificationPage();
                // Verification
                vehicleClassificationPage.ValidateVehicleClassificationPageDisplayed();

                for (int i = 0; i < vehicleInformation.ClassificationFlow.Length - 1; i++)
                {
                    vehicleClassificationPage.SelectClassificationFlow<VehicleFactorPage>(vehicleInformation.ClassificationFlow[i]);
                }

                if (vehicleInformation.FactorFlow != null)
                {
                    vehicleClassificationPage.SelectClassificationFlow<VehicleFactorPage>(vehicleInformation.ClassificationFlow.Last());
                }
                else if (vehicleInformation.ClassificationOverride != null)
                {
                    vehicleClassificationPage.SelectClassificationFlow<VehicleClassificationOverridePage>(vehicleInformation.ClassificationFlow.Last());
                }
                else
                {
                    vehicleClassificationPage.SelectClassificationFlow<VehiclePhysicalDamagePage>(vehicleInformation.ClassificationFlow.Last());
                }
            }

            //3.2 Select vehicle factor and select next
            if (vehicleInformation.FactorFlow != null)
            {
                VehicleFactorPage vehicleFactorPage = new VehicleFactorPage();
                // Verification
                vehicleFactorPage.ValidateVehicleFactorPageDisplayed();

                for (int i = 0; i < vehicleInformation.FactorFlow.Length - 1; i++)
                {
                    vehicleFactorPage.SelectFactorFlow<VehicleFactorPage>(vehicleInformation.FactorFlow[i]);
                }

                if (vehicleInformation.ClassificationOverride != null)
                {
                    vehicleFactorPage.SelectFactorFlow<VehicleClassificationOverridePage>(vehicleInformation.FactorFlow.Last());
                }
                else
                {
                    vehicleFactorPage.SelectFactorFlow<VehiclePhysicalDamagePage>(vehicleInformation.FactorFlow.Last());
                }
            }

            //3.3 Select vehicle classification override and select next
            if (vehicleInformation.ClassificationOverride != null)
            {
                VehicleClassificationOverridePage vehicleClassificationOverridePage = new VehicleClassificationOverridePage();
                // verification
                vehicleClassificationOverridePage.ValidateVehicleClassificationOverridePageDisplayed();

                for (int i = 0; i < vehicleInformation.ClassificationOverride.Length - 1; i++)
                {
                    vehicleClassificationOverridePage.SelectClassificationOverride<VehicleClassificationOverridePage>(vehicleInformation.ClassificationOverride[i]);
                }
                vehicleClassificationOverridePage.SelectClassificationOverride<VehiclePhysicalDamagePage>(vehicleInformation.ClassificationOverride.Last());
            }
            VehiclePhysicalDamagePage vehicleInfoPhysicalDamagePage = new VehiclePhysicalDamagePage();
            // Verification
            vehicleInfoPhysicalDamagePage.ValidateVehiclePhysicalDamagePageDisplayed();

            //4. Set Physical Damage and select next
            AddAnotherVehiclePage addAnotherVehiclePage = vehicleInfoPhysicalDamagePage.EnterPhysicalDamage(vehicleInformation.PhysicalDamage).SelectNextButton<AddAnotherVehiclePage>();
            // Verification
            addAnotherVehiclePage.ValidateAddAnotherVehiclePageDisplayed();

            return addAnotherVehiclePage;
        }

        public static VehicleEntryPage YesMoreVehicle(this AddAnotherVehiclePage addAnotherVehiclePage)
        {
            ParameterValidator.ValidateNotNull(addAnotherVehiclePage, "AddAnotherVehicle");
            // Select decision when prompted to add more vehicle
            addAnotherVehiclePage.SelectAddMoreVehicleButton(AnswerOption.Yes);
            // Select Next Button
            VehicleEntryPage vinEntryPage = addAnotherVehiclePage.SelectNextButton<VehicleEntryPage>();
            //Verification
            vinEntryPage.ValidateVehicleEntryPageDisplayed();
            return vinEntryPage;
        }

        public static DriverEnterPage NoMoreVehicle(this AddAnotherVehiclePage addAnotherVehiclePage)
        {
            ParameterValidator.ValidateNotNull(addAnotherVehiclePage, "addAnotherVehiclePage");

            // Select decsion when prompted to add more vehicle
            addAnotherVehiclePage.SelectAddMoreVehicleButton(AnswerOption.No);
            // Select next button
            DriverEnterPage driverEnterPage = addAnotherVehiclePage.SelectNextButton<DriverEnterPage>();
            //Verification
            driverEnterPage.ValidateDriverEnterPageDisplayed();
            return driverEnterPage;
        }

        public static T NoMoreVehicle<T>(this AddAnotherVehiclePage addAnotherVehiclePage) where T : BasePage
        {
            ParameterValidator.ValidateNotNull(addAnotherVehiclePage, "AddAnotherVehiclePage");

            // Select decsion when prompted to add more vehicle
            addAnotherVehiclePage.SelectAddMoreVehicleButton(AnswerOption.No);
            // Select next button
            dynamic currentPage = addAnotherVehiclePage.SelectNextButton<T>();
            if (typeof(T) == typeof(VehicleOverviewPage))
            {
                //verification
                currentPage.ValidateOverViewPageDisplayed();
            }
            else
            {
                //verification
                currentPage.ValidateDriverEnterPageDisPlayed();
            }
            return currentPage;
        }
    }
}
