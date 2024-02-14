using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildCarDatabase
{
    public class JsonDateTimeConverter : JsonConverter<DateTime?>
    { // Custom JSON converter for DateTime properties
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime? dateTime = null;

            if (reader.TokenType != JsonTokenType.Number) return dateTime;

            if (reader.TryGetDateTime(out DateTime parsedDateTime))
            {
                dateTime = parsedDateTime;
            }

            return dateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        { // Method to write DateTime to JSON
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("dd.MM.yyyy"));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
        
        public class JsonLinkConverter : JsonConverter<string?>
        { // Nested class for handling string conversion
            public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetString();
            }

            public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }
}