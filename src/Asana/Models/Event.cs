using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Event : IData
    {
        public string Action { get; }
        public EventChange Change { get; }
        public AsanaResource Parent { get; }
        public AsanaResource AsanaResource { get; }
        public User User { get; }

        [JsonConstructor]
        internal Event(string action, EventChange change, AsanaResource parent, AsanaResource asanaResource, User user)
        {
            Action = action;
            Change = change;
            Parent = parent;
            AsanaResource = asanaResource;
            User = user;
        }
    }
}