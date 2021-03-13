using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Users : Resource
    {
        internal Users(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<User> GetAll()
        {
            return new GetItemsCollectionRequest<User>(Dispatcher, "users");
        }

        public GetItemRequest<User> Get(string userGid)
        {
            return new GetItemRequest<User>(Dispatcher, $"users/{userGid}");
        }

        private GetItemsCollectionRequest<TFavouriteResource> GetFavourite<TFavouriteResource>(
            string userGid,
            string workspaceGid,
            string resourceType)
            where TFavouriteResource : Models.AsanaResource
        {
            return new GetItemsCollectionRequest<TFavouriteResource>(Dispatcher, $"users/{userGid}/favorites")
                .AddQueryParameter("workspace", workspaceGid)
                .AddQueryParameter("resource_type", resourceType);
        }

        public GetItemsCollectionRequest<Project> GetFavouriteProjects(string userGid, string workspaceGid)
        {
            return GetFavourite<Project>(userGid, workspaceGid, "project");
        }

        public GetItemsCollectionRequest<Team> GetFavouriteTeams(string userGid, string workspaceGid)
        {
            return GetFavourite<Team>(userGid, workspaceGid, "team");
        }

        public GetItemsCollectionRequest<Portfolio> GetFavouritePortfolios(string userGid, string workspaceGid)
        {
            return GetFavourite<Portfolio>(userGid, workspaceGid, "portfolio");
        }

        public GetItemsCollectionRequest<User> GetFavouriteUsers(string userGid, string workspaceGid)
        {
            return GetFavourite<User>(userGid, workspaceGid, "user");
        }

        public GetItemsCollectionRequest<Tag> GetFavouriteTags(string userGid, string workspaceGid)
        {
            return GetFavourite<Tag>(userGid, workspaceGid, "tag");
        }

        public GetItemsCollectionRequest<User> GetTeamUsers(string teamGid)
        {
            return new GetItemsCollectionRequest<User>(Dispatcher, $"teams/{teamGid}/users");
        }

        public GetItemsCollectionRequest<User> GetWorkspaceUsers(string workspaceId)
        {
            return new GetItemsCollectionRequest<User>(Dispatcher, $"workspaces/{workspaceId}/users");
        }

        public GetItemsCollectionRequest<User> GetOrganizationUsers(string organizationId) =>
            GetWorkspaceUsers(organizationId);
    }
}