using VitesseSDK;

namespace SmokeTests.Apps.TransactionApi
{
    /// <summary>
    /// Factory to handle creation of the Vitesse SDK client interfaces.
    /// </summary>
    public interface ISdkClientFactory
    {
        IAccountApiClient CreateAccountApiClient(Configuration config);
        IAccountMovementsApiClient CreateAccountMovementsApiClient(Configuration config);
        IDeferredTransactionApiClient CreateDeferredTransactionApiClient(Configuration config);
        IForexApiClient CreateForexApiClient(Configuration config);
        IImmediateTransactionApiClient CreateImmediateTransactionApiClient(Configuration config);
        IRuleApiClient CreateRuleApiClient(Configuration config);
        IStoredRecipientApiClient CreateStoredRecipientApiClient(Configuration config);
        ITransactionsApiClient CreateTransactionsApiClient(Configuration config);
    }
}
