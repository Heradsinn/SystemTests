namespace SmokeTests.Apps.Mas.MoneyOut
{
    public interface IMerchantBatchesPage : IBasePage
    {
        IMerchantBatchesPage Authenticate(string username, string password);
        IMerchantBatchUploadPage UploadBatch();
        int NumberOfEntries();
    }
}
