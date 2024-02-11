using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace BuildCarDatabase
{
    public static class DatabaseOperations
    {
        private const string DatabaseConnectionString = "Server=lundeconsultno01.mysql.domeneshop.no;Database=lundeconsultno01;User=lundeconsultno01;Password=gove-6666-4111-megga";

        public static List<Cars> GenerateCarDataFromDatabase()
        {
            List<Cars> listOfCars = GetCarsFromDatabase();
            return listOfCars;
        }

        public static string ExecuteQuery(string connectionString, string query)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                using MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Dictionary<string, object>> dataList = DataTableToList(dataTable);

                string jsonData = JsonConvert.SerializeObject(dataList, Formatting.Indented);
                return jsonData;
            }
            catch (Exception ex)
            {
                // Log the exception using a logging framework or console
                Console.WriteLine($"Error executing query: {ex.Message}");
                return string.Empty;
            }
        }

        private static List<Dictionary<string, object>> DataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, object> dataRow = new Dictionary<string, object>();

                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.ColumnName == Columns.Kjøpsannonse || col.ColumnName == Columns.Salgsannonse)
                    {
                        dataRow[col.ColumnName] = row[col].ToString() ?? throw new InvalidOperationException();
                    }
                    else
                    {
                        dataRow[col.ColumnName] = row[col];
                    }
                }

                dataList.Add(dataRow);
            }

            return dataList;
        }

        private static List<Cars> GetCarsFromDatabase()
        {
            List<Cars> carsFromDatabase = new List<Cars>();

            try
            {
                using MySqlConnection connection = new MySqlConnection(DatabaseConnectionString);
                using MySqlCommand command = new MySqlCommand(SqlQuery.Query, connection);
                connection.Open();

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cars car = new Cars
                    {
                        BilId = reader.GetInt32(Columns.BilId),
                        MerkeNavn = reader.IsDBNull(reader.GetOrdinal(Columns.MerkeNavn)) ? null : reader.GetString(reader.GetOrdinal(Columns.MerkeNavn)),
                        // ... other properties
                    };

                    carsFromDatabase.Add(car);
                }
            }
            catch (Exception ex)
            {
                // Log the exception using a logging framework or console
                Console.WriteLine($"Error fetching cars from the database: {ex.Message}");
            }

            return carsFromDatabase;
        }
    }
}