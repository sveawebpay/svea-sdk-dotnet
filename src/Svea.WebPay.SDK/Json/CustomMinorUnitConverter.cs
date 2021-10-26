using System;
using System.Linq;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomMinorUnitConverter : JsonConverter<MinorUnit>
    {
        public override MinorUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    return MinorUnit.FromLowestMonetaryUnit(reader.GetInt64());
                }

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                var unit = 0L;
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        reader.Read();
                        if (!long.TryParse(reader.GetString(), out unit))
                        {
                            throw new JsonException($"Error converting {reader.GetString() ?? "Null"} to {typeToConvert.Name}.");
                        }
                    }

                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return MinorUnit.FromLowestMonetaryUnit(unit);
                    }
                }

                return new MinorUnit(reader.GetInt64());
            }
            catch (Exception exception)
            {
                throw new JsonException($"Error converting {reader.GetString() ?? "Null"} to {typeToConvert.Name}.", exception);
            }
        }

        public override void Write(Utf8JsonWriter writer, MinorUnit value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.InLowestMonetaryUnit);
        }
    }
}
