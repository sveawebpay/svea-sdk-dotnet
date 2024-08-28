using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.CheckoutApi.Recurring;
using Svea.WebPay.SDK.Tests.Helpers.DataSamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Checkout.Recurring
{
    public class RecurringOrderTests : TestBase
    {

        [Fact]
        public async Task CreateRecurringOrderAsync_ShouldReturnOrderData()
        {
            // Arrange
            var mockResponse = "{\"orderId\":123456, \"status\":\"Final\"}";
            var mockHandler = CreateHandlerMock(mockResponse);
            var recurringToken = "some-recurring-token";
            var createRecurringOrderModel = new CreateRecurringOrderModel();
            var sveaClient = SveaClient(mockHandler);

            // Act
            var result = await sveaClient.Checkout.Recurring.CreateRecurringOrderAsync(createRecurringOrderModel, recurringToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123456, result.OrderId);
            Assert.Equal(CheckoutOrderStatus.Final, result.Status);
        }

        [Fact]
        public async Task GetRecurringOrder_WithToken_ShouldReturnRecurringToken()
        {
            // Arrange
            var mockResponse = "{\"token\":\"some-recurring-token\", \"status\":\"Active\"}";
            var mockHandler = CreateHandlerMock(mockResponse);

            var recurringToken = "some-recurring-token";
            var sveaClient = SveaClient(mockHandler);

            // Act
            var result = await sveaClient.Checkout.Recurring.GetRecurringTokenAsync(recurringToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("some-recurring-token", result.Token);
            Assert.Equal(TokenStatus.Active, result.Status);
        }

        [Fact]
        public async Task GetRecurringOrder_WithTokenAndOrderId_ShouldReturnOrderData()
        {
            // Arrange
            var mockResponse = "{\"orderId\":123456, \"status\":\"Final\"}";
            var mockHandler = CreateHandlerMock(mockResponse);

            var recurringToken = "some-recurring-token";
            long orderId = 123456;
            var sveaClient = SveaClient(mockHandler);

            // Act
            var result = await sveaClient.Checkout.Recurring.GetRecurringOrderAsync(recurringToken, orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123456, result.OrderId);
            Assert.Equal(CheckoutOrderStatus.Final, result.Status);
        }

        [Fact]
        public async Task ChangePaymentMethod_ShouldReturnChangePaymentMethodResponse()
        {
            // Arrange
            var mockResponse = "{\"snippet\":\"<iframe src=\\\"\\\"></iframe>\", \"expiration\":\"2023-12-01T19:01:55.714942\"}";
            var mockHandler = CreateHandlerMock(mockResponse);

            var recurringToken = "some-recurring-token";
            var changePaymentMethodModel = new ChangepaymentMethodModel(); // Assuming a valid model instance
            var sveaClient = SveaClient(mockHandler);

            // Act
            var result = await sveaClient.Checkout.Recurring.ChangePaymentMethodAsync(changePaymentMethodModel, recurringToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("<iframe src=\"\"></iframe>", result.Snippet);
            Assert.Equal(DateTime.Parse("2023-12-01T19:01:55.714942"), result.Expiration);
        }
    }
}
