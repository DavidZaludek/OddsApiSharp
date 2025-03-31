using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class Outcome
{
    [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Active { get; set; }

    [JsonProperty("betslip", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Betslip { get; set; }

    [JsonProperty("bookmakerOutcomeId", NullValueHandling = NullValueHandling.Ignore)]
    public string BookmakerOutcomeId { get; set; }

    [JsonProperty("changedAt", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? ChangedAt { get; set; }

    [JsonProperty("oddsId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OddsId { get; set; }

    [JsonProperty("outcomeId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OutcomeId { get; set; }

    [JsonProperty("outcomeName", NullValueHandling = NullValueHandling.Ignore)]
    public string OutcomeName { get; set; }

    [JsonProperty("playerId", NullValueHandling = NullValueHandling.Ignore)]
    public long? PlayerId { get; set; }

    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public double? Price { get; set; }
}