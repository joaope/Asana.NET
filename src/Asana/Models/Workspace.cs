using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Workspace : AsanaResource
    {
        public const string NameFieldName = "name";
        public const string IsOrganizationFieldName = "is_organization";

        [JsonProperty(NameFieldName)]
        public string Name { get; }
        [JsonProperty(IsOrganizationFieldName)]
        public bool IsOrganization { get; }

        [JsonConstructor]
        internal Workspace(string gid, string resourceType, string name, bool isOrganization) : base(gid, resourceType)
        {
            Name = name;
            IsOrganization = isOrganization;
        }
    }
}