using System;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomEmailAddressConverter : JsonConverter<EmailAddress>

    {
        //private readonly Type[] types;


        //public CustomEmailAddressConverter(params Type[] types)
        //{
        //    this.types = types;
        //}


        //public CustomEmailAddressConverter()
        //{
        //}


        //public override bool CanRead => true;


        //public override bool CanConvert(Type objectType)
        //{
        //    return this.types.Any(t => t == objectType);
        //}


        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    if (reader.TokenType == JsonToken.StartObject)
        //    {
        //        var jo = JObject.Load(reader);
        //        var addressString = jo.Values().FirstOrDefault()?.ToString();
        //        return new EmailAddress(addressString);
        //    }

        //    return new EmailAddress(reader.Value.ToString());
        //}


        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    var t = JToken.FromObject(value);

        //    if (t.Type != JTokenType.Object)
        //        t.WriteTo(writer);
        //    else
        //        writer.WriteValue(value.ToString());
        //}

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
