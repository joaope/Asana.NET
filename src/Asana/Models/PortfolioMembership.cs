using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class PortfolioMembership : AsanaResource
    {
        public Portfolio Portfolio { get; }
        public User User { get; }

        [JsonConstructor]
        internal PortfolioMembership(string gid, string resourceType, Portfolio portfolio, User user) : base(gid, resourceType)
        {
            Portfolio = portfolio;
            User = user;
        }
    }
}