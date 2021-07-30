namespace SmokeTests.Apps.Mas.Administration
{
    public interface IApiPage : IBasePage
    {
        IApiPage Authenticate(string username, string password);
        IApiCreateTokenPage Create();
        int NumberOfEntries();
    }
}
