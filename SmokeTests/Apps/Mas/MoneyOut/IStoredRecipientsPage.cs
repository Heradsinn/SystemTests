namespace SmokeTests.Apps.Mas.MoneyOut
{
    public interface IStoredRecipientsPage : IBasePage
    {
        IStoredRecipientsPage Authenticate(string username, string password);
        IStoredRecipientCreatePage NewStoredRecipient();
        int NumberOfEntries();
    }
}
