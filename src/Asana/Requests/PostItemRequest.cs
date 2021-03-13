using System.Net.Http;
using Asana.Models;

namespace Asana.Requests
{
    public sealed class PostItemRequest<TData> : ItemRequest<TData> where TData : class, IData
    {
        protected override HttpMethod HttpMethod => HttpMethod.Post;

        public PostItemRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }

        public new PostItemRequest<TData> AddData(object data) => (PostItemRequest<TData>)base.AddData(data);

        public new PostItemRequest<TData> PrettyOutput(bool pretty) => (PostItemRequest<TData>)base.PrettyOutput(pretty);
    }
}