using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.Tests.Helpers.DataSamples
{
    public class CheckoutDataSamples
    {
        public static string CheckoutCreateRecurringOrderResponse = @"{
            ""MerchantSettings"":
            {
                ""CheckoutValidationCallBackUri"":null,
                ""PushUri"":""https://svea.com/push.aspx?sid=123&svea_order=123"",
                ""TermsUri"":""http://localhost:51898/terms"",
                ""CheckoutUri"":""http://localhost:8080/php-checkout/examples/create-order.php"",
                ""ConfirmationUri"":""http://localhost/php-checkout/examples/get-order.php"",
                ""ActivePartPaymentCampaigns"":[],
                ""PromotedPartPaymentCampaign"":0
            },
            ""Cart"":
            {
                ""Items"":
                [{
                    ""ArticleNumber"":""ABC80"",
                    ""Name"":""Computer"",
                    ""Quantity"":1000,
                    ""UnitPrice"":500000,
                    ""DiscountAmount"":1000,
                    ""VatPercent"":2500,
                    ""Unit"":""SEK"",
                    ""TemporaryReference"":null,
                    ""RowNumber"":1,
                    ""MerchantData"":null
                },
		         {
    ""ArticleNumber"":""7c7068f1-5223-4043-9be2-7adfe4fe9856"",
			        ""Name"":""Order Discount"",
			        ""Quantity"":100,
			        ""UnitPrice"":-107060,
			        ""DiscountPercent"":0,
			        ""DiscountAmount"":0,
			        ""VatPercent"":0,
			        ""Unit"":"""",

                    ""TemporaryReference"":null,
			        ""RowNumber"":3,
			        ""MerchantData"":null

                }]
            },
            ""Customer"":null,
            ""ShippingAddress"":null,
            ""BillingAddress"":null,
            ""Gui"":
            {
    ""Layout"":""desktop"",
             ""Snippet"":""\r\n<div id=\""svea-checkout-container\"" data-sco-sveacheckout=\""\"" data-sco-sveacheckout-locale=\""sv-SE\"" style=\""overflow-x: hidden; overflow-y: hidden;\"">\r\n    <noscript> Please <a href=\""http://enable-javascript.com\"">enable JavaScript</a>. </noscript>\r\n    <iframe id=\""svea-checkout-iframe\"" name=\""svea-checkout-iframe\"" data-sco-sveacheckout-iframeSrc=\""https://checkoutapistage.svea.com/b/index.html?orderId=2461925&authToken=SveaCheckout+aLs4Zd%2bOBynWBr%2bNGpSVsNE7Sts%3d&token=9D5B49C2B8553149868937DB288B3C7C&locale=sv-SE\"" scrolling=\""no\"" frameborder=\""0\"" style=\""display: none; width: 1px; min-width: 100%; max-width: 100%;\""></iframe>\r\n</div>\r\n\r\n<script type=\""text/javascript\"" src=\""https://checkoutapistage.svea.com/merchantscript/build/index.js?v=ce8e0b83c1ae01bae5769711c9a3f857\""></script>\r\n<script type=\""text/javascript\"">!function(e){var t=e.document,n=t.querySelectorAll(\""[data-sco-sveacheckout]\"");if(!n.length)throw new Error(\""No Svea checkout container exists on page\"");function c(){return!!e.scoInitializeInjectedInstances&&(e.scoInitializeInjectedInstances(),!0)}if(!c()){var i=0,o=function(){i+=1,c()||(i<150?e.setTimeout(o,20):[].slice.call(n).forEach(function(e){var n=t.createElement(\""div\"");n.innerHTML=\""Something went wrong, please refresh the page\"",e.appendChild(n)}))};e.setTimeout(o,20)}}(window);</script>\r\n\r\n""
},
            ""Locale"":""sv-SE"",
            ""Currency"":""SEK"",
            ""CountryCode"":""SE"",
            ""ClientOrderNumber"":""637285981136373230"",
            ""OrderId"":2461925,
            ""EmailAddress"":null,
            ""PhoneNumber"":null,
            ""PaymentType"":null,
            ""Payment"":null,
            ""Status"":""Created"",
            ""CustomerReference"":null,
            ""SveaWillBuyOrder"":null,
            ""IdentityFlags"":null,
            ""MerchantData"":null,
            ""PeppolId"":null,
            ""Recurring"":true,
            ""RecurringToken"": null
        }";
    }
}
