using System;
using System.Net.Http;

namespace Asana.Tests.Utils
{
    public sealed class MockDispatcher : Dispatcher
    {
        private readonly Action<HttpRequestMessage> _onBeforeSendRequest = r => { };

        public MockDispatcher(
            HttpClient httpClient,
            AsanaClientOptions options,
            Action<HttpRequestMessage> onBeforeSendRequest) 
            : base(httpClient, options)
        {
            _onBeforeSendRequest = onBeforeSendRequest;
        }

        public MockDispatcher(AsanaClientOptions options, Action<HttpRequestMessage> onBeforeSendRequest) : base(options)
        {
            _onBeforeSendRequest = onBeforeSendRequest;
        }

        public MockDispatcher(AsanaClientOptions options) : base(options)
        {
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request)
        {
            _onBeforeSendRequest(request);
        }
    }
}