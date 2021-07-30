using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = BankBatchPage;

    [Url("BankBatchDetails/Overview")]
    public class BankBatchPage : BasePage<_>, IBankBatchPage
    {
        [Term("Lock")]
        public Button<_> LockButton { get; private set; }

        [Term("Export")]
        public Button<_> ExportButton { get; private set; }

        [Term("Complete")]
        public Button<_> CompleteButton { get; private set; }

        public BankBatchPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IBankBatchPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating BankBatchPage...");
            return base.Authenticate<IBankBatchPage>(username, password);
        }

        public IBankBatchPage CompleteBatch()
        {
            _logger.LogInformation("Completing batch...");
            return CompleteButton.Click();
        }

        public IBankBatchPage ExportBatch()
        {
            _logger.LogInformation("Exporting batch...");
            return ExportButton.Click();
        }

        public IBankBatchPage LockBatch()
        {
            _logger.LogInformation("Locking batch...");
            return LockButton.Click();
        }

        private readonly ILogger _logger;
    }
}
