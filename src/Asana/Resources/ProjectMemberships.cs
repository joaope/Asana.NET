using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class ProjectMemberships : Resource
    {
        internal ProjectMemberships(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<ProjectMembership> Get(string projectMembershipGid)
        {
            return new GetItemRequest<ProjectMembership>(Dispatcher, $"project_memberships/{projectMembershipGid}");
        }

        public GetItemsCollectionRequest<ProjectMembership> GetByProject(string projectGid)
        {
            return new GetItemsCollectionRequest<ProjectMembership>(Dispatcher,
                $"projects/{projectGid}/project_memberships");
        }

        public GetItemsCollectionRequest<ProjectMembership> GetByProject(string projectGid, string userGid) =>
            GetByProject(projectGid).AddQueryParameter("user", userGid);
    }
}