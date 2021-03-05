using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Jobs : Resource
    {
        internal Jobs(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<Job> Get(string jobGid)
        {
            return new GetItemRequest<Job>(Dispatcher, $"jobs/{jobGid}");
        }
    }
}