using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Stories : Resource
    {
        public Stories(Dispatcher dispatcher) : base(dispatcher)
        {
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
            return new GetItemsCollectionRequest<Story>(Dispatcher, $"tasks/{taskGid}/stories");
        }

        public PostItemRequest<Story> CreateOnTask(string taskGid, object data)
        {
            return new PostItemRequest<Story>(Dispatcher, $"tasks/{taskGid}").AddData(data);
        }
    }
}