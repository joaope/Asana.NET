using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class Job : AsanaResource
    {
        public string Name { get; }
        [JsonProperty("resource_subtype")]
        public string ResourceSubType { get; }
        public string Status { get; }
        [JsonProperty("new_task")]
        public Task NewTask { get; }
        [JsonProperty("new_project")]
        public Project NewProject { get; }

        [JsonConstructor]
        internal Job(string gid, string resourceType, string resourceSubType, string status, Task newTask, Project newProject, string name) : base(gid, resourceType)
        {
            ResourceSubType = resourceSubType;
            Status = status;
            NewTask = newTask;
            NewProject = newProject;
            Name = name;
        }
    }
}