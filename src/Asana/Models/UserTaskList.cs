using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class UserTaskList : AsanaNamedResource
    {
        public User Owner { get; }
        public Workspace Workspace { get; }

        [JsonConstructor]
        internal UserTaskList(string gid, string resourceType, string name, User owner, Workspace workspace) : base(gid, resourceType, name)
        {
            Owner = owner;
            Workspace = workspace;
        }
    }
}