using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = DeferredTransactionGroupAddLiquidityPage;

    [Url("DeferredTransactions/AddLiquidity")]
    public class DeferredTransactionGroupAddLiquidityPage : BasePage<_>, IDeferredTransactionGroupAddLiquidityPage
    {
        public Button<_> ConfirmAddLiquidityButton { get; private set; }

        public DeferredTransactionGroupAddLiquidityPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IDeferredTransactionGroupAddLiquidityPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating DeferredTransactionGroupAddLiquidityPage...");
            return base.Authenticate<IDeferredTransactionGroupAddLiquidityPage>(username, password);
        }

        public IDeferredTransactionsPage ConfirmAddLiquidity()
        {
            _logger.LogInformation("Confirming Adding Liquidity to pay DTX...");
            return base.ClickAndGetPage<IDeferredTransactionsPage>(ConfirmAddLiquidityButton);
        }

        private readonly ILogger _logger;
    }
}
