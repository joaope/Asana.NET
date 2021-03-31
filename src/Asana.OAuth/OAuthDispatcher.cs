using System.Net.Http;

namespace Asana.OAuth
{
    public sealed class OAuthDispatcher : Dispatcher
    {
        public OAuthDispatcher(HttpClient httpClient, AsanaClientOptions options) : base(httpClient, options)
        {
        }
    }
}