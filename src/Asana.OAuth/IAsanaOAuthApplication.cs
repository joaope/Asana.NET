using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asana.OAuth
{
    public interface IAsanaOAuthApplication
    {
        TimeSpan ApiDiscoveryCacheDuration { get; }
        TokenResponse? LatestTokenResponse { get; }
        Task<TokenResponse> AuthorizeCode(string code);
        Task<TokenResponse> RefreshToken();
        Task<string> DiscoverAuthorizationUrl(
            string clientId,
            string redirectUrl,
            string? state,
            IEnumerable<OAuthScope>? scopes);
    }
}