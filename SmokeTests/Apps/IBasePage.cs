namespace SmokeTests.Apps
{
    /// <summary>
    /// Base interface for all web pages, SM or MAS. Only absolute common page/browser functions live here
    /// </summary>
    public interface IBasePage
    {
        string GetPageContent();
        string GetPageTitle();
    }
}
