using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class ProjectStatus : AsanaResource
    {
        public string Color { get; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }
        [JsonProperty("created_by")]
        public User CreatedBy { get; }
        public string Title { get; }
        public string Text { get; }

        [JsonConstructor]
        internal ProjectStatus(string gid, string resourceType, string color, DateTime createdAt, User createdBy, string title, string text) : base(gid, resourceType)
        {
            Color = color;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            Title = title;
            Text = text;
        }
    }
}