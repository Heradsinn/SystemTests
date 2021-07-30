using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System.Threading.Tasks;
using VitesseSDK;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class ForexTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _forexApiClient = SdkClientFactory.CreateForexApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task ForexApi_Create_ReturnsSuccess()
        {
            // Act
            var createForexGroup = await _forexApiClient.CreateAsync(1);

            // Assert
            AssertApiResponse(createForexGroup);
        }

        [TestMethod]
        public async Task ForexApi_Get_ReturnsSuccess()
        {
            // Act
            var getForexGroup = await _forexApiClient.RetrieveAsync(TestRun.SmokeTestData.ForexRateGroupId);

            // Assert
            getForexGroup.Should().NotBeNull();
            getForexGroup.Currencies.Should().NotBeNull();
        }

        private IForexApiClient _forexApiClient;
    }
}
