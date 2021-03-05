using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Asana.Models.Results
{
    public sealed class ResultsCollection<TData> where TData : class, IData
    {
        public Error[] Errors { get; } = new Error[0];
        public HttpStatusCode HttpStatusCode { get; }
        public TData[]? Data { get; }
        public NextPageInformation? NextPage { get; }
        public bool HasNextPage => NextPage != null;
        public bool IsError { get; }

        private ResultsCollection(HttpStatusCode httpStatusCode, IEnumerable<TData> data, NextPageInformation? nextPage)
        {
            Data = data?.ToArray() ?? new TData[0];
            HttpStatusCode = httpStatusCode;
            NextPage = nextPage;
        }

        private ResultsCollection(HttpStatusCode httpStatusCode, IEnumerable<Error> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors?.ToArray() ?? new Error[0];
            IsError = true;
        }

        internal static async Task<ResultsCollection<TData>> FromHttpResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var dataBody = string.IsNullOrEmpty(content)
                    ? new DataCollectionBody<TData>(new TData[0])
                    : JsonConvert.DeserializeObject<DataCollectionBody<TData>>(content);

                return new ResultsCollection<TData>(response.StatusCode, dataBody.Data, dataBody.NextPage);
            }

            var errorsBody = !string.IsNullOrEmpty(content)
                ? JsonConvert.DeserializeObject<ErrorsBody>(content)
                : new ErrorsBody(null);

            return new ResultsCollection<TData>(response.StatusCode, errorsBody.Errors);
        }
    }
}