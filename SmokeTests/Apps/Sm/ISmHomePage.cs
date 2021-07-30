namespace SmokeTests.Apps.Sm
{
    public interface ISmHomePage : IBasePage
    {
        ISmHomePage Authenticate(string username, string password);
        ISmMenu GetMenu();
    }
}
