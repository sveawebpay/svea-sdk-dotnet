using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class CreateOrderHandler
    {

        public static async Task<Data> Handle(CreateOrderRequestModel createOrderModel, SveaWebPayClient sveaClient)
        {
            if (string.IsNullOrWhiteSpace(createOrderModel?.ClientOrderNumber))
            {
                createOrderModel.ClientOrderNumber = Guid.NewGuid().ToString().Replace("-", "");
                createOrderModel.Currency = "SEK";
            }


            var response = await sveaClient.Checkout.CreateOrder(new CreateOrderModel(new RegionInfo(createOrderModel.CountryCode), new CurrencyCode(createOrderModel.Currency), new Language(createOrderModel.Locale),
                createOrderModel.ClientOrderNumber, createOrderModel.MerchantSettings?.ToSDKModel(), createOrderModel.Cart.ToSDKModel(),
                createOrderModel.RequireElectronicIdAuthentication,
                createOrderModel.PresetValues?.Select(x => new Presetvalue(x.TypeName, x.Value, x.IsReadonly)).ToList(), null, null, createOrderModel.MerchantData, null));
 
            return response;

        }
        public class CreateOrderRequestModel
        {
            public string CountryCode { get; set; } = string.Empty;
            public string Currency { get; set; } = string.Empty;
            public string Locale { get; set; } = string.Empty;
            public string ClientOrderNumber { get; set; } = string.Empty;
            public MerchantSettings MerchantSettings { get; set; } = new MerchantSettings()
            {

                CheckoutUri = "https://loalhost:3000/checkoutPage",
                ConfirmationUri = "https://localhost:3000/confirmationPage/clientOrderNumber",
                TermsUri = "https://localhost:3000/legalterms",
                PushUri = "https://localhost:3000/pushUri"

            };
            public Cart Cart { get; set; } = new Cart();
            public ShippingInformation? ShippingInformation { get; set; }
            public List<PresetValue>? PresetValues { get; set; }
            public Validation Validation { get; set; } = new Validation();
            public IdentityFlags? IdentityFlags { get; set; }
            public bool RequireElectronicIdAuthentication { get; set; }
            public string? PartnerKey { get; set; }
            public string? MerchantData { get; set; }
            public bool Recurring { get; set; }
        }

        public class MerchantSettings
        {
            public string? CheckoutValidationCallBackUri { get; set; } = string.Empty;
            public string PushUri { get; set; } = string.Empty;
            public string TermsUri { get; set; } = string.Empty;
            public string CheckoutUri { get; set; } = string.Empty;
            public string ConfirmationUri { get; set; } = string.Empty;
            public List<long>? ActivePartPaymentCampaigns { get; set; }
            public long? PromotedPartPaymentCampaign { get; set; }

            public Svea.WebPay.SDK.CheckoutApi.MerchantSettings ToSDKModel()
            {
                return new Svea.WebPay.SDK.CheckoutApi.MerchantSettings(new Uri(PushUri),
                    new Uri(TermsUri),
                    new Uri(CheckoutUri),
                    new Uri(ConfirmationUri),
                    !string.IsNullOrEmpty(CheckoutValidationCallBackUri) ? new Uri(CheckoutValidationCallBackUri) : null,
                    null,
                    ActivePartPaymentCampaigns ?? new List<long>(),
                    PromotedPartPaymentCampaign);
            }
        }

        public class Cart
        {
            public List<OrderRow> Items { get; set; } = new List<OrderRow>();
            public Svea.WebPay.SDK.CheckoutApi.Cart ToSDKModel()
            {
                return new Svea.WebPay.SDK.CheckoutApi.Cart(
                    Items.Select(x => new Svea.WebPay.SDK.CheckoutApi.OrderRow(
                        x.ArticleNumber,
                        x.Name,
                        x.Quantity,
                        x.UnitPrice,
                        x.DiscountPercent,
                        x.DiscountAmount,
                        x.VatPercent,
                        x.Unit,
                        x.TemporaryReference,
                        x.RowNumber,
                        x.MerchantData,
                        x.RowType)).ToList());
            }
        }

        public class OrderRow
        {
            public string? ArticleNumber { get; set; }
            public string Name { get; set; } = string.Empty;
            public long Quantity { get; set; }
            public long UnitPrice { get; set; }
            public long DiscountPercent { get; set; }
            public long DiscountAmount { get; set; }
            public long VatPercent { get; set; }
            public string Unit { get; set; } = string.Empty;
            public string? TemporaryReference { get; set; }
            public int RowNumber { get; set; }
            public string? MerchantData { get; set; }
            public string? RowType { get; set; }
        }

        public class ShippingInformation
        {
            public bool EnableShipping { get; set; }
            public bool EnforceFallback { get; set; }
            public double Weight { get; set; }
            public Dictionary<string, string>? Tags { get; set; }
            public List<FallbackOption>? FallbackOptions { get; set; }
            public bool ShouldRejectShippingSession { get; set; }
        }

        public class FallbackOption
        {
            public string Id { get; set; } = string.Empty;
            public string Carrier { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public long ShippingFee { get; set; }
            public List<dynamic>? Addons { get; set; }
            public List<dynamic>? Fields { get; set; }
        }

        public class PresetValue
        {
            public string TypeName { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
            public bool IsReadonly { get; set; }
        }

        public class Validation
        {
            int? MinAge { get; set; }
        }

        public class IdentityFlags
        {
            public bool HideNotYou { get; set; }
            public bool HideChangeAddress { get; set; }
            public bool HideAnonymous { get; set; }
        }

    }
}
