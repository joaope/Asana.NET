using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Asana.OAuth
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static OAuthAsanaClient WithOAuth(
            this IConfigurableAsanaClient configurableAsanaClient,
            string clientId,
            string clientSecret,
            string redirectUrl)
        {
            var oAuthApplication = new OAuthApplication(configurableAsanaClient.Options.ApiBaseUri, clientId, clientSecret, redirectUrl);

            var oAuthDispatcher = new OAuthDispatcher(
                GetHttpClient(configurableAsanaClient.Options.ApiBaseUri, oAuthApplication), 
                configurableAsanaClient.Options);
            var asanaClient = configurableAsanaClient.WithDispatcher(oAuthDispatcher);

            return new OAuthAsanaClient(asanaClient, oAuthApplication);
        }

        public static OAuthAsanaClient WithNativeRedirectOAuth(
            this IConfigurableAsanaClient configurableAsanaClient,
            string clientId,
            string clientSecret)
        {
            return WithOAuth(configurableAsanaClient, clientId, clientSecret, OAuthApplication.NativeRedirectUrl);
        }

        private static HttpClient GetHttpClient(Uri apiBaseUri, IOAuthApplication oAuthApplication) =>
            new HttpClient(new OAuthHttpMessageHandler(oAuthApplication))
            {
                BaseAddress = apiBaseUri,
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                }
            };

        private sealed class OAuthHttpMessageHandler : DelegatingHandler
        {
            private readonly IOAuthApplication _oAuthApplication;

            public OAuthHttpMessageHandler(IOAuthApplication oAuthApplication)
            {
                _oAuthApplication = oAuthApplication;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (_oAuthApplication.LatestTokenResponse != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(
                        _oAuthApplication.LatestTokenResponse.TokenType,
                        _oAuthApplication.LatestTokenResponse.AccessToken);
                }

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}