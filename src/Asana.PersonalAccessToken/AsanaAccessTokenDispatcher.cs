using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.PersonalAccessToken
{
    public sealed class AsanaAccessTokenDispatcher : Dispatcher
    {
        private readonly AsanaAccessTokenOptions _asanaAccessTokenOptions;

        public AsanaAccessTokenDispatcher(
            AsanaAccessTokenOptions asanaAccessTokenOptions, 
            HttpClient httpClient, 
            AsanaClientOptions options) 
            : base(httpClient, options)
        {
            _asanaAccessTokenOptions = asanaAccessTokenOptions;
        }

        public AsanaAccessTokenDispatcher(
            AsanaAccessTokenOptions asanaAccessTokenOptions,
            AsanaClientOptions options)
            : this(asanaAccessTokenOptions, new HttpClient(), options)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _asanaAccessTokenOptions.AccessToken);
        }
    }
}