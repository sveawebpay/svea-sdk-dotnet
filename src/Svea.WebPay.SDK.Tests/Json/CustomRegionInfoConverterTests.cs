namespace Svea.WebPay.SDK.Tests.Json
{
    using Svea.WebPay.SDK.Json;

    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.Json;

    using Xunit;

    using JsonSerializer = System.Text.Json.JsonSerializer;

    public class CustomRegionInfoConverterTests
    {
        [Theory]
        [InlineData("SE")]
        [InlineData("NO")]
        [InlineData("GB")]
        public void CanDeSerialize_RegionInfo(string regionInfoString)
        {
            //ARRANGE
            var jsonObject = $"{{ \"x\": \"{regionInfoString}\" }}";
            
            //ACT
            var result = JsonSerializer.Deserialize<RegionInfo>(jsonObject, JsonSerialization.Settings);

            //ASSERT
            Assert.Equal(regionInfoString, result.Name);
        }


        [Theory]
        [InlineData("SE")]
        [InlineData("NO")]
        [InlineData("GB")]
        public void CanSerialize_RegionInfo(string regionInfoString)
        {
            //ARRANGE
            var dummy = new
            {
                Region = new RegionInfo(regionInfoString)
            };

            //ACT
            var result = JsonSerializer.Serialize(dummy, JsonSerialization.Settings);
            var obj = JsonDocument.Parse(result);

            obj.RootElement.TryGetProperty("region", out var region);
            //ASSERT
            Assert.Equal(regionInfoString, region.GetString());
        }
    }
}
