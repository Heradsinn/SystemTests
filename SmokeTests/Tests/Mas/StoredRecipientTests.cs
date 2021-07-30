using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas.MoneyOut;
using SmokeTests.Utils;
using System;
using VitesseSDK.Models;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class StoredRecipientTests : WebTestFixture
    {
        [TestMethod]
        public void StoredRecipient_CreateNew_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            var storedRecipientsPage = GetPage<IStoredRecipientsPage>()
                .Authenticate(username, password);

            var numberOfEntriesPrior = storedRecipientsPage.NumberOfEntries();

            // Act
            var createStoredRecipientResult = storedRecipientsPage
                .NewStoredRecipient()
                .EnterRecipientDetails(DefaultStoredRecipient())
                .Save();

            // Assert
            createStoredRecipientResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because creating a new stored recipient should load the stored recipient page");

            createStoredRecipientResult.NumberOfEntries().Should().BeGreaterThan(numberOfEntriesPrior, "because the grid count should increase when a stored recipient is created");
        }

        // TODO: Potentially use the builder pattern for test data creation
        private StoredRecipient DefaultStoredRecipient()
        {
            return new StoredRecipient
            {
                Name = "MAS Stored Recipient",
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
    }
}
