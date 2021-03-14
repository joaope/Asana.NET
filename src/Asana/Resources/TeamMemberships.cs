using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class TeamMemberships : Resource
    {
        private readonly uint? _defaultPageSize;

        public TeamMemberships(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemRequest<TeamMembership> Get(string teamMembershipGid)
        {
            return new GetItemRequest<TeamMembership>(Dispatcher, $"team_memberships/{teamMembershipGid}");
        }

        public GetItemsCollectionRequest<TeamMembership> GetByTeam(string teamGid)
        {
            return new GetItemsCollectionRequest<TeamMembership>(Dispatcher, _defaultPageSize, "team_memberships")
                .AddQueryParameter("team", teamGid);
        }

        public GetItemsCollectionRequest<TeamMembership> GetByTeam(string teamGid, string userGid, string workspaceGid)
        {
            return new GetItemsCollectionRequest<TeamMembership>(Dispatcher, _defaultPageSize, "team_memberships")
                .AddQueryParameter("team", teamGid)
                .AddQueryParameter("user", userGid)
                .AddQueryParameter("workspace", workspaceGid);
        }

        public GetItemsCollectionRequest<TeamMembership> GetFromTeam(string teamGid)
        {
            return new GetItemsCollectionRequest<TeamMembership>(Dispatcher, _defaultPageSize, $"teams/{teamGid}/team_memberships");
        }

        public GetItemsCollectionRequest<TeamMembership> GetFromUser(string userGid, string workspaceGid)
        {
            return new GetItemsCollectionRequest<TeamMembership>(Dispatcher, _defaultPageSize, $"users/{userGid}/team_memberships")
                .AddQueryParameter("workspace", workspaceGid);
        }
    }
}