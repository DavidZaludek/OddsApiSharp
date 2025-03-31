using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers.Websocket;

public partial class WsMsgHeader
{
    [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
    public string Channel { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }
}