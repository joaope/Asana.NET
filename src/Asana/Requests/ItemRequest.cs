using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Asana.Dispatchers;
using Asana.Models;
using Asana.Models.Results;

namespace Asana.Requests
{
    public abstract class ItemRequest<TData> : Request where TData : class, IData
    {
        protected abstract HttpMethod HttpMethod { get; }

        protected ItemRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }

        public new ItemRequest<TData> AddField(string fieldName) =>
            (ItemRequest<TData>) base.AddField(fieldName);

        public new ItemRequest<TData> AddFields(params string[] fieldNames) =>
            (ItemRequest<TData>) base.AddFields(fieldNames);

        public new ItemRequest<TData> AddFields(IEnumerable<string> fieldNames) =>
            (ItemRequest<TData>) base.AddFields(fieldNames);

        public new ItemRequest<TData> AddQueryParameter(string name, string value) => (ItemRequest<TData>)base.AddQueryParameter(name, value);

        public new ItemRequest<TData> PrettyOutput(bool pretty) =>
            (ItemRequest<TData>) base.PrettyOutput(pretty);

        public Task<Result<TData>> Execute() => Execute(CancellationToken.None);

        public async Task<Result<TData>> Execute(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod, RequestUrl);

            if (Content != null)
            {
                request.Content = Content;
            }

            var response = await Dispatcher.AuthenticatedHttpClient.SendAsync(request, cancellationToken);

            return await Result<TData>.FromHttpResponse(response);
        }
    }
}