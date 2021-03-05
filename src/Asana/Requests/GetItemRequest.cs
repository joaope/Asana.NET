using System.Net.Http;
using Asana.Dispatchers;
using Asana.Models;

namespace Asana.Requests
{
    public sealed class GetItemRequest<TData> : ItemRequest<TData> where TData : IData
    {
        protected override HttpMethod HttpMethod => HttpMethod.Get;

        public GetItemRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }
    }
}