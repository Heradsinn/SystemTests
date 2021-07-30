using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitesseSDK;
using VitesseSDK.Models;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class DeferredTransactionsTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _deferredTransactionApiClient = SdkClientFactory.CreateDeferredTransactionApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_Create_ReturnsSuccess()
        {
            // Act
            var createDeferredTx = await _deferredTransactionApiClient.CreateAsync(DefaultTransactionRequest(TestRun.SmokeTestData.ForexRateGroupId));

            // Assert
            AssertApiResponse(createDeferredTx);
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_Validate_ReturnsSuccess()
        {
            // Act
            var validateDeferredTx = await _deferredTransactionApiClient.ValidateAsync(DefaultTransactionRequest(TestRun.SmokeTestData.ForexRateGroupId));

            // Assert
            AssertApiResponse(validateDeferredTx);
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_CreateWithGroup_ReturnsSuccess()
        {
            // Act
            var createDeferredTx = await _deferredTransactionApiClient.CreateWithGroupAsync(DefaultTransactionRequest(TestRun.SmokeTestData.ForexRateGroupId));

            // Assert
            AssertApiResponse(createDeferredTx);
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_CreateGroup_ReturnsSuccess()
        {
            // Arrange
            var dtxToGroupOne = await _deferredTransactionApiClient.CreateAsync(DefaultTransactionRequest(TestRun.SmokeTestData.ForexRateGroupId));
            var dtxToGroupTwo = await _deferredTransactionApiClient.CreateAsync(DefaultTransactionRequest(TestRun.SmokeTestData.ForexRateGroupId));

            // Act
            var groupDeferredTransactions = await _deferredTransactionApiClient.CreateGroupAsync(new List<Guid> { dtxToGroupOne.Resource.TransactionId, dtxToGroupTwo.Resource.TransactionId });

            // Assert
            AssertApiResponse(groupDeferredTransactions);
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_Get_ReturnsSuccess()
        {
            // Act
            var getTransactionResult = await _deferredTransactionApiClient.RetrieveAsync(TestRun.SmokeTestData.DeferredTxToRetreive);

            // Assert
            getTransactionResult.Should().NotBeNull();
        }

        [TestMethod]
        public async Task DeferredTransactionsApi_GetUngroupedTransactions_ReturnsSuccess()
        {
            // Act
            var getUngroupedTransactions = await _deferredTransactionApiClient.RetrieveUngroupedAsync(new UngroupedDeferredTransactionQuery());

            // Assert
            getUngroupedTransactions.Should().NotBeNull();
        }

        // TODO: Potentially use the builder pattern for test data creation
        private TransactionRequest DefaultTransactionRequest(int rateGroupId)
        {
            return new TransactionRequest
            {
                RateGroupId = rateGroupId,
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
                ExternalReference1 = "Deferred Api Smoke Test"
            };
        }

        private IDeferredTransactionApiClient _deferredTransactionApiClient;
    }
}
