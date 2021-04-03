using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Asana.Tests.Utils
{
    public sealed class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpStatusCode _responseStatusCode;
        public HttpRequestMessage LatestRequest { get; private set; }
        public List<HttpRequestMessage> Requests { get; } = new List<HttpRequestMessage>();

        private string _content;
        private string _mediaType;

        public MockHttpMessageHandler(HttpStatusCode responseStatusCode, string content = "content", string mediaType = "application/json")
        {
            _content = content;
            _mediaType = mediaType;
            _responseStatusCode = responseStatusCode;
        }

        public void SetResponse(HttpStatusCode statusCode, string content = "content", string mediaType = "application/json")
        {
            _responseStatusCode = statusCode;
            _content = content;
            _mediaType = mediaType;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);
            LatestRequest = request;
            return Task.FromResult(new HttpResponseMessage(_responseStatusCode)
            {
                Content = new StringContent(_content, null, _mediaType)
            });
        }

        public static MockHttpMessageHandler BadRequest => new MockHttpMessageHandler(HttpStatusCode.BadRequest);

        public static MockHttpMessageHandler Ok => new MockHttpMessageHandler(HttpStatusCode.OK);

        public static MockHttpMessageHandler NotFound => new MockHttpMessageHandler(HttpStatusCode.NotFound);
    }
}
