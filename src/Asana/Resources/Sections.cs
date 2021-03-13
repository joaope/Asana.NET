using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Sections : Resource
    {
        public Sections(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<Section> Get(string sectionGid)
        {
            return new GetItemRequest<Section>(Dispatcher, $"sections/{sectionGid}");
        }

        public PutItemRequest<Section> Update(string sectionGid, object data)
        {
            return new PutItemRequest<Section>(Dispatcher, $"sections/{sectionGid}").AddData(data);
        }

        public DeleteRequest<EmptyData> Delete(string sectionGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"sections/{sectionGid}");
        }

        public GetItemsCollectionRequest<Section> GetByProject(string projectGid)
        {
            return new GetItemsCollectionRequest<Section>(Dispatcher, $"projects/{projectGid}/sections");
        }

        public PostItemRequest<Section> CreateInProject(string projectGid, object data)
        {
            return new PostItemRequest<Section>(Dispatcher, $"projects/{projectGid}/sections").AddData(data);
        }

        public PostItemRequest<EmptyData> AddTask(string sectionGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"sections/{sectionGid}/addTask");
        }

        public PostItemRequest<EmptyData> Insert(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/sections/insert").AddData(data);
        }
    }
}