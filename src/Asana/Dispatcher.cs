using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Asana
{
    public abstract class Dispatcher
    {
        public static Uri ApiBaseUri => new Uri("https://app.asana.com/api/1.0/");

        private readonly HttpClient _httpClient;

        protected Dispatcher()
        {
            _httpClient = new HttpClient(new InternalHttpMessageHandler(HandleSend))
            {
                BaseAddress = ApiBaseUri,
                DefaultRequestHeaders =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") }
                }
            };
        }

        protected abstract Task<HttpResponseMessage> HandleSend(HttpRequestMessage request, CancellationToken cancellationToken);

        public Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _httpClient.SendAsync(request, cancellationToken);
        }

        private sealed class InternalHttpMessageHandler : DelegatingHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendHandler;

            public InternalHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendHandler) 
                : base(new HttpClientHandler())
            {
                _sendHandler = sendHandler;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken) => _sendHandler(request, cancellationToken);
        }
    }
}
