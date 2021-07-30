namespace SmokeTests.Apps.Sm
{
    public interface ISmLoginPage : IBasePage
    {
        ISmHomePage Login(string username, string password);
    }
}
