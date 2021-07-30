using VitesseSDK;

namespace SmokeTests.Apps.TransactionApi
{
    public class SdkClientFactory : ISdkClientFactory
    {
        public IAccountApiClient CreateAccountApiClient(Configuration config)
        {
            return new AccountApiClient(config);
        }

        public IAccountMovementsApiClient CreateAccountMovementsApiClient(Configuration config)
        {
            return new AccountMovementsApiClient(config);
        }

        public IDeferredTransactionApiClient CreateDeferredTransactionApiClient(Configuration config)
        {
            return new DeferredTransactionApiClient(config);
        }

        public IForexApiClient CreateForexApiClient(Configuration config)
        {
            return new ForexClient(config);
        }

        public IImmediateTransactionApiClient CreateImmediateTransactionApiClient(Configuration config)
        {
            return new ImmediateTransactionApiClient(config);
        }

        public IRuleApiClient CreateRuleApiClient(Configuration config)
        {
            return new RuleApiClient(config);
        }

        public IStoredRecipientApiClient CreateStoredRecipientApiClient(Configuration config)
        {
            return new StoredRecipientApiClient(config);
        }

        public ITransactionsApiClient CreateTransactionsApiClient(Configuration config)
        {
            return new TransactionsClient(config);
        }
    }
}
