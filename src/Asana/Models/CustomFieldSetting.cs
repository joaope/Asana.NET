using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class CustomFieldSetting : AsanaResource
    {
        public Project Project { get; }
        public Project Parent { get; }
        [JsonProperty("is_important")]
        public bool IsImportant { get; }

        [JsonConstructor]
        internal CustomFieldSetting(string gid, string resourceType, Project project, Project parent, bool isImportant) : base(gid, resourceType)
        {
            Project = project;
            Parent = parent;
            IsImportant = isImportant;
        }
    }
}