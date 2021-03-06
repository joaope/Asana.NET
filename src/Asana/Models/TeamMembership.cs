using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class TeamMembership : AsanaResource
    {
        [JsonProperty("is_guest")]
        public bool IsGuest { get; }
        public Team Team { get; }
        public User User { get; }

        [JsonConstructor]
        internal TeamMembership(string gid, string resourceType, bool isGuest, Team team, User user) : base(gid, resourceType)
        {
            IsGuest = isGuest;
            Team = team;
            User = user;
        }
    }
}