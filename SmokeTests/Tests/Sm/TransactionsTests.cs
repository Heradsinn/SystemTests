using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.MoneyOut;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class TransactionsTests : WebTestFixture
    {
        [TestMethod]
        public void SmTransactions_Load_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            // Act
            var transactionsPageResult = GetPage<ISmTransactionsPage>()
                .Authenticate(username, password);

            // Assert
            transactionsPageResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode);

            transactionsPageResult.NumberOfEntries()
                .Should()
                .BePositive("because all transactions should load in the grid");
        }
    }
}
