using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Portfolio : AsanaNamedResource
    {
        public User Owner { get; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        [JsonProperty("created_by")]
        public User CreatedBy { get; }
        public Workspace Workspace { get; set; }
        [JsonProperty("start_on")]
        public DateTime? StartOn { get; set; }
        [JsonProperty("due_on")]
        public DateTime? DueOn { get; set; }
        [JsonProperty("permalink_url")]
        public string PermaLink { get; set; }

        public User[] Members { get; set; }
        public string Color { get; set; }
        [JsonProperty("custom_field_settings")]
        public CustomFieldSetting[] CustomFieldSettings { get; set; }

        [JsonConstructor]
        internal Portfolio(
            string gid,
            string resourceType,
            string name,
            User owner,
            DateTime? createdAt,
            User createdBy,
            Workspace workspace,
            DateTime? startOn,
            DateTime? dueOn,
            string permaLink,
            User[] members,
            string color,
            CustomFieldSetting[] customFieldSettings) : base(gid, resourceType, name)
        {
            Owner = owner;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            Workspace = workspace;
            StartOn = startOn;
            DueOn = dueOn;
            PermaLink = permaLink;
            Members = members;
            Color = color;
            CustomFieldSettings = customFieldSettings;
        }
    }
}