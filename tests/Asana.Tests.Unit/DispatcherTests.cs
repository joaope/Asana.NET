using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Asana.Tests.Utils;
using Xunit;

namespace Asana.Tests.Unit
{
    public sealed partial class DispatcherTests
    {
        [Fact]
        public void AssertBaseHttpClientAddressAndHeaders()
        {
            var options = AsanaClientOptions.Default;
            var (_, _, httpClient) = GetDispatcher(options, new HttpResponseMessage(HttpStatusCode.OK));

            Assert.Equal(options.ApiBaseUri, httpClient.BaseAddress);
            Assert.Single(httpClient.DefaultRequestHeaders.Accept);
            Assert.Equal("application/json", httpClient.DefaultRequestHeaders.Accept.ToString());
        }

        [Fact]
        public void AssertBaseHttpClientAddressFromOptions()
        {
            var options = new AsanaClientOptions
            {
                ApiBaseUri = new Uri("https://asana.com/api/path")
            };
            var (_, _, httpClient) = GetDispatcher(options, new HttpResponseMessage(HttpStatusCode.OK));

            Assert.Equal(options.ApiBaseUri, httpClient.BaseAddress);
            Assert.Single(httpClient.DefaultRequestHeaders.Accept);
            Assert.Equal("application/json", httpClient.DefaultRequestHeaders.Accept.ToString());
        }

        [Fact]
        public async Task WhenSendRequest_OnBeforeSendRequestIsCalled_AndRequestIsSend()
        {
            var called = false;
            var (dispatcher, _, httpClient) = GetDispatcher(AsanaClientOptions.Default, new HttpResponseMessage(HttpStatusCode.OK),
                message => called = true);

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Post, "https://asana.com/api"),
                CancellationToken.None);

            Assert.True(called);
            Assert.Single(httpClient.Requests);
            Assert.Equal(HttpMethod.Post, httpClient.LastRequest.Method);
        }

        [Fact]
        public async Task WhenSendRequest_OnBeforeSendRequestIsCalled_AndRequestIsSend2()
        {
            var called = false;
            var (dispatcher, _, httpClient) = GetDispatcher(AsanaClientOptions.Default, new HttpResponseMessage(HttpStatusCode.OK),
                message =>
                {
                    called = true;
                    message.Headers.Add("Some-Header-Here", "custom header value");
                });

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Delete, "https://asana.com/api"),
                CancellationToken.None);

            Assert.True(called);
            Assert.Single(httpClient.Requests);
            Assert.Equal(HttpMethod.Delete, httpClient.LastRequest.Method);
            Assert.Single(httpClient.LastRequest.Headers.GetValues("Some-Header-Here"));
            Assert.Equal(
                "custom header value",
                httpClient.LastRequest.Headers.GetValues("Some-Header-Here").ElementAt(0));
        }

        private static (Dispatcher Dispatcher, MockLoggerFactory LoggerFactory, MockHttpClient HttpClient) GetDispatcher(
            AsanaClientOptions options,
            HttpResponseMessage response,
            Action<HttpRequestMessage> onBeforeSendRequest = null)
        {
            var httpClient = new MockHttpClient(response);
            var loggerFactory = new MockLoggerFactory();

            options.Deprecations.LoggerFactory = loggerFactory;

            return (new MockDispatcher(httpClient, options, onBeforeSendRequest ?? (m => {})), loggerFactory, httpClient);
        }
    }
}