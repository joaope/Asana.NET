using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Workspaces : Resource
    {
        internal Workspaces(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<Workspace> Get(string gid)
        {
            return new GetItemRequest<Workspace>(Dispatcher, $"workspaces/{gid}");
        }

        public GetItemsCollectionRequest<Workspace> GetAll()
        {
            return new GetItemsCollectionRequest<Workspace>(Dispatcher, "workspaces");
        }
    }
}