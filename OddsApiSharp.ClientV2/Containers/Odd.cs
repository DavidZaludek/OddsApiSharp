using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class Odd
{
    [JsonProperty("bookmaker", NullValueHandling = NullValueHandling.Ignore)]
    public string Bookmaker { get; set; }

    [JsonProperty("bookmakerFixtureId", NullValueHandling = NullValueHandling.Ignore)]
    public string BookmakerFixtureId { get; set; }

    [JsonProperty("clones", NullValueHandling = NullValueHandling.Ignore)]
    public List<dynamic> Clones { get; set; }

    [JsonProperty("fixturePath", NullValueHandling = NullValueHandling.Ignore)]
    public Uri FixturePath { get; set; }

    [JsonProperty("markets", NullValueHandling = NullValueHandling.Ignore)]
    public List<Market> Markets { get; set; }

    [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? UpdatedAt { get; set; }
}