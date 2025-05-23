﻿namespace OddsApiSharp.ClientV2;

public class OddsApiSettings
{
    public string ApiKey { get; set; }
    public string ClientName { get; set; }
    public string WebsocketUrl { get; set; } = "wss://push.oddspapi.io";
    public string ApiUrl { get; set; } = "https://api-v2.oddspapi.io";
    public List<string> AllowedBookmakers { get; set; } = new List<string>()
    {
        "roobet"
    }; 
}