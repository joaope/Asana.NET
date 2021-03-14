using System;

namespace Asana
{
    public sealed class AsanaClientOptions
    {
        private uint? _defaultPageSize;
        private RetryPolicyOptions? _retryPolicy;
        private Uri? _apiBaseUri;

        public uint? DefaultPageSize
        {
            get => _defaultPageSize ?? null;
            set => _defaultPageSize = value;
        }

        public RetryPolicyOptions RetryPolicy
        {
            get => _retryPolicy ?? RetryPolicyOptions.Default;
            set => _retryPolicy = value;
        }

        public Uri ApiBaseUri
        {
            get => _apiBaseUri ?? new Uri("https://app.asana.com/api/1.0/");
            set => _apiBaseUri = value;
        }

        public static AsanaClientOptions Default => new AsanaClientOptions();
    }
}