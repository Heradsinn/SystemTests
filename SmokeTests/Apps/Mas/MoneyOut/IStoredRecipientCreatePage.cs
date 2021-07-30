using VitesseSDK.Models;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    public interface IStoredRecipientCreatePage : IBasePage
    {
        IStoredRecipientCreatePage Authenticate(string username, string password);
        IStoredRecipientCreatePage EnterRecipientDetails(StoredRecipient storedRecipient);
        IStoredRecipientsPage Save();
        IStoredRecipientsPage Cancel();
    }
}
