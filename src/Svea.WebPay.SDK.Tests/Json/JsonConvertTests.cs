using System.Collections.Generic;
using Newtonsoft.Json;
using Svea.WebPay.SDK.PaymentAdminApi;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Json
{
    using Svea.WebPay.SDK.Json;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public class JsonConvertTests
    {

        [Fact]
        public void CanDeserialize_OrderResponseObject()
        {
            string result = "{\"Id\":1665688,\"Currency\":\"SEK\",\"MerchantOrderId\":\"637157555827738278\"," +
                            "\"OrderStatus\":\"Open\",\"" +
                            "SystemStatus\":\"AUTHORIZED\"," +
                            "\"SystemStatusMessage\":\"SUCCESS\"," +
                            "\"PaymentCreditStatus\":null," +
                            "\"EmailAddress\":\"daniel.sundqvist@authority.se\"," +
                            "\"PhoneNumber\":\"0811111111\"," +
                            "\"CustomerReference\":null," +
                            "\"PaymentType\":\"Card\"," +
                            "\"CreationDate\":\"2020-01-27T20:03:00\"," +
                            "\"NationalId\":\"194605092222\"," +
                            "\"IsCompany\":false," +
                            "\"CancelledAmount\":0," +
                            "\"OrderAmount\":75000," +
                            "\"BillingAddress\":{\"FullName\":null,\"StreetAddress\":null,\"CoAddress\":null,\"PostalCode\":null,\"City\":null,\"CountryCode\":\"SE\"}," +
                            "\"ShippingAddress\":{\"FullName\":\"Persson Tess T\",\"StreetAddress\":\"Testgatan 1\",\"CoAddress\":\"c / o Eriksson, Erik\",\"PostalCode\":\"99999\",\"City\":\"Stan\",\"CountryCode\":\"SE\"}," +
                            "\"Deliveries\":null," +
                            "\"OrderRows\":[{\"OrderRowId\":1,\"ArticleNumber\":\"P2\",\"Name\":\"Nike Metcon 5\",\"Quantity\":100,\"UnitPrice\":75000,\"DiscountPercent\":0,\"VatPercent\":0,\"Unit\":null,\"IsCancelled\":false,\"Actions\":[]}]," +
                            "\"Deliveries\":[{\"Id\":5077532,\"CreationDate\":\"2020-04-30T14:22:40\",\"InvoiceId\":null,\"DeliveryAmount\":23000,\"CreditedAmount\":0,\"Credits\":null,\"OrderRows\":[{\"OrderRowId\":1,\"ArticleNumber\":\"P1\",\"Name\":\"Puma Black Sneakers Shoes\",\"Quantity\":100,\"UnitPrice\":23000,\"DiscountPercent\":0,\"VatPercent\":0,\"Unit\":null,\"IsCancelled\":false,\"Actions\":[]}],\"Actions\":[\"CanCreditAmount\"],\"Status\":null,\"DueDate\":null}]," +
                            "\"Actions\":[\"CanDeliverOrder\",\"CanCancelOrder\",\"CanCancelAmount\"]," +
                            "\"SveaWillBuy\":null}";

            var order = JsonConvert.DeserializeObject<OrderResponseObject>(result, JsonSerialization.Settings);
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
