using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.Json;
using Svea.WebPay.SDK.Tests.Helpers;
using Svea.WebPay.SDK.Tests.Helpers.DataSamples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Checkout
{
    public class PartPaymentCampaignTests : TestBase
    {

        [Fact]
        public async Task ShouldReturnPartPaymentCampaigns()
        {
            // Arrange
            var expectedOrder = JsonSerializer.Deserialize<List<CampaignCodeInfo>>(UtilityDataSamples.GetPartPaymentCampaigns, JsonSerialization.Settings);
            var sveaClient = SveaClient(CreateHandlerMock(UtilityDataSamples.GetPartPaymentCampaigns));

            // Act
            var actualPartPaymentCampaigns = await sveaClient.Checkout.Utility.GetPartPaymentCampaigns(true);

            // Assert
            Assert.Equal(expectedOrder.Count, actualPartPaymentCampaigns.Count);
        }
    }
}
