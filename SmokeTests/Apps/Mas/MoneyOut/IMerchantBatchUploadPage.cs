namespace SmokeTests.Apps.Mas.MoneyOut
{
    public interface IMerchantBatchUploadPage : IBasePage
    {
        IMerchantBatchesPage ImportBatch(string path);
    }
}
