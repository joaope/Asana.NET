using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class ProjectStatus : AsanaResource
    {
        public string Color { get; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        [JsonProperty("created_by")]
        public User CreatedBy { get; }
        public string Title { get; }
        public string Text { get; }
        public User Author { get; }
        public string HtmlText { get; }
        public DateTime? ModifiedAt { get; }

        [JsonConstructor]
        internal ProjectStatus(
            string gid,
            string resourceType,
            string color,
            DateTime? createdAt,
            User createdBy,
            string title,
            string text,
            User author,
            string htmlText,
            DateTime? modifiedAt) : base(gid, resourceType)
        {
            Color = color;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            Title = title;
            Text = text;
            Author = author;
            HtmlText = htmlText;
            ModifiedAt = modifiedAt;
        }
    }
}