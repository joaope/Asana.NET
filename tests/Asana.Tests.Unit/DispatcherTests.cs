using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Asana.Tests.Utils;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Asana.Tests.Unit
{
    public sealed class DispatcherTests
    {
        [Theory]
        [InlineData(LogLevel.Warning, LogLevel.Warning)]
        [InlineData(LogLevel.Error, LogLevel.Error)]
        public async Task OnlyLogAffectedDeprecationsWithCorrectLogLevel(
            LogLevel affectedLogLevel,
            LogLevel expectedLogLevel)
        {
            var (dispatcher, loggerFactory, _) = GetDeprecationsReadyDispatcher(new AsanaClientOptions
                {
                    Deprecations =
                    {
                        LogAffectedRequestsOnly = true,
                        AffectedLogLevel = affectedLogLevel
                    }
                },
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Headers =
                    {
                        {"Asana-Change", "name=security_headers;info=https://asa.na/api-sh;affected=true"},
                        {"Asana-Change", "name=other_change;info=https://asa.na/api-oc"}
                    }
                });

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Get, "https://asana.com/path"),
                CancellationToken.None);

            Assert.Single(loggerFactory.CreatedLoggers);
            Assert.Single(loggerFactory.CreatedLoggers[0].EmittedEvents);
            Assert.Equal(expectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[0].LogLevel);
            Assert.Equal(
                "This request is affected by the 'security_headers' deprecation. Please visit this url for more info: https://asa.na/api-sh. Use AsanaClientOptions to enable/disable features. With that you can opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[0].Message);
        }

        [Theory]
        [InlineData(LogLevel.Warning, LogLevel.Information, LogLevel.Warning, LogLevel.Information)]
        [InlineData(LogLevel.Error, LogLevel.Warning, LogLevel.Error, LogLevel.Warning)]
        [InlineData(LogLevel.None, LogLevel.None, LogLevel.None, LogLevel.None)]
        public async Task WhenLoggingAllDeprecations_ShouldEmmitLogsCorrectly(
            LogLevel affectedLogLevel,
            LogLevel notAffectedLogLevel,
            LogLevel expectedAffectedLogLevel,
            LogLevel expectedNotAffectedLogLevel)
        {
            var (dispatcher, loggerFactory, _) = GetDeprecationsReadyDispatcher(new AsanaClientOptions
                {
                    Deprecations =
                    {
                        LogAffectedRequestsOnly = false,
                        AffectedLogLevel = affectedLogLevel,
                        NotAffectedLogLevel = notAffectedLogLevel,
                        
                    }
                },
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Headers =
                    {
                        {"Asana-Change", "name=security_headers;info=https://asa.na/api-sh;affected=true"},
                        {"Asana-Change", "name=other_change;info=https://asa.na/api-oc"}
                    }
                });

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Get, "https://asana.com/path"),
                CancellationToken.None);

            Assert.Single(loggerFactory.CreatedLoggers);
            Assert.Equal(2, loggerFactory.CreatedLoggers[0].EmittedEvents.Count);
            Assert.Equal(expectedAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[0].LogLevel);
            Assert.Equal(
                "This request is affected by the 'security_headers' deprecation. Please visit this url for more " +
                "info: https://asa.na/api-sh. Use AsanaClientOptions to enable/disable features. With that you can " +
                "opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[0].Message);
            Assert.Equal(expectedNotAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[1].LogLevel);
            Assert.Equal(
                "A new change 'other_change' is being reported by Asana. This request is not affected by it. " +
                "Please visit this url for more info: https://asa.na/api-oc. Use AsanaClientOptions to enable/disable " +
                "features. With that you can opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[1].Message);
        }

        [Theory]
        [InlineData(LogLevel.Warning, LogLevel.Information, LogLevel.Warning, LogLevel.Information)]
        [InlineData(LogLevel.Error, LogLevel.Warning, LogLevel.Error, LogLevel.Warning)]
        [InlineData(LogLevel.None, LogLevel.None, LogLevel.None, LogLevel.None)]
        public async Task WhenLoggingAllDeprecations_ShouldEmmitLogsCorrectly_WhenMultipleDeprecations(
            LogLevel affectedLogLevel,
            LogLevel notAffectedLogLevel,
            LogLevel expectedAffectedLogLevel,
            LogLevel expectedNotAffectedLogLevel)
        {
            var (dispatcher, loggerFactory, _) = GetDeprecationsReadyDispatcher(new AsanaClientOptions
            {
                Deprecations =
                    {
                        LogAffectedRequestsOnly = false,
                        AffectedLogLevel = affectedLogLevel,
                        NotAffectedLogLevel = notAffectedLogLevel,

                    }
            },
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Headers =
                    {
                        {"Asana-Change", "name=security_headers1;info=https://asa.na/api-sh1;affected=true"},
                        {"Asana-Change", "name=security_headers2;info=https://asa.na/api-sh2;affected=true"},
                        {"Asana-Change", "name=other_change1;info=https://asa.na/api-oc1"},
                        {"Asana-Change", "name=other_change2;info=https://asa.na/api-oc2"}
                    }
                });

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Get, "https://asana.com/path"),
                CancellationToken.None);

            Assert.Single(loggerFactory.CreatedLoggers);
            Assert.Equal(4, loggerFactory.CreatedLoggers[0].EmittedEvents.Count);
            Assert.Equal(expectedAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[0].LogLevel);
            Assert.Equal(
                "This request is affected by the 'security_headers1' deprecation. Please visit this url for more " +
                "info: https://asa.na/api-sh1. Use AsanaClientOptions to enable/disable features. With that you can " +
                "opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[0].Message);
            Assert.Equal(expectedAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[1].LogLevel);
            Assert.Equal(
                "This request is affected by the 'security_headers2' deprecation. Please visit this url for more " +
                "info: https://asa.na/api-sh2. Use AsanaClientOptions to enable/disable features. With that you can " +
                "opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[1].Message);
            Assert.Equal(expectedNotAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[2].LogLevel);
            Assert.Equal(
                "A new change 'other_change1' is being reported by Asana. This request is not affected by it. " +
                "Please visit this url for more info: https://asa.na/api-oc1. Use AsanaClientOptions to enable/disable " +
                "features. With that you can opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[2].Message);
            Assert.Equal(expectedNotAffectedLogLevel, loggerFactory.CreatedLoggers[0].EmittedEvents[3].LogLevel);
            Assert.Equal(
                "A new change 'other_change2' is being reported by Asana. This request is not affected by it. " +
                "Please visit this url for more info: https://asa.na/api-oc2. Use AsanaClientOptions to enable/disable " +
                "features. With that you can opt in/out to this deprecation and suppress this logging message.",
                loggerFactory.CreatedLoggers[0].EmittedEvents[3].Message);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("feature1;feature2", null, "feature1,feature2", null)]
        [InlineData(null, "disable1;disable2", null, "disable1,disable2")]
        [InlineData("feature1;feature2", "disable1;disable2", "feature1,feature2", "disable1,disable2")]
        public async Task GivenEnabledDisabledFeatures_ShouldSendRequestHeadersCorrectly(
            string enabledFeatures,
            string disabledFeatures,
            string expectedEnableHeader,
            string expectedDisableHeader)
        {
            var (dispatcher, loggerFactory, httpClient) = GetDeprecationsReadyDispatcher(new AsanaClientOptions
                {
                    Deprecations =
                    {
                        Enabled = { enabledFeatures?.Split(';') ?? Array.Empty<string>() },
                        Disabled = { disabledFeatures?.Split(';') ?? Array.Empty<string>() }
                    }
                },
                new HttpResponseMessage(HttpStatusCode.OK));

            await dispatcher.Send(
                new HttpRequestMessage(HttpMethod.Get, "https://asana.com/path"),
                CancellationToken.None);

            Assert.Empty(loggerFactory.CreatedLoggers);
            Assert.Single(httpClient.Requests);
            if (string.IsNullOrEmpty(expectedEnableHeader))
            {
                Assert.False(httpClient.LastRequest.Headers.Contains("Asana-Enable"));
            }
            else
            {
                var enableHeader = httpClient.LastRequest.Headers.GetValues("Asana-Enable").ToArray();
                Assert.Single(enableHeader);
                Assert.Equal(expectedEnableHeader, enableHeader.ElementAt(0));
            }
            if (string.IsNullOrEmpty(expectedDisableHeader))
            {
                Assert.False(httpClient.LastRequest.Headers.Contains("Asana-Disable"));
            }
            else
            {
                var disableHeader = httpClient.LastRequest.Headers.GetValues("Asana-Disable").ToArray();
                Assert.Single(disableHeader);
                Assert.Equal(expectedDisableHeader, disableHeader.ElementAt(0));
            }
        }

        private static (Dispatcher Dispatcher, MockLoggerFactory LoggerFactory, MockHttpClient HttpClient) GetDeprecationsReadyDispatcher(
            AsanaClientOptions options,
            HttpResponseMessage response)
        {
            var httpClient = new MockHttpClient(response);
            var loggerFactory = new MockLoggerFactory();

            options.Deprecations.LoggerFactory = loggerFactory;

            return (new MockDispatcher(httpClient, options, message => { }), loggerFactory, httpClient);
        }
    }
}