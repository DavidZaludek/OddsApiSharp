using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class OddsResponseWrapper
{
    [JsonProperty("sportId", NullValueHandling = NullValueHandling.Ignore)]
    public long? SportId { get; set; }

    [JsonProperty("sportName", NullValueHandling = NullValueHandling.Ignore)]
    public string SportName { get; set; }

    [JsonProperty("tournaments", NullValueHandling = NullValueHandling.Ignore)]
    public List<Tournament> Tournaments { get; set; }
}