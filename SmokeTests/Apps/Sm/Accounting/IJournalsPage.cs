namespace SmokeTests.Apps.Sm.Accounting
{
    public interface IJournalsPage : IBasePage
    {
        IJournalsPage Authenticate(string username, string password);
        IViewJournalPage CreateJournal(string reference);
        IViewJournalPage ViewJournal(string reference);
        IViewJournalPage ViewJournal(int JournalId);
    }
}
