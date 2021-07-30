using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = NotificationsPage;

    [Url("Notification")]
    public class NotificationsPage : BasePage<_>, INotificationsPage
    {
        [FindById("create")]
        public Button<_> CreateNewNotification{ get; private set; }

        public KendoGrid<NotificationRow, _> Entries { get; private set; }

        public NotificationsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public INotificationsPage Authenticate(string username, string password)
        {
            _logger.LogInformation($"Navigating to and authenticating NotificationsPage");
            return base.Authenticate<INotificationsPage>(username, password);
        }

        public INotificationsCreatePage NewNotification()
        {
            _logger.LogInformation($"Creating a new notification...");
            return base.ClickAndGetPage<INotificationsCreatePage>(CreateNewNotification);
        }

        public int NumberOfEntries()
        {
            var entriesCount = Entries.Rows.Count.Value;
            return entriesCount;
        }

        public class NotificationRow : KendoGridRow<_>
        {
            public Text<_> Event { get; private set; }
            public Text<_> Notification { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
