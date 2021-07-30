using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.Administration;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class SecurityTests : WebTestFixture
    {
        [TestInitialize]
        public void TestInit()
        {
            // Arrange
            _username = TestRun.SmokeTestData.SmCreator;
            _password = TestRun.SmokeTestData.UserPassword;
        }

        [TestMethod]
        public void Security_MerchantUserSetPassword_IsSuccessful()
        {
            // Arrange
            var merchantUserId = TestRun.SmokeTestData.MasUserToSetPasswordId;

            // Act
            var setPasswordResult = GetPage<IMerchantUserPasswordPage>(merchantUserId)
                .Authenticate(_username, _password)
                .SetPassword("New_Vit123");

            // Assert
            setPasswordResult
                .GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because setting a new password should not throw an error screen")
                .And.Contain("User password change was successful", "because setting a new password should prompt a confirmation message");
        }

        [TestMethod]
        public void Security_MerchantUserEditRoles_IsSuccessful()
        {
            // Arrange
            var merchantUserId = TestRun.SmokeTestData.MasUserToEditId;
            string[] roles = 
            { 
                "Payment Request Approver",
                "Single Payment Approver"
            };

            // Act
            var setRolesResult = GetPage<IMerchantUserEditPage>(merchantUserId)
                .Authenticate(_username, _password)
                .SetRoles(roles);

            // Assert
            setRolesResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode, "because editing a user should not throw an error screen")
                .And.Contain("User changes were successfully applied", "because setting user roles should prompt a confirmation message");

        }

        private string _username;
        private string _password;
    }
}
