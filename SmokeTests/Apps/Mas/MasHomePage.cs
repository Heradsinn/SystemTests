using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas
{
    using _ = MasHomePage;

    [Url("home")]
    public class MasHomePage : BasePage<_>, IMasHomePage
    {
        public MasHomePage(IPageFactory pageFactory, ILogger logger, IMasMenu masMenu) : base(pageFactory, logger)
        {
            _masMenu = masMenu;
        }

        public IMasHomePage Authenticate(string username, string password)
        {
            return base.Authenticate<IMasHomePage>(username, password);
        }

        public IMasMenu GetMenu() => _masMenu;

        private readonly IMasMenu _masMenu;
    }
}
