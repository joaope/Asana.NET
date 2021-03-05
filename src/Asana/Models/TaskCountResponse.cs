using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class TaskCountResponse : IData
    {
        [JsonProperty("num_completed_milestones")]
        public int CompletedMilestones { get; }
        [JsonProperty("num_completed_tasks")]
        public int CompletedTasks { get; }
        [JsonProperty("num_incomplete_milestones")]
        public int IncompleteMilestones { get; }
        [JsonProperty("num_incomplete_tasks")]
        public int IncompleteTasks { get; }
        [JsonProperty("num_milestones")]
        public int Milestones { get; }
        [JsonProperty("num_tasks")]
        public int Tasks { get; }

        [JsonConstructor]
        public TaskCountResponse(int completedMilestones, int completedTasks, int incompleteMilestones, int incompleteTasks, int milestones, int tasks)
        {
            CompletedMilestones = completedMilestones;
            CompletedTasks = completedTasks;
            IncompleteMilestones = incompleteMilestones;
            IncompleteTasks = incompleteTasks;
            Milestones = milestones;
            Tasks = tasks;
        }
    }
}