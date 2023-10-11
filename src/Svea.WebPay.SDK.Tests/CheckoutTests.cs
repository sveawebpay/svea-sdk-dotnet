using System.Collections.Generic;

using Svea.WebPay.SDK.CheckoutApi;

using Xunit;

using Svea.WebPay.SDK.Tests.Builders;
using Svea.WebPay.SDK.Tests.Helpers;
using Svea.WebPay.SDK.Json;

using System.Linq;

using OrderRow = Svea.WebPay.SDK.CheckoutApi.OrderRow;

namespace Svea.WebPay.SDK.Tests;

using System.Text.Json;

public class CheckoutTests : TestBase
{
    private readonly CheckoutOrderBuilder checkoutOrderBuilder = new CheckoutOrderBuilder();

    [Fact]
    public async System.Threading.Tasks.Task CreateOrder_Should_Serialize_AsExpected()
    {
        // Arrange
        var expectedOrder = JsonSerializer.Deserialize<Data>(DataSample.CheckoutCreateOrderResponse, JsonSerialization.Settings);
        var sveaClient = SveaClient(CreateHandlerMock(DataSample.CheckoutCreateOrderResponse));
        var request = checkoutOrderBuilder.UseTestValues().Build();

        // Act
        var actualOrder = await sveaClient.Checkout.CreateOrder(request);

        // Assert
        Assert.True(DataComparison.DataAreEqual(expectedOrder, actualOrder));
    }
        
