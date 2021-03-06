using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Task : AsanaNamedResource
    {
        [JsonProperty("approval_status")]
        public string ApprovalStatus { get; }
        public bool Completed { get; }
        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; }
        [JsonProperty("completed_by")]
        public User CompletedBy { get; }
        public AsanaResource[] Dependencies { get; }
        public AsanaResource[] Dependents { get; }
        [JsonProperty("due_at")]
        public DateTime? DueAt { get; }
        [JsonProperty("due_on")]
        public DateTime? DueOn { get; }
        [JsonProperty("html_notes")] 
        public string HtmlNotes { get; set; }
        [JsonProperty("is_rendered_as_separator")]
        public bool IsRenderedAsSeparator { get; set; }
        public bool Liked { get; }
        public User[] Likes { get; }
        [JsonProperty("modified_at")]
        public DateTime? ModifiedAt { get; }
        public string Notes { get; }
        [JsonProperty("num_likes")]
        public int NumberOfLikes { get; }
        [JsonProperty("num_subtasks")]
        public int NumberOfSubTasks { get; }
        [JsonProperty("resource_subtype")]
        public string ResourceSubType { get; }
        [JsonProperty("start_on")]
        public DateTime? StartOn { get; }
        public User Assignee { get; }
        [JsonProperty("custom_fields")]
        public CustomField[] CustomFields { get; }
        public User[] Followers { get; }
        public AsanaResource? Parent { get; }
        [JsonProperty("permalink_url")]
        public string PermaLinkUrl { get; }
        public Project[] Projects { get; }
        public Tag[] Tags { get; }
        public Workspace Workspace { get; }

        [JsonConstructor]
        internal Task(
            string gid,
            string resourceType,
            string name,
            string approvalStatus,
            bool completed,
            DateTime? completedAt,
            User completedBy,
            AsanaResource[] dependencies,
            AsanaResource[] dependents,
            DateTime? dueAt,
            DateTime? dueOn,
            string htmlNotes,
            bool isRenderedAsSeparator,
            bool liked,
            User[] likes,
            DateTime? modifiedAt,
            string notes,
            int numberOfLikes,
            int numberOfSubTasks,
            string resourceSubType,
            DateTime? startOn,
            User assignee,
            CustomField[] customFields,
            User[] followers,
            AsanaResource? parent,
            string permaLinkUrl,
            Project[] projects,
            Tag[] tags,
            Workspace workspace) : base(gid,
            resourceType,
            name)
        {
            ApprovalStatus = approvalStatus;
            Completed = completed;
            CompletedAt = completedAt;
            CompletedBy = completedBy;
            Dependencies = dependencies;
            Dependents = dependents;
            DueAt = dueAt;
            DueOn = dueOn;
            HtmlNotes = htmlNotes;
            IsRenderedAsSeparator = isRenderedAsSeparator;
            Liked = liked;
            Likes = likes;
            ModifiedAt = modifiedAt;
            Notes = notes;
            NumberOfLikes = numberOfLikes;
            NumberOfSubTasks = numberOfSubTasks;
            ResourceSubType = resourceSubType;
            StartOn = startOn;
            Assignee = assignee;
            CustomFields = customFields;
            Followers = followers;
            Parent = parent;
            PermaLinkUrl = permaLinkUrl;
            Projects = projects;
            Tags = tags;
            Workspace = workspace;
        }
    }
}