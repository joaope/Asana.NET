using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Asana.OAuth
{
    public sealed class OAuthApplication : IOAuthApplication
    {
        public const string NativeRedirectUrl = "urn:ietf:wg:oauth:2.0:oob";

        private readonly string _discoveryEndpointUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string? _redirectUrl;
        private readonly DiscoveryCache _discoveryCache;
        private readonly HttpClient _authClient;

        public TokenResponse? LatestTokenResponse { get; private set; }

        public TimeSpan ApiDiscoveryCacheDuration
        {
            get => _discoveryCache.CacheDuration;
            set => _discoveryCache.CacheDuration = value;
        }

        internal OAuthApplication(Uri apiBaseUri, string clientId, string clientSecret, string redirectUrl)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(clientId));
            }

            if (string.IsNullOrEmpty(redirectUrl))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(redirectUrl));
            }

            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUrl = string.IsNullOrEmpty(redirectUrl) ? NativeRedirectUrl : redirectUrl;
            _discoveryEndpointUrl = apiBaseUri.ToString();

            _discoveryCache = new DiscoveryCache(_discoveryEndpointUrl, new DiscoveryPolicy
            {
                ValidateEndpoints = false
            });

            _authClient = new HttpClient();
        }

        internal OAuthApplication(Uri apiBaseUri, string clientId, string clientSecret)
            : this(apiBaseUri, clientId, clientSecret, NativeRedirectUrl)
        {
        }

        public async Task<TokenResponse> AuthorizeCode(string code)
        {
            var discovery = await _discoveryCache.GetAsync();

            if (discovery.IsError)
            {
                throw new OAuthException(
                    $"Error while fetching a new authorization token. OpenID Connect Discovery failed for {_discoveryEndpointUrl}. " +
                    "Check exception properties for more detailed information.",
                    discovery.Error,
                    discovery.HttpErrorReason,
                    discovery.ErrorType,
                    discovery.Exception);
            }

            var response = await _authClient.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = discovery.TokenEndpoint,

                ClientId = _clientId,
                ClientSecret = _clientSecret,

                Code = code,
                RedirectUri = _redirectUrl
            });

            if (response.IsError)
            {
                LatestTokenResponse = null;

                throw new OAuthException(
                    "Error while authorizing OAuth code. Check exception properties for more detailed information.",
                    response.Error,
                    response.ErrorDescription,
                    response.HttpErrorReason,
                    response.ErrorType,
                    response.Exception);
            }

            LatestTokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Raw);
            return LatestTokenResponse;
        }

        public Task<TokenResponse> RefreshToken() => InternalRefreshToken(false)!;

        private async Task<TokenResponse?> InternalRefreshToken(bool quiet)
        {
            if (LatestTokenResponse == null)
            {
                if (!quiet)
                {
                    throw new OAuthException(
                    "Refreshing token failed because the user is not authenticated. " +
                    $"Try starting OAuth authorization process again by calling '{nameof(AuthorizeCode)}' with a new authorization code.");
                }

                return null;
            }

            var discovery = await _discoveryCache.GetAsync();

            if (discovery.IsError)
            {
                if (!quiet)
                {
                    throw new OAuthException(
                    $"Error while refreshing access token. OpenID Connect Discovery failed for {_discoveryEndpointUrl}. " +
                    "Check exception properties for more detailed information.",
                    discovery.Error,
                    discovery.HttpErrorReason,
                    discovery.ErrorType,
                    discovery.Exception);
                }

                return null;
            }

            var response = await _authClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                RefreshToken = LatestTokenResponse.RefreshToken
            });

            if (response.IsError)
            {
                LatestTokenResponse = null;

                if (!quiet)
                {
                    throw new OAuthException(
                        "Error while refreshing authorization access token. Check exception properties for more detailed information.",
                        response.Error,
                        response.ErrorDescription,
                        response.HttpErrorReason,
                        response.ErrorType,
                        response.Exception);
                }

                return null;
            }

            LatestTokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Raw);
            return LatestTokenResponse;
        }

        public async Task<string> DiscoverAuthorizationUrl(
            string clientId,
            string redirectUrl,
            string? state,
            IEnumerable<OAuthScope>? scopes)
        {
            var discovery = await _discoveryCache.GetAsync();

            if (discovery.IsError)
            {
                throw new OAuthException(
                    $"Error while fetching authorization endpoint. OpenID Connect Discovery failed for {_discoveryEndpointUrl}. " +
                    "Check exception properties for more detailed information.",
                    discovery.Error,
                    discovery.HttpErrorReason,
                    discovery.ErrorType,
                    discovery.Exception);
            }

            scopes ??= new[] {OAuthScope.Default};

            var queryStringParams = new NameValueCollection
            {
                {"client_id", Uri.EscapeDataString(clientId)}, 
                {"redirect_uri", Uri.EscapeDataString(redirectUrl)},
                {"scope", string.Join(" ", scopes.Select(s => s.ToString().ToLowerInvariant()))},
                {"response_type", "code"}
            };

            if (!string.IsNullOrEmpty(state))
            {
                queryStringParams.Add("state", state);
            }

            static string ToQueryString(NameValueCollection source)
            {
                return string.Join("&", source
                    .AllKeys
                    .SelectMany(key => 
                        (source.GetValues(key) ?? Array.Empty<string>())
                        .Select(value => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}"))
                    .ToArray());
            }

            var uriBuilder = new UriBuilder(discovery.AuthorizeEndpoint)
            {
                Query = ToQueryString(queryStringParams)
            };

            return uriBuilder.Uri.ToString();
        }
    }
}
