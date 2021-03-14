using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Events : Resource
    {
        internal Events(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public EventRequest Get(string resourceGid)
        {
            return new EventRequest(Dispatcher, "events")
                .AddQueryParameter("resource", resourceGid);
        }

        public EventRequest Get(string resourceGid, string syncToken)
        {
            return new EventRequest(Dispatcher, "events")
                .AddQueryParameter("resource", resourceGid)
                .AddQueryParameter("sync", syncToken);
        }
    }
}
