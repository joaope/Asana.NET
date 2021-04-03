using System;
using System.Net.Http;

namespace Asana.Tests.Utils
{
    public sealed class MockHttpClient : HttpClient
    {
        public MockHttpMessageHandler Handler { get; }
        public HttpRequestMessage LatestRequest => Handler.LatestRequest;

        public MockHttpClient(MockHttpMessageHandler handler, Uri baseAddress)
            : base(handler)
        {
            Handler = handler;
            BaseAddress = baseAddress;
        }

        public MockHttpClient(MockHttpMessageHandler handler)
            : this(handler, new Uri("https://asana.com/"))
        {
        }

        public static MockHttpClient BadRequest => new MockHttpClient(MockHttpMessageHandler.BadRequest);

        public static MockHttpClient Ok => new MockHttpClient(MockHttpMessageHandler.Ok);

        public static MockHttpClient NotFound => new MockHttpClient(MockHttpMessageHandler.NotFound);
    }
}
