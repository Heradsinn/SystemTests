using Atata;
using Atata.Bootstrap;
using Microsoft.Extensions.Logging;
using SmokeTests.Apps.Mas.Administration;
using SmokeTests.Apps.Mas.MoneyOut;

namespace SmokeTests.Apps.Mas
{
    using _ = MasMenu;

    public class MasMenu : Page<_>, IMasMenu
    {
        public MasMenu<_> MasMenuComponent { get; private set; }

        public MasMenu(IPageFactory pageFactory, ILogger logger)
        {
            _pageFactory = pageFactory;
            _logger = logger;
            InitComponent();
        }

        public IMasLoginPage Logoff()
        {
            _logger.LogInformation("Logging off MAS...");
            MasMenuComponent.RightMenu.Logoff.Click();
            return _pageFactory.GetPage<IMasLoginPage>();
        }

        public IStoredRecipientsPage MoneyOutStoredRecipients()
        {
            _logger.LogInformation($"Navigating to Money Out > Stored Recipients...");
            MasMenuComponent.MoneyOut.StoredRecipients.Click();
            return _pageFactory.GetPage<IStoredRecipientsPage>();
        }

        public IApiPage AdministrationApi()
        {
            _logger.LogInformation($"Navigating to Administration > API...");
            MasMenuComponent.Administration.Api.Click();
            return _pageFactory.GetPage<IApiPage>();
        }

        private readonly IPageFactory _pageFactory;
        private readonly ILogger _logger;
    }

    public class MasMenu<T> : BSNavbar<T> where T : PageObject<T>
    {
        public MasMenuRight RightMenu { get; private set; }

        [Term(TermMatch.StartsWith)]
        public MoneyOutDropdown MoneyOut { get; private set; }

        [Term(TermMatch.StartsWith)]
        public AdministrationDropdown Administration { get; private set; }

        [FindByCss(".navbar-right")]
        public class MasMenuRight : BSDropdownToggle<T>
        {
            [Term("Log off")]
            public Link<T> Logoff { get; private set; }
        }

        public class MoneyOutDropdown : BSDropdownToggle<T>
        {
            public Link<T> StoredRecipients { get; private set; }
        }

        public class AdministrationDropdown : BSDropdownToggle<T>
        {
            [Term(TermCase.Upper)]
            public Link<T> Api { get; private set; }
        }
    }
}
