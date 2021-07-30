using Atata;
using Atata.Bootstrap;
using Microsoft.Extensions.Logging;
using SmokeTests.Apps.Sm.Accounting;

namespace SmokeTests.Apps.Sm
{
    using _ = SmMenu;

    public class SmMenu : Page<_>, ISmMenu
    {
        public SmMenu<_> SmMenuComponent { get; private set; }

        public SmMenu(IPageFactory pageFactory, ILogger logger)
        {
            _pageFactory = pageFactory;
            _logger = logger;
            InitComponent();
        }

        public ISmLoginPage Logoff()
        {
            _logger.LogInformation("Logging off SM...");
            SmMenuComponent.RightMenu.Logoff.Click();
            return _pageFactory.GetPage<ISmLoginPage>();
        }

        public IJournalsPage AccountingJournals()
        {
            SmMenuComponent.Accounting.Journals.Click();
            return _pageFactory.GetPage<IJournalsPage>();
        }

        private readonly IPageFactory _pageFactory;
        private readonly ILogger _logger;
    }

    public class SmMenu<T> : BSNavbar<T> where T : PageObject<T>
    {
        public SmMenuRight RightMenu { get; private set; }

        [Term(TermMatch.StartsWith)]
        public AccountingDropdown Accounting { get; private set; }

        [FindByCss(".navbar-right")]
        public class SmMenuRight : BSDropdownToggle<T>
        {
            [Term("Log off")]
            public Link<T> Logoff { get; private set; }
        }

        public class AccountingDropdown : BSDropdownToggle<T>
        {
            public Link<T> Journals { get; private set; }
        }
    }
}
