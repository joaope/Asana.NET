﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Asana.OAuth
{
    public sealed class OAuthDispatcher : Dispatcher
    {
        public const string NativeRedirectUrl = "urn:ietf:wg:oauth:2.0:oob";

        private readonly string _discoveryEndpointUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string? _redirectUrl;
        private readonly DiscoveryCache _discoveryCache;
        private readonly HttpClient _authClient;

        private TokenResponse? _tokenResponse;

        public TimeSpan ApiDiscoveryCacheDuration
        {
            get => _discoveryCache.CacheDuration;
            set => _discoveryCache.CacheDuration = value;
        }

        public OAuthDispatcher(string clientId, string clientSecret, string redirectUrl, AsanaClientOptions options)
            : base(options)
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
            _discoveryEndpointUrl = options.ApiBaseUri.ToString();

            _discoveryCache = new DiscoveryCache(_discoveryEndpointUrl, new DiscoveryPolicy
            {
                ValidateEndpoints = false
            });

            _authClient = new HttpClient();
        }

        public OAuthDispatcher(string clientId, string clientSecret, string redirectUrl)
            : this(clientId, clientSecret, redirectUrl, AsanaClientOptions.Default)
        {
        }

        public OAuthDispatcher(string clientId, string clientSecret) : this(clientId, clientSecret, NativeRedirectUrl)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            if (_tokenResponse != null)
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue(_tokenResponse.TokenType, _tokenResponse.AccessToken);
            }
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
                _tokenResponse = null;

                throw new OAuthException(
                    "Error while authorizing OAuth code. Check exception properties for more detailed information.",
                    response.Error,
                    response.ErrorDescription,
                    response.HttpErrorReason,
                    response.ErrorType,
                    response.Exception);
            }

            _tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Raw);
            return _tokenResponse;
        }

        public Task<TokenResponse> RefreshToken() => InternalRefreshToken(false)!;

        private async Task<TokenResponse?> InternalRefreshToken(bool quiet)
        {
            if (_tokenResponse == null)
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
                RefreshToken = _tokenResponse.RefreshToken
            });

            if (response.IsError)
            {
                _tokenResponse = null;

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

            _tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Raw);
            return _tokenResponse;
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

        private static string Sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
