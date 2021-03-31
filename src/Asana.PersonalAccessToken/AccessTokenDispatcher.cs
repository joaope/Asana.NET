using System.Net.Http;

namespace Asana.PersonalAccessToken
{
    public sealed class AccessTokenDispatcher : Dispatcher
    {
        public AccessTokenDispatcher(HttpClient httpClient, AsanaClientOptions options) : base(httpClient, options)
        {
        }
    }
}