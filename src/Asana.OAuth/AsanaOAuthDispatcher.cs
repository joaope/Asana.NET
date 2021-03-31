using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.OAuth
{
    public sealed class AsanaOAuthDispatcher : Dispatcher
    {
        private readonly IAsanaOAuthApplication _asanaOAuthApplication;

        public AsanaOAuthDispatcher(
            IAsanaOAuthApplication asanaOAuthApplication,
            HttpClient httpClient,
            AsanaClientOptions options) 
            : base(httpClient, options)
        {
            _asanaOAuthApplication = asanaOAuthApplication;
        }

        public AsanaOAuthDispatcher(IAsanaOAuthApplication asanaOAuthApplication, AsanaClientOptions options) 
            : this(asanaOAuthApplication, new HttpClient(), options)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            if (_asanaOAuthApplication.LatestTokenResponse != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    _asanaOAuthApplication.LatestTokenResponse.TokenType,
                    _asanaOAuthApplication.LatestTokenResponse.AccessToken);
            }
        }
    }
}