using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.MoneyOut;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class DeferredTransactionsTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _username = TestRun.SmokeTestData.SmApprover;
            _password = TestRun.SmokeTestData.UserPassword;
        }

        [TestMethod]
        public void DeferredTransactions_ConfirmPaid_IsSuccessful()
        {
            // Arrange
            var dtxId = TestRun.SmokeTestData.DeferredTxToConfirmPaidId.ToString();

            // Act
            var confirmPaidResult = GetPage<IDeferredTransactionGroupPaidPage>(dtxId)
                .Authenticate(_username, _password)
                .ConfirmPaid();

            // Assert
            confirmPaidResult
                .GetPageContent()
                .Should()
                .NotContain(ErrorCode);
        }

        [TestMethod]
        public void DeferredTransactions_AddLiquidity_IsSuccessful()
        {
            // Arrange
            var dtxId = TestRun.SmokeTestData.DeferredTxToAddLiquidityId.ToString();

            // Act
            var addLiquidityResult = GetPage<IDeferredTransactionGroupAddLiquidityPage>(dtxId)
                .Authenticate(_username, _password)
                .ConfirmAddLiquidity();

            // Assert
            addLiquidityResult
                .GetPageContent()
                .Should()
                .NotContain(ErrorCode);
        }

        private string _username;
        private string _password;
    }
}
