using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Utils;
using System;
using System.Threading.Tasks;
using VitesseSDK;
using VitesseSDK.Models;

namespace SmokeTests.Tests.TransactionApi
{
    [TestClass]
    [TestCategoryApp(App.TransactionApi)]
    public class StoredRecipientTests : ApiTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _storedRecipientApiClient = SdkClientFactory.CreateStoredRecipientApiClient(DefaultConfiguration());
        }

        [TestMethod]
        public async Task StoredRecipientApi_Create_ReturnsSuccess()
        {
            // Act
            var createdStoredRecipient = await _storedRecipientApiClient.CreateAsync(DefaultStoredRecipient());

            // Assert
            AssertApiResponse(createdStoredRecipient);
        }

        [TestMethod]
        public async Task StoredRecipientApi_Get_ReturnsSuccess()
        {
            // Act
            var getStoredRecipient = await _storedRecipientApiClient.RetrieveAsync(TestRun.SmokeTestData.StoredRecipientId);

            // Assert
            getStoredRecipient.Should().NotBeNull();
        }

        [TestMethod]
        public async Task StoredRecipientApi_Delete_ReturnsSuccess()
        {
            // Arrange
            var createdStoredRecipient = await _storedRecipientApiClient.CreateAsync(DefaultStoredRecipient());

            // Act
            var getStoredRecipient = await _storedRecipientApiClient.DeleteAsync(createdStoredRecipient.Resource.ExternalStoredRecipientId);

            // Assert
            AssertApiResponse(getStoredRecipient);
        }

        // TODO: Potentially use the builder pattern for test data creation
        private StoredRecipient DefaultStoredRecipient()
        {
            return new StoredRecipient
            {
                Name = "Smoke Testing",
                Country = Country.GB,
                Currency = Currency.GBP,
                ExternalStoredRecipientId = Guid.NewGuid().ToString(),
                Account = new AccountDescriptor
                {
                    AccountNumber = "11111111",
                    SortCode = "111111"
                }
            };
        }

        private IStoredRecipientApiClient _storedRecipientApiClient;
    }
}
