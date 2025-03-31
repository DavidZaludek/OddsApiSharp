using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class Market
{
    [JsonProperty("bookmakerMarketId")]
    public string BookmakerMarketId { get; set; }

    [JsonProperty("handicap", NullValueHandling = NullValueHandling.Ignore)]
    public double? Handicap { get; set; }

    [JsonProperty("marketId", NullValueHandling = NullValueHandling.Ignore)]
    public long? MarketId { get; set; }

    [JsonProperty("marketName", NullValueHandling = NullValueHandling.Ignore)]
    public string MarketName { get; set; }

    [JsonProperty("outcomes", NullValueHandling = NullValueHandling.Ignore)]
    public List<Outcome> Outcomes { get; set; }

    [JsonProperty("playerProp", NullValueHandling = NullValueHandling.Ignore)]
    public bool? PlayerProp { get; set; }
}