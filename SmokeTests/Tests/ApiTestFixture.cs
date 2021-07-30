using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.TransactionApi;
using SmokeTests.Utils;
using VitesseSDK;
using VitesseSDK.Models;

namespace SmokeTests.Tests
{
    /// <summary>
    /// Start and end of each API test, any new API test should inherit from this class.
    /// </summary>
    public abstract class ApiTestFixture
    {
        public TestContext TestContext { get; set; }

        protected ILogger Logger;
        protected ISdkClientFactory SdkClientFactory;
        protected ISdkConfigFactory SdkConfigFactory;

        [TestInitialize]
        public void Setup()
        {
            // Register api test dependencies
            var serviceProvider = TestRun.ApiDependencies()
                .AddScoped((a) => TestContext)
                .AddScoped<ILogger, MsTestLogger>()
                .BuildServiceProvider();
            _scope = serviceProvider.CreateScope();

            // Resolve api test dependencies
            Logger = _scope.ServiceProvider.GetService<ILogger>();
            SdkClientFactory = _scope.ServiceProvider.GetService<ISdkClientFactory>();
            SdkConfigFactory = _scope.ServiceProvider.GetService<ISdkConfigFactory>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _scope.Dispose();
        }

        protected Configuration DefaultConfiguration()
        {
            return SdkConfigFactory.Create(TestRun.SmokeTestData.ApiToken, TestRun.TransactionApiUrl);
        }

        protected void AssertApiResponse(Response apiResponse)
        {
            using (new AssertionScope())
            {
                apiResponse.IsSuccessful.Should().BeTrue();
                apiResponse.Errors.Should().BeNull();
            }
        }

        private IServiceScope _scope;
    }
}
