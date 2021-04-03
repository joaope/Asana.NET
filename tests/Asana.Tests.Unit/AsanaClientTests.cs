using Asana.Tests.Utils;
using Xunit;

namespace Asana.Tests.Unit
{
    public sealed class AsanaClientTests
    {
        [Fact]
        public void InitializesWithDefaultOptions()
        {
            var client = AsanaClient.Create();
            var options = client.Options;
            var defaultOptions = AsanaClientOptions.Default;

            Assert.Equal(defaultOptions.ApiBaseUri, options.ApiBaseUri);
            Assert.Equal(defaultOptions.DefaultPageSize, options.DefaultPageSize);
            Assert.Equal(defaultOptions.Deprecations.AffectedLogLevel, options.Deprecations.AffectedLogLevel);
            Assert.Equal(defaultOptions.Deprecations.Disabled, options.Deprecations.Disabled);
            Assert.Equal(defaultOptions.Deprecations.Disabled.HasFeatures, options.Deprecations.Disabled.HasFeatures);
            Assert.Equal(defaultOptions.Deprecations.Disabled.Count, options.Deprecations.Disabled.Count);
            Assert.Equal(defaultOptions.Deprecations.Enabled, options.Deprecations.Enabled);
            Assert.Equal(defaultOptions.Deprecations.LogAffectedRequestsOnly, options.Deprecations.LogAffectedRequestsOnly);
            Assert.Equal(defaultOptions.Deprecations.Logger, options.Deprecations.Logger);
            Assert.Equal(defaultOptions.Deprecations.LoggerFactory.GetType(), options.Deprecations.LoggerFactory.GetType());
            Assert.Equal(defaultOptions.Deprecations.LoggerType, options.Deprecations.LoggerType);
            Assert.Equal(defaultOptions.Deprecations.NotAffectedLogLevel, options.Deprecations.NotAffectedLogLevel);
        }

        [Fact]
        public void InitializesWithTheGivenDispatcher()
        {
            var dispatcher = new MockDispatcher(AsanaClientOptions.Default);
            var client = AsanaClient.Create().WithDispatcher(dispatcher);

            Assert.Equal(client.Dispatcher, dispatcher);
        }
    }
}