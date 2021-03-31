using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.PersonalAccessToken
{
    public sealed class AccessTokenDispatcher : Dispatcher
    {
        private readonly AccessTokenOptions _accessTokenOptions;

        public AccessTokenDispatcher(
            AccessTokenOptions accessTokenOptions, 
            HttpClient httpClient, 
            AsanaClientOptions options) 
            : base(httpClient, options)
        {
            _accessTokenOptions = accessTokenOptions;
        }

        public AccessTokenDispatcher(
            AccessTokenOptions accessTokenOptions,
            AsanaClientOptions options)
            : this(accessTokenOptions, new HttpClient(), options)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessTokenOptions);
        }
    }
}