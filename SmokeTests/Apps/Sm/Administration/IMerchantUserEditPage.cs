using SmokeTests.Apps.Models;

namespace SmokeTests.Apps.Sm.Administration
{
    public interface IMerchantUserEditPage : IBasePage
    {
        IMerchantUserEditPage Authenticate(string username, string password);
        ISecurityMerchantUsersPage SetRoles(params string[] roles);
    }
}
