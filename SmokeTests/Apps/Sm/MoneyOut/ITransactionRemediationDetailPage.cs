namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface ITransactionRemediationDetailPage : IBasePage
    {
        ITransactionRemediationDetailPage Authenticate(string username, string password);
        ITransactionRemediationPage PassCARiskCheck(string comment);
    }
}
