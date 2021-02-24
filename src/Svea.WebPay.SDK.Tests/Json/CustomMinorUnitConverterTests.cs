using System;
using Svea.WebPay.SDK.Json;
using Xunit;
using OrderRow = Svea.WebPay.SDK.CheckoutApi.OrderRow;

namespace Svea.WebPay.SDK.Tests.Json
{
    using System.Text.Json;

    using JsonSerializer = System.Text.Json.JsonSerializer;

    public class CustomMinorUnitConverterTests
    {
        private readonly long expectedAmount = 100;


        [Fact]
        public void CanDeserialize_Amount()
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{this.expectedAmount}\" }}";

            //ACT
            var result = JsonSerializer.Deserialize<MinorUnit>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(this.expectedAmount, result.InLowestMonetaryUnit);
        }


        [Fact]
        public void CanSerialize_Amount()
        {
            //ARRANGE
            var orderRow = new OrderRow("1", "Test", new MinorUnit(1), new MinorUnit(1), new MinorUnit(0), new MinorUnit(25),
                "#", "", 1);

            //ACT
            var result = JsonSerializer.Serialize(orderRow, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);
            obj.RootElement.TryGetProperty("unitPrice", out var amount);

            //ASSERT
            Assert.Equal(this.expectedAmount, amount.GetInt32());
        }
    }
}
