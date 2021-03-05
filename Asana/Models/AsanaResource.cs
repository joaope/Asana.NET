using Newtonsoft.Json;

namespace Asana.Models
{
    public class AsanaResource : IData
    {
        public string Gid { get; }
        [JsonProperty("resource_type")]
        public string ResourceType { get; }

        [JsonConstructor]
        internal AsanaResource(string gid, string resourceType)
        {
            Gid = gid;
            ResourceType = resourceType;
        }
    }
}