using System;

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

        public static AsanaClientOptions Default => new AsanaClientOptions();
    }
}