namespace SmokeTests.Apps.Mas.Administration
{
    public interface IUsersCreatePage : IBasePage
    {
        IUsersCreatePage Authenticate(string username, string password);
        IUsersPage CreateUser(string email);
    }
}
