using Framework.Test.UI.Constant;
using Framework.Test.UI.DataObject;
using System;
using System.Linq;

namespace Framework.Test.UITests.TestData
{
    public static class SharedTestData
    {
        

        public static readonly string[] LeftNavigationHeaders =
        {
            LeftNavigationHeader.Business,
            LeftNavigationHeader.Customer,
            LeftNavigationHeader.Vehicles,
            LeftNavigationHeader.Drivers,
            LeftNavigationHeader.Coverage
        };

        public static readonly string[] CompletedLeftNavigationHeaders =
        {
            LeftNavigationHeader.Business,
            LeftNavigationHeader.Customer,
            LeftNavigationHeader.Vehicles,
            LeftNavigationHeader.Drivers,
            LeftNavigationHeader.Coverage,
            LeftNavigationHeader.Summary
        };

        public static Account[] Accounts;

        public static Account DefaultAccount
        {
            get { return GetDefaultAccountByAgent("breeze"); }
        }

        public static Account GetDefaultAccountByAgent(string agent)
        {
            try
            {
                return Accounts.Where(x => x.Agent.Equals(agent, StringComparison.CurrentCultureIgnoreCase)).First();
            }
            catch (Exception)
            {
                throw new Exception($"Can't get account of agent {agent}");
            }
        }
    }
}
