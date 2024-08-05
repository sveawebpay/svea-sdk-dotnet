using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Svea.WebPay.SDK;
using System.Net.Sockets;

namespace Sample.Testshop.Server.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {


            var checkoutUri = new Uri(builder.Configuration.GetValue<string>("SveaApiUrls:CheckoutApiUri") ?? "");
            var paymentAdminUri = new Uri(builder.Configuration.GetValue<string>("SveaApiUrls:PaymentAdminApiUri") ?? "");

            // We can later add retry Policy like in the old sample testshop
            builder.Services.AddHttpClient("checkoutApi", client => client.BaseAddress = checkoutUri)
              .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            builder.Services.AddHttpClient("paymentAdminApi", client => client.BaseAddress = paymentAdminUri)
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            //builder.Services.AddTransient(s =>
            //{

            //    //var httpContextAccessor = s.GetService<IHttpContextAccessor>();
            //    //var marketService = s.GetService<Market>();
            //    //var currentMarket = httpContextAccessor.HttpContext.Request.Headers["merchantId"].FirstOrDefault() ?? marketService.MarketId;
            //    //var credentials = s.GetService<IOptions<List<Credentials>>>()?.Value;
            //    //var credential = credentials?.FirstOrDefault(x => x.MarketId.Equals(currentMarket, StringComparison.InvariantCultureIgnoreCase));
            //    //var httpClientFactory = s.GetService<IHttpClientFactory>();
            //    //var checkoutApiHttpClient = httpClientFactory.CreateClient("checkoutApi");
            //    //var paymentAdminApiHttpClient = httpClientFactory.CreateClient("paymentAdminApi");
            //    //return new SveaWebPayClient(checkoutApiHttpClient, paymentAdminApiHttpClient, new Svea.WebPay.SDK.Credentials(credential?.MerchantId ?? merchantId, credential?.Secret ?? secret), s.GetService<ILogger>());
            //});

            return builder;

        }
        private static HttpClientHandler RedirectHandler => new HttpClientHandler { AllowAutoRedirect = false };
    }
}
