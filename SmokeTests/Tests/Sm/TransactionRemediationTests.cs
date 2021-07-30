using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.MoneyOut;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class TransactionRemediationTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            var riskProcessId = TestRun.SmokeTestData.TxToRemediateId.ToString();
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            _transactionRemediationDetailPage = GetPage<ITransactionRemediationDetailPage>(uriSegment: riskProcessId)
                .Authenticate(username, password);
        }

        [TestMethod]
        public void TransactionRemediation_PassCAHighRiskCheck_IsSuccessful()
        {
            // Act
            var remediationResultPage = _transactionRemediationDetailPage
                .PassCARiskCheck("Smoke Test");

            // Assert
            remediationResultPage
                .GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because passing a risk check should not throw an error")
                .And.Contain("The remediation was saved successfully", "because passing a risk check should successfully save and complete the remediation");
        }

        private ITransactionRemediationDetailPage _transactionRemediationDetailPage;
    }
}
