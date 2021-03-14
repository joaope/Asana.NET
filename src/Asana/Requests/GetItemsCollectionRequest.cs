using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Asana.Models;
using Asana.Models.Results;

namespace Asana.Requests
{
    public sealed class GetItemsCollectionRequest<TData> : Request where TData : class, IData
    {
        private readonly uint? _defaultPageSize;

        public GetItemsCollectionRequest(Dispatcher dispatcher, uint? defaultPageSize, string requestPath) : base(dispatcher, requestPath)
        {
            _defaultPageSize = defaultPageSize;
        }

        public new GetItemsCollectionRequest<TData> AddField(string fieldName) =>
            (GetItemsCollectionRequest<TData>)base.AddField(fieldName);

        public new GetItemsCollectionRequest<TData> AddFields(params string[] fieldNames) =>
            (GetItemsCollectionRequest<TData>)base.AddFields(fieldNames);

        public new GetItemsCollectionRequest<TData> AddFields(IEnumerable<string> fieldNames) =>
            (GetItemsCollectionRequest<TData>)base.AddFields(fieldNames);

        public new GetItemsCollectionRequest<TData> AddQueryParameter(string name, string? value) => 
            (GetItemsCollectionRequest<TData>)base.AddQueryParameter(name, value);

        public new GetItemsCollectionRequest<TData> PrettyOutput(bool pretty) =>
            (GetItemsCollectionRequest<TData>) base.PrettyOutput(pretty);

        private async Task<ResultsCollection<TData>> InternalExecute(CancellationToken cancellationToken, uint? limit, string? offset)
        {
            if (limit.HasValue)
            {
                AddQueryParameter("limit", $"{limit.Value}");
            }

            if (!string.IsNullOrEmpty(offset))
            {
                AddQueryParameter("offset", $"{offset}");
            }

            var response = await Dispatcher.Send(new HttpRequestMessage(HttpMethod.Get, RequestUrl), cancellationToken);
            return await ResultsCollection<TData>.FromHttpResponse(response);
        }

        public Task<ResultsCollection<TData>> Execute() => Execute( CancellationToken.None);

        public Task<ResultsCollection<TData>> Execute(CancellationToken cancellationToken) =>
            Execute(_defaultPageSize, cancellationToken);

        public Task<ResultsCollection<TData>> Execute(uint? limit) => Execute(limit, CancellationToken.None);

        public Task<ResultsCollection<TData>> Execute(uint? limit, CancellationToken cancellationToken)
        {
            return InternalExecute(cancellationToken, limit, null);
        }

        public Task<ResultsCollection<TData>> ExecuteOffset(string offset, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(offset))
            {
                throw new ArgumentNullException(nameof(offset));
            }

            return InternalExecute(cancellationToken, null, offset);
        }

        public Task<ResultsCollection<TData>> ExecuteOffset(string offset) =>
            ExecuteOffset(offset, CancellationToken.None);

        public IAsyncEnumerable<ResultsCollection<TData>> ExecuteAsEnumerable() =>
            ExecuteAsEnumerable(_defaultPageSize);

        public IAsyncEnumerable<ResultsCollection<TData>> ExecuteAsEnumerable(CancellationToken cancellationToken) =>
            ExecuteAsEnumerable(_defaultPageSize, cancellationToken);

        public IAsyncEnumerable<ResultsCollection<TData>> ExecuteAsEnumerable(uint? limit) =>
            ExecuteAsEnumerable(limit, CancellationToken.None);

        public async IAsyncEnumerable<ResultsCollection<TData>> ExecuteAsEnumerable(
            uint? limit,
            [EnumeratorCancellation]CancellationToken cancellationToken)
        {
            if (limit < 1 || limit > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), limit, "Limit value must be betwen 1 and 100");
            }

            var pageResult = await InternalExecute(cancellationToken, limit, null);

            yield return pageResult;

            if (pageResult.NextPage != null)
            {
                yield return await InternalExecute(cancellationToken, limit, pageResult.NextPage.Offset);
            }
        }
    }
}