using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas.Administration;
using SmokeTests.Utils;
using System;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class UsersTests : WebTestFixture
    {
        [TestMethod]
        public void Users_CreateNew_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            var usersPage = GetPage<IUsersPage>()
                .Authenticate(username, password);

            var numberOfUsersPrior = usersPage.NumberOfEntries();

            // Act
            var createUserResult = usersPage
                .NewUser()
                .CreateUser(CreateRandomEmailAddress());

            // Assert
            createUserResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because creating a new user should load the Users page");

            createUserResult.NumberOfEntries().Should().BeGreaterThan(numberOfUsersPrior, "because the grid count should increase when a new user is created");
        }

        //TODO: Potentially use the builder pattern for test data creation
        private string CreateRandomEmailAddress()
        {
            var random = new Random().Next(1, 50);
            return $"test{random}@vittest.com";
        }
    }
}