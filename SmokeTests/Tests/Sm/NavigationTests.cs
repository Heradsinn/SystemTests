using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class NavigationTests : WebTestFixture
    {
        [TestMethod]
        public void Sm_NavigateMenu_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            // Act
            var navigateMenuJournalsPage = GetPage<ISmLoginPage>()
                .Login(username, password)
                .GetMenu()
                .AccountingJournals()
                .GetPageContent();

            // Assert
            navigateMenuJournalsPage
                .Should()
                .NotContain(ErrorCode, "because the Journals page was expected after navigating the menu")
                .And.Contain("Journals");
        }
    }
}
