namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface ISmTransactionsPage : IBasePage
    {
        ISmTransactionsPage Authenticate(string username, string password);
        int NumberOfEntries();
    }
}