    [Fact]
    public async System.Threading.Tasks.Task GetOrder_Should_Serialize_AsExpected()
    {
        // Arrange
        var createdOrder = JsonSerializer.Deserialize<Data>(DataSample.CheckoutCreateOrderResponse, JsonSerialization.Settings);
        var expectedOrder = JsonSerializer.Deserialize<Data>(DataSample.CheckoutGetOrderResponse, JsonSerialization.Settings);
        var sveaClient = SveaClient(CreateHandlerMock(DataSample.CheckoutGetOrderResponse));

        // Act
        var actualOrder = await sveaClient.Checkout.GetOrder(createdOrder.OrderId);

        // Assert
        Assert.True(DataComparison.DataAreEqual(expectedOrder, actualOrder));
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateOrder_Should_Serialize_AsExpected()
    {
        // Arrange
        var createdOrder = JsonSerializer.Deserialize<Data>(DataSample.CheckoutUpdateOrderResponse, JsonSerialization.Settings);
        var expectedOrder = JsonSerializer.Deserialize<Data>(DataSample.CheckoutUpdateOrderResponse, JsonSerialization.Settings);
        var sveaClient = SveaClient(CreateHandlerMock(DataSample.CheckoutUpdateOrderResponse));

        // Act
        var update = CreateUpdateOrderRequest("");
        var actualOrder = await sveaClient.Checkout.UpdateOrder(createdOrder.OrderId, update);

        // Assert
        Assert.True(DataComparison.DataAreEqual(expectedOrder, actualOrder));

        Assert.Equal(4, actualOrder.Cart.Items[0].Quantity);
        Assert.Equal(2000, actualOrder.Cart.Items[0].UnitPrice);
        Assert.Equal(0, actualOrder.Cart.Items[0].DiscountAmount);
        Assert.Equal(6, actualOrder.Cart.Items[0].VatPercent);
    }


    [Fact]
    public void Order_Should_Serialize_AsExpected()
    {
        // Act
        var order = JsonSerializer.Deserialize<Data>(DataSample.CheckoutGetOrderResponse, JsonSerialization.Settings);

        // Assert
        Assert.Null(order.MerchantSettings.CheckoutValidationCallBackUri);
        Assert.Equal("https://svea.com/push.aspx?sid=123&svea_order=123", order.MerchantSettings.PushUri.OriginalString);
        Assert.Equal("http://localhost:51898/terms", order.MerchantSettings.TermsUri.OriginalString);
        Assert.Equal("http://localhost:8080/php-checkout/examples/create-order.php", order.MerchantSettings.CheckoutUri.OriginalString);
        Assert.Equal("http://localhost/php-checkout/examples/get-order.php", order.MerchantSettings.ConfirmationUri.OriginalString);
        Assert.Empty(order.MerchantSettings.ActivePartPaymentCampaigns);
        Assert.Equal(0, order.MerchantSettings.PromotedPartPaymentCampaign);

        var item = order.Cart.Items.First();
        Assert.Single(order.Cart.Items);
        Assert.Equal("ABC80", item.ArticleNumber);
        Assert.Equal("Computer", item.Name);
        Assert.Equal(10, item.Quantity);
        Assert.Equal(5000, item.UnitPrice);
        Assert.Equal(10, item.DiscountAmount);
        Assert.Equal(25, item.VatPercent);
        Assert.Equal("SEK", item.Unit);
        Assert.Null(item.TemporaryReference);
        Assert.Equal(1, item.RowNumber);
        Assert.Null(item.MerchantData);

        Assert.Null(order.Customer);
        Assert.Null(order.ShippingAddress);
        Assert.Null(order.BillingAddress);

        Assert.Equal("desktop", order.Gui.Layout);
        Assert.Equal("\r\n<div id=\"svea-checkout-container\" data-sco-sveacheckout=\"\" data-sco-sveacheckout-locale=\"sv-SE\" style=\"overflow-x: hidden; overflow-y: hidden;\">\r\n    <noscript> Please <a href=\"http://enable-javascript.com\">enable JavaScript</a>. </noscript>\r\n    <iframe id=\"svea-checkout-iframe\" name=\"svea-checkout-iframe\" data-sco-sveacheckout-iframeSrc=\"https://checkoutapistage.svea.com/b/index.html?orderId=2461925&authToken=SveaCheckout+aLs4Zd%2bOBynWBr%2bNGpSVsNE7Sts%3d&token=9D5B49C2B8553149868937DB288B3C7C&locale=sv-SE\" scrolling=\"no\" frameborder=\"0\" style=\"display: none; width: 1px; min-width: 100%; max-width: 100%;\"></iframe>\r\n</div>\r\n\r\n<script type=\"text/javascript\" src=\"https://checkoutapistage.svea.com/merchantscript/build/index.js?v=ce8e0b83c1ae01bae5769711c9a3f857\"></script>\r\n<script type=\"text/javascript\">!function(e){var t=e.document,n=t.querySelectorAll(\"[data-sco-sveacheckout]\");if(!n.length)throw new Error(\"No Svea checkout container exists on page\");function c(){return!!e.scoInitializeInjectedInstances&&(e.scoInitializeInjectedInstances(),!0)}if(!c()){var i=0,o=function(){i+=1,c()||(i<150?e.setTimeout(o,20):[].slice.call(n).forEach(function(e){var n=t.createElement(\"div\");n.innerHTML=\"Something went wrong, please refresh the page\",e.appendChild(n)}))};e.setTimeout(o,20)}}(window);</script>\r\n\r\n", order.Gui.Snippet);

        Assert.Equal("sv-SE", order.Locale);
        Assert.Equal("SEK", order.Currency);
        Assert.Equal("SE", order.CountryCode);
        Assert.Equal("637285981136373230", order.ClientOrderNumber);
        Assert.Equal(2461925, order.OrderId);
        Assert.Null(order.EmailAddress);
        Assert.Null(order.PhoneNumber);
        Assert.Equal(PaymentType.Unknown, order.PaymentType);
        Assert.Null(order.Payment);
        Assert.Equal(CheckoutOrderStatus.Created, order.Status);
        Assert.Null(order.CustomerReference);
        Assert.Null(order.SveaWillBuyOrder);
        Assert.Null(order.IdentityFlags);
        Assert.Null(order.MerchantData);
        Assert.Null(order.PeppolId);
    }
        
    [Fact]
    public void Completed_ZeroSum_Order_Should_Serialize_AsExpected()
    {
        // Act
        var order = JsonSerializer.Deserialize<Data>(DataSample.CheckoutGetCompletedZeroSumOrderResponse, JsonSerialization.Settings);

        // Assert
        Assert.Equal("https://svea2-sample.eu.ngrok.io/api/svea/validation/{checkout.order.uri}/?marketId=SE", order.MerchantSettings.CheckoutValidationCallBackUri.OriginalString);
        Assert.Equal("https://svea2-sample.eu.ngrok.io/api/svea/push/{checkout.order.uri}/?marketId=SE", order.MerchantSettings.PushUri.OriginalString);
        Assert.Equal("https://svea2-sample.eu.ngrok.io/terms", order.MerchantSettings.TermsUri.OriginalString);
        Assert.Equal("https://localhost:44345/CheckOut/LoadPaymentMenu", order.MerchantSettings.CheckoutUri.OriginalString);
        Assert.Equal("https://localhost:44345/checkout/thankyou", order.MerchantSettings.ConfirmationUri.OriginalString);
        Assert.Empty(order.MerchantSettings.ActivePartPaymentCampaigns);
        Assert.Equal(0, order.MerchantSettings.PromotedPartPaymentCampaign);

        var item1 = order.Cart.Items.First();
        Assert.Equal(2, order.Cart.Items.Count);
        Assert.Equal("Ref2", item1.ArticleNumber);
        Assert.Equal("Levis 501 Jeans", item1.Name);
        Assert.Equal(1, item1.Quantity);
        Assert.Equal(1190, item1.UnitPrice);
        Assert.Equal(0, item1.DiscountAmount);
        Assert.Equal(0, item1.VatPercent);
        Assert.Null(item1.TemporaryReference);
        Assert.Equal(1, item1.RowNumber);
        Assert.Null(item1.MerchantData);

        var item2 = order.Cart.Items[1];
        Assert.Equal(2, order.Cart.Items.Count);
        Assert.Equal("Ref5", item2.ArticleNumber);
        Assert.Equal("Rabattkod 1190", item2.Name);
        Assert.Equal(1, item2.Quantity);
        Assert.Equal(-1190, item2.UnitPrice);
        Assert.Equal(0, item2.DiscountAmount);
        Assert.Equal(0, item2.VatPercent);
        Assert.Null(item2.TemporaryReference);
        Assert.Equal(2, item2.RowNumber);
        Assert.Null(item2.MerchantData);

        Assert.Equal(626, order.Customer.Id);
        Assert.Equal("194605092222", order.Customer.NationalId);
        Assert.Equal("SE", order.Customer.CountryCode);
            
        Assert.Equal("Persson, Tess T", order.ShippingAddress.FullName);
        Assert.Equal("Tess", order.ShippingAddress.FirstName);
        Assert.Equal("c/o Eriksson, Erik", order.ShippingAddress.CoAddress);
            
        Assert.Equal("Persson, Tess T", order.BillingAddress.FullName);
        Assert.Equal("Tess", order.BillingAddress.FirstName);
        Assert.Equal("c/o Eriksson, Erik", order.BillingAddress.CoAddress);

        Assert.Equal("desktop", order.Gui.Layout);
        Assert.Equal("sv-SE", order.Locale);
        Assert.Equal("SEK", order.Currency);
        Assert.Equal("SE", order.CountryCode);
        Assert.Equal("638297691863676770", order.ClientOrderNumber);
        Assert.Equal(8932787, order.OrderId);
        Assert.Equal("test@test.com",order.EmailAddress);
        Assert.Equal("34345435435", order.PhoneNumber);
        Assert.Equal(PaymentType.ZEROSUM, order.PaymentType);
        Assert.Equal(PaymentMethodType.ZeroSum, order.Payment.PaymentMethodType);
        Assert.Equal(CheckoutOrderStatus.Final, order.Status);
        Assert.Null(order.CustomerReference);
        Assert.Null(order.SveaWillBuyOrder);
        Assert.Null(order.IdentityFlags);
        Assert.Null(order.MerchantData);
        Assert.Null(order.PeppolId);
    }

        
    private static UpdateOrderModel CreateUpdateOrderRequest(string merchantData)
    {
        var orderRows = new List<OrderRow>
        {
            new OrderRow(
                "ABC80",
                "Computer",
                new MinorUnit(4),
                new MinorUnit(2000),
                new MinorUnit(0),
                new MinorUnit(6),
                null,
                null,
                2)
        };

        var cart = new Cart(orderRows);
        var updateModel = new UpdateOrderModel(cart, merchantData);

        return updateModel;
    }
}