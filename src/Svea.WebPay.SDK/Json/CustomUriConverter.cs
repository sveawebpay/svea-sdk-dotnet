using System;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Web;

    public class CustomUriConverter : JsonConverter<Uri>
    {
        public override Uri Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new Uri(reader.GetString(), UriKind.RelativeOrAbsolute);
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            
            var uri = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    uri = reader.GetString();
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new Uri(uri, UriKind.RelativeOrAbsolute);
                }
            }

            throw new InvalidOperationException(
                "Unhandled case for CustomUriConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }

        public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
        {
            if (null == value)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is Uri uri)
            {
                writer.WriteStringValue(HttpUtility.UrlDecode(uri.OriginalString));
                return;
            }
            
            throw new InvalidOperationException("Unhandled case for CustomUriConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }
    }
}
