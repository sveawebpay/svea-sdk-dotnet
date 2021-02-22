namespace Svea.WebPay.SDK.Tests.Json
{
    using Svea.WebPay.SDK.CheckoutApi;
    using Svea.WebPay.SDK.Json;

    using System.Text.Json;

    using Xunit;

    using JsonSerializer = System.Text.Json.JsonSerializer;

    public class CustomCurrencyCodeConverterTests
    {
        private readonly string currencyCode = "SEK";

        [Fact]
        public void CanDeSerialize_CurrencyCode()
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{this.currencyCode}\" }}";
           

            //ACT
            var result = JsonSerializer.Deserialize<CurrencyCode>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.currencyCode, result.ToString());
        }


        [Fact]
        public void CanSerialize_CurrencyCode()
        {
            //ARRANGE
            var currency = new { CurrencyCode = new CurrencyCode(this.currencyCode) };


            //ACT
            var result = JsonSerializer.Serialize(currency, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);
            obj.RootElement.TryGetProperty("currencyCode", out var code);

            //ASSERT
            Assert.Equal(this.currencyCode, code.GetString());
        }
    }
}
