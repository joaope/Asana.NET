using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.PersonalAccessToken
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithPersonalAccessToken(string accessToken, AsanaClientOptions options)
        {
            return AsanaClient
                .Create(options)
                .WithDispatcher(new AccessTokenDispatcher(GetHttpClient(options.ApiBaseUri, accessToken), options));
        }

        private static HttpClient GetHttpClient(Uri apiBaseUri, string accessToken) => new HttpClient
        {
            BaseAddress = apiBaseUri,
            DefaultRequestHeaders =
            {
                Accept = {MediaTypeWithQualityHeaderValue.Parse("application/json")},
                Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
            }
        };
    }
}