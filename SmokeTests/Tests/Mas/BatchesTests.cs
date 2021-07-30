using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Mas.MoneyOut;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Mas
{
    [TestClass]
    [TestCategoryApp(App.Mas)]
    public class BatchesTests : WebTestFixture
    {
        [TestMethod]
        public void Mas_Batches_Upload_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            const string fileName = "CombinedImporterBatch.csv";
            var path = TestRun.GetProjectFilePath(fileName);

            var merchantBatchesPage = GetPage<IMerchantBatchesPage>()
                .Authenticate(username, password);

            var numberOfEntriesPrior = merchantBatchesPage.NumberOfEntries();

            // Act
            var uploadBatchResult = merchantBatchesPage
                .UploadBatch()
                .ImportBatch(path);

            // Assert
            uploadBatchResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because uploading a batch should load the merchant batches page")
                .And.Contain("The file was successfully uploaded", "because the batch uploaded should be successful");

            uploadBatchResult.NumberOfEntries().Should().BeGreaterThan(numberOfEntriesPrior, "because the grid count should increase when a batch is uploaded");
        }

        [TestMethod]
        public void Mas_Batches_RequestApproval_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasCreator;
            var password = TestRun.SmokeTestData.UserPassword;
            var batchToRequestApproval = TestRun.SmokeTestData.MerchantBatchToRequestApproval.ToString();

            var merchantBatchDetailsPage = GetPage<IMerchantBatchDetailsPage>(batchToRequestApproval)
                .Authenticate(username, password);

            // Act
            var requestApprovalResult = merchantBatchDetailsPage.RequestApproval();

            // Assert
            requestApprovalResult.GetPageContent()
               .Should()
               .NotContain(ErrorCode, "because requesting approval on a batch should load the merchant batches page")
               .And.ContainAny("Approval Request Pending", "Approval Requested");
        }

        [TestMethod]
        public void Mas_Batches_ApproveBatch_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.MasApprover;
            var password = TestRun.SmokeTestData.UserPassword;
            var batchToApprove = TestRun.SmokeTestData.MerchantBatchToApprove.ToString();

            var merchantBatchDetailsPage = GetPage<IMerchantBatchDetailsPage>(batchToApprove)
                .Authenticate(username, password);

            // Act
            var approveResult = merchantBatchDetailsPage.ApproveBatch();

            // Assert
            approveResult.GetPageContent()
               .Should()
               .NotContain(ErrorCode, "because approving a batch should load the merchant batches page")
               .And.ContainAny("Approval Pending", "Approval Requested", "Processing", "Processed");
        }
    }
}
