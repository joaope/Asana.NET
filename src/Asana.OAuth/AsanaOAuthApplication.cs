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
    public sealed class OAuthApplicationOptions
    {
        public const string NativeRedirectUrl = "urn:ietf:wg:oauth:2.0:oob";

        public string ClientId { get; }
        public string ClientSecret { get; }
        public string RedirectUrl { get; }

        public OAuthApplicationOptions(string clientId, string clientSecret, string redirectUrl)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUrl = redirectUrl;
        }

        public OAuthApplicationOptions(string clientId, string clientSecret)
            : this(clientId, clientSecret, NativeRedirectUrl)
        {
        }
    }

    public sealed class AsanaOAuthApplication : IAsanaOAuthApplication
    {
        private readonly OAuthApplicationOptions _options;
        private readonly string _discoveryEndpointUrl;
        private readonly DiscoveryCache _discoveryCache;
        private readonly HttpClient _authClient;

        public TokenResponse? LatestTokenResponse { get; private set; }

        public TimeSpan ApiDiscoveryCacheDuration
        {
            get => _discoveryCache.CacheDuration;
            set => _discoveryCache.CacheDuration = value;
        }

        public AsanaOAuthApplication(OAuthApplicationOptions oAuthApplicationOptions, AsanaClientOptions options, HttpClient authClient)
        {
            _options = oAuthApplicationOptions ?? throw new ArgumentNullException(nameof(oAuthApplicationOptions));
            _discoveryEndpointUrl = options.ApiBaseUri.ToString();

            _discoveryCache = new DiscoveryCache(_discoveryEndpointUrl, new DiscoveryPolicy
            {
                ValidateEndpoints = false
            });

            _authClient = authClient;
        }

        public AsanaOAuthApplication(OAuthApplicationOptions oAuthApplicationOptions, AsanaClientOptions options)
            : this(oAuthApplicationOptions, options, new HttpClient())
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

                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,

                Code = code,
                RedirectUri = _options.RedirectUrl
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
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
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

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Raw);

            return LatestTokenResponse = new TokenResponse(
                tokenResponse.AccessToken,
                tokenResponse.ExpiresInSeconds,
                LatestTokenResponse.RefreshToken,
                tokenResponse.AuthenticatedUser,
                tokenResponse.TokenType);
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
