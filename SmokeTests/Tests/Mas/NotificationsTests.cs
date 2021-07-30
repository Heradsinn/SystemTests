using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas.Administration;
using SmokeTests.Apps.Models;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class NotificationsTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _notificationsPage = GetPage<INotificationsPage>()
                .Authenticate(TestRun.SmokeTestData.MasCreator, TestRun.SmokeTestData.UserPassword);
        }

        [TestMethod]
        public void Notifications_CreateNew_EmailNotify_IsSuccessful()
        {
            // Arrange
            var notification = DefaultEmailNotification();

            var numberOfEntriesPrior = _notificationsPage.NumberOfEntries();

            // Act
            var createNotificationResult = _notificationsPage
                .NewNotification()
                .EnterBaseNotificationDetails(notification)
                .EnterAccountEmailNotificationDetails(notification)
                .Create();

            // Assert
            createNotificationResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because creating a new email notification should load the Notifications page");

            createNotificationResult.NumberOfEntries().Should().BeGreaterThan(numberOfEntriesPrior, "because the grid count should increase when a email notification is created");
        }

        [TestMethod]
        public void Notifications_CreateNew_HttpNotify_IsSuccessful()
        {
            // Arrange
            var notification = DefaultHttpNotification();

            var numberOfEntriesPrior = _notificationsPage.NumberOfEntries();

            // Act
            var createNotificationResult = _notificationsPage
                .NewNotification()
                .EnterBaseNotificationDetails(notification)
                .EnterHttpNotificationDetails(notification)
                .Create();

            // Assert
            createNotificationResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because creating a new http notification should load the Notifications page");

            createNotificationResult.NumberOfEntries().Should().BeGreaterThan(numberOfEntriesPrior, "because the grid count should increase when a http notification is created");
        }

        // TODO: Potentially use the builder pattern for test data creation
        private AccountEmailNotificationModel DefaultEmailNotification()
        {
            return new AccountEmailNotificationModel
            {
                NotificationEvent = "Account Debited",
                NotificationType = "Account Debit Email",
                Email = "testing@vitesepsp.com",
                TransactionType = "Payout"
            };
        }

        private HttpNotificationModel DefaultHttpNotification()
        {
            return new HttpNotificationModel
            {
                NotificationEvent = "Transaction Completed",
                NotificationType = "Transaction Completed Webhook",
                ServerAddress = "https://testing.com",
                Token = "qwerty+!"
            };
        }

        private INotificationsPage _notificationsPage;
    }
}
