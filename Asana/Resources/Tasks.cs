using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Tasks : Resource
    {
        internal Tasks(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Task> GetFromProject(string projectGid)
        {
            return new GetItemsCollectionRequest<Task>(Dispatcher, $"projects/{projectGid}/tasks");
        }

        public DeleteRequest<Task> Delete(string taskGid)
        {
            return new DeleteRequest<Task>(Dispatcher, $"tasks/{taskGid}");
        }
    }
}