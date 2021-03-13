using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Workspaces : Resource
    {
        internal Workspaces(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<Workspace> Get(string workspaceGid)
        {
            return new GetItemRequest<Workspace>(Dispatcher, $"workspaces/{workspaceGid}");
        }

        public GetItemsCollectionRequest<Workspace> GetAll()
        {
            return new GetItemsCollectionRequest<Workspace>(Dispatcher, "workspaces");
        }

        public PutItemRequest<Workspace> Update(string workspaceGid, object data)
        {
            return new PutItemRequest<Workspace>(Dispatcher, $"workspaces/{workspaceGid}").AddData(data);
        }

        public PutItemRequest<User> AddUser(string workspaceGid, object data)
        {
            return new PutItemRequest<User>(Dispatcher, $"workspaces/{workspaceGid}/addUser").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveUser(string workspaceGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"workspaces/{workspaceGid}").AddData(data);
        }
    }
}