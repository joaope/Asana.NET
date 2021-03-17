using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Asana
{
    public sealed class AsanaClientOptions
    {
        private uint? _defaultPageSize;
        private Uri? _apiBaseUri;

        public uint? DefaultPageSize
        {
            get => _defaultPageSize ?? null;
            set => _defaultPageSize = value;
        }

        public Uri ApiBaseUri
        {
            get => _apiBaseUri ?? new Uri("https://app.asana.com/api/1.0/");
            set => _apiBaseUri = value;
        }

        public DeprecationsOptions Deprecations { get; } = new DeprecationsOptions();

        public static AsanaClientOptions Default => new AsanaClientOptions();

        public sealed class DeprecationsOptions
        {
            private ILoggerFactory? _loggerFactory;
            private Type? _loggerType;
            public Features Enabled { get; } = new Features();

            public Features Disabled { get; } = new Features();

            public Type LoggerType
            {
                get => _loggerType ?? typeof(AsanaClient);
                set => _loggerType = value;
            }

            public ILoggerFactory LoggerFactory
            {
                get => _loggerFactory ?? new NullLoggerFactory();
                set
                {
                    _loggerFactory = value;
                    Logger = _loggerFactory.CreateLogger(LoggerType);
                }
            }

            public ILogger Logger { get; private set; }

            public LogLevel AffectedLogLevel { get; set; } = LogLevel.Warning;

            public LogLevel NotAffectedLogLevel { get; set; } = LogLevel.Information;

            public bool LogAffectedRequestsOnly { get; } = true;

            public DeprecationsOptions()
            {
                Logger = LoggerFactory.CreateLogger(LoggerType);
            }
        }

        public sealed class Features : IEnumerable<string>
        {
            private readonly List<string> _features = new List<string>();

            public void Add(params string[] features)
            {
                _features.AddRange(features);
            }

            public void Remove(string feature)
            {
                _features.Remove(feature);
            }

            public void RemoveAll()
            {
                _features.RemoveAll(f => true);
            }

            public IEnumerator<string> GetEnumerator()
            {
                return _features.GetEnumerator();
            }

            public bool HasFeatures => _features.Count > 0;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return string.Join(",", _features);
            }
        }
    }
}