using System;
using System.Linq;

namespace Svea.WebPay.SDK.Json
{
    using Svea.WebPay.SDK.CheckoutApi;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomCurrencyCodeConverter : JsonConverter<CurrencyCode>
    {
        //private readonly Type[] types;


        //public CustomCurrencyCodeConverter(params Type[] types)
        //{
        //    this.types = types;
        //}


        //public CustomCurrencyCodeConverter()
        //{
        //}
        
        //public override bool CanConvert(Type objectType)
        //{
        //    return this.types.Any(t => t == objectType);
        //}

        public override CurrencyCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            
            var currencyCode = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    currencyCode = reader.GetString();
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new CurrencyCode(currencyCode);
                }
            }

            return new CurrencyCode(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CurrencyCode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    if (reader.TokenType == JsonToken.StartObject)
        //    {
        //        var jo = JObject.Load(reader);
        //        var currencyCodeString = jo.Values().FirstOrDefault()?.ToString();
        //        return new CurrencyCode(currencyCodeString);
        //    }

        //    return new CurrencyCode(reader.Value.ToString());
        //}

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    var t = JToken.FromObject(value);

        //    if (t.Type != JTokenType.Object)
        //    {
        //        t.WriteTo(writer);
        //    }
        //    else
        //    {
        //        writer.WriteValue(value.ToString());
        //    }
        //}
    }
}
