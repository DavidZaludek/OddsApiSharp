using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class Fixture
{
    [JsonProperty("externalProviders", NullValueHandling = NullValueHandling.Ignore)]
    public ExternalProviders ExternalProviders { get; set; }

    [JsonProperty("fixtureId", NullValueHandling = NullValueHandling.Ignore)]
    public string FixtureId { get; set; }

    [JsonProperty("fixtureStatus", NullValueHandling = NullValueHandling.Ignore)]
    public FixtureStatus FixtureStatus { get; set; }

    [JsonProperty("odds", NullValueHandling = NullValueHandling.Ignore)]
    public List<Odd> Odds { get; set; }

    [JsonProperty("participant1Id", NullValueHandling = NullValueHandling.Ignore)]
    public long? Participant1Id { get; set; }

    [JsonProperty("participant1Name", NullValueHandling = NullValueHandling.Ignore)]
    public string Participant1Name { get; set; }

    [JsonProperty("participant2Id", NullValueHandling = NullValueHandling.Ignore)]
    public long? Participant2Id { get; set; }

    [JsonProperty("participant2Name", NullValueHandling = NullValueHandling.Ignore)]
    public string Participant2Name { get; set; }

    [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
    public Score Score { get; set; }

    [JsonProperty("seasonId", NullValueHandling = NullValueHandling.Ignore)]
    public long? SeasonId { get; set; }

    [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
    public long? StartTime { get; set; }
}