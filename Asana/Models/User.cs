using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class User : AsanaResource
    {
        public string Name { get; }
        public string Email { get; }
        public string Photo { get; }

        [JsonConstructor]
        internal User(string gid, string resourceType, string name, string email, string photo) : base(gid, resourceType)
        {
            Name = name;
            Email = email;
            Photo = photo;
        }
    }
}