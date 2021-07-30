using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System.Threading.Tasks;
using VitesseSDK.Models;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class TransactionSearchTests : ApiTestFixture
    {
        [TestMethod]
        public async Task TransactionsApi_Search_ReturnsSuccess()
        {
            // Arrange
            var txApiClient = SdkClientFactory.CreateTransactionsApiClient(DefaultConfiguration());

            // Act
            var searchTransactionsResult = await txApiClient.SearchAsync(new TransactionSearchRequest());

            // Assert
            searchTransactionsResult.Should().NotBeNull();
            searchTransactionsResult.CurrentItems.Should().NotBeEmpty();
        }
    }
}
