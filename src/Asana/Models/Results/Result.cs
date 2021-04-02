using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Asana.Models.Results
{
    public sealed class Result<TData> where TData : class, IData
    {
        public Error[]? Errors { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public TData? Data { get; }
        public bool IsError { get; }

        private Result(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        private Result(HttpStatusCode httpStatusCode, TData data)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
        }

        private Result(HttpStatusCode httpStatusCode, IEnumerable<Error> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors?.ToArray() ?? new Error[0];
            IsError = true;
        }

        internal static async Task<Result<TData>> FromHttpResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return string.IsNullOrEmpty(content) 
                    ? new Result<TData>(response.StatusCode) 
                    : new Result<TData>(response.StatusCode, JsonConvert.DeserializeObject<DataBody<TData>>(content)!.Data);
            }

            var errorsBody = !string.IsNullOrEmpty(content)
                ? JsonConvert.DeserializeObject<ErrorsBody>(content)
                : new ErrorsBody(null);

            return new Result<TData>(response.StatusCode, errorsBody!.Errors);
        }
    }
}
