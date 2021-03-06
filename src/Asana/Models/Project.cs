using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Project : AsanaNamedResource
    {
        public bool Archived { get; }
        public string Color { get; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }
        [JsonProperty("custom_fields")]
        public CustomField[] CustomFields { get; }
        [JsonProperty("custom_field_settings")]
        public CustomFieldSetting[] CustomFieldSettings { get; }
        [JsonProperty("due_on")]
        public DateTime DueOn { get; }
        [JsonProperty("due_date")]
        public DateTime DueDate { get; }
        public User[] Followers { get; }
        public bool IsTemplate { get; }
        public User[] Members { get; }
        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; }
        public bool Public { get; }
        [JsonProperty("start_at")]
        public DateTime StartAt { get; }
        [JsonProperty("permalink_url")]
        public string PermaLinkUrl { get; }
        public User Owner { get; }
        public string Notes { get; }
        [JsonProperty("html_notes")]
        public string HtmlNotes { get; }
        public Workspace Workspace { get; }
        [JsonProperty("default_view")]
        public string DefaultView { get; }
        [JsonProperty("current_status")] 
        public ProjectStatus CurrentStatus { get; }
        // TODO: public Team Team { get; }

        [JsonConstructor]
        internal Project(
            string gid,
            string resourceType,
            string name,
            bool archived,
            string color,
            DateTime createdAt,
            CustomField[] customFields,
            CustomFieldSetting[] customFieldSettings,
            DateTime dueOn,
            DateTime dueDate,
            User[] followers,
            bool isTemplate,
            User[] members,
            DateTime modifiedAt,
            bool @public,
            DateTime startAt,
            string permaLinkUrl,
            User owner,
            string notes,
            string htmlNotes,
            Workspace workspace,
            string defaultView,
            ProjectStatus currentStatus) : base(gid, resourceType, name)
        {
            Archived = archived;
            Color = color;
            CreatedAt = createdAt;
            CustomFields = customFields;
            CustomFieldSettings = customFieldSettings;
            DueOn = dueOn;
            DueDate = dueDate;
            Followers = followers;
            IsTemplate = isTemplate;
            Members = members;
            ModifiedAt = modifiedAt;
            Public = @public;
            StartAt = startAt;
            PermaLinkUrl = permaLinkUrl;
            Owner = owner;
            Notes = notes;
            HtmlNotes = htmlNotes;
            Workspace = workspace;
            DefaultView = defaultView;
            CurrentStatus = currentStatus;
        }
    }
}
