using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    //Original code found here: https://gist.github.com/gubenkoved/999eb73e227b7063a67a50401578c3a7
    public class TypesafeEnumConverter : JsonConverter<Type>
    {
        [ThreadStatic]
        private static Dictionary<Type, Dictionary<string, object>> _fromValueMap; // string representation to Enum value map

        [ThreadStatic]
        private static Dictionary<Type, Dictionary<object, string>> _toValueMap; // Enum value to string map

        private string UnknownValue { get; set; } = "Unknown";

        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            InitMap(typeToConvert);
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            typeToConvert = underlyingType != null ? underlyingType : typeToConvert;

            if (reader.TokenType == JsonTokenType.String)
            {
                var enumText = reader.GetString();

                var val = FromValue(typeToConvert, enumText);

                if (val != null)
                {
                    return val;
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var enumVal = reader.GetInt32();
                var values = (int[])Enum.GetValues(typeToConvert);
                if (values.Contains(enumVal))
                {
                    return Enum.Parse(typeToConvert, enumVal.ToString()).GetType();
                }
            }

            var names = Enum.GetNames(typeToConvert);

            var unknownName = names
                .Where(n => string.Equals(n, UnknownValue, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (unknownName == null)
            {
                throw new JsonException($"Unable to parse '{reader.GetString()}' to enum {typeToConvert}. Consider adding Unknown as fail-back value.");
            }

            return Enum.Parse(typeToConvert, unknownName).GetType();
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            var enumType = value;

            InitMap(enumType);

            var val = ToValue(enumType, value);

            writer.WriteStringValue(val);
        }

        private static void InitMap(Type enumType)
        {
            var underlyingType = Nullable.GetUnderlyingType(enumType);
            enumType = underlyingType ?? enumType;

            if (_fromValueMap == null)
            {
                _fromValueMap = new Dictionary<Type, Dictionary<string, object>>();
            }

            if (_toValueMap == null)
            {
                _toValueMap = new Dictionary<Type, Dictionary<object, string>>();
            }

            if (_fromValueMap.ContainsKey(enumType))
            {
                return; // already initialized
            }

            var fromMap = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var toMap = new Dictionary<object, string>();

            var fields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                var name = field.Name;
                var enumValue = Enum.Parse(enumType, name);

                toMap[enumValue] = name;
                fromMap[name] = enumValue;
            }

            _fromValueMap[enumType] = fromMap;
            _toValueMap[enumType] = toMap;
        }

        private static string ToValue(Type enumType, object obj)
        {
            var map = _toValueMap[enumType];

            return map[obj];
        }

        private static Type FromValue(Type enumType, string value)
        {
            var map = _fromValueMap[enumType];

            return !map.ContainsKey(value) ? null : map[value].GetType();
        }
    }
}