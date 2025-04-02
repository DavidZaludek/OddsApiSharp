using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers.Websocket;

public partial class WsFixtureData
{
    [JsonProperty("bookmaker", NullValueHandling = NullValueHandling.Ignore)]
    public string Bookmaker { get; set; }

    [JsonProperty("bookmakerFixtureId", NullValueHandling = NullValueHandling.Ignore)]
    public string BookmakerFixtureId { get; set; }

    [JsonProperty("bookmakerIsActive", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BookmakerIsActive { get; set; }

    [JsonProperty("fixtureId", NullValueHandling = NullValueHandling.Ignore)]
    public string FixtureId { get; set; }

    [JsonProperty("fixturePath", NullValueHandling = NullValueHandling.Ignore)]
    public Uri FixturePath { get; set; }

    [JsonProperty("outcomes", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, List<Outcome>> Outcomes { get; set; }

    [JsonProperty("outcomesHash", NullValueHandling = NullValueHandling.Ignore)]
    public string OutcomesHash { get; set; }

    [JsonProperty("sportId", NullValueHandling = NullValueHandling.Ignore)]
    public long? SportId { get; set; }

    [JsonProperty("tournamentId", NullValueHandling = NullValueHandling.Ignore)]
    public long? TournamentId { get; set; }

    [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? UpdatedAt { get; set; }
}