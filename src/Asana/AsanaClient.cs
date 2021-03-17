using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Asana.Resources;

namespace Asana
{
    public sealed class AsanaClient : IAsanaClient
    {
        private AsanaClientOptions Options { get; }
        public Dispatcher Dispatcher { get; }

        public AsanaClient(Dispatcher dispatcher, AsanaClientOptions options)
        {
            Options = options;
            Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public AsanaClient(Dispatcher dispatcher) : this(dispatcher, AsanaClientOptions.Default)
        {
        }

        public AsanaClient(string accessToken, AsanaClientOptions options)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            Options = options;
            Dispatcher = new AccessTokenDispatcher(accessToken, Options);
        }

        public AsanaClient(string accessToken) : this(accessToken, AsanaClientOptions.Default)
        {
        }

        public Attachments Attachments => new Attachments(Dispatcher, Options.DefaultPageSize);
        public BatchApi BatchApi => new BatchApi(Dispatcher);
        public CustomFields CustomFields => new CustomFields(Dispatcher, Options.DefaultPageSize);
        public CustomFieldSettings CustomFieldSettings => new CustomFieldSettings(Dispatcher, Options.DefaultPageSize);
        public Events Events => new Events(Dispatcher);
        public Jobs Jobs => new Jobs(Dispatcher);
        public OrganizationExports OrganizationExports => new OrganizationExports(Dispatcher);
        public Portfolios Portfolios => new Portfolios(Dispatcher, Options.DefaultPageSize);
        public PortfolioMemberships PortfolioMemberships => new PortfolioMemberships(Dispatcher, Options.DefaultPageSize);
        public Projects Projects => new Projects(Dispatcher, Options.DefaultPageSize);
        public ProjectMemberships ProjectMemberships => new ProjectMemberships(Dispatcher, Options.DefaultPageSize);
        public ProjectStatuses ProjectStatuses => new ProjectStatuses(Dispatcher, Options.DefaultPageSize);
        public Sections Sections => new Sections(Dispatcher, Options.DefaultPageSize);
        public Stories Stories => new Stories(Dispatcher, Options.DefaultPageSize);
        public Tags Tags => new Tags(Dispatcher, Options.DefaultPageSize);
        public Tasks Tasks => new Tasks(Dispatcher, Options.DefaultPageSize);
        public Teams Teams => new Teams(Dispatcher, Options.DefaultPageSize);
        public TeamMemberships TeamMemberships => new TeamMemberships(Dispatcher, Options.DefaultPageSize);
        public Typeahead Typeahead => new Typeahead(Dispatcher, Options.DefaultPageSize);
        public Users Users => new Users(Dispatcher, Options.DefaultPageSize);
        public UserTaskLists UserTaskLists => new UserTaskLists(Dispatcher);
        public Webhooks Webhooks => new Webhooks(Dispatcher, Options.DefaultPageSize);
        public Workspaces Workspaces => new Workspaces(Dispatcher, Options.DefaultPageSize);
        public WorkspaceMemberships WorkspaceMemberships => new WorkspaceMemberships(Dispatcher, Options.DefaultPageSize);

        private sealed class AccessTokenDispatcher : Dispatcher
        {
            private readonly string _accessToken;

            internal AccessTokenDispatcher(string accessToken, AsanaClientOptions options)
                : base(options)
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
                }

                _accessToken = accessToken;
            }

            protected override void OnBeforeSendRequest(HttpRequestMessage request)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }
    }
}
