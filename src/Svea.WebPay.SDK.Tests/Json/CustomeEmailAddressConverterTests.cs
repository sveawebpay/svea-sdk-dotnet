
using Svea.WebPay.SDK.Json;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Json
{
    using System.Text.Json;

    
    public class CustomeEmailAddressConverterTests
    {
        private readonly string address = "email@example.com";


        [Fact]
        public void CanDeSerialize_EmailAddress()
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{this.address}\" }}";

            //ACT
            var result = JsonSerializer.Deserialize<EmailAddress>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.address, result.ToString());
        }


        [Fact]
        public void CanSerialize_EmailAddress()
        {
            //ARRANGE
            var emailAddress = new { EmailAddress = new EmailAddress(this.address) };

            //ACT
            var result = JsonSerializer.Serialize(emailAddress, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);
            obj.RootElement.TryGetProperty("emailAddress", out var address);

            //ASSERT
            Assert.Equal(this.address, address.ToString());
        }
    }
}
