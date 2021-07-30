using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = TransactionRemediationPage;

    [Url("TransactionRemediation")]
    public class TransactionRemediationPage : BasePage<_>, ITransactionRemediationPage
    {
        public TransactionRemediationPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public ITransactionRemediationPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating TransactionRemediationPage...");
            return Authenticate<_>(username, password);
        }

        private readonly ILogger _logger;
    }
}
