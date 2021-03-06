using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Section : AsanaNamedResource
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        public Project Project { get; }

        internal Section(string gid, string resourceType, string name, DateTime? createdAt, Project project) : base(gid, resourceType, name)
        {
            CreatedAt = createdAt;
            Project = project;
        }
    }
}