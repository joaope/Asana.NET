using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Asana
{
    public abstract class Dispatcher
    {
        private readonly AsanaClientOptions _options;

        private readonly HttpClient _httpClient;

        protected Dispatcher(HttpClient httpClient, AsanaClientOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = options.ApiBaseUri;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _options = options;
        }

        protected Dispatcher(AsanaClientOptions options) : this(new HttpClient(), options)
        {
        }

        protected abstract void OnBeforeSendRequest(HttpRequestMessage request);

        public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OnBeforeSendRequest(request);

            if (_options.Deprecations.Enabled.HasFeatures)
            {
                request.Headers.Add("Asana-Enable", _options.Deprecations.Enabled.ToString());
            }

            if (_options.Deprecations.Disabled.HasFeatures)
            {
                request.Headers.Add("Asana-Disable", _options.Deprecations.Disabled.ToString());
            }

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.Headers.TryGetValues("Asana-Change", out var changes))
            {
                return response;
            }

            var changesDescriptions = changes.Where(c => !string.IsNullOrEmpty(c)).Select(c =>
            {
                var split = c.Split(';');
                string? name = null;
                string? info = null;
                var affected = false;

                foreach (var change in split)
                {
                    var item = change.Split('=');

                    if (item.Length != 2)
                    {
                        continue;
                    }

                    switch (item[0].ToLowerInvariant())
                    {
                        case "name":
                            name = item[1];
                            break;
                        case "info":
                            info = item[1];
                            break;
                        case "affected":
                            affected = bool.TryParse(item[1], out var isAffected) && isAffected;
                            break;
                    }
                }

                return new
                {
                    Name = name,
                    Affected = affected,
                    Info = info
                };
            }).ToArray();

            if (changesDescriptions.Length == 0)
            {
                return response;
            }

            var logger = _options.Deprecations.LoggerFactory.CreateLogger<IAsanaClient>();
            
            foreach (var change in changesDescriptions)
            {
                if (_options.Deprecations.LogAffectedRequestsOnly && !change.Affected)
                {
                    continue;
                }

                if (change.Affected)
                {
                    logger.Log(
                        _options.Deprecations.AffectedLogLevel,
                        "This request is affected by the '{ChangeName}' deprecation. " +
                        "Please visit this url for more info: {ChangeInfo}. " +
                        $"Use {nameof(AsanaClientOptions)} to enable/disable features. With " +
                        "that you can opt in/out to this deprecation and suppress this logging message.",
                        change.Name,
                        change.Info);
                }
                else
                {
                    logger.Log(
                        _options.Deprecations.NotAffectedLogLevel,
                        "A new change '{ChangeName}' is being reported by Asana. This request is not affected by it. " +
                        "Please visit this url for more info: {ChangeInfo}. " +
                        $"Use {nameof(AsanaClientOptions)} to enable/disable features. With " +
                        "that you can opt in/out to this deprecation and suppress this logging message.",
                        change.Name,
                        change.Info);
                }
            }

            return response;
        }
    }
}
