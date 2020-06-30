using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Svea.WebPay.SDK.Json;
using Xunit;
using OrderRow = Svea.WebPay.SDK.CheckoutApi.OrderRow;

namespace Svea.WebPay.SDK.Tests.Json
{
    public class CustomMinorUnitConverterTests
    {
        private readonly long expectedAmount = 100;


        [Fact]
        public void CanDeserialize_Amount()
        {
            //ARRANGE
            var jsonObject = new JObject { { "x", this.expectedAmount } };

            //ACT
            var result = JsonConvert.DeserializeObject<MinorUnit>(jsonObject.ToString(), JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.expectedAmount, result.Value);
        }


        [Fact]
        public void CanSerialize_Amount()
        {
            //ARRANGE
            var orderRow = new OrderRow("1", "Test", MinorUnit.FromDecimal(1), MinorUnit.FromDecimal(1), MinorUnit.FromDecimal(0), MinorUnit.FromDecimal(25),
                "#", "", 1);

            //ACT
            var result = JsonConvert.SerializeObject(orderRow, JsonSerialization.Settings);
            var obj = JObject.Parse(result);
            obj.TryGetValue("UnitPrice", StringComparison.InvariantCultureIgnoreCase, out var amount);

            //ASSERT
            Assert.Equal(this.expectedAmount, amount);
        }
    }
}
