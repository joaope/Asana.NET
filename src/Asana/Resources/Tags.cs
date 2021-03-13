using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Tags : Resource
    {
        internal Tags(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Tag> GetAll()
        {
            return new GetItemsCollectionRequest<Tag>(Dispatcher, "tags");
        }

        public PostItemRequest<Tag> Create(object data)
        {
            return new PostItemRequest<Tag>(Dispatcher, "tags").AddData(data);
        }

        public GetItemRequest<Tag> Get(string tagGid)
        {
            return new GetItemRequest<Tag>(Dispatcher, $"tags/{tagGid}");
        }

        public PutItemRequest<Tag> Update(string tagGid, object data)
        {
            return new PutItemRequest<Tag>(Dispatcher, $"tags/{tagGid}").AddData(data);
        }

        public DeleteRequest<Tag> Delete(string tagGid)
        {
            return new DeleteRequest<Tag>(Dispatcher, $"tags/{tagGid}");
        }

        public GetItemsCollectionRequest<Tag> GetTaskTags(string taskGid)
        {
            return new GetItemsCollectionRequest<Tag>(Dispatcher, $"tasks/{taskGid}/tags");
        }

        public GetItemsCollectionRequest<Tag> GetWorkspaceTags(string workspaceGid)
        {
            return new GetItemsCollectionRequest<Tag>(Dispatcher, $"workspaces/{workspaceGid}/tags");
        }

        public PostItemRequest<Tag> CreateWorkspaceTag(string workspaceGid, object data)
        {
            return new PostItemRequest<Tag>(Dispatcher, $"workspaces/{workspaceGid}/tags").AddData(data);
        }
    }
}