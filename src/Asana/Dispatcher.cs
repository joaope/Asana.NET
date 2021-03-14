using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Asana
{
    public abstract class Dispatcher
    {
        public static Uri ApiBaseUri => new Uri("https://app.asana.com/api/1.0/");

        private readonly RetryPolicyOptions _retryPolicy;
        private readonly HttpClient _httpClient;

        protected Dispatcher(RetryPolicyOptions retryPolicy)
        {
            _retryPolicy = retryPolicy;
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

            for (var i = 0; i < _retryPolicy.MaxRetries; i++)
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

                if (i < _retryPolicy.MaxRetries && _retryPolicy.HttpStatusCodes.Contains(response.StatusCode))
                {
                    await Task.Delay(_retryPolicy.PollInterval, cancellationToken);
                    continue;
                }

                return response;
            }

            return await _httpClient.SendAsync(request, cancellationToken);
        }
    }
}
