using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Team : AsanaNamedResource
    {
        public string Description { get; }
        [JsonProperty("html_description")]
        public string HtmlDescription { get; }
        public Workspace Organization { get; }
        [JsonProperty("permalink_url")]
        public string PermaLinkUrl { get; }

        internal Team(
            string gid,
            string resourceType,
            string name,
            string description,
            string htmlDescription,
            Workspace organization,
            string permaLinkUrl) : base(gid, resourceType, name)
        {
            Description = description;
            HtmlDescription = htmlDescription;
            Organization = organization;
            PermaLinkUrl = permaLinkUrl;
        }
    }
}