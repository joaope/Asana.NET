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
                .AddScoped<IOAuthApplication, OAuthApplication>()
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddHttpClient<OAuthApplication>()
                .Services
                .AddHttpClient<Dispatcher, OAuthDispatcher>();
        }

        public static IHttpClientBuilder AddAsana(
            this IServiceCollection services,
            OAuthApplicationOptions oAuthApplicationOptions)
        {
            return AddAsana(services, oAuthApplicationOptions, AsanaClientOptions.Default);
        }
    }
}