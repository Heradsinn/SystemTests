using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System.Threading.Tasks;
using VitesseSDK;
using VitesseSDK.Models;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class RulesTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _ruleApiClient = SdkClientFactory.CreateRuleApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task RulesApi_GetDomesticRule_ReturnsSuccess()
        {
            // Act
            var getDomesticRulesResult = await _ruleApiClient.GetRulesAsync(Country.BR, Currency.BRL);

            // Assert
            getDomesticRulesResult.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task RulesApi_GetNonDomesticRule_ReturnsSuccess()
        {
            // Act
            var getNonDomesticRulesResult = await _ruleApiClient.GetRulesAsync(Country.FR, Currency.USD);

            // Assert
            getNonDomesticRulesResult.Should().NotBeNullOrEmpty();
        }

        private IRuleApiClient _ruleApiClient;
    }
}
