using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas
{
    using _ = MasLoginPage;

    public class MasLoginPage : BasePage<_>, IMasLoginPage
    {
        public MasLoginPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMasHomePage Login(string username, string password)
        {
            _logger.LogInformation($"Logging into MAS...");
            return base.Authenticate<IMasHomePage>(username, password);
        }

        private readonly ILogger _logger;
    }
}
