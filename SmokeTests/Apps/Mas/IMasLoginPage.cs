namespace SmokeTests.Apps.Mas
{
    public interface IMasLoginPage : IBasePage
    {
        IMasHomePage Login(string username, string password);
    }
}
