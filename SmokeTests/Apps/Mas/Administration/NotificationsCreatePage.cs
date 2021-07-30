using Atata;
using Microsoft.Extensions.Logging;
using SmokeTests.Apps.Models;
using SmokeTests.Utils;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = NotificationsCreatePage;

    [Url("Notification/Create")]
    public class NotificationsCreatePage : BasePage<_>, INotificationsCreatePage
    {
        [FindByAttribute("aria-owns", "Event_listbox")]
        public CustomKendoDropDownList<_> NotificationEvent { get; private set; }

        [FindByAttribute("aria-owns", "NotificationTypeId_listbox")]
        public CustomKendoDropDownList<_> NotificationType { get; private set; }

        [FindByAttribute("type", "submit")]
        public new Button<_> Submit { get; private set; }

        public EmailInput<_> EmailAddress { get; private set; }

        public Select<_> TransactionType { get; private set; }

        [FindById("ServerAddress")]
        public UrlInput<_> ServerAddress { get; private set; }

        public TextInput<_> Token { get; private set; }

        public NotificationsCreatePage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public INotificationsCreatePage EnterBaseNotificationDetails(NotificationModel baseNotification)
        {
            _logger.LogInformation($"Select notification event: '{baseNotification.NotificationEvent}' and type: '{baseNotification.NotificationType}'");
            NotificationEvent.Set(baseNotification.NotificationEvent);
            NotificationType.Set(baseNotification.NotificationType);
            return Submit.Click();
        }

        public INotificationsCreatePage EnterAccountEmailNotificationDetails(AccountEmailNotificationModel accountEmailNotificationModel)
        {
            _logger.LogInformation($"Entering email address: '{accountEmailNotificationModel.Email}' and transaction type: '{accountEmailNotificationModel.TransactionType}'");
            EmailAddress.Set(accountEmailNotificationModel.Email);
            return TransactionType.Set(accountEmailNotificationModel.TransactionType);
        }

        public INotificationsCreatePage EnterHttpNotificationDetails(HttpNotificationModel httpNotificationModel)
        {
            _logger.LogInformation($"Entering server address '{httpNotificationModel.ServerAddress}' and token '{httpNotificationModel.Token}'");
            ServerAddress.Set(httpNotificationModel.ServerAddress);
            return Token.Set(httpNotificationModel.Token);
        }

        public INotificationsPage Create()
        {
            return base.ClickAndGetPage<INotificationsPage>(Submit);
        }

        private readonly ILogger _logger;
    }
}
