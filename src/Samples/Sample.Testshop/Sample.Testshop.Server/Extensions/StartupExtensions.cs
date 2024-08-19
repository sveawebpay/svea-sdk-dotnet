using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sample.Testshop.Server.Configurations;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
namespace Sample.Testshop.Server.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {

            var sveaApiUrlsSettings = builder.Configuration.GetSection("SveaApiUrls");
            builder.Services.Configure<SveaApiUrls>(sveaApiUrlsSettings);
            var sveaApiUrls = sveaApiUrlsSettings.Get<SveaApiUrls>();
            var checkoutUri = new Uri(builder.Configuration.GetValue<string>("SveaApiUrls:CheckoutApiUri") ?? "");
            var paymentAdminUri = new Uri(builder.Configuration.GetValue<string>("SveaApiUrls:PaymentAdminApiUri") ?? "");

            // We can later add retry Policy like in the old sample testshop
            builder.Services.AddHttpClient("checkoutApi", client => client.BaseAddress = checkoutUri)
              .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            builder.Services.AddHttpClient("paymentAdminApi", client => client.BaseAddress = paymentAdminUri)
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);
            var credentialsSettings = builder.Configuration.GetSection("Credentials");
            builder.Services.Configure<List<Credentials>>(credentialsSettings);
            var credentials = credentialsSettings.Get<List<Credentials>>();
            var credential = credentials?.FirstOrDefault();

            var merchantSettingsSettings = builder.Configuration.GetSection("MerchantSettings");
            builder.Services.Configure<MerchantSettings>(sveaApiUrlsSettings);
            var merchantSettings = merchantSettingsSettings.Get<MerchantSettings>();

            builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


            builder.Services.Configure<List<MarketSettings>>(builder.Configuration.GetSection("Markets"));

            builder.Services.AddSveaClient(checkoutUri, paymentAdminUri, credential?.MerchantId, credential?.Secret);


            return builder;

        }
        public static IServiceCollection AddSveaClient(this IServiceCollection services, Uri checkoutUri, Uri paymentAdminUri, string merchantId, string secret)
        {
            services.AddHttpClient("checkoutApi", client => client.BaseAddress = checkoutUri)
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            services.AddHttpClient("paymentAdminApi", client => client.BaseAddress = paymentAdminUri)
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            services.AddTransient(s =>
            {

                var httpContextAccessor = s.GetService<IHttpContextAccessor>();
                var currentMarket = httpContextAccessor.HttpContext.Request.Headers["merchantId"].FirstOrDefault() ?? "SE";
                var credentials = s.GetService<IOptions<List<Credentials>>>()?.Value;
                var credential = credentials?.FirstOrDefault(x => x.MerchantId.Equals(currentMarket, StringComparison.InvariantCultureIgnoreCase));
                var httpClientFactory = s.GetService<IHttpClientFactory>();
                var checkoutApiHttpClient = httpClientFactory.CreateClient("checkoutApi");
                var paymentAdminApiHttpClient = httpClientFactory.CreateClient("paymentAdminApi");
                return new Svea.WebPay.SDK.SveaWebPayClient(checkoutApiHttpClient, paymentAdminApiHttpClient, new Svea.WebPay.SDK.Credentials(credential?.MerchantId ?? merchantId, credential?.Secret ?? secret), s.GetService<ILogger>());
            });

            return services;
        }

        private static HttpClientHandler RedirectHandler => new HttpClientHandler { AllowAutoRedirect = false };
    }
}
