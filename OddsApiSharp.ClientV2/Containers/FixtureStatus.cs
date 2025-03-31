using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class FixtureStatus
{
    [JsonProperty("live", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Live { get; set; }

    [JsonProperty("statusId", NullValueHandling = NullValueHandling.Ignore)]
    public long? StatusId { get; set; }

    [JsonProperty("statusName", NullValueHandling = NullValueHandling.Ignore)]
    public string StatusName { get; set; }
}