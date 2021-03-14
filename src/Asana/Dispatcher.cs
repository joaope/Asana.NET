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
            _httpClient = new HttpClient
            {
                BaseAddress = ApiBaseUri,
                DefaultRequestHeaders =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") }
                }
            };
        }

        protected abstract void OnBeforeSendRequest(HttpRequestMessage request);

        public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OnBeforeSendRequest(request);

            return await _httpClient.SendAsync(request, cancellationToken);
        }
    }
}
