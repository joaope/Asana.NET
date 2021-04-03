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

            public Features Enabled { get; } = new Features();

            public Features Disabled { get; } = new Features();

            public ILoggerFactory LoggerFactory
            {
                get => _loggerFactory ?? new NullLoggerFactory();
                set => _loggerFactory = value;
            }

            public LogLevel AffectedLogLevel { get; set; } = LogLevel.Warning;

            public LogLevel NotAffectedLogLevel { get; set; } = LogLevel.Information;

            public bool LogAffectedRequestsOnly { get; set; } = true;
        }

        public sealed class Features : IList<string>
        {
            private readonly List<string> _features = new List<string>();

            internal Features() { }
            
            public int Count => _features.Count;
            public bool IsReadOnly => false;

            public void Add(params string[] features) => _features.AddRange(features);

            public void Add(string feature) => _features.Add(feature);

            public void Clear() => _features.Clear();

            public bool Contains(string feature) => _features.Contains(feature);

            public void CopyTo(string[] array, int arrayIndex) => _features.CopyTo(array, arrayIndex);

            bool ICollection<string>.Remove(string feature) => _features.Remove(feature);

            public void Remove(string feature) => _features.Remove(feature);

            public void RemoveAll() => _features.RemoveAll(f => true);

            public IEnumerator<string> GetEnumerator() => _features.GetEnumerator();

            public bool HasFeatures => _features.Count > 0;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public override string ToString() => string.Join(",", _features);

            public int IndexOf(string feature) => _features.IndexOf(feature);

            public void Insert(int index, string feature) => _features.Insert(index, feature);

            public void RemoveAt(int index) => _features.RemoveAt(index);

            public string this[int index]
            {
                get => _features[index];
                set => _features[index] = value;
            }
        }
    }
}