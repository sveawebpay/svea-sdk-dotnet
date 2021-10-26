using System;

namespace Svea.WebPay.SDK.Json
{
    using Svea.WebPay.SDK.CheckoutApi;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomCurrencyCodeConverter : JsonConverter<CurrencyCode>
    {
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
    }
}
