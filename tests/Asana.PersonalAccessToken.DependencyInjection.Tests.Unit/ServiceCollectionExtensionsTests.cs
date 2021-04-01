using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Asana.PersonalAccessToken.DependencyInjection.Tests.Unit
{
    public sealed class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void ForAGivenServiceCollection_EnsureAllServicesAreCorrectlyRegistered()
        {
            var services = new ServiceCollection().AddSingleton<IHttpClientFactory, MockHttpClientFactory>();

            var asanaAccessTokenOptions = new AsanaAccessTokenOptions("some_access_token");
            var asanaClientOptions = new AsanaClientOptions {DefaultPageSize = 45};
            services.AddAsana(asanaAccessTokenOptions, asanaClientOptions);
            var provider = services.BuildServiceProvider();

            var requiredAsanaClient = provider.GetRequiredService<IAsanaClient>();
            var requiredDispatcher = provider.GetRequiredService<Dispatcher>();
            var requiredAccessTokenOptions = provider.GetRequiredService<AsanaAccessTokenOptions>();
            var requiredAsanaClientOptions = provider.GetRequiredService<AsanaClientOptions>();

            Assert.NotNull(requiredDispatcher);
            Assert.IsType<AsanaAccessTokenDispatcher>(requiredDispatcher);
            Assert.Equal(requiredAsanaClient.Dispatcher, requiredDispatcher);
            Assert.Equal(requiredAccessTokenOptions, asanaAccessTokenOptions);
            Assert.Equal(asanaClientOptions, requiredAsanaClientOptions);
        }

        private sealed class MockHttpClientFactory : IHttpClientFactory
        {
            public HttpClient CreateClient(string name)
            {
                return new HttpClient();
            }
        }
    }
}
