using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Asana.Models.Results
{
    public sealed class EventResult
    {
        [JsonProperty("data")] 
        public Event[] Events { get; }
        [JsonProperty("sync")] 
        public string? SyncToken { get; }
        public Error[]? Errors { get; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public bool IsError { get; private set; }

        [JsonConstructor]
        internal EventResult(Event[] events, string? syncToken)
        {
            Events = events;
            SyncToken = syncToken;
        }

        private EventResult(IEnumerable<Error> errors)
        {
            Events = new Event[0];
            Errors = errors?.ToArray() ?? new Error[0];
            IsError = true;
        }

        internal static async Task<EventResult> FromHttpResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                var eventResult = JsonConvert.DeserializeObject<EventResult>(content);
                eventResult.HttpStatusCode = response.StatusCode;

                return eventResult;
            }

            var errorsBody = !string.IsNullOrEmpty(content)
                ? JsonConvert.DeserializeObject<ErrorsBody>(content)
                : new ErrorsBody(null);

            return new EventResult(errorsBody.Errors)
            {
                HttpStatusCode = response.StatusCode
            };
        }
    }
}