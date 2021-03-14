using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Stories : Resource
    {
        private readonly uint? _defaultPageSize;

        public Stories(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemRequest<Story> Get(string storyGid)
        {
            return new GetItemRequest<Story>(Dispatcher, $"stories/{storyGid}");
        }

        public PutItemRequest<Story> Update(string storyGid, object data)
        {
            return new PutItemRequest<Story>(Dispatcher, $"stories/{storyGid}").AddData(data);
        }

        public DeleteRequest<EmptyData> Delete(string storyGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"stories/{storyGid}");
        }

        public GetItemsCollectionRequest<Story> GetFromTask(string taskGid)
        {
            return new GetItemsCollectionRequest<Story>(Dispatcher, _defaultPageSize, $"tasks/{taskGid}/stories");
        }

        public PostItemRequest<Story> CreateOnTask(string taskGid, object data)
        {
            return new PostItemRequest<Story>(Dispatcher, $"tasks/{taskGid}").AddData(data);
        }
    }
}