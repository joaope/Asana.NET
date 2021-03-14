using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Asana.Models.Results;

namespace Asana.Requests
{
    public sealed class EventRequest : Request
    {
        public EventRequest(Dispatcher dispatcher, string requestPath) : base(dispatcher, requestPath)
        {
        }

        public new EventRequest AddField(string fieldName) =>
            (EventRequest)base.AddField(fieldName);

        public new EventRequest AddFields(params string[] fieldNames) =>
            (EventRequest)base.AddFields(fieldNames);

        public new EventRequest AddFields(IEnumerable<string> fieldNames) =>
            (EventRequest)base.AddFields(fieldNames);

        public new EventRequest AddQueryParameter(string name, string? value) => (EventRequest)base.AddQueryParameter(name, value);

        public new EventRequest PrettyOutput(bool pretty) =>
            (EventRequest)base.PrettyOutput(pretty);

        public Task<EventResult> Execute() => Execute(CancellationToken.None);

        public async Task<EventResult> Execute(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, RequestUrl);

            if (Content != null)
            {
                request.Content = Content;
            }

            var response = await Dispatcher.Send(request, cancellationToken);

            return await EventResult.FromHttpResponse(response);
        }
    }
}