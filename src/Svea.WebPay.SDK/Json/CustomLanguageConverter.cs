using System;

namespace Svea.WebPay.SDK.Json
{
    using Svea.WebPay.SDK.CheckoutApi;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomLanguageConverter : JsonConverter<Language>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Language);
        }

        public override Language Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                return new Language(reader.GetString());
            }

            var language = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    language = reader.GetString();
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new Language(language);
                }
            }

            throw new InvalidOperationException(
                "Unhandled case for CustomLanguageConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }

        public override void Write(Utf8JsonWriter writer, Language value, JsonSerializerOptions options)
        {
            if (null == value)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is Language language)
            {
                writer.WriteStringValue(language.ToString());
                return;
            }

            throw new InvalidOperationException(
                "Unhandled case for CustomLanguageConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }


        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    if (reader.TokenType == JsonToken.String)
        //    {
        //        return new Language((string)reader.Value);
        //    }

        //    if (reader.TokenType == JsonToken.StartObject)
        //    {
        //        var jo = JObject.Load(reader);
        //        var language = jo.Values().FirstOrDefault()?.ToString();
        //        return language != null ? new Language(language) : null;
        //    }

        //    if (reader.TokenType == JsonToken.Null)
        //    {
        //        return null;
        //    }

        //    throw new InvalidOperationException(
        //        "Unhandled case for CustomLanguageConverter. Check to see if this converter has been applied to the wrong serialization type.");

        //}


        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    if (null == value)
        //    {
        //        writer.WriteNull();
        //        return;
        //    }

        //    if (value is Language language)
        //    {
        //        writer.WriteValue(language.ToString());
        //        return;
        //    }

        //    throw new InvalidOperationException(
        //        "Unhandled case for CustomLanguageConverter. Check to see if this converter has been applied to the wrong serialization type.");

        //}
    }
}
