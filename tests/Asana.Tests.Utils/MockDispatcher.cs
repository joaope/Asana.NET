using System;
using System.Net.Http;

namespace Asana.Tests.Utils
{
    public sealed class MockDispatcher : Dispatcher
    {
        public MockHttpClient HttpClient { get; }
        private readonly Action<HttpRequestMessage> _onBeforeSendRequest;

        public MockDispatcher(
            MockHttpClient httpClient,
            AsanaClientOptions options,
            Action<HttpRequestMessage> onBeforeSendRequest) 
            : base(httpClient, options)
        {
            HttpClient = httpClient;
            _onBeforeSendRequest = onBeforeSendRequest;
        }

        public MockDispatcher(
            MockHttpClient httpClient,
            AsanaClientOptions options)
            : this(httpClient, options, message => {})
        {
        }

        public MockDispatcher(
            AsanaClientOptions options)
            : this(new MockHttpClient(MockHttpMessageHandler.Ok), options, message => { })
        {
        }

        public MockDispatcher(
            MockHttpClient httpClient)
            : this(httpClient, AsanaClientOptions.Default, message => { })
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            _onBeforeSendRequest(request);
        }
    }
}