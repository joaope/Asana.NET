using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Asana.OAuth.DependencyInjection.Tests.Unit
{
    public sealed class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void ForAGivenServiceCollection_EnsureAllServicesAreCorrectlyRegistered()
        {
            var services = new ServiceCollection().AddSingleton<IHttpClientFactory, MockHttpClientFactory>();

            var options = new OAuthApplicationOptions("clientid", "clientsecret");
            var asanaClientOptions = new AsanaClientOptions { DefaultPageSize = 45 };
            services.AddAsana(options, asanaClientOptions);
            var provider = services.BuildServiceProvider();

            var asanaClient = provider.GetRequiredService<IAsanaClient>();
            var asanaOAuthApp = provider.GetRequiredService<IAsanaOAuthApplication>();
            var dispatcher = provider.GetRequiredService<Dispatcher>();
            var oAuthApplicationOptions = provider.GetRequiredService<OAuthApplicationOptions>();
            var requiredAsanaClientOptions = provider.GetRequiredService<AsanaClientOptions>();

            Assert.NotNull(asanaOAuthApp);
            Assert.NotNull(dispatcher);
            Assert.IsType<AsanaOAuthDispatcher>(dispatcher);
            Assert.NotEqual(asanaClient.Dispatcher, dispatcher);
            Assert.Equal(oAuthApplicationOptions, options);
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
