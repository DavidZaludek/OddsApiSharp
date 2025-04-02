using OddsApiSharp.ClientV2.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddOddsApiClientV2(builder.Configuration);
builder.Services.AddSingleton<OddsApiClient>();
builder.Services.AddHostedService(sp => sp.GetService<OddsApiClient>());


var app = builder.Build();

app.Run();
