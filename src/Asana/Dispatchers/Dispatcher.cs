using System;
using System.Net.Http;

namespace Asana.Dispatchers
{
    public abstract class Dispatcher
    {
        protected Uri ApiBaseUri { get; }

        public abstract HttpClient AuthenticatedHttpClient { get; }

        protected Dispatcher(Uri apiBaseUri)
        {
            ApiBaseUri = apiBaseUri;
        }
    }
}
