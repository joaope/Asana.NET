using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class ProjectMembership : AsanaResource
    {
        public User User { get; }
        public Project Project { get; }
        [JsonProperty("write_access")]
        public string WriteAccess { get; }

        [JsonConstructor]
        internal ProjectMembership(string gid, string resourceType, User user, Project project, string writeAccess) : base(gid, resourceType)
        {
            User = user;
            Project = project;
            WriteAccess = writeAccess;
        }
    }
}