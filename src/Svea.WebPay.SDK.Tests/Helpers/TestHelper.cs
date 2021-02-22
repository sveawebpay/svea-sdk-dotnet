namespace Svea.WebPay.SDK.Tests.Helpers
{
    using Microsoft.Extensions.Configuration;

    using Svea.WebPay.SDK.Tests.Models;

    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath = "")
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("114d902c-dbe0-4606-883a-cca99f92fd56")
                .AddEnvironmentVariables()
                .Build();
        }

        public static SveaConfiguration GetApplicationConfiguration(string outputPath = "")
        {
            var configuration = new SveaConfiguration();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                //.GetSection("Credentials")
                .Bind(configuration);

            return configuration;
        }
    }
}
