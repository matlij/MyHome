﻿using Microsoft.Extensions.Options;
using MyHome.Core.Options;
using MyHome.Core.Repositories.WifiSocket;

namespace MyHome.ApiService.Extensions;

public static class RadiatorServiceConfiguration
{
    public static IServiceCollection AddWifiSocketClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<WifiSocketsService>();

        var radiatorConfigs = configuration
            .GetSection("WifiSockets")
            .Get<List<WifiSocketOptions>>() ?? [];

        foreach (var config in radiatorConfigs)
        {
            services.AddScoped(sp =>
            {
                var options = Options.Create(config);
                var logger = sp.GetRequiredService<ILogger<WifiSocketClient>>();
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                return new WifiSocketClient(httpClientFactory, options, logger);
            });
        }

        return services;
    }
}
