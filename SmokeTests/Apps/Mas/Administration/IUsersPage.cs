namespace SmokeTests.Apps.Mas.Administration
{
    public interface IUsersPage : IBasePage
    {
        IUsersPage Authenticate(string username, string password);
        IUsersCreatePage NewUser();
        int NumberOfEntries();
    }
}
