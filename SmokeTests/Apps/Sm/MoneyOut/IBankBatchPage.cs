namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface IBankBatchPage : IBasePage
    {
        IBankBatchPage Authenticate(string username, string password);
        IBankBatchPage LockBatch();
        IBankBatchPage ExportBatch();
        IBankBatchPage CompleteBatch();
    }
}
