using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Events : Resource
    {
        private readonly uint? _defaultPageSize;

        internal Events(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemsCollectionRequest<Event> GetResourceEvents(string resourceGid)
        {
            return new GetItemsCollectionRequest<Event>(Dispatcher, _defaultPageSize, "events")
                .AddQueryParameter("resource", resourceGid);
        }

        public GetItemsCollectionRequest<Event> GetResourceEvents(string resourceGid, string syncToken)
        {
            return new GetItemsCollectionRequest<Event>(Dispatcher, _defaultPageSize, "events")
                .AddQueryParameter("resource", resourceGid)
                .AddQueryParameter("sync", syncToken);
        }
    }
}
