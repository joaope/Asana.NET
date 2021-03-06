using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Tag : AsanaNamedResource
    {
        public string Color { get; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        public Workspace Workspace { get; }
        public User[] Followers { get; }

        [JsonConstructor]
        internal Tag(
            string gid,
            string resourceType,
            string name,
            string color,
            DateTime? createdAt,
            Workspace workspace,
            User[] followers) : base(gid, name, resourceType)
        {
            Color = color;
            CreatedAt = createdAt;
            Workspace = workspace;
            Followers = followers;
        }
    }
}