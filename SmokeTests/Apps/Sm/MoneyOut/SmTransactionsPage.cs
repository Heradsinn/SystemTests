using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = SmTransactionsPage;

    [Url("Transactions")]
    public class SmTransactionsPage : BasePage<_>, ISmTransactionsPage
    {
        public KendoGrid<TransactionRow, _> Entries { get; private set; }

        public SmTransactionsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public ISmTransactionsPage Authenticate(string username, string password)
        {
            _logger.LogInformation($"Logging into the SM Transactions page...");
            return base.Authenticate<ISmTransactionsPage>(username, password);
        }

        public int NumberOfEntries()
        {
            var entriesCount = Entries.Rows.Count.Value;
            _logger.LogInformation($"'{entriesCount}' number of transactions exist in the current transaction grid view.");
            return entriesCount;
        }

        public class TransactionRow : KendoGridRow<_>
        {
            public TextInput<_> TransactionRequestId { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
