using System;

namespace Svea.WebPay.SDK.Tests.Json
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Svea.WebPay.SDK.CheckoutApi;
    using Svea.WebPay.SDK.Json;

    using Xunit;

    public class CustomCurrencyCodeConverterTests
    {
        private readonly string currencyCode = "SEK";

        [Fact]
        public void CanDeSerialize_CurrencyCode()
        {
            //ARRANGE

            var jsonObject = new JObject { { "currency", this.currencyCode } };

            //ACT
            var result = JsonConvert.DeserializeObject<CurrencyCode>(jsonObject.ToString(), JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.currencyCode, result.ToString());
        }


        [Fact]
        public void CanSerialize_CurrencyCode()
        {
            //ARRANGE
            var currency = new { CurrencyCode = new CurrencyCode(this.currencyCode) };


            //ACT
            var result = JsonConvert.SerializeObject(currency, JsonSerialization.Settings);
            var obj = JObject.Parse(result);
            obj.TryGetValue("CurrencyCode", StringComparison.InvariantCultureIgnoreCase, out var code);

            //ASSERT
            Assert.Equal(this.currencyCode, code);
        }
    }
}
