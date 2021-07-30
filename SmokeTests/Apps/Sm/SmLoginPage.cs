using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm
{
    using _ = SmLoginPage;

    public class SmLoginPage : BasePage<_>, ISmLoginPage
    {
        public SmLoginPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public ISmHomePage Login(string username, string password)
        {
            _logger.LogInformation($"Navigating to and authenticating SmLogingPage...");
            return base.Authenticate<ISmHomePage>(username, password);
        }

        private readonly ILogger _logger;
    }
}
