using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class EnumOption : AsanaNamedResource
    {
        public bool Enabled { get; }
        public string Color { get; }

        [JsonConstructor]
        internal EnumOption(string gid, string resourceType, string name, bool enabled, string color) : base(gid, resourceType, name)
        {
            Enabled = enabled;
            Color = color;
        }
    }
}