using Svea.WebPay.SDK.CheckoutApi.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class CheckoutRecurring
    {
        private readonly SveaHttpClient _sveaHttpClient;

        public CheckoutRecurring(SveaHttpClient sveaHttpClient)
        {
            _sveaHttpClient = sveaHttpClient;
        }

        public async Task<OrderData> CreateRecurringOrderAsync(CreateRecurringOrderModel createRecurringOrder, string recurringToken, bool configureAwait = false)
        {
            var url = new Uri($"/api/tokens/{recurringToken}/orders", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPost<Data>(url, createRecurringOrder, configureAwait);
            return data;
        }

    }
}
