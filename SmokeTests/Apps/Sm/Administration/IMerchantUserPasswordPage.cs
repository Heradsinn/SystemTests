namespace SmokeTests.Apps.Sm.Administration
{
    public interface IMerchantUserPasswordPage : IBasePage
    {
        IMerchantUserPasswordPage Authenticate(string username, string password);
        ISecurityMerchantUsersPage SetPassword(string newPassword);
    }
}
