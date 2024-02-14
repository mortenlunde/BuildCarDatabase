using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuildCarDatabase;

public class Cars
{
    [Column("BilID")]
    public int BilId { get; set; }

    [Column("MerkeNavn")]
    public string? MerkeNavn { get; set; }

    [Column("ModellNavn")]
    public string? ModellNavn { get; set; }

    [Column("Årsmodell")]
    public int Årsmodell { get; set; }

    [Column("Farge")]
    public string? Farge { get; set; }

    [Column("RegNR")]
    public string? RegNr { get; set; }

    [Column("Hestekrefter")]
    public string? Hestekrefter { get; set; }

    [Column("Karosseri")]
    public string? Karosseri { get; set; }

    [Column("Drivstoff")]
    public string? Drivstoff { get; set; }

    [Column("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Kjøpsdato")]
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime? Kjøpsdato { get; set; }

    [JsonPropertyName("Salgsdato")]
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime? Salgsdato { get; set; }

    [Column("KilometerstandVedKjøp")]
    public string? KilometerstandVedKjøp { get; set; }

    [Column("KilometerstandVedSalg")]
    public string? KilometerstandVedSalg { get; set; }
    [Column("Kjøpsannonse")]
    public string? Kjøpsannonse { get; set; }

    [Column("Salgsannonse")]
    public string? Salgsannonse { get; set; }
}