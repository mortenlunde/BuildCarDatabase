namespace BuildCarDatabase
{
    public static class SqlQuery
    {
        public const string Query = 
        @"SELECT BilID, Merker.MerkeNavn, Modeller.ModellNavn, " +
        "Årsmodell, Farge, Registreringsnummer AS RegNR, " +
        "CONCAT(Hestekrefter, ' hk') AS Hestekrefter, Karosseri, Drivstoff, " +
        "CASE WHEN Status = 0 THEN 'Solgt' WHEN Status = 1 THEN 'Eier' ELSE 'Ukjent' END AS Status, " +
        "Kjøpsdato, CASE WHEN Salgsdato IS NULL THEN '' ELSE Salgsdato END AS Salgsdato, " +
        "CONCAT(CASE WHEN KilometerstandVedKjøp <= 99999 THEN FORMAT(KilometerstandVedKjøp, 0) " +
        "WHEN KilometerstandVedKjøp BETWEEN 100000 AND 999999 THEN CONCAT(FORMAT(KilometerstandVedKjøp / 1000, 0), '.', " +
        "LPAD(KilometerstandVedKjøp % 1000, 3, '0')) ELSE 'Ukjent' END, ' km') AS KilometerstandVedKjøp, " +
        "CASE WHEN KilometerstandVedSalg IS NULL THEN '' ELSE " +
        "CONCAT(CASE WHEN KilometerstandVedSalg <= 99999 THEN FORMAT(KilometerstandVedSalg, 0) " +
        "WHEN KilometerstandVedSalg BETWEEN 100000 AND 999999 THEN " +
        "CONCAT(FORMAT(KilometerstandVedSalg / 1000, 0), '.', LPAD(KilometerstandVedSalg % 1000, 3, '0')) ELSE 'Ukjent' END, ' km')" +
        "END AS KilometerstandVedSalg, " +
        "IF(KjøpsannonseLink IS NULL, '', KjøpsannonseLink) AS Kjøpsannonse, " +
        "IF(SalgsannonseLink IS NULL, '', SalgsannonseLink) AS Salgsannonse " +
        "FROM Eierskap E JOIN Merker ON E.MerkeID = Merker.MerkeID JOIN Modeller ON E.ModellID = Modeller.ModellID ORDER BY BilID;";
    }
}