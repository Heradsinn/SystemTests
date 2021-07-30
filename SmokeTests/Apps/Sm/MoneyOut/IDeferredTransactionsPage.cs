using System.Collections.Generic;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    public interface IDeferredTransactionsPage : IBasePage
    {
        IDeferredTransactionsPage Authenticate(string username, string password);
    }
}
