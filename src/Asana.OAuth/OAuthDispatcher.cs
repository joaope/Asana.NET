using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.OAuth
{
    public sealed class OAuthDispatcher : Dispatcher
    {
        private readonly IOAuthApplication _oAuthApplication;

        public OAuthDispatcher(
            IOAuthApplication oAuthApplication,
            HttpClient httpClient,
            AsanaClientOptions options) 
            : base(httpClient, options)
        {
            _oAuthApplication = oAuthApplication;
        }

        public OAuthDispatcher(IOAuthApplication oAuthApplication, AsanaClientOptions options) 
            : this(oAuthApplication, new HttpClient(), options)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            if (_oAuthApplication.LatestTokenResponse != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    _oAuthApplication.LatestTokenResponse.TokenType,
                    _oAuthApplication.LatestTokenResponse.AccessToken);
            }
        }
    }
}