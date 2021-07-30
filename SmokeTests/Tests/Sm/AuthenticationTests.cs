using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class AuthenticationTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _smLoginPage = GetPage<ISmLoginPage>();
            _username = TestRun.SmokeTestData.SmCreator;
            _password = TestRun.SmokeTestData.UserPassword;
        }

        [TestMethod]
        public void Sm_Login_IsSuccessful()
        {
            // Act
            var masLoginResult = _smLoginPage
                .Login(_username, _password)
                .GetPageContent();

            // Assert
            masLoginResult
                .Should()
                .NotContain(ErrorCode, $"because the {App.Sm} Home page was expected after logging in");
        }

        [TestMethod]
        public void Sm_Logoff_IsSuccessful()
        {
            // Act
            var masLogoffResult = _smLoginPage
                .Login(_username, _password)
                .GetMenu().Logoff()
                .GetPageContent();

            // Assert
            masLogoffResult
                .Should()
                .NotContain(ErrorCode, $"because the {App.Sm} Login page was expected after logging off");
        }

        private ISmLoginPage _smLoginPage;
        private string _username;
        private string _password;
    }
}
