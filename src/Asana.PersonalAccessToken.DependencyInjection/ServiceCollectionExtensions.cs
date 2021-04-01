using Microsoft.Extensions.DependencyInjection;

namespace Asana.PersonalAccessToken.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAsana(this IServiceCollection services, AsanaAccessTokenOptions accessToken, AsanaClientOptions options)
        {
            return services
                .AddSingleton(options)
                .AddSingleton(accessToken)
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddScoped<Dispatcher, AsanaAccessTokenDispatcher>()
                .AddHttpClient<AsanaAccessTokenDispatcher>();
        }

        public static IHttpClientBuilder AddAsana(this IServiceCollection services, AsanaAccessTokenOptions accessToken)
        {
            return AddAsana(services, accessToken, AsanaClientOptions.Default);
        }
    }
}
