using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class WorkspaceMemberships : Resource
    {
        private readonly uint? _defaultPageSize;

        public WorkspaceMemberships(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemRequest<WorkspaceMembership> Get(string workspaceMembershipGid)
        {
            return new GetItemRequest<WorkspaceMembership>(Dispatcher,
                $"workspace_memberships/{workspaceMembershipGid}");
        }

        public GetItemsCollectionRequest<WorkspaceMembership> GetForUser(string userGid)
        {
            return new GetItemsCollectionRequest<WorkspaceMembership>(
                Dispatcher,
                _defaultPageSize,
                $"users/{userGid}/workspace_memberships");
        }

        public GetItemsCollectionRequest<WorkspaceMembership> GetForWorkspace(string workspaceGid, string? userGid)
        {
            return new GetItemsCollectionRequest<WorkspaceMembership>(
                    Dispatcher,
                    _defaultPageSize,
                    $"workspaces/{workspaceGid}/workspace_memberships").AddQueryParameter("user", userGid);
        }
    }
}