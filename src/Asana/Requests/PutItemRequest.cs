using System.Net.Http;
using Asana.Models;

namespace Asana.Requests
{
    public sealed class PutItemRequest<TData> : ItemRequest<TData> where TData : class, IData
    {
        protected override HttpMethod HttpMethod => HttpMethod.Put;

        public PutItemRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }

        public new PutItemRequest<TData> AddData(object data) => (PutItemRequest<TData>)base.AddData(data);

        public new PutItemRequest<TData> PrettyOutput(bool pretty) => (PutItemRequest<TData>)base.PrettyOutput(pretty);
    }
}