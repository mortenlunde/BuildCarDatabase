using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildCarDatabase
{
    public static class Program
    {
        private const string HtmlPage = "cars.html";
        private const string JsonFilename = "cars.json";
        private static async Task Main()
        {
            // Read HTML content from file
            string htmlContent = await File.ReadAllTextAsync(HtmlPage);
            // Get cars data from the database
            List<Cars> cars = DatabaseOperations.GetCarsFromDatabase();
            // Serialize cars data to JSON and write to a file
            SerializeToJsonAndWriteFile(cars);
            // Start the web server with the HTML content
            WebServer.Start(htmlContent);
        }
        private static void SerializeToJsonAndWriteFile(List<Cars> cars)
        {
            string jsonString = JsonSerializer.Serialize(cars, JsonOptions);
            File.WriteAllText(JsonFilename, jsonString, Encoding.UTF8);
        }
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonDateTimeConverter(), new JsonDateTimeConverter.JsonLinkConverter() }
        };
    }
}