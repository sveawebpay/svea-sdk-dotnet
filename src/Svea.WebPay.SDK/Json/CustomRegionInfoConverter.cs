using System;

namespace Svea.WebPay.SDK.Json
{
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomRegionInfoConverter : JsonConverter<RegionInfo>
    {
        public override RegionInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                return new RegionInfo(reader.GetString());
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var region = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    region = reader.GetString();
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new RegionInfo(region);
                }
            }

            throw new InvalidOperationException(
                "Unhandled case for CustomRegionInfoConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }
        
        public override void Write(Utf8JsonWriter writer, RegionInfo value, JsonSerializerOptions options)
        {
            if (null == value)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is RegionInfo region)
            {
                writer.WriteStringValue(region.Name);
                return;
            }

            throw new InvalidOperationException(
                "Unhandled case for CustomLanguageConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }
    }
}
