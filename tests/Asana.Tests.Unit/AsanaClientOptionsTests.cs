using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Asana.Tests.Unit
{
    public class AsanaClientOptionsTests
    {
        [Fact]
        public void AssertDefaultsAgainstEmptyConstructor()
        {
            var options = new AsanaClientOptions();
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
        public void AssertDefaults()
        {
            var defaultOptions = AsanaClientOptions.Default;

            Assert.Equal(new Uri("https://app.asana.com/api/1.0/"), defaultOptions.ApiBaseUri);
            Assert.Null(defaultOptions.DefaultPageSize);
            Assert.Equal(LogLevel.Warning, defaultOptions.Deprecations.AffectedLogLevel);
            Assert.Empty(defaultOptions.Deprecations.Disabled);
            Assert.False(defaultOptions.Deprecations.Disabled.HasFeatures);
            Assert.Empty(defaultOptions.Deprecations.Enabled);
            Assert.False(defaultOptions.Deprecations.Enabled.HasFeatures);
            Assert.True(defaultOptions.Deprecations.LogAffectedRequestsOnly);
            Assert.IsType<NullLogger>(defaultOptions.Deprecations.Logger);
            Assert.IsType<NullLoggerFactory>(defaultOptions.Deprecations.LoggerFactory);
            Assert.Equal(typeof(AsanaClient), defaultOptions.Deprecations.LoggerType);
            Assert.Equal(LogLevel.Information, defaultOptions.Deprecations.NotAffectedLogLevel);
        }
    }
}
