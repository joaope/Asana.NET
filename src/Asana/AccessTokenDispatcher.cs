using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Asana
{
    internal sealed class AccessTokenDispatcher : Dispatcher
    {
        private readonly string _accessToken;

        protected override Task<HttpResponseMessage> HandleSend(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            return Send(request, cancellationToken);
        }

        internal AccessTokenDispatcher(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            _accessToken = accessToken;
        }
    }
}