using System;
using Asana.Resources;

namespace Asana
{
    public sealed class AsanaClient : IAsanaClient
    {
        private readonly AsanaClientOptions _options;
        public Dispatcher Dispatcher { get; }

        public AsanaClient(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public AsanaClient(string accessToken, AsanaClientOptions options)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            _options = options;
            Dispatcher = new AccessTokenDispatcher(_options.RetryPolicy, accessToken);
        }

        public Attachments Attachments => new Attachments(Dispatcher);
        public BatchApi BatchApi => new BatchApi(Dispatcher);
        public CustomFields CustomFields => new CustomFields(Dispatcher);
        public CustomFieldSettings CustomFieldSettings => new CustomFieldSettings(Dispatcher);
        public Events Events => new Events(Dispatcher);
        public Jobs Jobs => new Jobs(Dispatcher);
        public OrganizationExports OrganizationExports => new OrganizationExports(Dispatcher);
        public Portfolios Portfolios => new Portfolios(Dispatcher);
        public PortfolioMemberships PortfolioMemberships => new PortfolioMemberships(Dispatcher);
        public Projects Projects => new Projects(Dispatcher);
        public ProjectMemberships ProjectMemberships => new ProjectMemberships(Dispatcher);
        public ProjectStatuses ProjectStatuses => new ProjectStatuses(Dispatcher);
        public Sections Sections => new Sections(Dispatcher);
        public Stories Stories => new Stories(Dispatcher);
        public Tags Tags => new Tags(Dispatcher);
        public Tasks Tasks => new Tasks(Dispatcher);
        public Teams Teams => new Teams(Dispatcher);
        public TeamMemberships TeamMemberships => new TeamMemberships(Dispatcher);
        public Typeahead Typeahead => new Typeahead(Dispatcher);
        public Users Users => new Users(Dispatcher);
        public UserTaskLists UserTaskLists => new UserTaskLists(Dispatcher);
        public Webhooks Webhooks => new Webhooks(Dispatcher);
        public Workspaces Workspaces => new Workspaces(Dispatcher);
        public WorkspaceMemberships WorkspaceMemberships => new WorkspaceMemberships(Dispatcher);
    }
}
