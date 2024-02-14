using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace BuildCarDatabase
{
    public static class WebServer
    {
        private static string BaseUrl => "http://localhost:8080/";
        private static string? _htmlContent;

        public static void Start(string? initialHtmlContent)
        {
            _htmlContent = initialHtmlContent;  // Set the HTML content

            using HttpListener listener = new();
            listener.Prefixes.Add(BaseUrl);
            listener.Start();
            Console.WriteLine($"Web server is running at {BaseUrl}");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                Task.Run(() => ProcessRequest(context));
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void ProcessRequest(HttpListenerContext context)
        { // Method to process incoming HTTP requests
            try
            {   // Send the HTML content as the response
                if (context.Request.Url != null && context.Request.Url.AbsolutePath == "/")
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    
                    if (_htmlContent == null) return;
                    byte[] buffer = Encoding.UTF8.GetBytes(_htmlContent);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    List<Cars> cars = DatabaseOperations.GetCarsFromDatabase();
            
                    // Convert the list of cars to JSON string
                    string response = JsonConvert.SerializeObject(cars, Formatting.Indented);                   
                    context.Response.ContentType = "text/html; charset=utf-8";

                    byte[] buffer = Encoding.UTF8.GetBytes(response);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing request: {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}