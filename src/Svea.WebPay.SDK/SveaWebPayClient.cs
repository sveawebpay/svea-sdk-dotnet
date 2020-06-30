using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.PaymentAdminApi;

namespace Svea.WebPay.SDK
{
    public class SveaWebPayClient : ISveaClient
    {
        public SveaWebPayClient(HttpClient checkoutApihttpClient,
            HttpClient paymentAdminApiClient,
            Credentials credentials,
            ILogger logger = null)
        {
            if (!ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12))
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }

            if (checkoutApihttpClient == null)
            {
                throw new ArgumentNullException(nameof(checkoutApihttpClient));
            }

            if (paymentAdminApiClient == null)
            {
                throw new ArgumentNullException(nameof(paymentAdminApiClient));
            }

            if (checkoutApihttpClient.BaseAddress == null)
            {
                throw new ArgumentNullException(nameof(checkoutApihttpClient), $"{nameof(checkoutApihttpClient.BaseAddress)} cannot be null.");
            }

            if (paymentAdminApiClient.BaseAddress == null)
            {
                throw new ArgumentNullException(nameof(paymentAdminApiClient), $"{nameof(paymentAdminApiClient.BaseAddress)} cannot be null.");
            }


            var sveaLogger = logger ?? NullLogger.Instance;
            var checkoutApiClient = new SveaHttpClient(checkoutApihttpClient, credentials, sveaLogger);
            var paymentAdminClient = new SveaHttpClient(paymentAdminApiClient, credentials, sveaLogger);

            Checkout = new Checkout(checkoutApiClient);
            PaymentAdmin = new PaymentAdmin(paymentAdminClient);
        }

        public Checkout Checkout { get; }
        public PaymentAdmin PaymentAdmin { get; }
    }
}