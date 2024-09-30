using System.Collections.Generic;
using Svea.WebPay.SDK.CheckoutApi;
using Xunit;
using Svea.WebPay.SDK.Tests.Builders;
using Svea.WebPay.SDK.Tests.Helpers;
using Svea.WebPay.SDK.Json;
using System.Linq;
using OrderRow = Svea.WebPay.SDK.CheckoutApi.OrderRow;

namespace Svea.WebPay.SDK.Tests.Checkout
{
    using Svea.WebPay.SDK.Tests.Helpers.DataSamples;
    using System.Text.Json;

    public class CreateOrderTests : TestBase
    {
        private readonly CheckoutOrderBuilder checkoutOrderBuilder = new CheckoutOrderBuilder();

        [Fact]
        public async System.Threading.Tasks.Task CreateOrder_Should_Serialize_AsExpectedForRecurringOrder()
        {
            // Arrange
            var expectedOrder = JsonSerializer.Deserialize<Data>(CheckoutDataSamples.CheckoutCreateRecurringOrderResponse, JsonSerialization.Settings);
            var sveaClient = SveaClient(CreateHandlerMock(CheckoutDataSamples.CheckoutCreateRecurringOrderResponse));
            var request = checkoutOrderBuilder.UseTestValues().AddRecurring().Build();

            // Act
            var actualOrder = await sveaClient.Checkout.CreateOrder(request);

            // Assert
            Assert.True(DataComparison.DataAreEqual(expectedOrder, actualOrder));
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateOrder_Should_Serialize_AsExpectedForOrderValidationr()
        {
            // Arrange
            var expectedOrder = JsonSerializer.Deserialize<Data>(CheckoutDataSamples.CheckoutCreateRecurringOrderResponse, JsonSerialization.Settings);
            var sveaClient = SveaClient(CreateHandlerMock(CheckoutDataSamples.CheckoutCreateRecurringOrderResponse));
            var request = checkoutOrderBuilder.UseTestValues().AddOrderValidation().Build();

            // Act
            var actualOrder = await sveaClient.Checkout.CreateOrder(request);

            // Assert
            Assert.True(DataComparison.DataAreEqual(expectedOrder, actualOrder));
        }


    }
}
