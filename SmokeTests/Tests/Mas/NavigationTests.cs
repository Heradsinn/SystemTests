using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class NavigationTests : WebTestFixture
    {
        [TestMethod]
        public void Mas_NavigateMenu_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            // Act
            var navigateMenuStoredRecipientPage = GetPage<IMasLoginPage>()
                .Login(username, password)
                .GetMenu()
                .MoneyOutStoredRecipients()
                .GetPageContent();

            // Assert
            navigateMenuStoredRecipientPage
                .Should()
                .NotContain(ErrorCode, "because the Stored Recipients page was expected after navigating the menu")
                .And.Contain("Stored Recipients");
        }
    }
}
