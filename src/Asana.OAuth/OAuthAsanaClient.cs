using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asana.Resources;

namespace Asana.OAuth
{
    public sealed class OAuthAsanaClient : IAsanaClient, IOAuthApplication
    {
        private readonly IAsanaClient _innerClient;
        private readonly IOAuthApplication _innerOAuthApplication;

        internal OAuthAsanaClient(IAsanaClient innerClient, IOAuthApplication innerOAuthApplication)
        {
            _innerClient = innerClient;
            _innerOAuthApplication = innerOAuthApplication;
        }

        public Dispatcher Dispatcher => _innerClient.Dispatcher;
        public Attachments Attachments => _innerClient.Attachments;
        public BatchApi BatchApi => _innerClient.BatchApi;
        public CustomFields CustomFields => _innerClient.CustomFields;
        public CustomFieldSettings CustomFieldSettings => _innerClient.CustomFieldSettings;
        public Events Events => _innerClient.Events;
        public Jobs Jobs => _innerClient.Jobs;
        public OrganizationExports OrganizationExports => _innerClient.OrganizationExports;
        public Portfolios Portfolios => _innerClient.Portfolios;
        public PortfolioMemberships PortfolioMemberships => _innerClient.PortfolioMemberships;
        public Projects Projects => _innerClient.Projects;
        public ProjectMemberships ProjectMemberships => _innerClient.ProjectMemberships;
        public ProjectStatuses ProjectStatuses => _innerClient.ProjectStatuses;
        public Sections Sections => _innerClient.Sections;
        public Stories Stories => _innerClient.Stories;
        public Tags Tags => _innerClient.Tags;
        public Tasks Tasks => _innerClient.Tasks;
        public Teams Teams => _innerClient.Teams;
        public TeamMemberships TeamMemberships => _innerClient.TeamMemberships;
        public Typeahead Typeahead => _innerClient.Typeahead;
        public Users Users => _innerClient.Users;
        public UserTaskLists UserTaskLists => _innerClient.UserTaskLists;
        public Webhooks Webhooks => _innerClient.Webhooks;
        public Workspaces Workspaces => _innerClient.Workspaces;
        public WorkspaceMemberships WorkspaceMemberships => _innerClient.WorkspaceMemberships;
        public TimeSpan ApiDiscoveryCacheDuration => _innerOAuthApplication.ApiDiscoveryCacheDuration;
        public TokenResponse? LatestTokenResponse => _innerOAuthApplication.LatestTokenResponse;
        public Task<TokenResponse> AuthorizeCode(string code) => _innerOAuthApplication.AuthorizeCode(code);

        public Task<TokenResponse> RefreshToken() => _innerOAuthApplication.RefreshToken();

        public Task<string> DiscoverAuthorizationUrl(
            string clientId,
            string redirectUrl,
            string? state,
            IEnumerable<OAuthScope>? scopes) => _innerOAuthApplication.DiscoverAuthorizationUrl(clientId, redirectUrl, state, scopes);
    }
}