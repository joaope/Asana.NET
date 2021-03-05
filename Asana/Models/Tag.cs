using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Tag : AsanaResource
    {
        public string Name { get; }
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
            User[] followers) : base(gid, resourceType)
        {
            Name = name;
            Color = color;
            CreatedAt = createdAt;
            Workspace = workspace;
            Followers = followers;
        }
    }
}