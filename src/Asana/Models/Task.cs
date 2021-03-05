using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Task : AsanaResource
    {
        public string Name { get; }
        public string ApprovalStatus { get; }
        public bool Completed { get; }
        public DateTime? CompletedAt { get; }
        public User CompletedBy { get; }
        public AsanaResource[] Dependencies { get; }
        public AsanaResource[] Dependents { get; }
        public DateTime? DueAt { get; }
        public DateTime? DueOn { get; }
        public bool Liked { get; }
        public User[] Likes { get; }
        public DateTime? ModifiedAt { get; }
        public string Notes { get; }
        public int NumberOfLikes { get; }
        public int NumberOfSubTasks { get; }
        public string ResourceSubType { get; }
        public DateTime? StartOn { get; }
        public User Assignee { get; }
        public CustomField[] CustomFields { get; }
        public User[] Followers { get; }
        public AsanaResource? Parent { get; }
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
            Workspace workspace) : base(gid, resourceType)
        {
            Name = name;
            ApprovalStatus = approvalStatus;
            Completed = completed;
            CompletedAt = completedAt;
            CompletedBy = completedBy;
            Dependencies = dependencies;
            Dependents = dependents;
            DueAt = dueAt;
            DueOn = dueOn;
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