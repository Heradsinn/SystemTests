using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System.Threading.Tasks;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class AccountsTests : ApiTestFixture
    {
        [TestMethod]
        public async Task AccountsApi_GetAccounts_ReturnsSuccess()
        {
            // Arrange
            var accountsApiClient = SdkClientFactory.CreateAccountApiClient(DefaultConfiguration());

            // Act
            var getAccountsResult = await accountsApiClient.GetAccountsAsync();

            // Assert
            getAccountsResult.Should().NotBeNullOrEmpty();
        }
    }
}
