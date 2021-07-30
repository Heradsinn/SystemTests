using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm
{
    using _ = SmHomePage;

    [Url("home")]
    public class SmHomePage : BasePage<_>, ISmHomePage
    {
        public SmHomePage(IPageFactory pageFactory, ILogger logger, ISmMenu smMenu) : base(pageFactory, logger)
        {
            _logger = logger;
            _smMenu = smMenu;
        }

        public ISmHomePage Authenticate(string username, string password)
        {
            _logger.LogInformation($"Navigating to and authenticating SmHomePage...");
            return base.Authenticate<ISmHomePage>(username, password);
        }

        public ISmMenu GetMenu() => _smMenu;

        private readonly ILogger _logger;
        private readonly ISmMenu _smMenu;
    }
}
