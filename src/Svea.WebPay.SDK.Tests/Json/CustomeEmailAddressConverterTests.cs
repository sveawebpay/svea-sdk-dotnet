using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Svea.WebPay.SDK.Json;
using Xunit;

namespace Svea.WebPay.SDK.Tests.Json
{
    public class CustomeEmailAddressConverterTests
    {
        private readonly string address = "email@example.com";


        [Fact]
        public void CanDeSerialize_EmailAddress()
        {
            //ARRANGE

            var jsonObject = new JObject
            {
                { "x", this.address }
            };

            //ACT
            var result = JsonConvert.DeserializeObject<EmailAddress>(jsonObject.ToString(), JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.address, result.ToString());
        }


        [Fact]
        public void CanSerialize_EmailAddress()
        {
            //ARRANGE
            var emailAddress = new  { EmailAddress = new EmailAddress(this.address) };

            //ACT
            var result = JsonConvert.SerializeObject(emailAddress, JsonSerialization.Settings);
            var obj = JObject.Parse(result);
            obj.TryGetValue("EmailAddress", StringComparison.InvariantCultureIgnoreCase, out var address);

            //ASSERT
            Assert.Equal(this.address, address.ToString());
        }
    }
}
