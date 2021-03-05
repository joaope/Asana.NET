using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class OrganizationExportResponse : AsanaResource
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        [JsonProperty("download_url")]
        public string DownloadUrl { get; }
        public Workspace Organization { get; }
        public string Name { get; }
        public string State { get; }

        [JsonConstructor]
        internal OrganizationExportResponse(
            string gid,
            string resourceType,
            DateTime? createdAt,
            string downloadUrl,
            Workspace organization,
            string name,
            string state) : base(gid, resourceType)
        {
            CreatedAt = createdAt;
            DownloadUrl = downloadUrl;
            Organization = organization;
            Name = name;
            State = state;
        }
    }
}