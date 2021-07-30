using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas.Administration;
using SmokeTests.Apps.Models;
using SmokeTests.Utils;
using System.Collections.Generic;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class ApiTokenTests : WebTestFixture
    {
        [TestMethod]
        public void ApiToken_Create_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            var apiPage = GetPage<IApiPage>()
                .Authenticate(username, password);

            var numberOfTokenEntriesPrior = apiPage.NumberOfEntries();

            // Act
            var createApiTokenResult = apiPage
                .Create()
                .CreateToken(new List<string> { ApiRoles.AccountMovementsCreate });

            // Assert
            createApiTokenResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because creating a new api token should load the api page");

            createApiTokenResult.NumberOfEntries().Should().BeGreaterThan(numberOfTokenEntriesPrior, "because the grid count should increase when a api token is created");
        }
    }
}
