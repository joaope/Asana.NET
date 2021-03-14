using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Asana
{
    public abstract class Dispatcher
    {
        protected AsanaClientOptions Options { get; private set; } = AsanaClientOptions.Default;

        private readonly HttpClient _httpClient;

        protected Dispatcher()
        {
            _httpClient = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") }
                }
            };
        }

        internal void Initialize(AsanaClientOptions options)
        {
            Options = options;
            _httpClient.BaseAddress = options.ApiBaseUri;
        }
        
        protected abstract void OnBefore(HttpRequestMessage request);

        public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OnBefore(request);

            for (var i = 0; i < Options.RetryPolicy.MaxRetries; i++)
            {
                HttpResponseMessage response;

                try
                {
                    response = await _httpClient.SendAsync(request, cancellationToken);
                }
                catch (HttpRequestException e)
                {
                    throw new AsanaHttpRequestException(
                        "The request failed due to an underlying issue such as network connectivity, " +
                        "DNS failure, server certificate validation or timeout.",
                        request,
                        e);
                }

                if (i < Options.RetryPolicy.MaxRetries &&
                    Options.RetryPolicy.HttpStatusCodes.Contains(response.StatusCode))
                {
                    await Task.Delay(Options.RetryPolicy.PollInterval, cancellationToken);
                    continue;
                }

                return response;
            }

            return await _httpClient.SendAsync(request, cancellationToken);
        }
    }
}
