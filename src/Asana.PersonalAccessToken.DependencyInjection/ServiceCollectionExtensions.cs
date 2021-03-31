using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Asana.PersonalAccessToken.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddAsanaPersonalAccessToken(this IServiceCollection services, string accessToken, AsanaClientOptions options)
        {
            return services
                .AddSingleton(options)
                .AddScoped<IAsanaClient, AsanaClient>()
                .AddHttpClient<AccessTokenDispatcher>(client =>
                {
                    client.BaseAddress = options.ApiBaseUri;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appplication/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                });
        }
    }
}
