using Asana.Resources;

namespace Asana
{
    public interface IAsanaClient
    {
        Dispatcher Dispatcher { get; }
        Attachments Attachments { get; }
        BatchApi BatchApi { get; }
        CustomFields CustomFields { get; }
        CustomFieldSettings CustomFieldSettings { get; }
        Events Events { get; }
        Jobs Jobs { get; }
        OrganizationExports OrganizationExports { get; }
        Portfolios Portfolios { get; }
        PortfolioMemberships PortfolioMemberships { get; }
        Projects Projects { get; }
        ProjectMemberships ProjectMemberships { get; }
        ProjectStatuses ProjectStatuses { get; }
        Sections Sections { get; }
        Stories Stories { get; }
        Tags Tags { get; }
        Tasks Tasks { get; }
        Teams Teams { get; }
        TeamMemberships TeamMemberships { get; }
        Typeahead Typeahead { get; }
        Users Users { get; }
        UserTaskLists UserTaskLists { get; }
        Webhooks Webhooks { get; }
        Workspaces Workspaces { get; }
        WorkspaceMemberships WorkspaceMemberships { get; }
    }
}