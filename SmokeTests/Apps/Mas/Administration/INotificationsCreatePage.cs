using SmokeTests.Apps.Models;

namespace SmokeTests.Apps.Mas.Administration
{
    public interface INotificationsCreatePage : IBasePage
    {
        INotificationsCreatePage EnterBaseNotificationDetails(NotificationModel baseNotification);
        INotificationsCreatePage EnterAccountEmailNotificationDetails(AccountEmailNotificationModel accountEmailNotificationModel);
        INotificationsCreatePage EnterHttpNotificationDetails(HttpNotificationModel httpNotificationModel);
        INotificationsPage Create();
    }
}
