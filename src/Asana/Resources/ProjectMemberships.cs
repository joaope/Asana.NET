using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class ProjectMemberships : Resource
    {
        private readonly uint? _defaultPageSize;

        internal ProjectMemberships(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemRequest<ProjectMembership> Get(string projectMembershipGid)
        {
            return new GetItemRequest<ProjectMembership>(Dispatcher, $"project_memberships/{projectMembershipGid}");
        }

        public GetItemsCollectionRequest<ProjectMembership> GetByProject(string projectGid)
        {
            return new GetItemsCollectionRequest<ProjectMembership>(
                Dispatcher,
                _defaultPageSize,
                $"projects/{projectGid}/project_memberships");
        }

        public GetItemsCollectionRequest<ProjectMembership> GetByProject(string projectGid, string userGid) =>
            GetByProject(projectGid).AddQueryParameter("user", userGid);
    }
}