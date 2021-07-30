using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.Administration
{
    using _ = SecurityMerchantUsersPage;
 
    [Url("SecurityMerchantUsers")]
    public class SecurityMerchantUsersPage : BasePage<_>, ISecurityMerchantUsersPage
    {
        
        public SecurityMerchantUsersPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public ISecurityMerchantUsersPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating SecurityMerchantUsersPage...");
            return base.Authenticate<ISecurityMerchantUsersPage>(username, password);
        }

        private readonly ILogger _logger;

    }
}
