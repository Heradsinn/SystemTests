using SmokeTests.Apps.Sm.Accounting;

namespace SmokeTests.Apps.Sm
{
    public interface ISmMenu
    {
        ISmLoginPage Logoff();
        IJournalsPage AccountingJournals();
    }
}
