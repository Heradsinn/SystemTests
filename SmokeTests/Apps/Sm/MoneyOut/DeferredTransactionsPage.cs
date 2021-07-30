using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = DeferredTransactionsPage;

    [Url("DeferredTransactions")]
    public class DeferredTransactionsPage : BasePage<_>, IDeferredTransactionsPage
    {
        public DeferredTransactionsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IDeferredTransactionsPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating DeferredTransactionsPage...");
            return base.Authenticate<IDeferredTransactionsPage>(username, password);
        }

        private readonly ILogger _logger;
    }
}
