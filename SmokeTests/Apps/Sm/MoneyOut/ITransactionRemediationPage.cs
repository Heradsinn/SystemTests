namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface ITransactionRemediationPage : IBasePage
    {
        ITransactionRemediationPage Authenticate(string username, string password);
    }
}
