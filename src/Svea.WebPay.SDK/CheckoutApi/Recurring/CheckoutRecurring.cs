using System;
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

        public async Task<RecurringOrder> CreateRecurringOrderAsync(CreateRecurringOrderModel createRecurringOrder, string recurringToken, bool configureAwait = false)
        {
            var url = new Uri($"/api/tokens/{recurringToken}/orders", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPost<RecurringOrder>(url, createRecurringOrder, configureAwait);
            return data;
        }

        public async Task<RecurringToken> GetRecurringTokenAsync(string recurringToken, bool configureAwait = false)
        {
            var url = new Uri($"/api/tokens/{recurringToken}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpGet<RecurringToken>(url, configureAwait);
            return data;
        }
        public async Task<RecurringOrder> GetRecurringOrderAsync(string recurringToken, long orderId, bool configureAwait = false)
        {
            var url = new Uri($"/api/tokens/{recurringToken}/orders/{orderId}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpGet<RecurringOrder>(url, configureAwait);
            return data;
        }
        public async Task<ChangePaymentMethodResponse> ChangePaymentMethodAsync(ChangepaymentMethodModel changePaymentMethodModel, string recurringToken, bool configureAwait = false)
        {
            var url = new Uri($"/api/tokens/{recurringToken}/payment-methods", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPost<ChangePaymentMethodResponse>(url, changePaymentMethodModel, configureAwait);
            return data;
        }

    }
}
