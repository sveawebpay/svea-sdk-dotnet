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
                    return new MinorUnit(reader.GetInt64());
                }

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                long unit = 0l;
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
                        return new MinorUnit(unit);
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
            //var t = JToken.FromObject(value);

            //if (t.Type != JTokenType.Object)
            //    t.WriteTo(writer);
            //else
                writer.WriteNumberValue(value.Value);
        }


        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    try
        //    {
        //        long value;
        //        if (reader.Value == null)
        //        {
        //            var jo = JObject.Load(reader);

        //            value = (long)jo.First.Values().FirstOrDefault();
        //        }
        //        else
        //        {
        //            value = (long)reader.Value;
        //        }

        //        return new MinorUnit(value);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new JsonSerializationException($"Error converting {reader.Value ?? "Null"} to {objectType.Name}.", exception);
        //    }
        //}


        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    var t = JToken.FromObject(value);

        //    if (t.Type != JTokenType.Object)
        //        t.WriteTo(writer);
        //    else
        //        writer.WriteValue(value.ToString());
        //}
    }
}
