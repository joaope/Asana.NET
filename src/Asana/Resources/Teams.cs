using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Teams : Resource
    {
        internal Teams(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public PostItemRequest<Team> Create(object data)
        {
            return new PostItemRequest<Team>(Dispatcher, "teams").AddData(data);
        }

        public GetItemRequest<Team> Get(string teamGid)
        {
            return new GetItemRequest<Team>(Dispatcher, $"teams/{teamGid}");
        }

        public GetItemsCollectionRequest<Team> GetOrganizationTeams(string workspaceGid)
        {
            return new GetItemsCollectionRequest<Team>(Dispatcher, $"organizations/{workspaceGid}/teams");
        }

        public GetItemsCollectionRequest<Team> GetUserTeams(string userGid)
        {
            return new GetItemsCollectionRequest<Team>(Dispatcher, $"users/{userGid}/teams");
        }

        public PostItemRequest<User> AddUser(string teamGid, object data)
        {
            return new PostItemRequest<User>(Dispatcher, $"teams/{teamGid}/addUser").AddData(data);
        }

        public DeleteRequest<EmptyData> RemoveUser(string teamGid, object data)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"teams/{teamGid}/removeUser").AddData(data);
        }
    }
}
