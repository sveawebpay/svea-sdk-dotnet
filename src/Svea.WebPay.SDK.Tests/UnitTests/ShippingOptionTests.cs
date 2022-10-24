namespace Svea.WebPay.SDK.Tests.UnitTests;

using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.Json;

using System.Text.Json;

using Xunit;

public class ShippingOptionTests
{
    [Fact]
    public void ShippingOptionJson_Should_DeserializeToObject()
    {
        var json = @"{
                   ""id"":""7d9ec357-ba1a-4bcd-ace8-9ef69eac97d6"",
                   ""name"":""DHL Home Delivery"",
                   ""description"":""3-4 arbetsdagar"",
                   ""carrier"":""dhl"",
                   ""price"":200,
                   ""shippingFee"":20000,
                   ""totalShippingFee"":20000,
                   ""location"":{
                      ""id"":""4210"",
                      ""name"":""Stockholm, Hägersten"",
                      ""address"":{
                         ""city"":""Hägersten"",
                         ""countryCode"":""SE"",
                         ""postalCode"":""12630"",
                         ""streetAddress"":""Västberga Allé 41"",
                         ""streetAddress2"":null
                      }
                   },
                   ""fields"":[
                      {
                         ""id"":""FCRECEIVERDOORCODE"",
                         ""value"":""""
                        },
                        {
                            ""id"":""FCRECEIVERSMS"",
                            ""value"":""223•••••35""
                        },
                        {
                            ""id"":""FCRECEIVERPHONE"",
                            ""value"":""223•••••35""
                        },
                        {
                            ""id"":""FCDELIVERYINSTRUCTIONS"",
                            ""value"":""zsdfgs""
                        }
                        ],
                    ""addons "":[],
                    ""timeslot "":"""",
                    ""postalCode "":""11338"",
                    ""orderId "":""8773762""
                }";

        var shippingOption = JsonSerializer.Deserialize<ShippingOption>(json, JsonSerialization.Settings);

        Assert.Equal("7d9ec357-ba1a-4bcd-ace8-9ef69eac97d6", shippingOption?.Id);
        Assert.Equal(200, shippingOption?.Price);
        Assert.Equal(20000, shippingOption?.ShippingFee);
        Assert.Equal("4210", shippingOption?.Location.Id);
        Assert.Equal("Hägersten", shippingOption?.Location.Address.City);
        Assert.Equal("FCRECEIVERSMS", shippingOption?.Fields[1].Id);
        Assert.Equal("223•••••35", shippingOption?.Fields[2].Value);
    }
}