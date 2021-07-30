namespace SmokeTests.Apps.Sm.Administration
{
    public interface ISecurityMerchantUsersPage: IBasePage
    {
        ISecurityMerchantUsersPage Authenticate(string username, string password);
    }
}
