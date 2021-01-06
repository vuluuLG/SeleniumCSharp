using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;

namespace Framework.Test.UITests.SharedStep
{
    public static class Customer
    {
        public static EntityTypePage EnterDotNumber(this DotNumberPage dotNumberPage, string dotNumber)
        {
            ParameterValidator.ValidateNotNull(dotNumberPage, "DotNumberPage");

            // Enter @DOT
            EntityTypePage entityTypePage;
            if (dotNumber != null)
            {
                // If valid DOT, select next
                entityTypePage = dotNumberPage.InputDotNumber(dotNumber).SelectNextButton();
            }
            else
            {
                // If customer does not have DOT/unkown proceed
                entityTypePage = dotNumberPage.SelectUnknownDotNumber();
            }

            //verification
            entityTypePage.ValidateEntityTypePageDisplayed();
            return entityTypePage;
        }

        public static VehicleInfoPage EnterCustomerInformation(this EffectiveDatePage effectiveDatePage, CustomerInformation customerInformation)
        {
            ParameterValidator.ValidateNotNull(effectiveDatePage, "EffectiveDatePage");
            ParameterValidator.ValidateNotNull(customerInformation, "Customer Information");

            //1. Enter customer @effectivedate and select next
            DotNumberPage dotNumberPage = effectiveDatePage.SelectEffectiveDate(customerInformation.EffectiveDate).SelectNextButton();
            // verification
            dotNumberPage.ValidateDotNumberPageDisplayed();

            //2. Enter @DOTInfo
            EntityTypePage entityTypePage = dotNumberPage.EnterDotNumber(customerInformation.DotNumber);

            //3. Select @EntityType and select next
            BusinessInformationPage businessInformationPage = entityTypePage.SelectEntityType(customerInformation.EntityType)
                                                                     .SelectNextButton();
            // verification
            businessInformationPage.ValidateBusinessInformationPageDisplayed();

            //4. If valid DOT was given, confrim business @name and @address and select next
            // If no Dot or invalid DOT was given, complete @name and @address and select next
            if (customerInformation.FilledOutBusinessInformation != null)
            {
                businessInformationPage.ValidateBusinessNameIsFilledOut(customerInformation.FilledOutBusinessInformation.CustomerName)
                                       .ValidateDBAIsFilledOut(customerInformation.FilledOutBusinessInformation.DBA)
                                       .ValidateAddressFieldsAreFilledOut(customerInformation.FilledOutBusinessInformation.Address);
            }

            if (customerInformation.BusinessInformation != null)
            {
                businessInformationPage.InputBusinessInformation(customerInformation.BusinessInformation);
            }

            PrimaryOfficerPage primaryOfficerPage = businessInformationPage.SelectNextButton();
            //verification
            primaryOfficerPage.ValidatePrimaryOfficerPageDisplayed();

            //5. Enter Primary officer information (@POName, @POAddress) and select next
            CustomerInfoPage customerInfoPage = new CustomerInfoPage();
            if (customerInformation.EntityType == "Individual")
            {
                // verification
                primaryOfficerPage.ValidatePrimaryOfficerNameAreFilledOut(customerInformation.POName).ValidatePrimaryOfficerNameDisabled();
                // Enter POAddress
                primaryOfficerPage.InputPrimaryOfficerAddress(customerInformation.POAddress).SelectNextButton();
            }
            else
            {
                //Enter POName and POAddress
                primaryOfficerPage.InputPrimaryOfficer(customerInformation.POName, customerInformation.POAddress).SelectNextButton();
            }

            //verification
            customerInfoPage.ValidateCustomerInfoPageDisplayed();

            //6. Enter @LiabilityLosses and select Next
            VehicleInfoPage vehicleInfoPage = customerInfoPage.EnterCustomerInfo(customerInformation.CustomerAdditionalInformation).SelectNextButton();

            //verification
            vehicleInfoPage.ValidateVehicleInfoPageDisplayed();
            return vehicleInfoPage;
        }
    }
}