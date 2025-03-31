using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public partial class Score
{
    [JsonProperty("participant1Score", NullValueHandling = NullValueHandling.Ignore)]
    public long? Participant1Score { get; set; }

    [JsonProperty("participant2Score", NullValueHandling = NullValueHandling.Ignore)]
    public long? Participant2Score { get; set; }
}