using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class WorkspaceMemberships : Resource
    {
        public WorkspaceMemberships(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<WorkspaceMembership> Get(string workspaceMembershipGid)
        {
            return new GetItemRequest<WorkspaceMembership>(Dispatcher,
                $"workspace_memberships/{workspaceMembershipGid}");
        }

        public GetItemsCollectionRequest<WorkspaceMembership> GetForUser(string userGid)
        {
            return new GetItemsCollectionRequest<WorkspaceMembership>(Dispatcher,
                $"users/{userGid}/workspace_memberships");
        }

        public GetItemsCollectionRequest<WorkspaceMembership> GetForWorkspace(string workspaceGid, string? userGid)
        {
            return new GetItemsCollectionRequest<WorkspaceMembership>(Dispatcher, $"workspaces/{workspaceGid}/workspace_memberships")
                .AddQueryParameter("user", userGid);
        }
    }
}