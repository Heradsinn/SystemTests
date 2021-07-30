using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class AuthenticationTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            _masHomePage = GetPage<IMasLoginPage>()
                .Login(username, password);
        }

        [TestMethod]
        public void Mas_Login_IsSuccessful()
        {
            // Act
            var masLoginResult = _masHomePage
                .GetPageContent();
            
            // Assert
            masLoginResult
                .Should()
                .NotContain(ErrorCode, $"because the {App.Mas} Home page was expected after logging in");
        }

        [TestMethod]
        public void Mas_Logoff_IsSuccessful()
        {
            // Act
            var masLogoffResult = _masHomePage
                .GetMenu().Logoff()
                .GetPageContent();
            
            // Assert
            masLogoffResult
                .Should()
                .NotContain(ErrorCode, $"because the {App.Mas} Login page was expected after logging off");
        }

        private IMasHomePage _masHomePage;
    }
}
