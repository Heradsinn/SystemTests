namespace SmokeTests.Apps.Mas
{
    public interface IMasHomePage : IBasePage
    {
        IMasHomePage Authenticate(string username, string password);
        IMasMenu GetMenu();
    }
}
