namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface IDeferredTransactionGroupAddLiquidityPage : IBasePage
    {
        IDeferredTransactionGroupAddLiquidityPage Authenticate(string username, string password);
        IDeferredTransactionsPage ConfirmAddLiquidity();
    }
}
