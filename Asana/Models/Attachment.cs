using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Attachment : AsanaResource
    {
        public string Name { get; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }
        [JsonProperty("download_url")]
        public string DownloadUrl { get; }
        public string Host { get; }
        [JsonProperty("view_url")]
        public string ViewUrl { get; }

        public Task Parent { get; set; }

        [JsonConstructor]
        internal Attachment(
            string gid,
            string resourceType,
            string name,
            string downloadUrl,
            string host,
            string viewUrl,
            Task parent)
            : base(gid, resourceType)
        {
            Name = name;
            DownloadUrl = downloadUrl;
            Host = host;
            ViewUrl = viewUrl;
            Parent = parent;
        }
    }
}