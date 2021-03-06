using System;
using Asana.Resources;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Webhook : AsanaResource
    {
        public bool Active { get; }
        public Resource Resource { get; }
        public string Target { get; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }
        public Filter[] Filters { get; }
        [JsonProperty("last_failure_at")]
        public DateTime? LastFailureAt { get; }
        [JsonProperty("last_failure_content")]
        public string LastFailureContent { get; }
        [JsonProperty("last_sucsess_at")]
        public DateTime? LastSuccessAt { get; }

        [JsonConstructor]
        internal Webhook(
            string gid,
            string resourceType,
            bool active,
            Resource resource,
            string target,
            DateTime? createdAt,
            Filter[] filters,
            DateTime? lastFailureAt,
            string lastFailureContent,
            DateTime? lastSuccessAt) : base(gid,
            resourceType)
        {
            Active = active;
            Resource = resource;
            Target = target;
            CreatedAt = createdAt;
            Filters = filters;
            LastFailureAt = lastFailureAt;
            LastFailureContent = lastFailureContent;
            LastSuccessAt = lastSuccessAt;
        }

        public sealed class Filter
        {
            public string Action { get; }
            public string[] Fields { get; }
            public string ResourceSubtype { get; }
            public string ResourceType { get; }

            [JsonConstructor]
            internal Filter(string action, string[] fields, string resourceSubtype, string resourceType)
            {
                Action = action;
                Fields = fields;
                ResourceSubtype = resourceSubtype;
                ResourceType = resourceType;
            }
        }
    }
}