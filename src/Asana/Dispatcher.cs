using System;
using System.Collections.Generic;
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

        public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
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

        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
