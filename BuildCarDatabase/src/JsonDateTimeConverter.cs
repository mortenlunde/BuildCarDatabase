using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildCarDatabase
{
    public class JsonDateTimeConverter : JsonConverter<DateTime?>
    {
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
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("dd.MM.yyyy"));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}