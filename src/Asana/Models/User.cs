using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class User : AsanaNamedResource
    {
        public string Email { get; }
        public UserPhoto Photo { get; }
        public Workspace[] Workspaces { get; }

        [JsonConstructor]
        internal User(string gid, string resourceType, string name, string email, UserPhoto photo, Workspace[] workspaces) : base(gid, resourceType, name)
        {
            Email = email;
            Photo = photo;
            Workspaces = workspaces;
        }

    }
}