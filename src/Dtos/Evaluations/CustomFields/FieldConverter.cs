using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields
{
    public class FieldConverter : JsonConverter<FieldDto>
    {
        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
        {
            { FieldTypes.SelectList, typeof(SelectListDto)},
            { FieldTypes.Checkbox, typeof(CheckboxDto)},
        };

        public override bool CanConvert(Type typeToConvert) => typeof(FieldDto).IsAssignableFrom(typeToConvert);

        public override FieldDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            // Copy the current state from reader (it's a struct). Will allow us to read the whole thing again after finding the correct type
            var readerAtStart = reader;

            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var jsonObject = jsonDocument.RootElement;

            if (!jsonObject.TryGetProperty("type", out var fieldType))
            {
                jsonObject.TryGetProperty("Type", out fieldType);
            }

            var type = fieldType.GetString();

            if (!string.IsNullOrEmpty(type) && TypeMap.ContainsKey(type))
            {
                return JsonSerializer.Deserialize(ref readerAtStart, TypeMap[type], options) as FieldDto;
            }

            throw new NotSupportedException($"{type ?? "<unknown>"} can not be deserialized");
        }

        public override void Write(Utf8JsonWriter writer, FieldDto value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

}
