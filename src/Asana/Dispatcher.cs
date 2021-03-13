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

        public abstract bool IsAuthenticated { get; }

        protected Dispatcher()
        {
            _httpClient = new HttpClient(new HttpMessageHandler())
            {
                BaseAddress = ApiBaseUri,
                DefaultRequestHeaders =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") }
                }
            };
        }

        protected abstract void OnBeforeSendRequest(HttpRequestMessage request, CancellationToken cancellationToken);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OnBeforeSendRequest(request, cancellationToken);
            return _httpClient.SendAsync(request, cancellationToken);
        }

        private sealed class HttpMessageHandler : DelegatingHandler
        {
            public HttpMessageHandler() : base(new HttpClientHandler())
            {
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
