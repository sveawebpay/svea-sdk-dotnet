using System;
using System.Net.Http;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

using Svea.WebPay.SDK;

namespace Sample.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSveaClient(this IServiceCollection services, Uri checkoutUri, Uri paymentAdminUri, string merchantId, string secret)
        {
            services.AddHttpClient("checkoutApi", client => client.BaseAddress = checkoutUri)
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(SleepDurations))
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            services.AddHttpClient("paymentAdminApi", client => client.BaseAddress = paymentAdminUri)
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(SleepDurations))
                .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

            services.AddTransient(s =>
            {
                var httpClientFactory = s.GetService<IHttpClientFactory>();
                var checkoutApiHttpClient = httpClientFactory.CreateClient("checkoutApi");
                var paymentAdminApiHttpClient = httpClientFactory.CreateClient("paymentAdminApi");
                return new SveaWebPayClient(checkoutApiHttpClient, paymentAdminApiHttpClient, new Svea.WebPay.SDK.Credentials(merchantId, secret), s.GetService<ILogger>());
            });

            return services;
        }

        private static TimeSpan[] SleepDurations =>
            new[] {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            };

        private static HttpClientHandler RedirectHandler => new HttpClientHandler { AllowAutoRedirect = false };
    }
}