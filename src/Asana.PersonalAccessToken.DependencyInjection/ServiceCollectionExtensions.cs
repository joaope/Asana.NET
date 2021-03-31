﻿using Microsoft.Extensions.DependencyInjection;

namespace Asana.PersonalAccessToken.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAsana(this IServiceCollection services, string accessToken, AsanaClientOptions options)
        {
            return services
                .AddSingleton(options)
                .AddSingleton<AsanaAccessTokenOptions>(accessToken)
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddScoped<Dispatcher, AsanaAccessTokenDispatcher>()
                .AddHttpClient<AsanaAccessTokenDispatcher>();
        }

        public static IHttpClientBuilder AddAsana(this IServiceCollection services, string accessToken)
        {
            return AddAsana(services, accessToken, AsanaClientOptions.Default);
        }
    }
}
