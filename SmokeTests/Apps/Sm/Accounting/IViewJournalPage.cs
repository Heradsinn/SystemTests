namespace SmokeTests.Apps.Sm.Accounting
{
    public interface IViewJournalPage : IBasePage
    {
        IViewJournalPage AddEntry(string debitAccount, string creditAccount, decimal amount, string entryType, string paymentRef, string merchantRef, string narrative);
        IViewJournalPage ImportJournal(string path);
        IViewJournalPage SubmitJournal();
        IViewJournalPage ApproveJournal();
        IViewJournalPage Authenticate(string username, string password);
        int NumberOfEntries();
    }
}
