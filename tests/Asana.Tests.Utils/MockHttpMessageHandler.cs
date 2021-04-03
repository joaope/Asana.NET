using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Asana.Tests.Utils
{
    public sealed class MockHttpMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage LastRequest => Requests.Last();
        public List<HttpRequestMessage> Requests { get; } = new List<HttpRequestMessage>();

        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpStatusCode responseStatusCode, string content = "content", string mediaType = "application/json")
        {
            _response = new HttpResponseMessage(responseStatusCode)
            {
                Content = new StringContent(content, null, mediaType)
            };
        }

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);

            return Task.FromResult(_response);
        }

        public static MockHttpMessageHandler BadRequest => new MockHttpMessageHandler(HttpStatusCode.BadRequest);

        public static MockHttpMessageHandler Ok => new MockHttpMessageHandler(HttpStatusCode.OK);

        public static MockHttpMessageHandler NotFound => new MockHttpMessageHandler(HttpStatusCode.NotFound);
    }
}
