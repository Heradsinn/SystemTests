using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System.Threading.Tasks;
using VitesseSDK;
using VitesseSDK.Models.AccountMovements;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class AccountMovementsTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _accountMovementsApiClient = SdkClientFactory.CreateAccountMovementsApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task AccountMovementsApi_Get_ReturnsSuccess()
        {
            // Act
            var getAccountMovements = await _accountMovementsApiClient.GetListAsync(new AccountMovementQuery());

            // Assert
            getAccountMovements.Should().NotBeNull();
        }

        [TestMethod]
        public async Task AccountMovementsApi_Create_ReturnsSuccess()
        {
            // Act
            var createAccountMovement = await _accountMovementsApiClient.CreateAsync(DefaultAccountMovement());

            // Assert
            AssertApiResponse(createAccountMovement);
        }

        // TODO: Potentially use the builder pattern for test data creation
        private AccountMovementRequest DefaultAccountMovement()
        {
            return new AccountMovementRequest
            {
                CreditAccountId = TestRun.SmokeTestData.GbpAccountId,
                DebitAccountId = TestRun.SmokeTestData.EurAccountId,
                DebitAmount = 1,
                MerchantReference = "api smoke merref",
                Narrative = "api smoke narrative",
                PaymentReference = "api smoke payref"
            };
        }

        private IAccountMovementsApiClient _accountMovementsApiClient;
    }
}
