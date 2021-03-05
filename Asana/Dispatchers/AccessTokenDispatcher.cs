using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Asana.Dispatchers
{
    internal sealed class AccessTokenDispatcher : Dispatcher
    {
        public override HttpClient AuthenticatedHttpClient { get; }

        public AccessTokenDispatcher(Uri apiBaseUri, string accessToken) : base(apiBaseUri)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            AuthenticatedHttpClient = new HttpClient
            {
                BaseAddress = apiBaseUri,
                DefaultRequestHeaders =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            };
        }
    }
}