using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public class Sport
{
    [JsonProperty("sportId")]
    public int SportId { get; set; }

    [JsonProperty("sportName")]
    public string SportName { get; set; }
}