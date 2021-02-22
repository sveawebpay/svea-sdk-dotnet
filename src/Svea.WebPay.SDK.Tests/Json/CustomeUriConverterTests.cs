
using Svea.WebPay.SDK.Json;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Json
{
    using System;
    using System.Text.Json;

    
    public class CustomeUriConverterTests
    {
        private readonly string uri = "https://www.test.com/";


        [Fact]
        public void CanDeSerialize_Uri()
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{this.uri}\" }}";

            //ACT
            var result = JsonSerializer.Deserialize<Uri>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.uri, result.ToString());
        }


        [Fact]
        public void CanSerialize_Uri()
        {
            //ARRANGE
            var uriObj = new { Uri = new Uri(this.uri) };

            //ACT
            var result = JsonSerializer.Serialize(uriObj, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);
            obj.RootElement.TryGetProperty("uri", out var address);

            //ASSERT
            Assert.Equal(this.uri, address.ToString());
        }
    }
}
