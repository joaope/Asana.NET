using System;
using System.Net.Http;
using Xunit;

namespace Asana.Tests.Unit
{
    public sealed class AsanaHttpRequestExceptionTests
    {
        [Fact]
        public void PropertiesAreCorrectlyKept()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://asana.com"));
            var innerException = new InvalidOperationException("some inner message");
            var exception = new AsanaHttpRequestException("some message", request, innerException);

            Assert.Equal(exception.Request, request);
            Assert.Equal(exception.InnerException, innerException);
            Assert.Equal("some message", exception.Message);
        }
    }
}