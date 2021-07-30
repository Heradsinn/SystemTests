using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.MoneyOut;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class BankBatchesTests : WebTestFixture
    {
        [TestMethod]
        public void BankBatches_CompleteBatch_IsSuccessful()
        {
            // Arrange
            TestInit(TestRun.SmokeTestData.BankBatchToCompleteId);

            // Act
            var completeBatchResult = _bankBatchPage
                .CompleteBatch()
                .GetPageContent();

            // Assert
            completeBatchResult
                .Should()
                .NotContain(ErrorCode)
                .And.Contain("PendingCompletion", "because completing a journal should queue it for completion");
        }

        [TestMethod]
        public void BankBatches_LockBatch_IsSuccessful()
        {
            // Arrange
            TestInit(TestRun.SmokeTestData.BankBatchToLockId);

            // Act
            var lockBatchResult = _bankBatchPage
                .LockBatch()
                .GetPageContent();

            // Assert
            lockBatchResult
                .Should()
                .NotContain(ErrorCode)
                .And.Contain("Locked", "because locking a batch should lock it");
        }

        [TestMethod]
        public void BankBatches_ExportBatch_IsSuccessful()
        {
            // Arrange
            TestInit(TestRun.SmokeTestData.BankBatchToExportId);

            // Act
            var exportBatchResult = _bankBatchPage
                .ExportBatch()
                .GetPageContent();

            // Assert
            exportBatchResult
                .Should()
                .NotContain(ErrorCode)
                .And.ContainAny("Exporting", "Exported");
        }

        private void TestInit(int bankBatchId)
        {
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            _bankBatchPage = GetPage<IBankBatchPage>(bankBatchId.ToString())
                .Authenticate(username, password);
        }

        private IBankBatchPage _bankBatchPage;
    }
}
