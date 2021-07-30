using Atata;
using Atata.Bootstrap;
using Atata.KendoUI;
using Atata.WebDriverSetup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.IE;
using Scrutor;
using SmokeTests.Apps;
using SmokeTests.Apps.Mas;
using SmokeTests.Apps.Sm;
using SmokeTests.Apps.TransactionApi;
using SmokeTests.TestData;
using SmokeTests.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace SmokeTests
{
    /// <summary>
    /// Executed once per entire test run to setup/cleanup global dependencies
    /// </summary>
    [TestClass]
    public static class TestRun
    {
        public static string MasUrl { get; private set; }
        public static string SmUrl { get; private set; }
        public static string TransactionApiUrl { get; private set; }
        public static SmokeTestData SmokeTestData { get; private set; }
        private static string DeploymentDirectory { get; set; }
        private static string Browser { get; set; }

        [AssemblyInitialize]
        public static void AssemblySetup(TestContext context)
        {
            DeploymentDirectory = context.DeploymentDirectory;

            // Set browser
            Browser = context.GetContextProperty("Browser");

            // Set env details
            MasUrl = context.GetContextProperty("MasUrl");
            SmUrl = context.GetContextProperty("SmUrl");
            TransactionApiUrl = context.GetContextProperty("TransactionApiUrl");

            // Set smoke data for driven tests
            SetupSmokeData();

            DriverSetup.GetDefaultConfiguration(BrowserNames.InternetExplorer)
                .WithX32Architecture();

            // Set up web test config
            AtataContext.GlobalConfiguration
                .Attributes.Global.Add(new WaitForGridToLoadAttribute()
                {
                    TargetType = typeof(KendoGrid<,>),
                    TargetParentType = typeof(BasePage<>)
                })
                .Attributes.Global.Add(new FindByCssAttribute("div>button")
                {
                    TargetType = typeof(BSDropdownToggle<>),
                    TargetParentType = typeof(KendoGridRow<>)
                })
                .Attributes.Global.Add(new FormatAttribute("dd-MMM-yyyy HH:mm:ss")
                {
                    TargetType = typeof(DateTime<>),
                    TargetParentType = typeof(KendoGridRow<>)
                })
                .UseDriver(Browser)
                .ConfigureInternetExplorer().WithOptions(new InternetExplorerOptions // because IE11 sucks!
                {
                    IgnoreZoomLevel = true,
                    IntroduceInstabilityByIgnoringProtectedModeSettings = true
                })
                .AutoSetUpDriverToUse();
        }

        public static IServiceCollection WebDependencies() =>
            new ServiceCollection()
                .AddScoped<IMasMenu, MasMenu>()
                .AddScoped<ISmMenu, SmMenu>()
                .AddScoped<IPageFactory, AtataPageFactory>()
                .Scan(a => a.FromAssemblyOf<IBasePage>()
                    .AddClasses(classes => classes.AssignableTo<IBasePage>())
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()); // may need to use transient if using the pagefactory multiple times per test

        public static IServiceCollection ApiDependencies() =>
            new ServiceCollection()
                .AddScoped<ISdkClientFactory, SdkClientFactory>()
                .AddScoped<ISdkConfigFactory, SdkConfigFactory>();

        public static string GetProjectFilePath(string fileName)
        {
            var files = Directory.GetFiles(DeploymentDirectory, fileName, SearchOption.AllDirectories);
            if (files.Length < 1)
                throw new Exception($@"The file was not found in the bin directory. Expecting file: {DeploymentDirectory}\{fileName}");

            return files[0];
        }

        private static void SetupSmokeData()
        {
            var smokeTestDataPath = GetProjectFilePath("smoketestdata.json");
            SmokeTestData = JsonSerializer.Deserialize<SmokeTestData>(File.ReadAllText(smokeTestDataPath));
        }
    }
}
