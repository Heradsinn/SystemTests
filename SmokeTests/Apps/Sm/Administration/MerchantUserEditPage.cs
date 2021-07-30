using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;
using SmokeTests.Apps.Models;

namespace SmokeTests.Apps.Sm.Administration
{
    using _ = MerchantUserEditPage;

    [Url("SecurityMerchantUsers/Edit")]
    public class MerchantUserEditPage : BasePage<_>, IMerchantUserEditPage
    {

        [FindByLabel("Roles", Visibility = Visibility.Any)]
        public KendoMultiSelect<_> RolesSelect { get; private set; }

        [FindByValue("Save")]
        public Clickable<_> SaveClickable { get; private set; }

        public MerchantUserEditPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMerchantUserEditPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating MerchantUserEditPage...");
            return base.Authenticate<IMerchantUserEditPage>(username, password);
        }

        public ISecurityMerchantUsersPage SetRoles(params string[] roles)
        {
            foreach (var role in roles)
            {
                RolesSelect.Add(role);
            }
            SaveClickable.Focus();
            return base.ClickAndGetPage<ISecurityMerchantUsersPage>(SaveClickable);
        }

        private readonly ILogger _logger;
    }
}
