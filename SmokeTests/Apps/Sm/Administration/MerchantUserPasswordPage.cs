using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.Administration
{
    using _ = MerchantUserPasswordPage;

    [Url("SecurityMerchantUsers/SetPassword")]
    public class MerchantUserPasswordPage : BasePage<_>, IMerchantUserPasswordPage
    {
        [FindById("NewPassword")]
        public PasswordInput<_> NewPasswordInput { get; private set; }

        [FindById("ConfirmPassword")]
        public PasswordInput<_> ConfirmPasswordInput { get; private set; }

        [FindByValue("Set Password")]
        public Clickable<_> SetPasswordClickable { get; private set; }

        public MerchantUserPasswordPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMerchantUserPasswordPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating MerchantUserPasswordPage...");
            return base.Authenticate<IMerchantUserPasswordPage>(username, password);
        }

        public ISecurityMerchantUsersPage SetPassword(string newPassword)
        {
            _logger.LogInformation($"Setting password of merchant user to {newPassword}...");
            NewPasswordInput.Set(newPassword);
            ConfirmPasswordInput.Set(newPassword);
            return base.ClickAndGetPage<ISecurityMerchantUsersPage>(SetPasswordClickable);
        }

        private readonly ILogger _logger;
    }
}
