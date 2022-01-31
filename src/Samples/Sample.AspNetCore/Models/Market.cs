namespace Sample.AspNetCore.Models
{
    using Svea.WebPay.SDK.CheckoutApi;

    public class Market
    {
        private string _marketId;

        public string MarketId
        {
            get => !string.IsNullOrWhiteSpace(_marketId) ? _marketId : "SE";
            set => _marketId = value;
        }

        private string _languageId;

        public string LanguageId
        {
            get => !string.IsNullOrWhiteSpace(_languageId) ? _languageId : "sv-SE";
            set => _languageId = value;
        }

        private string _currencyCode;

        public string CurrencyCode
        {
            get => !string.IsNullOrWhiteSpace(_currencyCode) ? _currencyCode : "SEK";
            set => _currencyCode = value;
        }

        public virtual void Update()
        {
        }

        public virtual void SetMarket(string marketId)
        {
            MarketId = marketId;
        }

        public virtual void SetLanguage(string languageId)
        {
            var language = new Language(languageId);
            LanguageId = languageId;
        }

        public virtual void SetCurrency(string currencyCode)
        {
            var currency = new CurrencyCode(currencyCode);
            CurrencyCode = currency.ToString();
        }
    }
}
