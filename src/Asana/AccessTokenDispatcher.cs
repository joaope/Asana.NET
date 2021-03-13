using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Asana
{
    internal sealed class AccessTokenDispatcher : Dispatcher
    {
        private readonly string _accessToken;
        public override bool IsAuthenticated => true;

        internal AccessTokenDispatcher(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            _accessToken = accessToken;
        }

        protected override void OnBeforeSendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }
    }
}