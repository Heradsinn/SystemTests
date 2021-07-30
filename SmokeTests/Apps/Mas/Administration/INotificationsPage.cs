namespace SmokeTests.Apps.Mas.Administration
{
    public interface INotificationsPage : IBasePage
    {
        INotificationsPage Authenticate(string username, string password);
        INotificationsCreatePage NewNotification();
        int NumberOfEntries();
    }
}
