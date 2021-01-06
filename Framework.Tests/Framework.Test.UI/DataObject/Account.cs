using Framework.Test.Common.DataObject;

namespace Framework.Test.UI.DataObject
{
    public class Account : UserAccount
    {
        public bool Success { get; set; }
        public bool Remember { get; set; }
        public string FailureReason { get; set; }

        public Account()
        {
        }

        public Account(UserAccount baseAccount)
        {
            Email = baseAccount.Email;
            Password = baseAccount.Password;
            Username = baseAccount.Username;
            Agent = baseAccount.Agent;
            Success = true;
            Remember = false;
        }
    }
}
