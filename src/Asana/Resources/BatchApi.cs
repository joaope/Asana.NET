using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class BatchApi : Resource
    {
        internal BatchApi(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public PostItemRequest<Batch> SubmitParallelRequests(object data)
        {
            return new PostItemRequest<Batch>(Dispatcher, "batch").AddData(data);
        }
    }
}
