namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface IDeferredTransactionGroupPaidPage : IBasePage
    {
        IDeferredTransactionGroupPaidPage Authenticate(string username, string password);
        IDeferredTransactionsPage ConfirmPaid();
    }
}
