using Newtonsoft.Json;

namespace Asana.Models
{
    public class AsanaNamedResource : AsanaResource
    {
        public string Name { get; }

        [JsonConstructor]
        internal AsanaNamedResource(string gid, string resourceType, string name) : base(gid, resourceType)
        {
            Name = name;
        }
    }
}