using Microsoft.Extensions.DependencyInjection;

namespace Asana.PersonalAccessToken.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAsana(this IServiceCollection services, string accessToken, AsanaClientOptions options)
        {
            return services
                .AddSingleton(options)
                .AddSingleton<AccessTokenOptions>(accessToken)
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddScoped<Dispatcher, AccessTokenDispatcher>()
                .AddHttpClient<AccessTokenDispatcher>();
        }

        public static IHttpClientBuilder AddAsana(this IServiceCollection services, string accessToken)
        {
            return AddAsana(services, accessToken, AsanaClientOptions.Default);
        }
    }
}
