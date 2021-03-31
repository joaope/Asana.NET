using Asana.Resources;

namespace Asana
{
    public sealed class AsanaClient : IAsanaClient, IConfigurableAsanaClient
    {
        public AsanaClientOptions Options { get; }
        public Dispatcher Dispatcher { get; private set; }

        public AsanaClient(Dispatcher dispatcher, AsanaClientOptions options)
        {
            Dispatcher = dispatcher;
            Options = options;
        }

        public static IConfigurableAsanaClient Create(AsanaClientOptions options) => new ConfigurableAsanaClient(options);

        public static IConfigurableAsanaClient Create() => Create(AsanaClientOptions.Default);

        public IAsanaClient WithDispatcher(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            return this;
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

        private sealed class ConfigurableAsanaClient : IConfigurableAsanaClient
        {
            public AsanaClientOptions Options { get; }

            public ConfigurableAsanaClient(AsanaClientOptions options)
            {
                Options = options;
            }

            public IAsanaClient WithDispatcher(Dispatcher dispatcher)
            {
                return new AsanaClient(dispatcher, Options);
            }
        }
    }
}
