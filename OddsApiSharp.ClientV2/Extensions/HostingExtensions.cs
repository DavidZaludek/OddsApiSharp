using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OddsApiSharp.ClientV2.Extensions;

public static class HostingExtensions
{
    public static IServiceCollection AddOddsApiClientV2(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions();
        services.Configure<OddsApiSettings>(config.GetSection(nameof(OddsApiSettings)));
        services.AddSingleton<OddsApiConnection>();
        services.AddSingleton<OddsApiWebsocket>();
        services.AddHostedService(provider => provider.GetRequiredService<OddsApiWebsocket>());
        return services;
    }
}