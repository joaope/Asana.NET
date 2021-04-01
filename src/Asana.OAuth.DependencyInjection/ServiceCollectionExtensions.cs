using Microsoft.Extensions.DependencyInjection;

namespace Asana.OAuth.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAsana(
            this IServiceCollection services,
            OAuthApplicationOptions oAuthApplicationOptions,
            AsanaClientOptions options)
        {
            return services
                .AddSingleton(options)
                .AddSingleton(oAuthApplicationOptions)
                .AddScoped<IAsanaOAuthApplication, AsanaOAuthApplication>()
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddScoped<Dispatcher, AsanaOAuthDispatcher>()
                .AddHttpClient<AsanaOAuthApplication>()
                .Services
                .AddHttpClient<Dispatcher, AsanaOAuthDispatcher>();
        }

        public static IHttpClientBuilder AddAsana(
            this IServiceCollection services,
            OAuthApplicationOptions oAuthApplicationOptions)
        {
            return AddAsana(services, oAuthApplicationOptions, AsanaClientOptions.Default);
        }
    }
}