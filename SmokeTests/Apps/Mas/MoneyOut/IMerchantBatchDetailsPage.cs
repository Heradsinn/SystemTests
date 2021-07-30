namespace SmokeTests.Apps.Mas.MoneyOut
{
    public interface IMerchantBatchDetailsPage : IBasePage
    {
        IMerchantBatchDetailsPage Authenticate(string username, string password);
        IMerchantBatchesPage RequestApproval();
        IMerchantBatchesPage ApproveBatch();
    }
}
