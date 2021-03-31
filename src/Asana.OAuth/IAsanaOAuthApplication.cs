using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asana.OAuth
{
    public interface IAsanaOAuthApplication
    {
        TimeSpan ApiDiscoveryCacheDuration { get; }
        AsanaTokenResponse? LatestTokenResponse { get; }
        Task<AsanaTokenResponse> AuthorizeCode(string code);
        Task<AsanaTokenResponse> RefreshToken();
        Task<string> DiscoverAuthorizationUrl(
            string clientId,
            string redirectUrl,
            string? state,
            IEnumerable<AsanaOAuthScope>? scopes);
    }
}