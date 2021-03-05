using System;
using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class EventChange
    {
        [JsonProperty("added_value")]
        public AsanaResource AddedValue { get; }
        public string Field { get; }
        [JsonProperty("new_value")]
        public AsanaResource NewValue { get; }
        [JsonProperty("removed_value")]
        public AsanaResource RemovedValue { get; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; }

        [JsonConstructor]
        internal EventChange(AsanaResource addedValue, string field, AsanaResource newValue, AsanaResource removedValue, DateTime createdAt)
        {
            AddedValue = addedValue;
            Field = field;
            NewValue = newValue;
            RemovedValue = removedValue;
            CreatedAt = createdAt;
        }
    }
}