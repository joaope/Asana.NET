using System;
using System.Net.Http;

namespace Asana
{
    public sealed class AsanaHttpRequestException : Exception
    {
        public HttpRequestMessage Request { get; }

        internal AsanaHttpRequestException(string message, HttpRequestMessage request, Exception innerException)
            : base(message, innerException)
        {
            Request = request;
        }
    }
}