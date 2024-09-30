namespace Sample.Testshop.Server.Modules.Checkout.Models
{
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
}
