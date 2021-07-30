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
    public class ImmediateTransactionsTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _immediateTransactionApiClient = SdkClientFactory.CreateImmediateTransactionApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task ImmediateTransactionsApi_Create_ReturnsSuccess()
        {
            // Act
            var createImmediateTx = await _immediateTransactionApiClient.CreateAsync(DefaultTransactionRequest());

            // Assert
            AssertApiResponse(createImmediateTx);
        }

        [TestMethod]
        public async Task ImmediateTransactionsApi_Validate_ReturnsSuccess()
        {
            // Act
            var validateImmediateTx = await _immediateTransactionApiClient.ValidateAsync(DefaultTransactionRequest());

            // Assert
            AssertApiResponse(validateImmediateTx);
        }

        [TestMethod]
        public async Task ImmediateTransactionsApi_Get_ReturnsSuccess()
        {
            // Act
            var getTransactionResult = await _immediateTransactionApiClient.RetrieveAsync(TestRun.SmokeTestData.TxToRetreive);
            
            // Assert
            getTransactionResult.Should().NotBeNull();
        }

        // TODO: Potentially use the builder pattern for test data creation
        private TransactionRequest DefaultTransactionRequest()
        {
            return new TransactionRequest
            {
                SendValue = 1,
                SendCurrency = Currency.GBP,
                Recipient = new Recipient
                {
                    Name = "Mr Smoke",
                    Country = Country.GB,
                    Currency = Currency.GBP,
                    RecipientReference = "Smoke testing",
                    Account = new AccountDescriptor
                    {
                        AccountNumber = "11111111",
                        SortCode = "111111"
                    }
                },
                ExternalReference1 = "Immediate Api Smoke Test"
            };
        }

        private IImmediateTransactionApiClient _immediateTransactionApiClient;
    }
}
