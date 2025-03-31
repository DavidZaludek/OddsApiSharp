using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class ExternalProviders
{
    [JsonProperty("betgeniusId")]
    public long? BetgeniusId { get; set; }

    [JsonProperty("betradarId", NullValueHandling = NullValueHandling.Ignore)]
    public long? BetradarId { get; set; }

    [JsonProperty("flashscoreId")]
    public string FlashscoreId { get; set; }

    [JsonProperty("sofascoreId")]
    public long? SofascoreId { get; set; }
}