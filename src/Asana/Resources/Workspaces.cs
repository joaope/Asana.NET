using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Workspaces : Resource
    {
        private readonly uint? _defaultPageSize;

        internal Workspaces(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemRequest<Workspace> Get(string workspaceGid)
        {
            return new GetItemRequest<Workspace>(Dispatcher, $"workspaces/{workspaceGid}");
        }

        public GetItemsCollectionRequest<Workspace> GetAll()
        {
            return new GetItemsCollectionRequest<Workspace>(Dispatcher, _defaultPageSize, "workspaces");
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