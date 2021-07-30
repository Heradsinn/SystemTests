namespace SmokeTests.Apps
{
    /// <summary>
    /// Interface for factories to handle page instatiations.
    /// Examples of usage include starting a web test on a given page via navigation.
    /// </summary>
    public interface IPageFactory
    {
        /// <summary>
        /// Handling any necessary newing up and navigating to a given page.
        /// Allowing absolute paths or segments to be specified too.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriSegment"></param>
        /// <returns>Page instance</returns>
        T GetPage<T>(string uriSegment = null, bool navigate = true) where T : IBasePage;
    }
}
