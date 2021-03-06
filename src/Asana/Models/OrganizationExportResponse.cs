using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class OrganizationExportResponse : AsanaNamedResource
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        [JsonProperty("download_url")]
        public string DownloadUrl { get; }
        public Workspace Organization { get; }
        public string State { get; }

        [JsonConstructor]
        internal OrganizationExportResponse(
            string gid,
            string resourceType,
            DateTime? createdAt,
            string downloadUrl,
            Workspace organization,
            string name,
            string state) : base(gid, resourceType, name)
        {
            CreatedAt = createdAt;
            DownloadUrl = downloadUrl;
            Organization = organization;
            State = state;
        }
    }
}