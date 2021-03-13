using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Portfolios : Resource
    {
        internal Portfolios(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Portfolio> GetAll()
        {
            return new GetItemsCollectionRequest<Portfolio>(Dispatcher, "portfolios");
        }

        public GetItemRequest<Portfolio> Get(string portfolioGid)
        {
            return new GetItemRequest<Portfolio>(Dispatcher, $"portfolios/{portfolioGid}");
        }

        public GetItemsCollectionRequest<Models.AsanaResource> GetItems(string portfolioGid)
        {
            return new GetItemsCollectionRequest<Models.AsanaResource>(Dispatcher, $"portfolios/{portfolioGid}/items");
        }

        public PostItemRequest<Portfolio> Create(object data)
        {
            return new PostItemRequest<Portfolio>(Dispatcher, "portfolios").AddData(data);
        }

        public PutItemRequest<Portfolio> Update(string portfolioGid, object data)
        {
            return new PutItemRequest<Portfolio>(Dispatcher, $"portfolios/{portfolioGid}").AddData(data);
        }

        public DeleteRequest<Portfolio> Delete(string portfolioGid)
        {
            return new DeleteRequest<Portfolio>(Dispatcher, $"portfolios/{portfolioGid}");
        }

        public PostItemRequest<EmptyData> AddItem(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/addItem").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveItem(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/removeItem").AddData(data);
        }

        public PostItemRequest<EmptyData> AddCustomField(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/addCustomFieldSetting").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveCustomField(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/removeCustomFieldSetting").AddData(data);
        }

        public PostItemRequest<EmptyData> AddUsers(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/addMembers").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveUsers(string portfolioGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"portfolios/{portfolioGid}/removeMembers").AddData(data);
        }
    }
}