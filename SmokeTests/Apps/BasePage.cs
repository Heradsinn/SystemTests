using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps
{
    public class BasePage<T> : Page<T> where T : Page<T>
    {
        protected TextInput<T> Email { get; private set; }
        protected PasswordInput<T> Password { get; private set; }

        [FindByAttribute("type", TermCase.Lower)]
        protected Clickable<T> Submit { get; private set; }

        public BasePage(IPageFactory pageFactory, ILogger logger)
        {
            _pageFactory = pageFactory;
            _logger = logger;
        }
        
        public string GetPageContent()
        {
            return base.Content;
        }

        public string GetPageTitle()
        {
            return base.PageTitle;
        }

        protected TPage Authenticate<TPage>(string username, string password) where TPage : IBasePage
        {
            _logger.LogInformation($"Logging into {typeof(TPage).Name}...");
            Email.Set(username);
            Password.Set(password);
            return ClickAndGetPage<TPage>(Submit);
        }

        protected TPage ClickAndGetPage<TPage>(Control<T> component) where TPage : IBasePage
        {
            _logger.LogInformation($"Clicking {typeof(Control<T>).Name} and navigating to {typeof(TPage).Name}");
            component.Click();
            return _pageFactory.GetPage<TPage>(navigate: false);
        }

        private readonly IPageFactory _pageFactory;
        private readonly ILogger _logger;
    }
}
