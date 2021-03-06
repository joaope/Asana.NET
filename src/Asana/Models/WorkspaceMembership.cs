using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class WorkspaceMembership : AsanaResource
    {
        public User User { get; }
        public Workspace Workspace { get; }
        [JsonProperty("is_active")]
        public bool IsActive { get; }
        [JsonProperty("is_active")]
        public bool IsAdmin { get; }
        [JsonProperty("is_guest")]
        public bool IsGuest { get; }
        [JsonProperty("user_task_list")]
        public UserTaskList UserTaskList { get; }

        [JsonConstructor]
        internal WorkspaceMembership(
            string gid,
            string resourceType,
            User user,
            Workspace workspace,
            bool isActive,
            bool isAdmin,
            bool isGuest,
            UserTaskList userTaskList) : base(gid,
            resourceType)
        {
            User = user;
            Workspace = workspace;
            IsActive = isActive;
            IsAdmin = isAdmin;
            IsGuest = isGuest;
            UserTaskList = userTaskList;
        }
    }
}