using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Batch : IData
    {
        public object Body { get; }
        public IReadOnlyDictionary<string, string> Headers { get; }
        [JsonProperty("status_code")]
        public HttpStatusCode StatusCode { get; }

        [JsonConstructor]
        internal Batch(object body, IDictionary<string, string>? headers, HttpStatusCode statusCode)
        {
            Body = body;
            Headers = new ReadOnlyDictionary<string, string>(headers ?? new Dictionary<string, string>());
            StatusCode = statusCode;
        }
    }
}