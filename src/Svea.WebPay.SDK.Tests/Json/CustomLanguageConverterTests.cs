namespace Svea.WebPay.SDK.Tests.Json
{
    using Svea.WebPay.SDK.CheckoutApi;
    using Svea.WebPay.SDK.Json;

    using System.Collections.Generic;
    using System.Text.Json;

    using Xunit;

    public class CustomLanguageConverterTests
    {
        private readonly string languageString = "sv-SE";


        [Fact]
        public void CanDeSerialize_Language()
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{this.languageString}\" }}";

            //ACT
            var result = JsonSerializer.Deserialize<Language>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.languageString, result.ToString());
        }


        [Fact]
        public void CanSerialize_Language()
        {
            //ARRANGE
            var dummy = new
            {
                Language = new Language(this.languageString)
            };

            //ACT
            var result = JsonSerializer.Serialize(dummy, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);

            obj.RootElement.TryGetProperty("language",  out var language);
            
            //ASSERT
            Assert.Equal(this.languageString, language.GetString());
        }
    }
}
