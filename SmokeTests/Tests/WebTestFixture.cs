using Atata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps;
using SmokeTests.Utils;
using System;
using System.IO;

namespace SmokeTests.Tests
{
    /// <summary>
    /// Start and end of each Web test, any new Web test should inherit from this class.
    /// </summary>
    public abstract class WebTestFixture
    {
        public const string ErrorCode = "Err-";
        public TestContext TestContext { get; set; }
        protected ILogger Logger;

        [TestInitialize]
        public void Setup()
        {
            // Register web test dependencies
            var serviceProvider = TestRun.WebDependencies()
                .AddScoped((a) => TestContext)
                .AddScoped<ILogger, MsTestLogger>()
                .AddScoped<AtataMsTestLogConsumer>()
                .BuildServiceProvider();

            _scope = serviceProvider.CreateScope();

            // Resolve web test dependencies
            Logger = _scope.ServiceProvider.GetService<ILogger>();
            _pageFactory = _scope.ServiceProvider.GetService<IPageFactory>();
            var atataMsTestLogConsumer = _scope.ServiceProvider.GetService<AtataMsTestLogConsumer>();

            _resultsFilePath = @$"{TestContext.TestResultsDirectory}\{TestContext.TestName}";

            // Configure web test context
            AtataContext.Configure()
                .UseBaseUrl(GetBaseUrl())
                .UseTestName(TestContext.TestName)
                .AddScreenshotFileSaving().WithFolderPath(_resultsFilePath).WithFileName(si => $"{si.Number:D2} - {si.PageObjectFullName}{si.Title?.Prepend(" - ")}")
                .AddLogConsumer(atataMsTestLogConsumer).WithMinLevel(Atata.LogLevel.Info)
                .Build().Driver.Maximize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Error || TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                AtataContext.Current?.Log.Screenshot("Failure");

            if (Directory.Exists(_resultsFilePath))
            {
                foreach (var file in Directory.GetFiles(_resultsFilePath))
                    TestContext.AddResultFile(file);
            }

            AtataContext.Current?.CleanUp();
            _scope.Dispose();
        }

        protected T GetPage<T>(string uriSegment = null) where T : IBasePage
        {
            return _pageFactory.GetPage<T>(uriSegment);
        }

        private string GetBaseUrl()
        {
            var testAppAttribute = GetType().GetAttribute<TestCategoryAppAttribute>();
            if (testAppAttribute == null)
                throw new ArgumentException("No app has been identified from the web test. A 'TestCategoryApp' attribute is required on the class.");

            return testAppAttribute.TestCategoryApp == App.Mas ? TestRun.MasUrl : TestRun.SmUrl;
        }

        private IServiceScope _scope;
        private IPageFactory _pageFactory;
        private string _resultsFilePath;
    }
}
