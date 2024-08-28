using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.CheckoutApi.Utility
{
    public class CheckoutUtility
    {
        private readonly SveaHttpClient _sveaHttpClient;

        public CheckoutUtility(SveaHttpClient sveaHttpClient)
        {
            _sveaHttpClient = sveaHttpClient;
        }

        /// <summary>
        /// This request returns a list of available B2C/B2B part payment campaigns.
        /// </summary>
        /// <param name="isCompany">Required parameter indicating if it is for the company or private person</param>
        /// <param name="amount">Is used to calculate a monthly amount. Setting the parameter amount will also filter out campaigns that do not meet the amount requirement.</param>
        /// <param name="configureAwait"></param>
        /// <returns>A list of campaigns.</returns>
        /// <remarks>For example, this method can be used so "Part payment widget" can be displayed in Product pages.
        /// MonthlyAmount from Response is the value used in the widget.</remarks>
        public async Task<List<CampaignCodeInfo>> GetPartPaymentCampaigns(bool isCompany, long? amount = null, bool configureAwait = false)
        {
            var url = $"/api/util/GetAvailablePartPaymentCampaigns?isCompany={isCompany}" + (amount.HasValue ? $"&amount={amount}" : "");
            var uri = new Uri(url, UriKind.Relative);
            var data = await _sveaHttpClient.HttpGet<List<CampaignCodeInfo>>(uri, configureAwait);
            return data;
        }
    }
}
