using System.Collections.Generic;
using Svea.WebPay.SDK.PaymentAdminApi;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Json
{
    using Svea.WebPay.SDK.Json;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System.Text.Json;

    public class JsonConvertTests
    {

        [Fact]
        public void CanDeserialize_OrderResponseObject()
        {
            string result = "{\"actions\":[\"CanDeliverOrder\",\"CanCancelOrder\",\"CanCancelAmount\"]," +
            "\"billingAddress\":{\"fullName\":null,\"streetAddress\":null,\"coAddress\":null,\"postalCode\":null,\"city\":null,\"countryCode\":\"SE\"}," +
            "\"cancelledAmount\":0," +
            "\"creationDate\":\"2020-01-27T20:03:00\"," +
            "\"currency\":\"SEK\"," +
            "\"customerReference\":null," +
            "\"deliveries\":[{\"id\":5077532,\"creationDate\":\"2020-04-30T14:22:40\",\"invoiceId\":null,\"deliveryAmount\":23000,\"creditedAmount\":0,\"credits\":null,\"orderRows\":[{\"orderRowId\":1,\"articleNumber\":\"P1\",\"name\":\"Puma Black Sneakers Shoes\",\"quantity\":100,\"unitPrice\":23000,\"discountPercent\":0,\"vatPercent\":0,\"unit\":null,\"isCancelled\":false,\"actions\":[]}],\"actions\":[\"CanCreditAmount\"],\"status\":null,\"dueDate\":null}]," +
            "\"emailAddress\":\"daniel.sundqvist@authority.se\"," +
            "\"id\":1665688," +
            "\"isCompany\":false," +
            "\"merchantOrderId\":\"637157555827738278\"," +
            "\"nationalId\":\"194605092222\"," +
            "\"orderAmount\":75000," +
            "\"orderRows\":[{\"orderRowId\":1,\"articleNumber\":\"P2\",\"name\":\"Nike Metcon 5\",\"quantity\":100,\"unitPrice\":75000,\"discountPercent\":0,\"vatPercent\":0,\"unit\":null,\"isCancelled\":false,\"actions\":[]}]," +
            "\"orderStatus\":\"Open\"," +
            "\"paymentType\":\"Card\"," +
            "\"phoneNumber\":\"0811111111\"," +
            "\"shippingAddress\":{\"fullName\":\"Persson Tess T\",\"streetAddress\":\"Testgatan 1\",\"coAddress\":\"c / o Eriksson, Erik\",\"postalCode\":\"99999\",\"city\":\"Stan\",\"countryCode\":\"SE\"}," +
            "\"sveaWillBuy\":null}";



            var order = JsonSerializer.Deserialize<OrderResponseObject>(result, JsonSerialization.Settings);
            Assert.NotNull(order);
            Assert.Equal(1665688, order.Id);
            //    Assert.Equal("AUTHORIZED", order.sy);
            Assert.Equal("daniel.sundqvist@authority.se", order.EmailAddress.ToString());
            Assert.Equal("0811111111", order.PhoneNumber);
            Assert.Equal(PaymentType.Card, order.PaymentType);
            Assert.Equal(27, order.CreationDate.Day);
            Assert.Equal("194605092222", order.NationalId);
            Assert.Equal(new List<string> { "CanDeliverOrder", "CanCancelOrder", "CanCancelAmount" }, order.Actions);
        }

    }
}
