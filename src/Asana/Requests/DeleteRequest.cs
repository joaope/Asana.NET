using System.Net.Http;
using Asana.Models;

namespace Asana.Requests
{
    public sealed class DeleteRequest<TData> : ItemRequest<TData> where TData : class, IData
    {
        protected override HttpMethod HttpMethod => HttpMethod.Delete;

        public DeleteRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }

        public new DeleteRequest<TData> PrettyOutput(bool pretty) => (DeleteRequest<TData>)base.PrettyOutput(pretty);

        public new DeleteRequest<TData> AddData(object data) => (DeleteRequest<TData>)base.AddData(data);
    }
}