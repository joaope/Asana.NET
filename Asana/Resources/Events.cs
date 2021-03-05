using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Events : Resource
    {
        internal Events(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Event> GetResourceEvents(string resourceGid)
        {
            return new GetItemsCollectionRequest<Event>(Dispatcher, "events")
                .AddQueryParameter("resource", resourceGid);
        }

        public GetItemsCollectionRequest<Event> GetResourceEvents(string resourceGid, string syncToken)
        {
            return new GetItemsCollectionRequest<Event>(Dispatcher, "events")
                .AddQueryParameter("resource", resourceGid)
                .AddQueryParameter("sync", syncToken);
        }
    }
}
