using Newtonsoft.Json;

public partial class Bookmaker
{
    [JsonProperty("bookmakerName", NullValueHandling = NullValueHandling.Ignore)]
    public string BookmakerName { get; set; }

    [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
    public string Slug { get; set; }
}