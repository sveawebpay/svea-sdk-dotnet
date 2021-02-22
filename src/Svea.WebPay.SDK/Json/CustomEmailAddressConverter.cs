using System;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomEmailAddressConverter : JsonConverter<EmailAddress>

    {
        public override EmailAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new EmailAddress(reader.GetString());
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var email = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    email = reader.GetString();
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new EmailAddress(email);
                }
            }

            return new EmailAddress(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
