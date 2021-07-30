using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = DeferredTransactionGroupPaidPage;

    [Url("DeferredTransactions/Paid")]
    public class DeferredTransactionGroupPaidPage : BasePage<_>, IDeferredTransactionGroupPaidPage
    {
        public Button<_> ConfirmPaidButton { get; private set; }

        public DeferredTransactionGroupPaidPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IDeferredTransactionGroupPaidPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating DeferredTransactionGroupPaidPage...");
            return base.Authenticate<IDeferredTransactionGroupPaidPage>(username, password);
        }

        public IDeferredTransactionsPage ConfirmPaid()
        {
            _logger.LogInformation("Confirming payment of deferred transaction group...");
            return base.ClickAndGetPage<IDeferredTransactionsPage>(ConfirmPaidButton);
        }

        private readonly ILogger _logger;
    }
}
