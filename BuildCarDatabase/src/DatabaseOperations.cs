using MySql.Data.MySqlClient;

namespace BuildCarDatabase
{
    public static class DatabaseOperations
    {
        private const string DatabaseConnectionString = 
            "Server=lundeconsultno01.mysql.domeneshop.no;Database=lundeconsultno01;User=lundeconsultno01;Password=gove-6666-4111-megga";
        public static List<Cars> GetCarsFromDatabase()
        {
            List<Cars> carsFromDatabase = [];
            try
            {
                // Establish database connection
                using MySqlConnection connection = new(DatabaseConnectionString);
                using MySqlCommand command = new(SqlQuery.Query, connection);
                connection.Open();

                // Execute SQL query and process results
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cars car = new()
                    {
                        BilId = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.BilId)))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal(nameof(Cars.BilId))),
                        MerkeNavn = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.MerkeNavn)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.MerkeNavn))),
                        ModellNavn = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.ModellNavn)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.ModellNavn))),
                        Årsmodell = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Årsmodell)))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal(nameof(Cars.Årsmodell))),
                        Farge = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Farge)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Farge))),
                        RegNr = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.RegNr)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.RegNr))),
                        Hestekrefter = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Hestekrefter)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Hestekrefter))),
                        Karosseri = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Karosseri)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Karosseri))),
                        Drivstoff = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Drivstoff)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Drivstoff))),
                        Status = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Status)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Status))),
                        Kjøpsdato = reader.GetDateTime(reader.GetOrdinal(nameof(Cars.Kjøpsdato))),
                        Salgsdato = !string.IsNullOrEmpty(reader.GetString(nameof(Cars.Salgsdato)))
                            ? reader.GetDateTime("Salgsdato")
                            : null,
                        KilometerstandVedKjøp = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.KilometerstandVedKjøp)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.KilometerstandVedKjøp))),
                        KilometerstandVedSalg = !string.IsNullOrEmpty(reader.GetString(nameof(Cars.KilometerstandVedSalg)))
                            ? reader.GetString("KilometerstandVedSalg")
                            : null,
                        Kjøpsannonse = reader.IsDBNull(reader.GetOrdinal(nameof(Cars.Kjøpsannonse)))
                            ? null
                            : reader.GetString(reader.GetOrdinal(nameof(Cars.Kjøpsannonse))),
                        Salgsannonse = !string.IsNullOrEmpty(reader.GetString(nameof(Cars.Salgsannonse)))
                            ? reader.GetString("Salgsannonse")
                            : null
                    };
                    carsFromDatabase.Add(car);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cars from the database: {ex.Message}");
            }
            return carsFromDatabase;
        }
    }
}