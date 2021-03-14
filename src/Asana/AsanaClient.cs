using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Asana.Resources;

namespace Asana
{
    public sealed class AsanaClient : IAsanaClient
    {
        private readonly AsanaClientOptions _options;
        public Dispatcher Dispatcher { get; }

        public AsanaClient(Dispatcher dispatcher, AsanaClientOptions options)
        {
            _options = options;

            Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            Dispatcher.Initialize(options);
        }

        public AsanaClient(Dispatcher dispatcher) : this(dispatcher, AsanaClientOptions.Default) { }

        public AsanaClient(string accessToken, AsanaClientOptions options)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            _options = options;

            Dispatcher = new AccessTokenDispatcher(accessToken);
            Dispatcher.Initialize(options);
        }

        public AsanaClient(string accessToken) : this(accessToken, AsanaClientOptions.Default) { }

        public Attachments Attachments => new Attachments(Dispatcher, _options.DefaultPageSize);
        public BatchApi BatchApi => new BatchApi(Dispatcher);
        public CustomFields CustomFields => new CustomFields(Dispatcher, _options.DefaultPageSize);
        public CustomFieldSettings CustomFieldSettings => new CustomFieldSettings(Dispatcher, _options.DefaultPageSize);
        public Events Events => new Events(Dispatcher, _options.DefaultPageSize);
        public Jobs Jobs => new Jobs(Dispatcher);
        public OrganizationExports OrganizationExports => new OrganizationExports(Dispatcher);
        public Portfolios Portfolios => new Portfolios(Dispatcher, _options.DefaultPageSize);
        public PortfolioMemberships PortfolioMemberships => new PortfolioMemberships(Dispatcher, _options.DefaultPageSize);
        public Projects Projects => new Projects(Dispatcher, _options.DefaultPageSize);
        public ProjectMemberships ProjectMemberships => new ProjectMemberships(Dispatcher, _options.DefaultPageSize);
        public ProjectStatuses ProjectStatuses => new ProjectStatuses(Dispatcher, _options.DefaultPageSize);
        public Sections Sections => new Sections(Dispatcher, _options.DefaultPageSize);
        public Stories Stories => new Stories(Dispatcher, _options.DefaultPageSize);
        public Tags Tags => new Tags(Dispatcher, _options.DefaultPageSize);
        public Tasks Tasks => new Tasks(Dispatcher, _options.DefaultPageSize);
        public Teams Teams => new Teams(Dispatcher, _options.DefaultPageSize);
        public TeamMemberships TeamMemberships => new TeamMemberships(Dispatcher, _options.DefaultPageSize);
        public Typeahead Typeahead => new Typeahead(Dispatcher, _options.DefaultPageSize);
        public Users Users => new Users(Dispatcher, _options.DefaultPageSize);
        public UserTaskLists UserTaskLists => new UserTaskLists(Dispatcher);
        public Webhooks Webhooks => new Webhooks(Dispatcher, _options.DefaultPageSize);
        public Workspaces Workspaces => new Workspaces(Dispatcher, _options.DefaultPageSize);
        public WorkspaceMemberships WorkspaceMemberships => new WorkspaceMemberships(Dispatcher, _options.DefaultPageSize);

        private sealed class AccessTokenDispatcher : Dispatcher
        {
            private readonly string _accessToken;

            internal AccessTokenDispatcher(string accessToken)
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
                }

                _accessToken = accessToken;
            }

            protected override void OnBefore(HttpRequestMessage request)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }
    }
}
