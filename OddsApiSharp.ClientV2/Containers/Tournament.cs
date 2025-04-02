using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public class Tournament
{
    [JsonProperty("categoryName", NullValueHandling = NullValueHandling.Ignore)]
    public string CategoryName { get; set; }

    [JsonProperty("fixtures", NullValueHandling = NullValueHandling.Ignore)]
    public List<Fixture> Fixtures { get; set; }

    [JsonProperty("categorySlug")]
    public string CategorySlug { get; set; }

    [JsonProperty("sportId")]
    public int SportId { get; set; }

    [JsonProperty("tournamentId")]
    public int TournamentId { get; set; }

    [JsonProperty("tournamentName")]
    public string TournamentName { get; set; }

    [JsonProperty("tournamentSlug")]
    public string TournamentSlug { get; set; }
}