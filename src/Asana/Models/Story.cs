using System;
using Asana.Resources;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Story : AsanaNamedResource
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        [JsonProperty("html_text")]
        public string HtmlText { get; }
        [JsonProperty("is_pinned")]
        public bool IsPinned { get; }
        [JsonProperty("resource_subtype")]
        public string ResourceSubtype { get; }
        [JsonProperty("sticker_name")]
        public string StickerName { get; }
        public string Text { get; }
        public User Assignee { get; }
        [JsonProperty("created_by")]
        public User CreatedBy { get; }
        [JsonProperty("custom_field")]
        public CustomField CustomField { get; }
        public Task Dependency { get; }
        [JsonProperty("duplicate_of")]
        public Task DuplicateOf { get; }
        [JsonProperty("duplicated_from")]
        public Task DuplicatedFrom { get; }
        public Resource Follower { get; }
        [JsonProperty("is_edited")]
        public bool IsEdited { get; }
        public bool Liked { get; }
        public User[] Likes { get; }
        [JsonProperty("new_approval_status")]
        public string NewApprovalStatus { get; }
        [JsonProperty("new_dates")]
        public Dates? NewDates { get; }
        [JsonProperty("new_enum_value")]
        public EnumOption NewEnumValue { get; }
        public string Color { get; }
        public bool Enabled { get; }
        [JsonProperty("new_name")]
        public string NewName { get; }
        [JsonProperty("new_member_value")]
        public int NewNumberValue { get; }
        [JsonProperty("new_resource_subtype")]
        public string NewResourceSubtype { get; }
        [JsonProperty("new_section")]
        public Section NewSection { get; }
        [JsonProperty("new_text_value")]
        public string NewTextValue { get; }
        [JsonProperty("num_likes")]
        public int NumberOfLikes { get; }
        [JsonProperty("old_approval_status")]
        public string OldApprovalStatus { get; set; }
        [JsonProperty("old_dates")]
        public Dates OldDates { get; }
        [JsonProperty("old_enum_value")]
        public EnumOption OldEnumValue { get; }
        [JsonProperty("old_name")]
        public string OldName { get; }
        [JsonProperty("old_number_value")]
        public int OldNumberValue { get; }
        [JsonProperty("old_resource_subtype")]
        public string OldResourceSubtype { get; }
        [JsonProperty("old_section")]
        public Section OldSection { get; }
        [JsonProperty("old_text_value")]
        public string OldTextValue { get; }
        public Preview[] Previews { get; }
        public Project Project { get; }
        public string Source { get; }
        public Tag Tag { get; }
        public Resource Target { get; }
        public Task Task { get; }

        [JsonConstructor]
        internal Story(
            string gid,
            string resourceType,
            string name,
            DateTime? createdAt,
            string htmlText,
            bool isPinned,
            string resourceSubtype,
            string stickerName,
            string text,
            User assignee,
            User createdBy,
            CustomField customField,
            Task dependency,
            Task duplicateOf,
            Task duplicatedFrom,
            Resource follower,
            bool isEdited,
            bool liked,
            User[] likes,
            string newApprovalStatus,
            Dates? newDates,
            EnumOption newEnumValue,
            string color,
            bool enabled,
            string newName,
            int newNumberValue,
            string newResourceSubtype,
            Section newSection,
            string newTextValue,
            int numberOfLikes,
            string oldApprovalStatus,
            Dates oldDates,
            EnumOption oldEnumValue,
            string oldName,
            int oldNumberValue,
            string oldResourceSubtype,
            Section oldSection,
            string oldTextValue,
            Preview[] previews,
            Project project,
            string source,
            Tag tag,
            Resource target,
            Task task) : base(gid,
            resourceType,
            name)
        {
            CreatedAt = createdAt;
            HtmlText = htmlText;
            IsPinned = isPinned;
            ResourceSubtype = resourceSubtype;
            StickerName = stickerName;
            Text = text;
            Assignee = assignee;
            CreatedBy = createdBy;
            CustomField = customField;
            Dependency = dependency;
            DuplicateOf = duplicateOf;
            DuplicatedFrom = duplicatedFrom;
            Follower = follower;
            IsEdited = isEdited;
            Liked = liked;
            Likes = likes;
            NewApprovalStatus = newApprovalStatus;
            NewDates = newDates;
            NewEnumValue = newEnumValue;
            Color = color;
            Enabled = enabled;
            NewName = newName;
            NewNumberValue = newNumberValue;
            NewResourceSubtype = newResourceSubtype;
            NewSection = newSection;
            NewTextValue = newTextValue;
            NumberOfLikes = numberOfLikes;
            OldApprovalStatus = oldApprovalStatus;
            OldDates = oldDates;
            OldEnumValue = oldEnumValue;
            OldName = oldName;
            OldNumberValue = oldNumberValue;
            OldResourceSubtype = oldResourceSubtype;
            OldSection = oldSection;
            OldTextValue = oldTextValue;
            Previews = previews;
            Project = project;
            Source = source;
            Tag = tag;
            Target = target;
            Task = task;
        }

        public sealed class Preview
        {
            public string Fallback { get; }
            public string Footer { get; }
            public string Header { get; }
            [JsonProperty("header_link")]
            public string HeaderLink { get; }
            [JsonProperty("html_text")]
            public string HtmlText { get; }
            public string Text { get; }
            public string Title { get; }
            [JsonProperty("title_link")]
            public string TitleLink { get; }

            [JsonConstructor]
            internal Preview(
                string fallback,
                string footer,
                string header,
                string headerLink,
                string htmlText,
                string text,
                string title,
                string titleLink)
            {
                Fallback = fallback;
                Footer = footer;
                Header = header;
                HeaderLink = headerLink;
                HtmlText = htmlText;
                Text = text;
                Title = title;
                TitleLink = titleLink;
            }
        }

        public sealed class Dates
        {
            [JsonProperty("due_at")]
            public DateTime? DueAt { get; }
            [JsonProperty("due_on")]
            public DateTime? DueOn { get; }
            [JsonProperty("start_on")]
            public DateTime? StartOn { get; }

            [JsonConstructor]
            internal Dates(DateTime? dueAt, DateTime? dueOn, DateTime? startOn)
            {
                DueAt = dueAt;
                DueOn = dueOn;
                StartOn = startOn;
            }
        }
    }
}