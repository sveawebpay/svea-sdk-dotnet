using System;
using Atata;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Sample.AspNetCore.SystemTests.Services;
#if RELEASE
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Collections;
#endif

namespace Sample.AspNetCore.SystemTests.Test.Base
{
    
    using static Drivers;

    [TestFixture(DriverAliases.Chrome)]
    public abstract class TestBase
    {
        private readonly string _driverAlias;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
#if DEBUG
            AtataContext.GlobalConfiguration.
                UseChrome().
                    WithOptions(DriverOptionsFactory.GetDriverOptions(Driver.Chrome) as ChromeOptions).
                UseFirefox().
                    WithOptions(DriverOptionsFactory.GetDriverOptions(Driver.Firefox) as FirefoxOptions).
                UseVerificationTimeout(TimeSpan.FromSeconds(3)).
                UseElementFindTimeout(TimeSpan.FromSeconds(15)).
                UseWaitingTimeout(TimeSpan.FromSeconds(30)).
                LogConsumers.AddNUnitTestContext().
                WithMinLevel(LogLevel.Trace).
                ScreenshotConsumers.AddFile().
                        WithDirectoryPath(() => $@"Logs\{AtataContext.BuildStart:yyyy-MM-dd HH_mm_ss}").
                        WithFileName(screenshotInfo => $"{AtataContext.Current.TestName} - {screenshotInfo.PageObjectFullName}").
                UseTestName(() => $"[{_driverAlias}]{TestContext.CurrentContext.Test.Name}");
#endif
        }


        [SetUp]
        public void SetUp()
        {
#if DEBUG
            AtataContext.Configure()
                .UseDriver(_driverAlias)
                    .UseBaseUrl("https://localhost:44345/")
            .Build();
            AtataContext.Current.Driver.Maximize();
#elif DEV
            AtataContext.Configure()
                .UseDriver(_driverAlias)
                    .UseBaseUrl("https://svea-webpay-sdk-001-dev.azurewebsites.net/")
            .Build();
            AtataContext.Current.Driver.Maximize();
#elif RELEASE
            var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddEnvironmentVariables()
             .Build();

            AtataContext.Configure()
                .UseChrome()
                .WithOptions(DriverOptionsFactory.GetDriverOptions(Driver.Chrome) as ChromeOptions)
                .UseBaseUrl(config.GetSection("SampleWebsite").GetSection("Url").Value)
                .Build();
            AtataContext.Current.Driver.Maximize();
#endif
        }

        protected TestBase(string driverAlias) => _driverAlias = driverAlias;

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TestContext.Out.WriteLine(PageSource());
            }

            AtataContext.Current?.CleanUp();
        }


        [OneTimeTearDown]
        public void GlobalDown()
        {
            foreach (Driver driverType in Enum.GetValues(typeof(Driver)))
                WebDriverCleanerService.KillWebDriverProcess(WebDriverCleanerService.DriverNames[driverType]);
        }

        public static string PageSource()
        {
            return $"------ Start Page content ------"
                + AtataContext.Current.Driver.Url
                + Environment.NewLine
                + Environment.NewLine
                + AtataContext.Current.Driver.PageSource
                + Environment.NewLine
                + Environment.NewLine
                + "------ End Page content ------";
        }
    }
}