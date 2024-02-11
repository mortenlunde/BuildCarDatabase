using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildCarDatabase
{
    public static class Program
    {
        private const string DatabaseConnectionString = "Server=lundeconsultno01.mysql.domeneshop.no;Database=lundeconsultno01;User=lundeconsultno01;Password=gove-6666-4111-megga";
        private const string HtmlPage = "cars.html";
        private const string Filename = "cars.json";

        private static void Main()
        {
            string htmlContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), HtmlPage));
            
            List<Cars> cars = DatabaseOperations.GenerateCarDataFromDatabase();

            using (MemoryStream stream = new())
            {
                using (Utf8JsonWriter writer = new(stream, new JsonWriterOptions { Indented = true }))
                {
                    JsonDocument doc = JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(cars,
                        new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                            Converters = { new JsonDateTimeConverter(), new JsonLinkConverter() }
                        }));
                    doc.WriteTo(writer);
                }

                string jsonString = Encoding.UTF8.GetString(stream.ToArray());
                File.WriteAllText(Filename, jsonString, Encoding.UTF8);
            }

            DatabaseOperations.ExecuteQuery(DatabaseConnectionString, SqlQuery.Query);

            WebServer.Start(htmlContent);
        }
    }
}