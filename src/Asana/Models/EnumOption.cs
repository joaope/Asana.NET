using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class EnumOption : AsanaResource
    {
        public string Name { get; }
        public bool Enabled { get; }
        public string Color { get; }

        [JsonConstructor]
        internal EnumOption(string gid, string resourceType, string name, bool enabled, string color) : base(gid, resourceType)
        {
            Name = name;
            Enabled = enabled;
            Color = color;
        }
    }
}