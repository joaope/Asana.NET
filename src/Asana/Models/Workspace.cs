using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Workspace : AsanaNamedResource
    {
        [JsonProperty("is_organization")]
        public bool IsOrganization { get; }
        [JsonProperty("email_domains")]
        public string EmailDomains { get; }

        [JsonConstructor]
        internal Workspace(string gid, string resourceType, string name, bool isOrganization, string emailDomains) : base(gid, resourceType, name)
        {
            IsOrganization = isOrganization;
            EmailDomains = emailDomains;
        }
    }
}